using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves
{
    public class ComputedParametricCurve3D :
        IParametricCurve3D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve3D Create(Func<double, Float64Vector3D> getPointFunc)
        {
            return new ComputedParametricCurve3D(getPointFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve3D Create(Func<double, Float64Vector3D> getPointFunc, Func<double, Float64Vector3D> getTangentFunc)
        {
            return new ComputedParametricCurve3D(getPointFunc, getTangentFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve3D Create(DifferentialFunction xFunc, DifferentialFunction yFunc, DifferentialFunction zFunc)
        {
            var xDtFunc = xFunc.GetDerivative1();
            var yDtFunc = yFunc.GetDerivative1();
            var zDtFunc = zFunc.GetDerivative1();

            return Create(t => Float64Vector3D.Create(xFunc.GetValue(t),
                    yFunc.GetValue(t),
                    zFunc.GetValue(t)),
                t => Float64Vector3D.Create(xDtFunc.GetValue(t),
                    yDtFunc.GetValue(t),
                    zDtFunc.GetValue(t)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve3D Create(Func<double, double> xFunc, Func<double, double> yFunc, Func<double, double> zFunc)
        {
            return Create(t => Float64Vector3D.Create(xFunc(t),
                    yFunc(t),
                    zFunc(t)),
                t => Float64Vector3D.Create(Differentiate.FirstDerivative(xFunc, t),
                    Differentiate.FirstDerivative(yFunc, t),
                    Differentiate.FirstDerivative(zFunc, t)));
        }


        public Func<double, Float64Vector3D> GetPointFunc { get; }

        public Func<double, Float64Vector3D>? GetTangentFunc { get; }
        
        public Float64Range1D ParameterRange 
            => Float64Range1D.Infinite;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ComputedParametricCurve3D(Func<double, Float64Vector3D> getPointFunc)
        {
            GetPointFunc = getPointFunc;
            GetTangentFunc = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ComputedParametricCurve3D(Func<double, Float64Vector3D> getPointFunc, Func<double, Float64Vector3D> getTangentFunc)
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
        public Float64Vector3D GetPoint(double parameterValue)
        {
            return GetPointFunc(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetDerivative1Point(double parameterValue)
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