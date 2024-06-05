﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Basis;

public static class LinBasisVectorComposerUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinSignedBasisVector CreateZeroBasisVector()
    {
        var basisBlade = 0.ToLinBasisVector();

        return new LinSignedBasisVector(basisBlade, IntegerSign.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector ToLinBasisVector(this int index)
    {
        return new LinBasisVector(index);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinSignedBasisVector ToLinBasisVector(this int index, IntegerSign sign)
    {
        var basisVector = index.ToLinBasisVector();

        return sign.IsPositive
            ? basisVector
            : new LinSignedBasisVector(basisVector, sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> GetLinBasisVectorIndices(this int vSpaceDimensions)
    {
        return vSpaceDimensions.GetRange();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinBasisVector> GetLinBasisVectors(this int vSpaceDimensions)
    {
        return vSpaceDimensions
            .GetRange()
            .Select(id => new LinBasisVector(id));
    }

}