using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.SignalProcessing;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class FrameUtils
    {
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
        public static IReadOnlyList<GaVector<T>> ApplyGramSchmidt<T>(this IEnumerable<GaVector<T>> vectorsList, bool makeUnitVectors)
        {
            var inputVectorsList = new List<GaVector<T>>(
                vectorsList.Where(v => !v.IsNearZero())
            );

            var outputVectorsList = new List<GaVector<T>>(inputVectorsList.Count);

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
        
        public static Pair<GaVector<T>> ApplyGramSchmidt<T>(this IGeometricAlgebraProcessor<T> processor, GaVector<T> v1, GaVector<T> v2, bool makeUnitVectors)
        {
            var vectorsList = ApplyGramSchmidt(
                new []{ v1, v2 }, 
                makeUnitVectors
            );

            var zeroVector = processor.CreateVectorZero();

            return vectorsList.Count switch
            {
                0 => new Pair<GaVector<T>>(zeroVector, zeroVector),
                1 => new Pair<GaVector<T>>(vectorsList[0], zeroVector),
                _ => new Pair<GaVector<T>>(vectorsList[0], vectorsList[1])
            };
        }
        
        public static Triplet<GaVector<T>> ApplyGramSchmidt<T>(this IGeometricAlgebraProcessor<T> processor, GaVector<T> v1, GaVector<T> v2, GaVector<T> v3, bool makeUnitVectors)
        {
            var vectorsList = ApplyGramSchmidt(
                new []{ v1, v2, v3 }, 
                makeUnitVectors
            );
            
            var zeroVector = processor.CreateVectorZero();

            return vectorsList.Count switch
            {
                0 => new Triplet<GaVector<T>>(zeroVector, zeroVector, zeroVector),
                1 => new Triplet<GaVector<T>>(vectorsList[0], zeroVector, zeroVector),
                2 => new Triplet<GaVector<T>>(vectorsList[0], vectorsList[1], zeroVector),
                _ => new Triplet<GaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2])
            };
        }
        
        public static Quad<GaVector<T>> ApplyGramSchmidt<T>(this IGeometricAlgebraProcessor<T> processor, GaVector<T> v1, GaVector<T> v2, GaVector<T> v3, GaVector<T> v4, bool makeUnitVectors)
        {
            var vectorsList = ApplyGramSchmidt(
                new []{ v1, v2, v3, v4 }, 
                makeUnitVectors
            );

            var zeroVector = processor.CreateVectorZero();

            return vectorsList.Count switch
            {
                0 => new Quad<GaVector<T>>(zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Quad<GaVector<T>>(vectorsList[0], zeroVector, zeroVector, zeroVector),
                2 => new Quad<GaVector<T>>(vectorsList[0], vectorsList[1], zeroVector, zeroVector),
                3 => new Quad<GaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector),
                _ => new Quad<GaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3])
            };
        }
        
        public static Quint<GaVector<T>> ApplyGramSchmidt<T>(this IGeometricAlgebraProcessor<T> processor, GaVector<T> v1, GaVector<T> v2, GaVector<T> v3, GaVector<T> v4, GaVector<T> v5, bool makeUnitVectors)
        {
            var vectorsList = ApplyGramSchmidt(
                new []{ v1, v2, v3, v4, v5 }, 
                makeUnitVectors
            );

            var zeroVector = processor.CreateVectorZero();

            return vectorsList.Count switch
            {
                0 => new Quint<GaVector<T>>(zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Quint<GaVector<T>>(vectorsList[0], zeroVector, zeroVector, zeroVector, zeroVector),
                2 => new Quint<GaVector<T>>(vectorsList[0], vectorsList[1], zeroVector, zeroVector, zeroVector),
                3 => new Quint<GaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector, zeroVector),
                4 => new Quint<GaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], zeroVector),
                _ => new Quint<GaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4])
            };
        }
        
        public static Hexad<GaVector<T>> ApplyGramSchmidt<T>(this IGeometricAlgebraProcessor<T> processor, GaVector<T> v1, GaVector<T> v2, GaVector<T> v3, GaVector<T> v4, GaVector<T> v5, GaVector<T> v6, bool makeUnitVectors)
        {
            var vectorsList = ApplyGramSchmidt(
                new []{ v1, v2, v3, v4, v5, v6 }, 
                makeUnitVectors
            );

            var zeroVector = processor.CreateVectorZero();

            return vectorsList.Count switch
            {
                0 => new Hexad<GaVector<T>>(zeroVector, zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Hexad<GaVector<T>>(vectorsList[0], zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                2 => new Hexad<GaVector<T>>(vectorsList[0], vectorsList[1], zeroVector, zeroVector, zeroVector, zeroVector),
                3 => new Hexad<GaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector, zeroVector, zeroVector),
                4 => new Hexad<GaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], zeroVector, zeroVector),
                5 => new Hexad<GaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4], zeroVector),
                _ => new Hexad<GaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4], vectorsList[5])
            };
        }

        public static IReadOnlyList<GaVector<ScalarSignalFloat64>> ApplyGramSchmidtByProjections(this IReadOnlyList<GaVector<ScalarSignalFloat64>> vectorsList, bool makeUnitVectors)
        {
            var vectorMatrixList = 
                vectorsList
                    .Select(v => v.ToMatrix())
                    .ToArray();

            var geometricProcessor = vectorsList[0].GeometricProcessor;
            var samplingSpecs = vectorsList[0].GetSamplingSpecs();
            var vectorCount = vectorMatrixList.Length;
            var samplingRate = samplingSpecs.SamplingRate;
            var sampleCount = samplingSpecs.SampleCount;
            var vSpaceDimension = vectorMatrixList[0].ColumnCount;

            for (var sampleIndex = 0; sampleIndex < sampleCount; sampleIndex++)
            {
                var index = sampleIndex;

                var matrix = (Matrix) Matrix.Build.Dense(
                    vSpaceDimension, 
                    vectorCount,
                    (i, j) => vectorMatrixList[j][index, i]
                );
                
                var rank = matrix.Rank();
                
                var c = matrix.ColumnAbsoluteSums();
                var colList = new List<int>(c.Count);
                for (var i = 0; i < c.Count; i++)
                    if (c[i] != 0) colList.Add(i);

                var gramSchmidt = Matrix.Build.Dense(
                    vSpaceDimension,
                    rank,
                    (i, j) => j < colList.Count ? matrix[i, colList[j]] : 0d
                ).GramSchmidt();

                //if (rank >= 3)
                //{
                //    Console.WriteLine($"Q: {gramSchmidt.Q}");
                //    Console.WriteLine($"R: {gramSchmidt.R}");
                //}

                var orthogonalMatrix = gramSchmidt.Q;
                var vectorNorms = gramSchmidt.R.Diagonal();

                for (var j = 0; j < vectorCount; j++)
                {
                    var vectorMatrix = vectorMatrixList[j];

                    if (j < orthogonalMatrix.ColumnCount)
                    {
                        var vectorNorm = 
                            makeUnitVectors ? 1d : vectorNorms[j];

                        for (var i = 0; i < orthogonalMatrix.RowCount; i++)
                            vectorMatrix[index, i] = vectorNorm * orthogonalMatrix[i, j];
                    }
                    else
                    {
                        for (var i = 0; i < orthogonalMatrix.RowCount; i++)
                            vectorMatrix[index, i] = 0d;
                    }
                }
            }

            var orthogonalVectors = 
                vectorMatrixList.Select(matrix => 
                    geometricProcessor.ToVectorSignal(matrix, samplingRate)
                );

            return makeUnitVectors
                ? orthogonalVectors.Select(v => v.DivideByNorm()).ToArray()
                : orthogonalVectors.ToArray();
        }

        /// <summary>
        /// Apply the Gram-Schmidt process using classical vector projection
        /// See here for more details:
        /// https://en.wikipedia.org/wiki/Gram%E2%80%93Schmidt_process
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vectorsList"></param>
        /// <param name="makeUnitVectors"></param>
        /// <returns></returns>
        public static IReadOnlyList<GaVector<T>> ApplyGramSchmidtByProjections<T>(this IEnumerable<GaVector<T>> vectorsList, bool makeUnitVectors)
        {
            var inputVectorsList = new List<GaVector<T>>(
                vectorsList.Where(v => !v.IsNearZero())
            );

            var outputVectorsList = new List<GaVector<T>>(inputVectorsList.Count)
            {
                inputVectorsList[0]
            };

            for (var i = 1; i < inputVectorsList.Count; i++)
            {
                var vector = outputVectorsList.Aggregate(
                    inputVectorsList[i], 
                    (current, projectionVector) => 
                        current - current.ProjectOn(projectionVector.GetSubspace())
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

        public static Pair<GaVector<T>> ApplyGramSchmidtByProjections<T>(this IGeometricAlgebraProcessor<T> processor, GaVector<T> v1, GaVector<T> v2, bool makeUnitVectors)
        {
            var vectorsList = ApplyGramSchmidtByProjections(
                new []{ v1, v2 }, 
                makeUnitVectors
            );

            var zeroVector = processor.CreateVectorZero();

            return vectorsList.Count switch
            {
                0 => new Pair<GaVector<T>>(zeroVector, zeroVector),
                1 => new Pair<GaVector<T>>(vectorsList[0], zeroVector),
                _ => new Pair<GaVector<T>>(vectorsList[0], vectorsList[1])
            };
        }
        
        public static Triplet<GaVector<T>> ApplyGramSchmidtByProjections<T>(this IGeometricAlgebraProcessor<T> processor, GaVector<T> v1, GaVector<T> v2, GaVector<T> v3, bool makeUnitVectors)
        {
            var vectorsList = ApplyGramSchmidtByProjections(
                new []{ v1, v2, v3 }, 
                makeUnitVectors
            );
            
            var zeroVector = processor.CreateVectorZero();

            return vectorsList.Count switch
            {
                0 => new Triplet<GaVector<T>>(zeroVector, zeroVector, zeroVector),
                1 => new Triplet<GaVector<T>>(vectorsList[0], zeroVector, zeroVector),
                2 => new Triplet<GaVector<T>>(vectorsList[0], vectorsList[1], zeroVector),
                _ => new Triplet<GaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2])
            };
        }
        
        public static Quad<GaVector<T>> ApplyGramSchmidtByProjections<T>(this IGeometricAlgebraProcessor<T> processor, GaVector<T> v1, GaVector<T> v2, GaVector<T> v3, GaVector<T> v4, bool makeUnitVectors)
        {
            var vectorsList = ApplyGramSchmidtByProjections(
                new []{ v1, v2, v3, v4 }, 
                makeUnitVectors
            );

            var zeroVector = processor.CreateVectorZero();

            return vectorsList.Count switch
            {
                0 => new Quad<GaVector<T>>(zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Quad<GaVector<T>>(vectorsList[0], zeroVector, zeroVector, zeroVector),
                2 => new Quad<GaVector<T>>(vectorsList[0], vectorsList[1], zeroVector, zeroVector),
                3 => new Quad<GaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector),
                _ => new Quad<GaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3])
            };
        }
        
        public static Quint<GaVector<T>> ApplyGramSchmidtByProjections<T>(this IGeometricAlgebraProcessor<T> processor, GaVector<T> v1, GaVector<T> v2, GaVector<T> v3, GaVector<T> v4, GaVector<T> v5, bool makeUnitVectors)
        {
            var vectorsList = ApplyGramSchmidtByProjections(
                new []{ v1, v2, v3, v4, v5 }, 
                makeUnitVectors
            );

            var zeroVector = processor.CreateVectorZero();

            return vectorsList.Count switch
            {
                0 => new Quint<GaVector<T>>(zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Quint<GaVector<T>>(vectorsList[0], zeroVector, zeroVector, zeroVector, zeroVector),
                2 => new Quint<GaVector<T>>(vectorsList[0], vectorsList[1], zeroVector, zeroVector, zeroVector),
                3 => new Quint<GaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector, zeroVector),
                4 => new Quint<GaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], zeroVector),
                _ => new Quint<GaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4])
            };
        }
        
        public static Hexad<GaVector<T>> ApplyGramSchmidtByProjections<T>(this IGeometricAlgebraProcessor<T> processor, GaVector<T> v1, GaVector<T> v2, GaVector<T> v3, GaVector<T> v4, GaVector<T> v5, GaVector<T> v6, bool makeUnitVectors)
        {
            var vectorsList = ApplyGramSchmidtByProjections(
                new []{ v1, v2, v3, v4, v5, v6 }, 
                makeUnitVectors
            );

            var zeroVector = processor.CreateVectorZero();

            return vectorsList.Count switch
            {
                0 => new Hexad<GaVector<T>>(zeroVector, zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Hexad<GaVector<T>>(vectorsList[0], zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                2 => new Hexad<GaVector<T>>(vectorsList[0], vectorsList[1], zeroVector, zeroVector, zeroVector, zeroVector),
                3 => new Hexad<GaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector, zeroVector, zeroVector),
                4 => new Hexad<GaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], zeroVector, zeroVector),
                5 => new Hexad<GaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4], zeroVector),
                _ => new Hexad<GaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4], vectorsList[5])
            };
        }
    }
}
