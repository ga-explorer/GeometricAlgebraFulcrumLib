using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Containers;

/// <summary>
/// https://konvajs.org/api/Konva.Layer.html
/// </summary>
public class GrKonvaJsLayer :
    GrKonvaJsContainer,
    IGrKonvaJsStageObject
{
    protected override string ConstructorName
        => "new Konva.Layer";

    public GrKonvaJsLayerOptions Options { get; private set; }

    public GrKonvaJsLayerProperties Properties { get; private set; }

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
        Options = new GrKonvaJsLayerOptions()
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new GrKonvaJsLayerProperties();
    }


    public GrKonvaJsLayer SetOptions(GrKonvaJsLayerOptions options)
    {
        Options = new GrKonvaJsLayerOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        return this;
    }

    public GrKonvaJsLayer SetProperties(GrKonvaJsLayerProperties properties)
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

        var layerObjects =
            LayerObjects
                .Where(item => 
                    item is GrKonvaJsShapeBase ||
                    (item is GrKonvaJsGroup group && group.ContainsShapes())
                ).ToImmutableArray();

        foreach (var item in layerObjects)
        {
            composer.AppendLineAtNewLine(item.GetKonvaJsCode());
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