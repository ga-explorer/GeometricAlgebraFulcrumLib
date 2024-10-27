using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding
{
    public static class CGaFloat64EncoderUtils
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaFloat64Blade EncodeVGaVector(this ILinFloat64Vector2D egaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
        {
            return cgaGeometricSpace.Encode.VGa.Vector(egaKVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaFloat64Blade EncodeVGaVector(this ILinFloat64Vector3D egaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
        {
            return cgaGeometricSpace.Encode.VGa.Vector(egaKVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaFloat64Blade EncodeVGaVector(this LinFloat64Vector egaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
        {
            return cgaGeometricSpace.Encode.VGa.Vector(egaKVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaFloat64Blade EncodeVGaVector(this RGaFloat64Vector egaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
        {
            return cgaGeometricSpace.Encode.VGa.Vector(egaKVector);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaFloat64Blade EncodeVGaBivector(this LinFloat64Bivector2D egaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
        {
            return cgaGeometricSpace.Encode.VGa.Bivector(egaKVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaFloat64Blade EncodeVGaBivector(this LinFloat64Bivector3D egaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
        {
            return cgaGeometricSpace.Encode.VGa.Bivector(egaKVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaFloat64Blade EncodeVGaBivector(this RGaFloat64Bivector egaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
        {
            return cgaGeometricSpace.Encode.VGa.Bivector(egaKVector);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaFloat64Blade EncodeVGaTrivector(this LinFloat64Trivector3D egaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
        {
            return cgaGeometricSpace.Encode.VGa.Trivector(egaKVector);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaFloat64Blade EncodeVGaBlade(this RGaFloat64KVector egaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
        {
            return cgaGeometricSpace.Encode.VGa.Blade(egaKVector);
        }

    }
}
