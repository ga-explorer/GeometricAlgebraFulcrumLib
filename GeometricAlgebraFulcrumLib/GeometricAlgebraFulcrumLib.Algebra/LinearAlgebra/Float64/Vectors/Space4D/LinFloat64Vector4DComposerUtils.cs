using System.Numerics;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;

public static class LinFloat64Vector4DComposerUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ToLinVector4D(this IQuad<double> vector)
    {
        return vector is LinFloat64Vector4D vector4D
            ? vector4D
            : LinFloat64Vector4D.Create(vector.Item1, vector.Item2, vector.Item3, vector.Item4);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ToLinVector4D(this IPair<double> tuple)
    {
        return LinFloat64Vector4D.Create(
            tuple.Item1,
            tuple.Item2,
            Float64Scalar.Zero,
            Float64Scalar.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ToLinVector4D(this IntTuple2D tuple)
    {
        return LinFloat64Vector4D.Create(tuple.Item1, tuple.Item2, 0.0d, 0.0d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ToLinVector4D(this ITriplet<double> tuple)
    {
        return LinFloat64Vector4D.Create(
            tuple.Item1,
            tuple.Item2,
            tuple.Item3,
            Float64Scalar.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ToLinVector4D(this IntTuple3D tuple)
    {
        return LinFloat64Vector4D.Create(tuple.ItemX, tuple.ItemY, tuple.ItemZ, 0.0d);
    }
    
    public static LinFloat64Vector4D ToLinVector4D(this IEnumerable<double> scalarList)
    {
        var scalarArray = new double[4];

        var i = 0;
        foreach (var scalar in scalarList)
            scalarArray[i++] = scalar;

        return LinFloat64Vector4D.Create(
            scalarArray[0],
            scalarArray[1],
            scalarArray[2],
            scalarArray[3]
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinFloat64Vector4D> ToLinVector4DSequence(this IEnumerable<IQuad<double>> vectorList)
    {
        return vectorList.Select(v => v.ToLinVector4D());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<LinFloat64Vector4D> ToLinVector4DList(this IReadOnlyList<IQuad<double>> vectorList)
    {
        return vectorList.Select(v => v.ToLinVector4D()).ToArray();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D[] ToLinVector4DList(this IQuad<double>[] vectorList)
    {
        return vectorList.Select(v => v.ToLinVector4D()).ToArray();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D RealPartToLinVector4D(this IQuad<Complex> tuple)
    {
        return LinFloat64Vector4D.Create(
            tuple.Item1.Real,
            tuple.Item2.Real,
            tuple.Item3.Real,
            tuple.Item4.Real
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D ImaginaryPartToLinVector4D(this IQuad<Complex> tuple)
    {
        return LinFloat64Vector4D.Create(
            tuple.Item1.Imaginary,
            tuple.Item2.Imaginary,
            tuple.Item3.Imaginary,
            tuple.Item4.Imaginary
        );
    }


}