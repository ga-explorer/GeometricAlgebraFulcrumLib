using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.ParametricShapes.Volumes.Sampled;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.ParametricShapes.Volumes;

public static class GrParametricVolumeFactory3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrParametricVolumeTree3D CreateSampledVolume3D(this IGraphicsParametricVolume3D surface, GrParametricVolumeTreeOptions3D options)
    {
        var surfaceTree = new GrParametricVolumeTree3D(
            surface,
            Float64BoundingBox3D.Create(0, 0, 0, 1, 1, 1)
        );

        return surfaceTree.GenerateTree(options);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrParametricVolumeTree3D CreateSampledVolume3D(this IGraphicsParametricVolume3D surface, Float64BoundingBox3D parameterValueRange, GrParametricVolumeTreeOptions3D options)
    {
        var surfaceTree = new GrParametricVolumeTree3D(
            surface,
            parameterValueRange
        );

        return surfaceTree.GenerateTree(options);
    }

}