using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Maps.Space3D;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves
{
    public class GrMappedParametricCurve3D :
        IGraphicsC1ParametricCurve3D
    {
        public IGraphicsC1ParametricCurve3D BaseCurve { get; }

        public IAffineMap3D Map { get; }


        public GrMappedParametricCurve3D([NotNull] IGraphicsC1ParametricCurve3D baseCurve, [NotNull] IAffineMap3D map)
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
        public Tuple3D GetPoint(double parameterValue)
        {
            return Map.MapPoint(BaseCurve.GetPoint(parameterValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetTangent(double parameterValue)
        {
            return Map.MapPoint(BaseCurve.GetTangent(parameterValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetUnitTangent(double parameterValue)
        {
            return GetTangent(parameterValue).ToUnitVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            var frame = BaseCurve.GetFrame(parameterValue);

            return GrParametricCurveLocalFrame3D.CreateFrame(
                parameterValue,
                Map.MapPoint(frame.Point),
                Map.MapNormal(frame.Normal1),
                Map.MapNormal(frame.Normal2),
                Map.MapVector(frame.Tangent)
            );
        }
    }
}