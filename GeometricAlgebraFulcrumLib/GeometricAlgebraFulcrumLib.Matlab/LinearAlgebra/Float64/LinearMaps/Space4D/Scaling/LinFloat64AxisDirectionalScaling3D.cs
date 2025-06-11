using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space4D.Scaling;

public sealed class LinFloat64AxisDirectionalScaling4D :
    LinFloat64DirectionalScalingLinearMap4D
{
    
    public static LinFloat64AxisDirectionalScaling4D Create(double scalingFactor, int scalingBasisIndex)
    {
        return new LinFloat64AxisDirectionalScaling4D(
            scalingFactor,
            scalingBasisIndex
        );
    }

    
    public static LinFloat64AxisDirectionalScaling4D Create(double scalingFactor, LinBasisVector scalingAxis)
    {
        return new LinFloat64AxisDirectionalScaling4D(
            scalingFactor,
            scalingAxis.Index
        );
    }


    public override double ScalingFactor { get; }

    public LinBasisVector ScalingAxis { get; }

    public override LinFloat64Vector4D ScalingVector
        => ScalingAxis.ToLinVector4D();


    
    public LinFloat64AxisDirectionalScaling4D(double factor, int basisIndex)
    {
        Debug.Assert(
            factor.IsNotZero()
        );

        ScalingFactor = factor;
        ScalingAxis = basisIndex.ToAxis4D(false);
    }


    
    public override bool IsValid()
    {
        return
            ScalingVector.IsNearUnit() &&
            ScalingFactor.IsNotZero();
    }

    
    public override LinFloat64Vector4D MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0
        );

        var composer = new LinFloat64Vector4DComposer();

        composer.SetTerm(basisIndex, 1d);

        if (basisIndex == ScalingAxis.Index)
            composer.AddTerm(basisIndex, ScalingFactor - 1d);

        return composer.GetVector();
    }

    
    public override LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector)
    {
        return new LinFloat64Vector4DComposer()
            .SetVector(vector)
            .AddTerm(
                ScalingAxis.Index,
                (ScalingFactor - 1d) * vector.GetItem(ScalingAxis.Index)
            ).GetVector();
    }

    
    public override ILinFloat64DirectionalScalingLinearMap4D GetDirectionalScalingInverse()
    {
        return GetAxisScalingInverse();
    }

    
    public LinFloat64AxisDirectionalScaling4D GetAxisScalingInverse()
    {
        return new LinFloat64AxisDirectionalScaling4D(
            1d / ScalingFactor,
            ScalingAxis.Index
        );
    }

    
    public override LinFloat64VectorDirectionalScaling4D ToVectorDirectionalScaling()
    {
        return LinFloat64VectorDirectionalScaling4D.Create(
            1d,
            ScalingVector
        );
    }
}