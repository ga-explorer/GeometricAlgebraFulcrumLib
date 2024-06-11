using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.ParametricShapes.Volumes;

public class GrComputedParametricVolume3D :
    IGraphicsParametricVolume3D
{
    public Func<double, double, double, LinFloat64Vector3D> GetPointFunc { get; }

    public Func<double, double, double, double> GetScalarDistanceFunc { get; }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrComputedParametricVolume3D(Func<double, double, double, double> getScalarDistanceFunc)
    {
        GetPointFunc = null;
        GetScalarDistanceFunc = getScalarDistanceFunc;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrComputedParametricVolume3D(Func<double, double, double, LinFloat64Vector3D> getPointFunc, Func<double, double, double, double> getScalarDistanceFunc)
    {
        GetPointFunc = getPointFunc;
        GetScalarDistanceFunc = getScalarDistanceFunc;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetPoint(ILinFloat64Vector3D parameterValue)
    {
        return GetPointFunc is null
            ? parameterValue.ToLinVector3D()
            : GetPointFunc(parameterValue.Item1, parameterValue.Item2, parameterValue.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetPoint(double parameterValue1, double parameterValue2, double parameterValue3)
    {
        return GetPointFunc is null
            ? LinFloat64Vector3D.Create(parameterValue1, parameterValue2, parameterValue3)
            : GetPointFunc(parameterValue1, parameterValue2, parameterValue3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetScalarDistance(ILinFloat64Vector3D parameterValue)
    {
        return GetScalarDistanceFunc(
            parameterValue.Item1, 
            parameterValue.Item2, 
            parameterValue.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetScalarDistance(double parameterValue1, double parameterValue2, double parameterValue3)
    {
        return GetScalarDistanceFunc(
            parameterValue1, 
            parameterValue2, 
            parameterValue3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricVolumeLocalFrame3D GetFrame(ILinFloat64Vector3D parameterValue)
    {
        return new GrParametricVolumeLocalFrame3D(
            this, 
            parameterValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricVolumeLocalFrame3D GetFrame(double parameterValue1, double parameterValue2, double parameterValue3)
    {
        return new GrParametricVolumeLocalFrame3D(
            this, 
            parameterValue1, 
            parameterValue2, 
            parameterValue3
        );
    }
}