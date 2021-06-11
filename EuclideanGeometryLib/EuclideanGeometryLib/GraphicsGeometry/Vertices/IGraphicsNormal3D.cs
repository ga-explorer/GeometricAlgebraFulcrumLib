using EuclideanGeometryLib.BasicMath.Tuples;

namespace EuclideanGeometryLib.GraphicsGeometry.Vertices
{
    public interface IGraphicsNormal3D :
        ITuple3D
    {
        /// <summary>
        /// Reset the normal to zero
        /// </summary>
        void Reset();

        /// <summary>
        /// Set the normal to the given value
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        void Set(double x, double y, double z);

        /// <summary>
        /// Set the normal to the given value
        /// </summary>
        /// <param name="normal"></param>
        void Set(ITuple3D normal);

        /// <summary>
        /// Add the given vector to the normal of this vertex
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="dz"></param>
        void Update(double dx, double dy, double dz);

        /// <summary>
        /// Add the given vector to this normal
        /// </summary>
        /// <param name="dNormal"></param>
        void Update(ITuple3D dNormal);

        /// <summary>
        /// Make the normal vector of this vertex a unit vector if not near zero
        /// </summary>
        void MakeUnit();

        /// <summary>
        /// 
        /// </summary>
        void MakeNegativeUnit();

        /// <summary>
        /// 
        /// </summary>
        void MakeNegative();
    }
}