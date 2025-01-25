using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayColorValue :
    GrPovRayValue<IQuint<GrPovRayFloat32Value>>,
    IGrPovRayRValue
{
    public static GrPovRayColorValue Rgb(GrPovRayFloat32Value rgb)
    {
        var value = new Quint<GrPovRayFloat32Value>(
            rgb,
            rgb,
            rgb,
            GrPovRayFloat32Value.Zero,
            GrPovRayFloat32Value.Zero
        );

        return new GrPovRayColorValue(value);
    }
    
    public static GrPovRayColorValue Rgb(GrPovRayFloat32Value red, GrPovRayFloat32Value green, GrPovRayFloat32Value blue)
    {
        var value = new Quint<GrPovRayFloat32Value>(
            red,
            green,
            blue,
            GrPovRayFloat32Value.Zero,
            GrPovRayFloat32Value.Zero
        );

        return new GrPovRayColorValue(value);
    }
    
    public static GrPovRayColorValue Rgbf(GrPovRayFloat32Value rgb, GrPovRayFloat32Value filter)
    {
        var value = new Quint<GrPovRayFloat32Value>(
            rgb,
            rgb,
            rgb,
            filter,
            GrPovRayFloat32Value.Zero
        );

        return new GrPovRayColorValue(value);
    }

    public static GrPovRayColorValue Rgbf(GrPovRayFloat32Value red, GrPovRayFloat32Value green, GrPovRayFloat32Value blue, GrPovRayFloat32Value filter)
    {
        var value = new Quint<GrPovRayFloat32Value>(
            red,
            green,
            blue,
            filter,
            GrPovRayFloat32Value.Zero
        );

        return new GrPovRayColorValue(value);
    }
    
    public static GrPovRayColorValue Rgbt(GrPovRayFloat32Value rgb, GrPovRayFloat32Value transmit)
    {
        var value = new Quint<GrPovRayFloat32Value>(
            rgb,
            rgb,
            rgb,
            GrPovRayFloat32Value.Zero,
            transmit
        );

        return new GrPovRayColorValue(value);
    }

    public static GrPovRayColorValue Rgbt(GrPovRayFloat32Value red, GrPovRayFloat32Value green, GrPovRayFloat32Value blue, GrPovRayFloat32Value transmit)
    {
        var value = new Quint<GrPovRayFloat32Value>(
            red,
            green,
            blue,
            GrPovRayFloat32Value.Zero,
            transmit
        );

        return new GrPovRayColorValue(value);
    }

    public static GrPovRayColorValue Rgbft(GrPovRayFloat32Value red, GrPovRayFloat32Value green, GrPovRayFloat32Value blue, GrPovRayFloat32Value filter, GrPovRayFloat32Value transmit)
    {
        var value = new Quint<GrPovRayFloat32Value>(
            red,
            green,
            blue,
            filter,
            transmit
        );

        return new GrPovRayColorValue(value);
    }

    public static GrPovRayColorValue Rgbft(IQuint<GrPovRayFloat32Value> value)
    {
        return new GrPovRayColorValue(value);
    }


    public static implicit operator GrPovRayColorValue(string valueText)
    {
        return new GrPovRayColorValue(valueText);
    }
    
    public static implicit operator GrPovRayColorValue(double value)
    {
        Debug.Assert(!double.IsNaN(value) && value >= 0 && value <= 1);

        var value1 = new Quint<GrPovRayFloat32Value>(
            value,
            value,
            value,
            0f,
            0f
        );

        return new GrPovRayColorValue(value1);
    }

    public static implicit operator GrPovRayColorValue(System.Drawing.Color value)
    {
        var value1 = new Quint<GrPovRayFloat32Value>(
            value.R / 255f,
            value.G / 255f,
            value.B / 255f,
            0f,
            1f - value.A / 255f
        );

        return new GrPovRayColorValue(value1);
    }

    public static implicit operator GrPovRayColorValue(Color value)
    {
        var color = value.ToPixel<Rgba32>();

        var value1 = new Quint<GrPovRayFloat32Value>(
            color.R / 255f,
            color.G / 255f,
            color.B / 255f,
            0f,
            1f - color.A / 255f
        );

        return new GrPovRayColorValue(value1);
    }
    
    public static implicit operator GrPovRayColorValue(Quint<GrPovRayFloat32Value> value)
    {
        return new GrPovRayColorValue(value);
    }


    public GrPovRayFloat32Value Red 
        => this.HasValue() ? Value.Item1 : GrPovRayFloat32Value.Zero;

    public GrPovRayFloat32Value Green 
        => this.HasValue() ? Value.Item2 : GrPovRayFloat32Value.Zero;

    public GrPovRayFloat32Value Blue 
        => this.HasValue() ? Value.Item3 : GrPovRayFloat32Value.Zero;

    public GrPovRayFloat32Value Filter 
        => this.HasValue() ? Value.Item4 : GrPovRayFloat32Value.Zero;

    public GrPovRayFloat32Value Transmit 
        => this.HasValue() ? Value.Item5 : GrPovRayFloat32Value.Zero;


    private GrPovRayColorValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayColorValue(IQuint<GrPovRayFloat32Value> value)
        : base(value)
    {
    }
    
    
    public override string GetPovRayCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetPovRayCodeRgbft() 
            : ValueText;
    }
}