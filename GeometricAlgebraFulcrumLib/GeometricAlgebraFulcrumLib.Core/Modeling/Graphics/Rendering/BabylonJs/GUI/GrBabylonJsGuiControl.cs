using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.GUI;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.GUI.Control
/// </summary>
public abstract class GrBabylonJsGuiControl :
    GrBabylonJsObject
{
    public abstract class GuiControlProperties :
        GrBabylonJsObjectProperties
    {
        public GrBabylonJsContainerValue? Parent
        {
            get => GetAttributeValueOrNull<GrBabylonJsContainerValue>("parent");
            set => SetAttributeValue("parent", value);
        }

        public GrBabylonJsBooleanValue? IsEnabled
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isEnabled");
            set => SetAttributeValue("isEnabled", value);
        }

        public GrBabylonJsBooleanValue? IsHighlighted
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isHighlighted");
            set => SetAttributeValue("isHighlighted", value);
        }

        public GrBabylonJsBooleanValue? IsReadOnly
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isReadOnly");
            set => SetAttributeValue("isReadOnly", value);
        }

        public GrBabylonJsBooleanValue? IsVisible
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isVisible");
            set => SetAttributeValue("isVisible", value);
        }

        public GrBabylonJsBooleanValue? NotRenderable
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("notRenderable");
            set => SetAttributeValue("notRenderable", value);
        }

        public GrBabylonJsBooleanValue? ClipChildren
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("clipChildren");
            set => SetAttributeValue("clipChildren", value);
        }

        public GrBabylonJsBooleanValue? ClipContent
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("clipContent");
            set => SetAttributeValue("clipContent", value);
        }

        public GrBabylonJsFloat32Value? FixedRatio
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("fixedRatio");
            set => SetAttributeValue("fixedRatio", value);
        }

        public GrBabylonJsStringValue? HoverCursor
        {
            get => GetAttributeValueOrNull<GrBabylonJsStringValue>("hoverCursor");
            set => SetAttributeValue("hoverCursor", value);
        }

        public GrBabylonJsBooleanValue? IsFocusInvisible
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isFocusInvisible");
            set => SetAttributeValue("isFocusInvisible", value);
        }

        public GrBabylonJsBooleanValue? IsHitTestVisible
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isHitTestVisible");
            set => SetAttributeValue("isHitTestVisible", value);
        }

        public GrBabylonJsBooleanValue? IsPointerBlocker
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isPointerBlocker");
            set => SetAttributeValue("isPointerBlocker", value);
        }

        public GrBabylonJsFloat32Value? OverlapDeltaMultiplier
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("overlapDeltaMultiplier");
            set => SetAttributeValue("overlapDeltaMultiplier", value);
        }
    
        public GrBabylonJsInt32Value? OverlapGroup
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("overlapGroup");
            set => SetAttributeValue("overlapGroup", value);
        }

        public GrBabylonJsBooleanValue? UseBitmapCache
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useBitmapCache");
            set => SetAttributeValue("useBitmapCache", value);
        }

        public GrBabylonJsBooleanValue? AllowAlphaInheritance
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("allowAlphaInheritance");
            set => SetAttributeValue("allowAlphaInheritance", value);
        }
    
        public GrBabylonJsFloat32Value? Alpha
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("alpha");
            set => SetAttributeValue("alpha", value);
        }

        public GrBabylonJsGuiColorValue? Color
        {
            get => GetAttributeValueOrNull<GrBabylonJsGuiColorValue>("color");
            set => SetAttributeValue("color", value);
        }

        public GrBabylonJsGuiColorValue? DisabledColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsGuiColorValue>("disabledColor");
            set => SetAttributeValue("disabledColor", value);
        }

        public GrBabylonJsGuiColorValue? DisabledColorItem
        {
            get => GetAttributeValueOrNull<GrBabylonJsGuiColorValue>("disabledColorItem");
            set => SetAttributeValue("disabledColorItem", value);
        }

        public GrBabylonJsGuiColorValue? HighlightColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsGuiColorValue>("highlightColor");
            set => SetAttributeValue("highlightColor", value);
        }

        public GrBabylonJsBooleanValue? DescendantsOnlyPadding
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("descendantsOnlyPadding");
            set => SetAttributeValue("descendantsOnlyPadding", value);
        }

        public GrBabylonJsStringValue? FontFamily
        {
            get => GetAttributeValueOrNull<GrBabylonJsStringValue>("fontFamily");
            set => SetAttributeValue("fontFamily", value);
        }

        public GrBabylonJsCodeValue? FontOffset
        {
            get => GetAttributeValueOrNull<GrBabylonJsCodeValue>("fontOffset");
            set => SetAttributeValue("fontOffset", value);
        }

        public GrBabylonJsFloat32Value? FontSize
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("fontSize");
            set => SetAttributeValue("fontSize", value);
        }

        public GrBabylonJsInt32Value? FontSizeInPixels
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("fontSizeInPixels");
            set => SetAttributeValue("fontSizeInPixels", value);
        }

        public GrBabylonJsStringValue? FontStyle
        {
            get => GetAttributeValueOrNull<GrBabylonJsStringValue>("fontStyle");
            set => SetAttributeValue("fontStyle", value);
        }

        public GrBabylonJsStringValue? FontWeight
        {
            get => GetAttributeValueOrNull<GrBabylonJsStringValue>("fontWeight");
            set => SetAttributeValue("fontWeight", value);
        }

        public GrBabylonJsFloat32Value? Height
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("height");
            set => SetAttributeValue("height", value);
        }

        public GrBabylonJsInt32Value? HeightInPixels
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("heightInPixels");
            set => SetAttributeValue("heightInPixels", value);
        }

        public GrBabylonJsFloat32Value? Width
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("width");
            set => SetAttributeValue("width", value);
        }

        public GrBabylonJsInt32Value? WidthInPixels
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("widthInPixels");
            set => SetAttributeValue("widthInPixels", value);
        }

        public GrBabylonJsFloat32Value? HighlightLineWidth
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("highlightLineWidth");
            set => SetAttributeValue("highlightLineWidth", value);
        }

        public GrBabylonJsHorizontalAlignmentValue? HorizontalAlignment
        {
            get => GetAttributeValueOrNull<GrBabylonJsHorizontalAlignmentValue>("horizontalAlignment");
            set => SetAttributeValue("horizontalAlignment", value);
        }

        public GrBabylonJsFloat32Value? Top
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("top");
            set => SetAttributeValue("top", value);
        }

        public GrBabylonJsInt32Value? TopInPixels
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("topInPixels");
            set => SetAttributeValue("topInPixels", value);
        }

        public GrBabylonJsFloat32Value? Left
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("left");
            set => SetAttributeValue("left", value);
        }

        public GrBabylonJsInt32Value? LeftInPixels
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("leftInPixels");
            set => SetAttributeValue("leftInPixels", value);
        }

        public GrBabylonJsFloat32Value? LinkOffsetX
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("linkOffsetX");
            set => SetAttributeValue("linkOffsetX", value);
        }

        public GrBabylonJsInt32Value? LinkOffsetXInPixels
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("linkOffsetXInPixels");
            set => SetAttributeValue("linkOffsetXInPixels", value);
        }
        
        public GrBabylonJsFloat32Value? LinkOffsetY
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("linkOffsetY");
            set => SetAttributeValue("linkOffsetY", value);
        }

        public GrBabylonJsInt32Value? LinkOffsetYInPixels
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("linkOffsetYInPixels");
            set => SetAttributeValue("linkOffsetYInPixels", value);
        }

        public GrBabylonJsFloat32Value? PaddingBottom
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("paddingBottom");
            set => SetAttributeValue("paddingBottom", value);
        }

        public GrBabylonJsInt32Value? PaddingBottomInPixels
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("paddingBottomInPixels");
            set => SetAttributeValue("paddingBottomInPixels", value);
        }

        public GrBabylonJsFloat32Value? PaddingLeft
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("paddingLeft");
            set => SetAttributeValue("paddingLeft", value);
        }

        public GrBabylonJsInt32Value? PaddingLeftInPixels
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("paddingLeftInPixels");
            set => SetAttributeValue("paddingLeftInPixels", value);
        }

        public GrBabylonJsFloat32Value? PaddingRight
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("paddingRight");
            set => SetAttributeValue("paddingRight", value);
        }

        public GrBabylonJsInt32Value? PaddingRightInPixels
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("paddingRightInPixels");
            set => SetAttributeValue("paddingRightInPixels", value);
        }

        public GrBabylonJsFloat32Value? PaddingTop
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("paddingTop");
            set => SetAttributeValue("paddingTop", value);
        }

        public GrBabylonJsInt32Value? PaddingTopInPixels
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("paddingTopInPixels");
            set => SetAttributeValue("paddingTopInPixels", value);
        }

        public GrBabylonJsFloat32Value? Rotation
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("rotation");
            set => SetAttributeValue("rotation", value);
        }

        public GrBabylonJsFloat32Value? ScaleX
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("scaleX");
            set => SetAttributeValue("scaleX", value);
        }

        public GrBabylonJsFloat32Value? ScaleY
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("scaleY");
            set => SetAttributeValue("scaleY", value);
        }

        public GrBabylonJsFloat32Value? ShadowBlur
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("shadowBlur");
            set => SetAttributeValue("shadowBlur", value);
        }

        public GrBabylonJsGuiColorValue? ShadowColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsGuiColorValue>("shadowColor");
            set => SetAttributeValue("shadowColor", value);
        }

        public GrBabylonJsFloat32Value? ShadowOffsetX
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("shadowOffsetX");
            set => SetAttributeValue("shadowOffsetX", value);
        }

        public GrBabylonJsFloat32Value? ShadowOffsetY
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("shadowOffsetY");
            set => SetAttributeValue("shadowOffsetY", value);
        }

        public GrBabylonJsFloat32Value? TransformCenterX
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("transformCenterX");
            set => SetAttributeValue("transformCenterX", value);
        }

        public GrBabylonJsFloat32Value? TransformCenterY
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("transformCenterY");
            set => SetAttributeValue("transformCenterY", value);
        }

        public GrBabylonJsVerticalAlignmentValue? VerticalAlignment
        {
            get => GetAttributeValueOrNull<GrBabylonJsVerticalAlignmentValue>("verticalAlignment");
            set => SetAttributeValue("verticalAlignment", value);
        }

        public GrBabylonJsInt32Value? ZIndex
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("zIndex");
            set => SetAttributeValue("zIndex", value);
        }

        public GrBabylonJsStyleValue? Style
        {
            get => GetAttributeValueOrNull<GrBabylonJsStyleValue>("style");
            set => SetAttributeValue("style", value);
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