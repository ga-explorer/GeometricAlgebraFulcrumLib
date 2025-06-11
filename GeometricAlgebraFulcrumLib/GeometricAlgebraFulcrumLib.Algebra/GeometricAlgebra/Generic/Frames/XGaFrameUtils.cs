using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Frames;

public static class XGaFrameUtils
{
    
    /// <summary>
    /// Apply the Gram-Schmidt process using classical vector projection
    /// See here for more details:
    /// https://en.wikipedia.org/wiki/Gram%E2%80%93Schmidt_process
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="vectorsList"></param>
    /// <param name="makeUnitVectors"></param>
    /// <returns></returns>
    public static IReadOnlyList<XGaVector<T>> ApplyGramSchmidtByProjections<T>(this IEnumerable<XGaVector<T>> vectorsList, bool makeUnitVectors)
    {
        var inputVectorsList = new List<XGaVector<T>>(
            vectorsList.Where(v => !v.IsNearZero())
        );

        var outputVectorsList = new List<XGaVector<T>>(inputVectorsList.Count)
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

    /// <summary>
    /// Apply the Gram-Schmidt process using the outer product
    /// See here for more details:
    /// https://en.wikipedia.org/wiki/Gram%E2%80%93Schmidt_process
    /// "Gram-Schmidt Orthogonalization in Geometric Algebra"
    /// https://vixra.org/abs/1306.0176
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="vectorsList"></param>
    /// <param name="makeUnitVectors"></param>
    /// <returns></returns>
    public static IReadOnlyList<XGaVector<T>> ApplyGramSchmidt<T>(this IEnumerable<XGaVector<T>> vectorsList, bool makeUnitVectors)
    {
        var inputVectorsList = new List<XGaVector<T>>(
            vectorsList.Where(v => !v.IsNearZero())
        );

        var outputVectorsList = new List<XGaVector<T>>(inputVectorsList.Count);

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

}