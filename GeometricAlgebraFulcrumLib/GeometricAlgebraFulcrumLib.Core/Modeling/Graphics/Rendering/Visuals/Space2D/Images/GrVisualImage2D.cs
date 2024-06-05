using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space2D.Images;

public abstract class GrVisualImage2D :
    GrVisualElement2D, 
    IGrVisualImage2D
{
    protected GrVisualImage2D(string name) 
        : base(name)
    {
    }


    public abstract Pair<int> GetSize();

    public abstract Image GetImage();
        
    public void SaveImage(string filePath)
    {
        var image = GetImage();

        image.Save(filePath);
    }

    public void SaveImage(string filePath, IImageEncoder format)
    {
        var image = GetImage();

        image.Save(filePath, format);
    }
}