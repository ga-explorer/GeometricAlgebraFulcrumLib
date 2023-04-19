using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Meshes.PathsMesh;
using GraphicsComposerLib.Geometry.Meshes.PathsMesh.Space3D;
using GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D;
using GraphicsComposerLib.Geometry.Primitives;
using GraphicsComposerLib.Geometry.Primitives.Lines;
using GraphicsComposerLib.Geometry.Primitives.Triangles;
using GraphicsComposerLib.Rendering.Xeogl;

namespace GeometricAlgebraFulcrumLib.Samples.Graphics.Xeogl
{
    public static class Sample3
    {
        public static string Generate()
        {
            var firstPath = new LinearPointsPath3D(
                new Float64Tuple3D(-3,0,0), 
                new Float64Tuple3D(3,0,0), 
                5
            );

            var lastPath = new LinearPointsPath3D(
                new Float64Tuple3D(0, -2, 0),
                new Float64Tuple3D(0, 2, 2),
                5
            );

            var pathMesh = new LerpPathsMesh3D(firstPath, lastPath, 10);

            //var pathMesh = SimpleComposers.ParallelogramPathMesh(
            //    new Tuple3D(0, 0, 0), 
            //    new Tuple3D(2, 0, 0), 
            //    new Tuple3D(0, 2, 0), 
            //    3,
            //    3
            //    );

            //Make two patches covering the whole path mesh to render triangles from both sides
            var composer = new GrTriangleGeometryComposer3D
            {
                NormalComputationMethod = GrVertexNormalComputationMethod.WeightedNormals
            };

            composer
                .BeginBatch()
                .AddTriangles(pathMesh.GetTriangles())
                .EndBatch();
            
            composer
                .BeginBatch()
                .AddTriangles(pathMesh.GetTriangles(true))
                .EndBatch();

            var trianglesGeometry = composer;//.GenerateGeometry();

            //Render normals to mesh patches
            var normalsLinesComposer = new GrLineGeometryComposer3D();
            normalsLinesComposer.AddLines(trianglesGeometry.GetNormalLines(1));

            var normalsLinesGeometry = normalsLinesComposer.GenerateGeometry();

            //Render triangles of mesh patches
            var linesComposer = new GrLineGeometryComposer3D();
            linesComposer.AddLines(trianglesGeometry.GetDisplacedTriangleEdges(-0.025d));
            linesComposer.AddLines(trianglesGeometry.GetDisplacedTriangleEdges(0.025d));

            var linesGeometry = linesComposer.GenerateGeometry();

            var scriptGenerator = new XeoglHtmlComposer();
            scriptGenerator.IncludesList.Add("js/xeogl/xeogl.js");
            scriptGenerator.IncludesList.Add("js/generation/geometryBuilder.js");

            scriptGenerator.AddTrianglesGeometry(trianglesGeometry, @"new xeogl.PhongMaterial({ diffuse: [0.6, 0.6, 1.0] })");
            scriptGenerator.AddLinesGeometry(normalsLinesGeometry, @"new xeogl.PhongMaterial({ emissive: [Math.random() + 0.5, Math.random() + 0.5, Math.random() + 0.5] })");
            scriptGenerator.AddLinesGeometry(linesGeometry, @"new xeogl.PhongMaterial({ emissive: [Math.random() + 0.5, Math.random() + 0.5, Math.random() + 0.5] })");

            var scriptCode = scriptGenerator.GenerateHtmlPage();

            return scriptCode;
        }
    }
}
