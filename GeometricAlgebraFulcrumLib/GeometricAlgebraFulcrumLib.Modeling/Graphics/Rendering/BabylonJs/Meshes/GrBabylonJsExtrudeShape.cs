using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#ExtrudeShape-2
/// https://doc.babylonjs.com/features/featuresDeepDive/mesh/creation/param/extrude_shape
/// </summary>
public sealed class GrBabylonJsExtrudeShape :
    GrBabylonJsMesh
{
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.ExtrudeShape";

    public GrBabylonJsExtrudeShapeOptions Options { get; private set; }
        = new GrBabylonJsExtrudeShapeOptions();

    public override GrBabylonJsObjectOptions ObjectOptions
        => Options;


    public GrBabylonJsExtrudeShape(string constName)
        : base(constName)
    {
        UseLetDeclaration = true;
    }

    public GrBabylonJsExtrudeShape(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
        UseLetDeclaration = true;
    }


    public GrBabylonJsExtrudeShape SetOptions(GrBabylonJsExtrudeShapeOptions options)
    {
        Options = new GrBabylonJsExtrudeShapeOptions(options);

        return this;
    }

    public GrBabylonJsExtrudeShape SetProperties(GrBabylonJsMeshProperties properties)
    {
        Properties = new GrBabylonJsMeshProperties(properties);

        return this;
    }
}