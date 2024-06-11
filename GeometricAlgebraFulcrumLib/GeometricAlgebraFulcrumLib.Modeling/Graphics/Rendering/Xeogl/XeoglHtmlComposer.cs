using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Lines;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Triangles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Generators;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Parametric;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Structured;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl;

public sealed class XeoglHtmlComposer
{
    private readonly LinearTextComposer _scriptComposer
        = new LinearTextComposer();

    private readonly List<XeoglMeshGenerator> _generatorsList
        = new List<XeoglMeshGenerator>();


    public string PageTitle { get; set; } = "xeogl Script";

    public bool RotateCamera
        => !RotateCameraRate.X.IsZero() ||
           !RotateCameraRate.Y.IsZero() ||
           !RotateCameraRate.Z.IsZero();

    public LinFloat64Vector3DComposer RotateCameraRate { get; }
        = LinFloat64Vector3DComposer.Create();

    public List<string> IncludesList { get; }
        = new List<string>();


    private void GenerateInitializationCode()
    {

    }

    private void GenerateFinalizationCode()
    {
        if (RotateCamera)
        {
            _scriptComposer
                .AppendAtNewLine(@"xeogl.scene.on('tick', function () {")
                .IncreaseIndentation();

            if (!RotateCameraRate.X.IsZero())
                _scriptComposer
                    .AppendAtNewLine("xeogl.scene.camera.view.rotateEyeX(")
                    .Append(RotateCameraRate.X.ToString("G"))
                    .Append(")");

            if (!RotateCameraRate.Y.IsZero())
                _scriptComposer
                    .AppendAtNewLine("xeogl.scene.camera.view.rotateEyeY(")
                    .Append(RotateCameraRate.Y.ToString("G"))
                    .Append(")");

            if (!RotateCameraRate.Z.IsZero())
                _scriptComposer
                    .AppendAtNewLine("xeogl.scene.camera.view.rotateEyeZ(")
                    .Append(RotateCameraRate.Z.ToString("G"))
                    .Append(")");

            _scriptComposer
                .DecreaseIndentation()
                .AppendAtNewLine("});")
                .AppendLine();
        }

        _scriptComposer
            .AppendAtNewLine(@"new xeogl.CameraControl();");
    }

    public string GenerateScript()
    {
        _scriptComposer.Clear();

        GenerateInitializationCode();

        foreach (var generator in _generatorsList)
            _scriptComposer.AppendLineAtNewLine(generator.Generate());

        GenerateFinalizationCode();

        return _scriptComposer.ToString();
    }

    private string GenerateIncludes()
    {
        var composer =
            new ListTextComposer(Environment.NewLine)
            {
                ActiveItemPrefix = @"<script src = """,
                ActiveItemSuffix = @""" ></script>"
            };

        foreach (var i in IncludesList)
            composer.Add(i);

        return composer.ToString();
    }


    public XeoglLinesMeshGenerator AddLinesGeometry(IGraphicsLineGeometry3D geometry, string material)
    {
        var generator = new XeoglLinesMeshGenerator(geometry, material);
        _generatorsList.Add(generator);

        return generator;
    }

    public XeoglTrianglesMeshGenerator AddTrianglesGeometry(IGraphicsTriangleGeometry3D geometry, string material)
    {
        var generator = new XeoglTrianglesMeshGenerator(geometry, material);
        _generatorsList.Add(generator);

        return generator;
    }


    public string GenerateHtmlPage()
    {
        var template = new ParametricTextComposer("#", "#", @"
<!DOCTYPE html>
<html>
    <head>
        <title>#page-title#</title>

        #includes#

        <style>
            body{
                /* set margin to 0 and overflow to hidden, to use the complete page */
                margin: 0;
                overflow: hidden;
            }
        </style>
    </head>
    <body>
        <!-- Div which will hold the Output -->
        <div id = ""WebGL-output"" >
        </ div >

        <!--Javascript code -->
        <script>
            // once everything is loaded, we run our WebGL stuff.
            function init()
            {
                #script#
            };

            window.onload = init;
        </script>
    </body>
</html>
");

        return template.GenerateText(
            "page-title", PageTitle,
            "includes", GenerateIncludes(),
            "script", GenerateScript()
        );
    }
}