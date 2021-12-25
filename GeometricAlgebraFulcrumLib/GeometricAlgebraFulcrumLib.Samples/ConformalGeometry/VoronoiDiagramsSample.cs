using System;
using System.Drawing;
using System.Linq;
using NumericalGeometryLib.Borders;
using NumericalGeometryLib.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.Geometry.Graphics.Space2D;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GraphicsComposerLib.Svg.DrawingBoard;

namespace GeometricAlgebraFulcrumLib.Samples.ConformalGeometry
{
    /// <summary>
    /// Implementation of "Constructing Voronoi Diagrams from Hollow Spheres
    /// Using Conformal Geometric Algebra"
    /// https://link.springer.com/article/10.1007/s00006-017-0787-x
    /// </summary>
    public static class VoronoiDiagramsSample
    {
        private static GeometricAlgebraRandomComposer<double> _randomComposer;

        public static EuclideanPoint2D[] PointsArray { get; private set; }

        public static GeometricAlgebraConformalProcessor<double> ConformalProcessor { get; }
            = ScalarAlgebraFloat64Processor.DefaultProcessor.CreateGeometricAlgebraConformalProcessor(4);


        public static void Execute()
        {
            var n = ConformalProcessor.VSpaceDimension - 2U;

            _randomComposer = ConformalProcessor.CreateGeometricRandomComposer(10);

            // Fill random points array
            PointsArray = new EuclideanPoint2D[8];

            for (var i = 0; i < PointsArray.Length; i++)
                PointsArray[i] = new EuclideanPoint2D(
                    _randomComposer.GetScalar(-500, 500),
                    _randomComposer.GetScalar(-500, 500)
                );

            // Given a set of m points, a hollow sphere Sn has the properties:
            //   * d + 1 points of the set P lie on the surface of Sn.
            //   * No point of P lies inside Sn (i.e. it is hollow).
            //   * The sphere center represents a vertex in a Voronoi diagram.
            //   * The line connecting the center of a sphere with the center of any border
            //     sphere represents an edge of a Voronoi diagram.

            // Initialization: Find a simplex containing all points:
            // Step 1: find bounding hyper-sphere, this is the inner sphere of the auxiliary simplex
            var boundingSphere = BoundingSphere2D.CreateFromPoints(PointsArray);

            // Step 4: compute coordinates of regular simplex vertices
            var simplexFrame =
                ConformalProcessor
                    .CreateFixedFrameOfSimplex(
                        n,
                        boundingSphere.Center.ToVector(),
                        boundingSphere.Radius * Math.Sqrt(n * (n + 1))
                    );

            Console.WriteLine(simplexFrame.ToString());
            Console.WriteLine();

            var simplexPoints = 
                simplexFrame.Select(p => p.ToEuclideanPoint2D()).ToArray();
            

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
}