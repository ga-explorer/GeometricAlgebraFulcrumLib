using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.ParametricShapes.Volumes;

public sealed record GrParametricVolumeLocalFrame3D :
    ILinFloat64Vector3D
{
        
    public int VSpaceDimensions 
        => 3;

    public double Item1
        => Point.X;
        
    public double Item2
        => Point.Y;
        
    public double Item3
        => Point.Z;

    public double X 
        => Point.X;

    public double Y 
        => Point.Y;

    public double Z 
        => Point.Z;
    
    public double this[int index] 
        => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => Float64Scalar.Zero
        };

    public int Index { get; internal set; } 
        = -1;

    public LinFloat64Vector3D ParameterValue { get; }

    public LinFloat64Vector3D Point { get; }
        
    public Color Color { get; set; }
        
    public double ScalarDistance { get; }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricVolumeLocalFrame3D(double parameterValue1, double parameterValue2, double parameterValue3, ILinFloat64Vector3D point, double scalarDistance)
    {
        ParameterValue = LinFloat64Vector3D.Create(parameterValue1, parameterValue2, parameterValue3);
        Point = point.ToLinVector3D();
        ScalarDistance = scalarDistance;

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricVolumeLocalFrame3D(ILinFloat64Vector3D parameterValue, ILinFloat64Vector3D point, double scalarDistance)
    {
        ParameterValue = parameterValue.ToLinVector3D();
        Point = point.ToLinVector3D();
        ScalarDistance = scalarDistance;

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricVolumeLocalFrame3D(IGraphicsParametricVolume3D volume, double parameterValue1, double parameterValue2, double parameterValue3)
    {
        ParameterValue = LinFloat64Vector3D.Create(parameterValue1, parameterValue2, parameterValue3);
        Point = volume.GetPoint(parameterValue1, parameterValue2, parameterValue3);
        ScalarDistance = volume.GetScalarDistance(parameterValue1, parameterValue2, parameterValue3);

        Debug.Assert(IsValid());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricVolumeLocalFrame3D(IGraphicsParametricVolume3D volume, ILinFloat64Vector3D parameterValue)
    {
        ParameterValue = parameterValue.ToLinVector3D();
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