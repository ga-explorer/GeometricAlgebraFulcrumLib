using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.ParametricShapes.Volumes;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.SdfGeometry;

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