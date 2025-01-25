namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

/// <summary>
/// There are seventeen different solid finite primitive shapes: blob, box,
/// cone, cylinder, height field, isosurface, Julia fractal, lathe, ovus,
/// parametric, prism, sphere, sphere_sweep, superellipsoid, surface of revolution,
/// text and torus. These have a well-defined inside and can be used in CSG: see
/// Constructive Solid Geometry. They are finite and respond to automatic bounding.
/// You may specify an interior for these objects.
/// http://www.povray.org/documentation/3.7.0/r3_4.html#r3_4_5_1
/// </summary>
public interface IGrPovRayFiniteSolidObject :
    IGrPovRaySolidObject
{

}