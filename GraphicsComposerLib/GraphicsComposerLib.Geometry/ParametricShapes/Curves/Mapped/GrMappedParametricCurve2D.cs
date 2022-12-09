using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Maps.Space2D;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Mapped
{
    public class GrMappedParametricCurve2D :
        IGraphicsParametricCurve2D
    {
        public IGraphicsParametricCurve2D BaseCurve { get; }

        public IAffineMap2D Map { get; }


        public GrMappedParametricCurve2D([NotNull] IGraphicsParametricCurve2D baseCurve, [NotNull] IAffineMap2D map)
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
        public Float64Tuple2D GetPoint(double parameterValue)
        {
            return Map.MapPoint(BaseCurve.GetPoint(parameterValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple2D GetTangent(double parameterValue)
        {
            return Map.MapPoint(BaseCurve.GetTangent(parameterValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple2D GetUnitTangent(double parameterValue)
        {
            return GetTangent(parameterValue).ToUnitVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveLocalFrame2D GetFrame(double parameterValue)
        {
            var frame = BaseCurve.GetFrame(parameterValue);

            return GrParametricCurveLocalFrame2D.CreateFrame(
                parameterValue,
                Map.MapPoint(frame.Point),
                Map.MapVector(frame.Tangent)
            );
        }
    }
}