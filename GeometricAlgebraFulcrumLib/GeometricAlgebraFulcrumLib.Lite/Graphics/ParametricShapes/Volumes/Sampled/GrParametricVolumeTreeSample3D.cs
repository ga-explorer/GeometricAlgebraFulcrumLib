using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.ParametricShapes.Volumes.Sampled;

public sealed record GrParametricVolumeTreeSample3D
{
    public GrParametricVolumeTreeLeaf3D LeafNode { get; }

    public double ParameterValue1 { get; }

    public double ParameterValue2 { get; }
        
    public double ParameterValue3 { get; }

    public double InterpolationValue1 { get; }

    public double InterpolationValue2 { get; }

    public double InterpolationValue3 { get; }


    internal GrParametricVolumeTreeSample3D(GrParametricVolumeTreeLeaf3D leafNode, double parameterValue1, double parameterValue2, double parameterValue3)
    {
        LeafNode = leafNode;
            
        ParameterValue1 = parameterValue1;
        ParameterValue2 = parameterValue2;
        ParameterValue3 = parameterValue3;

        InterpolationValue1 = 
            (ParameterValue1 - leafNode.MinParameterValue1) / 
            (leafNode.MaxParameterValue1 - leafNode.MinParameterValue1);

        InterpolationValue2 = 
            (ParameterValue2 - leafNode.MinParameterValue2) / 
            (leafNode.MaxParameterValue2 - leafNode.MinParameterValue2);

        InterpolationValue3 = 
            (ParameterValue3 - leafNode.MinParameterValue3) / 
            (leafNode.MaxParameterValue3 - leafNode.MinParameterValue3);
    }


    public Float64Vector3D GetPoint()
    {
        var t10 = 1d - InterpolationValue1;
        var t11 = InterpolationValue1;

        var t20 = 1 - InterpolationValue2;
        var t21 = InterpolationValue2;
            
        var t30 = 1 - InterpolationValue3;
        var t31 = InterpolationValue3;

        var s000 = t10 * t20 * t30;
        var s001 = t10 * t20 * t31;
        var s010 = t10 * t21 * t30;
        var s011 = t10 * t21 * t31;
        var s100 = t11 * t20 * t30;
        var s101 = t11 * t20 * t31;
        var s110 = t11 * t21 * t30;
        var s111 = t11 * t21 * t31;

        return s000 * LeafNode.Frame000.Point +
               s001 * LeafNode.Frame001.Point +
               s010 * LeafNode.Frame010.Point +
               s011 * LeafNode.Frame011.Point +
               s100 * LeafNode.Frame100.Point +
               s101 * LeafNode.Frame101.Point +
               s110 * LeafNode.Frame110.Point +
               s111 * LeafNode.Frame111.Point;
    }

    public double GetScalarDistance()
    {
        var t10 = 1d - InterpolationValue1;
        var t11 = InterpolationValue1;

        var t20 = 1 - InterpolationValue2;
        var t21 = InterpolationValue2;
            
        var t30 = 1 - InterpolationValue3;
        var t31 = InterpolationValue3;

        var s000 = t10 * t20 * t30;
        var s001 = t10 * t20 * t31;
        var s010 = t10 * t21 * t30;
        var s011 = t10 * t21 * t31;
        var s100 = t11 * t20 * t30;
        var s101 = t11 * t20 * t31;
        var s110 = t11 * t21 * t30;
        var s111 = t11 * t21 * t31;

        return s000 * LeafNode.Frame000.ScalarDistance +
               s001 * LeafNode.Frame001.ScalarDistance +
               s010 * LeafNode.Frame010.ScalarDistance +
               s011 * LeafNode.Frame011.ScalarDistance +
               s100 * LeafNode.Frame100.ScalarDistance +
               s101 * LeafNode.Frame101.ScalarDistance +
               s110 * LeafNode.Frame110.ScalarDistance +
               s111 * LeafNode.Frame111.ScalarDistance;
    }

    public GrParametricVolumeLocalFrame3D GetFrame()
    {
        var t10 = 1d - InterpolationValue1;
        var t11 = InterpolationValue1;

        var t20 = 1 - InterpolationValue2;
        var t21 = InterpolationValue2;
            
        var t30 = 1 - InterpolationValue3;
        var t31 = InterpolationValue3;

        var s000 = t10 * t20 * t30;
        var s001 = t10 * t20 * t31;
        var s010 = t10 * t21 * t30;
        var s011 = t10 * t21 * t31;
        var s100 = t11 * t20 * t30;
        var s101 = t11 * t20 * t31;
        var s110 = t11 * t21 * t30;
        var s111 = t11 * t21 * t31;

        var point = 
            s000 * LeafNode.Frame000.Point + 
            s001 * LeafNode.Frame001.Point + 
            s010 * LeafNode.Frame010.Point + 
            s011 * LeafNode.Frame011.Point + 
            s100 * LeafNode.Frame100.Point + 
            s101 * LeafNode.Frame101.Point + 
            s110 * LeafNode.Frame110.Point + 
            s111 * LeafNode.Frame111.Point;

        var scalarDistance = 
            s000 * LeafNode.Frame000.ScalarDistance +
            s001 * LeafNode.Frame001.ScalarDistance +
            s010 * LeafNode.Frame010.ScalarDistance +
            s011 * LeafNode.Frame011.ScalarDistance +
            s100 * LeafNode.Frame100.ScalarDistance +
            s101 * LeafNode.Frame101.ScalarDistance +
            s110 * LeafNode.Frame110.ScalarDistance +
            s111 * LeafNode.Frame111.ScalarDistance;

        return new GrParametricVolumeLocalFrame3D(
            ParameterValue1,
            ParameterValue2,
            ParameterValue3,
            point,
            scalarDistance
        );
    }
}