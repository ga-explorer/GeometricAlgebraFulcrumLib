using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;
using TextComposerLib;

namespace GraphicsComposerLib.Rendering.BabylonJs.GUI
{
    /// <summary>
    /// https://doc.babylonjs.com/typedoc/classes/BABYLON.GUI.Control
    /// </summary>
    public abstract class GrBabylonJsGuiControl :
        GrBabylonJsObject
    {
        public abstract class GuiControlProperties :
            GrBabylonJsObjectProperties
        {
            public GrBabylonJsContainerValue? Parent { get; set; }

            public GrBabylonJsBooleanValue? IsEnabled { get; set; }

            public GrBabylonJsBooleanValue? IsHighlighted { get; set; }

            public GrBabylonJsBooleanValue? IsReadOnly { get; set; }

            public GrBabylonJsBooleanValue? IsVisible { get; set; }

            public GrBabylonJsBooleanValue? NotRenderable { get; set; }

            public GrBabylonJsBooleanValue? ClipChildren { get; set; }

            public GrBabylonJsBooleanValue? ClipContent { get; set; }

            public GrBabylonJsFloat32Value? FixedRatio { get; set; }

            public GrBabylonJsStringValue? HoverCursor { get; set; }

            public GrBabylonJsBooleanValue? IsFocusInvisible { get; set; }

            public GrBabylonJsBooleanValue? IsHitTestVisible { get; set; }

            public GrBabylonJsBooleanValue? IsPointerBlocker { get; set; }

            public GrBabylonJsFloat32Value? OverlapDeltaMultiplier { get; set; }
    
            public GrBabylonJsInt32Value? OverlapGroup { get; set; }

            public GrBabylonJsBooleanValue? UseBitmapCache { get; set; }

            public GrBabylonJsBooleanValue? AllowAlphaInheritance { get; set; }
    
            public GrBabylonJsFloat32Value? Alpha { get; set; }

            public GrBabylonJsGuiColorValue? Color { get; set; }

            public GrBabylonJsGuiColorValue? DisabledColor { get; set; }

            public GrBabylonJsGuiColorValue? DisabledColorItem { get; set; }

            public GrBabylonJsGuiColorValue? HighlightColor { get; set; }

            public GrBabylonJsBooleanValue? DescendantsOnlyPadding { get; set; }

            public GrBabylonJsStringValue? FontFamily { get; set; }

            public GrBabylonJsCodeValue? FontOffset { get; set; }

            public GrBabylonJsFloat32Value? FontSize { get; set; }

            public GrBabylonJsInt32Value? FontSizeInPixels { get; set; }

            public GrBabylonJsStringValue? FontStyle { get; set; }

            public GrBabylonJsStringValue? FontWeight { get; set; }

            public GrBabylonJsFloat32Value? Height { get; set; }

            public GrBabylonJsInt32Value? HeightInPixels { get; set; }

            public GrBabylonJsFloat32Value? Width { get; set; }

            public GrBabylonJsInt32Value? WidthInPixels { get; set; }

            public GrBabylonJsFloat32Value? HighlightLineWidth { get; set; }

            public GrBabylonJsHorizontalAlignmentValue? HorizontalAlignment { get; set; }

            public GrBabylonJsFloat32Value? Top { get; set; }

            public GrBabylonJsInt32Value? TopInPixels { get; set; }

            public GrBabylonJsFloat32Value? Left { get; set; }

            public GrBabylonJsInt32Value? LeftInPixels { get; set; }

            public GrBabylonJsFloat32Value? LinkOffsetX { get; set; }

            public GrBabylonJsInt32Value? LinkOffsetXInPixels { get; set; }
        
            public GrBabylonJsFloat32Value? LinkOffsetY { get; set; }

            public GrBabylonJsInt32Value? LinkOffsetYInPixels { get; set; }

            public GrBabylonJsFloat32Value? PaddingBottom { get; set; }

            public GrBabylonJsInt32Value? PaddingBottomInPixels { get; set; }

            public GrBabylonJsFloat32Value? PaddingLeft { get; set; }

            public GrBabylonJsInt32Value? PaddingLeftInPixels { get; set; }

            public GrBabylonJsFloat32Value? PaddingRight { get; set; }

            public GrBabylonJsInt32Value? PaddingRightInPixels { get; set; }

            public GrBabylonJsFloat32Value? PaddingTop { get; set; }

            public GrBabylonJsInt32Value? PaddingTopInPixels { get; set; }

            public GrBabylonJsFloat32Value? Rotation { get; set; }

            public GrBabylonJsFloat32Value? ScaleX { get; set; }

            public GrBabylonJsFloat32Value? ScaleY { get; set; }

            public GrBabylonJsFloat32Value? ShadowBlur { get; set; }

            public GrBabylonJsGuiColorValue? ShadowColor { get; set; }

            public GrBabylonJsFloat32Value? ShadowOffsetX { get; set; }

            public GrBabylonJsFloat32Value? ShadowOffsetY { get; set; }

            public GrBabylonJsFloat32Value? TransformCenterX { get; set; }

            public GrBabylonJsFloat32Value? TransformCenterY { get; set; }

            public GrBabylonJsVerticalAlignmentValue? VerticalAlignment { get; set; }

            public GrBabylonJsInt32Value? ZIndex { get; set; }

            public GrBabylonJsStyleValue? Style { get; set; }


            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                yield return Parent.GetNameValueCodePair("parent");
                yield return IsEnabled.GetNameValueCodePair("isEnabled");
                yield return IsHighlighted.GetNameValueCodePair("isHighlighted");
                yield return IsReadOnly.GetNameValueCodePair("isReadOnly");
                yield return IsVisible.GetNameValueCodePair("isVisible");
                yield return NotRenderable.GetNameValueCodePair("notRenderable");
                yield return ClipChildren.GetNameValueCodePair("clipChildren");
                yield return ClipContent.GetNameValueCodePair("clipContent");
                yield return FixedRatio.GetNameValueCodePair("fixedRatio");
                yield return HoverCursor.GetNameValueCodePair("hoverCursor");
                yield return IsFocusInvisible.GetNameValueCodePair("isFocusInvisible");
                yield return IsHitTestVisible.GetNameValueCodePair("isHitTestVisible");
                yield return IsPointerBlocker.GetNameValueCodePair("isPointerBlocker");
                yield return OverlapDeltaMultiplier.GetNameValueCodePair("overlapDeltaMultiplier");
                yield return OverlapGroup.GetNameValueCodePair("overlapGroup");
                yield return UseBitmapCache.GetNameValueCodePair("useBitmapCache");
                yield return AllowAlphaInheritance.GetNameValueCodePair("AllowAlphaInheritance");
                yield return Alpha.GetNameValueCodePair("alpha");
                yield return Color.GetNameValueCodePair("color");
                yield return DisabledColor.GetNameValueCodePair("disabledColor");
                yield return DisabledColorItem.GetNameValueCodePair("disabledColorItem");
                yield return HighlightColor.GetNameValueCodePair("highlightColor");
                yield return DescendantsOnlyPadding.GetNameValueCodePair("descendantsOnlyPadding");
                yield return FontFamily.GetNameValueCodePair("fontFamily");
                yield return FontOffset.GetNameValueCodePair("fontOffset");
                yield return FontSize.GetNameValueCodePair("fontSize");
                yield return FontSizeInPixels.GetNameValueCodePair("fontSizeInPixels");
                yield return FontStyle.GetNameValueCodePair("fontStyle");
                yield return FontWeight.GetNameValueCodePair("fontWeight");
                yield return Height.GetNameValueCodePair("height");
                yield return HeightInPixels.GetNameValueCodePair("heightInPixels");
                yield return Width.GetNameValueCodePair("width");
                yield return WidthInPixels.GetNameValueCodePair("widthInPixels");
                yield return HighlightLineWidth.GetNameValueCodePair("highlightLineWidth");
                yield return HorizontalAlignment.GetNameValueCodePair("horizontalAlignment");
                yield return Top.GetNameValueCodePair("top");
                yield return TopInPixels.GetNameValueCodePair("topInPixels");
                yield return Left.GetNameValueCodePair("left");
                yield return LeftInPixels.GetNameValueCodePair("leftInPixels");
                yield return LinkOffsetX.GetNameValueCodePair("linkOffsetX");
                yield return LinkOffsetXInPixels.GetNameValueCodePair("linkOffsetXInPixels");
                yield return LinkOffsetY.GetNameValueCodePair("linkOffsetY");
                yield return LinkOffsetYInPixels.GetNameValueCodePair("linkOffsetYInPixels");
                yield return PaddingBottom.GetNameValueCodePair("paddingBottom");
                yield return PaddingBottomInPixels.GetNameValueCodePair("paddingBottomInPixels");
                yield return PaddingLeft.GetNameValueCodePair("paddingLeft");
                yield return PaddingLeftInPixels.GetNameValueCodePair("paddingLeftInPixels");
                yield return PaddingRight.GetNameValueCodePair("paddingRight");
                yield return PaddingRightInPixels.GetNameValueCodePair("paddingRightInPixels");
                yield return PaddingTop.GetNameValueCodePair("paddingTop");
                yield return PaddingTopInPixels.GetNameValueCodePair("paddingTopInPixels");
                yield return Rotation.GetNameValueCodePair("rotation");
                yield return ScaleX.GetNameValueCodePair("scaleX");
                yield return ScaleY.GetNameValueCodePair("scaleY");
                yield return ShadowBlur.GetNameValueCodePair("shadowBlur");
                yield return ShadowColor.GetNameValueCodePair("shadowColor");
                yield return ShadowOffsetX.GetNameValueCodePair("shadowOffsetX");
                yield return ShadowOffsetY.GetNameValueCodePair("shadowOffsetY");
                yield return TransformCenterX.GetNameValueCodePair("transformCenterX");
                yield return TransformCenterY.GetNameValueCodePair("transformCenterY");
                yield return VerticalAlignment.GetNameValueCodePair("verticalAlignment");
                yield return ZIndex.GetNameValueCodePair("zIndex");
                yield return Style.GetNameValueCodePair("style");
            }
        }

        public IGrBabylonJsGuiControlContainer ParentContainer { get; }

        public GrBabylonJsGuiFullScreenUiValue ParentUi { get; }

        public GrBabylonJsSceneValue? ParentScene 
            => ParentUi.Value.ParentScene;

        public override GrBabylonJsObjectOptions? ObjectOptions 
            => null;

    
        protected GrBabylonJsGuiControl(string constName, IGrBabylonJsGuiControlContainer parentContainer)
            : base(constName)
        {
            ParentContainer = parentContainer;
            ParentUi = parentContainer.ParentUi;
        }
    

        protected override IEnumerable<string> GetConstructorArguments()
        {
            yield return ConstName.DoubleQuote();
        }
    }
}