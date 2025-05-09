namespace GeometricAlgebraFulcrumLib.Core;

public interface IAlgebraicElement
{
    /// <summary>
    /// True if the basic elements of this geometry are valid.
    /// For example, if this is a 3D point, this geometry has no NAN values
    /// in its X, Y, and Z coordinates
    /// </summary>
    bool IsValid();
}