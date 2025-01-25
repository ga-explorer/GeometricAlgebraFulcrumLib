using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Containers;

/// <summary>
/// https://konvajs.org/api/Konva.Group.html
/// </summary>
public class GrKonvaJsGroup :
    GrKonvaJsContainer,
    IGrKonvaJsGroupObject
{
    protected override string ConstructorName
        => "new Konva.Group";

    public GrKonvaJsGroupOptions Options { get; private set; }

    public GrKonvaJsGroupProperties Properties { get; private set; }

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
        Options = new GrKonvaJsGroupOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new GrKonvaJsGroupProperties();
    }


    public GrKonvaJsGroup SetOptions(GrKonvaJsGroupOptions options)
    {
        Options = new GrKonvaJsGroupOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        return this;
    }

    public GrKonvaJsGroup SetProperties(GrKonvaJsGroupProperties properties)
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

        var groupObjects =
            GroupObjects
                .Where(item => 
                    item is GrKonvaJsShapeBase ||
                    (item is GrKonvaJsGroup group && group.ContainsShapes())
                ).ToImmutableArray();

        foreach (var item in groupObjects)
        {
            composer.AppendLineAtNewLine(item.GetKonvaJsCode());
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