using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using GeometricAlgebraFulcrumLib.Utilities.Web.Images;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space2D.Images;

public sealed class GrVisualLaTeXText2D :
    GrVisualImage2D
{
    public static GrVisualLaTeXText2D Create(string name, IGrVisualImageSource texture, ILinFloat64Vector2D position, double scalingFactor)
    {
        return new GrVisualLaTeXText2D(
            name, 
            texture, 
            position, 
            scalingFactor
        );
    }


    public IGrVisualImageSource Texture { get; }

    public double ScalingFactor { get; }

    public ILinFloat64Vector2D Position { get; }
        

    private GrVisualLaTeXText2D(string name, IGrVisualImageSource texture, ILinFloat64Vector2D position, double scalingFactor) 
        : base(name)
    {
        Texture = texture;
        Position = position;
        ScalingFactor = scalingFactor;
    }
    

    public override Pair<int> GetSize()
    {
        return new Pair<int>(Texture.ImageWidth, Texture.ImageHeight);
    }
    
    public override Image GetImage()
    {
        return Texture.GetImage();
    }
        
    public override bool IsValid()
    {
        return Position.IsValid();
    }
        
}