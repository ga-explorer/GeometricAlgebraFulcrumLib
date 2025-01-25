using System.Collections;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Textures;

public sealed class GrVisualTextureSetGroup :
    IReadOnlyDictionary<string, GrVisualTexture>
{
    private readonly Dictionary<string, GrVisualTexture> _nameItemDictionary
        = new Dictionary<string, GrVisualTexture>();

    public GrVisualTextureSet TextureSet { get; }

    public string GroupName { get; }

    public int Count
        => _nameItemDictionary.Count;

    public IEnumerable<string> Keys
        => _nameItemDictionary.Keys;

    public IEnumerable<GrVisualTexture> Values
        => _nameItemDictionary.Values;

    public GrVisualTexture this[string key]
        => _nameItemDictionary[key];

    public string TextureSetFolder 
        => TextureSet.TextureSetFolder;
    
    public string TextureGroupFolder 
        => Path.Combine(TextureSet.TextureSetFolder, GroupName);
    
    public int MinImageWidth { get; private set; }

    public int MaxImageWidth { get; private set; }

    public int MinImageHeight { get; private set; }

    public int MaxImageHeight { get; private set; }


    internal GrVisualTextureSetGroup(GrVisualTextureSet textureSet, string groupName)
    {
        TextureSet = textureSet;
        GroupName = groupName;
    }


    public void Clear()
    {
        _nameItemDictionary.Clear();
    }
    
    internal void ValidateGroupFolder()
    {
        if (!Directory.Exists(TextureGroupFolder))
            Directory.CreateDirectory(TextureGroupFolder);
    }

    public bool ContainsKey(string key)
    {
        return _nameItemDictionary.ContainsKey(key);
    }

    public bool TryGetValue(string key, out GrVisualTexture value)
    {
        return _nameItemDictionary.TryGetValue(key, out value);
    }

    public bool RemoveTexture(string imageName)
    {
        return _nameItemDictionary.Remove(imageName);
    }

    public void AddOrSetTexture(string imageName, Image image)
    {
        var item = new GrVisualTexture(this, imageName, image);

        if (_nameItemDictionary.ContainsKey(imageName))
            _nameItemDictionary[imageName] = item;
        else
            _nameItemDictionary.Add(imageName, item);
    }
    
    public void AddOrSetTextureFromPngFile(string imageName)
    {
        var item = new GrVisualTexture(this, imageName);

        if (_nameItemDictionary.ContainsKey(imageName))
            _nameItemDictionary[imageName] = item;
        else
            _nameItemDictionary.Add(imageName, item);
    }
    
    public void AddOrSetTextureFromFile(string imageName, string imageFilePath)
    {
        var item = new GrVisualTexture(this, imageName, imageFilePath);

        if (_nameItemDictionary.ContainsKey(imageName))
            _nameItemDictionary[imageName] = item;
        else
            _nameItemDictionary.Add(imageName, item);
    }

    internal void UpdateImageSizeRange()
    {
        if (_nameItemDictionary.Count == 0)
        {
            MinImageWidth = 0;
            MaxImageWidth = 0;
            MinImageHeight = 0;
            MaxImageHeight = 0;

            return;
        }

        MinImageWidth = int.MaxValue;
        MaxImageWidth = int.MinValue;
        MinImageHeight = int.MaxValue;
        MaxImageHeight = int.MinValue;

        foreach (var item in _nameItemDictionary.Values)
        {
            var imageWidth = item.ImageWidth;
            var imageHeight = item.ImageHeight;

            if (imageWidth < MinImageWidth) MinImageWidth = imageWidth;
            if (imageWidth > MaxImageWidth) MaxImageWidth = imageWidth;

            if (imageHeight < MinImageHeight) MinImageHeight = imageHeight;
            if (imageHeight > MaxImageHeight) MaxImageHeight = imageHeight;
        }
    }


    public IEnumerator<KeyValuePair<string, GrVisualTexture>> GetEnumerator()
    {
        return _nameItemDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}