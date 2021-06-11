namespace EuclideanGeometryLib.BasicMath
{
    public interface IGeometricElement
    {
        /// <summary>
        /// True if the basic elements of this geometry are valid.
        /// For example, if this is a 3D point, this geometry has no NAN values
        /// in its X, Y, and Z coordinates
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// True if the basic elements of this geometry are invalid.
        /// For example, if this is a 3D point, this geometry has a NAN value
        /// in its X, Y, or Z coordinates
        /// </summary>
        bool IsInvalid { get; }
    }
}