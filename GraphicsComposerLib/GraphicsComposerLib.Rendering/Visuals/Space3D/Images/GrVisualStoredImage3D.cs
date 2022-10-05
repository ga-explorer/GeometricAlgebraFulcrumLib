using DataStructuresLib.Basic;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Images;

public sealed class GrVisualStoredImage3D :
    GrVisualImage3D
{
    public Image StoredImage { get; }

    

    public GrVisualStoredImage3D(string name, Image storedImage) 
        : base(name)
    {
        StoredImage = storedImage;
    }


    public override Pair<int> GetSize()
    {
        return new Pair<int>(StoredImage.Width, StoredImage.Height);
    }

    public override Image GetImage()
    {
        return StoredImage;
    }
}