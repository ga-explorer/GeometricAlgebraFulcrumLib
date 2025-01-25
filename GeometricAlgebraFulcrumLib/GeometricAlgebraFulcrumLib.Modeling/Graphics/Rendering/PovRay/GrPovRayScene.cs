using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Cameras;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Finishes;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Textures;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Atmospheric;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay;

/// <summary>
/// This class represents a single POV-Ray SDL script file to be rendered
/// https://www.povray.org/documentation/3.7.0/r3_4.html
/// </summary>
public class GrPovRayScene :
    IGrPovRayCodeElement
{
    public string SceneName { get; }

    public GrPovRayRenderingOptions RenderingOptions { get; }

    public HashSet<string> IncludeFileNames { get; } 
        = new HashSet<string>();

    public GrPovRayFullCamera Camera { get; set; } 
        = GrPovRayCamera.Perspective();

    public GrPovRayStatementList Statements { get; } 
        = new GrPovRayStatementList();
    

    public GrPovRayScene(string sceneName)
    {
        SceneName = sceneName;
        RenderingOptions = new GrPovRayRenderingOptions();
    }
    
    public GrPovRayScene(string sceneName, GrPovRayRenderingOptions renderingOptions)
    {
        SceneName = sceneName;
        RenderingOptions = new GrPovRayRenderingOptions(renderingOptions);
    }


    public GrPovRayScene FreeCode(string code)
    {
        Statements.FreeCode(code);

        return this;
    }
    
    public GrPovRayScene AtmosphericBackground(GrPovRayColorValue color)
    {
        Statements.AtmosphericBackground(color);

        return this;
    }
    
    public GrPovRayAtmosphericSkySphere AtmosphericSkySphere(IGrPovRayPigment pigment, GrPovRayColorValue? emissionColor = null)
    {
        return Statements.AtmosphericSkySphere(pigment, emissionColor);
    }

    public GrPovRayScene IncludeGlobal(string fileName)
    {
        IncludeFileNames.Add(fileName);

        return this;
    }

    public GrPovRayScene IncludeGlobal(params string[] fileNameList)
    {
        foreach (var fileName in fileNameList)
            IncludeFileNames.Add(fileName);

        return this;
    }

    public GrPovRayScene Include(string fileName)
    {
        Statements.Include(fileName);

        return this;
    }

    public GrPovRayScene Include(params string[] fileNameList)
    {
        Statements.Include(fileNameList);

        return this;
    }
    
    public GrPovRayScene Default(IGrPovRayPigment pigment)
    {
        Statements.Default(pigment);

        return this;
    }
    
    public GrPovRayScene Default(IGrPovRayFinish finish)
    {
        Statements.Default(finish);

        return this;
    }

    public GrPovRayScene Default(IGrPovRayTexture texture)
    {
        Statements.Default(texture);

        return this;
    }

    public GrPovRayScene GlobalSettings(GrPovRayGlobalSettingsProperties properties)
    {
        Statements.Append(
            new GrPovRayGlobalSettings().SetProperties(properties)
        );

        return this;
    }

    
    public GrPovRayScene AddStatement(IGrPovRayStatement statement)
    {
        Statements.Add(statement);

        return this;
    }
    
    public GrPovRayScene AppendStatement(IGrPovRayStatement statement)
    {
        Statements.Add(statement);

        return this;
    }
    
    public GrPovRayScene PrependStatement(IGrPovRayStatement statement)
    {
        Statements.Insert(0, statement);

        return this;
    }
    
    public GrPovRayScene InsertStatement(int index, IGrPovRayStatement statement)
    {
        Statements.Insert(index, statement);

        return this;
    }


    public bool IsEmptyCodeElement()
    {
        return Statements.IsEmptyCodeElement();
    }

    public string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLineAtNewLine("#version 3.7;")
            .AppendLineAtNewLine();

        if (IncludeFileNames.Count > 0)
        {
            foreach (var fileName in IncludeFileNames)
                composer.AppendAtNewLine(
                    $"#include \"{fileName}.inc\""
                );

            composer.AppendLineAtNewLine();
        }

        if (!Camera.IsEmptyCodeElement())
        {
            composer
                .AppendLineAtNewLine(Camera.GetPovRayCode())
                .AppendLineAtNewLine();
        }

        composer.AppendAtNewLine(
            Statements.GetPovRayCode()
        );

        return composer.ToString();
    }
}