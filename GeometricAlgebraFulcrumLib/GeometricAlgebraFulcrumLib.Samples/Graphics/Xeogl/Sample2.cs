using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PathsMesh;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PathsMesh.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Primitives.Lines;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GraphicsComposerLib.Rendering.Xeogl;

namespace GeometricAlgebraFulcrumLib.Samples.Graphics.Xeogl
{
    public static class Sample2
    {
        private static IPathsMesh3D ConstructPathsMesh()
        {
            var path1 = new ConstantPointsPath3D(
                4, 
                Float64Vector3D.Create(0, 0, -2)
            );

            var path2 = new ArrayPointsPath3D(
                Float64Vector3D.Create(-2, -2, -2),
                Float64Vector3D.Create(2, -2, -2),
                Float64Vector3D.Create(2, 2, -2),
                Float64Vector3D.Create(-2, 2, -2)
            );

            var path3 = new ArrayPointsPath3D(
                Float64Vector3D.Create(-2, -2, 2),
                Float64Vector3D.Create(2, -2, 2),
                Float64Vector3D.Create(2, 2, 2),
                Float64Vector3D.Create(-2, 2, 2)
            );

            var path4 = new ArrayPointsPath3D(
                Float64Vector3D.Create(-1, -1, 2),
                Float64Vector3D.Create(1, -1, 2),
                Float64Vector3D.Create(1, 1, 2),
                Float64Vector3D.Create(-1, 1, 2)
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

        private static IGraphicsLineGeometry3D ConstructLinesGeometry(IPathsMesh3D pathMesh)
        {
            //Test 1:
            var lineMesh = GrLineSoupGeometry3D.Create(
                pathMesh.GetLines(true, true)
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

            return lineMesh;
        }

        public static string Generate()
        {
            var pathsMesh = ConstructPathsMesh();
            var linesGeometry = ConstructLinesGeometry(pathsMesh);

            var scriptGenerator = new XeoglHtmlComposer();
            scriptGenerator.IncludesList.Add("js/xeogl/xeogl.js");
            scriptGenerator.IncludesList.Add("js/generation/geometryBuilder.js");

            scriptGenerator.AddLinesGeometry(linesGeometry, @"new xeogl.PhongMaterial({ emissive: [Math.random() + 0.5, Math.random() + 0.5, Math.random() + 0.5] })");

            var scriptCode = scriptGenerator.GenerateHtmlPage();

            return scriptCode;
        }
    }
}
