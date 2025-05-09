using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;

public static class RGaBivectorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<ulong, T> CreateBivectorDictionary<T>(this IReadOnlyDictionary<IndexPair, T> inputDictionary)
    {
        var basisScalarDictionary = new Dictionary<ulong, T>();

        foreach (var (indexPair, value) in inputDictionary)
            basisScalarDictionary.Add(
                1UL << indexPair.Item1 | 1UL << indexPair.Item2,
                value
            );

        return basisScalarDictionary;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<ulong, T> CreateBivectorDictionary<T>(this IReadOnlyDictionary<Int32Pair, T> inputDictionary)
    {
        var basisScalarDictionary = new Dictionary<ulong, T>();

        foreach (var (indexPair, value) in inputDictionary)
            basisScalarDictionary.Add(
                1UL << indexPair.Item1 | 1UL << indexPair.Item2,
                value
            );

        return basisScalarDictionary;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> Bivector<T>(this RGaProcessor<T> processor, IReadOnlyDictionary<IndexPair, T> basisScalarDictionary)
    {
        return new RGaBivector<T>(
            processor,
            basisScalarDictionary.CreateBivectorDictionary()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> Bivector<T>(this RGaProcessor<T> processor, IReadOnlyDictionary<Int32Pair, T> basisScalarDictionary)
    {
        return new RGaBivector<T>(
            processor,
            basisScalarDictionary.CreateBivectorDictionary()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> Bivector<T>(this RGaProcessor<T> processor, IReadOnlyDictionary<ulong, T> basisScalarDictionary)
    {
        if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<ulong, T>)
            return processor.BivectorZero;

        if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<ulong, T>)
            return processor.BivectorTerm(basisScalarDictionary.First());

        return new RGaBivector<T>(
            processor,

            basisScalarDictionary
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> BivectorTerm<T>(this RGaProcessor<T> processor, int index1, int index2)
    {
        if (index1 < 0 || index1 >= index2 || index2 >= 64)
            throw new InvalidOperationException();

        return new RGaBivector<T>(
            processor,

            new SingleItemDictionary<ulong, T>(
                1UL << index1 | 1UL << index2,
                processor.ScalarProcessor.OneValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> BivectorTerm<T>(this RGaProcessor<T> processor, int index1, int index2, T scalar)
    {
        if (index1 < 0 || index1 >= index2 || index2 >= 64)
            throw new InvalidOperationException();

        if (processor.ScalarProcessor.IsZero(scalar))
            return new RGaBivector<T>(processor);

        return new RGaBivector<T>(
            processor,

            new SingleItemDictionary<ulong, T>(
                1UL << index1 | 1UL << index2,
                scalar
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> BivectorTerm<T>(this RGaProcessor<T> processor, int index1, int index2, IScalar<T> scalar)
    {
        return BivectorTerm(processor, index1, index2, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> BivectorTerm<T>(this RGaProcessor<T> processor, IPair<int> indexPair)
    {
        var index1 = indexPair.Item1;
        var index2 = indexPair.Item2;

        if (index1 < 0 || index1 >= index2 || index2 >= 64)
            throw new InvalidOperationException();

        return new RGaBivector<T>(
            processor,

            new SingleItemDictionary<ulong, T>(
                1UL << index1 | 1UL << index2,
                processor.ScalarProcessor.OneValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> BivectorTerm<T>(this RGaProcessor<T> processor, IPair<int> indexPair, T scalar)
    {
        var index1 = indexPair.Item1;
        var index2 = indexPair.Item2;

        if (index1 < 0 || index1 >= index2 || index2 >= 64)
            throw new InvalidOperationException();

        if (processor.ScalarProcessor.IsZero(scalar))
            return new RGaBivector<T>(processor);

        return new RGaBivector<T>(
            processor,

            new SingleItemDictionary<ulong, T>(
                1UL << index1 | 1UL << index2,
                scalar
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> BivectorTerm<T>(this RGaProcessor<T> processor, KeyValuePair<Int32Pair, T> indexScalarPair)
    {
        return processor.BivectorTerm(

            indexScalarPair.Key,
            indexScalarPair.Value
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> BivectorTerm<T>(this RGaProcessor<T> processor, KeyValuePair<ulong, T> indexScalarPair)
    {
        return processor.BivectorTerm(indexScalarPair.Key, indexScalarPair.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> BivectorTerm<T>(this RGaProcessor<T> processor, ulong basisBlade)
    {
        return new RGaBivector<T>(
            processor,

            new SingleItemDictionary<ulong, T>(basisBlade, processor.ScalarProcessor.OneValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> BivectorTerm<T>(this RGaProcessor<T> processor, ulong basisBlade, T scalar)
    {
        if (processor.ScalarProcessor.IsZero(scalar))
            return new RGaBivector<T>(processor);

        return new RGaBivector<T>(
            processor,

            new SingleItemDictionary<ulong, T>(basisBlade, scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> BivectorTerm<T>(this RGaProcessor<T> processor, ulong basisBlade, Scalar<T> scalar)
    {
        if (scalar.IsZero())
            return new RGaBivector<T>(processor);

        return new RGaBivector<T>(
            processor,

            new SingleItemDictionary<ulong, T>(basisBlade, scalar.ScalarValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> Bivector2D<T>(this RGaProcessor<T> processor, double scalar)
    {
        return processor.Bivector2D(
            processor.ScalarProcessor.ValueFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> Bivector2D<T>(this RGaProcessor<T> processor, string scalar)
    {
        return processor.Bivector2D(
            processor.ScalarProcessor.ValueFromText(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> Bivector2D<T>(this RGaProcessor<T> processor, T scalar)
    {
        if (processor.ScalarProcessor.IsZero(scalar))
            return new RGaBivector<T>(processor);

        return new RGaBivector<T>(
            processor,
            new SingleItemDictionary<ulong, T>(3UL, scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> Bivector3D<T>(this RGaProcessor<T> processor, double scalar01, double scalar02, double scalar12)
    {
        return processor
            .CreateComposer()
            .SetTerm(3, scalar01)
            .SetTerm(5, scalar02)
            .SetTerm(6, scalar12)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> Bivector3D<T>(this RGaProcessor<T> processor, string scalar01, string scalar02, string scalar12)
    {
        return processor
            .CreateComposer()
            .SetTerm(3, scalar01)
            .SetTerm(5, scalar02)
            .SetTerm(6, scalar12)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> Bivector3D<T>(this RGaProcessor<T> processor, T scalar01, T scalar02, T scalar12)
    {
        return processor
            .CreateComposer()
            .SetTerm(3, scalar01)
            .SetTerm(5, scalar02)
            .SetTerm(6, scalar12)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> Bivector3D<T>(this RGaProcessor<T> processor, LinFloat64Bivector3D bivector)
    {
        return processor
            .CreateComposer()
            .SetTerm(3, bivector.Xy)
            .SetTerm(5, bivector.Xz)
            .SetTerm(6, bivector.Yz)
            .GetBivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> ToBivector<T>(this LinFloat64Bivector3D bivector, RGaProcessor<T> processor)
    {
        return processor
            .CreateComposer()
            .SetBivectorTerm(0, 1, bivector.Xy)
            .SetBivectorTerm(0, 2, bivector.Xz)
            .SetBivectorTerm(1, 2, bivector.Yz)
            .GetBivector();
    }

}