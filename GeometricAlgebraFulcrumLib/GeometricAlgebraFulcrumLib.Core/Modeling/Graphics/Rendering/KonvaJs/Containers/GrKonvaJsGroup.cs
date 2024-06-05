using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Shapes;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Containers;

/// <summary>
/// https://konvajs.org/api/Konva.Group.html
/// </summary>
public class GrKonvaJsGroup :
    GrKonvaJsContainer,
    IGrKonvaJsGroupObject
{
    public class GroupOptions :
        GrKonvaJsObjectOptions
    {
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


        public GroupOptions()
        {
        }
        
        public GroupOptions(GroupOptions options)
        {
            SetAttributeValues(options);
        }
    }

    public class GroupProperties :
        GrKonvaJsContainerProperties
    {
        
    }


    protected override string ConstructorName
        => "new Konva.Group";

    public GroupOptions Options { get; private set; }

    public GroupProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsContainerProperties ContainerProperties
        => Properties;
    
    public override int Count 
        => GroupObjects.Count;
    
    public override IGrKonvaJsObject this[string key] 
        => GroupObjects.First(item => item.ConstName == key);

    public override IEnumerable<string> Keys 
        => GroupObjects.Select(item => item.ConstName);

    public override IEnumerable<IGrKonvaJsObject> Values 
        => GroupObjects;

    protected List<IGrKonvaJsGroupObject> GroupObjects { get; }
        = new List<IGrKonvaJsGroupObject>();
    

    public GrKonvaJsGroup(string constName)
        : base(constName)
    {
        Options = new GroupOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new GroupProperties();
    }


    public GrKonvaJsGroup SetOptions(GroupOptions options)
    {
        Options = new GroupOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        return this;
    }

    public GrKonvaJsGroup SetProperties(GroupProperties properties)
    {
        Properties = properties;

        return this;
    }

    
    public GrKonvaJsGroup Clear()
    {
        GroupObjects.Clear();

        return this;
    }

    public GrKonvaJsGroup Add(IGrKonvaJsGroupObject groupObject)
    {
        GroupObjects.Add(groupObject);

        return this;
    }

    public override bool ContainsShapes()
    {
        return GroupObjects.Any(item => 
            item is GrKonvaJsShapeBase ||
            (item is GrKonvaJsGroup group && group.ContainsShapes())
        );
    }

    public override bool ContainsKey(string key)
    {
        return GroupObjects.Any(item => item.ConstName == key);
    }

    public override bool TryGetValue(string key, out IGrKonvaJsObject value)
    {
        foreach (var item in GroupObjects.Where(layer => layer.ConstName == key))
        {
            value = item;
            return true;
        }

        value = default!;
        return false;
    }

    public override IEnumerator<KeyValuePair<string, IGrKonvaJsObject>> GetEnumerator()
    {
        return GroupObjects.Select(item => 
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

        var groupObjects =
            GroupObjects
                .Where(item => 
                    item is GrKonvaJsShapeBase ||
                    (item is GrKonvaJsGroup group && group.ContainsShapes())
                ).ToImmutableArray();

        foreach (var item in groupObjects)
        {
            composer.AppendLineAtNewLine(item.GetCode());
        }

        var groupObjectNames =
            groupObjects
                .Select(item => item.ConstName)
                .Concatenate(", ");

        composer.AppendLineAtNewLine($"{ConstName}.add({groupObjectNames});");
        
        if (DrawAfterCreation)
            composer.AppendLineAtNewLine($"{ConstName}.draw();");

        return composer.ToString();
    }
}