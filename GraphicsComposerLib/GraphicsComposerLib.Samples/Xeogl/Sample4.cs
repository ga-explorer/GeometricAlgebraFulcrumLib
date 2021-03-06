using EuclideanGeometryLib.Textures;
using GraphicsComposerLib.Geometry.Composers;
using GraphicsComposerLib.Geometry.Meshes.PathsMesh;
using GraphicsComposerLib.Geometry.Primitives;
using GraphicsComposerLib.Geometry.Primitives.Lines;
using GraphicsComposerLib.Geometry.Primitives.Triangles;
using GraphicsComposerLib.WebGl.Xeogl;

namespace GraphicsComposerLib.Samples.Xeogl
{
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
            gridTextureImage.Save(@"C:\Projects\Study\WebGL\samples\Sample4\gridTexture.png");

            var scriptCode = scriptGenerator.GenerateHtmlPage();

            return scriptCode;
        }
    }
}
