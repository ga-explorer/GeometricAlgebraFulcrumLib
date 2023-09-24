using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.ParametricShapes.Volumes
{
    public class GrComputedParametricVolume3D :
        IGraphicsParametricVolume3D
    {
        public Func<double, double, double, Float64Vector3D> GetPointFunc { get; }

        public Func<double, double, double, double> GetScalarDistanceFunc { get; }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrComputedParametricVolume3D(Func<double, double, double, double> getScalarDistanceFunc)
        {
            GetPointFunc = null;
            GetScalarDistanceFunc = getScalarDistanceFunc;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrComputedParametricVolume3D(Func<double, double, double, Float64Vector3D> getPointFunc, Func<double, double, double, double> getScalarDistanceFunc)
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
        public Float64Vector3D GetPoint(IFloat64Vector3D parameterValue)
        {
            return GetPointFunc is null
                ? parameterValue.ToVector3D()
                : GetPointFunc(parameterValue.Item1, parameterValue.Item2, parameterValue.Item3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetPoint(double parameterValue1, double parameterValue2, double parameterValue3)
        {
            return GetPointFunc is null
                ? Float64Vector3D.Create(parameterValue1, parameterValue2, parameterValue3)
                : GetPointFunc(parameterValue1, parameterValue2, parameterValue3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarDistance(IFloat64Vector3D parameterValue)
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
        public GrParametricVolumeLocalFrame3D GetFrame(IFloat64Vector3D parameterValue)
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
}