using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Mapped
{
    public class GrC1MappedParameterFiniteCurve3D :
        IParametricCurve3D
    {
        public IParametricCurve3D BaseCurve { get; }

        /// <summary>
        /// This function takes a number in the range [0, 1] and returns a number
        /// in the range [BaseCurve.ParameterValueMin, BaseCurve.ParameterValueMax]
        /// </summary>
        public Func<double, double> ParameterMapping { get; }

        public Float64ScalarRange ParameterRange
            => Float64ScalarRange.ZeroToOne;
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrC1MappedParameterFiniteCurve3D(IParametricCurve3D baseCurve)
        {
            BaseCurve = baseCurve;
            ParameterMapping = t => t.CosWave(
                baseCurve.ParameterRange,
                1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrC1MappedParameterFiniteCurve3D(IParametricCurve3D baseCurve, Func<double, double> parameterMapping)
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
        public Float64Vector3D GetPoint(double parameterValue)
        {
            return BaseCurve.GetPoint(
                ParameterMapping(parameterValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetDerivative1Point(double parameterValue)
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