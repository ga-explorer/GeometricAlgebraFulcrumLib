using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Differential.Functions;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves
{
    public class ComputedParametricCurve3D :
        IParametricCurve3D
    {
        public static ComputedParametricCurve3D Create(DifferentialFunction xFunc, DifferentialFunction yFunc, DifferentialFunction zFunc)
        {
            var xDtFunc = xFunc.GetDerivative1();
            var yDtFunc = yFunc.GetDerivative1();
            var zDtFunc = zFunc.GetDerivative1();

            return new ComputedParametricCurve3D(
                t => new Float64Tuple3D(
                    xFunc.GetValue(t),
                    yFunc.GetValue(t),
                    zFunc.GetValue(t)
                ),
                t => new Float64Tuple3D(
                    xDtFunc.GetValue(t),
                    yDtFunc.GetValue(t),
                    zDtFunc.GetValue(t)
                )
            );
        }

        public static ComputedParametricCurve3D Create(Func<double, double> xFunc, Func<double, double> yFunc, Func<double, double> zFunc)
        {
            return new ComputedParametricCurve3D(
                t => new Float64Tuple3D(
                    xFunc(t),
                    yFunc(t),
                    zFunc(t)
                ),
                t => new Float64Tuple3D(
                    Differentiate.FirstDerivative(xFunc, t),
                    Differentiate.FirstDerivative(yFunc, t),
                    Differentiate.FirstDerivative(zFunc, t)
                )
            );
        }

        public Func<double, Float64Tuple3D> GetPointFunc { get; }

        public Func<double, Float64Tuple3D> GetTangentFunc { get; }


        public ComputedParametricCurve3D(Func<double, Float64Tuple3D> getPointFunc)
        {
            GetPointFunc = getPointFunc;
            GetTangentFunc = null;
        }

        public ComputedParametricCurve3D(Func<double, Float64Tuple3D> getPointFunc, Func<double, Float64Tuple3D> getTangentFunc)
        {
            GetPointFunc = getPointFunc;
            GetTangentFunc = getTangentFunc;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetPoint(double parameterValue)
        {
            return GetPointFunc(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetDerivative1Point(double parameterValue)
        {
            if (GetTangentFunc is not null)
                return GetTangentFunc(parameterValue);

            const double epsilon = 1e-7;

            var p1 = GetPointFunc(parameterValue - epsilon);
            var p2 = GetPointFunc(parameterValue + epsilon);

            return (p2 - p1) / (2 * epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return ParametricCurveLocalFrame3D.Create(
                parameterValue,
                GetPoint(parameterValue),
                GetDerivative1Point(parameterValue)
            );
        }
    }
}