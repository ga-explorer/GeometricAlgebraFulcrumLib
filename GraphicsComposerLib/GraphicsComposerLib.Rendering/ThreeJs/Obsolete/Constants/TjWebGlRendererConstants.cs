namespace GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Constants
{
    public static class TjWebGlRendererConstants
    {
        public enum CullFaceModes
        {
            CullFaceNone,
            CullFaceBack,
            CullFaceFront,
            CullFaceFrontBack
        }

        public enum ShadowTypes
        {
            BasicShadowMap,
            PcfShadowMap,
            PcfSoftShadowMap,
            VsmShadowMap
        }

        public enum ToneMapping
        {
            NoToneMapping,
            LinearToneMapping,
            ReinhardToneMapping,
            CineonToneMapping,
            AcesFilmicToneMapping
        }

        
        public static string GetName(this CullFaceModes value)
        {
            return value switch
            {
                CullFaceModes.CullFaceNone => "THREE.CullFaceNone",
                CullFaceModes.CullFaceBack => "THREE.CullFaceBack",
                CullFaceModes.CullFaceFront => "THREE.CullFaceFront",
                CullFaceModes.CullFaceFrontBack => "THREE.CullFaceFrontBack",
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }
        
        public static string GetName(this ShadowTypes value)
        {
            return value switch
            {
                ShadowTypes.BasicShadowMap => "THREE.BasicShadowMap",
                ShadowTypes.PcfShadowMap => "THREE.PCFShadowMap",
                ShadowTypes.PcfSoftShadowMap => "THREE.PCFSoftShadowMap",
                ShadowTypes.VsmShadowMap => "THREE.VSMShadowMap",
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }
        
        public static string GetName(this ToneMapping value)
        {
            return value switch
            {
                ToneMapping.NoToneMapping => "THREE.NoToneMapping",
                ToneMapping.LinearToneMapping => "THREE.LinearToneMapping",
                ToneMapping.ReinhardToneMapping => "THREE.ReinhardToneMapping",
                ToneMapping.CineonToneMapping => "THREE.CineonToneMapping",
                ToneMapping.AcesFilmicToneMapping => "THREE.ACESFilmicToneMapping",
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }
    }
}