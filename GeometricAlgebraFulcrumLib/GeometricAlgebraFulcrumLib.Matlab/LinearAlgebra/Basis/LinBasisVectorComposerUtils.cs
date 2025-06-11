using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;

public static class LinBasisVectorComposerUtils
{
    
    //
    //public static LinBasisVector CreateZeroBasisVector()
    //{
    //    var basisBlade = 0.ToLinBasisVector();

    //    return new LinBasisVector(basisBlade, IntegerSign.Zero);
    //}

    
    public static LinBasisVector ToLinBasisVector(this int index)
    {
        return LinBasisVector.Positive(index);
    }
    
    
    public static LinBasisVector ToLinBasisVector(this int index, IntegerSign sign)
    {
        return LinBasisVector.Create(index, sign);
    }
    
    
    public static IEnumerable<int> GetLinBasisVectorIndices(this int vSpaceDimensions)
    {
        return vSpaceDimensions.GetRange();
    }

    
    public static IEnumerable<LinBasisVector> GetLinBasisVectors(this int vSpaceDimensions)
    {
        return vSpaceDimensions
            .GetRange()
            .Select(LinBasisVector.Positive);
    }

}