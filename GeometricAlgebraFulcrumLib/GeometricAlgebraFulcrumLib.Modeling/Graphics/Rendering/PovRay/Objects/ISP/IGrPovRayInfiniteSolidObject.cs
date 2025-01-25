namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.ISP;

/// <summary>
/// There are six polynomial primitive shapes that are possibly infinite
/// and do not respond to automatic bounding. They are plane, cubic, poly,
/// quartic, polynomial, and quadric. They do have a well-defined inside
/// and may be used in CSG and inside a clipped_by statement. As with all
/// shapes they can be translated, rotated and scaled.
/// http://www.povray.org/documentation/3.7.0/r3_4.html#r3_4_5_3
/// </summary>
public interface IGrPovRayInfiniteSolidObject :
    IGrPovRaySolidObject
{

}