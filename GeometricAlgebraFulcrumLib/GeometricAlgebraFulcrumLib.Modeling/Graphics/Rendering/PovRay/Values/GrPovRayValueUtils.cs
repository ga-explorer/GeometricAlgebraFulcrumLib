using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public static class GrPovRayValueUtils
{
    
    public static float ToFloat32(this GrPovRayFloat32Value? value)
    {
        return value is null || value.IsEmpty
            ? throw new InvalidOperationException()
            : value.Value;
    }

    public static double ToFloat64(this GrPovRayFloat32Value? value)
    {
        return value is null || value.IsEmpty
            ? throw new InvalidOperationException()
            : value.Value;
    }
    
    public static Float64Scalar ToFloat64Scalar(this GrPovRayFloat32Value? value)
    {
        return value is null || value.IsEmpty
            ? throw new InvalidOperationException()
            : value.Value;
    }

    
    public static LinFloat64Vector3D ToLinVector3D(this GrPovRayVector3Value? value)
    {
        if (value is null || value.IsEmpty)
            throw new InvalidOperationException();

        var x = value.Value.Item1.ToFloat64();
        var y = value.Value.Item2.ToFloat64();
        var z = value.Value.Item3.ToFloat64();

        return LinFloat64Vector3D.Create(x, y, z);
    }
    
    public static Float64Scalar NormSquared(this GrPovRayVector3Value? value)
    {
        if (value is null || value.IsEmpty)
            throw new InvalidOperationException();

        var x = value.Value.Item1.ToFloat64Scalar();
        var y = value.Value.Item2.ToFloat64Scalar();
        var z = value.Value.Item3.ToFloat64Scalar();

        return x * x + y * y + z * z;
    }

    public static Float64Scalar Norm(this GrPovRayVector3Value? value)
    {
        if (value is null || value.IsEmpty)
            throw new InvalidOperationException();

        var x = value.Value.Item1.ToFloat64Scalar();
        var y = value.Value.Item2.ToFloat64Scalar();
        var z = value.Value.Item3.ToFloat64Scalar();

        return (x * x + y * y + z * z).Sqrt();
    }


    internal static string? GetValueCode<T>(this SparseCodeAttributeValue<T>? value)
    {
        return value is null || value.IsEmpty
            ? null 
            : value.GetAttributeValueCode();
    }

    internal static Pair<string>? GetNameValueCodePair(this GrPovRayCodeValue? value, string name)
    {
        return value is null || value.IsEmpty
            ? null 
            : new Pair<string>(name, value.GetAttributeValueCode());
    }

    internal static Pair<string>? GetNameValueCodePair<T>(this SparseCodeAttributeValue<T>? value, string name)
    {
        return value is null || value.IsEmpty
            ? null 
            : new Pair<string>(name, value.GetAttributeValueCode());
    }
        
    internal static Pair<string>? GetNameValueCodePair<T>(this SparseCodeAttributeValue<T>? value, string name, SparseCodeAttributeValue<T>? defaultValue)
    {
        return value is null || value.IsEmpty
            ? defaultValue.GetNameValueCodePair(name) 
            : new Pair<string>(name, value.GetAttributeValueCode());
    }


    public static GrPovRayVector3Value ToPovRayVector3Value(this ILinFloat64Vector3D value)
    {
        return GrPovRayVector3Value.Create(value);
    }

    public static bool HasValue(this SparseCodeAttributeValue? value)
    {
        return value is not null && !value.IsEmpty;
    }
        
    public static bool IsNullOrEmpty(this SparseCodeAttributeValue? value)
    {
        return value is null || value.IsEmpty;
    }



}