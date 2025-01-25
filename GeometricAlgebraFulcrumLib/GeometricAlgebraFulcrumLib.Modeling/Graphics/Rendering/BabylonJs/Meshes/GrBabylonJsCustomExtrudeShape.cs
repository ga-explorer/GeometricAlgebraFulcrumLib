using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#ExtrudeShapeCustom-2
/// https://doc.babylonjs.com/features/featuresDeepDive/mesh/creation/param/custom_extrude
/// </summary>
public sealed class GrBabylonJsCustomExtrudeShape :
    GrBabylonJsMesh
{
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.ExtrudeShapeCustom";

    public GrBabylonJsCustomExtrudeShapeOptions Options { get; private set; }
        = new GrBabylonJsCustomExtrudeShapeOptions();

    public override GrBabylonJsObjectOptions ObjectOptions
        => Options;


    public GrBabylonJsCustomExtrudeShape(string constName)
        : base(constName)
    {
        UseLetDeclaration = true;
    }

    public GrBabylonJsCustomExtrudeShape(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
        UseLetDeclaration = true;
    }


    public GrBabylonJsCustomExtrudeShape SetOptions(GrBabylonJsCustomExtrudeShapeOptions options)
    {
        Options = new GrBabylonJsCustomExtrudeShapeOptions(options);

        return this;
    }

    public GrBabylonJsCustomExtrudeShape SetProperties(GrBabylonJsMeshProperties properties)
    {
        Properties = new GrBabylonJsMeshProperties(properties);

        return this;
    }
}