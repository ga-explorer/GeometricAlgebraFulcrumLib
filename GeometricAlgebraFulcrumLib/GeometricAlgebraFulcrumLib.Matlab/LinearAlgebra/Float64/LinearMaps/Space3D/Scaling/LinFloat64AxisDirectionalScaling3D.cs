using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Scaling;

public sealed class LinFloat64AxisDirectionalScaling3D :
    LinFloat64DirectionalScalingLinearMap3D
{
    
    public static LinFloat64AxisDirectionalScaling3D Create(double scalingFactor, int scalingBasisIndex)
    {
        return new LinFloat64AxisDirectionalScaling3D(
            scalingFactor,
            scalingBasisIndex
        );
    }

    
    public static LinFloat64AxisDirectionalScaling3D Create(double scalingFactor, LinBasisVector scalingAxis)
    {
        return new LinFloat64AxisDirectionalScaling3D(
            scalingFactor,
            scalingAxis.Index
        );
    }


    public override double ScalingFactor { get; }

    public LinBasisVector ScalingAxis { get; }

    public override LinFloat64Vector3D ScalingVector
        => ScalingAxis.ToLinVector3D();


    
    public LinFloat64AxisDirectionalScaling3D(double factor, int basisIndex)
    {
        Debug.Assert(
            factor.IsNotZero()
        );

        ScalingFactor = factor;
        ScalingAxis = basisIndex.ToAxis3D(false);
    }


    
    public override bool IsValid()
    {
        return
            ScalingVector.IsNearUnit() &&
            ScalingFactor.IsNotZero();
    }

    
    public override LinFloat64Vector3D MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0
        );

        var composer = LinFloat64Vector3DComposer.Create();

        composer.SetTerm(basisIndex, 1d);

        if (basisIndex == ScalingAxis.Index)
            composer.AddTerm(basisIndex, ScalingFactor - 1d);

        return composer.GetVector();
    }

    
    public override LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        return LinFloat64Vector3DComposer.Create()
            .SetVector(vector)
            .AddTerm(
                ScalingAxis.Index,
                (ScalingFactor - 1d) * vector.GetItem(ScalingAxis.Index)
            ).GetVector();
    }

    
    public override ILinFloat64DirectionalScalingLinearMap3D GetDirectionalScalingInverse()
    {
        return GetAxisScalingInverse();
    }

    
    public LinFloat64AxisDirectionalScaling3D GetAxisScalingInverse()
    {
        return new LinFloat64AxisDirectionalScaling3D(
            1d / ScalingFactor,
            ScalingAxis.Index
        );
    }

    
    public override LinFloat64VectorDirectionalScaling3D ToVectorDirectionalScaling()
    {
        return LinFloat64VectorDirectionalScaling3D.Create(
            1d,
            ScalingVector
        );
    }
}