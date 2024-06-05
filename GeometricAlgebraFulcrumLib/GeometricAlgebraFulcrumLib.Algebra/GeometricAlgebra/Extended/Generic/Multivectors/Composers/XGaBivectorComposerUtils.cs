using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;

public static class XGaBivectorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<IIndexSet, T> CreateBivectorDictionary<T>(this IReadOnlyDictionary<IndexPair, T> inputDictionary)
    {
        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (key, value) in inputDictionary)
            basisScalarDictionary.Add(key.IndexPairToIndexSet(), value);

        return basisScalarDictionary;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<IIndexSet, T> CreateBivectorDictionary<T>(this IReadOnlyDictionary<Int32Pair, T> inputDictionary)
    {
        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (key, value) in inputDictionary)
            basisScalarDictionary.Add(key.IndexPairToIndexSet(), value);

        return basisScalarDictionary;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> Bivector<T>(this XGaProcessor<T> processor, IReadOnlyDictionary<IndexPair, T> basisScalarDictionary)
    {
        return new XGaBivector<T>(
            processor,
            basisScalarDictionary.CreateBivectorDictionary()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> Bivector<T>(this XGaProcessor<T> processor, IReadOnlyDictionary<Int32Pair, T> basisScalarDictionary)
    {
        return new XGaBivector<T>(
            processor,
            basisScalarDictionary.CreateBivectorDictionary()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> Bivector<T>(this XGaProcessor<T> processor, IReadOnlyDictionary<IIndexSet, T> basisScalarDictionary)
    {
        if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IIndexSet, T>)
            return processor.BivectorZero;

        if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IIndexSet, T>)
            return processor.BivectorTerm(basisScalarDictionary.First());

        return new XGaBivector<T>(
            processor,
            basisScalarDictionary
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> BivectorTerm<T>(this XGaProcessor<T> processor, IPair<int> indexPair)
    {
        var index1 = indexPair.Item1;
        var index2 = indexPair.Item2;

        if (index1 < 0 || index1 >= index2)
            throw new InvalidOperationException();

        return new XGaBivector<T>(
            processor,

            new SingleItemDictionary<IIndexSet, T>(
                indexPair.IndexPairToIndexSet(),
                processor.ScalarProcessor.OneValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> BivectorTerm<T>(this XGaProcessor<T> processor, IPair<int> indexPair, T scalar)
    {
        var index1 = indexPair.Item1;
        var index2 = indexPair.Item2;

        if (index1 < 0 || index1 >= index2)
            throw new InvalidOperationException();

        if (processor.ScalarProcessor.IsZero(scalar))
            return new XGaBivector<T>(processor);

        return new XGaBivector<T>(
            processor,

            new SingleItemDictionary<IIndexSet, T>(
                indexPair.IndexPairToIndexSet(),
                scalar
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> BivectorTerm<T>(this XGaProcessor<T> processor, int index1, int index2)
    {
        if (index1 < 0 || index1 >= index2)
            throw new InvalidOperationException();

        return new XGaBivector<T>(
            processor,

            new SingleItemDictionary<IIndexSet, T>(
                IndexSetUtils.IndexPairToIndexSet(index1, index2),
                processor.ScalarProcessor.OneValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> BivectorTerm<T>(this XGaProcessor<T> processor, int index1, int index2, T scalar)
    {
        if (index1 < 0 || index1 >= index2)
            throw new InvalidOperationException();

        if (processor.ScalarProcessor.IsZero(scalar))
            return new XGaBivector<T>(processor);

        return new XGaBivector<T>(
            processor,

            new SingleItemDictionary<IIndexSet, T>(
                IndexSetUtils.IndexPairToIndexSet(index1, index2),
                scalar
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> BivectorTerm<T>(this XGaProcessor<T> processor, int index1, int index2, IScalar<T> scalar)
    {
        return processor.BivectorTerm(index1, index2, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> BivectorTerm<T>(this XGaProcessor<T> processor, KeyValuePair<Int32Pair, T> indexScalarPair)
    {
        return processor.BivectorTerm(

            indexScalarPair.Key,
            indexScalarPair.Value
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> BivectorTerm<T>(this XGaProcessor<T> processor, KeyValuePair<IIndexSet, T> indexScalarPair)
    {
        return processor.BivectorTerm(indexScalarPair.Key, indexScalarPair.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> BivectorTerm<T>(this XGaProcessor<T> processor, IIndexSet basisBlade)
    {
        return new XGaBivector<T>(
            processor,

            new SingleItemDictionary<IIndexSet, T>(basisBlade, processor.ScalarProcessor.OneValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> BivectorTerm<T>(this XGaProcessor<T> processor, IIndexSet basisBlade, T scalar)
    {
        if (processor.ScalarProcessor.IsZero(scalar))
            return new XGaBivector<T>(processor);

        return new XGaBivector<T>(
            processor,

            new SingleItemDictionary<IIndexSet, T>(basisBlade, scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> BivectorTerm<T>(this XGaProcessor<T> processor, IIndexSet basisBlade, Scalar<T> scalar)
    {
        if (scalar.IsZero())
            return new XGaBivector<T>(processor);

        return new XGaBivector<T>(
            processor,
            new SingleItemDictionary<IIndexSet, T>(basisBlade, scalar.ScalarValue)
        );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> Bivector2D<T>(this XGaProcessor<T> processor, double scalar01)
    {
        return processor
            .CreateComposer()
            .SetTerm(3, scalar01)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> Bivector3D<T>(this XGaProcessor<T> processor, double scalar01, double scalar02, double scalar12)
    {
        return processor
            .CreateComposer()
            .SetTerm(3, scalar01)
            .SetTerm(5, scalar02)
            .SetTerm(6, scalar12)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> Bivector3D<T>(this XGaProcessor<T> processor, T scalar01, T scalar02, T scalar12)
    {
        return processor
            .CreateComposer()
            .SetTerm(3, scalar01)
            .SetTerm(5, scalar02)
            .SetTerm(6, scalar12)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> Bivector3D<T>(this XGaProcessor<T> processor, LinFloat64Bivector3D bivector)
    {
        return processor
            .CreateComposer()
            .SetTerm(3, bivector.Xy)
            .SetTerm(5, bivector.Xz)
            .SetTerm(6, bivector.Yz)
            .GetBivector();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> ToXGaBivector<T>(this LinFloat64Bivector2D bivector, XGaProcessor<T> processor)
    {
        return processor
            .CreateComposer()
            .SetBivectorTerm(0, 1, bivector.Xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> ToXGaBivector<T>(this LinFloat64Bivector3D bivector, XGaProcessor<T> processor)
    {
        return processor
            .CreateComposer()
            .SetBivectorTerm(0, 1, bivector.Xy)
            .SetBivectorTerm(0, 2, bivector.Xz)
            .SetBivectorTerm(1, 2, bivector.Yz)
            .GetBivector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> ToXGaBivector<T>(this LinBivector2D<T> bivector, XGaProcessor<T> processor)
    {
        return processor
            .CreateComposer()
            .SetBivectorTerm(0, 1, bivector.Xy)
            .GetBivector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> ToXGaBivector<T>(this LinBivector3D<T> bivector, XGaProcessor<T> processor)
    {
        return processor
            .CreateComposer()
            .SetBivectorTerm(0, 1, bivector.Xy)
            .SetBivectorTerm(0, 2, bivector.Xz)
            .SetBivectorTerm(1, 2, bivector.Yz)
            .GetBivector();
    }
}