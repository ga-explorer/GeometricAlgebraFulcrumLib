using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayVector2Value :
    GrPovRayValue<IPair<GrPovRayFloat32Value>>,
    IGrPovRayRValue
{
    public static GrPovRayVector2Value Zero { get; }
        = new GrPovRayVector2Value(LinFloat64Vector2D.Zero);

    public static GrPovRayVector2Value E1 { get; }
        = new GrPovRayVector2Value(LinFloat64Vector2D.E1);

    public static GrPovRayVector2Value E2 { get; }
        = new GrPovRayVector2Value(LinFloat64Vector2D.E2);
    
    public static GrPovRayVector2Value NegativeE1 { get; }
        = new GrPovRayVector2Value(LinFloat64Vector2D.NegativeE1);

    public static GrPovRayVector2Value NegativeE2 { get; }
        = new GrPovRayVector2Value(LinFloat64Vector2D.NegativeE2);
    
    
    internal static GrPovRayVector2Value Create(IPair<GrPovRayFloat32Value> value)
    {
        return new GrPovRayVector2Value(value);
    }
    
    internal static GrPovRayVector2Value Create(IPair<Float64Scalar> value)
    {
        return new GrPovRayVector2Value(value);
    }
    
    public static GrPovRayVector2Value Create(int x, int y)
    {
        return new GrPovRayVector2Value(
            new Pair<Float64Scalar>(x, y)
        );
    }

    public static GrPovRayVector2Value Create(double x, double y)
    {
        return new GrPovRayVector2Value(
            new Pair<Float64Scalar>(x, y)
        );
    }

    public static GrPovRayVector2Value Create(Float64Scalar x, Float64Scalar y)
    {
        return new GrPovRayVector2Value(
            new Pair<Float64Scalar>(x, y)
        );
    }
    
    public static GrPovRayVector2Value Create(GrPovRayFloat32Value x, GrPovRayFloat32Value y)
    {
        return new GrPovRayVector2Value(
            new Pair<GrPovRayFloat32Value>(x, y)
        );
    }


    public static implicit operator GrPovRayVector2Value(string valueText)
    {
        return new GrPovRayVector2Value(valueText);
    }

    public static implicit operator GrPovRayVector2Value(LinFloat64Vector2D value)
    {
        return new GrPovRayVector2Value(value);
    }
    
    public static implicit operator GrPovRayVector2Value(LinFloat64PolarVector2D value)
    {
        return new GrPovRayVector2Value(value);
    }
    

    private GrPovRayVector2Value(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayVector2Value(IPair<Float64Scalar> value)
        : base(value.MapItems(s => (GrPovRayFloat32Value) s))
    {
    }
    
    private GrPovRayVector2Value(IPair<GrPovRayFloat32Value> value)
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