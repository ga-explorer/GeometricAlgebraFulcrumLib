using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicMath.Tuples.Mutable;
using GraphicsComposerLib.Geometry.Geometry.PathsMesh.Space3D;
using GraphicsComposerLib.Geometry.Geometry.PointsPath.Space3D;

namespace GraphicsComposerLib.Geometry.Composers
{
    public sealed class ZxGridComposer
    {
        /// <summary>
        /// The center point of the grid
        /// </summary>
        public MutableTuple3D Center { get; } 
            = new MutableTuple3D(0, 0, 0);

        /// <summary>
        /// The size of each grid unit in the Z direction
        /// </summary>
        public double ZUnitSize { get; set; } 
            = 1.0d;

        /// <summary>
        /// The size of each grid unit in the X direction
        /// </summary>
        public double XUnitSize { get; set; } 
            = 1.0d;

        /// <summary>
        /// The number of grid units in the Z direction on each side
        /// </summary>
        public int ZUnitsCount { get; set; } 
            = 10;

        /// <summary>
        /// The number of grid units in the X direction on each side
        /// </summary>
        public int XUnitsCount { get; set; } 
            = 10;

        /// <summary>
        /// The number of grid units in the Z direction on both side
        /// </summary>
        public int ZUnitTotalCount 
            => 2 * ZUnitsCount;

        /// <summary>
        /// The number of grid units in the X direction on both side
        /// </summary>
        public int XUnitTotalCount 
            => 2 * XUnitsCount;

        /// <summary>
        /// The smallest Z coordinate of the grid
        /// </summary>
        public double ZMin 
            => Center.Z - ZUnitSize * ZUnitsCount;

        /// <summary>
        /// The largest Z coordinate of the grid
        /// </summary>
        public double ZMax 
            => Center.Z + ZUnitSize * ZUnitsCount;

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
        /// The lower left corner point of the grid
        /// </summary>
        public Tuple3D CornerLowerLeft 
            => new Tuple3D(XMin, Center.Y, ZMin);

        /// <summary>
        /// The lower right corner point of the grid
        /// </summary>
        public Tuple3D CornerLowerRight 
            => new Tuple3D(XMin, Center.Y, ZMax);

        /// <summary>
        /// The upper left corner point of the grid
        /// </summary>
        public Tuple3D CornerUpperLeft 
            => new Tuple3D(XMax, Center.Y, ZMin);

        /// <summary>
        /// The upper right corner point of the grid
        /// </summary>
        public Tuple3D CornerUpperRight 
            => new Tuple3D(XMax, Center.Y, ZMax);

        /// <summary>
        /// The total length of the grid in the Z direction
        /// </summary>
        public double ZSize 
            => ZUnitTotalCount * ZUnitSize;

        /// <summary>
        /// The total length of the grid in the X direction
        /// </summary>
        public double XSize 
            => XUnitTotalCount * XUnitSize;


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
                ZMin, 
                ZMax, 
                XMin, 
                XMax
            );
        }
    }
}
