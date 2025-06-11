using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Subspaces;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

public static class XGaFloat64VectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector CreateXGaVector(this IEnumerable<double> scalarList, XGaFloat64Processor processor)
    {
        return new XGaFloat64Vector(
            processor, 
            scalarList.ToValidXGaVectorDictionary()
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector CreateUnitXGaFloat64Vector(this double angle, int index1, int index2)
    {
        Debug.Assert(index2 > index1);

        var processor = XGaFloat64Processor.Euclidean;

        var scalar1 = Math.Cos(angle);
        var scalar2 = Math.Sin(angle);

        return processor
            .CreateVectorComposer()
            .SetVectorTerm(index1, scalar1)
            .SetVectorTerm(index2, scalar2)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector CreateXGaPhasor(this double angle, double magnitude, int index1, int index2)
    {
        Debug.Assert(index2 > index1);

        var processor = XGaFloat64Processor.Euclidean;

        var scalar1 = magnitude * Math.Cos(angle);
        var scalar2 = magnitude * Math.Sin(angle);

        return processor
            .CreateVectorComposer()
            .SetVectorTerm(index1, scalar1)
            .SetVectorTerm(index2, scalar2)
            .GetVector();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector ToXGaFloat64Vector(this ILinFloat64Vector2D vector)
    {
        return XGaFloat64Processor
            .Euclidean
            .CreateVectorComposer()
            .SetVectorTerm(0, vector.X)
            .SetVectorTerm(1, vector.Y)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector ToXGaFloat64Vector(this ILinFloat64Vector2D vector, XGaFloat64Processor processor)
    {
        return processor
            .CreateVectorComposer()
            .SetVectorTerm(0, vector.X)
            .SetVectorTerm(1, vector.Y)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector ToXGaFloat64Vector(this ILinFloat64Vector3D vector)
    {
        return XGaFloat64Processor
            .Euclidean
            .CreateVectorComposer()
            .SetVectorTerm(0, vector.X)
            .SetVectorTerm(1, vector.Y)
            .SetVectorTerm(2, vector.Z)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector ToXGaFloat64Vector(this ILinFloat64Vector3D vector, XGaFloat64Processor processor)
    {
        return processor
            .CreateVectorComposer()
            .SetVectorTerm(0, vector.X)
            .SetVectorTerm(1, vector.Y)
            .SetVectorTerm(2, vector.Z)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector ToXGaFloat64Vector(this ILinFloat64Vector4D vector)
    {
        return XGaFloat64Processor
            .Euclidean
            .CreateVectorComposer()
            .SetVectorTerm(0, vector.X)
            .SetVectorTerm(1, vector.Y)
            .SetVectorTerm(2, vector.Z)
            .SetVectorTerm(3, vector.W)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector ToXGaFloat64Vector(this ILinFloat64Vector4D vector, XGaFloat64Processor processor)
    {
        return processor
            .CreateVectorComposer()
            .SetVectorTerm(0, vector.X)
            .SetVectorTerm(1, vector.Y)
            .SetVectorTerm(2, vector.Z)
            .SetVectorTerm(3, vector.W)
            .GetVector();
    }


    public static XGaFloat64Vector DiagonalToXGaFloat64Vector(this double[,] matrix, XGaFloat64Processor processor)
    {
        var count = Math.Min(matrix.GetLength(0), matrix.GetLength(1));
        var composer = processor.CreateVectorComposer();

        for (var i = 0; i < count; i++)
        {
            var scalar = matrix[i, i];

            composer.SetVectorTerm(i, scalar);
        }

        return composer.GetVector();
    }

    public static XGaFloat64Vector RowToXGaFloat64Vector(this double[,] matrix, int row, XGaFloat64Processor processor)
    {
        var columnCount = matrix.GetLength(1);
        var composer = processor.CreateVectorComposer();

        for (var i = 0; i < columnCount; i++)
        {
            var scalar = matrix[i, row];

            composer.SetVectorTerm(i, scalar);
        }

        return composer.GetVector();
    }

    public static XGaFloat64Vector ColumnToXGaFloat64Vector(this double[,] matrix, int column, XGaFloat64Processor processor)
    {
        var rowCount = matrix.GetLength(0);
        var composer = processor.CreateVectorComposer();

        for (var i = 0; i < rowCount; i++)
        {
            var scalar = matrix[column, i];

            composer.SetVectorTerm(i, scalar);
        }

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaFloat64Vector> RowsToXGaFloat64Vectors(this double[,] matrix, XGaFloat64Processor processor)
    {
        return matrix.GetLength(0).GetRange().Select(
            r => matrix.RowToXGaFloat64Vector(r, processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaFloat64Vector> ColumnsToXGaFloat64Vectors(this double[,] matrix, XGaFloat64Processor processor)
    {
        return matrix.GetLength(1).GetRange().Select(
            c => matrix.ColumnToXGaFloat64Vector(c, processor)
        );
    }


    public static XGaFloat64Vector DiagonalToXGaFloat64Vector(this Matrix matrix, XGaFloat64Processor processor)
    {
        var count = Math.Min(matrix.RowCount, matrix.ColumnCount);
        var composer = processor.CreateVectorComposer();

        for (var i = 0; i < count; i++)
        {
            var scalar = matrix[i, i];

            composer.SetVectorTerm(i, scalar);
        }

        return composer.GetVector();
    }

    public static XGaFloat64Vector RowToXGaFloat64Vector(this Matrix matrix, int row, XGaFloat64Processor processor)
    {
        var composer = processor.CreateVectorComposer();

        for (var i = 0; i < matrix.ColumnCount; i++)
            composer.SetVectorTerm(i, matrix[row, i]);

        return composer.GetVector();
    }

    public static XGaFloat64Vector ColumnToXGaFloat64Vector(this Matrix matrix, int column, XGaFloat64Processor processor)
    {
        var composer = processor.CreateVectorComposer();

        for (var i = 0; i < matrix.RowCount; i++)
            composer.SetVectorTerm(i, matrix[i, column]);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaFloat64Vector> RowsToXGaFloat64Vectors(this Matrix matrix, XGaFloat64Processor processor)
    {
        return matrix.RowCount.GetRange().Select(
            r => matrix.RowToXGaFloat64Vector(r, processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaFloat64Vector> ColumnsToXGaFloat64Vectors(this Matrix matrix, XGaFloat64Processor processor)
    {
        return matrix.ColumnCount.GetRange().Select(
            c => matrix.ColumnToXGaFloat64Vector(c, processor)
        );
    }
    

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static IEnumerable<Vector> OmMap(this IOutermorphism om, params Vector[] vectorsList)
    //{
    //    return vectorsList.Select(v => v.OmMapUsing(om));
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaFloat64Vector> OmMap(this IXGaFloat64Outermorphism om, IEnumerable<XGaFloat64Vector> vectorsList)
    {
        return vectorsList.Select(v => v.OmMapUsing(om));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaFloat64Vector> OmMapUsing(this IEnumerable<XGaFloat64Vector> vectorsList, IXGaFloat64Outermorphism om)
    {
        return vectorsList.Select(v => v.OmMapUsing(om));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaFloat64Vector> ProjectOn(this IEnumerable<XGaFloat64Vector> vectorsList, XGaFloat64Subspace subspace)
    {
        return vectorsList.Select(subspace.Project);
    }


    /// <summary>
    /// Apply the Gram-Schmidt process using the outer product
    /// See here for more details:
    /// https://en.wikipedia.org/wiki/Gram%E2%80%93Schmidt_process
    /// "Gram-Schmidt Orthogonalization in Geometric Algebra"
    /// https://vixra.org/abs/1306.0176
    /// </summary>
    /// <param name="vectorsList"></param>
    /// <param name="makeUnitVectors"></param>
    /// <returns></returns>
    public static IReadOnlyList<XGaFloat64Vector> ApplyGramSchmidt(this IEnumerable<XGaFloat64Vector> vectorsList, bool makeUnitVectors)
    {
        var inputVectorsList = new List<XGaFloat64Vector>(
            vectorsList.Where(v => !v.IsNearZero())
        );

        var outputVectorsList = new List<XGaFloat64Vector>(inputVectorsList.Count);

        var v1 = inputVectorsList[0];

        outputVectorsList.Add(makeUnitVectors ? v1 / v1.Norm() : v1);

        var mv1 = v1.AsKVector();

        for (var i = 1; i < inputVectorsList.Count; i++)
        {
            var mv2 = mv1.Op(inputVectorsList[i]);

            // If the new vector is linearly dependent on the previous ones, ignore it
            if (mv2.IsNearZero()) continue;

            var v2 = mv1.Reverse().Gp(mv2).GetVectorPart();

            outputVectorsList.Add(makeUnitVectors ? v2 / v2.Norm() : v2);

            mv1 = mv2;
        }

        return outputVectorsList;
    }
    
    /// <summary>
    /// Apply the Gram-Schmidt process using classical vector projection
    /// See here for more details:
    /// https://en.wikipedia.org/wiki/Gram%E2%80%93Schmidt_process
    /// </summary>
    /// <param name="vectorsList"></param>
    /// <param name="makeUnitVectors"></param>
    /// <returns></returns>
    public static IReadOnlyList<XGaFloat64Vector> ApplyGramSchmidtByProjections(this IEnumerable<XGaFloat64Vector> vectorsList, bool makeUnitVectors)
    {
        var inputVectorsList = new List<XGaFloat64Vector>(
            vectorsList.Where(v => !v.IsNearZero())
        );

        var outputVectorsList = new List<XGaFloat64Vector>(inputVectorsList.Count)
        {
            inputVectorsList[0]
        };

        for (var i = 1; i < inputVectorsList.Count; i++)
        {
            var vector = outputVectorsList.Aggregate(
                inputVectorsList[i],
                (current, projectionVector) =>
                    current - current.ProjectOn(projectionVector.ToSubspace())
            );

            // If the new vector is linearly dependent on the previous ones, ignore it
            if (vector.IsNearZero())
                continue;

            outputVectorsList.Add(vector);
        }

        return makeUnitVectors
            ? outputVectorsList.Select(v => v.DivideByNorm()).ToArray()
            : outputVectorsList;
    }

}