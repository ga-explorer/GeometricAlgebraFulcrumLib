using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Orthogonal;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Numerical
{
    public static class NumericalGeometricAlgebraUtils
    {
        /// <summary>
        /// Convert orthogonal matrix to rotor representation (when possible)
        /// </summary>
        /// <param name="matrix">Orthogonal matrix</param>
        /// <param name="tolerance">Numerical tolerance</param>
        /// <returns>Rotor representation</returns>
        public static XGaFloat64Multivector OrthogonalMatrixToVersor(this Matrix<double> matrix, double tolerance = Float64Utils.DefaultTolerance)
        {
            var result =
                matrix.GetHouseholderFactorization(tolerance);

            Debug.Assert(result.IsOrthogonal());

            return result.GetHouseholderVersor(
                XGaFloat64Processor.Euclidean
            );

            //// For simplicity, we'll use the exponential form for small rotations
            //// In practice, this would use more sophisticated algorithms like Quaternion extraction
            //var n = matrix.GetLength(0);

            //// Extract the skew-symmetric part: (M - M^T) / 2
            //// Convert skew-symmetric part to bivector
            //var composer = processor.CreateBivectorComposer();
            //for (var i = 0; i < n; i++)
            //for (var j = i + 1; j < n; j++)
            //{
            //    var skewPart = (matrix[i, j] - matrix[j, i]) / 2.0;

            //    if (Math.Abs(skewPart) < tolerance) continue;

            //    composer.AddBivectorTerm(i, j, skewPart);
            //}

            //var bivector = composer.GetBivector();

            //// For small angles, exp(B/2) ≈ 1 + B/2
            //var halfBivector = bivector.Times(0.5);
            //return processor.ScalarOne.Add(halfBivector);
        }

        /// <summary>
        /// Convert a versor to its corresponding orthogonal matrix representation
        /// </summary>
        /// <param name="versor">Rotor/versor multivector</param>
        /// <param name="dimensions">Vector space dimensions</param>
        /// <param name="tolerance">Numerical tolerance</param>
        /// <returns>Orthogonal matrix representation</returns>
        public static Matrix<double> VersorToMatrix(this XGaFloat64Multivector versor, int dimensions, double tolerance = Float64Utils.DefaultTolerance)
        {
            // Check if rotor is approximately identity (scalar part ≈ 1, other parts ≈ 0)
            var scalarPart = versor.GetScalarPart().ScalarValue;
            var nonScalarNorm = versor.Subtract(versor.GetScalarPart()).ENorm();
            
            if (Math.Abs(scalarPart - 1.0) < tolerance && nonScalarNorm < tolerance)
                return DenseMatrix.CreateIdentity(dimensions);

            var isEven = versor.GetMaxGrade().IsEven();
            var matrix = DenseMatrix.Create(dimensions, dimensions, 0);
            
            // Apply rotor to each basis vector and extract coordinates
            var processor = versor.Processor;
            var versorInv = versor.EVersorInverse();
            for (var i = 0; i < dimensions; i++)
            {
                var basisVector = processor.VectorTerm(i);
                var transformedVector = 
                    versor.Gp(basisVector).Gp(versorInv).GetVectorPart();
                
                // Extract vector part and get coefficients
                foreach (var (id, scalar) in transformedVector)
                {
                    var j = id.FirstIndex;
                    
                    matrix[i, j] = isEven ? scalar : -scalar;
                }
            }
            
            return matrix;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector GetXGaVector(this Random randGen, int vSpaceDimensions, double minValue = -1d, double maxValue = -1d)
        {
            return randGen.GetXGaVector(
                XGaFloat64EuclideanProcessor.Instance,
                vSpaceDimensions, 
                minValue, 
                maxValue
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector GetXGaVector(this Random randGen, XGaFloat64Processor processor, int vSpaceDimensions, double minValue = -1d, double maxValue = -1d)
        {
            return processor.Vector(
                randGen.GetFloat64Array1D(
                    vSpaceDimensions, 
                    minValue, 
                    maxValue
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector GetXGaBivector(this Random randGen, int vSpaceDimensions, double minValue = -1d, double maxValue = -1d)
        {
            return randGen.GetXGaBivector(
                XGaFloat64EuclideanProcessor.Instance, 
                vSpaceDimensions, 
                minValue, 
                maxValue
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector GetXGaBivector(this Random randGen, XGaFloat64Processor processor, int vSpaceDimensions, double minValue = -1d, double maxValue = -1d)
        {
            var n = vSpaceDimensions * (vSpaceDimensions - 1) / 2;

            return processor.Bivector(
                randGen.GetFloat64Array1D(n, minValue, maxValue)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector GetXGaBivectorSparse(this Random randGen, int vSpaceDimensions, double minValue = -1d, double maxValue = -1d)
        {
            var n = vSpaceDimensions * (vSpaceDimensions - 1) / 2;
            var termCount = randGen.GetInt32(1, n);

            return randGen.GetXGaBivectorSparse(
                XGaFloat64EuclideanProcessor.Instance, 
                vSpaceDimensions, 
                termCount, 
                minValue, 
                maxValue
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector GetXGaBivectorSparse(this Random randGen, XGaFloat64Processor processor, int vSpaceDimensions, double minValue = -1d, double maxValue = -1d)
        {
            var n = vSpaceDimensions * (vSpaceDimensions - 1) / 2;
            var termCount = randGen.GetInt32(1, n);

            return randGen.GetXGaBivectorSparse(
                processor, 
                vSpaceDimensions, 
                termCount, 
                minValue, 
                maxValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector GetXGaBivectorSparse(this Random randGen, int vSpaceDimensions, int termCount, double minValue = -1d, double maxValue = -1d)
        {
            return randGen.GetXGaBivectorSparse(
                XGaFloat64EuclideanProcessor.Instance, 
                vSpaceDimensions, 
                termCount, 
                minValue, 
                maxValue
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector GetXGaBivectorSparse(this Random randGen, XGaFloat64Processor processor, int vSpaceDimensions, int termCount, double minValue = -1d, double maxValue = -1d)
        {
            var n = vSpaceDimensions * (vSpaceDimensions - 1) / 2;
            var k = Math.Min(n, termCount);

            var indexList =
                randGen
                    .GetDistinctIndices(
                        k,
                        n
                    ).Select(
                        i => i.BasisBivectorIndexToId()
                    ).ToDictionary(
                        i => i, 
                        _ => randGen.GetFloat64(minValue, maxValue)
                    );
            
            return processor.Bivector(indexList);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector GetXGaBivectorTridiagonal(this Random randGen, int vSpaceDimensions, double minValue = -1d, double maxValue = -1d)
        {
            return randGen.GetXGaBivectorTridiagonal(
                XGaFloat64EuclideanProcessor.Instance, 
                vSpaceDimensions, 
                minValue, 
                maxValue
            );
        }

        public static XGaFloat64Bivector GetXGaBivectorTridiagonal(this Random randGen, XGaFloat64Processor processor, int vSpaceDimensions, double minValue = -1d, double maxValue = -1d)
        {
            var composer = processor.CreateBivectorComposer();

            for (var i = 0; i < vSpaceDimensions - 1; i++)
                composer.SetBivectorTerm(
                    i, 
                    i + 1, 
                    randGen.GetFloat64(minValue, maxValue)
                );

            return composer.GetBivector();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector GetXGaBivectorTridiagonalSparse(this Random randGen, int vSpaceDimensions, double minValue = -1d, double maxValue = -1d)
        {
            var termCount = randGen.GetInt32(1, vSpaceDimensions - 1);
            
            return randGen.GetXGaBivectorTridiagonalSparse(
                XGaFloat64EuclideanProcessor.Instance, 
                vSpaceDimensions, 
                termCount,
                minValue, 
                maxValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector GetXGaBivectorTridiagonalSparse(this Random randGen, int vSpaceDimensions, XGaFloat64Processor processor, double minValue = -1d, double maxValue = -1d)
        {
            var termCount = randGen.GetInt32(1, vSpaceDimensions - 1);
            
            return randGen.GetXGaBivectorTridiagonalSparse(
                processor, 
                vSpaceDimensions, 
                termCount,
                minValue, 
                maxValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector GetXGaBivectorTridiagonalSparse(this Random randGen, int vSpaceDimensions, int termCount, double minValue = -1d, double maxValue = -1d)
        {
            return randGen.GetXGaBivectorTridiagonalSparse(
                XGaFloat64EuclideanProcessor.Instance, 
                vSpaceDimensions, 
                termCount,
                minValue, 
                maxValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector GetXGaBivectorTridiagonalSparse(this Random randGen, XGaFloat64Processor processor, int vSpaceDimensions, int termCount, double minValue = -1d, double maxValue = -1d)
        {
            var k = Math.Min(vSpaceDimensions - 2, termCount);

            var indexList =
                randGen
                    .GetDistinctIndices(
                        k,
                        vSpaceDimensions - 1
                    ).Select(
                        i => IndexSet.CreatePair(i, i + 1)
                    ).ToDictionary(
                        i => i, 
                        _ => randGen.GetFloat64(minValue, maxValue)
                    );
            
            return processor.Bivector(indexList);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector GetXGaBivectorCanonical(this Random randGen, int vSpaceDimensions, double minValue = -1d, double maxValue = -1d)
        {
            return randGen.GetXGaBivectorCanonical(
                XGaFloat64EuclideanProcessor.Instance, 
                vSpaceDimensions, 
                minValue, 
                maxValue
            );
        }

        public static XGaFloat64Bivector GetXGaBivectorCanonical(this Random randGen, XGaFloat64Processor processor, int vSpaceDimensions, double minValue = -1d, double maxValue = -1d)
        {
            var composer = processor.CreateBivectorComposer();

            var n = vSpaceDimensions ^ 1;
            for (var i = 0; i < n; i += 2)
                composer.SetBivectorTerm(
                    i, 
                    i + 1, 
                    randGen.GetFloat64(minValue, maxValue)
                );

            return composer.GetBivector();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector GetXGaBivectorCanonicalSparse(this Random randGen, int vSpaceDimensions, double minValue = -1d, double maxValue = -1d)
        {
            var termCount = randGen.GetInt32(1, vSpaceDimensions >> 1);

            return randGen.GetXGaBivectorCanonicalSparse(
                XGaFloat64EuclideanProcessor.Instance, 
                vSpaceDimensions, 
                termCount,
                minValue, 
                maxValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector GetXGaBivectorCanonicalSparse(this Random randGen, XGaFloat64Processor processor, int vSpaceDimensions, double minValue = -1d, double maxValue = -1d)
        {
            var termCount = randGen.GetInt32(1, vSpaceDimensions >> 1);

            return randGen.GetXGaBivectorCanonicalSparse(
                processor, 
                vSpaceDimensions, 
                termCount,
                minValue, 
                maxValue
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector GetXGaBivectorCanonicalSparse(this Random randGen, int vSpaceDimensions, int termCount, double minValue = -1d, double maxValue = -1d)
        {
            return randGen.GetXGaBivectorCanonicalSparse(
                XGaFloat64EuclideanProcessor.Instance, 
                vSpaceDimensions, 
                termCount,
                minValue, 
                maxValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector GetXGaBivectorCanonicalSparse(this Random randGen, XGaFloat64Processor processor, int vSpaceDimensions, int termCount, double minValue = -1d, double maxValue = -1d)
        {
            var n = (vSpaceDimensions >> 1) - 1;
            var k = Math.Min(n, termCount);

            var indexList =
                randGen
                    .GetDistinctIndices(
                        k,
                        n + 1
                    ).Select(
                        i => IndexSet.CreatePair(2 * i, 2 * i + 1)
                    ).ToDictionary(
                        i => i, 
                        _ => randGen.GetFloat64(minValue, maxValue)
                    );
            
            return processor.Bivector(indexList);
        }

    }
}
