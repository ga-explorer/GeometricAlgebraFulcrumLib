using GeometricAlgebraFulcrumLib.Modeling.Graphics.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PathsMesh;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Lines;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Triangles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Textures;
using SixLabors.ImageSharp.Formats.Png;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Graphics.Xeogl;

public static class Sample4
{
    public static string Generate()
    {
        var xyGridComposer = new XyGridComposer();
        var yzGridComposer = new YzGridComposer();
        var zxGridComposer = new ZxGridComposer();
        yzGridComposer.Center.X = xyGridComposer.XMin;
        zxGridComposer.Center.Y = xyGridComposer.YMin;

        var trianglesComposer =
            new GrTriangleGeometryComposer3D();

        //Make two patches covering the whole path mesh to render triangles from both sides
        trianglesComposer
            .BeginBatch()
            .AddTrianglesFromMesh(xyGridComposer.ComposeTexturedMesh())
            .EndBatch();

        trianglesComposer
            .BeginBatch()
            .AddTrianglesFromMesh(yzGridComposer.ComposeTexturedMesh())
            .EndBatch();

        trianglesComposer
            .BeginBatch()
            .AddTrianglesFromMesh(zxGridComposer.ComposeTexturedMesh())
            .EndBatch();

        // var trianglesGeometry = trianglesComposer.GenerateGeometry();

        //Render normals to mesh patches
        var normalsLineComposer = new GrLineGeometryComposer3D();
        normalsLineComposer.AddLines(trianglesComposer.GetNormalLines(1));

        var normalsLineGeometry = normalsLineComposer.GenerateGeometry();

        //Render triangles of mesh patches
        var linesComposer = new GrLineGeometryComposer3D();

        linesComposer.AddLines(
            trianglesComposer.GetDisplacedTriangleEdges(-0.025d)
        );

        linesComposer.AddLines(
            trianglesComposer.GetDisplacedTriangleEdges(0.025d)
        );

        // var linesGeometry = linesComposer.GenerateGeometry();

        var scriptGenerator = new XeoglHtmlComposer();
        scriptGenerator.IncludesList.Add("js/xeogl/xeogl.js");
        scriptGenerator.IncludesList.Add("js/generation/geometryBuilder.js");

        scriptGenerator.AddTrianglesGeometry(trianglesComposer, @"new xeogl.PhongMaterial({ diffuseMap: new xeogl.Texture({ src: ""Sample4/gridTexture.png"" }) })");
        //scriptGenerator.AddLineMeshGenerator(normalsLineGeometry, @"new xeogl.PhongMaterial({ emissive: [Math.random() + 0.5, Math.random() + 0.5, Math.random() + 0.5] })");
        //scriptGenerator.AddLineMeshGenerator(linesGeometry, @"new xeogl.PhongMaterial({ emissive: [Math.random() + 0.5, Math.random() + 0.5, Math.random() + 0.5] })");

        var gridTexture = new GridTextureComposer();
        var gridTextureImage = gridTexture.ComposeImage();

        using var stream = File.OpenWrite(
            @"C:\Projects\Study\WebGL\samples\Sample4\gridTexture.png"
        );

        gridTextureImage.Save(stream, new PngEncoder());

        var scriptCode = scriptGenerator.GenerateHtmlPage();

        return scriptCode;
    }
}