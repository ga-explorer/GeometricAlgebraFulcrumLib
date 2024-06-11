using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PathsMesh.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Composers;

public sealed class XyGridComposer
{
    /// <summary>
    /// The center point of the grid
    /// </summary>
    public LinFloat64Vector3D Center { get; } 
        = LinFloat64Vector3D.Create(0, 0, 0);

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
    public LinFloat64Vector3D CornerLowerLeft 
        => LinFloat64Vector3D.Create(XMin, YMin, Center.Z.ScalarValue);

    /// <summary>
    /// The lower right corner point of the grid
    /// </summary>
    public LinFloat64Vector3D CornerLowerRight 
        => LinFloat64Vector3D.Create(XMax, YMin, Center.Z.ScalarValue);

    /// <summary>
    /// The upper left corner point of the grid
    /// </summary>
    public LinFloat64Vector3D CornerUpperLeft 
        => LinFloat64Vector3D.Create(XMin, YMax, Center.Z.ScalarValue);

    /// <summary>
    /// The upper right corner point of the grid
    /// </summary>
    public LinFloat64Vector3D CornerUpperRight 
        => LinFloat64Vector3D.Create(XMax, YMax, Center.Z.ScalarValue);

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