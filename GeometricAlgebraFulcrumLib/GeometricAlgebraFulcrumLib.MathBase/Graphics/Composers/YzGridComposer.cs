using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PathsMesh.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Composers
{
    public sealed class YzGridComposer
    {
        /// <summary>
        /// The center point of the grid
        /// </summary>
        public Float64Vector3DComposer Center { get; } 
            = Float64Vector3DComposer.Create(0, 0, 0);

        /// <summary>
        /// The size of each grid unit in the Y direction
        /// </summary>
        public double YUnitSize { get; set; } 
            = 1.0d;

        /// <summary>
        /// The size of each grid unit in the Z direction
        /// </summary>
        public double ZUnitSize { get; set; } 
            = 1.0d;

        /// <summary>
        /// The number of grid units in the Y direction on each side
        /// </summary>
        public int YUnitsCount { get; set; } 
            = 10;

        /// <summary>
        /// The number of grid units in the Z direction on each side
        /// </summary>
        public int ZUnitsCount { get; set; } 
            = 10;

        /// <summary>
        /// The number of grid units in the Y direction on both side
        /// </summary>
        public int YUnitTotalCount 
            => 2 * YUnitsCount;

        /// <summary>
        /// The number of grid units in the Z direction on both side
        /// </summary>
        public int ZUnitTotalCount 
            => 2 * ZUnitsCount;

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
        /// The lower left corner point of the grid
        /// </summary>
        public Float64Vector3D CornerLowerLeft 
            => Float64Vector3D.Create(Center.X.Value, YMin, ZMin);

        /// <summary>
        /// The lower right corner point of the grid
        /// </summary>
        public Float64Vector3D CornerLowerRight 
            => Float64Vector3D.Create(Center.X.Value, YMax, ZMin);

        /// <summary>
        /// The upper left corner point of the grid
        /// </summary>
        public Float64Vector3D CornerUpperLeft 
            => Float64Vector3D.Create(Center.X.Value, YMin, ZMax);

        /// <summary>
        /// The upper right corner point of the grid
        /// </summary>
        public Float64Vector3D CornerUpperRight 
            => Float64Vector3D.Create(Center.X.Value, YMax, ZMax);

        /// <summary>
        /// The total length of the grid in the Y direction
        /// </summary>
        public double YSize 
            => YUnitTotalCount * YUnitSize;

        /// <summary>
        /// The total length of the grid in the Z direction
        /// </summary>
        public double ZSize 
            => ZUnitTotalCount * ZUnitSize;


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
                YMin, 
                YMax, 
                ZMin, 
                ZMax
            );
        }
    }
}
