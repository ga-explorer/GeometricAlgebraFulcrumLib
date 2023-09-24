using TextComposerLib;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.InstancedMesh
/// </summary>
public sealed class GrBabylonJsInstancedMesh :
    GrBabylonJsMesh
{
    protected override string ConstructorName
        => "BABYLON.InstancedMesh";
        
    public override GrBabylonJsObjectOptions? ObjectOptions 
        => null;

    public GrBabylonJsMesh SourceMesh { get; }


    public GrBabylonJsInstancedMesh(string constName, GrBabylonJsMesh sourceMesh) 
        : base(constName)
    {
        SourceMesh = sourceMesh;
    }

        
    public GrBabylonJsInstancedMesh SetProperties(MeshProperties properties)
    {
        Properties = properties;

        return this;
    }

    
    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();
        
        yield return SourceMesh.ConstName;
    }
}