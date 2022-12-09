using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Maps.Space3D;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Mapped
{
    public class GrMappedParametricCurve3D :
        IGraphicsC1ParametricCurve3D
    {
        public IGraphicsC1ParametricCurve3D BaseCurve { get; }

        public IAffineMap3D AffineMap { get; }


        public GrMappedParametricCurve3D([NotNull] IGraphicsC1ParametricCurve3D baseCurve, [NotNull] IAffineMap3D map)
        {
            BaseCurve = baseCurve;
            AffineMap = map;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return BaseCurve.IsValid() &&
                   AffineMap.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetPoint(double parameterValue)
        {
            return AffineMap.MapPoint(BaseCurve.GetPoint(parameterValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetTangent(double parameterValue)
        {
            return AffineMap.MapPoint(BaseCurve.GetTangent(parameterValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetUnitTangent(double parameterValue)
        {
            return GetTangent(parameterValue).ToUnitVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            var frame = BaseCurve.GetFrame(parameterValue);

            return GrParametricCurveLocalFrame3D.Create(
                parameterValue,
                AffineMap.MapPoint(frame.Point),
                AffineMap.MapNormal(frame.Normal1),
                AffineMap.MapNormal(frame.Normal2),
                AffineMap.MapVector(frame.Tangent)
            );
        }
    }
}