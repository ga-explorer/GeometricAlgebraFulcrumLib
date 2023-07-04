//using System.Collections;

//namespace GraphicsComposerLib.Rendering.KonvaJs.Containers;

//public sealed class GrKonvaJsLayerObjectList :
//    IReadOnlyList<IGrKonvaJsLayerObject>
//{
//    private readonly Dictionary<string, IGrKonvaJsLayerObject> _objectDictionary
//        = new Dictionary<string, IGrKonvaJsLayerObject>();


//    public int Count
//        => _objectDictionary.Count;

//    public IGrKonvaJsLayerObject this[int index]
//        => _objectDictionary.Skip(index).First().Value;

//    public IGrKonvaJsLayerObject this[string constName]
//        => _objectDictionary[constName];


//    public GrKonvaJsLayerObjectList Add(IGrKonvaJsLayerObject babylonObject)
//    {
//        _objectDictionary.Add(babylonObject.ConstName, babylonObject);

//        return this;
//    }

//    public bool Contains(IGrKonvaJsLayerObject babylonObject)
//    {
//        return _objectDictionary.TryGetValue(babylonObject.ConstName, out var babylonObject1) &&
//               ReferenceEquals(babylonObject, babylonObject1);
//    }

//    public bool TryGetObject(string name, out IGrKonvaJsLayerObject? babylonObject)
//    {
//        return _objectDictionary.TryGetValue(name, out babylonObject);
//    }

//    public IEnumerator<IGrKonvaJsLayerObject> GetEnumerator()
//    {
//        return _objectDictionary.Values.GetEnumerator();
//    }

//    IEnumerator IEnumerable.GetEnumerator()
//    {
//        return GetEnumerator();
//    }
//}