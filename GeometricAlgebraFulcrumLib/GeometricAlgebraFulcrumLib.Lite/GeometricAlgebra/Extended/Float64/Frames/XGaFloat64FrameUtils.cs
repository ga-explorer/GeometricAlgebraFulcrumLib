using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Subspaces;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Frames
{
    public static class XGaFloat64FrameUtils
    {
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

        public static Pair<XGaFloat64Vector> ApplyGramSchmidt(this XGaFloat64Vector v1, XGaFloat64Vector v2, bool makeUnitVectors)
        {
            var vectorsList = (new[] { v1, v2 }).ApplyGramSchmidt(
                makeUnitVectors
            );

            var zeroVector = v1.Processor.CreateZeroVector();

            return vectorsList.Count switch
            {
                0 => new Pair<XGaFloat64Vector>(zeroVector, zeroVector),
                1 => new Pair<XGaFloat64Vector>(vectorsList[0], zeroVector),
                _ => new Pair<XGaFloat64Vector>(vectorsList[0], vectorsList[1])
            };
        }

        public static Triplet<XGaFloat64Vector> ApplyGramSchmidt(this XGaFloat64Vector v1, XGaFloat64Vector v2, XGaFloat64Vector v3, bool makeUnitVectors)
        {
            var vectorsList = (new[] { v1, v2, v3 }).ApplyGramSchmidt(
                makeUnitVectors
            );

            var zeroVector = v1.Processor.CreateZeroVector();

            return vectorsList.Count switch
            {
                0 => new Triplet<XGaFloat64Vector>(zeroVector, zeroVector, zeroVector),
                1 => new Triplet<XGaFloat64Vector>(vectorsList[0], zeroVector, zeroVector),
                2 => new Triplet<XGaFloat64Vector>(vectorsList[0], vectorsList[1], zeroVector),
                _ => new Triplet<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2])
            };
        }

        public static Quad<XGaFloat64Vector> ApplyGramSchmidt(this XGaFloat64Vector v1, XGaFloat64Vector v2, XGaFloat64Vector v3, XGaFloat64Vector v4, bool makeUnitVectors)
        {
            var vectorsList = (new[] { v1, v2, v3, v4 }).ApplyGramSchmidt(
                makeUnitVectors
            );

            var zeroVector = v1.Processor.CreateZeroVector();

            return vectorsList.Count switch
            {
                0 => new Quad<XGaFloat64Vector>(zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Quad<XGaFloat64Vector>(vectorsList[0], zeroVector, zeroVector, zeroVector),
                2 => new Quad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], zeroVector, zeroVector),
                3 => new Quad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector),
                _ => new Quad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3])
            };
        }

        public static Quint<XGaFloat64Vector> ApplyGramSchmidt(this XGaFloat64Vector v1, XGaFloat64Vector v2, XGaFloat64Vector v3, XGaFloat64Vector v4, XGaFloat64Vector v5, bool makeUnitVectors)
        {
            var vectorsList = (new[] { v1, v2, v3, v4, v5 }).ApplyGramSchmidt(
                makeUnitVectors
            );

            var zeroVector = v1.Processor.CreateZeroVector();

            return vectorsList.Count switch
            {
                0 => new Quint<XGaFloat64Vector>(zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Quint<XGaFloat64Vector>(vectorsList[0], zeroVector, zeroVector, zeroVector, zeroVector),
                2 => new Quint<XGaFloat64Vector>(vectorsList[0], vectorsList[1], zeroVector, zeroVector, zeroVector),
                3 => new Quint<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector, zeroVector),
                4 => new Quint<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], zeroVector),
                _ => new Quint<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4])
            };
        }

        public static Hexad<XGaFloat64Vector> ApplyGramSchmidt(this XGaFloat64Vector v1, XGaFloat64Vector v2, XGaFloat64Vector v3, XGaFloat64Vector v4, XGaFloat64Vector v5, XGaFloat64Vector v6, bool makeUnitVectors)
        {
            var vectorsList = (new[] { v1, v2, v3, v4, v5, v6 }).ApplyGramSchmidt(
                makeUnitVectors
            );

            var zeroVector = v1.Processor.CreateZeroVector();

            return vectorsList.Count switch
            {
                0 => new Hexad<XGaFloat64Vector>(zeroVector, zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Hexad<XGaFloat64Vector>(vectorsList[0], zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                2 => new Hexad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], zeroVector, zeroVector, zeroVector, zeroVector),
                3 => new Hexad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector, zeroVector, zeroVector),
                4 => new Hexad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], zeroVector, zeroVector),
                5 => new Hexad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4], zeroVector),
                _ => new Hexad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4], vectorsList[5])
            };
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

        public static Pair<XGaFloat64Vector> ApplyGramSchmidtByProjections(this XGaFloat64Vector v1, XGaFloat64Vector v2, bool makeUnitVectors)
        {
            var vectorsList = (new[] { v1, v2 }).ApplyGramSchmidtByProjections(
                makeUnitVectors
            );

            var zeroVector = v1.Processor.CreateZeroVector();

            return vectorsList.Count switch
            {
                0 => new Pair<XGaFloat64Vector>(zeroVector, zeroVector),
                1 => new Pair<XGaFloat64Vector>(vectorsList[0], zeroVector),
                _ => new Pair<XGaFloat64Vector>(vectorsList[0], vectorsList[1])
            };
        }

        public static Triplet<XGaFloat64Vector> ApplyGramSchmidtByProjections(this XGaFloat64Vector v1, XGaFloat64Vector v2, XGaFloat64Vector v3, bool makeUnitVectors)
        {
            var vectorsList = (new[] { v1, v2, v3 }).ApplyGramSchmidtByProjections(
                makeUnitVectors
            );

            var zeroVector = v1.Processor.CreateZeroVector();

            return vectorsList.Count switch
            {
                0 => new Triplet<XGaFloat64Vector>(zeroVector, zeroVector, zeroVector),
                1 => new Triplet<XGaFloat64Vector>(vectorsList[0], zeroVector, zeroVector),
                2 => new Triplet<XGaFloat64Vector>(vectorsList[0], vectorsList[1], zeroVector),
                _ => new Triplet<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2])
            };
        }

        public static Quad<XGaFloat64Vector> ApplyGramSchmidtByProjections(this XGaFloat64Vector v1, XGaFloat64Vector v2, XGaFloat64Vector v3, XGaFloat64Vector v4, bool makeUnitVectors)
        {
            var vectorsList = (new[] { v1, v2, v3, v4 }).ApplyGramSchmidtByProjections(
                makeUnitVectors
            );

            var zeroVector = v1.Processor.CreateZeroVector();

            return vectorsList.Count switch
            {
                0 => new Quad<XGaFloat64Vector>(zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Quad<XGaFloat64Vector>(vectorsList[0], zeroVector, zeroVector, zeroVector),
                2 => new Quad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], zeroVector, zeroVector),
                3 => new Quad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector),
                _ => new Quad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3])
            };
        }

        public static Quint<XGaFloat64Vector> ApplyGramSchmidtByProjections(this XGaFloat64Vector v1, XGaFloat64Vector v2, XGaFloat64Vector v3, XGaFloat64Vector v4, XGaFloat64Vector v5, bool makeUnitVectors)
        {
            var vectorsList = (new[] { v1, v2, v3, v4, v5 }).ApplyGramSchmidtByProjections(
                makeUnitVectors
            );

            var zeroVector = v1.Processor.CreateZeroVector();

            return vectorsList.Count switch
            {
                0 => new Quint<XGaFloat64Vector>(zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Quint<XGaFloat64Vector>(vectorsList[0], zeroVector, zeroVector, zeroVector, zeroVector),
                2 => new Quint<XGaFloat64Vector>(vectorsList[0], vectorsList[1], zeroVector, zeroVector, zeroVector),
                3 => new Quint<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector, zeroVector),
                4 => new Quint<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], zeroVector),
                _ => new Quint<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4])
            };
        }

        public static Hexad<XGaFloat64Vector> ApplyGramSchmidtByProjections(this XGaFloat64Vector v1, XGaFloat64Vector v2, XGaFloat64Vector v3, XGaFloat64Vector v4, XGaFloat64Vector v5, XGaFloat64Vector v6, bool makeUnitVectors)
        {
            var vectorsList = (new[] { v1, v2, v3, v4, v5, v6 }).ApplyGramSchmidtByProjections(
                makeUnitVectors
            );

            var zeroVector = v1.Processor.CreateZeroVector();

            return vectorsList.Count switch
            {
                0 => new Hexad<XGaFloat64Vector>(zeroVector, zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Hexad<XGaFloat64Vector>(vectorsList[0], zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                2 => new Hexad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], zeroVector, zeroVector, zeroVector, zeroVector),
                3 => new Hexad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector, zeroVector, zeroVector),
                4 => new Hexad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], zeroVector, zeroVector),
                5 => new Hexad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4], zeroVector),
                _ => new Hexad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4], vectorsList[5])
            };
        }
    }
}
