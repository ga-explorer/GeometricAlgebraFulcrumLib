﻿using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Subspaces;

public static class XGaSubspaceComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSubspace<T> CreateScalarSubspace<T>(this XGaProcessor<T> processor)
    {
        return new XGaSubspace<T>(
            processor.Scalar(processor.ScalarProcessor.OneValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSubspace<T> CreateVectorSubspace<T>(this XGaProcessor<T> processor, int index)
    {
        return new XGaSubspace<T>(
            processor.VectorTerm(index)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSubspace<T> CreateBivectorSubspace<T>(this XGaProcessor<T> processor, ulong index)
    {
        var id = 
            index.BasisBivectorIndexToId();

        return new XGaSubspace<T>(
            processor.KVectorTerm(
                id,
                processor.ScalarProcessor.OneValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSubspace<T> CreateBivectorSubspace<T>(this XGaProcessor<T> processor, int index1, int index2)
    {
        var id =
            IndexSet.CreatePair(index1, index2);

        return new XGaSubspace<T>(
            processor.KVectorTerm(
                id,
                processor.ScalarProcessor.OneValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSubspace<T> CreateSubspace<T>(this XGaProcessor<T> processor, ulong id)
    {
        return new XGaSubspace<T>(
            processor.KVectorTerm(
                id.ToUInt64IndexSet(),
                processor.ScalarProcessor.OneValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSubspace<T> CreateSubspace<T>(this XGaProcessor<T> processor, int grade, ulong index)
    {
        var id = 
            BasisBladeUtils.BasisBladeGradeIndexToId((uint)grade, index);

        return new XGaSubspace<T>(
            processor.KVectorTerm(
                id,
                processor.ScalarProcessor.OneValue
            )
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSubspace<T> CreateSubspace<T>(this XGaProcessor<T> processor, IndexSet id)
    {
        return new XGaSubspace<T>(
            processor.KVectorTerm(
                id,
                processor.ScalarProcessor.OneValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSubspace<T> CreatePseudoScalarSubspace<T>(this XGaProcessor<T> processor, int vSpaceDimensions)
    {
        return new XGaSubspace<T>(
            processor.PseudoScalar(vSpaceDimensions)
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