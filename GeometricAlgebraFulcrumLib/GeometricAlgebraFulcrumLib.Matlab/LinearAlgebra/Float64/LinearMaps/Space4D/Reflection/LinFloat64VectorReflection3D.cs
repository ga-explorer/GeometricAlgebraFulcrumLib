using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection;

public sealed class LinFloat64VectorReflection4D :
    ILinFloat64UnilinearMap4D
{
    public LinFloat64Vector4D ReflectionVector { get; }

    public int VSpaceDimensions
        => 3;

    public bool SwapsHandedness
        => true;


    
    public LinFloat64VectorReflection4D(LinFloat64Vector4D vector)
    {
        ReflectionVector = vector;
    }


    
    public bool IsValid()
    {
        return ReflectionVector.IsValid();
    }

    
    public bool IsIdentity()
    {
        return false;
    }

    
    public bool IsNearIdentity(double zeroEpsilon = 1E-12)
    {
        return false;
    }

    
    public LinFloat64Vector4D MapBasisVector(int basisIndex)
    {
        Debug.Assert(basisIndex >= 0);

        var s = 2d * ReflectionVector[basisIndex];

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(ReflectionVector, s)
            .SubtractTerm(basisIndex, 1d)
            .GetVector();
    }

    
    public LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector)
    {
        var s = 2d * vector.VectorESp(ReflectionVector);

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(ReflectionVector, s)
            .SubtractVector(vector)
            .GetVector();
    }

    
    public LinFloat64VectorReflection4D GetVectorReflectionInverse()
    {
        return this;
    }

    
    public ILinFloat64UnilinearMap4D GetInverseMap()
    {
        return GetVectorReflectionInverse();
    }
}