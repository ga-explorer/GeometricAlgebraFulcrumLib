using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Maps.Space3D;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Volumes
{
    public class GrMappedParametricVolume3D :
        IGraphicsParametricVolume3D
    {
        public IGraphicsParametricVolume3D BaseVolume { get; }

        public IAffineMap3D Map { get; }


        public GrMappedParametricVolume3D([NotNull] IGraphicsParametricVolume3D baseVolume, [NotNull] IAffineMap3D map)
        {
            BaseVolume = baseVolume;
            Map = map;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return BaseVolume.IsValid() &&
                   Map.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetPoint(ITuple3D parameterValue)
        {
            return Map.MapPoint(
                BaseVolume.GetPoint(parameterValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetPoint(double parameterValue1, double parameterValue2, double parameterValue3)
        {
            return Map.MapPoint(
                BaseVolume.GetPoint(parameterValue1, parameterValue2, parameterValue3)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarDistance(ITuple3D parameterValue)
        {
            return BaseVolume.GetScalarDistance(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarDistance(double parameterValue1, double parameterValue2, double parameterValue3)
        {
            return BaseVolume.GetScalarDistance(parameterValue1, parameterValue2, parameterValue3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricVolumeLocalFrame3D GetFrame(ITuple3D parameterValue)
        {
            var frame = BaseVolume.GetFrame(parameterValue);

            return new GrParametricVolumeLocalFrame3D(
                parameterValue,
                Map.MapPoint(frame.Point),
                frame.ScalarDistance
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricVolumeLocalFrame3D GetFrame(double parameterValue1, double parameterValue2, double parameterValue3)
        {
            var frame = BaseVolume.GetFrame(parameterValue1, parameterValue2, parameterValue3);

            return new GrParametricVolumeLocalFrame3D(
                parameterValue1, 
                parameterValue2,
                parameterValue3,
                Map.MapPoint(frame.Point),
                frame.ScalarDistance
            );
        }
    }
}