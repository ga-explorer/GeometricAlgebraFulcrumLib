using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;

public static class ListExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static List<T2> SelectToList<T1, T2>(this IEnumerable<T1> list, Func<T1, T2> itemMapping)
    {
        return list.Select(itemMapping).ToList();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ImmutableList<T2> SelectToImmutableList<T1, T2>(this IEnumerable<T1> list, Func<T1, T2> itemMapping)
    {
        return list.Select(itemMapping).ToImmutableList();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T2[] SelectToArray<T1, T2>(this IEnumerable<T1> list, Func<T1, T2> itemMapping)
    {
        return list.Select(itemMapping).ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ImmutableArray<T2> SelectToImmutableArray<T1, T2>(this IEnumerable<T1> list, Func<T1, T2> itemMapping)
    {
        return list.Select(itemMapping).ToImmutableArray();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ImmutableSortedSet<T2> SelectToImmutableSortedSet<T1, T2>(this IEnumerable<T1> list, Func<T1, T2> itemMapping)
    {
        return list.Select(itemMapping).ToImmutableSortedSet();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> SubList<T>(this IReadOnlyList<T> list, int index)
    {
        if (index >= list.Count) return Array.Empty<T>();

        return list.GetItems(index, list.Count - index).ToImmutableArray();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> SubList<T>(this IReadOnlyList<T> list, int index, int count)
    {
        return list.GetItems(index, count).ToImmutableArray();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Sample<T>(this IReadOnlyList<T> sValues, int skipCount)
    {
        for (var i = 0; i < sValues.Count; i += skipCount)
            yield return sValues[i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Sample<T>(this IReadOnlyList<T> sValues, int startIndex, int skipCount)
    {
        for (var i = startIndex; i < sValues.Count; i += skipCount)
            yield return sValues[i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> GetItems<T>(this IReadOnlyList<T> list, int index, int count)
    {
        var listCount = list.Count;

        for (var i = 0; i < count; i++)
        {
            var listIndex = i + index;

            if (listIndex < listCount)
                yield return list[listIndex];
            else
                break;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> GetItemsBetween<T>(this IReadOnlyList<T> list, int index1, int index2)
    {
        return list.GetItems(index1, index2 - index1 + 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetFirstItem<T>(this IList<T> list)
    {
        return list[0];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetLastItem<T>(this IList<T> list)
    {
        return list[^1];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetAndRemoveItemAt<T>(this IList<T> list, int index)
    {
        var item = list[index];

        list.RemoveAt(index);

        return item;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IList<T> SwapItems<T>(this IList<T> list, int index1, int index2)
    {
        (list[index1], list[index2]) = (list[index2], list[index1]);

        return list;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IList<T> SetItemFirst<T>(this IList<T> list, int oldIndex)
    {
        var item = list[oldIndex];
        list.RemoveAt(oldIndex);
        list.Insert(0, item);

        return list;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IList<T> SetItemLast<T>(this IList<T> list, int oldIndex)
    {
        var item = list[oldIndex];
        list.RemoveAt(oldIndex);
        list.Add(item);

        return list;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IList<T> SetItemIndex<T>(this IList<T> list, int oldIndex, int newIndex)
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Tuple<int, T>> ToIndexItemTuples<T>(this IEnumerable<T> itemsList)
    {
        return itemsList
            .Select(
                (item, index) => Tuple.Create(index, item)
            );
    }


    private static int Merge<T>(IList<T> inputArray, IList<T> tempArray, int left, int mid, int right)
        where T : IComparable<T>
    {
        // Stores the count of swaps
        var swaps = 0;
        int i = left, j = mid, k = left;

        while (i < mid && j <= right)
        {
            if (inputArray[i].CompareTo(inputArray[j]) <= 0)
            {
                tempArray[k] = inputArray[i];
                k++; i++;
            }
            else
            {
                tempArray[k] = inputArray[j];
                k++; j++;
                swaps += mid - i;
            }
        }

        while (i < mid)
        {
            tempArray[k] = inputArray[i];
            k++; i++;
        }

        while (j <= right)
        {
            tempArray[k] = inputArray[j];
            k++; j++;
        }

        while (left <= right)
        {
            inputArray[left] = tempArray[left];
            left++;
        }

        return swaps;
    }
        
    private static int MergeInsertionSwap<T>(IList<T> inputArray, IList<T> tempArray, int left, int right)
        where T : IComparable<T>
    {
        // Stores the total count
        // of swaps required
        var swaps = 0;

        if (left >= right) 
            return swaps;

        // Find the middle index
        // splitting the two halves
        var mid = left + (right - left) / 2;

        // Count the number of swaps
        // required to sort the left sub-array
        swaps += MergeInsertionSwap(inputArray, tempArray, left, mid);

        // Count the number of swaps
        // required to sort the right sub-array
        swaps += MergeInsertionSwap(inputArray, tempArray, mid + 1, right);

        // Count the number of swaps required
        // to sort the two sorted sub-arrays
        swaps += Merge(inputArray, tempArray, left, mid + 1, right);

        return swaps;
    }
        
    /// <summary>
    /// Sort in-place the array and count the minimum number of swaps required to sort the array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="inputArray"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int SortWithSwapCount<T>(this IList<T> inputArray)
        where T : IComparable<T>
    {
        var tempArray = new T[inputArray.Count];

        return MergeInsertionSwap(inputArray, tempArray, 0, inputArray.Count - 1);
    }


    //private static int Merge(IList<int> inputArray, IList<int> tempArray, int left, int mid, int right)
    //{
    //    // Stores the count of swaps
    //    var swaps = 0;
    //    int i = left, j = mid, k = left;

    //    while (i < mid && j <= right)
    //    {
    //        if (inputArray[i] <= inputArray[j])
    //        {
    //            tempArray[k] = inputArray[i];
    //            k++; i++;
    //        }
    //        else
    //        {
    //            tempArray[k] = inputArray[j];
    //            k++; j++;
    //            swaps += mid - i;
    //        }
    //    }

    //    while (i < mid)
    //    {
    //        tempArray[k] = inputArray[i];
    //        k++; i++;
    //    }

    //    while (j <= right)
    //    {
    //        tempArray[k] = inputArray[j];
    //        k++; j++;
    //    }

    //    while (left <= right)
    //    {
    //        inputArray[left] = tempArray[left];
    //        left++;
    //    }

    //    return swaps;
    //}

    //private static int MergeInsertionSwap(IList<int> inputArray, IList<int> tempArray, int left, int right)
    //{
    //    // Stores the total count
    //    // of swaps required
    //    var swaps = 0;

    //    if (left >= right) 
    //        return swaps;

    //    // Find the middle index
    //    // splitting the two halves
    //    var mid = left + (right - left) / 2;

    //    // Count the number of swaps
    //    // required to sort the left sub-array
    //    swaps += MergeInsertionSwap(inputArray, tempArray, left, mid);

    //    // Count the number of swaps
    //    // required to sort the right sub-array
    //    swaps += MergeInsertionSwap(inputArray, tempArray, mid + 1, right);

    //    // Count the number of swaps required
    //    // to sort the two sorted sub-arrays
    //    swaps += Merge(inputArray, tempArray, left, mid + 1, right);

    //    return swaps;
    //}

    ///// <summary>
    ///// Sort the array and count the minimum number of swaps required to sort the array
    ///// </summary>
    ///// <param name="inputArray"></param>
    ///// <returns></returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static int SortWithSwapCount(this IList<int> inputArray)
    //{
    //    var tempArray = new int[inputArray.Count];

    //    return MergeInsertionSwap(inputArray, tempArray, 0, inputArray.Count - 1);
    //}
}