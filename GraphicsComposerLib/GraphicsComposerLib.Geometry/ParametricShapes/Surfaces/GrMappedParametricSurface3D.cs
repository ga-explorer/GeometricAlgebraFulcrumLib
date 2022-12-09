using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Maps.Space3D;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Surfaces
{
    public class GrMappedParametricSurface3D :
        IGraphicsParametricSurface3D
    {
        public IGraphicsParametricSurface3D BaseSurface { get; }

        public IAffineMap3D Map { get; }


        public GrMappedParametricSurface3D([NotNull] IGraphicsParametricSurface3D baseSurface, [NotNull] IAffineMap3D map)
        {
            BaseSurface = baseSurface;
            Map = map;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return BaseSurface.IsValid() &&
                   Map.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetPoint(double parameterValue1, double parameterValue2)
        {
            return Map.MapPoint(BaseSurface.GetPoint(parameterValue1, parameterValue2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetNormal(double parameterValue1, double parameterValue2)
        {
            return Map.MapPoint(BaseSurface.GetNormal(parameterValue1, parameterValue2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetUnitNormal(double parameterValue1, double parameterValue2)
        {
            return GetNormal(parameterValue1, parameterValue2).ToUnitVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricSurfaceLocalFrame3D GetFrame(double parameterValue1, double parameterValue2)
        {
            var frame = BaseSurface.GetFrame(parameterValue1, parameterValue2);

            return new GrParametricSurfaceLocalFrame3D(
                parameterValue1, 
                parameterValue2,
                Map.MapPoint(frame.Point),
                Map.MapNormal(frame.Normal)
            );
        }
    }
}