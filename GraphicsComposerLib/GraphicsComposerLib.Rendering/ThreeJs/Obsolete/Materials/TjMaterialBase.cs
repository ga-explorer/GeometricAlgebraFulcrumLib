using GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Constants;
using GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Math;
using TextComposerLib.Code.JavaScript;
using TextComposerLib.Text;

namespace GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Materials
{
    public abstract class TjMaterialBase :
        TjComponentWithAttributes
    {
        public double AlphaTest { get; set; } = 0d;

        public TjMaterialConstants.BlendingMode BlendingMode { get; set; } 
            = TjMaterialConstants.BlendingMode.NormalBlending;

        public TjBlendingEquationConstants.DestinationFactors BlendingDestination { get; set; }
            = TjBlendingEquationConstants.DestinationFactors.OneMinusSrcAlphaFactor;

        public int? BlendingDestinationAlpha { get; set; } = null;

        public TjBlendingEquationConstants.BlendingEquations BlendingEquation { get; set; }
            = TjBlendingEquationConstants.BlendingEquations.AddEquation;

        public int? BlendingEquationAlpha { get; set; } = null;

        public TjBlendingEquationConstants.SourceFactors BlendingSource { get; set; }
            = TjBlendingEquationConstants.SourceFactors.SrcAlphaFactor;

        public int? BlendingSourceAlpha { get; set; } = null;

        public bool ClipIntersection { get; set; } = false;

        public List<TjPlane> ClippingPlanesList { get; }
            = new List<TjPlane>();

        public bool ClipShadows { get; set; } = false;

        public bool ColorWrite { get; set; } = true;

        public TjMaterialConstants.DepthMode DepthMode { get; set; }
            = TjMaterialConstants.DepthMode.LessEqualDepth;

        public bool DepthTest { get; set; } = true;

        public bool DepthWrite { get; set; } = true;

        public bool StencilWrite { get; set; } = false;

        public int StencilWriteMask { get; set; } = 255;

        public TjMaterialConstants.StencilFunctions StencilFunction { get; set; }
            = TjMaterialConstants.StencilFunctions.AlwaysStencilFunc;

        public int StencilRef { get; set; } = 0;

        public int StencilFunctionMask { get; set; } = 255;

        public TjMaterialConstants.StencilOperations StencilFailOperation { get; set; }
            = TjMaterialConstants.StencilOperations.KeepStencilOp;

        public TjMaterialConstants.StencilOperations StencilZFailOperation { get; set; }
            = TjMaterialConstants.StencilOperations.KeepStencilOp;

        public TjMaterialConstants.StencilOperations StencilZPassOperation { get; set; }
            = TjMaterialConstants.StencilOperations.KeepStencilOp;

        public bool AffectedByFog { get; set; } = true;

        public bool NeedsUpdate { get; set; } = false;

        public double Opacity { get; set; } = 1d;

        public bool PolygonOffset { get; set; } = false;

        public int PolygonOffsetFactor { get; set; } = 0;

        public int PolygonOffsetUnits { get; set; } = 0;

        public bool PreMultipliedAlpha { get; set; } = false;

        public bool Dithering { get; set; } = false;

        public TjMaterialConstants.RenderedSide RenderedSide { get; set; } 
            = TjMaterialConstants.RenderedSide.FrontSide;

        public TjMaterialConstants.RenderedSide? ShadowSide { get; set; } = null;

        public bool ToneMapped { get; set; } = true;

        public bool Transparent { get; set; } = false;

        public bool VertexColoring { get; set; } = false;

        public bool Visible { get; set; } = true;


        public override void UpdateComponentAttributes(JavaScriptAttributesDictionary attributesDictionary)
        {
            base.UpdateComponentAttributes(attributesDictionary);

            attributesDictionary
                .SetValue("alphaTest", AlphaTest, 0d)
                .SetValue("clipIntersection", ClipIntersection, false)
                .SetValue("clipShadows", ClipShadows, false)
                .SetValue("colorWrite", ColorWrite, true)
                .SetValue("depthTest", DepthTest, true)
                .SetValue("depthWrite", DepthWrite, true)
                .SetValue("stencilWrite", StencilWrite, false)
                .SetValue("stencilWriteMask", StencilWriteMask, 255)
                .SetValue("stencilRef", StencilRef, 0)
                .SetValue("stencilFuncMask", StencilFunctionMask, 255)
                .SetValue("fog", AffectedByFog, true)
                .SetValue("needsUpdate", NeedsUpdate, false)
                .SetValue("opacity", Opacity, 1d)
                .SetValue("polygonOffset", PolygonOffset, false)
                .SetValue("polygonOffsetFactor", PolygonOffsetFactor, 0)
                .SetValue("polygonOffsetUnits", PolygonOffsetUnits, 0)
                .SetValue("premultipliedAlpha", PreMultipliedAlpha, false)
                .SetValue("dithering", Dithering, false)
                .SetValue("toneMapped", ToneMapped, true)
                .SetValue("transparent", Transparent, false)
                .SetValue("vertexColors", VertexColoring, false)
                .SetValue("visible", Visible, true)
                .SetValue("transparent", Transparent, false)
                .SetTextValue(
                    "blending",
                    BlendingMode.GetName(),
                    TjMaterialConstants.BlendingMode.NormalBlending.GetName()
                )
                .SetTextValue(
                    "blendDst",
                    BlendingDestination.GetName(),
                    TjBlendingEquationConstants.DestinationFactors.OneMinusSrcAlphaFactor.GetName()
                ).SetTextValue(
                    "blending",
                    BlendingMode.GetName(),
                    TjMaterialConstants.BlendingMode.NormalBlending.GetName()
                )
                .SetTextValue(
                    "blendDstAlpha",
                    BlendingDestinationAlpha.HasValue
                        ? BlendingDestinationAlpha.Value.ToString()
                        : string.Empty,
                    string.Empty
                )
                .SetTextValue(
                    "blendEquation",
                    BlendingEquation.GetName(),
                    TjBlendingEquationConstants.BlendingEquations.AddEquation.GetName()
                )
                .SetTextValue(
                    "blendEquationAlpha",
                    BlendingEquationAlpha.HasValue
                        ? BlendingEquationAlpha.Value.ToString()
                        : string.Empty,
                    string.Empty
                )
                .SetTextValue(
                    "blendSrc",
                    BlendingSource.GetName(),
                    TjBlendingEquationConstants.SourceFactors.SrcAlphaFactor.GetName()
                )
                .SetTextValue(
                    "blendSrcAlpha",
                    BlendingSourceAlpha.HasValue
                        ? BlendingSourceAlpha.Value.ToString()
                        : string.Empty,
                    string.Empty
                )
                .SetTextValue(
                    "blendSrc",
                    BlendingSource.GetName(),
                    TjBlendingEquationConstants.SourceFactors.SrcAlphaFactor.GetName()
                )
                .SetTextValue(
                    "depthFunc",
                    DepthMode.GetName(),
                    TjMaterialConstants.DepthMode.LessEqualDepth.GetName()
                )
                .SetTextValue(
                    "clippingPlanes",
                    ClippingPlanesList.Select(p => p.GetJavaScriptVariableNameOrCode()).Concatenate(",", "[", "]"),
                    string.Empty
                )
                .SetTextValue(
                    "stencilFunc",
                    StencilFunction.GetName(),
                    TjMaterialConstants.StencilFunctions.AlwaysStencilFunc.GetName()
                )
                .SetTextValue(
                    "stencilFail",
                    StencilFailOperation.GetName(),
                    TjMaterialConstants.StencilOperations.KeepStencilOp.GetName()
                )
                .SetTextValue(
                    "stencilZFail",
                    StencilZFailOperation.GetName(),
                    TjMaterialConstants.StencilOperations.KeepStencilOp.GetName()
                )
                .SetTextValue(
                    "stencilZPass",
                    StencilZPassOperation.GetName(),
                    TjMaterialConstants.StencilOperations.KeepStencilOp.GetName()
                )
                .SetTextValue(
                    "side",
                    RenderedSide.GetName(),
                    TjMaterialConstants.RenderedSide.FrontSide.GetName()
                )
                .SetTextValue(
                    "side",
                    ShadowSide.HasValue ? ShadowSide.Value.GetName() : string.Empty,
                    string.Empty
                );
        }
    }
}