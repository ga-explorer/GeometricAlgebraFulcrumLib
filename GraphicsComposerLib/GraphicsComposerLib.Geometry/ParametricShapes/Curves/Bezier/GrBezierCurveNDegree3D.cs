using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Bezier
{
    public sealed class GrBezierCurveNDegree3D :
        IGraphicsParametricCurve3D
    {
        public bool IsValid() => ControlPoints.All(p => p.IsValid());

        public List<Tuple3D> ControlPoints { get; } 
            = new List<Tuple3D>();

        public int Degree 
            => ControlPoints.Count - 1;

        
        public GrBezierCurveNDegree3D GetDerivativeCurve()
        {
            var result = new GrBezierCurveNDegree3D();

            for (var n = 0; n < Degree; n++)
                result.ControlPoints.Add(Degree * (ControlPoints[n + 1] - ControlPoints[n]));

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetPoint(double t)
        {
            return t.DeCasteljau(ControlPoints.ToArray());
        }

        public Tuple3D GetTangent(double t)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetUnitTangent(double t)
        {
            return GetTangent(t).ToUnitVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return GrParametricCurveLocalFrame3D.CreateFrame(
                parameterValue,
                GetPoint(parameterValue),
                GetTangent(parameterValue)
            );
        }
    }
}
