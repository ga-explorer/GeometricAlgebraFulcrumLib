using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Bezier
{
    public sealed class BezierCurveNDegree3D :
        IParametricCurve3D
    {
        public bool IsValid() => ControlPoints.All(p => p.IsValid());

        public List<Float64Tuple3D> ControlPoints { get; }
            = new List<Float64Tuple3D>();

        public int Degree
            => ControlPoints.Count - 1;


        public BezierCurveNDegree3D GetDerivativeCurve()
        {
            var result = new BezierCurveNDegree3D();

            for (var n = 0; n < Degree; n++)
                result.ControlPoints.Add(Degree * (ControlPoints[n + 1] - ControlPoints[n]));

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetPoint(double t)
        {
            return t.DeCasteljau(ControlPoints.ToArray());
        }

        public Float64Tuple3D GetDerivative1Point(double t)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return ParametricCurveLocalFrame3D.Create(
                parameterValue,
                GetPoint(parameterValue),
                GetDerivative1Point(parameterValue)
            );
        }
    }
}
