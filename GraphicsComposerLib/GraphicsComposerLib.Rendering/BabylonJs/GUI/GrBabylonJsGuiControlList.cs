using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace GraphicsComposerLib.Rendering.BabylonJs.GUI;

public sealed class GrBabylonJsGuiControlList :
    IReadOnlyList<GrBabylonJsGuiControl>

{
    private readonly Dictionary<string, GrBabylonJsGuiControl> _objectDictionary 
        = new Dictionary<string, GrBabylonJsGuiControl>();


    public int Count 
        => _objectDictionary.Count;

    public GrBabylonJsGuiControl this[int index] 
        => _objectDictionary.Skip(index).First().Value;
    
    public GrBabylonJsGuiControl this[string constName] 
        => _objectDictionary[constName];


    public GrBabylonJsGuiControlList Add([NotNull] GrBabylonJsGuiControl babylonObject)
    {
        _objectDictionary.Add(babylonObject.ConstName, babylonObject);

        return this;
    }

    public bool Contains([NotNull] GrBabylonJsGuiControl babylonObject)
    {
        return _objectDictionary.TryGetValue(babylonObject.ConstName, out var babylonObject1) && 
               ReferenceEquals(babylonObject, babylonObject1);
    }

    public bool TryGetControl(string name, out GrBabylonJsGuiControl babylonObject)
    {
        return _objectDictionary.TryGetValue(name, out babylonObject);
    }

    public IEnumerator<GrBabylonJsGuiControl> GetEnumerator()
    {
        return _objectDictionary.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}