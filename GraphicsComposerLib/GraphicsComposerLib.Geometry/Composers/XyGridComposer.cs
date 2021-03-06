using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Meshes.PathsMesh.Space3D;
using GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D;

namespace GraphicsComposerLib.Geometry.Composers
{
    public sealed class XyGridComposer
    {
        /// <summary>
        /// The center point of the grid
        /// </summary>
        public Tuple3D Center { get; } 
            = new Tuple3D(0, 0, 0);

        /// <summary>
        /// The size of each grid unit in the X direction
        /// </summary>
        public double XUnitSize { get; set; } 
            = 1.0d;

        /// <summary>
        /// The size of each grid unit in the Y direction
        /// </summary>
        public double YUnitSize { get; set; } 
            = 1.0d;

        /// <summary>
        /// The number of grid units in the X direction on each side
        /// </summary>
        public int XUnitsCount { get; set; } 
            = 10;

        /// <summary>
        /// The number of grid units in the Y direction on each side
        /// </summary>
        public int YUnitsCount { get; set; } 
            = 10;

        /// <summary>
        /// The number of grid units in the X direction on both side
        /// </summary>
        public int XUnitTotalCount 
            => 2 * XUnitsCount;

        /// <summary>
        /// The number of grid units in the Y direction on both side
        /// </summary>
        public int YUnitTotalCount 
            => 2 * YUnitsCount;

        /// <summary>
        /// The smallest X coordinate of the grid
        /// </summary>
        public double XMin 
            => Center.X - XUnitSize * XUnitsCount;

        /// <summary>
        /// The largest X coordinate of the grid
        /// </summary>
        public double XMax 
            => Center.X + XUnitSize * XUnitsCount;

        /// <summary>
        /// The smallest Y coordinate of the grid
        /// </summary>
        public double YMin 
            => Center.Y - YUnitSize * YUnitsCount;

        /// <summary>
        /// The largest Y coordinate of the grid
        /// </summary>
        public double YMax 
            => Center.Y + YUnitSize * YUnitsCount;

        /// <summary>
        /// The lower left corner point of the grid
        /// </summary>
        public Tuple3D CornerLowerLeft 
            => new Tuple3D(XMin, YMin, Center.Z);

        /// <summary>
        /// The lower right corner point of the grid
        /// </summary>
        public Tuple3D CornerLowerRight 
            => new Tuple3D(XMax, YMin, Center.Z);

        /// <summary>
        /// The upper left corner point of the grid
        /// </summary>
        public Tuple3D CornerUpperLeft 
            => new Tuple3D(XMin, YMax, Center.Z);

        /// <summary>
        /// The upper right corner point of the grid
        /// </summary>
        public Tuple3D CornerUpperRight 
            => new Tuple3D(XMax, YMax, Center.Z);

        /// <summary>
        /// The total length of the grid in the X direction
        /// </summary>
        public double XSize 
            => XUnitTotalCount * XUnitSize;

        /// <summary>
        /// The total length of the grid in the Y direction
        /// </summary>
        public double YSize 
            => YUnitTotalCount * YUnitSize;


        /// <summary>
        /// Create a path mesh from the specs of this grid composer
        /// </summary>
        /// <returns></returns>
        public ListPathsMesh3D ComposeMesh()
        {
            var path1 = new ArrayPointsPath3D(
                CornerLowerLeft, 
                CornerLowerRight
            );

            var path2 = new ArrayPointsPath3D(
                CornerUpperLeft, 
                CornerUpperRight
            );

            return new ListPathsMesh3D(2, path1, path2);
        }

        /// <summary>
        /// Compose path mesh patch from the specs of this grid composer
        /// </summary>
        /// <returns></returns>
        public TexturedPathsMesh3D ComposeTexturedMesh()
        {
            return new TexturedPathsMesh3D(
                ComposeMesh(), 
                XMin, 
                XMax, 
                YMin, 
                YMax
            );
        }
    }
}
