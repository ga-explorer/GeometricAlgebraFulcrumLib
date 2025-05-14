using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.SortingNetworks;

/// <summary>
/// https://en.wikipedia.org/wiki/Sorting_network
/// https://github.com/bertdobbelaere/SorterHunter
/// https://bertdobbelaere.github.io/sorting_networks_extended.html
/// </summary>
public sealed class SortingNetwork
    : IReadOnlyList<IndexPair>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static SortingNetwork CreateEmpty()
    {
        return new SortingNetwork(
            [],
            [],
            []
        );
    }

    internal static SortingNetwork Create(params (int, int)[][] layers)
    {
        var layerCount = layers.Length;
        var connectorCount = layers.Sum(l => l.Length);
        var index1Array = new int[connectorCount];
        var index2Array = new int[connectorCount];
        var layerIndicesList = new IndexPair[layerCount];

        var layerIndex = 0;
        var connectorIndex = 0;
        foreach (var connectors in layers)
        {
            layerIndicesList[layerIndex] =
                new IndexPair(
                    connectorIndex,
                    connectorIndex + connectors.Length - 1
                );

            foreach (var (i1, i2) in connectors)
            {
                index1Array[connectorIndex] = i1;
                index2Array[connectorIndex] = i2;

                connectorIndex++;
            }

            layerIndex++;
        }

        return new SortingNetwork(
            index1Array.ToArray(),
            index2Array.ToArray(),
            layerIndicesList.ToArray()
        );
    }

    private static IEnumerable<IndexPair> GetLayers(IReadOnlyList<int> index1Array, IReadOnlyList<int> index2Array)
    {
        var n = index1Array.Count;

        var i1 = 0;
        var i2 = 0;

        for (var i = 1; i < n; i++)
        {
            var index1 = index1Array[i];
            var index2 = index2Array[i];

            // Try to find the index pair in the current layer
            var flag = false;
            for (var j = i1; j <= i2; j++)
            {
                var idx1 = index1Array[j];
                var idx2 = index2Array[j];

                if (index1 == idx1 || index1 == idx2 || index2 == idx1 || index2 == idx2)
                {
                    flag = true;
                    break;
                }
            }

            if (flag)
            {
                // Finalize the current layer
                yield return new IndexPair(i1, i2);

                // Start a new layer
                i1 = i2 + 1;
                i2 = i1;
            }
            else
            {
                // Continue with the current layer
                i2++;
            }
        }

        if (i2 < n)
        {
            // Finalize the last layer
            yield return new IndexPair(i1, i2);
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SortingNetwork Predefined(int n)
    {
        return PredefinedSortingNetworks.NetworkList[n];
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SortingNetwork PredefinedMedian(int n)
    {
        return PredefinedMedianNetworks.NetworkList[n];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="inputCount"></param>
    /// <returns></returns>
    public static SortingNetwork BubbleInsert(int inputCount)
    {
        var layerIndex1 = 0;
        var layerIndex2 = 2 * inputCount - 4;
        var connectorIndex1List = new List<Pair<int>>();

        while (layerIndex2 >= layerIndex1)
        {
            var input1Index = layerIndex1;

            for (var layerIndex = layerIndex1; layerIndex <= layerIndex2; layerIndex += 2)
            {
                connectorIndex1List.Add(
                    new Pair<int>(layerIndex, input1Index)
                );
            }

            layerIndex1++;
            layerIndex2--;
        }

        var index1Array =
            connectorIndex1List
                .OrderBy(p => p.Item1)
                .ThenBy(p => p.Item2)
                .Select(p => p.Item2)
                .ToArray();

        var index2Array =
            index1Array
                .Select(i => i + 1)
                .ToArray();

        return new SortingNetwork(
            index1Array,
            index2Array
        );
    }

    public static SortingNetwork BoseNelsen(int inputCount)
    {
        var index1Array = new List<int>();
        var index2Array = new List<int>();

        Star(1, inputCount); /* sort the sequence {X1,...,Xn} */

        return new SortingNetwork(
            index1Array.ToArray(),
            index2Array.ToArray()
        );

        void AddConnector(int i, int j)
        {
            index1Array.Add(i - 1);
            index2Array.Add(j - 1);

            /* print out in 0 based notation */
            //Console.WriteLine($"swap({i - 1}, {j - 1});");
        }

        void Bracket(
            int firstIndex1,  /* value of first element in sequence 1 */
            int length1,      /* length of sequence 1 */
            int firstIndex2,  /* value of first element in sequence 2 */
            int length2       /* length of sequence 2 */
        )
        {
            if (length1 == 1 && length2 == 1)
                AddConnector(firstIndex1, firstIndex2); /* 1 comparison sorts 2 items */

            else if (length1 == 1 && length2 == 2)
            {
                /* 2 comparisons inserts an item into an
                 * already sorted sequence of length 2. */
                AddConnector(firstIndex1, firstIndex2 + 1);
                AddConnector(firstIndex1, firstIndex2);
            }

            else if (length1 == 2 && length2 == 1)
            {
                /* As above, but inserting j */
                AddConnector(firstIndex1, firstIndex2);
                AddConnector(firstIndex1 + 1, firstIndex2);
            }

            else
            {
                /* Recurse on shorter sequences, attempting
                 * to make the length of one subsequence odd
                 * and the length of the other even. If we
                 * can do this, we eventually merge the two. */
                var a = length1 / 2;
                var b = (length1 & 1) != 0 ? length2 / 2 : (length2 + 1) / 2;
                Bracket(firstIndex1, a, firstIndex2, b);
                Bracket(firstIndex1 + a, length1 - a, firstIndex2 + b, length2 - b);
                Bracket(firstIndex1 + a, length1 - a, firstIndex2, b);
            }
        }

        void Star(
            int firstIndex,  /* value of first element in sequence */
            int itemCount   /* length of sequence */
        )
        {
            if (itemCount <= 1) return;

            // Partition into 2 shorter sequences,
            // generate a sorting method for each,
            // and merge the two subnetworks.
            var a = itemCount / 2;
            Star(firstIndex, a);
            Star(firstIndex + a, itemCount - a);
            Bracket(firstIndex, a, firstIndex + a, itemCount - a);
        }
    }

    /// <summary>
    /// https://en.wikipedia.org/wiki/Batcher_odd%E2%80%93even_mergesort
    /// </summary>
    /// <param name="inputCount"></param>
    /// <returns></returns>
    public static SortingNetwork OddEvenMergesort(int inputCount)
    {
        var index1Array = new List<int>();
        var index2Array = new List<int>();

        var p = 1;
        while (p < inputCount)
        {
            var k = p;
            while (k >= 1)
            {
                for (var j = (k % p); j <= inputCount - 1 - k; j += 2 * k)
                {
                    for (var i = 0; i <= Math.Min(k-1, inputCount-j-k-1); i++)
                    {
                        if (Math.Floor((i + j) / (p * 2d)) != Math.Floor((i + j + k) / (p * 2d))) 
                            continue;
                        
                        index1Array.Add(i + j);
                        index2Array.Add(i + j + k);
                    }
                }

                k >>= 1;
            }

            p <<= 1;
        }

        return new SortingNetwork(
            index1Array.ToArray(),
            index2Array.ToArray()
        );
    }


    private readonly IndexPair[] _layerIndices;
    private readonly int[] _index1Array;
    private readonly int[] _index2Array;

    public IndexSet InputIndices { get; }

    public int Count
        => _index1Array.Length;

    public int InputCount
        => InputIndices.Count;

    public int ConnectorCount
        => _index1Array.Length;

    public int LayerCount
        => _layerIndices.Length;

    public bool IsSerial
        => LayerCount == ConnectorCount;

    public IndexPair this[int index]
        => new IndexPair(
            _index1Array[index],
            _index2Array[index]
        );

    public IEnumerable<IEnumerable<IndexPair>> Layers
        => LayerCount.MapRange(GetLayer);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SortingNetwork(int[] index1Array, int[] index2Array)
        : this(
            index1Array,
            index2Array,
            GetLayers(index1Array, index2Array).ToArray()
        )
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SortingNetwork(int[] index1Array, int[] index2Array, IndexPair[] layerIndices)
    {
        _index1Array = index1Array;
        _index2Array = index2Array;
        _layerIndices = layerIndices;

        InputIndices = _index1Array
            .Concat(_index2Array)
            .ToIndexSet(false);

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return this.All(p => p.Item1 >= 0 && p.Item1 < p.Item2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IndexPair> GetLayer(int layerIndex)
    {
        var (i1, i2) = _layerIndices[layerIndex];

        for (var i = i1; i <= i2; i++)
            yield return new IndexPair(
                _index1Array[i],
                _index2Array[i]
            );
    }
    
    
    public void SortItemsAscending(byte[] itemArray, int startIndex = 0)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            Branchless.SwapIfGreaterThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }

    public void SortItemsAscending(byte[] itemArray, IReadOnlyList<int> indexList)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            Branchless.SwapIfGreaterThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }

    public void SortItemsDescending(byte[] itemArray, int startIndex = 0)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            Branchless.SwapIfLessThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }

    public void SortItemsDescending(byte[] itemArray, IReadOnlyList<int> indexList)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            Branchless.SwapIfLessThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }
    
    
    public void SortItemsAscending(sbyte[] itemArray, int startIndex = 0)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            Branchless.SwapIfGreaterThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }

    public void SortItemsAscending(sbyte[] itemArray, IReadOnlyList<int> indexList)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            Branchless.SwapIfGreaterThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }

    public void SortItemsDescending(sbyte[] itemArray, int startIndex = 0)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            Branchless.SwapIfLessThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }

    public void SortItemsDescending(sbyte[] itemArray, IReadOnlyList<int> indexList)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            Branchless.SwapIfLessThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }


    public void SortItemsAscending(ushort[] itemArray, int startIndex = 0)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            Branchless.SwapIfGreaterThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }

    public void SortItemsAscending(ushort[] itemArray, IReadOnlyList<int> indexList)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            Branchless.SwapIfGreaterThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }

    public void SortItemsDescending(ushort[] itemArray, int startIndex = 0)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            Branchless.SwapIfLessThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }

    public void SortItemsDescending(ushort[] itemArray, IReadOnlyList<int> indexList)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            Branchless.SwapIfLessThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }
    
    
    public void SortItemsAscending(short[] itemArray, int startIndex = 0)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            Branchless.SwapIfGreaterThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }

    public void SortItemsAscending(short[] itemArray, IReadOnlyList<int> indexList)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            Branchless.SwapIfGreaterThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }

    public void SortItemsDescending(short[] itemArray, int startIndex = 0)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            Branchless.SwapIfLessThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }

    public void SortItemsDescending(short[] itemArray, IReadOnlyList<int> indexList)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            Branchless.SwapIfLessThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }


    public void SortItemsAscending(uint[] itemArray, int startIndex = 0)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            Branchless.SwapIfGreaterThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }

    public void SortItemsAscending(uint[] itemArray, IReadOnlyList<int> indexList)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            Branchless.SwapIfGreaterThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }

    public void SortItemsDescending(uint[] itemArray, int startIndex = 0)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            Branchless.SwapIfLessThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }

    public void SortItemsDescending(uint[] itemArray, IReadOnlyList<int> indexList)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            Branchless.SwapIfLessThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }


    public void SortItemsAscending(int[] itemArray, int startIndex = 0)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            Branchless.SwapIfGreaterThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }

    public void SortItemsAscending(int[] itemArray, IReadOnlyList<int> indexList)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            Branchless.SwapIfGreaterThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }

    public void SortItemsDescending(int[] itemArray, int startIndex = 0)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            Branchless.SwapIfLessThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }

    public void SortItemsDescending(int[] itemArray, IReadOnlyList<int> indexList)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            Branchless.SwapIfLessThan(
                ref itemArray[index1],
                ref itemArray[index2]
            );
        }
    }
    
    
    public void SortItemsAscending(ulong[] itemArray, int startIndex = 0)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            var item1 = itemArray[index1];
            var item2 = itemArray[index2];
            
            if (item1 > item2)
            {
                itemArray[index1] = item2;
                itemArray[index2] = item1;
            }
        }
    }
    
    public void SortItemsAscending(ulong[] itemArray, IReadOnlyList<int> indexList)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            var item1 = itemArray[index1];
            var item2 = itemArray[index2];

            if (item1 > item2)
            {
                itemArray[index1] = item2;
                itemArray[index2] = item1;
            }
        }
    }

    public void SortItemsDescending(ulong[] itemArray, int startIndex = 0)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            var item1 = itemArray[index1];
            var item2 = itemArray[index2];

            if (item1 < item2)
            {
                itemArray[index1] = item2;
                itemArray[index2] = item1;
            }
        }
    }
    
    public void SortItemsDescending(ulong[] itemArray, IReadOnlyList<int> indexList)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            var item1 = itemArray[index1];
            var item2 = itemArray[index2];

            if (item1 < item2)
            {
                itemArray[index1] = item2;
                itemArray[index2] = item1;
            }
        }
    }


    public void SortItemsAscending(long[] itemArray, int startIndex = 0)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            var item1 = itemArray[index1];
            var item2 = itemArray[index2];
            
            if (item1 > item2)
            {
                itemArray[index1] = item2;
                itemArray[index2] = item1;
            }
        }
    }
    
    public void SortItemsAscending(long[] itemArray, IReadOnlyList<int> indexList)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            var item1 = itemArray[index1];
            var item2 = itemArray[index2];

            if (item1 > item2)
            {
                itemArray[index1] = item2;
                itemArray[index2] = item1;
            }
        }
    }

    public void SortItemsDescending(long[] itemArray, int startIndex = 0)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            var item1 = itemArray[index1];
            var item2 = itemArray[index2];

            if (item1 < item2)
            {
                itemArray[index1] = item2;
                itemArray[index2] = item1;
            }
        }
    }
    
    public void SortItemsDescending(long[] itemArray, IReadOnlyList<int> indexList)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            var item1 = itemArray[index1];
            var item2 = itemArray[index2];

            if (item1 < item2)
            {
                itemArray[index1] = item2;
                itemArray[index2] = item1;
            }
        }
    }
    

    public void SortItemsAscending(float[] itemArray, int startIndex = 0)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            var item1 = itemArray[index1];
            var item2 = itemArray[index2];
            
            if (item1 > item2)
            {
                itemArray[index1] = item2;
                itemArray[index2] = item1;
            }
        }
    }
    
    public void SortItemsAscending(float[] itemArray, IReadOnlyList<int> indexList)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            var item1 = itemArray[index1];
            var item2 = itemArray[index2];

            if (item1 > item2)
            {
                itemArray[index1] = item2;
                itemArray[index2] = item1;
            }
        }
    }

    public void SortItemsDescending(float[] itemArray, int startIndex = 0)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            var item1 = itemArray[index1];
            var item2 = itemArray[index2];

            if (item1 < item2)
            {
                itemArray[index1] = item2;
                itemArray[index2] = item1;
            }
        }
    }
    
    public void SortItemsDescending(float[] itemArray, IReadOnlyList<int> indexList)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            var item1 = itemArray[index1];
            var item2 = itemArray[index2];

            if (item1 < item2)
            {
                itemArray[index1] = item2;
                itemArray[index2] = item1;
            }
        }
    }

    
    public void SortItemsAscending(double[] itemArray, int startIndex = 0)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            var item1 = itemArray[index1];
            var item2 = itemArray[index2];
            
            if (item1 > item2)
            {
                itemArray[index1] = item2;
                itemArray[index2] = item1;
            }
        }
    }
    
    public void SortItemsAscending(double[] itemArray, IReadOnlyList<int> indexList)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            var item1 = itemArray[index1];
            var item2 = itemArray[index2];

            if (item1 > item2)
            {
                itemArray[index1] = item2;
                itemArray[index2] = item1;
            }
        }
    }

    public void SortItemsDescending(double[] itemArray, int startIndex = 0)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            var item1 = itemArray[index1];
            var item2 = itemArray[index2];

            if (item1 < item2)
            {
                itemArray[index1] = item2;
                itemArray[index2] = item1;
            }
        }
    }
    
    public void SortItemsDescending(double[] itemArray, IReadOnlyList<int> indexList)
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            var item1 = itemArray[index1];
            var item2 = itemArray[index2];

            if (item1 < item2)
            {
                itemArray[index1] = item2;
                itemArray[index2] = item1;
            }
        }
    }


    public void SortItemsAscending<T>(T[] itemArray, int startIndex = 0)
        where T : IComparable
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            var item1 = itemArray[index1];
            var item2 = itemArray[index2];

            if (item1.CompareTo(item2) <= 0) continue;

            itemArray[index1] = item2;
            itemArray[index2] = item1;
        }
    }

    public void SortItemsAscending<T>(T[] itemArray, IReadOnlyList<int> indexList)
        where T : IComparable
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            var item1 = itemArray[index1];
            var item2 = itemArray[index2];

            if (item1.CompareTo(item2) <= 0) continue;

            itemArray[index1] = item2;
            itemArray[index2] = item1;
        }
    }

    public void SortItemsDescending<T>(T[] itemArray, int startIndex = 0)
        where T : IComparable
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = startIndex + _index1Array[i];
            var index2 = startIndex + _index2Array[i];

            var item1 = itemArray[index1];
            var item2 = itemArray[index2];

            if (item1.CompareTo(item2) >= 0) continue;

            itemArray[index1] = item2;
            itemArray[index2] = item1;
        }
    }

    public void SortItemsDescending<T>(T[] itemArray, IReadOnlyList<int> indexList)
        where T : IComparable
    {
        var n = Count;
        for (var i = 0; i < n; i++)
        {
            var index1 = indexList[_index1Array[i]];
            var index2 = indexList[_index2Array[i]];

            var item1 = itemArray[index1];
            var item2 = itemArray[index2];

            if (item1.CompareTo(item2) >= 0) continue;

            itemArray[index1] = item2;
            itemArray[index2] = item1;
        }
    }

    
    public Pair<T> GetMedians<T>(T[] itemArray, int startIndex = 0)
        where T : IComparable
    {
        SortItemsAscending(itemArray, startIndex);

        var n = InputCount;
        var medianIndex = (n >> 1) - 1;
        if (n % 2 == 0)
            return new Pair<T>(
                itemArray[medianIndex], 
                itemArray[medianIndex + 1]
            );

        var median = itemArray[medianIndex];

        return new Pair<T>(
            median, 
            median
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<IndexPair> GetEnumerator()
    {
        return ConnectorCount.GetRange(i =>
            new IndexPair(
                _index1Array[i],
                _index2Array[i]
            )
        ).GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        var composer = new StringBuilder();

        if (IsSerial)
        {
            var code = this
                .Select(p => $"  ({p.Item1},{p.Item2})")
                .ConcatenateText("," + Environment.NewLine);

            composer
                .AppendLine($"Serial Sorting Network with {InputCount} inputs, {ConnectorCount} connectors: [")
                .AppendLine(code)
                .Append("]");
        }
        else
        {
            composer.AppendLine($"Sorting Network with {InputCount} inputs, {ConnectorCount} connectors, {LayerCount} layers: [");

            for (var i = 0; i < LayerCount; i++)
            {
                var code = GetLayer(i)
                    .Select(p => $"({p.Item1},{p.Item2})")
                    .ConcatenateText(", ");

                composer
                    .Append("  [")
                    .Append(code)
                    .AppendLine("],");
            }

            composer.Append("]");
        }

        return composer.ToString().Trim();
    }
}