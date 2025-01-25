namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.CSG;

/// <summary>
/// In addition to all the primitive shapes POV-Ray supports,
/// you can also combine multiple simple shapes into complex shapes
/// using Constructive Solid Geometry (CSG). There are four basic types
/// of CSG operations: union, intersection, difference, and merge.
/// CSG objects can be composed of primitives or other CSG objects to
/// create more, and more complex shapes.
/// http://www.povray.org/documentation/3.7.0/r3_4.html#r3_4_5_4
/// </summary>
public interface IGrPovRayCsgObject :
    IGrPovRaySolidObject
{

}