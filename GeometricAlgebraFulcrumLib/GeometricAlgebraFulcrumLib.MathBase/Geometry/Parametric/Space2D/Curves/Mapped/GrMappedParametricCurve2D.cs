using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.Mapped
{
    public class GrMappedParametricCurve2D :
        IParametricCurve2D
    {
        public IParametricCurve2D BaseCurve { get; }

        public IAffineMap2D Map { get; }
        
        public Float64Range1D ParameterRange
            => BaseCurve.ParameterRange;


        public GrMappedParametricCurve2D(IParametricCurve2D baseCurve, IAffineMap2D map)
        {
            BaseCurve = baseCurve;
            Map = map;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return BaseCurve.IsValid() &&
                   Map.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D GetPoint(double parameterValue)
        {
            return Map.MapPoint(BaseCurve.GetPoint(parameterValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D GetDerivative1Point(double parameterValue)
        {
            return Map.MapPoint(BaseCurve.GetDerivative1Point(parameterValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
        {
            var frame = BaseCurve.GetFrame(parameterValue);

            return ParametricCurveLocalFrame2D.Create(
                parameterValue,
                Map.MapPoint(frame.Point),
                Map.MapVector(frame.Tangent)
            );
        }
    }
}