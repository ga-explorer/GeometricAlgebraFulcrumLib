﻿using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Images;

public abstract class GrVisualImageWithAnimation3D :
    GrVisualElementWithAnimation3D, 
    IGrVisualImage3D
{
    protected GrVisualImageWithAnimation3D(string name, GrVisualAnimationSpecs animationSpecs) 
        : base(name, animationSpecs)
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