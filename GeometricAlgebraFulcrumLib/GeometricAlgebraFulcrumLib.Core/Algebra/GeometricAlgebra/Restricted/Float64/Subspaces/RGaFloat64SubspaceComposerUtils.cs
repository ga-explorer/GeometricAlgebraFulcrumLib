﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Subspaces;

public static class RGaFloat64SubspaceComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Subspace CreateScalarSubspace(this RGaFloat64Processor metric)
    {
        return new RGaFloat64Subspace(
            metric.Scalar(1d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Subspace CreateVectorSubspace(this RGaFloat64Processor metric, int index)
    {
        return new RGaFloat64Subspace(
            metric.VectorTerm(index, 1d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Subspace CreateBivectorSubspace(this RGaFloat64Processor metric, ulong index)
    {
        var id = 
            index.BasisBivectorIndexToId();

        return new RGaFloat64Subspace(
            metric.KVectorTerm(id, 1d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Subspace CreateBivectorSubspace(this RGaFloat64Processor metric, int index1, int index2)
    {
        var id =
            IndexSetUtils.IndexPairToUInt64IndexSetBitPattern(index1, index2);

        return new RGaFloat64Subspace(
            metric.KVectorTerm(id, 1d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Subspace CreateSubspace(this RGaFloat64Processor metric, ulong id)
    {
        return new RGaFloat64Subspace(
            metric.KVectorTerm(id, 1d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Subspace CreateSubspace(this RGaFloat64Processor metric, int grade, ulong index)
    {
        var id = 
            BasisBladeUtils.BasisBladeGradeIndexToId((uint) grade, index);

        return new RGaFloat64Subspace(
            metric.KVectorTerm(id, 1d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Subspace CreatePseudoScalarSubspace(this RGaFloat64Processor metric, int vSpaceDimensions)
    {
        return new RGaFloat64Subspace(
            metric.PseudoScalar(vSpaceDimensions)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Subspace ToSubspace(this RGaFloat64KVector blade)
    {
        return new RGaFloat64Subspace(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Subspace DualToSubspace(this RGaFloat64KVector blade, int vSpaceDimensions)
    {
        return new RGaFloat64Subspace(
            blade.Dual(vSpaceDimensions)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Subspace UnDualToSubspace(this RGaFloat64KVector blade, int vSpaceDimensions)
    {
        return new RGaFloat64Subspace(
            blade.UnDual(vSpaceDimensions)
        );
    }


}