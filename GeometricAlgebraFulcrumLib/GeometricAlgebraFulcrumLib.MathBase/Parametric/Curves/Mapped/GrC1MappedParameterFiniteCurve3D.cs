using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Mapped
{
    public class GrC1MappedParameterFiniteCurve3D :
        IParametricFiniteC1Curve3D
    {
        public IParametricFiniteC1Curve3D BaseCurve { get; }

        /// <summary>
        /// This function takes a number in the range [0, 1] and returns a number
        /// in the range [BaseCurve.ParameterValueMin, BaseCurve.ParameterValueMax]
        /// </summary>
        public Func<double, double> ParameterMapping { get; }

        public double ParameterValueMin
            => 0d;

        public double ParameterValueMax
            => 1d;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrC1MappedParameterFiniteCurve3D(IParametricFiniteC1Curve3D baseCurve)
        {
            BaseCurve = baseCurve;
            ParameterMapping = t => t.CosWave(
                baseCurve.ParameterValueMin,
                baseCurve.ParameterValueMax,
                1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrC1MappedParameterFiniteCurve3D(IParametricFiniteC1Curve3D baseCurve, Func<double, double> parameterMapping)
        {
            BaseCurve = baseCurve;
            ParameterMapping = parameterMapping;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return BaseCurve.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetPoint(double parameterValue)
        {
            return BaseCurve.GetPoint(
                ParameterMapping(parameterValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetDerivative1Point(double parameterValue)
        {
            return BaseCurve.GetDerivative1Point(
                ParameterMapping(parameterValue)
            );
        }

        public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return BaseCurve.GetFrame(
                ParameterMapping(parameterValue)
            );
        }
    }
}