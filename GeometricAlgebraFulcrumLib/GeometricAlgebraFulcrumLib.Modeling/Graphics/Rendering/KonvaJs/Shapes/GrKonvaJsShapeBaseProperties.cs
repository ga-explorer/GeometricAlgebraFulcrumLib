﻿using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

public abstract class GrKonvaJsShapeBaseProperties :
    GrKonvaJsNodeProperties
{
    public GrKonvaJsBooleanValue? DashEnabled
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("DashEnabled");
        set => SetAttributeValue("DashEnabled", value);
    }

    public GrKonvaJsFloat32ArrayValue? Dash
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32ArrayValue>("Dash");
        set => SetAttributeValue("Dash", value);
    }

    public GrKonvaJsFloat32Value? DashOffset
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("DashOffset");
        set => SetAttributeValue("DashOffset", value);
    }

    public GrKonvaJsColorValue? Fill
    {
        get => GetAttributeValueOrNull<GrKonvaJsColorValue>("Fill");
        set => SetAttributeValue("Fill", value);
    }

    public GrKonvaJsBooleanValue? FillAfterStrokeEnabled
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("FillAfterStrokeEnabled");
        set => SetAttributeValue("FillAfterStrokeEnabled", value);
    }

    public GrKonvaJsBooleanValue? FillEnabled
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("FillEnabled");
        set => SetAttributeValue("FillEnabled", value);
    }
        
    public GrKonvaJsColorLinearGradientListValue? FillLinearGradientColorStops
    {
        get => GetAttributeValueOrNull<GrKonvaJsColorLinearGradientListValue>("FillLinearGradientColorStops");
        set => SetAttributeValue("FillLinearGradientColorStops", value);
    }

    public GrKonvaJsVector2Value? FillLinearGradientEndPoint
    {
        get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("FillLinearGradientEndPoint");
        set => SetAttributeValue("FillLinearGradientEndPoint", value);
    }
        
    public GrKonvaJsFloat32Value? FillLinearGradientEndPointX
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillLinearGradientEndPointX");
        set => SetAttributeValue("FillLinearGradientEndPointX", value);
    }

    public GrKonvaJsFloat32Value? FillLinearGradientEndPointY
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillLinearGradientEndPointY");
        set => SetAttributeValue("FillLinearGradientEndPointY", value);
    }
            
    public GrKonvaJsVector2Value? FillLinearGradientStartPoint
    {
        get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("FillLinearGradientStartPoint");
        set => SetAttributeValue("FillLinearGradientStartPoint", value);
    }
        
    public GrKonvaJsFloat32Value? FillLinearGradientStartPointX
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillLinearGradientStartPointX");
        set => SetAttributeValue("FillLinearGradientStartPointX", value);
    }

    public GrKonvaJsFloat32Value? FillLinearGradientStartPointY
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillLinearGradientStartPointY");
        set => SetAttributeValue("FillLinearGradientStartPointY", value);
    }

    public GrKonvaJsImageValue? FillPatternImage
    {
        get => GetAttributeValueOrNull<GrKonvaJsImageValue>("FillPatternImage");
        set => SetAttributeValue("FillPatternImage", value);
    }
        
    public GrKonvaJsVector2Value? FillPatternOffset
    {
        get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("FillPatternOffset");
        set => SetAttributeValue("FillPatternOffset", value);
    }
        
    public GrKonvaJsFloat32Value? FillPatternOffsetX
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillPatternOffsetX");
        set => SetAttributeValue("FillPatternOffsetX", value);
    }

    public GrKonvaJsFloat32Value? FillPatternOffsetY
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillPatternOffsetY");
        set => SetAttributeValue("FillPatternOffsetY", value);
    }

    public GrKonvaJsFillPatternRepeatValue? FillPatternRepeat
    {
        get => GetAttributeValueOrNull<GrKonvaJsFillPatternRepeatValue>("FillPatternRepeat");
        set => SetAttributeValue("FillPatternRepeat", value);
    }

    public GrKonvaJsFloat32Value? FillPatternRotation
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillPatternRotation");
        set => SetAttributeValue("FillPatternRotation", value);
    }
            
    public GrKonvaJsVector2Value? FillPatternScale
    {
        get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("FillPatternScale");
        set => SetAttributeValue("FillPatternScale", value);
    }
        
    public GrKonvaJsFloat32Value? FillPatternScaleX
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillPatternScaleX");
        set => SetAttributeValue("FillPatternScaleX", value);
    }

    public GrKonvaJsFloat32Value? FillPatternScaleY
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillPatternScaleY");
        set => SetAttributeValue("FillPatternScaleY", value);
    }
        
    public GrKonvaJsFloat32Value? FillPatternX
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillPatternX");
        set => SetAttributeValue("FillPatternX", value);
    }

    public GrKonvaJsFloat32Value? FillPatternY
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillPatternY");
        set => SetAttributeValue("FillPatternY", value);
    }
        
    public GrKonvaJsFillPriorityValue? FillPriority
    {
        get => GetAttributeValueOrNull<GrKonvaJsFillPriorityValue>("FillPriority");
        set => SetAttributeValue("FillPriority", value);
    }
        
    public GrKonvaJsFillRuleValue? FillRule
    {
        get => GetAttributeValueOrNull<GrKonvaJsFillRuleValue>("FillRule");
        set => SetAttributeValue("FillRule", value);
    }

    public GrKonvaJsColorLinearGradientListValue? FillRadialGradientColorStops
    {
        get => GetAttributeValueOrNull<GrKonvaJsColorLinearGradientListValue>("FillRadialGradientColorStops");
        set => SetAttributeValue("FillRadialGradientColorStops", value);
    }
        
    public GrKonvaJsVector2Value? FillRadialGradientEndPoint
    {
        get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("FillRadialGradientEndPoint");
        set => SetAttributeValue("FillRadialGradientEndPoint", value);
    }
        
    public GrKonvaJsFloat32Value? FillRadialGradientEndPointX
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillRadialGradientEndPointX");
        set => SetAttributeValue("FillRadialGradientEndPointX", value);
    }

    public GrKonvaJsFloat32Value? FillRadialGradientEndPointY
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillRadialGradientEndPointY");
        set => SetAttributeValue("FillRadialGradientEndPointY", value);
    }
        
    public GrKonvaJsFloat32Value? FillRadialGradientStartRadius
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillRadialGradientStartRadius");
        set => SetAttributeValue("FillRadialGradientStartRadius", value);
    }
        
    public GrKonvaJsFloat32Value? FillRadialGradientEndRadius
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillRadialGradientEndRadius");
        set => SetAttributeValue("FillRadialGradientEndRadius", value);
    }

    public GrKonvaJsVector2Value? FillRadialGradientStartPoint
    {
        get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("FillRadialGradientStartPoint");
        set => SetAttributeValue("FillRadialGradientStartPoint", value);
    }
        
    public GrKonvaJsFloat32Value? FillRadialGradientStartPointX
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillRadialGradientStartPointX");
        set => SetAttributeValue("FillRadialGradientStartPointX", value);
    }

    public GrKonvaJsFloat32Value? FillRadialGradientStartPointY
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillRadialGradientStartPointY");
        set => SetAttributeValue("FillRadialGradientStartPointY", value);
    }
        
    public GrKonvaJsCodeValue? HitFunc
    {
        get => GetAttributeValueOrNull<GrKonvaJsCodeValue>("HitFunc");
        set => SetAttributeValue("HitFunc", value);
    }

    public GrKonvaJsFloat32Value? HitStrokeWidth
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("HitStrokeWidth");
        set => SetAttributeValue("HitStrokeWidth", value);
    }
        
    public GrKonvaJsLineCapValue? LineCap
    {
        get => GetAttributeValueOrNull<GrKonvaJsLineCapValue>("LineCap");
        set => SetAttributeValue("LineCap", value);
    }

    public GrKonvaJsLineJoinValue? LineJoin
    {
        get => GetAttributeValueOrNull<GrKonvaJsLineJoinValue>("LineJoin");
        set => SetAttributeValue("LineJoin", value);
    }
        
    public GrKonvaJsBooleanValue? PerfectDrawEnabled
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("PerfectDrawEnabled");
        set => SetAttributeValue("PerfectDrawEnabled", value);
    }
        
    public GrKonvaJsCodeValue? SceneFunc
    {
        get => GetAttributeValueOrNull<GrKonvaJsCodeValue>("SceneFunc");
        set => SetAttributeValue("SceneFunc", value);
    }

    public GrKonvaJsFloat32Value? ShadowBlur
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ShadowBlur");
        set => SetAttributeValue("ShadowBlur", value);
    }
        
    public GrKonvaJsColorValue? ShadowColor
    {
        get => GetAttributeValueOrNull<GrKonvaJsColorValue>("ShadowColor");
        set => SetAttributeValue("ShadowColor", value);
    }
            
    public GrKonvaJsBooleanValue? ShadowForStrokeEnabled
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("ShadowForStrokeEnabled");
        set => SetAttributeValue("ShadowForStrokeEnabled", value);
    }
        
    public GrKonvaJsVector2Value? ShadowOffset
    {
        get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("ShadowOffset");
        set => SetAttributeValue("ShadowOffset", value);
    }
        
    public GrKonvaJsFloat32Value? ShadowOffsetX
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ShadowOffsetX");
        set => SetAttributeValue("ShadowOffsetX", value);
    }

    public GrKonvaJsFloat32Value? ShadowOffsetY
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ShadowOffsetY");
        set => SetAttributeValue("ShadowOffsetY", value);
    }
        
    public GrKonvaJsFloat32Value? ShadowOpacity
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ShadowOpacity");
        set => SetAttributeValue("ShadowOpacity", value);
    }
        
    public GrKonvaJsFloat32Value? StrokeWidth
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("StrokeWidth");
        set => SetAttributeValue("StrokeWidth", value);
    }

    public GrKonvaJsColorValue? Stroke
    {
        get => GetAttributeValueOrNull<GrKonvaJsColorValue>("Stroke");
        set => SetAttributeValue("Stroke", value);
    }
        
    public GrKonvaJsBooleanValue? StrokeEnabled
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("StrokeEnabled");
        set => SetAttributeValue("StrokeEnabled", value);
    }
        
    public GrKonvaJsVector2Value? StrokeLinearGradientEndPoint
    {
        get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("StrokeLinearGradientEndPoint");
        set => SetAttributeValue("StrokeLinearGradientEndPoint", value);
    }
        
    public GrKonvaJsFloat32Value? StrokeLinearGradientEndPointX
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("StrokeLinearGradientEndPointX");
        set => SetAttributeValue("StrokeLinearGradientEndPointX", value);
    }

    public GrKonvaJsFloat32Value? StrokeLinearGradientEndPointY
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("StrokeLinearGradientEndPointY");
        set => SetAttributeValue("StrokeLinearGradientEndPointY", value);
    }
        
    public GrKonvaJsColorLinearGradientListValue? StrokeLinearGradientColorStops
    {
        get => GetAttributeValueOrNull<GrKonvaJsColorLinearGradientListValue>("StrokeLinearGradientColorStops");
        set => SetAttributeValue("StrokeLinearGradientColorStops", value);
    }
        
    public GrKonvaJsVector2Value? StrokeLinearGradientStartPoint
    {
        get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("StrokeLinearGradientStartPoint");
        set => SetAttributeValue("StrokeLinearGradientStartPoint", value);
    }
        
    public GrKonvaJsFloat32Value? StrokeLinearGradientStartPointX
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("StrokeLinearGradientStartPointX");
        set => SetAttributeValue("StrokeLinearGradientStartPointX", value);
    }

    public GrKonvaJsFloat32Value? StrokeLinearGradientStartPointY
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("StrokeLinearGradientStartPointY");
        set => SetAttributeValue("StrokeLinearGradientStartPointY", value);
    }
        
    public GrKonvaJsBooleanValue? StrokeScaleEnabled
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("StrokeScaleEnabled");
        set => SetAttributeValue("StrokeScaleEnabled", value);
    }
        
}