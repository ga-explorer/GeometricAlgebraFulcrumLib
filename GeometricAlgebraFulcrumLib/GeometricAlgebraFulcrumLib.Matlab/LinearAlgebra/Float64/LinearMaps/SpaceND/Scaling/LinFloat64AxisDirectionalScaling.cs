using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;

public sealed class LinFloat64AxisDirectionalScaling :
    LinFloat64DirectionalScalingLinearMap
{
    
    public static LinFloat64AxisDirectionalScaling Create(int dimensions, double scalingFactor, int scalingBasisIndex)
    {
        return new LinFloat64AxisDirectionalScaling(
            scalingFactor,
            dimensions,
            scalingBasisIndex
        );
    }

    
    public static LinFloat64AxisDirectionalScaling Create(double scalingFactor, int dimensions, LinBasisVector scalingAxis)
    {
        return new LinFloat64AxisDirectionalScaling(
            scalingFactor,
            dimensions,
            scalingAxis.Index
        );
    }


    public override double ScalingFactor { get; }

    public LinBasisVector ScalingAxis { get; }

    public override int VSpaceDimensions { get; }

    public override LinFloat64Vector ScalingVector
        => ScalingAxis.ToLinVector();


    
    public LinFloat64AxisDirectionalScaling(double factor, int dimensions, int basisIndex)
    {
        Debug.Assert(
            factor.IsNotZero()
        );

        VSpaceDimensions = dimensions;
        ScalingFactor = factor;
        ScalingAxis = LinBasisVector.Positive(basisIndex);
    }


    
    public override bool IsValid()
    {
        return
            ScalingVector.IsNearUnit() &&
            ScalingFactor.IsNotZero();
    }

    
    public override LinFloat64Vector MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0
        );

        var composer = LinFloat64VectorComposer.Create();

        composer.SetTerm(basisIndex, 1d);

        if (basisIndex == ScalingAxis.Index)
            composer.AddTerm(basisIndex, ScalingFactor - 1d);

        return composer.GetVector();
    }

    
    public override LinFloat64Vector MapVector(LinFloat64Vector vector)
    {
        return LinFloat64VectorComposer.Create()
            .SetVector(vector)
            .AddTerm(
                ScalingAxis.Index,
                (ScalingFactor - 1d) * vector[ScalingAxis.Index]
            ).GetVector();
    }

    
    public override ILinFloat64DirectionalScalingLinearMap GetDirectionalScalingInverse()
    {
        return GetAxisScalingInverse();
    }

    
    public LinFloat64AxisDirectionalScaling GetAxisScalingInverse()
    {
        return new LinFloat64AxisDirectionalScaling(
            1d / ScalingFactor,
            VSpaceDimensions,
            ScalingAxis.Index
        );
    }

    
    public override LinFloat64VectorDirectionalScaling ToVectorDirectionalScaling()
    {
        return LinFloat64VectorDirectionalScaling.Create(
            1d,
            ScalingVector
        );
    }
}