using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.Extensions
{
    public static class ListExtensions
    {
        public static T GetFirstItem<T>(this List<T> list)
        {
            return list[0];
        }

        public static T GetLastItem<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }

        public static T GetAndRemoveItemAt<T>(this List<T> list, int index)
        {
            var item = list[index];

            list.RemoveAt(index);

            return item;
        }

        public static List<T> SwapItems<T>(this List<T> list, int index1, int index2)
        {
            var item = list[index1];
            list[index1] = list[index2];
            list[index2] = item;

            return list;
        }

        public static List<T> SetItemFirst<T>(this List<T> list, int oldIndex)
        {
            var item = list[oldIndex];
            list.RemoveAt(oldIndex);
            list.Insert(0, item);

            return list;
        }

        public static List<T> SetItemLast<T>(this List<T> list, int oldIndex)
        {
            var item = list[oldIndex];
            list.RemoveAt(oldIndex);
            list.Add(item);

            return list;
        }

        public static List<T> SetItemIndex<T>(this List<T> list, int oldIndex, int newIndex)
        {
            var item = list[oldIndex];
            list.RemoveAt(oldIndex);

            if (newIndex <= 0)
                list.Insert(0, item);

            else if (newIndex >= list.Count)
                list.Add(item);

            else
                list.Insert(newIndex, item);

            return list;
        }

        public static IEnumerable<Tuple<int, T>> ToIndexItemTuples<T>(this IEnumerable<T> itemsList)
        {
            return itemsList
                .Select(
                    (item, index) => Tuple.Create(index, item)
                );
        }
    }
}
