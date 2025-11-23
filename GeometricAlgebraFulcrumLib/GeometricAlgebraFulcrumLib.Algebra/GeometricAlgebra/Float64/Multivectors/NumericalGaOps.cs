using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using MathNet.Numerics.LinearAlgebra;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Orthogonal;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors
{
    public abstract partial class XGaFloat64Multivector
    {
    }

    public partial class XGaFloat64Vector
    {

    }
    
    public partial class XGaFloat64Bivector
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<double> BivectorToMathNetVector()
        {
            return BivectorToMathNetVector((int) KvSpaceDimensions);
        }

        public Vector<double> BivectorToMathNetVector(int size)
        {
            if ((ulong)size < KvSpaceDimensions)
                throw new InvalidOperationException();

            var vector = MathNetNumericsUtils.CreateRealVector(
                size,
                IsKVectorSparsityMoreThan(0.2)
            );

            foreach (var (id, scalar) in IdScalarPairs)
            {
                var index1 = id.FirstIndex;
                var index2 = id.LastIndex;

                var index = (int)BinaryCombinationsUtilsUInt64.CombinadicToIndex(index1, index2);

                vector.At(index, scalar);
            }

            return vector;
        }

        /// <summary>
        /// Convert bivector to its skew-symmetric Euclidean matrix representation
        /// https://en.wikipedia.org/wiki/Bivector#Properties
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> ToSkewSymmetricMatrix()
        {
            return ToSkewSymmetricMatrix(VSpaceDimensions);
        }

        /// <summary>
        /// Convert bivector to its skew-symmetric Euclidean matrix representation
        /// https://en.wikipedia.org/wiki/Bivector#Properties
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Matrix<double> ToSkewSymmetricMatrix(int size)
        {
            if (size < VSpaceDimensions)
                throw new InvalidOperationException();

            var array = MathNetNumericsUtils.CreateRealMatrix(
                size,
                IsKVectorSparsityMoreThan(0.2)
            );

            foreach (var (id, scalar) in IdScalarPairs)
            {
                var (i, j) = id.FirstIndexPair;
                    
                array.At(i, j, scalar);
                array.At(j, i, -scalar);
            }

            Debug.Assert(array.IsNearSkewSymmetric());

            return array;
        }

        /// <summary>
        /// Extract a column from the bivector skew-symmetric Euclidean
        /// matrix representation. This corresponds to extracting the action
        /// of the bivector on a basis vector, which is the Euclidean left-
        /// contraction product of the basis vector with the bivector.
        /// </summary>
        /// <param name="bivector">Input bivector</param>
        /// <param name="size"></param>
        /// <param name="columnIndex">Column index to extract</param>
        /// <returns>Column vector as array</returns>
        public Vector<double> ToSkewSymmetricMatrixColumn(XGaFloat64Bivector bivector, int size, int columnIndex)
        {
            // This is equivalent to:
            //return Processor.ELcp(columnIndex, this).VectorToMathNetVector();

            if (size < VSpaceDimensions)
                throw new InvalidOperationException();

            var columnVector = MathNetNumericsUtils.CreateRealVector(
                size,
                IsKVectorSparsityMoreThan(0.2)
            );
            
            // For skew-symmetric matrix A corresponding to bivector B,
            // the j-th column is obtained by looking at coefficients involving index j
            foreach (var (id, scalar) in bivector)
            {
                var (i, j) = id.FirstIndexPair;
                    
                // If this bivector term involves the column index
                if (j == columnIndex)
                    columnVector.At(i, scalar);
                else if (i == columnIndex) 
                    columnVector.At(j, -scalar); // Anti-symmetric property
            }
            
            return columnVector;
        }


        /// <summary>
        /// Check if bivector has tridiagonal structure (only consecutive basis bivectors)
        /// </summary>
        /// <returns>True if tridiagonal structure</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsTridiagonal()
        {
            // Check that only consecutive pairs ei ∧ e(i+1) have non-zero coefficients
            return _idScalarDictionary.Keys.All(id => id.IsDenseSet);
        }
    
        /// <summary>
        /// Check if bivector has tridiagonal structure (only consecutive basis bivectors)
        /// </summary>
        /// <param name="tolerance">Numerical tolerance</param>
        /// <returns>True if tridiagonal structure</returns>
        public bool IsNearTridiagonal(double tolerance = Float64Utils.DefaultTolerance)
        {
            // Check that only consecutive pairs ei ∧ e(i+1) have non-zero coefficients
            foreach (var (id, scalar) in _idScalarDictionary)
            {
                if (Math.Abs(scalar) < tolerance) continue;
            
                if (!id.IsDenseSet) return false; // Not consecutive
            }
        
            return true;
        }

        
        /// <summary>
        /// Create a pure rotor multivector from a 2-blade
        /// </summary>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public XGaFloat64Multivector BladeExp(double tolerance = Float64Utils.DefaultTolerance)
        {
            var bv2 = 
                Gp(this).RemoveSmallTerms(tolerance);

            if (!bv2.IsScalar())
                throw new InvalidOperationException("Bivector is not a blade");

            var bladeSignature = bv2.Scalar();

            if (bladeSignature.IsNearZero(tolerance))
                return 1d + this;

            if (bladeSignature < 0)
            {
                var alpha = (-bladeSignature).Sqrt();

                return alpha.Cos() + alpha.Sin() / alpha * this;
            }
            else
            {
                var alpha = bladeSignature.Sqrt();
            
                return alpha.Cosh() + alpha.Sinh() / alpha * this;
            }
        }

        /// <summary>
        /// Create a pure rotor multivector from a 2-blade
        /// </summary>
        /// <param name="bladeSignatureSign"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public XGaFloat64Multivector BladeExp(IntegerSign bladeSignatureSign, double tolerance = Float64Utils.DefaultTolerance)
        {
            var bladeSignature = SpSquared();

            if (bladeSignatureSign.IsZero)
                return 1d + this;

            if (bladeSignatureSign.IsNegative)
            {
                var alpha = (-bladeSignature).Sqrt();

                return alpha.Cos() + alpha.Sin() / alpha * this;
            }
            else
            {
                var alpha = bladeSignature.Sqrt();
            
                return alpha.Cosh() + alpha.Sinh() / alpha * this;
            }
        }

        
        /// <summary>
        /// Reduce bivector to tridiagonal form using Householder reflections
        /// via GA operations
        /// </summary>
        /// <returns>Tuple of (tridiagonal bivector, accumulated versor)</returns>
        public (XGaFloat64Bivector tridiagonalBivector, IReadOnlyList<XGaFloat64Vector> householderVectors) ToTridiagonal(double tolerance = Float64Utils.DefaultTolerance)
        {
            var vSpaceDimensions = VSpaceDimensions;
            var householderVectors = new List<XGaFloat64Vector>(VSpaceDimensions);
            var tridiagonalBivector = this;
            
            // Apply Householder reflections iteratively
            for (var basisVectorIndex = 0; basisVectorIndex < vSpaceDimensions - 2; basisVectorIndex++)
            {
                // Apply Householder reflection to zero out elements below the subdiagonal
                (tridiagonalBivector, var householderVector) = 
                    ApplyHouseholderReflection(
                        tridiagonalBivector, 
                        basisVectorIndex
                    );

                // Accumulate the orthogonal transformations
                if (!householderVector.IsZero)
                    householderVectors.Add(householderVector);
            }
            
            return (
                tridiagonalBivector.RemoveSmallTerms(tolerance), 
                householderVectors
            );

            // <summary>
            // Apply a single Householder reflection step using GA operations
            // </summary>
            // <param name="bivector"></param>
            // <param name="vSpaceDimensions"></param>
            // <param name="basisVectorIndex">Current step index</param>
            // <param name="tolerance"></param>
            // <returns>Tuple of (reflected bivector, Householder vector)</returns>
            (XGaFloat64Bivector reflectedBivector, XGaFloat64Vector householderVector) ApplyHouseholderReflection(XGaFloat64Bivector bivector, int basisVectorIndex)
            {
                var startRowIndex = basisVectorIndex + 1;
                var columnLength = vSpaceDimensions - startRowIndex;
                if (columnLength <= 0) 
                    return (bivector, Processor.VectorZero);

                // Extract the column vector from position (step+1, step) downward
                var x = Processor
                    .ERcp(bivector, basisVectorIndex)
                    .GetVectorPart((int idx) => idx >= startRowIndex);

                var xNorm = x.ENorm();
            
                if (xNorm < tolerance) 
                    return (bivector, Processor.VectorZero);

                // Create Householder vector v = x - ||x||e₁
                var v = x - Processor.VectorTerm(
                    startRowIndex, 
                    x[startRowIndex] >= 0 ? xNorm : -xNorm
                );
                
                var vNorm = v.ENorm();
            
                if (vNorm < tolerance) 
                    return (bivector, Processor.VectorZero);

                var householderVector = 
                    v.Divide(vNorm);

                var reflectedBivector = 
                    householderVector.Gp(bivector).Gp(householderVector).GetBivectorPart();
                
                return (reflectedBivector, householderVector);
            }
        }
        
        /// <summary>
        /// Reduce bivector to tridiagonal form using Householder reflections
        /// via matrix operations
        /// </summary>
        /// <returns>Tuple of (tridiagonal bivector, accumulated versor)</returns>
        public (XGaFloat64Bivector tridiagonalBivector, IReadOnlyList<XGaFloat64Vector> householderVectors) ToTridiagonalViaMatrix(double tolerance = Float64Utils.DefaultTolerance)
        {
            // Convert bivector to matrix for Householder reduction
            var reductionResults = ToSkewSymmetricMatrix(VSpaceDimensions).GetHouseholderReduction(tolerance);
            
            // Convert back to bivector representation
            var tridiagonalBivector = 
                Processor.MatrixToBivector(reductionResults.ReducedMatrix, tolerance);

            var householderVectors = 
                reductionResults.GetHouseholderXGaVectors(Processor).ToArray();

            return (tridiagonalBivector, householderVectors);
        }

        

    }
}
