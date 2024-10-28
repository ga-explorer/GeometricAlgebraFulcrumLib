using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Objects;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Graphics.ThreeJs;

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