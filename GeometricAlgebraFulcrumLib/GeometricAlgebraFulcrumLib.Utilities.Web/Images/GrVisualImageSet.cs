using GeometricAlgebraFulcrumLib.Utilities.Structures.Files;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.KaTeX;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Images
{
    public sealed class GrVisualImageSet
    {
        private readonly Dictionary<string, GrVisualImageSetGroup> _nameGroupDictionary
            = new Dictionary<string, GrVisualImageSetGroup>();


        public string ImageSetFolder { get; }

        public GrVisualImageSetGroup this[string groupName]
            => _nameGroupDictionary[groupName];

        public GrVisualImageSetItem this[string groupName, string imageName]
            => _nameGroupDictionary[groupName][imageName];


        public GrVisualImageSet(string imageSetFolder)
        {
            ImageSetFolder = imageSetFolder;

            if (!Directory.Exists(ImageSetFolder))
                Directory.CreateDirectory(ImageSetFolder);
        }


        public GrVisualImageSet Clear()
        {
            _nameGroupDictionary.Clear();

            return this;
        }

        public GrVisualImageSetGroup GetGroup(string groupName)
        {
            return _nameGroupDictionary[groupName];
        }

        public GrVisualImageSetGroup GetOrAddGroup(string groupName)
        {
            if (_nameGroupDictionary.TryGetValue(groupName, out var group))
                return group;

            group = new GrVisualImageSetGroup(this, groupName);

            _nameGroupDictionary.Add(groupName, group);

            return group;
        }

        public GrVisualImageSetItem GetImage(string groupName, string imageName)
        {
            return _nameGroupDictionary.TryGetValue(groupName, out var group)
                ? group[imageName]
                : throw new KeyNotFoundException(nameof(groupName));
        }

        public GrVisualImageSet AddImage(string groupName, string imageName, Image image)
        {
            var group = GetOrAddGroup(groupName);

            group.AddOrSetImage(imageName, image);

            return this;
        }

        public GrVisualImageSet AddImages(string groupName, IEnumerable<KeyValuePair<string, Image>> nameImagePairs)
        {
            var group = GetOrAddGroup(groupName);

            foreach (var (imageName, image) in nameImagePairs)
                group.AddOrSetImage(imageName, image);

            return this;
        }

        public GrVisualImageSet AddImages(string groupName, WclKaTeXComposer katexComposer)
        {
            var group = GetOrAddGroup(groupName);

            foreach (var (imageName, item) in katexComposer)
                group.AddOrSetImage(imageName, item.PngImage);

            return this;
        }

        public GrVisualImageSet AddImageFromPngFile(string groupName, string imageName, string? filePath = null)
        {
            //var filePath = ImageSetFolder.GetFilePath($@"{groupName}\{imageName}.png");

            var image = Image.Load<Rgba32>(
                filePath ?? ImageSetFolder.GetFilePath($@"{groupName}\{imageName}.png")
            );

            return AddImage(groupName, imageName, image);
        }

        public GrVisualImageSet AddImagesFromPngFiles(string groupName, IEnumerable<string> imageNames)
        {
            var group = GetOrAddGroup(groupName);

            foreach (var imageName in imageNames)
            {
                group.AddOrSetImageFromPngFile(imageName);
            }

            return this;
        }

        public GrVisualImageSet AddImagesFromFiles(string groupName, IEnumerable<KeyValuePair<string, string>> nameFilePairs)
        {
            var group = GetOrAddGroup(groupName);

            foreach (var (imageName, imageFilePath) in nameFilePairs)
            {
                group.AddOrSetImageFromFile(imageName, imageFilePath);
            }

            return this;
        }

        public GrVisualImageSet FinalizeGroups()
        {
            foreach (var group in _nameGroupDictionary.Values)
            {
                group.UpdateImageSizeRange();
                //group.SavePngImages();
            }

            return this;
        }

    }
}
