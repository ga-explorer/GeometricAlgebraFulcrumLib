using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64;

public static class MathNetNumericsUtils
{

    
    public static Vector<double> ToMathNetVector(this double[] array)
    {
        return Vector<double>.Build.DenseOfArray(array);
    }

    
    public static Vector<double> ToMathNetVector(this IEnumerable<double> scalarList)
    {
        return Vector<double>.Build.DenseOfEnumerable(scalarList);
    }

    
    public static Vector<double> ToMathNetVector(this IReadOnlyDictionary<int, double> scalarList, int size)
    {
        var array = new double[size];

        foreach (var (i, s) in scalarList.ToTuples())
            array[i] = s;

        return Vector<double>.Build.DenseOfArray(array);
    }

    
    public static LinFloat64Vector ToVector(this Vector<double> vector)
    {
        return vector.ToArray().ToLinVector();
    }


    
    public static double GetVectorNorm(this Vector<double> vector)
    {
        return vector.DotProduct(vector).Sqrt();
    }

    
    public static double GetVectorNormSquared(this Vector<double> vector)
    {
        return vector.DotProduct(vector);
    }

    
    public static double VectorDot(this Vector<double> vector1, Vector<double> vector2)
    {
        return vector1.DotProduct(vector2);
    }

    
    public static Matrix<double> GetMathNetOrthogonalMatrix(this Random random, int size)
    {
        return random
            .GetFloat64Array2D(size, size)
            .ToMatrix()
            .QR()
            .Q;
    }

    public static IReadOnlyList<Vector<double>> GetMathNetOrthonormalVectors(this Random random, int size, int count)
    {
        count = Math.Min(count, size);

        var matrix = random.GetMathNetOrthogonalMatrix(size);

        var vectorArray = new Vector<double>[count];

        for (var i = 0; i < count; i++)
            vectorArray[i] = matrix.Column(i);

        return vectorArray;
    }
}