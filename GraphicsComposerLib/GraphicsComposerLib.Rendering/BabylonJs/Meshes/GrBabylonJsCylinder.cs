using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateCylinder-2
/// </summary>
public sealed class GrBabylonJsCylinder :
    GrBabylonJsMesh
{
    public sealed class CylinderOptions :
        GrBabylonJsObjectOptions
    {
        public GrBabylonJsFloat32Value? Arc { get; set; }

        public GrBabylonJsFloat32Value? Diameter { get; set; }

        public GrBabylonJsFloat32Value? DiameterBottom { get; set; }

        public GrBabylonJsFloat32Value? DiameterTop { get; set; }
        
        public GrBabylonJsFloat32Value? Height { get; set; }
        
        public GrBabylonJsInt32Value? Subdivisions { get; set; }

        public GrBabylonJsInt32Value? Tessellation { get; set; }
        
        public GrBabylonJsMeshCapValue? Cap { get; set; }

        public GrBabylonJsColor4ArrayValue? FaceColors { get; set; }

        public GrBabylonJsVector4ArrayValue? FaceUVs { get; set; }

        public GrBabylonJsMeshOrientationValue? SideOrientation { get; set; }

        public GrBabylonJsVector4Value? FrontUVs { get; set; }

        public GrBabylonJsVector4Value? BackUVs { get; set; }

        public GrBabylonJsBooleanValue? Enclose { get; set; }

        public GrBabylonJsBooleanValue? HasRings { get; set; }

        public GrBabylonJsBooleanValue? Updateable { get; set; }


        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            yield return Arc.GetNameValueCodePair("arc");
            yield return HasRings.GetNameValueCodePair("hasRings");
            yield return Enclose.GetNameValueCodePair("enclose");
            yield return Diameter.GetNameValueCodePair("diameter");
            yield return DiameterBottom.GetNameValueCodePair("diameterBottom");
            yield return DiameterTop.GetNameValueCodePair("diameterTop");
            yield return Height.GetNameValueCodePair("height");
            yield return Subdivisions.GetNameValueCodePair("subdivisions");
            yield return Tessellation.GetNameValueCodePair("tessellation");
            yield return Cap.GetNameValueCodePair("cap");
            yield return FaceColors.GetNameValueCodePair("faceColors");
            yield return FaceUVs.GetNameValueCodePair("faceUVs");
            yield return SideOrientation.GetNameValueCodePair("sideOrientation");
            yield return FrontUVs.GetNameValueCodePair("frontUVs");
            yield return BackUVs.GetNameValueCodePair("backUVs");
            yield return Updateable.GetNameValueCodePair("updateable");
        }
    }


    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateCylinder";

    public CylinderOptions? Options { get; private set; }
        = new CylinderOptions();

    public override GrBabylonJsObjectOptions? ObjectOptions 
        => Options;


    public GrBabylonJsCylinder(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsCylinder(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsCylinder SetOptions([NotNull] CylinderOptions? options)
    {
        Options = options;

        return this;
    }

    public GrBabylonJsCylinder SetProperties([NotNull] MeshProperties? properties)
    {
        Properties = properties;

        return this;
    }
}