using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.BasicShapes.Lines.Immutable;
using GraphicsComposerLib.Geometry.ParametricShapes.Volumes;

namespace GraphicsComposerLib.Geometry.SdfShapes
{
    /// <summary>
    /// This interface represents 3D geometry defined using a
    /// Scalar Distance Function (SDF)
    /// </summary>
    public interface ISdfGeometry3D :
        IGraphicsParametricVolume3D
    {
        double SdfDistanceDelta { get; }

        double SdfDistanceDeltaInv { get; }

        double SdfAlpha { get; }

        double SdfDelta { get; }

        double ComputeSdfRayStep(Line3D ray, double t0);

        Tuple3D ComputeSdfNormal(ITuple3D point);
    }
}
