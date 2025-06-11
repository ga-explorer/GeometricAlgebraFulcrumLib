using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;

public static class LinBasisVectorComposerUtils
{
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinBasisVector CreateZeroBasisVector()
    //{
    //    var basisBlade = 0.ToLinBasisVector();

    //    return new LinBasisVector(basisBlade, IntegerSign.Zero);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector ToLinBasisVector(this int index)
    {
        return LinBasisVector.Positive(index);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector ToLinBasisVector(this int index, IntegerSign sign)
    {
        return LinBasisVector.Create(index, sign);
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
            .Select(LinBasisVector.Positive);
    }

}