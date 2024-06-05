using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PathsMesh;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PathsMesh.Space3D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Primitives.Lines;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Primitives.Triangles;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Samples.Modeling.Graphics.Xeogl;

public static class Sample1
{
    private static IPathsMesh3D ComposePathsMesh()
    {
        var path1 = new ConstantPointsPath3D(
            4,
            LinFloat64Vector3D.Create(0, 0, -2)
        );

        var path2 = new ArrayPointsPath3D(
            LinFloat64Vector3D.Create(-2, -2, -2),
            LinFloat64Vector3D.Create(2, -2, -2),
            LinFloat64Vector3D.Create(2, 2, -2),
            LinFloat64Vector3D.Create(-2, 2, -2)
        );

        var path3 = new ArrayPointsPath3D(
            LinFloat64Vector3D.Create(-2, -2, 2),
            LinFloat64Vector3D.Create(2, -2, 2),
            LinFloat64Vector3D.Create(2, 2, 2),
            LinFloat64Vector3D.Create(-2, 2, 2)
        );

        var path4 = new ArrayPointsPath3D(
            LinFloat64Vector3D.Create(-1, -1, 2),
            LinFloat64Vector3D.Create(1, -1, 2),
            LinFloat64Vector3D.Create(1, 1, 2),
            LinFloat64Vector3D.Create(-1, 1, 2)
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