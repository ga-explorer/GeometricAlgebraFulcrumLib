using System.Collections;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Images;

public sealed class GrVisualImageSetGroup :
    IReadOnlyDictionary<string, GrVisualImageSetItem>
{
    private readonly Dictionary<string, GrVisualImageSetItem> _nameItemDictionary
        = new Dictionary<string, GrVisualImageSetItem>();

    public GrVisualImageSet ImageSet { get; }

    public string GroupName { get; }

    public int Count
        => _nameItemDictionary.Count;

    public IEnumerable<string> Keys
        => _nameItemDictionary.Keys;

    public IEnumerable<GrVisualImageSetItem> Values
        => _nameItemDictionary.Values;

    public GrVisualImageSetItem this[string key]
        => _nameItemDictionary[key];

    public string ImageSetFolder
        => ImageSet.ImageSetFolder;

    public string ImageSetGroupFolder
        => Path.Combine(ImageSet.ImageSetFolder, GroupName);

    public int MinImageWidth { get; private set; }

    public int MaxImageWidth { get; private set; }

    public int MinImageHeight { get; private set; }

    public int MaxImageHeight { get; private set; }


    internal GrVisualImageSetGroup(GrVisualImageSet imageSet, string groupName)
    {
        ImageSet = imageSet;
        GroupName = groupName;

        if (!Directory.Exists(ImageSetGroupFolder))
            Directory.CreateDirectory(ImageSetGroupFolder);
    }


    public void Clear()
    {
        _nameItemDictionary.Clear();
    }

    internal void ValidateGroupFolder()
    {
        if (!Directory.Exists(ImageSetGroupFolder))
            Directory.CreateDirectory(ImageSetGroupFolder);
    }

    public bool ContainsKey(string key)
    {
        return _nameItemDictionary.ContainsKey(key);
    }

    public bool TryGetValue(string key, out GrVisualImageSetItem value)
    {
        return _nameItemDictionary.TryGetValue(key, out value);
    }

    public bool RemoveImage(string imageName)
    {
        return _nameItemDictionary.Remove(imageName);
    }

    public void AddOrSetImage(string imageName, Image image)
    {
        var item = new GrVisualImageSetItem(this, imageName, image);

        if (_nameItemDictionary.ContainsKey(imageName))
            _nameItemDictionary[imageName] = item;
        else
            _nameItemDictionary.Add(imageName, item);
    }

    public void AddOrSetImageFromPngFile(string imageName)
    {
        var item = new GrVisualImageSetItem(this, imageName);

        if (_nameItemDictionary.ContainsKey(imageName))
            _nameItemDictionary[imageName] = item;
        else
            _nameItemDictionary.Add(imageName, item);
    }

    public void AddOrSetImageFromFile(string imageName, string imageFilePath)
    {
        var item = new GrVisualImageSetItem(this, imageName, imageFilePath);

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


    public IEnumerator<KeyValuePair<string, GrVisualImageSetItem>> GetEnumerator()
    {
        return _nameItemDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}