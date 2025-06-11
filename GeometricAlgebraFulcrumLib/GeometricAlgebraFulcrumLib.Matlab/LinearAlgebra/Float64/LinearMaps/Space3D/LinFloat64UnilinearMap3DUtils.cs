using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Composers;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Rotation;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D;

public static class LinFloat64UnilinearMap3DUtils
{
    
    public static double[,] ToArray2D(this ILinFloat64UnilinearMap3D map)
    {
        return map.ToArray2D(3, 3);
    }

    
    public static double[,] ToArray2D(this ILinFloat64UnilinearMap3D map, int size)
    {
        return map.ToArray2D(size, size);
    }

    
    public static double[,] ToArray2D(this ILinFloat64UnilinearMap3D map, int rowCount, int colCount)
    {
        if (rowCount < 3 || colCount < 3)
            throw new InvalidOperationException();

        var matrix = new double[rowCount, colCount];

        var vector0 = map.MapBasisVector(0);
        var vector1 = map.MapBasisVector(1);
        var vector2 = map.MapBasisVector(2);

        matrix[0, 0] = vector0.X;
        matrix[0, 1] = vector1.X;
        matrix[0, 2] = vector2.X;

        matrix[1, 0] = vector0.Y;
        matrix[1, 1] = vector1.Y;
        matrix[1, 2] = vector2.Y;

        matrix[2, 0] = vector0.Z;
        matrix[2, 1] = vector1.Z;
        matrix[2, 2] = vector2.Z;

        return matrix;
    }

    
    public static SquareMatrix3 ToSquareMatrix3(this ILinFloat64UnilinearMap3D map)
    {
        var matrix = new SquareMatrix3();

        matrix.SetColumn(0, map.MapBasisVector(0));
        matrix.SetColumn(1, map.MapBasisVector(1));
        matrix.SetColumn(2, map.MapBasisVector(2));

        return matrix;
    }

    
    public static Matrix<double> ToMatrix(this ILinFloat64UnilinearMap3D map)
    {
        return map.ToMatrix(3, 3);
    }

    
    public static Matrix<double> ToMatrix(this ILinFloat64UnilinearMap3D map, int size)
    {
        return map.ToMatrix(size, size);
    }

    
    public static Matrix<double> ToMatrix(this ILinFloat64UnilinearMap3D map, int rowCount, int colCount)
    {
        if (rowCount < 3 || colCount < 3)
            throw new InvalidOperationException();

        var matrix = Matrix<double>.Build.Dense(rowCount, colCount);

        var vector0 = map.MapBasisVector(0);
        var vector1 = map.MapBasisVector(1);
        var vector2 = map.MapBasisVector(2);

        matrix[0, 0] = vector0.X;
        matrix[0, 1] = vector1.X;
        matrix[0, 2] = vector2.X;

        matrix[1, 0] = vector0.Y;
        matrix[1, 1] = vector1.Y;
        matrix[1, 2] = vector2.Y;

        matrix[2, 0] = vector0.Z;
        matrix[2, 1] = vector1.Z;
        matrix[2, 2] = vector2.Z;

        return matrix;
    }

    
    public static IEnumerable<LinFloat64Vector3D> GetMappedBasisVectors(this ILinFloat64UnilinearMap3D map)
    {
        yield return map.MapBasisVector(0);
        yield return map.MapBasisVector(1);
        yield return map.MapBasisVector(2);
    }





    
    public static LinFloat64Vector3D MapVector(this double[,] matrix, LinFloat64Vector3D vector)
    {
        var composer = LinFloat64Vector3DComposer.Create();

        var rowCount = matrix.GetLength(0);
        var colCount = matrix.GetLength(1);

        for (var colIndex = 0; colIndex < 3; colIndex++)
        {
            var scalar = vector[1 << colIndex];

            if (colIndex >= colCount)
                continue;

            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                composer.AddTerm(
                    rowIndex,
                    scalar * matrix[rowIndex, colIndex]
                );
        }

        return composer.GetVector();
    }

    
    public static LinFloat64Vector3D MapVector(this double[,] matrix, IEnumerable<double> vector)
    {
        var composer = LinFloat64Vector3DComposer.Create();

        var rowCount = matrix.GetLength(0);
        var colCount = matrix.GetLength(1);

        var colIndex = 0;
        foreach (var scalar in vector)
        {
            if (colIndex >= colCount)
                return composer.GetVector();

            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                composer.AddTerm(
                    rowIndex,
                    scalar * matrix[rowIndex, colIndex]
                );

            colIndex++;
        }

        return composer.GetVector();
    }

    
    public static LinFloat64Vector3D MapVector(this double[,] matrix, IReadOnlyDictionary<int, double> vector)
    {
        var composer = LinFloat64Vector3DComposer.Create();

        var rowCount = matrix.GetLength(0);
        var colCount = matrix.GetLength(1);

        foreach (var (colIndex, scalar) in vector.ToTuples())
        {
            if (colIndex >= colCount)
                continue;

            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                composer.AddTerm(
                    rowIndex,
                    scalar * matrix[rowIndex, colIndex]
                );
        }

        return composer.GetVector();
    }

    //
    //public static Float64Tuple3D MapVectorUsing(this IReadOnlyList<double> vector, LinFloat64UnilinearMap map)
    //{
    //    return map.MapVector(vector);
    //}

    //
    //public static Float64Tuple3D MapVectorUsing(this IReadOnlyDictionary<int, double> vector, LinFloat64UnilinearMap map)
    //{
    //    return map.MapVector(vector);
    //}


    
    public static LinFloat64UnilinearMap CreateVectorToVectorRotationMatrix(this IReadOnlyDictionary<int, double> sourceVector, IReadOnlyDictionary<int, double> targetVector, int basisVectorIndex, int vSpaceDimensions)
    {
        var map2 = sourceVector.CreateVectorToBasisRotationMap(basisVectorIndex, vSpaceDimensions);
        var map1 = basisVectorIndex.CreateBasisToVectorRotationMap(targetVector, vSpaceDimensions);

        return map1.Map(map2);
    }

    public static LinFloat64UnilinearMap CreateBasisToVectorRotationMap(this int basisVectorIndex, IReadOnlyDictionary<int, double> unitVector, int vSpaceDimensions)
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        var v1 = unitVector.GetVectorTermScalar(basisVectorIndex);

        // Special case: unitVector == e_{basisVectorIndex}
        if (v1.IsOne())
            return vSpaceDimensions.CreateIdentityLinUnilinearMap();

        // Special case: unitVector == -e_{basisVectorIndex}
        if (v1.IsMinusOne())
        {
            //TODO: Handle this case
            throw new InvalidOperationException();
        }

        var map =
            new LinFloat64UnilinearMapComposer();

        foreach (var (index, scalar) in unitVector.ToTuples())
        {
            // Fill column number basisVectorIndex
            map[index, basisVectorIndex] = scalar;

            if (index == basisVectorIndex)
                continue;

            // Fill row number basisVectorIndex
            map[basisVectorIndex, index] =
                -scalar;

            map[index, index] = 1d - scalar * scalar / v1;

            foreach (var (index2, scalar2) in unitVector.ToTuples())
            {
                if (index2 == basisVectorIndex || index2 == index)
                    continue;

                map[index, index2] = -(scalar * scalar2) / v1;
            }
        }

        return map.GetMap();
    }

    public static LinFloat64UnilinearMap CreateVectorToBasisRotationMap(this IReadOnlyDictionary<int, double> unitVector, int basisVectorIndex, int vSpaceDimensions)
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        var v1 = unitVector.GetVectorTermScalar(basisVectorIndex);

        // Special case: unitVector == e_{basisVectorIndex}
        if (v1.IsOne())
            return vSpaceDimensions.CreateIdentityLinUnilinearMap();

        // Special case: unitVector == -e_{basisVectorIndex}
        if (v1.IsMinusOne())
        {
            //TODO: Handle this case
            throw new InvalidOperationException();
        }

        var map =
            new LinFloat64UnilinearMapComposer();

        foreach (var (index, scalar) in unitVector.ToTuples())
        {
            // Fill row number basisVectorIndex
            map[basisVectorIndex, index] = scalar;

            if (index == basisVectorIndex)
                continue;

            // Fill column number basisVectorIndex
            map[index, basisVectorIndex] =
                -scalar;

            // Fill diagonal
            map[index, index] = 1d - scalar * scalar / v1;

            // Fill remaining items
            foreach (var (index2, scalar2) in unitVector.ToTuples())
            {
                if (index2 == basisVectorIndex || index2 == index)
                    continue;

                map[index, index2] = -(scalar * scalar2) / v1;
            }
        }

        return map.GetMap();
    }


    
    public static LinFloat64Rotation3D GetOrthogonalizingRotation(this ILinFloat64Vector3D vector1, ILinFloat64Vector3D vector2)
    {
        return LinFloat64RotationComposer3D
            .Create()
            .AppendBasisAlignmentRotation(vector1, vector2)
            .GetInverseRotation();
    }

    
    public static Pair<LinFloat64Vector3D> OrthogonalizeFrameUsingRotation(this ILinFloat64Vector3D vector1, ILinFloat64Vector3D vector2)
    {
        var rotation = vector1.GetOrthogonalizingRotation(vector2);

        return new Pair<LinFloat64Vector3D>(
            rotation.MapBasisVector(0),
            rotation.MapBasisVector(1)
        );
    }

    //
    //public static LinFloat64UnilinearMap Power(this double scalar, LinFloat64UnilinearMap v1)
    //{
    //    return v1.MapScalars(s => Math.Log(scalar, s));
    //}


}