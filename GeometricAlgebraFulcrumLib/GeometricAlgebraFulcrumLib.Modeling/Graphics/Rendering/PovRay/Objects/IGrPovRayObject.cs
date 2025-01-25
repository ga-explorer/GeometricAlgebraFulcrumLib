namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects;

/// <summary>
/// Objects are the building blocks of your scene. There are a lot of
/// different types of objects supported by POV-Ray. There are Finite
/// Solid Primitives, Finite Patch Primitives and Infinite Solid Primitives.
/// These primitive shapes may be combined into complex shapes using
/// Constructive Solid Geometry (also known as CSG).
/// http://www.povray.org/documentation/3.7.0/r3_4.html#r3_4_5
/// </summary>
public interface IGrPovRayObject : 
    IGrPovRayStatement,
    IGrPovRayTransformableCodeElement
{
}