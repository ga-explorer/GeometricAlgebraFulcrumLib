using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves
{
    public sealed record ParametricCurveLocalFrame2D :
        IParametricCurveLocalFrame2D
    {
        public static ParametricCurveLocalFrame2D Create(double parameterValue, IFloat64Vector2D point, IFloat64Vector2D tangent)
        {
            return new ParametricCurveLocalFrame2D(
                parameterValue,
                point.ToVector2D(),
                tangent.ToVector2D()
            );
        }


        /// <summary>
        /// The curve parameter value at the given curve point
        /// </summary>
        public Float64Scalar ParameterValue { get; }

        public int Index { get; internal set; } = -1;

        /// <summary>
        /// A point on the curve
        /// </summary>
        public Float64Vector2D Point { get; }

        public int VSpaceDimensions
            => 2;

        public double Item1
            => Point.X.Value;

        public double Item2
            => Point.Y.Value;

        public Float64Scalar X
            => Point.X;

        public Float64Scalar Y
            => Point.Y;

        public Color Color { get; set; }

        /// <summary>
        /// The tangent unit vector to the curve at the given curve point
        /// </summary>
        public Float64Vector2D Tangent { get; }

        public Float64Vector2D Normal { get; }


        private ParametricCurveLocalFrame2D(double parameterValue, IFloat64Vector2D point, IFloat64Vector2D tangent)
        {
            ParameterValue = parameterValue;
            Point = point.ToVector2D();
            Tangent = tangent.ToVector2D();
            Normal = Tangent.GetNormal();

            Debug.Assert(IsValid());
        }


        public bool IsValid()
        {
            var isValid =
                !double.IsNaN(ParameterValue) &&
                Point.IsValid() &&
                Tangent.IsValid() &&
                Tangent.ENormSquared().IsNearEqual(1);

            return isValid;
        }

        public double GetMaxDirectionAngleWithFrame(ParametricCurveLocalFrame2D frame2)
        {
            var maxAngle = 0d;

            var angle = Tangent.GetVectorsAngle(frame2.Tangent);
            if (maxAngle < angle) maxAngle = angle;

            return maxAngle;
        }


        public ParametricCurveLocalFrame2D TranslateBy(IFloat64Vector2D translationVector)
        {
            Debug.Assert(translationVector.IsValid());

            return new ParametricCurveLocalFrame2D(
                ParameterValue,
                Float64Vector2D.Create(Point.X + translationVector.X,
                    Point.Y + translationVector.Y),
                Tangent
            );
        }
    }
}