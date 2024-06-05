using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders.Space3D.Immutable;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.ParametricShapes.Volumes.Sampled;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.ParametricShapes.Volumes;

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