using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Scaling;

public sealed class LinFloat64VectorDirectionalScaling3D :
    LinFloat64DirectionalScalingLinearMap3D
{
    
    public static LinFloat64VectorDirectionalScaling3D Create(double scalingFactor, ILinFloat64Vector3D scalingVector)
    {
        return new LinFloat64VectorDirectionalScaling3D(scalingFactor, scalingVector.ToLinVector3D());
    }


    public override double ScalingFactor { get; }

    public override LinFloat64Vector3D ScalingVector { get; }


    
    private LinFloat64VectorDirectionalScaling3D(double factor, LinFloat64Vector3D vector)
    {
        Debug.Assert(
            vector.IsNearUnit() &&
            factor.IsValid() &&
            factor.IsNotZero()
        );

        ScalingFactor = factor;
        ScalingVector = vector;
    }


    
    public override bool IsValid()
    {
        return
            ScalingVector.IsNearUnit() &&
            ScalingFactor.IsNotNaN() &&
            ScalingFactor.IsNotZero();
    }

    
    public override LinFloat64Vector3D MapBasisVector(int basisIndex)
    {
        var s = (ScalingFactor - 1d) * ScalingVector[1 << basisIndex];

        return LinFloat64Vector3DComposer
            .Create()
            .SetVector(ScalingVector, s)
            .AddTerm(basisIndex, 1d)
            .GetVector();
    }

    
    public override LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        var s = (ScalingFactor - 1d) * vector.VectorESp(ScalingVector);

        return LinFloat64Vector3DComposer
            .Create()
            .SetVector(vector)
            .AddVector(ScalingVector, s)
            .GetVector();
    }

    
    public override ILinFloat64DirectionalScalingLinearMap3D GetDirectionalScalingInverse()
    {
        return GetVectorDirectionalScalingInverse();
    }

    
    public LinFloat64VectorDirectionalScaling3D GetVectorDirectionalScalingInverse()
    {
        return new LinFloat64VectorDirectionalScaling3D(
            1d / ScalingFactor,
            ScalingVector
        );
    }

    
    public override LinFloat64VectorDirectionalScaling3D ToVectorDirectionalScaling()
    {
        return this;
    }
}