using System.Collections.Generic;
using GraphicsComposerLib.POVRay.SDL.Objects;
using GraphicsComposerLib.POVRay.SDL.Transforms;
using GraphicsComposerLib.POVRay.SDL.Values;

namespace GraphicsComposerLib.POVRay.SDL.Lights
{
    public class SdlLight : ISdlLight
    {
        public SdlSpotLightSpecs SpotLightSpecs { get; private set; }

        public SdlAreaLightSpecs AreaLightSpecs { get; private set; }


        public bool IsPointLight => IsAreaLight == false && IsSpotLight == false;

        public bool IsSpotLight => SpotLightSpecs != null;

        public bool IsAreaLight => AreaLightSpecs != null;

        public bool IsConicSpotLight => ReferenceEquals(SpotLightSpecs, null) == false 
                                        && SpotLightSpecs.Shape == SdlSpotLightShape.Conic;

        public bool IsCylindricalSpotLight => ReferenceEquals(SpotLightSpecs, null) == false
                                              && SpotLightSpecs.Shape == SdlSpotLightShape.Cylindrical;


        public ISdlVectorValue Location { get; set; }

        public ISdlColorValue Color { get; set; }

        public ISdlObject LooksLike { get; set; }

        public ISdlObject ProjectedThrough { get; set; }

        public ISdlScalarValue FadeDistance { get; set; }

        public ISdlScalarValue FadePower { get; set; }

        public ISdlBooleanValue MediaAttenuation { get; set; }

        public ISdlBooleanValue MediaInteraction { get; set; }

        public ISdlVectorValue ParallelPintAt { get; set; }

        public bool Parallel => ParallelPintAt != null;

        public bool Shadowless { get; set; }

        public List<ISdlTransform> Transforms { get; }


        internal SdlLight()
        {
            Transforms = new List<ISdlTransform>();
        }


        public SdlLight SetSpotLightSpecs(SdlSpotLightSpecs specs = null)
        {
            SpotLightSpecs = specs;
            return this;
        }

        public SdlLight SetAreaLightSpecs(SdlAreaLightSpecs specs = null)
        {
            AreaLightSpecs = specs;
            return this;
        }
    }
}
