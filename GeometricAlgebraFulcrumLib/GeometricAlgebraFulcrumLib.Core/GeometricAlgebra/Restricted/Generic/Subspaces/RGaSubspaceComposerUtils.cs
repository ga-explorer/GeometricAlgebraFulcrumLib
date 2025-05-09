using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Subspaces;

public static class RGaSubspaceComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSubspace<T> CreateScalarSubspace<T>(this RGaProcessor<T> processor)
    {
        return new RGaSubspace<T>(
            processor.Scalar(processor.ScalarProcessor.OneValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSubspace<T> CreateVectorSubspace<T>(this RGaProcessor<T> processor, int index)
    {
        return new RGaSubspace<T>(
            processor.VectorTerm(
                index, 
                processor.ScalarProcessor.OneValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSubspace<T> CreateBivectorSubspace<T>(this RGaProcessor<T> processor, ulong index)
    {
        var id = 
            index.BasisBivectorIndexToId();

        return new RGaSubspace<T>(
            processor.KVectorTerm(
                id,
                processor.ScalarProcessor.OneValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSubspace<T> CreateBivectorSubspace<T>(this RGaProcessor<T> processor, int index1, int index2)
    {
        var id =
            IndexSetUtils.IndexPairToUInt64IndexSetBitPattern(index1, index2);

        return new RGaSubspace<T>(
            processor.KVectorTerm(
                id,
                processor.ScalarProcessor.OneValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSubspace<T> CreateSubspace<T>(this RGaProcessor<T> processor, ulong id)
    {
        return new RGaSubspace<T>(
            processor.KVectorTerm(
                id,
                processor.ScalarProcessor.OneValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSubspace<T> CreateSubspace<T>(this RGaProcessor<T> processor, int grade, ulong index)
    {
        var id = 
            BasisBladeUtils.BasisBladeGradeIndexToId((uint) grade, index);

        return new RGaSubspace<T>(
            processor.KVectorTerm(
                id,
                processor.ScalarProcessor.OneValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSubspace<T> CreatePseudoScalarSubspace<T>(this RGaProcessor<T> processor, int vSpaceDimensions)
    {
        return new RGaSubspace<T>(
            processor.PseudoScalar(vSpaceDimensions)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSubspace<T> ToSubspace<T>(this RGaKVector<T> blade)
    {
        return new RGaSubspace<T>(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSubspace<T> DualToSubspace<T>(this RGaKVector<T> blade, int vSpaceDimensions)
    {
        return new RGaSubspace<T>(
            blade.Dual(vSpaceDimensions)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSubspace<T> UnDualToSubspace<T>(this RGaKVector<T> blade, int vSpaceDimensions)
    {
        return new RGaSubspace<T>(
            blade.UnDual(vSpaceDimensions)
        );
    }


}