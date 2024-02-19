using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Graphics.ParametricShapes.Volumes;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.SdfGeometry;

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

    Float64Vector3D ComputeSdfNormal(IFloat64Vector3D point);
}