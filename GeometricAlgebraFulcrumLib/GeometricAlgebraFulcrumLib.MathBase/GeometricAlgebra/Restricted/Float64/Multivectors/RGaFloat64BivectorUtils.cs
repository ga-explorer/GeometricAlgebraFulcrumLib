using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;

public static class RGaFloat64BivectorUtils
{
    public static double[] BivectorToArray1D(this RGaFloat64Bivector bivector)
    {
        var array = new double[(int) bivector.KvSpaceDimensions];

        foreach (var (id, scalar) in bivector.IdScalarPairs)
        {
            var (index1, index2) = id.FirstLastOneBitPosition();

            var index = (int)BinaryCombinationsUtilsUInt64.CombinadicToIndex(index1, index2);

            array[index] = scalar;
        }

        return array;
    }

    public static double[] VectorToArray1D(this RGaFloat64Bivector bivector, int arraySize)
    {
        if ((ulong) arraySize < bivector.KvSpaceDimensions)
            throw new InvalidOperationException();

        var array = new double[arraySize];

        foreach (var (id, scalar) in bivector.IdScalarPairs)
        {
            var (index1, index2) = id.FirstLastOneBitPosition();

            var index = (int)BinaryCombinationsUtilsUInt64.CombinadicToIndex(index1, index2);

            array[index] = scalar;
        }

        return array;
    }

    public static double[,] BivectorToArray2D(this RGaFloat64Bivector bivector)
    {
        var arraySize = bivector.VSpaceDimensions;

        var array = new double[arraySize, arraySize];

        foreach (var (id, scalar) in bivector.IdScalarPairs)
        {
            var (index1, index2) = id.FirstLastOneBitPosition();
            
            array[index1, index2] = scalar;
            array[index2, index1] = -scalar;
        }

        return array;
    }
    
    public static double[,] BivectorToArray2D(this RGaFloat64Bivector bivector, int arraySize)
    {
        if (arraySize < bivector.VSpaceDimensions)
            throw new InvalidOperationException();

        var array = new double[arraySize, arraySize];

        foreach (var (id, scalar) in bivector.IdScalarPairs)
        {
            var (index1, index2) = id.FirstLastOneBitPosition();
            
            array[index1, index2] = scalar;
            array[index2, index1] = -scalar;
        }

        return array;
    }
}