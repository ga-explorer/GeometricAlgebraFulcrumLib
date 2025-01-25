using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Parametric;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Containers;

/// <summary>
/// https://konvajs.org/api/Konva.Stage.html
/// </summary>
public class GrKonvaJsStage :
    GrKonvaJsContainer
{
    protected override string ConstructorName
        => "new Konva.Stage";

    public GrKonvaJsStageOptions Options { get; private set; }

    public GrKonvaJsStageProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsContainerProperties ContainerProperties
        => Properties;

    public override int Count 
        => Layers.Count;
    
    public override IGrKonvaJsObject this[string key] 
        => Layers.First(layer => layer.ConstName == key);

    public override IEnumerable<string> Keys 
        => Layers.Select(layer => layer.ConstName);

    public override IEnumerable<IGrKonvaJsObject> Values 
        => Layers;

    public IReadOnlyList<GrKonvaJsLayer> Layers { get; }

    public IReadOnlyList<GrKonvaJsLayerComposer> LayerComposers { get; }
    

    public GrKonvaJsStage(string constName, GrKonvaJsCodeValue container, double width, double height)
        : base(constName)
    {
        Layers = 5.GetRange(i => 
            new GrKonvaJsLayer($"layer{i}")
        ).ToImmutableArray();

        LayerComposers = 5.GetRange(i => 
            new GrKonvaJsLayerComposer(this, i)
        ).ToImmutableArray();
        
        Options = new GrKonvaJsStageOptions
        {
            Container = container,
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote(),
            Width = width,
            Height = height,
            X = width / 2, 
            Y = height / 2,
            ScaleY = -1
        };

        Properties = new GrKonvaJsStageProperties();

        DrawAfterCreation = true;
    }


    public GrKonvaJsStage SetOptions(GrKonvaJsStageOptions options)
    {
        var container = 
            Options.Container;
        
        Options = new GrKonvaJsStageOptions(options)
        {
            Container = container,
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        return this;
    }
    
    public GrKonvaJsStage UpdateOptions(GrKonvaJsStageOptions options)
    {
        Options.SetAttributeValues(options);

        return this;
    }

    public GrKonvaJsStage SetProperties(GrKonvaJsStageProperties properties)
    {
        Properties = properties;

        return this;
    }

    
    public override bool ContainsShapes()
    {
        return Layers.Any(item => item.ContainsShapes());
    }

    public override bool ContainsKey(string key)
    {
        return Layers.Any(layer => layer.ConstName == key);
    }

    public override bool TryGetValue(string key, out IGrKonvaJsObject value)
    {
        foreach (var layer in Layers)
            if (layer.ConstName == key)
            {
                value = layer;
                return true;
            }

        value = default!;
        return false;
    }

    public override IEnumerator<KeyValuePair<string, IGrKonvaJsObject>> GetEnumerator()
    {
        return Layers.Select(layer => 
            new KeyValuePair<string, IGrKonvaJsObject>(layer.ConstName, layer)
        ).GetEnumerator();
    }

    
    public override string GetKonvaJsCode()
    {
        var composer = new LinearTextComposer();

        var constructorCode = GetConstructorCode();
        var propertiesCode = GetPropertiesCode();
        
        if (!string.IsNullOrEmpty(ConstName))
        {
            var declarationKeyword = UseLetDeclaration ? "let" : "const";

            composer.Append($"{declarationKeyword} {ConstName} = ");
        }

        composer
            .AppendLine(constructorCode)
            .AppendLine(propertiesCode);
        
        var stageObjects =
            Layers
                .Where(item => item.ContainsShapes())
                .ToImmutableArray();

        foreach (var item in stageObjects)
        {
            composer.AppendLineAtNewLine(item.GetKonvaJsCode());
        }
        
        var stageObjectNames =
            stageObjects
                .Select(item => item.ConstName)
                .Concatenate(", ");

        composer.AppendLineAtNewLine($"{ConstName}.add({stageObjectNames});");

        if (DrawAfterCreation)
            composer.AppendLineAtNewLine($"{ConstName}.draw();");

        return composer.ToString();
    }

    public string GetHtmlCode()
    {
        var htmlCodeComposer
            = new ParametricTextComposer("!#", "#!", @"
<!DOCTYPE html>
<html>

<head>
    <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />

    <title>Konva.js code</title>

    <!--  -->
    <!-- script src=""./konva.min.js""></script -->
    <script src=""https://unpkg.com/konva@9/konva.min.js""></script>

    <style>
        html,
        body {
            overflow: auto;
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
        }

        #renderDiv {
            position: absolute;
            top: 0px;
            left: 0px;
            width: !#width#!px;
            height: !#height#!px;
            touch-action: none;
        }
    </style>
</head>

<body>
    <div id='renderDiv'> </div>

    <script>
        !#stage-code#!
    </script>
</body>

</html>
");

        var stageCode = GetKonvaJsCode();

        return htmlCodeComposer.GenerateText(
            new Dictionary<string, string>
            {
                {"width", Options.Width!.GetAttributeValueCode()},
                {"height", Options.Height!.GetAttributeValueCode()},
                {"stage-code", stageCode}
            }
        );
    }

    public void SaveHtmlCode(string filePath)
    {
        var htmlCode = GetHtmlCode();

        File.WriteAllText(filePath, htmlCode);
    }
}