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
        public static void AddRange<T>(this ObservableCollection<T> collection, List<T>? items)
        {
            if (items is null || !items.Any()) return;
            if (collection is null) return;
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
        public static ObservableCollection<T> ToObservableCollection<T>(this List<T> values)
        {
            return new ObservableCollection<T>(values);
        }

        public static void Sort<T>(this ObservableCollection<T> collection) where T : IComparable
        {
            List<T> sorted = collection.OrderBy(x => x).ToList();
            for (int i = 0; i < sorted.Count(); i++)
                collection.Move(collection.IndexOf(sorted[i]), i);
        }
        public static void SortDescending<T>(this ObservableCollection<T> collection) where T : IComparable
        {
            List<T> sorted = collection.OrderByDescending(x => x).ToList();
            for (int i = 0; i < sorted.Count(); i++)
                collection.Move(collection.IndexOf(sorted[i]), i);
        }
        

    }
}

