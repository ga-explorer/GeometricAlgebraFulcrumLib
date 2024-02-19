using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Subspaces;

public static class RGaSubspaceComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSubspace<T> CreateScalarSubspace<T>(this RGaProcessor<T> processor)
    {
        return new RGaSubspace<T>(
            processor.CreateScalar(processor.ScalarProcessor.ScalarOne)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSubspace<T> CreateVectorSubspace<T>(this RGaProcessor<T> processor, int index)
    {
        return new RGaSubspace<T>(
            processor.CreateTermVector(
                index, 
                processor.ScalarProcessor.ScalarOne
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSubspace<T> CreateBivectorSubspace<T>(this RGaProcessor<T> processor, ulong index)
    {
        var id = 
            index.BasisBivectorIndexToId();

        return new RGaSubspace<T>(
            processor.CreateTermKVector(
                id,
                processor.ScalarProcessor.ScalarOne
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSubspace<T> CreateBivectorSubspace<T>(this RGaProcessor<T> processor, int index1, int index2)
    {
        var id =
            IndexSetUtils.IndexPairToUInt64IndexSetBitPattern(index1, index2);

        return new RGaSubspace<T>(
            processor.CreateTermKVector(
                id,
                processor.ScalarProcessor.ScalarOne
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSubspace<T> CreateSubspace<T>(this RGaProcessor<T> processor, ulong id)
    {
        return new RGaSubspace<T>(
            processor.CreateTermKVector(
                id,
                processor.ScalarProcessor.ScalarOne
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSubspace<T> CreateSubspace<T>(this RGaProcessor<T> processor, int grade, ulong index)
    {
        var id = 
            BasisBladeUtils.BasisBladeGradeIndexToId((uint) grade, index);

        return new RGaSubspace<T>(
            processor.CreateTermKVector(
                id,
                processor.ScalarProcessor.ScalarOne
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSubspace<T> CreatePseudoScalarSubspace<T>(this RGaProcessor<T> processor, int vSpaceDimensions)
    {
        return new RGaSubspace<T>(
            processor.CreatePseudoScalar(vSpaceDimensions)
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