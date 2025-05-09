//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
//using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64;

//namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;

//public static class Float64Vector3DRandomUtils
//{

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static LinVector3D<T> GetVector3D<T>(this Random random)
//    {
//        return new LinSphericalVector3D<T>(
//            random.GetAngle(LinAngle<T>.Angle180),
//            random.GetAngle(),
//            random.NextDouble()
//        ).ToVector3D();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static LinVector3D<T> GetUnitVector3D<T>(this Random random)
//    {
//        return new LinSphericalUnitVector3D<T>(
//            random.GetAngle(LinAngle<T>.Angle180),
//            random.GetAngle()
//        ).ToVector3D();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Pair<LinVector3D<T>> GetOrthonormalVectorPair3D<T>(this Random random)
//    {
//        var v1 = random.GetUnitVector3D();
//        var v2 = random.GetVector3D().RejectOnUnitVector(v1).ToUnitVector();

//        return new Pair<LinVector3D<T>>(v1, v2);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static LinBivector3D<T> GetBivector3D<T>(this Random random)
//    {
//        return LinBivector3D<T>.Create(
//            random.NextDouble(),
//            random.NextDouble(),
//            random.NextDouble()
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static LinTrivector3D<T> GetTrivector3D<T>(this Random random)
//    {
//        return LinTrivector3D<T>.Create(
//            random.NextDouble()
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static LinQuaternion<T> GetQuaternion<T>(this Random random)
//    {
//        return LinQuaternion<T>.Create(
//            random.NextDouble(),
//            random.NextDouble(),
//            random.NextDouble(),
//            random.NextDouble()
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static LinVector3D<T> GetLinearCombination<T>(this Random random, LinVector3D<T> vector1, LinVector3D<T> vector2)
//    {
//        return vector1 * random.NextDouble() +
//               vector2 * random.NextDouble();
//    }

//}