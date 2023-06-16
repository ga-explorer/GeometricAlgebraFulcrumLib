using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateIcoSphere-2
/// </summary>
public sealed class GrBabylonJsIcoSphere :
    GrBabylonJsMesh
{
    public sealed class IcoSphereOptions :
        GrBabylonJsObjectOptions
    {
        public GrBabylonJsBooleanValue? Flat { get; init; }
        
        public GrBabylonJsFloat32Value? Radius { get; init; }

        public GrBabylonJsFloat32Value? RadiusX { get; init; }

        public GrBabylonJsFloat32Value? RadiusY { get; init; }
    
        public GrBabylonJsFloat32Value? RadiusZ { get; init; }
    
        public GrBabylonJsInt32Value? Subdivisions { get; init; }
    
        public GrBabylonJsMeshOrientationValue? SideOrientation { get; init; }

        public GrBabylonJsVector4Value? FrontUVs { get; init; }

        public GrBabylonJsVector4Value? BackUVs { get; init; }

        public GrBabylonJsBooleanValue? Updateable { get; init; }

        
        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            yield return Flat.GetNameValueCodePair("flat");
            yield return Radius.GetNameValueCodePair("radius");
            yield return RadiusX.GetNameValueCodePair("radiusX");
            yield return RadiusY.GetNameValueCodePair("radiusY");
            yield return RadiusZ.GetNameValueCodePair("radiusZ");
            yield return Subdivisions.GetNameValueCodePair("subdivisions");
            yield return SideOrientation.GetNameValueCodePair("sideOrientation");
            yield return FrontUVs.GetNameValueCodePair("frontUVs");
            yield return BackUVs.GetNameValueCodePair("backUVs");
            yield return Updateable.GetNameValueCodePair("updatable");
        }
    }
    
    
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateIcoSphere";

    public IcoSphereOptions? Options { get; private set; }
        = new IcoSphereOptions();

    public override GrBabylonJsObjectOptions? ObjectOptions 
        => Options;


    public GrBabylonJsIcoSphere(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsIcoSphere(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsIcoSphere SetOptions(IcoSphereOptions options)
    {
        Options = options;

        return this;
    }

    public GrBabylonJsIcoSphere SetProperties(MeshProperties properties)
    {
        Properties = properties;

        return this;
    }
}