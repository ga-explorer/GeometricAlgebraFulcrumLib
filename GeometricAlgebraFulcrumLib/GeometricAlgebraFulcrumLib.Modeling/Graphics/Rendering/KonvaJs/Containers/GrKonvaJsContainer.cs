using System.Collections;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Containers;

/// <summary>
/// https://konvajs.org/api/Konva.Container.html
/// </summary>
public abstract class GrKonvaJsContainer :
    GrKonvaJsNode,
    IReadOnlyDictionary<string, IGrKonvaJsObject>
{
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