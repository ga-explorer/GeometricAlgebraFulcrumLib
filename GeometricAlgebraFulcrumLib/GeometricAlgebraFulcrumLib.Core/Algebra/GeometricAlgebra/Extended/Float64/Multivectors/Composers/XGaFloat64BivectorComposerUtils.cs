using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;

public static class XGaFloat64BivectorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<IIndexSet, double> CreateBivectorDictionary(this IReadOnlyDictionary<IndexPair, double> inputDictionary)
    {
        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (key, value) in inputDictionary)
            basisScalarDictionary.Add(key.IndexPairToIndexSet(), value);

        return basisScalarDictionary;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<IIndexSet, double> CreateBivectorDictionary(this IReadOnlyDictionary<Int32Pair, double> inputDictionary)
    {
        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (key, value) in inputDictionary)
            basisScalarDictionary.Add(key.IndexPairToIndexSet(), value);

        return basisScalarDictionary;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector Bivector(this XGaFloat64Processor processor, IReadOnlyDictionary<IndexPair, double> basisScalarDictionary)
    {
        return new XGaFloat64Bivector(
            processor,
            basisScalarDictionary.CreateBivectorDictionary()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector Bivector(this XGaFloat64Processor processor, IReadOnlyDictionary<Int32Pair, double> basisScalarDictionary)
    {
        return new XGaFloat64Bivector(
            processor,
            basisScalarDictionary.CreateBivectorDictionary()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector Bivector(this XGaFloat64Processor processor, IReadOnlyDictionary<IIndexSet, double> basisScalarDictionary)
    {
        if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IIndexSet, double>)
            return processor.BivectorZero;

        if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IIndexSet, double>)
            return processor.BivectorTerm(basisScalarDictionary.First());

        return new XGaFloat64Bivector(
            processor,
            basisScalarDictionary
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector BivectorTerm(this XGaFloat64Processor processor, int index1, int index2)
    {
        if (index1 < 0 || index1 >= index2)
            throw new InvalidOperationException();

        return new XGaFloat64Bivector(
            processor,
            new SingleItemDictionary<IIndexSet, double>(
                IndexSetUtils.IndexPairToIndexSet(index1, index2),
                1d
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector BivectorTerm(this XGaFloat64Processor processor, int index1, int index2, double scalar)
    {
        if (index1 < 0 || index1 >= index2)
            throw new InvalidOperationException();

        if (scalar.IsZero())
            return new XGaFloat64Bivector(processor);

        return new XGaFloat64Bivector(
            processor,
            new SingleItemDictionary<IIndexSet, double>(
                IndexSetUtils.IndexPairToIndexSet(index1, index2),
                scalar
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector BivectorTerm(this XGaFloat64Processor processor, IPair<int> indexPair)
    {
        return new XGaFloat64Bivector(
            processor,

            new SingleItemDictionary<IIndexSet, double>(
                indexPair.IndexPairToIndexSet(),
                1d
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector BivectorTerm(this XGaFloat64Processor processor, IPair<int> indexPair, double scalar)
    {
        if (scalar.IsZero())
            return new XGaFloat64Bivector(processor);

        return new XGaFloat64Bivector(
            processor,

            new SingleItemDictionary<IIndexSet, double>(
                indexPair.IndexPairToIndexSet(),
                scalar
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector BivectorTerm(this XGaFloat64Processor processor, KeyValuePair<Int32Pair, double> indexScalarPair)
    {
        return processor.BivectorTerm(
            indexScalarPair.Key, indexScalarPair.Value);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector BivectorTerm(this XGaFloat64Processor processor, KeyValuePair<IIndexSet, double> indexScalarPair)
    {
        return processor.BivectorTerm(indexScalarPair.Key, indexScalarPair.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector BivectorTerm(this XGaFloat64Processor processor, IIndexSet basisBlade)
    {
        return new XGaFloat64Bivector(
            processor,
            new SingleItemDictionary<IIndexSet, double>(basisBlade, 1d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector BivectorTerm(this XGaFloat64Processor processor, IIndexSet basisBlade, double scalar)
    {
        if (scalar.IsZero())
            return new XGaFloat64Bivector(processor);

        return new XGaFloat64Bivector(
            processor,
            new SingleItemDictionary<IIndexSet, double>(basisBlade, scalar)
        );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector Bivector2D(this XGaFloat64Processor processor, double scalar01)
    {
        return processor
            .CreateComposer()
            .SetTerm(3, scalar01)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector Bivector3D(this XGaFloat64Processor processor, double scalar01, double scalar02, double scalar12)
    {
        return processor
            .CreateComposer()
            .SetTerm(3, scalar01)
            .SetTerm(5, scalar02)
            .SetTerm(6, scalar12)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector Bivector3D(this XGaFloat64Processor processor, LinFloat64Bivector3D bivector)
    {
        return processor
            .CreateComposer()
            .SetTerm(3, bivector.Xy)
            .SetTerm(5, bivector.Xz)
            .SetTerm(6, bivector.Yz)
            .GetBivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector ToBivector(this LinFloat64Bivector3D bivector)
    {
        return XGaFloat64Processor.Euclidean
            .CreateComposer()
            .SetBivectorTerm(0, 1, bivector.Xy)
            .SetBivectorTerm(0, 2, bivector.Xz)
            .SetBivectorTerm(1, 2, bivector.Yz)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector ToBivector(this LinFloat64Bivector3D bivector, XGaFloat64Processor processor)
    {
        return processor
            .CreateComposer()
            .SetBivectorTerm(0, 1, bivector.Xy)
            .SetBivectorTerm(0, 2, bivector.Xz)
            .SetBivectorTerm(1, 2, bivector.Yz)
            .GetBivector();
    }

}