using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;

public static class RGaFloat64BivectorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<ulong, double> CreateBivectorDictionary(this IReadOnlyDictionary<IndexPair, double> inputDictionary)
    {
        var basisScalarDictionary = new Dictionary<ulong, double>();

        foreach (var (indexPair, value) in inputDictionary)
            basisScalarDictionary.Add(
                1UL << indexPair.Item1 | 1UL << indexPair.Item2,
                value
            );

        return basisScalarDictionary;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<ulong, double> CreateBivectorDictionary(this IReadOnlyDictionary<Int32Pair, double> inputDictionary)
    {
        var basisScalarDictionary = new Dictionary<ulong, double>();

        foreach (var (indexPair, value) in inputDictionary)
            basisScalarDictionary.Add(
                1UL << indexPair.Item1 | 1UL << indexPair.Item2,
                value
            );

        return basisScalarDictionary;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector Bivector(this RGaFloat64Processor metric, IReadOnlyDictionary<IndexPair, double> basisScalarDictionary)
    {
        return new RGaFloat64Bivector(
            metric,
            basisScalarDictionary.CreateBivectorDictionary()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector Bivector(this RGaFloat64Processor metric, IReadOnlyDictionary<Int32Pair, double> basisScalarDictionary)
    {
        return new RGaFloat64Bivector(
            metric,
            basisScalarDictionary.CreateBivectorDictionary()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector Bivector(this RGaFloat64Processor metric, IReadOnlyDictionary<ulong, double> basisScalarDictionary)
    {
        if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<ulong, double>)
            return metric.BivectorZero;

        if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<ulong, double>)
            return metric.BivectorTerm(basisScalarDictionary.First());

        return new RGaFloat64Bivector(
            metric,
            basisScalarDictionary
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector BivectorTerm(this RGaFloat64Processor metric, int index1, int index2)
    {
        if (index1 < 0 || index1 >= index2 || index2 >= 64)
            throw new InvalidOperationException();

        return new RGaFloat64Bivector(
            metric,
            new SingleItemDictionary<ulong, double>(
                1UL << index1 | 1UL << index2,
                1d
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector BivectorTerm(this RGaFloat64Processor metric, int index1, int index2, double scalar)
    {
        if (index1 < 0 || index1 >= index2 || index2 >= 64)
            throw new InvalidOperationException();

        if (scalar.IsZero())
            return new RGaFloat64Bivector(metric);

        return new RGaFloat64Bivector(
            metric,
            new SingleItemDictionary<ulong, double>(
                1UL << index1 | 1UL << index2,
                scalar
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector BivectorTerm(this RGaFloat64Processor metric, IPair<int> indexPair)
    {
        var index1 = indexPair.Item1;
        var index2 = indexPair.Item2;

        if (index1 < 0 || index1 >= index2 || index2 >= 64)
            throw new InvalidOperationException();

        return new RGaFloat64Bivector(
            metric,
            new SingleItemDictionary<ulong, double>(
                1UL << index1 | 1UL << index2,
                1d
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector BivectorTerm(this RGaFloat64Processor metric, IPair<int> indexPair, double scalar)
    {
        var index1 = indexPair.Item1;
        var index2 = indexPair.Item2;

        if (index1 < 0 || index1 >= index2 || index2 >= 64)
            throw new InvalidOperationException();

        if (scalar.IsZero())
            return new RGaFloat64Bivector(metric);

        return new RGaFloat64Bivector(
            metric,
            new SingleItemDictionary<ulong, double>(
                1UL << index1 | 1UL << index2,
                scalar
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector BivectorTerm(this RGaFloat64Processor metric, KeyValuePair<Int32Pair, double> indexScalarPair)
    {
        return metric.BivectorTerm(
            indexScalarPair.Key,
            indexScalarPair.Value
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector BivectorTerm(this RGaFloat64Processor metric, KeyValuePair<ulong, double> indexScalarPair)
    {
        return metric.BivectorTerm(indexScalarPair.Key, indexScalarPair.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector BivectorTerm(this RGaFloat64Processor metric, ulong basisBlade)
    {
        return new RGaFloat64Bivector(
            metric,
            new SingleItemDictionary<ulong, double>(basisBlade, 1d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector BivectorTerm(this RGaFloat64Processor metric, ulong basisBlade, double scalar)
    {
        if (scalar.IsZero())
            return new RGaFloat64Bivector(metric);

        return new RGaFloat64Bivector(
            metric,

            new SingleItemDictionary<ulong, double>(basisBlade, scalar)
        );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector Bivector2D(this RGaFloat64Processor processor, double scalar01)
    {
        return processor
            .CreateComposer()
            .SetTerm(3, scalar01)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector Bivector3D(this RGaFloat64Processor processor, double scalar01, double scalar02, double scalar12)
    {
        return processor
            .CreateComposer()
            .SetTerm(3, scalar01)
            .SetTerm(5, scalar02)
            .SetTerm(6, scalar12)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector Bivector3D(this RGaFloat64Processor processor, LinFloat64Bivector3D bivector)
    {
        return processor
            .CreateComposer()
            .SetTerm(3, bivector.Xy)
            .SetTerm(5, bivector.Xz)
            .SetTerm(6, bivector.Yz)
            .GetBivector();
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector ToRGaFloat64Bivector(this LinFloat64Bivector2D bivector)
    {
        return RGaFloat64Processor
            .Euclidean
            .CreateComposer()
            .SetBivectorTerm(0, 1, bivector.Xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector ToRGaFloat64Bivector(this LinFloat64Bivector3D bivector)
    {
        return RGaFloat64Processor
            .Euclidean
            .CreateComposer()
            .SetBivectorTerm(0, 1, bivector.Xy)
            .SetBivectorTerm(0, 2, bivector.Xz)
            .SetBivectorTerm(1, 2, bivector.Yz)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector ToRGaFloat64Bivector(this LinFloat64Bivector3D bivector, RGaFloat64Processor processor)
    {
        return processor
            .CreateComposer()
            .SetBivectorTerm(0, 1, bivector.Xy)
            .SetBivectorTerm(0, 2, bivector.Xz)
            .SetBivectorTerm(1, 2, bivector.Yz)
            .GetBivector();
    }
}