using System;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Scaling;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Reflection;

public sealed class LinFloat64HyperPlaneAxisReflection3D :
    LinFloat64ReflectionBase3D,
    ILinFloat64HyperPlaneNormalReflectionLinearMap3D
{
    
    public static LinFloat64HyperPlaneAxisReflection3D Create(int axisIndex)
    {
        return new LinFloat64HyperPlaneAxisReflection3D(axisIndex);
    }

    
    public static LinFloat64HyperPlaneAxisReflection3D Create(LinBasisVector axis)
    {
        return new LinFloat64HyperPlaneAxisReflection3D(axis.Index
        );
    }


    public LinBasisVector ReflectionNormalAxis { get; }

    public int ReflectionNormalAxisIndex { get; }

    public LinFloat64Vector3D ReflectionNormal
        => ReflectionNormalAxis.ToLinVector3D();

    public override bool SwapsHandedness
        => true;

    public double ScalingFactor
        => -1d;

    public LinFloat64Vector3D ScalingVector
        => ReflectionNormalAxis.ToLinVector3D();


    
    private LinFloat64HyperPlaneAxisReflection3D(int basisIndex)
    {
        ReflectionNormalAxis = basisIndex switch
        {
            0 => LinBasisVector.Px,
            1 => LinBasisVector.Py,
            2 => LinBasisVector.Pz,
            _ => throw new IndexOutOfRangeException()
        };

        ReflectionNormalAxisIndex = basisIndex;
    }


    
    public override bool IsValid()
    {
        return true;
    }

    
    public override bool IsIdentity()
    {
        return false;
    }

    
    public override bool IsNearIdentity(double zeroEpsilon = 1E-12)
    {
        return false;
    }

    
    public override LinFloat64Vector3D MapBasisVector(int basisIndex)
    {
        Debug.Assert(basisIndex >= 0);

        return LinFloat64Vector3DComposer
            .Create()
            .SetTerm(
                basisIndex,
                ReflectionNormalAxisIndex == basisIndex ? -1d : 1d
            ).GetVector();
    }

    
    public override LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        var composer =
            LinFloat64Vector3DComposer
                .Create()
                .SetVector(vector);

        composer[ReflectionNormalAxisIndex] *= -1d;

        return composer.GetVector();
    }

    
    public override LinFloat64ReflectionBase3D GetReflectionLinearMapInverse()
    {
        return this;
    }

    
    public LinFloat64HyperPlaneAxisReflection3D GetHyperPlaneAxisReflectionInverse()
    {
        return this;
    }

    
    public ILinFloat64HyperPlaneNormalReflectionLinearMap3D GetHyperPlaneNormalReflectionLinearMapInverse()
    {
        return this;
    }

    
    public LinFloat64HyperPlaneNormalReflection3D ToHyperPlaneNormalReflection()
    {
        return LinFloat64HyperPlaneNormalReflection3D.Create(ReflectionNormal);
    }

    
    public ILinFloat64DirectionalScalingLinearMap3D GetDirectionalScalingInverse()
    {
        return this;
    }

    
    public override LinFloat64HyperPlaneNormalReflectionSequence3D ToHyperPlaneReflectionSequence()
    {
        return LinFloat64HyperPlaneNormalReflectionSequence3D.Create(this);
    }

    
    public LinFloat64VectorDirectionalScaling3D ToVectorDirectionalScaling()
    {
        return LinFloat64VectorDirectionalScaling3D.Create(
            -1d,
            ReflectionNormal
        );
    }
}