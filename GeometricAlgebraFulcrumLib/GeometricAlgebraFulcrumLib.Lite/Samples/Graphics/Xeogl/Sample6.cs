using GeometricAlgebraFulcrumLib.Lite.Graphics.LatticeShapes;
using GeometricAlgebraFulcrumLib.Lite.Graphics.LatticeShapes.Surfaces;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives.Lines;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl;

namespace GeometricAlgebraFulcrumLib.Lite.Samples.Graphics.Xeogl;

public static class Sample6
{
    private static GrLatticeSurfaceList3D ComposeDataSet()
    {
        var dataSet = new GrLatticeSurfaceList3D();
            
        dataSet
            .AddZSphereSurface(36, 19)
            .ScalePointsBy(10);

        dataSet.FinalizeSet();

        return dataSet;
    }

    public static string Generate(string htmlFilePath = "")
    {
        var dataSet = ComposeDataSet();
        var trianglesGeometry = dataSet.GetTriangleGeometry();

        trianglesGeometry.VertexColorsEnabled = false;
        trianglesGeometry.VertexNormalsEnabled = true;

        var linesGeometryComposer = new GrLineGeometryComposer3D();
        //linesGeometryComposer.AddLines(trianglesGeometry.GetTriangleEdges());
        linesGeometryComposer.AddLines(trianglesGeometry.GetDisplacedTriangleEdges(-0.005d));
        linesGeometryComposer.AddLines(trianglesGeometry.GetDisplacedTriangleEdges(0.005d));
            
        var normalLinesGeometryComposer = new GrLineGeometryComposer3D();
        normalLinesGeometryComposer.AddLines(trianglesGeometry.GetNormalLines(0, 1));

        var scriptGenerator = new XeoglHtmlComposer();
        scriptGenerator.IncludesList.Add("js/xeogl.js");
        scriptGenerator.IncludesList.Add("js/generation/geometryBuilder.js");

        scriptGenerator.AddTrianglesGeometry(trianglesGeometry, @"new xeogl.PhongMaterial({ diffuse: [0.6, 0.6, 1.0] })");
        scriptGenerator.AddLinesGeometry(linesGeometryComposer, @"new xeogl.PhongMaterial({ emissive: [Math.random() + 0.5, Math.random() + 0.5, Math.random() + 0.5] })");
        scriptGenerator.AddLinesGeometry(normalLinesGeometryComposer, @"new xeogl.PhongMaterial({ emissive: [Math.random() + 0.5, Math.random() + 0.5, Math.random() + 0.5] })");

        var scriptCode = scriptGenerator.GenerateHtmlPage();

        if (string.IsNullOrEmpty(htmlFilePath))
            return scriptCode;

        File.WriteAllText(htmlFilePath, scriptCode);

        return scriptCode;
    }
}