using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PathsMesh;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PathsMesh.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Lines;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Triangles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl;

namespace GeometricAlgebraFulcrumLib.Samples.Modeling.Graphics.Xeogl;

public static class Sample3
{
    public static string Generate()
    {
        var firstPath = new LinearPointsPath3D(
            LinFloat64Vector3D.Create(-3, 0, 0),
            LinFloat64Vector3D.Create(3, 0, 0),
            5
        );

        var lastPath = new LinearPointsPath3D(
            LinFloat64Vector3D.Create(0, -2, 0),
            LinFloat64Vector3D.Create(0, 2, 2),
            5
        );

        var pathMesh = new LerpPathsMesh3D(firstPath, lastPath, 10);

        //var pathMesh = SimpleComposers.ParallelogramPathMesh(
        //    Float64Vector3D.Create(0, 0, 0), 
        //    Float64Vector3D.Create(2, 0, 0), 
        //    Float64Vector3D.Create(0, 2, 0), 
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

        //Render normals to mesh patches
        var normalsLinesComposer = new GrLineGeometryComposer3D();
        normalsLinesComposer.AddLines(composer.GetNormalLines(1));

        var normalsLinesGeometry = normalsLinesComposer.GenerateGeometry();

        //Render triangles of mesh patches
        var linesComposer = new GrLineGeometryComposer3D();
        linesComposer.AddLines(composer.GetDisplacedTriangleEdges(-0.025d));
        linesComposer.AddLines(composer.GetDisplacedTriangleEdges(0.025d));

        var linesGeometry = linesComposer.GenerateGeometry();

        var scriptGenerator = new XeoglHtmlComposer();
        scriptGenerator.IncludesList.Add("js/xeogl/xeogl.js");
        scriptGenerator.IncludesList.Add("js/generation/geometryBuilder.js");

        scriptGenerator.AddTrianglesGeometry(composer, @"new xeogl.PhongMaterial({ diffuse: [0.6, 0.6, 1.0] })");
        scriptGenerator.AddLinesGeometry(normalsLinesGeometry, @"new xeogl.PhongMaterial({ emissive: [Math.random() + 0.5, Math.random() + 0.5, Math.random() + 0.5] })");
        scriptGenerator.AddLinesGeometry(linesGeometry, @"new xeogl.PhongMaterial({ emissive: [Math.random() + 0.5, Math.random() + 0.5, Math.random() + 0.5] })");

        var scriptCode = scriptGenerator.GenerateHtmlPage();

        return scriptCode;
    }
}