using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayVector3Value :
    GrPovRayValue<ITriplet<GrPovRayFloat32Value>>,
    IGrPovRayRValue
{
    public static GrPovRayVector3Value Zero { get; }
        = new GrPovRayVector3Value(LinFloat64Vector3D.Zero);

    public static GrPovRayVector3Value E1 { get; }
        = new GrPovRayVector3Value(LinFloat64Vector3D.E1);

    public static GrPovRayVector3Value E2 { get; }
        = new GrPovRayVector3Value(LinFloat64Vector3D.E2);

    public static GrPovRayVector3Value E3 { get; }
        = new GrPovRayVector3Value(LinFloat64Vector3D.E3);
    
    public static GrPovRayVector3Value NegativeE1 { get; }
        = new GrPovRayVector3Value(LinFloat64Vector3D.NegativeE1);

    public static GrPovRayVector3Value NegativeE2 { get; }
        = new GrPovRayVector3Value(LinFloat64Vector3D.NegativeE2);

    public static GrPovRayVector3Value NegativeE3 { get; }
        = new GrPovRayVector3Value(LinFloat64Vector3D.NegativeE3);

    
    public static GrPovRayVector3Value Create(ITriplet<GrPovRayFloat32Value> value)
    {
        return new GrPovRayVector3Value(value);
    }

    public static GrPovRayVector3Value Create(ITriplet<Float64Scalar> value)
    {
        return new GrPovRayVector3Value(value);
    }
    
    public static GrPovRayVector3Value Create(int x, int y, int z)
    {
        return new GrPovRayVector3Value(
            new Triplet<Float64Scalar>(x, y, z)
        );
    }

    public static GrPovRayVector3Value Create(double x, double y, double z)
    {
        return new GrPovRayVector3Value(
            new Triplet<Float64Scalar>(x, y, z)
        );
    }

    public static GrPovRayVector3Value Create(Float64Scalar x, Float64Scalar y, Float64Scalar z)
    {
        return new GrPovRayVector3Value(
            new Triplet<Float64Scalar>(x, y, z)
        );
    }
    
    public static GrPovRayVector3Value Create(GrPovRayFloat32Value x, GrPovRayFloat32Value y, GrPovRayFloat32Value z)
    {
        return new GrPovRayVector3Value(
            new Triplet<GrPovRayFloat32Value>(x, y, z)
        );
    }


    public static implicit operator GrPovRayVector3Value(string valueText)
    {
        return new GrPovRayVector3Value(valueText);
    }

    public static implicit operator GrPovRayVector3Value(LinFloat64Vector3D value)
    {
        return new GrPovRayVector3Value(value);
    }
    
    public static implicit operator GrPovRayVector3Value(LinFloat64SphericalVector3D value)
    {
        return new GrPovRayVector3Value(value);
    }
    
    public static implicit operator GrPovRayVector3Value(LinFloat64SphericalUnitVector3D value)
    {
        return new GrPovRayVector3Value(value);
    }


    private GrPovRayVector3Value(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayVector3Value(ITriplet<Float64Scalar> value)
        : base(value.MapItems(s => (GrPovRayFloat32Value)s))
    {
    }
    
    private GrPovRayVector3Value(ITriplet<GrPovRayFloat32Value> value)
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