using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateCylinder-2
/// </summary>
public sealed class GrBabylonJsCylinder :
    GrBabylonJsMesh
{
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateCylinder";

    public GrBabylonJsCylinderOptions Options { get; private set; }
        = new GrBabylonJsCylinderOptions();

    public override GrBabylonJsObjectOptions ObjectOptions
        => Options;


    public GrBabylonJsCylinder(string constName)
        : base(constName)
    {
    }

    public GrBabylonJsCylinder(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
    }


    public GrBabylonJsCylinder SetOptions(GrBabylonJsCylinderOptions options)
    {
        Options = new GrBabylonJsCylinderOptions(options);

        return this;
    }

    public GrBabylonJsCylinder SetProperties(GrBabylonJsMeshProperties properties)
    {
        Properties = new GrBabylonJsMeshProperties(properties);

        return this;
    }
}