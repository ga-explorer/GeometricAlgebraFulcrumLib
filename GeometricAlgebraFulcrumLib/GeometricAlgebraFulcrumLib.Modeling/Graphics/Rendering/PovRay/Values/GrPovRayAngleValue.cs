using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayAngleValue :
    GrPovRayValue<LinFloat64Angle>,
    IGrPovRayRValue
{
    public static GrPovRayAngleValue Angle0 { get; } 
        = new GrPovRayAngleValue(LinFloat64PolarAngle.Angle0);

    public static GrPovRayAngleValue Angle30 { get; } 
        = new GrPovRayAngleValue(LinFloat64PolarAngle.Angle30);
    
    public static GrPovRayAngleValue Angle45 { get; } 
        = new GrPovRayAngleValue(LinFloat64PolarAngle.Angle45);

    public static GrPovRayAngleValue Angle60 { get; } 
        = new GrPovRayAngleValue(LinFloat64PolarAngle.Angle60);

    public static GrPovRayAngleValue Angle90 { get; } 
        = new GrPovRayAngleValue(LinFloat64PolarAngle.Angle90);
    
    public static GrPovRayAngleValue Angle120 { get; } 
        = new GrPovRayAngleValue(LinFloat64PolarAngle.Angle120);
    
    public static GrPovRayAngleValue Angle135 { get; } 
        = new GrPovRayAngleValue(LinFloat64PolarAngle.Angle135);
    
    public static GrPovRayAngleValue Angle150 { get; } 
        = new GrPovRayAngleValue(LinFloat64PolarAngle.Angle150);
    
    public static GrPovRayAngleValue Angle180 { get; } 
        = new GrPovRayAngleValue(LinFloat64PolarAngle.Angle180);
    
    public static GrPovRayAngleValue Angle210 { get; } 
        = new GrPovRayAngleValue(LinFloat64PolarAngle.Angle210);

    public static GrPovRayAngleValue Angle225 { get; } 
        = new GrPovRayAngleValue(LinFloat64PolarAngle.Angle225);

    public static GrPovRayAngleValue Angle240 { get; } 
        = new GrPovRayAngleValue(LinFloat64PolarAngle.Angle240);

    public static GrPovRayAngleValue Angle270 { get; } 
        = new GrPovRayAngleValue(LinFloat64PolarAngle.Angle270);

    public static GrPovRayAngleValue Angle300 { get; } 
        = new GrPovRayAngleValue(LinFloat64PolarAngle.Angle300);

    public static GrPovRayAngleValue Angle315 { get; } 
        = new GrPovRayAngleValue(LinFloat64PolarAngle.Angle315);

    public static GrPovRayAngleValue Angle330 { get; } 
        = new GrPovRayAngleValue(LinFloat64PolarAngle.Angle330);

    public static GrPovRayAngleValue Angle360 { get; } 
        = new GrPovRayAngleValue(LinFloat64PolarAngle.Angle360);


    public static implicit operator GrPovRayAngleValue(string valueText)
    {
        return new GrPovRayAngleValue(valueText);
    }

    public static implicit operator GrPovRayAngleValue(LinFloat64Angle value)
    {
        return new GrPovRayAngleValue(value);
    }

    public static implicit operator GrPovRayAngleValue(double value)
    {
        return new GrPovRayAngleValue(value.RadiansToPolarAngle());
    }


    private GrPovRayAngleValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayAngleValue(LinFloat64Angle value)
        : base(value)
    {
    }


    public override string GetPovRayCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetPovRayCode() 
            : ValueText;
    }
}