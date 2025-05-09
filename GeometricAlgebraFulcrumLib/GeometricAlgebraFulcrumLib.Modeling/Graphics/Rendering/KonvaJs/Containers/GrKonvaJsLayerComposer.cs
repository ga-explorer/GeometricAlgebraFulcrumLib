using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Colors;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Containers;

public class GrKonvaJsLayerComposer
{
    public GrKonvaJsStage Stage { get; }

    public int LayerIndex { get; }

    public GrKonvaJsLayer Layer { get; }

    public GrKonvaShapeStyle ActiveStyle { get; private set; }


    internal GrKonvaJsLayerComposer(GrKonvaJsStage stage, int layerIndex)
    {
        Stage = stage;
        LayerIndex = layerIndex;
        Layer = Stage.Layers[LayerIndex];
        ActiveStyle = new GrKonvaShapeStyle();
    }
    
    internal GrKonvaJsLayerComposer(GrKonvaJsStage stage, int layerIndex, GrKonvaShapeStyle activeStyle)
    {
        Stage = stage;
        LayerIndex = layerIndex;
        Layer = Stage.Layers[LayerIndex];
        ActiveStyle = activeStyle;
    }


    public GrKonvaJsLayerComposer SwitchLayer(int layerIndex)
    {
        return Stage.LayerComposers[layerIndex];
    }


    public GrKonvaJsLayerComposer ResetStyle()
    {
        ActiveStyle = new GrKonvaShapeStyle();

        return this;
    }

    public GrKonvaJsLayerComposer UpdateStyle(GrKonvaShapeStyle style)
    {
        ActiveStyle.SetAttributeValues(style);

        return this;
    }
    
    public GrKonvaJsLayerComposer SetStyle(GrKonvaShapeStyle style)
    {
        ActiveStyle = new GrKonvaShapeStyle(style);

        return this;
    }

    public GrKonvaJsLayerComposer SetColorStyle(double lineWidth, Color lineColor)
    {
        return SetStyle(
            GrKonvaShapeStyle.CreateColorStyle(
                lineWidth,
                lineColor
            )
        );
    }

    public GrKonvaJsLayerComposer SetColorStyle(double lineWidth, Color lineColor, Color fillColor)
    {
        return SetStyle(
            GrKonvaShapeStyle.CreateColorStyle(
                lineWidth,
                lineColor,
                fillColor
            )
        );
    }
    
    public GrKonvaJsLayerComposer SetDashedColorStyle(double lineWidth, IReadOnlyList<float> dashArray, Color lineColor)
    {
        return SetStyle(
            GrKonvaShapeStyle.CreateDashedColorStyle(
                lineWidth,
                dashArray,
                lineColor
            )
        );
    }

    public GrKonvaJsLayerComposer SetDashedColorStyle(double lineWidth, IReadOnlyList<float> dashArray, Color lineColor, Color fillColor)
    {
        return SetStyle(
            GrKonvaShapeStyle.CreateDashedColorStyle(
                lineWidth,
                dashArray,
                lineColor,
                fillColor
            )
        );
    }

    
    public GrKonvaJsLayerComposer SetStrokeNone()
    {
        ActiveStyle.SetStrokeNone();

        return this;
    }

    public GrKonvaJsLayerComposer SetStrokeColor()
    {
        ActiveStyle.SetStrokeColor();

        return this;
    }

    public GrKonvaJsLayerComposer SetStrokeColor(Color color)
    {
        ActiveStyle.SetStrokeColor(color);

        return this;
    }
    
    public GrKonvaJsLayerComposer SetStrokeColor(string colorText)
    {
        ActiveStyle.SetStrokeColor(colorText);

        return this;
    }

    public GrKonvaJsLayerComposer SetStrokeLinearGradient()
    {
        ActiveStyle.SetStrokeLinearGradient();

        return this;
    }

    public GrKonvaJsLayerComposer SetStrokeLinearGradient(Color color)
    {
        ActiveStyle.SetStrokeLinearGradient(color);

        return this;
    }
    
    public GrKonvaJsLayerComposer SetStrokeLinearGradient(Color color1, Color color2)
    {
        ActiveStyle.SetStrokeLinearGradient(color1, color2);

        return this;
    }
    
    public GrKonvaJsLayerComposer SetStrokeLinearGradient(params Color[] colorArray)
    {
        ActiveStyle.SetStrokeLinearGradient(colorArray);

        return this;
    }
    
    public GrKonvaJsLayerComposer SetStrokeLinearGradient(GrColorLinearGradientList colorList)
    {
        ActiveStyle.SetStrokeLinearGradient(colorList);

        return this;
    }
    
    public GrKonvaJsLayerComposer SetStrokeLinearGradient(GrColorLinearGradientList colorList, IPair<double> startPoint, IPair<double> endPoint)
    {
        ActiveStyle.SetStrokeLinearGradient(colorList, startPoint, endPoint);

        return this;
    }

    public GrKonvaJsLayerComposer SetStrokeLinearGradient(string colorText)
    {
        ActiveStyle.SetStrokeLinearGradient(colorText);

        return this;
    }
    

    public GrKonvaJsLayerComposer SetFillNone()
    {
        ActiveStyle.SetFillNone();

        return this;
    }

    public GrKonvaJsLayerComposer SetFillColor()
    {
        ActiveStyle.SetFillColor();

        return this;
    }

    public GrKonvaJsLayerComposer SetFillColor(Color color)
    {
        ActiveStyle.SetFillColor(color);

        return this;
    }
    
    public GrKonvaJsLayerComposer SetFillColor(string colorText)
    {
        ActiveStyle.SetFillColor(colorText);

        return this;
    }
    
    public GrKonvaJsLayerComposer SetFillPattern()
    {
        ActiveStyle.SetFillPattern();

        return this;
    }

    public GrKonvaJsLayerComposer SetFillLinearGradient()
    {
        ActiveStyle.SetFillLinearGradient();

        return this;
    }

    public GrKonvaJsLayerComposer SetFillLinearGradient(Color color)
    {
        ActiveStyle.SetFillLinearGradient(color);

        return this;
    }
    
    public GrKonvaJsLayerComposer SetFillLinearGradient(Color color1, Color color2)
    {
        ActiveStyle.SetFillLinearGradient(color1, color2);

        return this;
    }
    
    public GrKonvaJsLayerComposer SetFillLinearGradient(params Color[] colorArray)
    {
        ActiveStyle.SetFillLinearGradient(colorArray);

        return this;
    }
    
    public GrKonvaJsLayerComposer SetFillLinearGradient(GrColorLinearGradientList colorList)
    {
        ActiveStyle.SetFillLinearGradient(colorList);

        return this;
    }
    
    public GrKonvaJsLayerComposer SetFillLinearGradient(GrColorLinearGradientList colorList, IPair<double> startPoint, IPair<double> endPoint)
    {
        ActiveStyle.SetFillLinearGradient(colorList, startPoint, endPoint);

        return this;
    }

    public GrKonvaJsLayerComposer SetFillLinearGradient(string colorText)
    {
        ActiveStyle.SetFillLinearGradient(colorText);

        return this;
    }
    
    public GrKonvaJsLayerComposer SetFillRadialGradient()
    {
        ActiveStyle.SetFillRadialGradient();

        return this;
    }
    
    public GrKonvaJsLayerComposer SetFillRadialGradient(Color color1, Color color2)
    {
        ActiveStyle.SetFillRadialGradient(color1, color2);

        return this;
    }
    
    public GrKonvaJsLayerComposer SetFillRadialGradient(params Color[] colorArray)
    {
        ActiveStyle.SetFillRadialGradient(colorArray);

        return this;
    }
    
    public GrKonvaJsLayerComposer SetFillRadialGradient(GrColorLinearGradientList colorList)
    {
        ActiveStyle.SetFillRadialGradient(colorList);

        return this;
    }
    
    public GrKonvaJsLayerComposer SetFillRadialGradient(GrColorLinearGradientList colorList, IPair<double> startPoint, IPair<double> endPoint, double startRadius, double endRadius)
    {
        ActiveStyle.SetFillRadialGradient(colorList, startPoint, endPoint, startRadius, endRadius);

        return this;
    }

    public GrKonvaJsLayerComposer SetFillRadialGradient(string colorText)
    {
        ActiveStyle.SetFillRadialGradient(colorText);

        return this;
    }


    public GrKonvaJsLayerComposer AddPoint(string name, double pointX, double pointY, double radius)
    {
        var circle = new GrKonvaJsCircle(name);
        
        circle.SetOptions(
            new GrKonvaJsCircleOptions
            {
                Radius = radius,
                X = pointX,
                Y = pointY
            }
        );
        
        circle.Properties.SetAttributeValues(ActiveStyle);
        
        Layer.Add(circle);

        return this;
    }
    
    public GrKonvaJsLayerComposer AddPoint(string name, IPair<Float64Scalar> point, double radius)
    {
        return AddPoint(
            name, 
            point.Item1,
            point.Item2,
            radius
        );
    }

    public GrKonvaJsLayerComposer AddPoints(string name, IEnumerable<IPair<Float64Scalar>> pointList, double radius)
    {
        var i = 0;
        foreach (var point in pointList)
        {
            AddPoint($"{name}{i}", point, radius);
            i++;
        }

        return this;
    }
    
    public GrKonvaJsLayerComposer AddLine(string name, IReadOnlyList<IPair<Float64Scalar>> pointArray, bool closed)
    {
        var line = new GrKonvaJsLine(name);
        
        line.SetOptions(
            new GrKonvaJsLineOptions
            {
                Points = GrKonvaJsVector2ArrayValue.Create(pointArray),
                Closed = closed
            }
        );
        
        line.Properties.SetAttributeValues(ActiveStyle);
        
        Layer.Add(line);

        return this;
    }
    
    public GrKonvaJsLayerComposer AddLine(string name, IReadOnlyList<IPair<Float64Scalar>> pointArray, bool closed, double tension)
    {
        var line = new GrKonvaJsLine(name);
        
        line.SetOptions(
            new GrKonvaJsLineOptions
            {
                Points = GrKonvaJsVector2ArrayValue.Create(pointArray),
                Closed = closed,
                Tension = tension
            }
        );
        
        line.Properties.SetAttributeValues(ActiveStyle);
        
        Layer.Add(line);

        return this;
    }
    
    public GrKonvaJsLayerComposer AddBezierLine(string name, IReadOnlyList<IPair<Float64Scalar>> pointArray)
    {
        var line = new GrKonvaJsLine(name);
        
        line.SetOptions(
            new GrKonvaJsLineOptions
            {
                Points = GrKonvaJsVector2ArrayValue.Create(pointArray),
                Bezier = true
            }
        );
        
        line.Properties.SetAttributeValues(ActiveStyle);
        
        Layer.Add(line);

        return this;
    }

    public GrKonvaJsLayerComposer AddBezierLine(string name, params IPair<Float64Scalar>[] pointArray)
    {
        return AddBezierLine(
            name, 
            (IReadOnlyList<IPair<Float64Scalar>>) pointArray
        );
    }

    public GrKonvaJsLayerComposer AddBezierLine(string name, IReadOnlyList<IPair<Float64Scalar>> pointArray, bool closed)
    {
        var line = new GrKonvaJsLine(name);
        
        line.SetOptions(
            new GrKonvaJsLineOptions
            {
                Points = GrKonvaJsVector2ArrayValue.Create(pointArray),
                Closed = closed,
                Bezier = true
            }
        );
        
        line.Properties.SetAttributeValues(ActiveStyle);
        
        Layer.Add(line);

        return this;
    }
    
    public GrKonvaJsLayerComposer AddClosedBezierLine(string name, params IPair<Float64Scalar>[] pointArray)
    {
        return AddBezierLine(
            name, 
            pointArray, 
            true
        );
    }

    public GrKonvaJsLayerComposer AddLine(string name, IPair<Float64Scalar> point1, IPair<Float64Scalar> point2)
    {
        var line = new GrKonvaJsLine(name);
        
        line.SetOptions(
            new GrKonvaJsLineOptions
            {
                Points = GrKonvaJsVector2ArrayValue.Create(point1, point2)
            }
        );
        
        line.Properties.SetAttributeValues(ActiveStyle);
        
        Layer.Add(line);

        return this;
    }
}