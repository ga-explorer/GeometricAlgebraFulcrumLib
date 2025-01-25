using GeometricAlgebraFulcrumLib.Utilities.Structures.Files;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.KaTeX;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Textures
{
    public sealed class GrVisualTextureSet
    {
        private readonly Dictionary<string, GrVisualTextureSetGroup> _nameGroupDictionary
            = new Dictionary<string, GrVisualTextureSetGroup>();


        public string TextureSetFolder { get; }

        public GrVisualTextureSetGroup this[string groupName]
            => _nameGroupDictionary[groupName];

        public GrVisualTexture this[string groupName, string imageName]
            => _nameGroupDictionary[groupName][imageName];


        public GrVisualTextureSet(string textureSetFolder)
        {
            TextureSetFolder = textureSetFolder;
        }


        public GrVisualTextureSet Clear()
        {
            _nameGroupDictionary.Clear();

            return this;
        }

        public GrVisualTextureSetGroup GetGroup(string groupName)
        {
            return _nameGroupDictionary[groupName];
        }

        public GrVisualTextureSetGroup GetOrAddGroup(string groupName)
        {
            if (_nameGroupDictionary.TryGetValue(groupName, out var group))
                return group;

            group = new GrVisualTextureSetGroup(this, groupName);

            _nameGroupDictionary.Add(groupName, group);

            return group;
        }

        public GrVisualTexture GetTexture(string groupName, string imageName)
        {
            return _nameGroupDictionary.TryGetValue(groupName, out var group)
                ? group[imageName]
                : throw new KeyNotFoundException(nameof(groupName));
        }

        public GrVisualTextureSet AddTexture(string groupName, string imageName, Image image)
        {
            var group = GetOrAddGroup(groupName);

            group.AddOrSetTexture(imageName, image);

            return this;
        }
        
        public GrVisualTextureSet AddTextures(string groupName, IEnumerable<KeyValuePair<string, Image>> nameImagePairs)
        {
            var group = GetOrAddGroup(groupName);

            foreach (var (imageName, image) in nameImagePairs)
                group.AddOrSetTexture(imageName, image);

            return this;
        }
        
        public GrVisualTextureSet AddTextures(string groupName, WclKaTeXComposer katexComposer)
        {
            var group = GetOrAddGroup(groupName);

            foreach (var (imageName, item) in katexComposer)
                group.AddOrSetTexture(imageName, item.PngImage);

            return this;
        }

        public GrVisualTextureSet AddTextureFromPngFile(string groupName, string imageName, string? filePath = null)
        {
            //var filePath = TextureSetFolder.GetFilePath($@"{groupName}\{imageName}.png");

            var image = Image.Load<Rgba32>(
                filePath ?? TextureSetFolder.GetFilePath($@"{groupName}\{imageName}.png")
            );

            return AddTexture(groupName, imageName, image);
        }

        public GrVisualTextureSet AddTexturesFromPngFiles(string groupName, IEnumerable<string> imageNames)
        {
            var group = GetOrAddGroup(groupName);

            foreach (var imageName in imageNames)
            {
                group.AddOrSetTextureFromPngFile(imageName);
            }

            return this;
        }

        public GrVisualTextureSet AddTexturesFromFiles(string groupName, IEnumerable<KeyValuePair<string, string>> nameFilePairs)
        {
            var group = GetOrAddGroup(groupName);

            foreach (var (imageName, imageFilePath) in nameFilePairs)
            {
                group.AddOrSetTextureFromFile(imageName, imageFilePath);
            }

            return this;
        }

        public GrVisualTextureSet FinalizeTextures()
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
