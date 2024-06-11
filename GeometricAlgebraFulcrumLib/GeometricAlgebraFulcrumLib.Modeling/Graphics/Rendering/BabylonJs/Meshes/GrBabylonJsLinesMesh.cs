using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

public abstract class GrBabylonJsLinesMesh :
    GrBabylonJsObject
{
    public sealed class LinesMeshProperties :
        GrBabylonJsMesh.MeshProperties
    {
        public GrBabylonJsColor3Value? Color
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("color");
            set => SetAttributeValue("color", value);
        }

        public GrBabylonJsFloat32Value? Alpha
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("alpha");
            set => SetAttributeValue("alpha", value);
        }


        public LinesMeshProperties()
        {
        }

        public LinesMeshProperties(LinesMeshProperties properties)
        {
            SetAttributeValues(properties);
        }
    }


    public GrBabylonJsSceneValue ParentScene { get; set; }

    public string SceneVariableName 
        => ParentScene.Value.ConstName;

    public LinesMeshProperties Properties { get; protected set; }
        = new LinesMeshProperties();

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
                : ObjectOptions.GetCode();

        yield return optionsCode;

        if (ParentScene.IsNullOrEmpty()) yield break;
        yield return ParentScene.Value.ConstName;
    }
}