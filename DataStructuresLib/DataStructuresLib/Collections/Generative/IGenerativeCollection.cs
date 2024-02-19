using System.Collections.Generic;

namespace DataStructuresLib.Collections.Generative;

public interface IGenerativeCollection
{
}

public interface IGenerativeCollection<out T> : IGenerativeCollection
{
    /// <summary>
    /// This is the value returned if the given index of GetItem is out of the range
    /// of allowed indices in the collection
    /// </summary>
    T DefaultValue { get; }

    /// <summary>
    /// Read an item by its index from this collection
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    T GetItem(int index);

    /// <summary>
    /// Read several items into an array by their indices
    /// </summary>
    /// <param name="indexList"></param>
    /// <returns></returns>
    T[] GetItems(params int[] indexList);

    /// <summary>
    /// Read several items into a sequence by their indices
    /// </summary>
    /// <param name="indexList"></param>
    /// <returns></returns>
    IEnumerable<T> GetItems(IEnumerable<int> indexList);
}