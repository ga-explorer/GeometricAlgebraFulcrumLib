using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Quaternions
{
    public class ComputedParametricQuaternion :
        IParametricQuaternion
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricQuaternion Create(Float64ScalarRange parameterRange, Func<double, Float64Quaternion> getPointFunc)
        {
            return new ComputedParametricQuaternion(
                parameterRange,
                getPointFunc
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricQuaternion Create(Func<double, Float64Quaternion> getPointFunc)
        {
            return new ComputedParametricQuaternion(
                Float64ScalarRange.Infinite,
                getPointFunc
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricQuaternion Create(Float64ScalarRange parameterRange, Func<double, Float64Quaternion> getPointFunc, Func<double, Float64Quaternion> getTangentFunc)
        {
            return new ComputedParametricQuaternion(
                parameterRange,
                getPointFunc, 
                getTangentFunc
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricQuaternion Create(Func<double, Float64Quaternion> getPointFunc, Func<double, Float64Quaternion> getTangentFunc)
        {
            return new ComputedParametricQuaternion(
                Float64ScalarRange.Infinite,
                getPointFunc, 
                getTangentFunc
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricQuaternion Create(DifferentialFunction xFunc, DifferentialFunction yFunc, DifferentialFunction zFunc, DifferentialFunction wFunc)
        {
            var xDtFunc = xFunc.GetDerivative1();
            var yDtFunc = yFunc.GetDerivative1();
            var zDtFunc = zFunc.GetDerivative1();
            var wDtFunc = wFunc.GetDerivative1();

            return new ComputedParametricQuaternion(
                Float64ScalarRange.Infinite,
                t => Float64Quaternion.Create(xFunc.GetValue(t),
                    yFunc.GetValue(t),
                    zFunc.GetValue(t),
                    wFunc.GetValue(t)
                ),
                t => Float64Quaternion.Create(xDtFunc.GetValue(t),
                    yDtFunc.GetValue(t),
                    zDtFunc.GetValue(t),
                    wDtFunc.GetValue(t)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricQuaternion Create(Func<double, double> xFunc, Func<double, double> yFunc, Func<double, double> zFunc, Func<double, double> wFunc)
        {
            return new ComputedParametricQuaternion(
                Float64ScalarRange.Infinite,
                t => Float64Quaternion.Create(xFunc(t),
                    yFunc(t),
                    zFunc(t),
                    wFunc(t)
                ),
                t => Float64Quaternion.Create(Differentiate.FirstDerivative(xFunc, t),
                    Differentiate.FirstDerivative(yFunc, t),
                    Differentiate.FirstDerivative(zFunc, t),
                    Differentiate.FirstDerivative(wFunc, t)
                )
            );
        }


        public Func<double, Float64Quaternion> GetPointFunc { get; }

        public Func<double, Float64Quaternion>? GetTangentFunc { get; }

        public Float64ScalarRange ParameterRange { get; }
            //=> Float64Range1D.Infinite;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ComputedParametricQuaternion(Float64ScalarRange parameterRange, Func<double, Float64Quaternion> getPointFunc)
        {
            ParameterRange = parameterRange;
            GetPointFunc = getPointFunc;
            GetTangentFunc = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ComputedParametricQuaternion(Float64ScalarRange parameterRange, Func<double, Float64Quaternion> getPointFunc, Func<double, Float64Quaternion> getTangentFunc)
        {
            ParameterRange = parameterRange;
            GetPointFunc = getPointFunc;
            GetTangentFunc = getTangentFunc;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Quaternion GetQuaternion(double parameterValue)
        {
            return GetPointFunc(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Quaternion GetDerivative1Quaternion(double parameterValue)
        {
            if (GetTangentFunc is not null)
                return GetTangentFunc(parameterValue);

            const double epsilon = 1e-7;

            var p1 = GetPointFunc(parameterValue - epsilon);
            var p2 = GetPointFunc(parameterValue + epsilon);

            return (p2 - p1) / (2 * epsilon);
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public ParametricCurveLocalFrame4D GetFrame(double parameterValue)
        //{
        //    return ParametricCurveLocalFrame4D.Create(
        //        parameterValue,
        //        GetPoint(parameterValue),
        //        GetDerivative1Point(parameterValue)
        //    );
        //}
    }
}