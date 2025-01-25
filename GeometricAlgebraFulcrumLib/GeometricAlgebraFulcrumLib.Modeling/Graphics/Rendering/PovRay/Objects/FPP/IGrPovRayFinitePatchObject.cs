namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FPP;

/// <summary>
/// There are six totally thin, finite objects which have no well-defined inside.
/// They are bicubic patch, disc, smooth triangle, triangle, polygon, mesh, and mesh2.
/// They may be combined in CSG union, but cannot be used inside a clipped_by statement.
/// Patch objects may give unexpected results when used in differences and intersections.
/// These conditions apply:
/// - Solids may be differenced from bicubic patches with the expected results.
/// - Differencing a bicubic patch from a solid may give unexpected results.
///   Especially if the inverse keyword is used!
/// - Intersecting a solid and a bicubic patch will give the expected results.
///   The parts of the patch that intersect the solid object will be visible.
/// - Merging a solid and a bicubic patch will remove the parts of the bicubic
///   patch that intersect the solid.
/// Because these types are finite POV-Ray can use automatic bounding on them to
/// speed up rendering time. As with all shapes they can be translated, rotated and scaled.
/// http://www.povray.org/documentation/3.7.0/r3_4.html#r3_4_5_2
/// </summary>
public interface IGrPovRayFinitePatchObject :
    IGrPovRayObject
{

}