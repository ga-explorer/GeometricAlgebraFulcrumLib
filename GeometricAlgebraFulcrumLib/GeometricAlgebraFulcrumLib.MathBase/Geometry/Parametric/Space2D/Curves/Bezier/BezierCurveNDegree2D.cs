using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.Bezier
{
    public sealed class BezierCurveNDegree2D :
        IParametricCurve2D
    {
        public bool IsValid() => ControlPoints.All(p => p.IsValid());

        public List<Float64Vector2D> ControlPoints { get; }
            = new List<Float64Vector2D>();

        public int Degree
            => ControlPoints.Count - 1;
        
        public Float64Range1D ParameterRange 
            => Float64Range1D.Infinite;


        public BezierCurveNDegree2D GetDerivativeCurve()
        {
            var result = new BezierCurveNDegree2D();

            for (var n = 0; n < Degree; n++)
                result.ControlPoints.Add(Degree * (ControlPoints[n + 1] - ControlPoints[n]));

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D GetPoint(double t)
        {
            return t.DeCasteljau(ControlPoints.ToArray());
        }

        public Float64Vector2D GetDerivative1Point(double t)
        {
            throw new NotImplementedException();
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
