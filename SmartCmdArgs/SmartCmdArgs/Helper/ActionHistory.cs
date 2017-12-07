using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCmdArgs.Helper
{
    public class ActionHistory
    {
        private IAction[] actions;

        private int tail = 0;
        private int head = 0;
        private int cur = 0;

        private bool canAddAction = true;

        private ActionGroup curGroup;

        public ActionHistory(int size)
        {
            actions = new IAction[size];
        }

        public bool AddAction(IAction action)
        {
            if (!canAddAction) return false;
            if (curGroup != null)
                curGroup.AddAction(action);
            else
            {
                cur = (cur + 1) % actions.Length;
                actions[cur] = action;
                head = cur;
                if (head == tail)
                    tail = (tail + 1) % actions.Length;
            }
            return true;
        }

        public bool Undo()
        {
            if (cur == tail) return false;
            canAddAction = false;
            actions[cur].Undo();
            canAddAction = true;
            cur = (cur - 1 + actions.Length) % actions.Length;
            return true;
        }

        public bool Redo()
        {
            if (cur == head) return false;
            cur = (cur + 1) % actions.Length;
            canAddAction = false;
            actions[cur].Redo();
            canAddAction = true;
            return true;
        }

        public bool OpenGroup()
        {
            if (curGroup != null) return false;
            curGroup = new ActionGroup();
            return true;
        }

        public bool CloseGroup()
        {
            if (curGroup == null) return false;
            var group = curGroup;
            curGroup = null;
            return AddAction(group);
        }

        public GroupContext OpenGroupContext() => new GroupContext(this);

        public class GroupContext : IDisposable
        {
            private ActionHistory owner;

            public GroupContext(ActionHistory owner)
            {
                this.owner = owner;
                this.owner.OpenGroup();
            }

            public void Dispose()
            {
                owner.CloseGroup();
            }
        }
    }

    public class ActionGroup : IAction
    {
        private List<IAction> actions = new List<IAction>();

        public void AddAction(IAction action)
        {
            actions.Add(action);
        }

        public void Undo()
        {
            for (int i = actions.Count - 1; i >= 0; i--)
            {
                actions[i].Undo();
            }
        }

        public void Redo()
        {
            foreach (var action in actions)
            {
                action.Redo();
            }
        }
    }

    public interface IAction
    {
        void Undo();
        void Redo();
    }
}
