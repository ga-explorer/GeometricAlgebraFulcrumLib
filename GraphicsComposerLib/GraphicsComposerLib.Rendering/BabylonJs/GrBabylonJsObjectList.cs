using System.Collections;

namespace GraphicsComposerLib.Rendering.BabylonJs;

public sealed class GrBabylonJsObjectList :
    IReadOnlyList<GrBabylonJsObject>
{
    private readonly Dictionary<string, GrBabylonJsObject> _objectDictionary 
        = new Dictionary<string, GrBabylonJsObject>();


    public int Count 
        => _objectDictionary.Count;

    public GrBabylonJsObject this[int index] 
        => _objectDictionary.Skip(index).First().Value;
    
    public GrBabylonJsObject this[string constName] 
        => _objectDictionary[constName];


    public GrBabylonJsObjectList Add(GrBabylonJsObject babylonObject)
    {
        _objectDictionary.Add(babylonObject.ConstName, babylonObject);

        return this;
    }

    public bool Contains(GrBabylonJsObject babylonObject)
    {
        return _objectDictionary.TryGetValue(babylonObject.ConstName, out var babylonObject1) && 
               ReferenceEquals(babylonObject, babylonObject1);
    }

    public bool TryGetObject(string name, out GrBabylonJsObject? babylonObject)
    {
        return _objectDictionary.TryGetValue(name, out babylonObject);
    }

    public IEnumerator<GrBabylonJsObject> GetEnumerator()
    {
        return _objectDictionary.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}