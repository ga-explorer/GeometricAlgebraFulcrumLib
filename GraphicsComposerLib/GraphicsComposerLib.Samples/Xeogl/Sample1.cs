using System.IO;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PathsMesh;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PathsMesh.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Primitives.Lines;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Primitives.Triangles;
using GraphicsComposerLib.Rendering.Xeogl;

namespace GraphicsComposerLib.Samples.Xeogl
{
    public static class Sample1
    {
        private static IPathsMesh3D ComposePathsMesh()
        {
            var path1 = new ConstantPointsPath3D(
                4, 
                new Float64Tuple3D(0, 0, -2)
            );

            var path2 = new ArrayPointsPath3D(
                new Float64Tuple3D(-2, -2, -2),
                new Float64Tuple3D(2, -2, -2),
                new Float64Tuple3D(2, 2, -2),
                new Float64Tuple3D(-2, 2, -2)
            );

            var path3 = new ArrayPointsPath3D(
                new Float64Tuple3D(-2, -2, 2),
                new Float64Tuple3D(2, -2, 2),
                new Float64Tuple3D(2, 2, 2),
                new Float64Tuple3D(-2, 2, 2)
            );

            var path4 = new ArrayPointsPath3D(
                    new Float64Tuple3D(-1, -1, 2),
                    new Float64Tuple3D(1, -1, 2),
                    new Float64Tuple3D(1, 1, 2),
                    new Float64Tuple3D(-1, 1, 2)
                );

            var pathMesh = new ArrayPathsMesh3D(
                4, 
                path1, 
                path2, 
                path3, 
                path4
            );

            return pathMesh;
        }

        private static GrTriangleGeometry3D ComposeTrianglesGeometry(IPathsMesh3D pathMesh)
        {
            //Test 1:
            var trianglesGeometry = pathMesh.GetGraphicsTrianglesGeometry(
                GrVertexNormalComputationMethod.WeightedNormals,
                false
            );

            //Test 2:
            //foreach (var patch in pathMesh.GetQuadPatches(true, true, false))
            //    triMesh.AddPatch(patch);


            //Test 3:
            //var patch = new PathMeshPatch3D(pathMesh);

            //patch.SetPathsRange(0, 2, false);
            //triMesh.AddPatch(patch);

            //patch.SetPathsRange(1, 2, false);
            //triMesh.AddPatch(patch);

            //patch.SetPathsRange(2, 2, false);
            //triMesh.AddPatch(patch);

            //patch.SetPathsRange(3, 2, false);
            //triMesh.AddPatch(patch);

            return trianglesGeometry;
        }

        public static string Generate(string htmlFilePath = "")
        {
            var pathMesh = ComposePathsMesh();

            var trianglesGeometry = pathMesh.GetGraphicsTrianglesGeometry(
                GrVertexNormalComputationMethod.WeightedNormals,
                false
            );

            var linesGeometryComposer = new GrLineGeometryComposer3D();
            linesGeometryComposer.AddLines(trianglesGeometry.GetDisplacedTriangleEdges(-0.05d));
            linesGeometryComposer.AddLines(trianglesGeometry.GetDisplacedTriangleEdges(0.05d));
            var linesGeometry = linesGeometryComposer.GenerateGeometry();

            var normalLinesGeometryComposer = new GrLineGeometryComposer3D();
            normalLinesGeometryComposer.AddLines(trianglesGeometry.GetNormalLines(-1, 1));
            var normalLinesGeometry = normalLinesGeometryComposer.GenerateGeometry();

            var scriptGenerator = new XeoglHtmlComposer();
            scriptGenerator.IncludesList.Add("js/xeogl/xeogl.js");
            scriptGenerator.IncludesList.Add("js/generation/geometryBuilder.js");

            scriptGenerator.AddTrianglesGeometry(trianglesGeometry, @"new xeogl.PhongMaterial({ diffuse: [0.6, 0.6, 1.0] })");
            scriptGenerator.AddLinesGeometry(linesGeometry, @"new xeogl.PhongMaterial({ emissive: [Math.random() + 0.5, Math.random() + 0.5, Math.random() + 0.5] })");
            scriptGenerator.AddLinesGeometry(normalLinesGeometry, @"new xeogl.PhongMaterial({ emissive: [Math.random() + 0.5, Math.random() + 0.5, Math.random() + 0.5] })");

            var scriptCode = scriptGenerator.GenerateHtmlPage();

            if (string.IsNullOrEmpty(htmlFilePath))
                return scriptCode;

            File.WriteAllText(htmlFilePath, scriptCode);

            return scriptCode;
        }
    }
}
