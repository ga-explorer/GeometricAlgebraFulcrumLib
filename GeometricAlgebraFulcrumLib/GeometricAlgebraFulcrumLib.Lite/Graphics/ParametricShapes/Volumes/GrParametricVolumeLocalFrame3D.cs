using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.ParametricShapes.Volumes;

public sealed record GrParametricVolumeLocalFrame3D :
    IFloat64Vector3D
{
        
    public int VSpaceDimensions 
        => 3;

    public double Item1
        => Point.X;
        
    public double Item2
        => Point.Y;
        
    public double Item3
        => Point.Z;

    public Float64Scalar X 
        => Point.X;

    public Float64Scalar Y 
        => Point.Y;

    public Float64Scalar Z 
        => Point.Z;

    public int Index { get; internal set; } 
        = -1;

    public Float64Vector3D ParameterValue { get; }

    public Float64Vector3D Point { get; }
        
    public Color Color { get; set; }
        
    public double ScalarDistance { get; }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricVolumeLocalFrame3D(double parameterValue1, double parameterValue2, double parameterValue3, IFloat64Vector3D point, double scalarDistance)
    {
        ParameterValue = Float64Vector3D.Create(parameterValue1, parameterValue2, parameterValue3);
        Point = point.ToVector3D();
        ScalarDistance = scalarDistance;

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricVolumeLocalFrame3D(IFloat64Vector3D parameterValue, IFloat64Vector3D point, double scalarDistance)
    {
        ParameterValue = parameterValue.ToVector3D();
        Point = point.ToVector3D();
        ScalarDistance = scalarDistance;

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricVolumeLocalFrame3D(IGraphicsParametricVolume3D volume, double parameterValue1, double parameterValue2, double parameterValue3)
    {
        ParameterValue = Float64Vector3D.Create(parameterValue1, parameterValue2, parameterValue3);
        Point = volume.GetPoint(parameterValue1, parameterValue2, parameterValue3);
        ScalarDistance = volume.GetScalarDistance(parameterValue1, parameterValue2, parameterValue3);

        Debug.Assert(IsValid());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricVolumeLocalFrame3D(IGraphicsParametricVolume3D volume, IFloat64Vector3D parameterValue)
    {
        ParameterValue = parameterValue.ToVector3D();
        Point = volume.GetPoint(parameterValue);
        ScalarDistance = volume.GetScalarDistance(parameterValue);

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return ParameterValue.Item1.IsValid() &&
               ParameterValue.Item2.IsValid() &&
               ParameterValue.Item3.IsValid() &&
               Point.IsValid() &&
               ScalarDistance.IsValid();
    }
}