//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

//namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

//public sealed class GrPovRayQuaternionValue :
//    GrPovRayValue<LinFloat64Quaternion>
//{
//    internal static GrPovRayQuaternionValue Create(LinFloat64Quaternion value)
//    {
//        return new GrPovRayQuaternionValue(value);
//    }


//    public static implicit operator GrPovRayQuaternionValue(string valueText)
//    {
//        return new GrPovRayQuaternionValue(valueText);
//    }

//    public static implicit operator GrPovRayQuaternionValue(LinFloat64Quaternion value)
//    {
//        return new GrPovRayQuaternionValue(value);
//    }


//    private GrPovRayQuaternionValue(string valueText)
//        : base(valueText)
//    {
//    }

//    private GrPovRayQuaternionValue(LinFloat64Quaternion value)
//        : base(value)
//    {
//    }


//    public override string GetPovRayCode()
//    {
//        return string.IsNullOrEmpty(ValueText) 
//            ? Value.GetPovRayCode() 
//            : ValueText;
//    }
//}