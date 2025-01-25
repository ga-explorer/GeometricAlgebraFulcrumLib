using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Finishes;

/// <summary>
/// 
/// </summary>
public class GrPovRayFinish :
    IGrPovRayFinish
{
    public static GrPovRayFinish Dull { get; }
        = new GrPovRayFinish("Dull");

    public static GrPovRayFinish Shiny { get; }
        = new GrPovRayFinish("Shiny");
    
    public static GrPovRayFinish Glossy { get; }
        = new GrPovRayFinish("Glossy");

    public static GrPovRayFinish Luminous { get; }
        = new GrPovRayFinish("Luminous");
    
    public static GrPovRayFinish Mirror { get; }
        = new GrPovRayFinish("Mirror");
    
    public static GrPovRayFinish PhongDull { get; }
        = new GrPovRayFinish("Phong_Dull");
    
    public static GrPovRayFinish PhongShiny { get; }
        = new GrPovRayFinish("Phong_Shiny");
    
    public static GrPovRayFinish PhongGlossy { get; }
        = new GrPovRayFinish("Phong_Glossy");


    public string BaseFinishIdentifier { get; }

    public GrPovRayFinishProperties Properties { get; private set; }
        = new GrPovRayFinishProperties();


    internal GrPovRayFinish()
    {
        BaseFinishIdentifier = string.Empty;
    }

    internal GrPovRayFinish(string finishIdentifier)
    {
        BaseFinishIdentifier = finishIdentifier;
    }


    public GrPovRayFinish SetProperties(GrPovRayFinishProperties properties)
    {
        Properties = new GrPovRayFinishProperties(properties);

        return this;
    }


    public bool IsEmptyCodeElement()
    {
        return BaseFinishIdentifier.IsNullOrEmpty() &&
               Properties.IsNullOrEmpty();
    }

    public string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("finish {")
            .IncreaseIndentation();

        if (!string.IsNullOrEmpty(BaseFinishIdentifier))
            composer.AppendAtNewLine(BaseFinishIdentifier);

        composer
            .AppendAtNewLine(Properties.GetPovRayCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
}