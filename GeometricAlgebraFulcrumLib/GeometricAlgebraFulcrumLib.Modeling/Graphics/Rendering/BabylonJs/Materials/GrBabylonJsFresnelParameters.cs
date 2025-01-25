namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;

public sealed class GrBabylonJsFresnelParameters :
    GrBabylonJsObject
{
    protected override string ConstructorName
        => "new BABYLON.FresnelParameters";

    public GrBabylonJsFresnelParametersOptions Options { get; private set; }
        = new GrBabylonJsFresnelParametersOptions();

    public GrBabylonJsFresnelParametersProperties Properties { get; private set; }
        = new GrBabylonJsFresnelParametersProperties();

    public override GrBabylonJsObjectOptions ObjectOptions 
        => Options;

    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;
    
    
    public GrBabylonJsFresnelParameters(string constName) 
        : base(constName)
    {
    }


    public GrBabylonJsFresnelParameters SetOptions(GrBabylonJsFresnelParametersOptions options)
    {
        Options = new GrBabylonJsFresnelParametersOptions(options);

        return this;
    }

    public GrBabylonJsFresnelParameters SetProperties(GrBabylonJsFresnelParametersProperties properties)
    {
        Properties = new GrBabylonJsFresnelParametersProperties(properties);

        return this;
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        var optionsCode = 
            ObjectOptions.Count == 0
                ? "{}" 
                : ObjectOptions.GetAttributeSetCode();

        yield return optionsCode;
    }
}