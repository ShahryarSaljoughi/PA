using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Extensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, List<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
        public static ObservableCollection<T> ToObservableCollection<T>(this List<T> values)
        {
            return new ObservableCollection<T>(values);
        }
    }
}
