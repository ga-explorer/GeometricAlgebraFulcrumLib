//using System;
//using System.Collections;
//using System.Collections.Immutable;
//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using System.Text;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

//namespace GeometricAlgebraFulcrumLib.Utilities.Structures.SortingNetworks;

//public sealed class MedianNetwork
//    : IReadOnlyList<IndexPair>
//{
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    internal static MedianNetwork CreateEmpty()
//    {
//        return new MedianNetwork(
//            [],
//            [],
//            []
//        );
//    }

//    internal static MedianNetwork Create(params (int, int)[][] layers)
//    {
//        var layerCount = layers.Length;
//        var connectorCount = layers.Sum(l => l.Length);
//        var index1Array = new int[connectorCount];
//        var index2Array = new int[connectorCount];
//        var layerIndicesList = new IndexPair[layerCount];

//        var layerIndex = 0;
//        var connectorIndex = 0;
//        foreach (var connectors in layers)
//        {
//            layerIndicesList[layerIndex] =
//                new IndexPair(
//                    connectorIndex,
//                    connectorIndex + connectors.Length - 1
//                );

//            foreach (var (i1, i2) in connectors)
//            {
//                index1Array[connectorIndex] = i1;
//                index2Array[connectorIndex] = i2;

//                connectorIndex++;
//            }

//            layerIndex++;
//        }

//        return new MedianNetwork(
//            index1Array.ToArray(),
//            index2Array.ToArray(),
//            layerIndicesList.ToArray()
//        );
//    }

//    private static IEnumerable<IndexPair> GetLayers(IReadOnlyList<int> index1Array, IReadOnlyList<int> index2Array)
//    {
//        var n = index1Array.Count;

//        var i1 = 0;
//        var i2 = 0;

//        for (var i = 1; i < n; i++)
//        {
//            var index1 = index1Array[i];
//            var index2 = index2Array[i];

//            // Try to find the index pair in the current layer
//            var flag = false;
//            for (var j = i1; j <= i2; j++)
//            {
//                var idx1 = index1Array[j];
//                var idx2 = index2Array[j];

//                if (index1 == idx1 || index1 == idx2 || index2 == idx1 || index2 == idx2)
//                {
//                    flag = true;
//                    break;
//                }
//            }

//            if (flag)
//            {
//                // Finalize the current layer
//                yield return new IndexPair(i1, i2);

//                // Start a new layer
//                i1 = i2 + 1;
//                i2 = i1;
//            }
//            else
//            {
//                // Continue with the current layer
//                i2++;
//            }
//        }

//        if (i2 < n)
//        {
//            // Finalize the last layer
//            yield return new IndexPair(i1, i2);
//        }
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static MedianNetwork Predefined(int n)
//    {
//        return PredefinedMedianNetworks.NetworkList[n];
//    }
    

//    private readonly IndexPair[] _layerIndices;
//    private readonly int[] _index1Array;
//    private readonly int[] _index2Array;

//    public ImmutableSortedSet<int> InputIndices { get; }

//    public int Count
//        => _index1Array.Length;

//    public int InputCount
//        => InputIndices.Count;

//    public int ConnectorCount
//        => _index1Array.Length;

//    public int LayerCount
//        => _layerIndices.Length;

//    public bool IsSerial
//        => LayerCount == ConnectorCount;

//    public IndexPair this[int index]
//        => new IndexPair(
//            _index1Array[index],
//            _index2Array[index]
//        );

//    public IEnumerable<IEnumerable<IndexPair>> Layers
//        => LayerCount.MapRange(GetLayer);


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private MedianNetwork(int[] index1Array, int[] index2Array)
//        : this(
//            index1Array,
//            index2Array,
//            GetLayers(index1Array, index2Array).ToArray()
//        )
//    {
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private MedianNetwork(int[] index1Array, int[] index2Array, IndexPair[] layerIndices)
//    {
//        _index1Array = index1Array;
//        _index2Array = index2Array;
//        _layerIndices = layerIndices;

//        InputIndices =
//            _index1Array
//                .Concat(_index2Array)
//                .ToIndexSet(false);

//        Debug.Assert(IsValid());
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public bool IsValid()
//    {
//        return this.All(p => p.Item1 >= 0 && p.Item1 < p.Item2);
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public IEnumerable<IndexPair> GetLayer(int layerIndex)
//    {
//        var (i1, i2) = _layerIndices[layerIndex];

//        for (var i = i1; i <= i2; i++)
//            yield return new IndexPair(
//                _index1Array[i],
//                _index2Array[i]
//            );
//    }
    

//    public Pair<T> GetMedians<T>(T[] itemArray, int startIndex = 0)
//        where T : IComparable
//    {
//        var n = Count;
//        for (var i = 0; i < n; i++)
//        {
//            var index1 = startIndex + _index1Array[i];
//            var index2 = startIndex + _index2Array[i];

//            var item1 = itemArray[index1];
//            var item2 = itemArray[index2];

//            if (item1.CompareTo(item2) <= 0) continue;

//            itemArray[index1] = item2;
//            itemArray[index2] = item1;
//        }

//        var medianIndex = (n >> 1) - 1;
//        if (n % 2 == 0)
//            return new Pair<T>(
//                itemArray[medianIndex], 
//                itemArray[medianIndex + 1]
//            );

//        var median = itemArray[medianIndex];

//        return new Pair<T>(
//            median, 
//            median
//        );
//    }

    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public IEnumerator<IndexPair> GetEnumerator()
//    {
//        return ConnectorCount.GetRange(i =>
//            new IndexPair(
//                _index1Array[i],
//                _index2Array[i]
//            )
//        ).GetEnumerator();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    IEnumerator IEnumerable.GetEnumerator()
//    {
//        return GetEnumerator();
//    }

//    public override string ToString()
//    {
//        var composer = new StringBuilder();

//        if (IsSerial)
//        {
//            var code = this
//                .Select(p => $"  ({p.Item1},{p.Item2})")
//                .ConcatenateText("," + Environment.NewLine);

//            composer
//                .AppendLine($"Serial Sorting Network with {InputCount} inputs, {ConnectorCount} connectors: [")
//                .AppendLine(code)
//                .Append("]");
//        }
//        else
//        {
//            composer.AppendLine($"Sorting Network with {InputCount} inputs, {ConnectorCount} connectors, {LayerCount} layers: [");

//            for (var i = 0; i < LayerCount; i++)
//            {
//                var code = GetLayer(i)
//                    .Select(p => $"({p.Item1},{p.Item2})")
//                    .ConcatenateText(", ");

//                composer
//                    .Append("  [")
//                    .Append(code)
//                    .AppendLine("],");
//            }

//            composer.Append("]");
//        }

//        return composer.ToString().Trim();
//    }
//}