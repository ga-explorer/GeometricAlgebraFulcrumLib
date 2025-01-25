using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Colors;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Filters;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space2D.Styles;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Styles;

public class GrKonvaShapeStyle : 
    GrKonvaJsAttributeSet,
    IGrVisualCurveStyle2D
{
    public static GrKonvaShapeStyle CreateColorStyle(double lineWidth, Color lineColor)
    {
        var style = new GrKonvaShapeStyle(
            GrKonvaShapeStrokeKind.Color
        )
        {
            StrokeEnabled = true,
            StrokeWidth = lineWidth,
            ColorStroke =
            {
                Color = lineColor
            }
        };

        return style;
    }

    public static GrKonvaShapeStyle CreateColorStyle(double lineWidth, Color lineColor, Color fillColor)
    {
        var style = new GrKonvaShapeStyle(
            GrKonvaShapeStrokeKind.Color, 
            GrKonvaShapeFillKind.Color
        )
        {
            StrokeEnabled = true,
            StrokeWidth = lineWidth,
            ColorStroke =
            {
                Color = lineColor
            },

            FillEnabled = true,
            ColorFill =
            {
                Color = fillColor
            }
        };

        return style;
    }
        
    public static GrKonvaShapeStyle CreateDashedColorStyle(double lineWidth, IReadOnlyList<float> dashArray, Color lineColor)
    {
        var style = new GrKonvaShapeStyle(
            GrKonvaShapeStrokeKind.Color, 
            GrKonvaShapeFillKind.Color
        )
        {
            DashEnabled = true,
            Dash = GrKonvaJsFloat32ArrayValue.Create(dashArray),

            StrokeEnabled = true,
            StrokeWidth = lineWidth,
            ColorStroke =
            {
                Color = lineColor
            }
        };

        return style;
    }

    public static GrKonvaShapeStyle CreateDashedColorStyle(double lineWidth, IReadOnlyList<float> dashArray, Color lineColor, Color fillColor)
    {
        var style = new GrKonvaShapeStyle(
            GrKonvaShapeStrokeKind.Color, 
            GrKonvaShapeFillKind.Color
        )
        {
            DashEnabled = true,
            Dash = GrKonvaJsFloat32ArrayValue.Create(dashArray),

            StrokeEnabled = true,
            StrokeWidth = lineWidth,
            ColorStroke =
            {
                Color = lineColor
            },

            FillEnabled = true,
            ColorFill =
            {
                Color = fillColor
            }
        };

        return style;
    }


    public GrKonvaJsFloat32Value? Visible
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Visible");
        set => SetAttributeValue("Visible", value);
    }
        
    public GrKonvaJsFloat32Value? Opacity
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Opacity");
        set => SetAttributeValue("Opacity", value);
    }
        
    public GrKonvaJsBooleanValue? PerfectDrawEnabled
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("PerfectDrawEnabled");
        set => SetAttributeValue("PerfectDrawEnabled", value);
    }

    public GrKonvaJsGlobalCompositeOperationValue? GlobalCompositeOperation
    {
        get => GetAttributeValueOrNull<GrKonvaJsGlobalCompositeOperationValue>("globalCompositeOperation");
        set => SetAttributeValue("globalCompositeOperation", value);
    }
        
    public GrKonvaJsBooleanValue? StrokeEnabled
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("StrokeEnabled");
        set => SetAttributeValue("StrokeEnabled", value);
    }
        
    public GrKonvaJsBooleanValue? StrokeScaleEnabled
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("StrokeScaleEnabled");
        set => SetAttributeValue("StrokeScaleEnabled", value);
    }
        
    public GrKonvaJsFloat32Value? StrokeWidth
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("StrokeWidth");
        set => SetAttributeValue("StrokeWidth", value);
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

    public GrKonvaShapeStroke Stroke { get; private set; }

    public GrKonvaShapeStrokeColor ColorStroke
        => (GrKonvaShapeStrokeColor) Stroke;
        
    public GrKonvaShapeStrokeLinearGradient LinearGradientStroke
        => (GrKonvaShapeStrokeLinearGradient) Stroke;
        
    public GrKonvaJsBooleanValue? FillEnabled
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("FillEnabled");
        set => SetAttributeValue("FillEnabled", value);
    }

    public GrKonvaJsBooleanValue? FillAfterStrokeEnabled
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("FillAfterStrokeEnabled");
        set => SetAttributeValue("FillAfterStrokeEnabled", value);
    }
        
    public GrKonvaJsFillRuleValue? FillRule
    {
        get => GetAttributeValueOrNull<GrKonvaJsFillRuleValue>("FillRule");
        set => SetAttributeValue("FillRule", value);
    }

    public GrKonvaShapeFill Fill { get; private set; }

    public GrKonvaShapeFillColor ColorFill 
        => (GrKonvaShapeFillColor) Fill; 
        
    public GrKonvaShapeFillPattern PatternFill 
        => (GrKonvaShapeFillPattern) Fill; 
        
    public GrKonvaShapeFillLinearGradient LinearGradientFill 
        => (GrKonvaShapeFillLinearGradient) Fill; 
        
    public GrKonvaShapeFillRadialGradient RadialGradientFill 
        => (GrKonvaShapeFillRadialGradient) Fill; 
        
    public GrKonvaShapeShadow Shadow { get; }

    public GrKonvaFilterList Filters { get; }


    public GrKonvaShapeStyle()
    {
        StrokeEnabled = true;
        StrokeWidth = 1;
        StrokeScaleEnabled = false;
        Stroke = new GrKonvaShapeStrokeColor(this)
        {
            Color = Color.Black
        };

        FillEnabled = false;
        Fill = new GrKonvaShapeFillColor(this)
        {
            Color = Color.Transparent
        };

        Shadow = new GrKonvaShapeShadow(this);
        Filters = new GrKonvaFilterList(this);
    }
        
    public GrKonvaShapeStyle(GrKonvaShapeStrokeKind strokeKind)
    {
        StrokeEnabled = true;
        StrokeWidth = 1;
        StrokeScaleEnabled = false;
        Stroke = strokeKind switch
        {
            GrKonvaShapeStrokeKind.Color => new GrKonvaShapeStrokeColor(this),
            GrKonvaShapeStrokeKind.LinearGradient => new GrKonvaShapeStrokeLinearGradient(this),
            _ => throw new InvalidOperationException()
        };

        FillEnabled = false;
        Fill = new GrKonvaShapeFillColor(this)
        {
            Color = Color.Transparent
        };

        Shadow = new GrKonvaShapeShadow(this);
        Filters = new GrKonvaFilterList(this);
    }
        
    public GrKonvaShapeStyle(GrKonvaShapeStrokeKind strokeKind, GrKonvaShapeFillKind fillKind)
    {
        StrokeEnabled = true;
        StrokeWidth = 1;
        StrokeScaleEnabled = false;
        Stroke = strokeKind switch
        {
            GrKonvaShapeStrokeKind.Color => new GrKonvaShapeStrokeColor(this),
            GrKonvaShapeStrokeKind.LinearGradient => new GrKonvaShapeStrokeLinearGradient(this),
            _ => throw new InvalidOperationException()
        };

        FillEnabled = fillKind != GrKonvaShapeFillKind.None;
        Fill = fillKind switch 
        {
            GrKonvaShapeFillKind.None => new GrKonvaShapeFillColor(this){Color = Color.Transparent},
            GrKonvaShapeFillKind.Color => new GrKonvaShapeFillColor(this),
            GrKonvaShapeFillKind.Pattern => new GrKonvaShapeFillPattern(this),
            GrKonvaShapeFillKind.LinearGradient => new GrKonvaShapeFillLinearGradient(this),
            GrKonvaShapeFillKind.RadialGradient => new GrKonvaShapeFillRadialGradient(this),
            _ => throw new InvalidOperationException()
        };

        Shadow = new GrKonvaShapeShadow(this);
        Filters = new GrKonvaFilterList(this);
    }

    public GrKonvaShapeStyle(GrKonvaShapeStyle style) 
        : this()
    {
        SetAttributeValues(style);
    }


    public GrKonvaShapeStyle GetCopy()
    {
        return new GrKonvaShapeStyle(this);
    }

    public GrKonvaShapeStyle SetStrokeNone()
    {
        StrokeEnabled = false;

        return this;
    }

    public GrKonvaShapeStyle SetStrokeColor()
    {
        StrokeEnabled = true;

        Stroke = new GrKonvaShapeStrokeColor(this);

        return this;
    }

    public GrKonvaShapeStyle SetStrokeColor(Color color)
    {
        StrokeEnabled = true;

        Stroke = new GrKonvaShapeStrokeColor(this)
        {
            Color = color
        };

        return this;
    }
        
    public GrKonvaShapeStyle SetStrokeColor(string code)
    {
        StrokeEnabled = true;

        Stroke = new GrKonvaShapeStrokeColor(this)
        {
            Color = code
        };

        return this;
    }

    public GrKonvaShapeStyle SetStrokeLinearGradient()
    {
        StrokeEnabled = true;

        Stroke = new GrKonvaShapeStrokeLinearGradient(this);

        return this;
    }

    public GrKonvaShapeStyle SetStrokeLinearGradient(Color color)
    {
        StrokeEnabled = true;

        Stroke = new GrKonvaShapeStrokeLinearGradient(this)
        {
            ColorStops = GrKonvaJsColorLinearGradientListValue.Create(color)
        };

        return this;
    }
        
    public GrKonvaShapeStyle SetStrokeLinearGradient(Color color1, Color color2)
    {
        StrokeEnabled = true;

        Stroke = new GrKonvaShapeStrokeLinearGradient(this)
        {
            ColorStops = GrKonvaJsColorLinearGradientListValue.Create(color1, color2)
        };

        return this;
    }
        
    public GrKonvaShapeStyle SetStrokeLinearGradient(params Color[] colorArray)
    {
        StrokeEnabled = true;

        Stroke = new GrKonvaShapeStrokeLinearGradient(this)
        {
            ColorStops = GrKonvaJsColorLinearGradientListValue.Create(colorArray)
        };

        return this;
    }
        
    public GrKonvaShapeStyle SetStrokeLinearGradient(GrColorLinearGradientList colorList)
    {
        StrokeEnabled = true;

        Stroke = new GrKonvaShapeStrokeLinearGradient(this)
        {
            ColorStops = GrKonvaJsColorLinearGradientListValue.Create(colorList)
        };

        return this;
    }
        
    public GrKonvaShapeStyle SetStrokeLinearGradient(GrColorLinearGradientList colorList, IPair<double> startPoint, IPair<double> endPoint)
    {
        StrokeEnabled = true;

        Stroke = new GrKonvaShapeStrokeLinearGradient(this)
        {
            ColorStops = GrKonvaJsColorLinearGradientListValue.Create(colorList),
            StartPoint = startPoint.ToLinVector2D(),
            EndPoint = endPoint.ToLinVector2D()
        };

        return this;
    }

    public GrKonvaShapeStyle SetStrokeLinearGradient(string code)
    {
        StrokeEnabled = true;

        Stroke = new GrKonvaShapeStrokeLinearGradient(this)
        {
            ColorStops = GrKonvaJsColorLinearGradientListValue.Create(code)
        };

        return this;
    }
        
    public GrKonvaShapeStyle SetFillNone()
    {
        FillEnabled = false;

        return this;
    }

    public GrKonvaShapeStyle SetFillColor()
    {
        FillEnabled = true;

        Fill = new GrKonvaShapeFillColor(this);

        return this;
    }

    public GrKonvaShapeStyle SetFillColor(Color color)
    {
        FillEnabled = true;

        Fill = new GrKonvaShapeFillColor(this)
        {
            Color = color
        };

        return this;
    }
        
    public GrKonvaShapeStyle SetFillColor(string code)
    {
        FillEnabled = true;

        Fill = new GrKonvaShapeFillColor(this)
        {
            Color = code
        };

        return this;
    }
        
    public GrKonvaShapeStyle SetFillPattern()
    {
        FillEnabled = true;

        Fill = new GrKonvaShapeFillPattern(this);

        return this;
    }

    public GrKonvaShapeStyle SetFillLinearGradient()
    {
        FillEnabled = true;

        Fill = new GrKonvaShapeFillLinearGradient(this);

        return this;
    }

    public GrKonvaShapeStyle SetFillLinearGradient(Color color)
    {
        FillEnabled = true;

        Fill = new GrKonvaShapeFillLinearGradient(this)
        {
            ColorStops = GrKonvaJsColorLinearGradientListValue.Create(color)
        };

        return this;
    }
        
    public GrKonvaShapeStyle SetFillLinearGradient(Color color1, Color color2)
    {
        FillEnabled = true;

        Fill = new GrKonvaShapeFillLinearGradient(this)
        {
            ColorStops = GrKonvaJsColorLinearGradientListValue.Create(color1, color2)
        };

        return this;
    }
        
    public GrKonvaShapeStyle SetFillLinearGradient(params Color[] colorArray)
    {
        FillEnabled = true;

        Fill = new GrKonvaShapeFillLinearGradient(this)
        {
            ColorStops = GrKonvaJsColorLinearGradientListValue.Create(colorArray)
        };

        return this;
    }
        
    public GrKonvaShapeStyle SetFillLinearGradient(GrColorLinearGradientList colorList)
    {
        FillEnabled = true;

        Fill = new GrKonvaShapeFillLinearGradient(this)
        {
            ColorStops = GrKonvaJsColorLinearGradientListValue.Create(colorList)
        };

        return this;
    }
        
    public GrKonvaShapeStyle SetFillLinearGradient(GrColorLinearGradientList colorList, IPair<double> startPoint, IPair<double> endPoint)
    {
        FillEnabled = true;

        Fill = new GrKonvaShapeFillLinearGradient(this)
        {
            ColorStops = GrKonvaJsColorLinearGradientListValue.Create(colorList),
            StartPoint = startPoint.ToLinVector2D(),
            EndPoint = endPoint.ToLinVector2D()
        };

        return this;
    }

    public GrKonvaShapeStyle SetFillLinearGradient(string code)
    {
        FillEnabled = true;

        Fill = new GrKonvaShapeFillLinearGradient(this)
        {
            ColorStops = GrKonvaJsColorLinearGradientListValue.Create(code)
        };

        return this;
    }
        
    public GrKonvaShapeStyle SetFillRadialGradient()
    {
        FillEnabled = true;

        Fill = new GrKonvaShapeFillRadialGradient(this);

        return this;
    }
        
    public GrKonvaShapeStyle SetFillRadialGradient(Color color1, Color color2)
    {
        FillEnabled = true;

        Fill = new GrKonvaShapeFillRadialGradient(this)
        {
            ColorStops = GrKonvaJsColorLinearGradientListValue.Create(color1, color2)
        };

        return this;
    }
        
    public GrKonvaShapeStyle SetFillRadialGradient(params Color[] colorArray)
    {
        FillEnabled = true;

        Fill = new GrKonvaShapeFillRadialGradient(this)
        {
            ColorStops = GrKonvaJsColorLinearGradientListValue.Create(colorArray)
        };

        return this;
    }
        
    public GrKonvaShapeStyle SetFillRadialGradient(GrColorLinearGradientList colorList)
    {
        FillEnabled = true;

        Fill = new GrKonvaShapeFillRadialGradient(this)
        {
            ColorStops = GrKonvaJsColorLinearGradientListValue.Create(colorList)
        };

        return this;
    }
        
    public GrKonvaShapeStyle SetFillRadialGradient(GrColorLinearGradientList colorList, IPair<double> startPoint, IPair<double> endPoint, double startRadius, double endRadius)
    {
        FillEnabled = true;

        Fill = new GrKonvaShapeFillRadialGradient(this)
        {
            ColorStops = GrKonvaJsColorLinearGradientListValue.Create(colorList),
            StartPoint = startPoint.ToLinVector2D(),
            EndPoint = endPoint.ToLinVector2D(),
            StartRadius = startRadius,
            EndRadius = endRadius
        };

        return this;
    }

    public GrKonvaShapeStyle SetFillRadialGradient(string code)
    {
        FillEnabled = true;

        Fill = new GrKonvaShapeFillRadialGradient(this)
        {
            ColorStops = GrKonvaJsColorLinearGradientListValue.Create(code)
        };

        return this;
    }


    public override string GetKonvaJsCode()
    {
        throw new NotImplementedException();
    }
}