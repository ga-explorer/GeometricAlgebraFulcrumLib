using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space3D.Immutable;
using GraphicsComposerLib.Geometry.ParametricShapes.Volumes.Sampled;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Volumes
{
    public static class GrParametricVolumeFactory3D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrParametricVolumeTree3D CreateSampledVolume3D(this IGraphicsParametricVolume3D surface, GrParametricVolumeTreeOptions3D options)
        {
            var surfaceTree = new GrParametricVolumeTree3D(
                surface,
                BoundingBox3D.Create(0, 0, 0, 1, 1, 1)
            );

            return surfaceTree.GenerateTree(options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GrParametricVolumeTree3D CreateSampledVolume3D(this IGraphicsParametricVolume3D surface, BoundingBox3D parameterValueRange, GrParametricVolumeTreeOptions3D options)
        {
            var surfaceTree = new GrParametricVolumeTree3D(
                surface,
                parameterValueRange
            );

            return surfaceTree.GenerateTree(options);
        }

    }
}