using System.Collections;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Containers;

/// <summary>
/// https://konvajs.org/api/Konva.Container.html
/// </summary>
public abstract class GrKonvaJsContainer :
    GrKonvaJsNode,
    IReadOnlyDictionary<string, IGrKonvaJsObject>
{
    public abstract class GrKonvaJsContainerProperties :
        GrKonvaJsNodeProperties
    {
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

        public GrKonvaJsBoundingBoxValue? Clip
        {
            get => GetAttributeValueOrNull<GrKonvaJsBoundingBoxValue>("Clip");
            init => SetAttributeValue("Clip", value);
        }

        public GrKonvaJsCodeValue? ClipFunc
        {
            get => GetAttributeValueOrNull<GrKonvaJsCodeValue>("ClipFunc");
            init => SetAttributeValue("ClipFunc", value);
        }
    }


    public abstract GrKonvaJsContainerProperties? ContainerProperties { get; }

    public override GrKonvaJsNodeProperties? NodeProperties
        => ContainerProperties;
    
    public abstract int Count { get; }

    public abstract IGrKonvaJsObject this[string key] { get; }

    public abstract IEnumerable<string> Keys { get; }

    public abstract IEnumerable<IGrKonvaJsObject> Values { get; }
    

    protected GrKonvaJsContainer(string constName)
        : base(constName)
    {
    }

    
    public abstract bool ContainsShapes();

    public abstract bool ContainsKey(string key);

    public abstract bool TryGetValue(string key, out IGrKonvaJsObject value);

    public abstract IEnumerator<KeyValuePair<string, IGrKonvaJsObject>> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}