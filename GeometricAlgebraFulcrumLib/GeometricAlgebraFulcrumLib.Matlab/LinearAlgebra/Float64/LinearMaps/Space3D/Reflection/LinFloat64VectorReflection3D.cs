using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Reflection;

public sealed class LinFloat64VectorReflection3D :
    ILinFloat64UnilinearMap3D
{
    public LinFloat64Vector3D ReflectionVector { get; }

    public int VSpaceDimensions
        => 3;

    public bool SwapsHandedness
        => true;


    
    public LinFloat64VectorReflection3D(LinFloat64Vector3D vector)
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

    
    public LinFloat64Vector3D MapBasisVector(int basisIndex)
    {
        Debug.Assert(basisIndex >= 0);

        var s = 2d * ReflectionVector[1 << basisIndex];

        return LinFloat64Vector3DComposer
            .Create()
            .SetVector(ReflectionVector, s)
            .SubtractTerm(basisIndex, 1d)
            .GetVector();
    }

    
    public LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        var s = 2d * vector.VectorESp(ReflectionVector);

        return LinFloat64Vector3DComposer
            .Create()
            .SetVector(ReflectionVector, s)
            .SubtractVector(vector)
            .GetVector();
    }

    
    public LinFloat64VectorReflection3D GetVectorReflectionInverse()
    {
        return this;
    }

    
    public ILinFloat64UnilinearMap3D GetInverseMap()
    {
        return GetVectorReflectionInverse();
    }
}