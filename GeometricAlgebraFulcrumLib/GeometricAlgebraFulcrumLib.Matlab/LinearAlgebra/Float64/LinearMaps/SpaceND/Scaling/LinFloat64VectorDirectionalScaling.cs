using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;

public sealed class LinFloat64VectorDirectionalScaling :
    LinFloat64DirectionalScalingLinearMap
{
    
    public static LinFloat64VectorDirectionalScaling Create(double scalingFactor, LinFloat64Vector scalingVector)
    {
        return new LinFloat64VectorDirectionalScaling(scalingFactor, scalingVector);
    }


    public override int VSpaceDimensions
        => ScalingVector.VSpaceDimensions;

    public override double ScalingFactor { get; }

    public override LinFloat64Vector ScalingVector { get; }


    
    private LinFloat64VectorDirectionalScaling(double factor, LinFloat64Vector vector)
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

    
    public override LinFloat64Vector MapBasisVector(int basisIndex)
    {
        var s = (ScalingFactor - 1d) * ScalingVector[basisIndex];

        return LinFloat64VectorComposer
            .Create()
            .SetVector(ScalingVector, s)
            .AddTerm(basisIndex, 1d)
            .GetVector();
    }

    
    public override LinFloat64Vector MapVector(LinFloat64Vector vector)
    {
        var s = (ScalingFactor - 1d) * Float64ArrayUtils.VectorDot(vector, ScalingVector);

        return LinFloat64VectorComposer
            .Create()
            .SetVector(vector)
            .AddVector(ScalingVector, s)
            .GetVector();
    }

    
    public override ILinFloat64DirectionalScalingLinearMap GetDirectionalScalingInverse()
    {
        return GetVectorDirectionalScalingInverse();
    }

    
    public LinFloat64VectorDirectionalScaling GetVectorDirectionalScalingInverse()
    {
        return new LinFloat64VectorDirectionalScaling(
            1d / ScalingFactor,
            ScalingVector
        );
    }

    
    public override LinFloat64VectorDirectionalScaling ToVectorDirectionalScaling()
    {
        return this;
    }
}