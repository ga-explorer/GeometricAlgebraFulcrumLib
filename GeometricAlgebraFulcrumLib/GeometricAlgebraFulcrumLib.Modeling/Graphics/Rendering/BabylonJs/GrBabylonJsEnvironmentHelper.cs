namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs;

/// <summary>
/// https://doc.babylonjs.com/typedoc/interfaces/BABYLON.IEnvironmentHelperOptions
/// </summary>
public sealed class GrBabylonJsEnvironmentHelper :
    GrBabylonJsObject
{
    public GrBabylonJsScene ParentScene { get; set; }

    protected override string ConstructorName
        => $"{ParentScene.ConstName}.createDefaultEnvironment";

    public GrBabylonJsEnvironmentHelperOptions Options { get; private set; }
        = new GrBabylonJsEnvironmentHelperOptions();

    public GrBabylonJsEnvironmentHelperProperties Properties { get; private set; }
        = new GrBabylonJsEnvironmentHelperProperties();

    public override GrBabylonJsObjectOptions ObjectOptions
        => Options;

    public override GrBabylonJsObjectProperties ObjectProperties
        => Properties;


    public GrBabylonJsEnvironmentHelper(string constName, GrBabylonJsScene parentScene)
        : base(constName)
    {
        ParentScene = parentScene;
    }


    public GrBabylonJsEnvironmentHelper SetOptions(GrBabylonJsEnvironmentHelperOptions options)
    {
        Options = new GrBabylonJsEnvironmentHelperOptions(options);

        return this;
    }

    public GrBabylonJsEnvironmentHelper SetProperties(GrBabylonJsEnvironmentHelperProperties properties)
    {
        Properties = new GrBabylonJsEnvironmentHelperProperties(properties);

        return this;
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        if (Options.Count == 0) yield break;
        yield return Options.GetAttributeSetCode();
    }
}