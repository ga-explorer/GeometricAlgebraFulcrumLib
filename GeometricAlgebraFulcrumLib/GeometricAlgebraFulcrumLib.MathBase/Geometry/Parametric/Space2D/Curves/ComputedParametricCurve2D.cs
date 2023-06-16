using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves
{
    public class ComputedParametricCurve2D :
        IParametricCurve2D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D Create(Func<double, Float64Vector2D> getPointFunc)
        {
            return new ComputedParametricCurve2D(getPointFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D Create(Func<double, Float64Vector2D> getPointFunc, Func<double, Float64Vector2D> getTangentFunc)
        {
            return new ComputedParametricCurve2D(getPointFunc, getTangentFunc);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D Create(DifferentialFunction xFunc, DifferentialFunction yFunc)
        {
            var xDtFunc = xFunc.GetDerivative1();
            var yDtFunc = yFunc.GetDerivative1();
            
            return Create(t => new Float64Vector2D(
                    xFunc.GetValue(t),
                    yFunc.GetValue(t)
                ),
                t => new Float64Vector2D(
                    xDtFunc.GetValue(t),
                    yDtFunc.GetValue(t)
                ));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve2D Create(Func<double, double> xFunc, Func<double, double> yFunc)
        {
            return Create(t => new Float64Vector2D(
                    xFunc(t),
                    yFunc(t)
                ),
                t => new Float64Vector2D(
                    Differentiate.FirstDerivative(xFunc, t),
                    Differentiate.FirstDerivative(yFunc, t)
                ));
        }


        public Float64Range1D ParameterRange 
            => Float64Range1D.Infinite;
        
        public Func<double, Float64Vector2D> GetPointFunc { get; }

        public Func<double, Float64Vector2D>? GetTangentFunc { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ComputedParametricCurve2D(Func<double, Float64Vector2D> getPointFunc)
        {
            GetPointFunc = getPointFunc;
            GetTangentFunc = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ComputedParametricCurve2D(Func<double, Float64Vector2D> getPointFunc, Func<double, Float64Vector2D> getTangentFunc)
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
        public Float64Vector2D GetPoint(double parameterValue)
        {
            return GetPointFunc(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D GetDerivative1Point(double parameterValue)
        {
            if (GetTangentFunc is not null)
                return GetTangentFunc(parameterValue);

            const double epsilon = 1e-7;

            var p1 = GetPointFunc(parameterValue - epsilon);
            var p2 = GetPointFunc(parameterValue + epsilon);

            return (p2 - p1) / (2 * epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
        {
            return ParametricCurveLocalFrame2D.Create(
                parameterValue,
                GetPoint(parameterValue),
                GetDerivative1Point(parameterValue)
            );
        }
    }
}