using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay;

public class GrPovRayGlobalSettings : 
    IGrPovRayStatement
{
    public GrPovRayGlobalSettingsProperties Properties { get; private set; }
        = new GrPovRayGlobalSettingsProperties();

    
    internal GrPovRayGlobalSettings()
    {
    }


    public GrPovRayGlobalSettings SetProperties(GrPovRayGlobalSettingsProperties properties)
    {
        Properties = new GrPovRayGlobalSettingsProperties(properties);

        return this;
    }

    public bool IsEmptyCodeElement()
    {
        return Properties.IsNullOrEmpty();
    }

    public string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("global_settings {")
            .IncreaseIndentation()
            .AppendAtNewLine(Properties.GetPovRayCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }

    public override string ToString()
    {
        return GetPovRayCode();
    }
}