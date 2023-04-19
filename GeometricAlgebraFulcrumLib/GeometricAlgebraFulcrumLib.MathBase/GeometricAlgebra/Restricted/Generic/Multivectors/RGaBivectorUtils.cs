using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

public static class RGaBivectorUtils
{
    public static T[] BivectorToArray1D<T>(this RGaBivector<T> bivector)
    {
        var array = bivector
            .ScalarProcessor
            .CreateArrayZero1D((int) bivector.KvSpaceDimensions);

        foreach (var (id, scalar) in bivector.IdScalarPairs)
        {
            var (index1, index2) = id.FirstLastOneBitPosition();

            var index = (int)BinaryCombinationsUtilsUInt64.CombinadicToIndex(index1, index2);

            array[index] = scalar;
        }

        return array;
    }

    public static T[] BivectorToArray1D<T>(this RGaBivector<T> bivector, int arraySize)
    {
        if ((ulong) arraySize < bivector.KvSpaceDimensions)
            throw new InvalidOperationException();

        var array = bivector
            .ScalarProcessor
            .CreateArrayZero1D(arraySize);

        foreach (var (id, scalar) in bivector.IdScalarPairs)
        {
            var (index1, index2) = id.FirstLastOneBitPosition();

            var index = (int)BinaryCombinationsUtilsUInt64.CombinadicToIndex(index1, index2);

            array[index] = scalar;
        }

        return array;
    }

    public static T[,] BivectorToArray2D<T>(this RGaBivector<T> bivector)
    {
        var array = bivector
            .ScalarProcessor
            .CreateArrayZero2D(bivector.VSpaceDimensions);

        foreach (var (id, scalar) in bivector.IdScalarPairs)
        {
            var (index1, index2) = id.FirstLastOneBitPosition();
            
            array[index1, index2] = scalar;
            array[index2, index1] = bivector.ScalarProcessor.Negative(scalar);
        }

        return array;
    }
    
    public static T[,] BivectorToArray2D<T>(this RGaBivector<T> bivector, int arraySize)
    {
        if (arraySize < bivector.VSpaceDimensions)
            throw new InvalidOperationException();

        var array = bivector
            .ScalarProcessor
            .CreateArrayZero2D(arraySize);

        foreach (var (id, scalar) in bivector.IdScalarPairs)
        {
            var (index1, index2) = id.FirstLastOneBitPosition();
            
            array[index1, index2] = scalar;
            array[index2, index1] = bivector.ScalarProcessor.Negative(scalar);
        }

        return array;
    }
}