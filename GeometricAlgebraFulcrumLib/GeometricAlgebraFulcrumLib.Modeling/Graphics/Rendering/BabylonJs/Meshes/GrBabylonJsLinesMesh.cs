using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

public abstract class GrBabylonJsLinesMesh :
    GrBabylonJsObject
{
    public GrBabylonJsSceneValue ParentScene { get; set; }

    public string SceneVariableName 
        => ParentScene.Value.ConstName;

    public GrBabylonJsLinesMeshProperties Properties { get; protected set; }
        = new GrBabylonJsLinesMeshProperties();

    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;
    

    protected GrBabylonJsLinesMesh(string constName) 
        : base(constName)
    {
        UseLetDeclaration = true;
    }
    
    protected GrBabylonJsLinesMesh(string constName, GrBabylonJsSceneValue scene) 
        : base(constName)
    {
        ParentScene = scene;
        UseLetDeclaration = true;
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();

        var optionsCode = 
            ObjectOptions is null 
                ? "{}" 
                : ObjectOptions.GetAttributeSetCode();

        yield return optionsCode;

        if (ParentScene.IsNullOrEmpty()) yield break;
        yield return ParentScene.Value.ConstName;
    }
}