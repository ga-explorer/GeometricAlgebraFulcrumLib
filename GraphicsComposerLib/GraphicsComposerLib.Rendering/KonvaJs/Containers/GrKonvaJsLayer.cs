using System.Collections.Immutable;
using GraphicsComposerLib.Rendering.KonvaJs.Shapes;
using GraphicsComposerLib.Rendering.KonvaJs.Values;
using TextComposerLib;
using TextComposerLib.Text;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Rendering.KonvaJs.Containers;

/// <summary>
/// https://konvajs.org/api/Konva.Layer.html
/// </summary>
public class GrKonvaJsLayer :
    GrKonvaJsContainer,
    IGrKonvaJsStageObject
{
    public class LayerOptions :
        GrKonvaJsObjectOptions
    {
        public GrKonvaJsBooleanValue? ClearBeforeDraw
        {
            get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("ClearBeforeDraw");
            init => SetAttributeValue("ClearBeforeDraw", value);
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

        public GrKonvaJsFloat32Value? ClipX
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ClipX");
            init => SetAttributeValue("ClipX", value);
        }

        public GrKonvaJsFloat32Value? ClipY
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ClipY");
            init => SetAttributeValue("ClipY", value);
        }

        public GrKonvaJsFloat32Value? ClipWidth
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ClipWidth");
            init => SetAttributeValue("ClipWidth", value);
        }

        public GrKonvaJsFloat32Value? ClipHeight
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ClipHeight");
            init => SetAttributeValue("ClipHeight", value);
        }
        
        public GrKonvaJsCodeValue? ClipFunc
        {
            get => GetAttributeValueOrNull<GrKonvaJsCodeValue>("ClipFunc");
            init => SetAttributeValue("ClipFunc", value);
        }


        public LayerOptions()
        {
        }
        
        public LayerOptions(LayerOptions options)
        {
            SetAttributeValues(options);
        }
    }

    public class LayerProperties :
        GrKonvaJsContainerProperties
    {
        public GrKonvaJsBooleanValue? ClearBeforeDraw
        {
            get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("ClearBeforeDraw");
            init => SetAttributeValue("ClearBeforeDraw", value);
        }

        public GrKonvaJsBooleanValue? ImageSmoothingEnabled
        {
            get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("ImageSmoothingEnabled");
            init => SetAttributeValue("ImageSmoothingEnabled", value);
        }

        
    }


    protected override string ConstructorName
        => "new Konva.Layer";

    public LayerOptions Options { get; private set; }

    public LayerProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsContainerProperties ContainerProperties
        => Properties;

    public override int Count 
        => LayerObjects.Count;
    
    public override IGrKonvaJsObject this[string key] 
        => LayerObjects.First(item => item.ConstName == key);

    public override IEnumerable<string> Keys 
        => LayerObjects.Select(item => item.ConstName);

    public override IEnumerable<IGrKonvaJsObject> Values 
        => LayerObjects;

    protected List<IGrKonvaJsLayerObject> LayerObjects { get; }
        = new List<IGrKonvaJsLayerObject>();
    

    public GrKonvaJsLayer(string constName)
        : base(constName)
    {
        Options = new LayerOptions()
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new LayerProperties();
    }


    public GrKonvaJsLayer SetOptions(LayerOptions options)
    {
        Options = new LayerOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        return this;
    }

    public GrKonvaJsLayer SetProperties(LayerProperties properties)
    {
        Properties = properties;

        return this;
    }

    
    public override bool ContainsShapes()
    {
        return LayerObjects.Any(item => 
            item is GrKonvaJsShapeBase ||
            (item is GrKonvaJsGroup group && group.ContainsShapes())
        );
    }

    public GrKonvaJsLayer Clear()
    {
        LayerObjects.Clear();

        return this;
    }

    public GrKonvaJsLayer Add(IGrKonvaJsLayerObject layerObject)
    {
        LayerObjects.Add(layerObject);

        return this;
    }

    public override bool ContainsKey(string key)
    {
        return LayerObjects.Any(item => item.ConstName == key);
    }

    public override bool TryGetValue(string key, out IGrKonvaJsObject value)
    {
        foreach (var item in LayerObjects.Where(layer => layer.ConstName == key))
        {
            value = item;
            return true;
        }

        value = default!;
        return false;
    }

    public override IEnumerator<KeyValuePair<string, IGrKonvaJsObject>> GetEnumerator()
    {
        return LayerObjects.Select(item => 
            new KeyValuePair<string, IGrKonvaJsObject>(item.ConstName, item)
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

        var layerObjects =
            LayerObjects
                .Where(item => 
                    item is GrKonvaJsShapeBase ||
                    (item is GrKonvaJsGroup group && group.ContainsShapes())
                ).ToImmutableArray();

        foreach (var item in layerObjects)
        {
            composer.AppendLineAtNewLine(item.GetCode());
        }

        var layerObjectNames =
            layerObjects
                .Select(item => item.ConstName)
                .Concatenate(", ");

        composer.AppendLineAtNewLine($"{ConstName}.add({layerObjectNames});");
        
        if (DrawAfterCreation)
            composer.AppendLineAtNewLine($"{ConstName}.draw();");

        return composer.ToString();
    }

}