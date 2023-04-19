using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Volumes
{
    public class GrComputedParametricVolume3D :
        IGraphicsParametricVolume3D
    {
        public Func<double, double, double, Float64Tuple3D> GetPointFunc { get; }

        public Func<double, double, double, double> GetScalarDistanceFunc { get; }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrComputedParametricVolume3D([NotNull] Func<double, double, double, double> getScalarDistanceFunc)
        {
            GetPointFunc = null;
            GetScalarDistanceFunc = getScalarDistanceFunc;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrComputedParametricVolume3D([NotNull] Func<double, double, double, Float64Tuple3D> getPointFunc, [NotNull] Func<double, double, double, double> getScalarDistanceFunc)
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
        public Float64Tuple3D GetPoint(IFloat64Tuple3D parameterValue)
        {
            return GetPointFunc is null
                ? parameterValue.ToTuple3D()
                : GetPointFunc(parameterValue.Item1, parameterValue.Item2, parameterValue.Item3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetPoint(double parameterValue1, double parameterValue2, double parameterValue3)
        {
            return GetPointFunc is null
                ? new Float64Tuple3D(parameterValue1, parameterValue2, parameterValue3)
                : GetPointFunc(parameterValue1, parameterValue2, parameterValue3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarDistance(IFloat64Tuple3D parameterValue)
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
        public GrParametricVolumeLocalFrame3D GetFrame(IFloat64Tuple3D parameterValue)
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