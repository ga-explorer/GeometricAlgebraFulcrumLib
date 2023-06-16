using System.Diagnostics;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space1D.Frames
{
    public sealed record ParametricCurveLocalFrame1D :
        IParametricCurveLocalFrame1D
    {
        public static ParametricCurveLocalFrame1D Create(double parameterValue, double point, double tangent)
        {
            return new ParametricCurveLocalFrame1D(
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
        

        private ParametricCurveLocalFrame1D(double parameterValue, double point, double tangent)
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
        

        public ParametricCurveLocalFrame1D TranslateBy(double translationVector)
        {
            Debug.Assert(translationVector.IsValid());

            return new ParametricCurveLocalFrame1D(
                ParameterValue,
                Point + translationVector,
                Tangent
            );
        }
    }
}