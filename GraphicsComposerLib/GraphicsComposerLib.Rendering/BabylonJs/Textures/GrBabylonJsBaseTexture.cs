using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Textures
{
    public abstract class GrBabylonJsBaseTexture :
        GrBabylonJsObject
    {
        public abstract class BaseTextureProperties :
            GrBabylonJsObjectProperties
        {
            public GrBabylonJsInt32Value? AnisotropicFilteringLevel { get; set; }

            public GrBabylonJsBooleanValue? HomogeneousRotationInUvTransform { get; set; }

            public GrBabylonJsBooleanValue? InvertY { get; set; }

            public GrBabylonJsBooleanValue? InvertZ { get; set; }

            public GrBabylonJsBooleanValue? IsRenderTarget { get; set; }
        
            public GrBabylonJsBooleanValue? CanRescale { get; set; }

            public GrBabylonJsBooleanValue? IsBlocking { get; set; }

            public GrBabylonJsBooleanValue? IsRgbd { get; set; }

            public GrBabylonJsBooleanValue? LinearSpecularLod { get; set; }

            public GrBabylonJsBooleanValue? HasAlpha { get; set; }

            public GrBabylonJsBooleanValue? GetAlphaFromRgb { get; set; }
        
            public GrBabylonJsBooleanValue? Is2DArray { get; set; }
        
            public GrBabylonJsBooleanValue? Is3D { get; set; }

            public GrBabylonJsBooleanValue? IsCube { get; set; }

            public GrBabylonJsFloat32Value? Level { get; set; }

            public GrBabylonJsFloat32Value? UAng { get; set; }
        
            public GrBabylonJsFloat32Value? VAng { get; set; }
        
            public GrBabylonJsFloat32Value? WAng { get; set; }

            public GrBabylonJsFloat32Value? UOffset { get; set; }
        
            public GrBabylonJsFloat32Value? VOffset { get; set; }

            public GrBabylonJsFloat32Value? URotationCenter { get; set; }
    
            public GrBabylonJsFloat32Value? VRotationCenter { get; set; }
        
            public GrBabylonJsFloat32Value? WRotationCenter { get; set; }

            public GrBabylonJsFloat32Value? UScale { get; set; }
        
            public GrBabylonJsFloat32Value? VScale { get; set; }

            public GrBabylonJsFloat32Value? LodGenerationOffset { get; set; }

            public GrBabylonJsFloat32Value? LodGenerationScale { get; set; }
        
            public GrBabylonJsTextureWrapModeValue? WrapU { get; set; }

            public GrBabylonJsTextureWrapModeValue? WrapV { get; set; }

            public GrBabylonJsTextureWrapModeValue? WrapR { get; set; }
        
            public GrBabylonJsStringValue? Url { get; set; }
    
            public GrBabylonJsTextureCoordinatesModeValue? CoordinatesMode { get; set; }
        

            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                yield return AnisotropicFilteringLevel.GetNameValueCodePair("anisotropicFilteringLevel");
                yield return HomogeneousRotationInUvTransform.GetNameValueCodePair("homogeneousRotationInUVTransform");
                yield return InvertY.GetNameValueCodePair("invertY");
                yield return InvertZ.GetNameValueCodePair("invertZ");
                yield return IsRenderTarget.GetNameValueCodePair("isRenderTarget");
                yield return CanRescale.GetNameValueCodePair("canRescale");
                yield return IsBlocking.GetNameValueCodePair("isBlocking");
                yield return IsRgbd.GetNameValueCodePair("isRGBD");
                yield return LinearSpecularLod.GetNameValueCodePair("linearSpecularLOD");
                yield return HasAlpha.GetNameValueCodePair("hasAlpha");
                yield return GetAlphaFromRgb.GetNameValueCodePair("getAlphaFromRGB");
                yield return Is2DArray.GetNameValueCodePair("is2DArray");
                yield return Is3D.GetNameValueCodePair("is3D");
                yield return IsCube.GetNameValueCodePair("isCube");
                yield return Level.GetNameValueCodePair("level");
                yield return UAng.GetNameValueCodePair("uAng");
                yield return VAng.GetNameValueCodePair("vAng");
                yield return WAng.GetNameValueCodePair("wAng");
                yield return UOffset.GetNameValueCodePair("uOffset");
                yield return VOffset.GetNameValueCodePair("vOffset");
                yield return URotationCenter.GetNameValueCodePair("uRotationCenter");
                yield return VRotationCenter.GetNameValueCodePair("vRotationCenter");
                yield return WRotationCenter.GetNameValueCodePair("wRotationCenter");
                yield return UScale.GetNameValueCodePair("uScale");
                yield return VScale.GetNameValueCodePair("vScale");
                yield return LodGenerationOffset.GetNameValueCodePair("lodGenerationOffset");
                yield return LodGenerationScale.GetNameValueCodePair("lodGenerationScale");
                yield return WrapU.GetNameValueCodePair("wrapU");
                yield return WrapV.GetNameValueCodePair("wrapV");
                yield return WrapR.GetNameValueCodePair("wrapR");
                yield return Url.GetNameValueCodePair("url");
                yield return CoordinatesMode.GetNameValueCodePair("coordinatesMode");
            }
        }

    
        public GrBabylonJsSceneValue? ParentScene { get; set; }

        public string SceneVariableName 
            => ParentScene?.Value.ConstName ?? string.Empty;
    
        public override GrBabylonJsObjectOptions? ObjectOptions 
            => null;

    
        protected GrBabylonJsBaseTexture(string constName) 
            : base(constName)
        {
        }

        protected GrBabylonJsBaseTexture(string constName, GrBabylonJsSceneValue scene) 
            : base(constName)
        {
            ParentScene = scene;
        }
    }
}