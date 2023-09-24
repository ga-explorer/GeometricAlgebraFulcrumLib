using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars
{
    public sealed record ParametricScalarLocalFrame :
        IParametricScalarLocalFrame1D
    {
        public static ParametricScalarLocalFrame Create(double parameterValue, double point, double tangent)
        {
            return new ParametricScalarLocalFrame(
                parameterValue,
                point,
                tangent
            );
        }


        /// <summary>
        /// The curve parameter value at the given curve point
        /// </summary>
        public double ParameterValue { get; }

        public int Index { get; internal set; } = -1;

        /// <summary>
        /// A point on the curve
        /// </summary>
        public double Point { get; }

        public Color Color { get; set; }

        /// <summary>
        /// The tangent unit vector to the curve at the given curve point
        /// </summary>
        public double Tangent { get; }


        private ParametricScalarLocalFrame(double parameterValue, double point, double tangent)
        {
            ParameterValue = parameterValue;
            Point = point;
            Tangent = tangent;

            Debug.Assert(IsValid());
        }


        public bool IsValid()
        {
            var isValid =
                !double.IsNaN(ParameterValue) &&
                Point.IsValid() &&
                Tangent.IsValid();

            return isValid;
        }


        public ParametricScalarLocalFrame TranslateBy(double translationVector)
        {
            Debug.Assert(translationVector.IsValid());

            return new ParametricScalarLocalFrame(
                ParameterValue,
                Point + translationVector,
                Tangent
            );
        }
    }
}