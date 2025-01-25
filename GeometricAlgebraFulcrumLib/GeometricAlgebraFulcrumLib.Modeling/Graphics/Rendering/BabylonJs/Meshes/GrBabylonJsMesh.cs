using System.Text;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

public abstract class GrBabylonJsMesh :
    GrBabylonJsObject
{
    public GrBabylonJsSceneValue ParentScene { get; set; }

    public string SceneVariableName
        => ParentScene.Value.ConstName;

    public GrBabylonJsMeshProperties Properties { get; protected set; }
        = new GrBabylonJsMeshProperties();

    public override GrBabylonJsObjectProperties ObjectProperties
        => Properties;

    public IFloat64AffineMap3D PreTransformMap { get; set; }
        = Float64IdentityAffineMap3D.Instance;


    protected GrBabylonJsMesh(string constName)
        : base(constName)
    {
    }

    protected GrBabylonJsMesh(string constName, GrBabylonJsSceneValue scene)
        : base(constName)
    {
        ParentScene = scene;
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

    public override string GetBabylonJsCode()
    {
        var composer = new StringBuilder();

        var constructorCode = GetConstructorCode();
        var propertiesCode = GetPropertiesCode();

        if (!string.IsNullOrEmpty(ConstName))
        {
            var declarationKeyword = UseLetDeclaration ? "let" : "const";

            composer.Append($"{declarationKeyword} {ConstName} = ");
        }

        composer
            .AppendLine(constructorCode)
            .AppendLine(propertiesCode);

        if (!string.IsNullOrEmpty(ConstName) && PreTransformMap is not Float64IdentityAffineMap3D)
        {
            var matrixCode =
                PreTransformMap.GetBabylonJsMatrixCode();

            composer.AppendLine(
                $"{ConstName}.setPreTransformMatrix({matrixCode});"
            );
        }

        return composer.ToString();
    }

}