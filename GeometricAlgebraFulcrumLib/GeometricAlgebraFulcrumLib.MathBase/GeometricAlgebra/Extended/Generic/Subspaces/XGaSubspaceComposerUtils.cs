using DataStructuresLib.IndexSets;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Subspaces;

public static class XGaSubspaceComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSubspace<T> CreateScalarSubspace<T>(this XGaProcessor<T> processor)
    {
        return new XGaSubspace<T>(
            processor.CreateScalar(processor.ScalarProcessor.ScalarOne)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSubspace<T> CreateVectorSubspace<T>(this XGaProcessor<T> processor, int index)
    {
        return new XGaSubspace<T>(
            processor.CreateTermVector(index)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSubspace<T> CreateBivectorSubspace<T>(this XGaProcessor<T> processor, ulong index)
    {
        var id = 
            index.BasisBivectorIndexToId().BitPatternToUInt64IndexSet();

        return new XGaSubspace<T>(
            processor.CreateTermKVector(
                id,
                processor.ScalarProcessor.ScalarOne
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSubspace<T> CreateBivectorSubspace<T>(this XGaProcessor<T> processor, int index1, int index2)
    {
        var id =
            IndexSetUtils.IndexPairToIndexSet(index1, index2);

        return new XGaSubspace<T>(
            processor.CreateTermKVector(
                id,
                processor.ScalarProcessor.ScalarOne
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSubspace<T> CreateSubspace<T>(this XGaProcessor<T> processor, ulong id)
    {
        return new XGaSubspace<T>(
            processor.CreateTermKVector(
                id.BitPatternToUInt64IndexSet(),
                processor.ScalarProcessor.ScalarOne
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSubspace<T> CreateSubspace<T>(this XGaProcessor<T> processor, int grade, ulong index)
    {
        var id = 
            BasisBladeUtils.BasisBladeGradeIndexToId((uint) grade, index).BitPatternToUInt64IndexSet();

        return new XGaSubspace<T>(
            processor.CreateTermKVector(
                id,
                processor.ScalarProcessor.ScalarOne
            )
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSubspace<T> CreateSubspace<T>(this XGaProcessor<T> processor, IIndexSet id)
    {
        return new XGaSubspace<T>(
            processor.CreateTermKVector(
                id,
                processor.ScalarProcessor.ScalarOne
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSubspace<T> CreatePseudoScalarSubspace<T>(this XGaProcessor<T> processor, int vSpaceDimensions)
    {
        return new XGaSubspace<T>(
            processor.CreatePseudoScalar(vSpaceDimensions)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSubspace<T> ToSubspace<T>(this XGaKVector<T> blade)
    {
        return new XGaSubspace<T>(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSubspace<T> DualToSubspace<T>(this XGaKVector<T> blade, int vSpaceDimensions)
    {
        return new XGaSubspace<T>(
            blade.Dual(vSpaceDimensions)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSubspace<T> UnDualToSubspace<T>(this XGaKVector<T> blade, int vSpaceDimensions)
    {
        return new XGaSubspace<T>(
            blade.UnDual(vSpaceDimensions)
        );
    }


}