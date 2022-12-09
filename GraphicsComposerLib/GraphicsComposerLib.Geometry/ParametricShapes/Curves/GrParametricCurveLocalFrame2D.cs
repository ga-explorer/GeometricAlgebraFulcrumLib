using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Primitives;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves
{
    public sealed record GrParametricCurveLocalFrame2D :
        IGraphicsCurveLocalFrame2D
    {
        public static GrParametricCurveLocalFrame2D CreateFrame(double parameterValue, IFloat64Tuple2D point, IFloat64Tuple2D tangent)
        {
            return new GrParametricCurveLocalFrame2D(
                parameterValue,
                point.ToTuple2D(),
                tangent.ToTuple2D()
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
        public Float64Tuple2D Point { get; }

        public double Item1 
            => Point.X;
        
        public double Item2 
            => Point.Y;
        
        public double X 
            => Point.X;
        
        public double Y 
            => Point.Y;
        
        public Color Color { get; set; }
        
        /// <summary>
        /// The tangent unit vector to the curve at the given curve point
        /// </summary>
        public Float64Tuple2D Tangent { get; }

        public bool IsValid()
        {
            var isValid = 
                !double.IsNaN(ParameterValue) &&
                Point.IsValid() &&
                Tangent.IsValid() &&
                Tangent.GetVectorNormSquared().IsNearEqual(1);

            return isValid;
        }
        

        private GrParametricCurveLocalFrame2D(double parameterValue, [NotNull] IFloat64Tuple2D point, [NotNull] IFloat64Tuple2D tangent)
        {
            ParameterValue = parameterValue;
            Point = point.ToTuple2D();
            Tangent = tangent.ToTuple2D();

            Debug.Assert(IsValid());
        }

        
        public double GetMaxDirectionAngleWithFrame(GrParametricCurveLocalFrame2D frame2)
        {
            var maxAngle = 0d;

            var angle = Tangent.GetVectorsAngle(frame2.Tangent);
            if (maxAngle < angle) maxAngle = angle;

            return maxAngle;
        }


        public GrParametricCurveLocalFrame2D TranslateBy(IFloat64Tuple2D translationVector)
        {
            Debug.Assert(translationVector.IsValid());

            return new GrParametricCurveLocalFrame2D(
                ParameterValue,
                new Float64Tuple2D(
                    Point.X + translationVector.X,
                    Point.Y + translationVector.Y
                ),
                Tangent
            );
        }
    }
}