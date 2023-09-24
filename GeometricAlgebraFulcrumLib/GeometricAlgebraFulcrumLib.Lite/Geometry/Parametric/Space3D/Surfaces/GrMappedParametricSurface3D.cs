using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.Maps.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Surfaces
{
    public class GrMappedParametricSurface3D :
        IGraphicsParametricSurface3D
    {
        public IGraphicsParametricSurface3D BaseSurface { get; }

        public IAffineMap3D Map { get; }


        public GrMappedParametricSurface3D(IGraphicsParametricSurface3D baseSurface, IAffineMap3D map)
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
        public Float64Vector3D GetPoint(double parameterValue1, double parameterValue2)
        {
            return Map.MapPoint(BaseSurface.GetPoint(parameterValue1, parameterValue2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetNormal(double parameterValue1, double parameterValue2)
        {
            return Map.MapPoint(BaseSurface.GetNormal(parameterValue1, parameterValue2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetUnitNormal(double parameterValue1, double parameterValue2)
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