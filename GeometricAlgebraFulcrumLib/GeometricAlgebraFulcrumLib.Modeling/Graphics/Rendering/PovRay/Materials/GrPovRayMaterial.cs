using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Finishes;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Interiors;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Textures;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials;

public sealed class GrPovRayMaterial :
    IGrPovRayCodeElement,
    IGrVisualElementMaterial3D
{
    public static GrPovRayMaterial Create()
    {
        var material = new GrPovRayMaterial();

        return material;
    }
    
    public static GrPovRayMaterial Create(string baseMaterialName)
    {
        var material = new GrPovRayMaterial(baseMaterialName);

        return material;
    }


    public static GrPovRayMaterial Create(GrPovRayColorValue pigmentColor)
    {
        var material = new GrPovRayMaterial();

        return material.SetPigment(pigmentColor);
    }
    
    public static GrPovRayMaterial Create(GrPovRayColorValue pigmentColor, IGrPovRayFinish finish)
    {
        var material = new GrPovRayMaterial();

        return material.SetPigment(pigmentColor).SetFinish(finish);
    }
    
    public static GrPovRayMaterial Create(GrPovRayColorValue pigmentColor, GrPovRayFinishProperties finishProperties)
    {
        var material = new GrPovRayMaterial();

        return material.SetPigment(pigmentColor).SetFinish(finishProperties);
    }
    
    public static GrPovRayMaterial Create(GrPovRayColorValue pigmentColor, string baseFinishName, GrPovRayFinishProperties? finishProperties = null)
    {
        var material = new GrPovRayMaterial();

        return material
            .SetPigment(pigmentColor)
            .SetFinish(baseFinishName, finishProperties);
    }
    

    public static GrPovRayMaterial Create(string baseMaterialName, GrPovRayColorValue pigmentColor)
    {
        var material = new GrPovRayMaterial(baseMaterialName);

        return material.SetPigment(pigmentColor);
    }
    
    public static GrPovRayMaterial Create(string baseMaterialName, GrPovRayColorValue pigmentColor, IGrPovRayFinish finish)
    {
        var material = new GrPovRayMaterial(baseMaterialName);

        return material.SetPigment(pigmentColor).SetFinish(finish);
    }
    
    public static GrPovRayMaterial Create(string baseMaterialName, GrPovRayColorValue pigmentColor, GrPovRayFinishProperties finishProperties)
    {
        var material = new GrPovRayMaterial(baseMaterialName);

        return material.SetPigment(pigmentColor).SetFinish(finishProperties);
    }
    
    public static GrPovRayMaterial Create(string baseMaterialName, GrPovRayColorValue pigmentColor, string baseFinishName, GrPovRayFinishProperties? finishProperties = null)
    {
        var material = new GrPovRayMaterial(baseMaterialName);

        return material
            .SetPigment(pigmentColor)
            .SetFinish(baseFinishName, finishProperties);
    }
    

    public static GrPovRayMaterial Create(IGrPovRayPigment pigment)
    {
        var material = new GrPovRayMaterial();

        return material.SetPigment(pigment);
    }
    
    public static GrPovRayMaterial Create(IGrPovRayPigment pigment, IGrPovRayFinish finish)
    {
        var material = new GrPovRayMaterial();

        return material.SetPigment(pigment).SetFinish(finish);
    }
    
    public static GrPovRayMaterial Create(IGrPovRayPigment pigment, GrPovRayFinishProperties finishProperties)
    {
        var material = new GrPovRayMaterial();

        return material.SetPigment(pigment).SetFinish(finishProperties);
    }
    
    public static GrPovRayMaterial Create(IGrPovRayPigment pigment, string baseFinishName, GrPovRayFinishProperties? finishProperties = null)
    {
        var material = new GrPovRayMaterial();

        return material
            .SetPigment(pigment)
            .SetFinish(baseFinishName, finishProperties);
    }
    

    public static GrPovRayMaterial Create(string baseMaterialName, IGrPovRayPigment pigment)
    {
        var material = new GrPovRayMaterial(baseMaterialName);

        return material.SetPigment(pigment);
    }
    
    public static GrPovRayMaterial Create(string baseMaterialName, IGrPovRayPigment pigment, IGrPovRayFinish finish)
    {
        var material = new GrPovRayMaterial(baseMaterialName);

        return material.SetPigment(pigment).SetFinish(finish);
    }
    
    public static GrPovRayMaterial Create(string baseMaterialName, IGrPovRayPigment pigment, GrPovRayFinishProperties finishProperties)
    {
        var material = new GrPovRayMaterial(baseMaterialName);

        return material.SetPigment(pigment).SetFinish(finishProperties);
    }
    
    public static GrPovRayMaterial Create(string baseMaterialName, IGrPovRayPigment pigment, string baseFinishName, GrPovRayFinishProperties? finishProperties = null)
    {
        var material = new GrPovRayMaterial(baseMaterialName);

        return material
            .SetPigment(pigment)
            .SetFinish(baseFinishName, finishProperties);
    }


    public string BaseMaterialName { get; }

    public string MaterialName 
        => BaseMaterialName;

    public IGrPovRayTexture? Texture { get; set; }
        = new GrPovRayPlainTexture();

    public IGrPovRayTexture? InteriorTexture { get; set; }
        = new GrPovRayPlainTexture();

    public GrPovRayInterior? Interior { get; set; } 
        = new GrPovRayInterior();

    public GrPovRayTransformList Transforms { get; }
        = new GrPovRayTransformList();

    
    private GrPovRayMaterial()
    {
        BaseMaterialName = string.Empty;
    }

    private GrPovRayMaterial(string baseMaterialName)
    {
        BaseMaterialName = baseMaterialName;
    }

    
    public GrPovRayMaterial SetPigment(GrPovRayColorValue color)
    {
        if (Texture is not GrPovRayPlainTexture)
            Texture = new GrPovRayPlainTexture();

        ((GrPovRayPlainTexture)Texture).Pigment = new GrPovRaySolidColorPigment(color);

        return this;
    }

    public GrPovRayMaterial SetPigment(IGrPovRayPigment pigment)
    {
        if (Texture is not GrPovRayPlainTexture)
            Texture = new GrPovRayPlainTexture();

        ((GrPovRayPlainTexture)Texture).Pigment = pigment;

        return this;
    }

    public GrPovRayMaterial SetFinish(IGrPovRayFinish finish)
    {
        if (Texture is not GrPovRayPlainTexture)
            Texture = new GrPovRayPlainTexture();

        ((GrPovRayPlainTexture)Texture).Finish = finish;

        return this;
    }

    public GrPovRayMaterial SetFinish(GrPovRayFinishProperties properties)
    {
        if (Texture is not GrPovRayPlainTexture)
            Texture = new GrPovRayPlainTexture();

        ((GrPovRayPlainTexture)Texture).Finish = new GrPovRayFinish().SetProperties(properties);

        return this;
    }

    public GrPovRayMaterial SetFinish(string baseFinishName, GrPovRayFinishProperties? properties)
    {
        if (Texture is not GrPovRayPlainTexture)
            Texture = new GrPovRayPlainTexture();

        var finish = new GrPovRayFinish(baseFinishName);
            
        if (properties is not null)
            finish.SetProperties(properties);

        ((GrPovRayPlainTexture)Texture).Finish = finish;

        return this;
    }

    // TODO: Add more modifiers


    public bool IsEmptyCodeElement()
    {
        return BaseMaterialName.IsNullOrEmpty() &&
               Texture.IsNullOrEmpty() &&
               InteriorTexture.IsNullOrEmpty() &&
               Interior.IsNullOrEmpty() &&
               Transforms.IsNullOrEmpty();
    }

    public string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("material {")
            .IncreaseIndentation();

        if (!BaseMaterialName.IsNullOrEmpty())
            composer.AppendAtNewLine(BaseMaterialName);

        if (Texture is not null && !Texture.IsEmptyCodeElement())
            composer.AppendAtNewLine(Texture.GetPovRayCode(false));

        if (InteriorTexture is not null && !InteriorTexture.IsEmptyCodeElement())
            composer.AppendAtNewLine(InteriorTexture.GetPovRayCode(true));
        
        if (Interior is not null && !Interior.IsEmptyCodeElement())
            composer.AppendAtNewLine(Interior.GetPovRayCode());

        if (!Transforms.IsEmptyCodeElement())
            composer.AppendAtNewLine(Transforms.GetPovRayCode());

        composer
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }

    public override string ToString()
    {
        return GetPovRayCode();
    }

}