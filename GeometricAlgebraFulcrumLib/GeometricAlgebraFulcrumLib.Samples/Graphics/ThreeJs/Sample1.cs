using System;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs.Objects;
using TextComposerLib.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Samples.Graphics.ThreeJs
{
    public static class Sample1
    {
        public static void Execute()
        {
            var composer = JavaScriptCodeComposer.DefaultComposer;

            composer
                .VarLet("scene, camera, renderer, mesh")
                .EmptyLine();

            var camera = "camera".JsConst(
                new JsPerspectiveCamera()
            );

            camera.Zoom = 3;
            camera.Up = new JsVector3(0, 0, 10);
            camera.SetViewOffset(1, 2, 3, 4, 5, 6);

            Console.WriteLine(composer.GetJsCode());
        }
    }
}