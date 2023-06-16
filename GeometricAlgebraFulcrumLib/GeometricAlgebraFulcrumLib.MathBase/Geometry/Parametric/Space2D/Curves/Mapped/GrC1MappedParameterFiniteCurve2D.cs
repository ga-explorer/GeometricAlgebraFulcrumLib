using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.Mapped
{
    public class GrC1MappedParameterFiniteCurve2D :
        IParametricCurve2D
    {
        public IParametricCurve2D BaseCurve { get; }

        /// <summary>
        /// This function takes a number in the range [0, 1] and returns a number
        /// in the range [BaseCurve.ParameterValueMin, BaseCurve.ParameterValueMax]
        /// </summary>
        public Func<double, double> ParameterMapping { get; }

        public Float64Range1D ParameterRange
            => Float64Range1D.ZeroToOne;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrC1MappedParameterFiniteCurve2D(IParametricCurve2D baseCurve)
        {
            BaseCurve = baseCurve;
            ParameterMapping = t => t.CosWave(
                baseCurve.ParameterRange,
                1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrC1MappedParameterFiniteCurve2D(IParametricCurve2D baseCurve, Func<double, double> parameterMapping)
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
        public Float64Vector2D GetPoint(double parameterValue)
        {
            return BaseCurve.GetPoint(
                ParameterMapping(parameterValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D GetDerivative1Point(double parameterValue)
        {
            return BaseCurve.GetDerivative1Point(
                ParameterMapping(parameterValue)
            );
        }

        public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
        {
            return BaseCurve.GetFrame(
                ParameterMapping(parameterValue)
            );
        }
    }
}