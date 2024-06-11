using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.ParametricShapes.Volumes;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.SdfGeometry;

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

    LinFloat64Vector3D ComputeSdfNormal(ILinFloat64Vector3D point);
}