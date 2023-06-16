using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space1D.Frames;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space1D.Curves
{
    public class ComputedParametricCurve1D :
        IParametricCurve1D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve1D Create(Func<double, double> xFunc)
        {
            return Create(
                xFunc, 
                Differentiate.FirstDerivativeFunc(xFunc)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve1D Create(Func<double, double> getPointFunc, Func<double, double> getTangentFunc)
        {
            return new ComputedParametricCurve1D(
                getPointFunc, 
                getTangentFunc
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricCurve1D Create(DifferentialFunction xFunc)
        {
            var xDtFunc = xFunc.GetDerivative1();

            return Create(
                xFunc.GetValue, 
                xDtFunc.GetValue
            );
        }


        public Func<double, double> GetPointFunc { get; }

        public Func<double, double>? GetTangentFunc { get; }
        
        public Float64Range1D ParameterRange 
            => Float64Range1D.Infinite;

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ComputedParametricCurve1D(Func<double, double> getPointFunc, Func<double, double> getTangentFunc)
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
        public double GetPoint(double parameterValue)
        {
            return GetPointFunc(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetDerivative1Point(double parameterValue)
        {
            if (GetTangentFunc is not null)
                return GetTangentFunc(parameterValue);

            const double epsilon = 1e-7;

            var p1 = GetPointFunc(parameterValue - epsilon);
            var p2 = GetPointFunc(parameterValue + epsilon);

            return (p2 - p1) / (2 * epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricCurveLocalFrame1D GetFrame(double parameterValue)
        {
            return ParametricCurveLocalFrame1D.Create(
                parameterValue,
                GetPoint(parameterValue),
                GetDerivative1Point(parameterValue)
            );
        }
    }
}