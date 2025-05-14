using System.Drawing;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Svg.DrawingBoard;

namespace GeometricAlgebraFulcrumLib.Applications.Geometry.VoronoiDiagrams;

/// <summary>
/// Implementation of "Constructing Voronoi Diagrams from Hollow Spheres
/// Using Conformal Geometric Algebra"
/// https://link.springer.com/article/10.1007/s00006-017-0787-x
/// </summary>
public static class VoronoiDiagramsSample
{
    private static XGaFloat64RandomComposer _randomComposer;

    public static LinFloat64Vector2D[] PointsArray { get; private set; }

    public static XGaFloat64Processor ConformalProcessor { get; }
        = XGaFloat64Processor.Conformal;

    public static int VSpaceDimensions 
        => 4;


    public static void Execute()
    {
        var n = VSpaceDimensions - 2;

        _randomComposer = ConformalProcessor.CreateXGaRandomComposer(VSpaceDimensions, 10);

        // Fill random points array
        PointsArray = new LinFloat64Vector2D[8];

        for (var i = 0; i < PointsArray.Length; i++)
            PointsArray[i] = LinFloat64Vector2D.Create(
                _randomComposer.GetScalarValue(-500, 500),
                _randomComposer.GetScalarValue(-500, 500)
            );

        // Given a set of m points, a hollow sphere Sn has the properties:
        //   * d + 1 points of the set P lie on the surface of Sn.
        //   * No point of P lies inside Sn (i.e. it is hollow).
        //   * The sphere center represents a vertex in a Voronoi diagram.
        //   * The line connecting the center of a sphere with the center of any border
        //     sphere represents an edge of a Voronoi diagram.

        // Initialization: Find a simplex containing all points:
        // Step 1: find bounding hyper-sphere, this is the inner sphere of the auxiliary simplex
        var boundingSphere = Float64BoundingCircle2D.CreateFromPoints(PointsArray);

        // Step 4: compute coordinates of regular simplex vertices
        var simplexFrame =
            boundingSphere.Center.ToXGaFloat64Vector()
                .CreateFixedFrameOfSimplex(
                    n,
                    boundingSphere.Radius * Math.Sqrt(n * (n + 1))
                );

        Console.WriteLine(simplexFrame.ToString());
        Console.WriteLine();

        var simplexPoints = 
            simplexFrame.Select(p => p.ToVector2D()).ToArray();
            

        var boundingBox = simplexPoints.GetBoundingBox(1.15);
        var drawingBoard = SvgDrawingBoard.Create(boundingBox, 1);
        var drawingBoardLayer = drawingBoard.ActiveLayer;

        drawingBoardLayer
            .SetDefaultPen(1, Color.Black);

        drawingBoardLayer
            .SetFill(Color.AntiqueWhite)
            .DrawCircle(boundingSphere.Center, boundingSphere.Radius);

        foreach (var point in PointsArray)
            drawingBoardLayer
                .SetFill(Color.CadetBlue)
                .DrawCircleMarker(point, 5);

        foreach (var point in simplexPoints)
            drawingBoardLayer
                .SetFill(Color.Red)
                .DrawCircleMarker(point, 5);

        drawingBoardLayer
            .DrawLineSegment(simplexPoints[0], simplexPoints[1])
            .DrawLineSegment(simplexPoints[1], simplexPoints[2])
            .DrawLineSegment(simplexPoints[2], simplexPoints[0]);

        drawingBoard.SaveToPngFile(@"D:\drawingBoard.png");
    }
}