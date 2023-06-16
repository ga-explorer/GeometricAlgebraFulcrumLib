using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;

public static class MathNetNumericsUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MathNet.Numerics.LinearAlgebra.Vector<double> ToVector(this double[] array)
    {
        return MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfArray(array);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MathNet.Numerics.LinearAlgebra.Vector<double> ToVector(this IEnumerable<double> scalarList)
    {
        return MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfEnumerable(scalarList);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MathNet.Numerics.LinearAlgebra.Vector<double> ToVector(this IReadOnlyDictionary<int, double> scalarList, int size)
    {
        var array = new double[size];

        foreach (var (i, s) in scalarList)
            array[i] = s;

        return MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfArray(array);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector ToTuple(this MathNet.Numerics.LinearAlgebra.Vector<double> vector)
    {
        return vector.ToArray().CreateTuple();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetVectorNorm(this MathNet.Numerics.LinearAlgebra.Vector<double> vector)
    {
        return vector.DotProduct(vector).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetVectorNormSquared(this MathNet.Numerics.LinearAlgebra.Vector<double> vector)
    {
        return vector.DotProduct(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double VectorDot(this MathNet.Numerics.LinearAlgebra.Vector<double> vector1, MathNet.Numerics.LinearAlgebra.Vector<double> vector2)
    {
        return vector1.DotProduct(vector2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Matrix<double> GetOrthogonalMatrix(this System.Random random, int size)
    {
        return random
            .GetFloat64Array2D(size, size)
            .ToMatrix()
            .QR()
            .Q;
    }

    public static IReadOnlyList<Vector<double>> GetOrthonormalVectors(this System.Random random, int size, int count)
    {
        count = Math.Min(count, size);

        var matrix = random.GetOrthogonalMatrix(size);

        var vectorArray = new Vector<double>[count];

        for (var i = 0; i < count; i++)
            vectorArray[i] = matrix.Column(i);

        return vectorArray;
    }
}