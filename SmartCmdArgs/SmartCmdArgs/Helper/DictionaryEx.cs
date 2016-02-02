using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCmdArgs.Helper
{
    static class DictionaryEx
    {
        public static TValue GetValueOrAddDefault<TKey, TValue> (this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            TValue ret;
            // Ignore return value
            if (dictionary.TryGetValue(key, out ret))
                return ret;
            
            dictionary.Add(key, defaultValue);
            return defaultValue;
        }
    }
}
