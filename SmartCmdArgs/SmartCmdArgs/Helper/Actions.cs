using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCmdArgs.ViewModel;

namespace SmartCmdArgs.Helper
{
    public class InsertAction : IAction
    {
        private List<CmdBase> items;
        private int insertIndex;
        private CmdContainer container;

        public InsertAction(IEnumerable<CmdBase> items, int insertIndex, CmdContainer container)
        {
            this.items = items.ToList();
            this.insertIndex = insertIndex;
            this.container = container;
        }

        public void Undo()
        {
            container.Items.RemoveRange(items);
        }

        public void Redo()
        {
            container.Items.InsertRange(insertIndex, items);
        }
    }

    public class RemoveAction : IAction
    {
        private List<CmdBase> items;
        private int removeIndex;
        private CmdContainer container;

        public RemoveAction(IEnumerable<CmdBase> items, int removeIndex, CmdContainer container)
        {
            this.items = items.ToList();
            this.removeIndex = removeIndex;
            this.container = container;
        }

        public void Undo()
        {
            container.Items.InsertRange(removeIndex, items);
        }

        public void Redo()
        {
            container.Items.RemoveRange(items);
        }
    }
}
