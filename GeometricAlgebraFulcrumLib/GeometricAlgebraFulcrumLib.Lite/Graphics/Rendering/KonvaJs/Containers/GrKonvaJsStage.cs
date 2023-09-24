using System.Collections.Immutable;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;
using TextComposerLib;
using TextComposerLib.Text;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Parametric;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Containers;

/// <summary>
/// https://konvajs.org/api/Konva.Stage.html
/// </summary>
public class GrKonvaJsStage :
    GrKonvaJsContainer
{
    public class StageOptions :
        GrKonvaJsObjectOptions
    {
        public GrKonvaJsCodeValue? Container
        {
            get => GetAttributeValueOrNull<GrKonvaJsCodeValue>("Container");
            set => SetAttributeValue("Container", value);
        }

        public GrKonvaJsFloat32Value? X
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("X");
            set => SetAttributeValue("X", value);
        }

        public GrKonvaJsFloat32Value? Y
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Y");
            set => SetAttributeValue("Y", value);
        }

        public GrKonvaJsFloat32Value? Width
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Width");
            set => SetAttributeValue("Width", value);
        }

        public GrKonvaJsFloat32Value? Height
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Height");
            set => SetAttributeValue("Height", value);
        }

        public GrKonvaJsBooleanValue? Visible
        {
            get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("Visible");
            set => SetAttributeValue("Visible", value);
        }

        public GrKonvaJsBooleanValue? Listening
        {
            get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("Listening");
            set => SetAttributeValue("Listening", value);
        }

        public GrKonvaJsStringValue? Id
        {
            get => GetAttributeValueOrNull<GrKonvaJsStringValue>("Id");
            set => SetAttributeValue("Id", value);
        }

        public GrKonvaJsStringValue? Name
        {
            get => GetAttributeValueOrNull<GrKonvaJsStringValue>("Name");
            set => SetAttributeValue("Name", value);
        }

        public GrKonvaJsFloat32Value? Opacity
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Opacity");
            set => SetAttributeValue("Opacity", value);
        }

        public GrKonvaJsVector2Value? Scale
        {
            get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("Scale");
            set => SetAttributeValue("Scale", value);
        }

        public GrKonvaJsFloat32Value? ScaleX
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ScaleX");
            set => SetAttributeValue("ScaleX", value);
        }

        public GrKonvaJsFloat32Value? ScaleY
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ScaleY");
            set => SetAttributeValue("ScaleY", value);
        }

        public GrKonvaJsFloat32Value? Rotation
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Rotation");
            set => SetAttributeValue("Rotation", value);
        }

        public GrKonvaJsVector2Value? Offset
        {
            get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("Offset");
            set => SetAttributeValue("Offset", value);
        }

        public GrKonvaJsFloat32Value? OffsetX
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("OffsetX");
            set => SetAttributeValue("OffsetX", value);
        }

        public GrKonvaJsFloat32Value? OffsetY
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("OffsetY");
            set => SetAttributeValue("OffsetY", value);
        }

        public GrKonvaJsBooleanValue? Draggable
        {
            get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("Draggable");
            set => SetAttributeValue("Draggable", value);
        }

        public GrKonvaJsFloat32Value? DragDistance
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("DragDistance");
            set => SetAttributeValue("DragDistance", value);
        }

        public GrKonvaJsCodeValue? DragBoundFunc
        {
            get => GetAttributeValueOrNull<GrKonvaJsCodeValue>("DragBoundFunc");
            set => SetAttributeValue("DragBoundFunc", value);
        }


        public StageOptions()
        {
            SetAttributeValue("Container", (GrKonvaJsCodeValue)"renderDiv");
        }
        
        public StageOptions(StageOptions options)
        {
            SetAttributeValues(options);
            SetAttributeValue("Container", (GrKonvaJsCodeValue)"renderDiv");
        }
    }

    public class StageProperties :
        GrKonvaJsContainerProperties
    {

    }


    protected override string ConstructorName
        => "new Konva.Stage";

    public StageOptions Options { get; private set; }

    public StageProperties Properties { get; private set; }

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
        
        Options = new StageOptions
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

        Properties = new StageProperties();

        DrawAfterCreation = true;
    }


    public GrKonvaJsStage SetOptions(StageOptions options)
    {
        var container = 
            Options.Container;
        
        Options = new StageOptions(options)
        {
            Container = container,
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        return this;
    }
    
    public GrKonvaJsStage UpdateOptions(StageOptions options)
    {
        Options.SetAttributeValues(options);

        return this;
    }

    public GrKonvaJsStage SetProperties(StageProperties properties)
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

    
    public override string GetCode()
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
            composer.AppendLineAtNewLine(item.GetCode());
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

        var stageCode = GetCode();

        return htmlCodeComposer.GenerateText(
            new Dictionary<string, string>
            {
                {"width", Options.Width!.GetCode()},
                {"height", Options.Height!.GetCode()},
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