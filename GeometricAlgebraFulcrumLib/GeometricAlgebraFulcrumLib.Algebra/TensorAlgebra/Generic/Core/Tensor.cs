#region copyright
/*
 * MIT License
 * 
 * Copyright (c) 2020-2021 WhiteBlackGoose
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
#endregion


using System.Runtime.CompilerServices;
using GenericTensor.Functions;
using GeometricAlgebraFulcrumLib.Algebra.TensorAlgebra.Generic.Functions;
using HonkPerf.NET.Core;

namespace GeometricAlgebraFulcrumLib.Algebra.TensorAlgebra.Generic.Core
{
    public class GenTensor<T, TWrapper> : 
        IEquatable<GenTensor<T, TWrapper>>,
        ICloneable where TWrapper : struct, 
        IOperations<T>
    {
        #region Composition

        /// <summary>
        /// Creates a new axis that is put backward
        /// and then sets all elements as children
        /// e.g.
        /// say you have a bunch of tensors {t1, t2, t3} with shape of [2 x 4]
        /// Stack(t1, t2, t3) => T
        /// where T is a tensor of shape of [3 x 2 x 4]
        ///
        /// O(V)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> Stack(params GenTensor<T, TWrapper>[] elements)
        {
            return Composition<T, TWrapper>.Stack(elements);
        }

        /// <summary>
        /// Concatenates two tensors over the first axis,
        /// for example, if you had a tensor of
        /// [4 x 3 x 5] and a tensor of [9 x 3 x 5], their concat
        /// result will be of shape [13 x 3 x 5]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> Concat(GenTensor<T, TWrapper> a, GenTensor<T, TWrapper> b)
        {
            return Composition<T, TWrapper>.Concat(a, b);
        }


        /// <summary>
        /// Works similarly to Linq's <see cref="System.Linq.Enumerable.Aggregate{TSource,TAccumulate}"/>, but aggregates over the given <paramref name="axis"/>
        /// and mutates the given <paramref name="accumulated"/> value.
        /// </summary>
        /// <param name="tensor">
        /// The tensor to aggregate over.
        /// <br/>Shape: N1 x N2 x N3 x ... Nn
        /// </param>
        /// <param name="accumulated">
        /// The starting value of the aggregation,
        /// simultaneously being the destination tensor
        /// for accumulation so this method
        /// does not return a new tensor, but instead
        /// modifies the accumulated value.
        /// <br/>Shape: N1 x ... x N[<paramref name="axis"/>-1] x N[<paramref name="axis"/>+1] x ... x Nn
        /// </param>
        /// <param name="accumulator">
        /// Function which maps the accumulated value
        /// and the current one to the new value.
        /// </param>
        /// <param name="axis">
        /// The index of the axis (dimension) to aggregate over.
        /// </param>
        public static void Aggregate<TAggregatorFunc, TU, TUWrapper>(GenTensor<T, TWrapper> tensor, GenTensor<TU, TUWrapper> accumulated, TAggregatorFunc accumulator, int axis)
            where TAggregatorFunc : struct, IValueDelegate<TU, T, TU>
            where TUWrapper : struct, IOperations<TU>
        {
            Composition<T, TWrapper>.Aggregate(tensor, accumulated, accumulator, axis);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a tensor with all matrices are identity matrices
        /// <para>1 is achieved with <see cref="GeometricAlgebraFulcrumLib.Algebra.TensorAlgebra.Generic.Core.IOperations{T}.CreateOne"/></para>
        /// <para>0 is achieved with <see cref="GeometricAlgebraFulcrumLib.Algebra.TensorAlgebra.Generic.Core.IOperations{T}.CreateZero"/></para>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> CreateIdentityTensor(int[] dimensions, int finalMatrixDiagonal)
        {
            return Constructors<T, TWrapper>.CreateIdentityTensor(dimensions, finalMatrixDiagonal);
        }

        /// <summary>
        /// Creates an identity matrix whose width and height are equal to diagonal
        /// <para>1 is achieved with <see cref="GeometricAlgebraFulcrumLib.Algebra.TensorAlgebra.Generic.Core.IOperations{T}.CreateOne"/></para>
        /// <para>0 is achieved with <see cref="GeometricAlgebraFulcrumLib.Algebra.TensorAlgebra.Generic.Core.IOperations{T}.CreateZero"/></para>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> CreateIdentityMatrix(int diagonal)
        {
            return Constructors<T, TWrapper>.CreateIdentityMatrix(diagonal);
        }

        /// <summary>
        /// Creates a vector from an array of primitives
        /// Its length will be equal to elements.Length
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> CreateVector(params T[] elements)
        {
            return Constructors<T, TWrapper>.CreateVector(elements);
        }

        /// <summary>
        /// Creates a vector from an array of primitives
        /// Its length will be equal to elements.Length
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> CreateVector(int length)
        {
            return Constructors<T, TWrapper>.CreateVector(length);
        }

        /// <summary>
        /// Creates a matrix from a two-dimensional array of primitives
        /// for example
        /// <code>
        /// var M = Tensor.CreateMatrix(new[,]
        /// {
        ///     {1, 2},
        ///     {3, 4}
        /// });
        /// </code>
        /// where yourData.GetLength(0) is Shape[0] and
        /// yourData.GetLength(1) is Shape[1]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> CreateMatrix(T[,] data)
        {
            return Constructors<T, TWrapper>.CreateMatrix(data);
        }

        /// <summary>
        /// Creates an uninitialized square matrix
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> CreateSquareMatrix(int diagonalLength)
        {
            return Constructors<T, TWrapper>.CreateSquareMatrix(diagonalLength);
        }

        /// <summary>
        /// Creates a matrix of width and height size
        /// and iterator for each pair of coordinate
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> CreateMatrix(int width, int height, Func<int, int, T> stepper)
        {
            return Constructors<T, TWrapper>.CreateMatrix(width, height, stepper);
        }

        /// <summary>
        /// Creates a matrix of width and height size
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> CreateMatrix(int width, int height)
        {
            return Constructors<T, TWrapper>.CreateMatrix(width, height);
        }

        /// <summary>
        /// Creates a tensor of given size with iterator over its indices
        /// (its only argument is an array of integers which are indices of the tensor)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> CreateTensor(TensorShape shape, Func<int[], T> operation, Threading threading = Threading.Single)
        {
            return Constructors<T, TWrapper>.CreateTensor(shape, operation, threading);
        }

        /// <summary>
        /// Creates a tensor from an array
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> CreateTensor(T[] data)
        {
            return Constructors<T, TWrapper>.CreateTensor(data);
        }

        /// <summary>
        /// Creates a tensor from a two-dimensional array
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> CreateTensor(T[,] data)
        {
            return Constructors<T, TWrapper>.CreateTensor(data);
        }

        /// <summary>
        /// Creates a tensor from a three-dimensional array
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> CreateTensor(T[,,] data)
        {
            return Constructors<T, TWrapper>.CreateTensor(data);
        }

        /// <summary>
        /// Creates a tensor from an n-dimensional array
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> CreateTensor(Array data)
        {
            return Constructors<T, TWrapper>.CreateTensor(data);
        }

        #endregion

        #region Echelon forms

        /// <summary>
        /// Decomposes a matrix into a triangular one.
        /// Is of the Row Echelon Form (leading elements might be different from ones).
        /// </summary>
        public GenTensor<T, TWrapper> RowEchelonFormSimple()
        {
            return EchelonForm<T, TWrapper>.RowEchelonFormSimple(this);
        }

        /// <summary>
        /// Decomposes a matrix into a triangular one.
        /// Is of the Row Echelon Form (leading elements might be different from ones).
        /// 
        /// In addition, returns a permutation that algorithm performs with rows.
        /// Permutation array is size of rows there are in matrix.
        ///
        /// Initial state of that array is:
        /// 1 2 3 ... numberOfRows
        /// 
        /// For example, algorithm swaps first and third rows then:
        /// 3 2 1 ... numberOfRows
        ///
        /// It can be useful performing decompositions
        /// </summary>
        public (GenTensor<T, TWrapper>, int[]) RowEchelonFormPermuteSimple()
        {
            return EchelonForm<T, TWrapper>.RowEchelonFormPermuteSimple(this);
        }

        /// <summary>
        /// Decomposes a matrix into a triangular one.
        /// Is of the Row Echelon Form (leading elements might be different from ones).
        /// 
        /// In addition, returns a permutation that algorithm performs with rows.
        /// Permutation array is size of rows there are in matrix.
        ///
        /// Initial state of that array is:
        /// 1 2 3 ... numberOfRows
        /// 
        /// For example, algorithm swaps first and third rows then:
        /// 3 2 1 ... numberOfRows
        ///
        /// It can be useful performing decompositions
        /// </summary>
        public (GenTensor<T, TWrapper>, int[]) RowEchelonFormPermuteSafeDivision()
        {
            return EchelonForm<T, TWrapper>.RowEchelonFormPermuteSafeDivision(this);
        }

        /// <summary>
        /// Decomposes a matrix into a triangular one.
        /// Is of the Row Echelon Form (leading elements might be different from ones).
        /// Uses safe division, i.e. perform division only when computing the final result.
        /// </summary>
        public GenTensor<T, TWrapper> RowEchelonFormSafeDivision()
        {
            return EchelonForm<T, TWrapper>.RowEchelonFormSafeDivision(this);
        }


        /// <summary>
        /// Decomposes a matrix into a triangular one.
        /// Is of the Row Echelon Form (leading elements are ones).
        /// </summary>
        /// <returns></returns>
        public GenTensor<T, TWrapper> RowEchelonFormLeadingOnesSimple()
        {
            return EchelonForm<T, TWrapper>.RowEchelonFormLeadingOnesSimple(this);
        }

        /// <summary>
        /// Decomposes a matrix into a triangular one.
        /// Is of the Row Echelon Form (leading elements are ones).
        /// Uses safe division, i.e. perform division only when computing the final result.
        /// </summary>
        /// <returns></returns>
        public GenTensor<T, TWrapper> RowEchelonFormLeadingOnesSafeDivision()
        {
            return EchelonForm<T, TWrapper>.RowEchelonFormLeadingOnesSafeDivision(this);
        }


        /// <summary>
        /// Finds the reduced echelon form of a matrix.
        /// </summary>
        public GenTensor<T, TWrapper> ReducedRowEchelonFormSimple()
        {
            return EchelonForm<T, TWrapper>.ReducedRowEchelonFormSimple(this);
        }

        /// <summary>
        /// Finds the reduced echelon form of a matrix.
        /// Uses safe division, i.e. perform division only when computing the final result.
        /// </summary>
        public GenTensor<T, TWrapper> ReducedRowEchelonFormSafeDivision()
        {
            return EchelonForm<T, TWrapper>.ReducedRowEchelonFormSafeDivision(this);
        }

        /// <summary>
        /// Finds the reduced echelon form of a matrix.
        /// Uses safe division, i.e. perform division only when computing the final result.
        ///
        /// Additionally, returns row permutation
        /// </summary>
        public (GenTensor<T, TWrapper>, int[]) ReducedRowEchelonFormPermuteSafeDivision()
        {
            return EchelonForm<T, TWrapper>.ReducedRowEchelonFormPermuteSafeDivision(this);
        }

        #endregion

        #region Determinant

        /// <summary>
        /// Finds Determinant with the 100% precision for O(N!) where
        /// N is your matrix width
        /// The matrix should be square
        /// Borrowed from here: https://www.geeksforgeeks.org/adjoint-inverse-matrix/
        ///
        /// O(N!)
        /// </summary>
        public T DeterminantLaplace()
        {
            return Determinant<T, TWrapper>.DeterminantLaplace(this);
        }

        /// <summary>
        /// Finds Determinant with possible overflow
        /// because it uses fractions for avoiding division
        ///
        /// O(N^3)
        /// </summary>
        public T DeterminantGaussianSafeDivision()
        {
            return Determinant<T, TWrapper>.DeterminantGaussianSafeDivision(this);
        }

        /// <summary>
        /// Performs simple Gaussian elimination method on a tensor
        ///
        /// O(N^3)
        /// </summary>
        public T DeterminantGaussianSimple()
        {
            return Determinant<T, TWrapper>.DeterminantGaussianSimple(this);
        }

        /// <summary>
        /// Computers Laplace's Determinant for all
        /// matrices in the tensor
        /// </summary>
        public GenTensor<T, TWrapper> TensorDeterminantLaplace()
        {
            return Determinant<T, TWrapper>.TensorDeterminantLaplace(this);
        }

        /// <summary>
        /// Computers Determinant via Gaussian elimination and safe division
        /// for all matrices in the tensor
        /// </summary>
        public GenTensor<T, TWrapper> TensorDeterminantGaussianSafeDivision()
        {
            return Determinant<T, TWrapper>.TensorDeterminantGaussianSafeDivision(this);
        }

        /// <summary>
        /// Computers Determinant via Gaussian elimination
        /// for all matrices in the tensor
        /// </summary>
        public GenTensor<T, TWrapper> TensorDeterminantGaussianSimple()
        {
            return Determinant<T, TWrapper>.TensorDeterminantGaussianSimple(this);
        }

        #endregion

        #region Inversion

        /// <summary>
        /// Returns adjugate matrix
        ///
        /// O(N^5)
        /// </summary>
        public GenTensor<T, TWrapper> Adjoint()
        {
            return Inversion<T, TWrapper>.Adjoint(this);
        }

        /// <summary>
        /// Inverts a matrix A to B so that A * B = I
        /// Borrowed from here: https://www.geeksforgeeks.org/adjoint-inverse-matrix/
        ///
        /// O(N^5)
        /// </summary>
        public void InvertMatrix()
        {
            Inversion<T, TWrapper>.InvertMatrix(this);
        }

        /// <summary>
        /// Inverts all matrices in a tensor
        /// </summary>
        public void TensorMatrixInvert()
        {
            Inversion<T, TWrapper>.TensorMatrixInvert(this);
        }

        #endregion

        #region Elementary matrix operations

        /// <summary>
        /// Multiples the given row by the given coefficient.
        /// Modifies the matrix.
        /// </summary>
        public void RowMultiply(int rowId, T coefficient)
        {
            ElementaryRowOperations<T, TWrapper>.RowMultiply(this, rowId, coefficient);
        }

        /// <summary>
        /// To the first row adds the second row multiplied by the coefficient.
        /// Modifies the matrix.
        /// </summary>
        public void RowAdd(int dstRowId, int srcRowId, T coefficient)
        {
            ElementaryRowOperations<T, TWrapper>.RowAdd(this, dstRowId, srcRowId, coefficient);
        }

        /// <summary>
        /// From the first row subtracts the second row multiplied by the coefficient.
        /// Modifies the matrix.
        /// </summary>
        public void RowSubtract(int dstRowId, int srcRowId, T coefficient)
        {
            ElementaryRowOperations<T, TWrapper>.RowSubtract(this, dstRowId, srcRowId, coefficient);
        }

        /// <summary>
        /// Swaps the given two rows.
        /// Modifies the matrix.
        /// </summary>
        public void RowSwap(int row1Id, int row2Id)
        {
            ElementaryRowOperations<T, TWrapper>.RowSwap(this, row1Id, row2Id);
        }

        /// <summary>
        /// Finds the leading element of the row (the first non-zero element).
        /// </summary>
        /// <returns>
        /// Null if all elements are zero,
        /// Tuple of index and value of the first non-zero element otherwise
        /// </returns>
        public (int index, T value)? RowGetLeadingElement(int rowId)
        {
            return ElementaryRowOperations<T, TWrapper>.LeadingElement(this, rowId);
        }

        #endregion

        #region Matrix multiplication & division

        /// <summary>
        /// A / B
        /// Finds such C = A / B that A = C * B
        ///
        /// O(N^5)
        /// </summary>
        public static GenTensor<T, TWrapper> MatrixDivide(GenTensor<T, TWrapper> a, GenTensor<T, TWrapper> b)
        {
            return Inversion<T, TWrapper>.MatrixDivide(a, b);
        }

        /// <summary>
        /// Divides all matrices from tensor a over tensor b and returns a new
        /// tensor with them divided
        /// </summary>
        public static GenTensor<T, TWrapper> TensorMatrixDivide(GenTensor<T, TWrapper> a, GenTensor<T, TWrapper> b)
        {
            return Inversion<T, TWrapper>.TensorMatrixDivide(a, b);
        }


        /// <summary>
        /// Finds matrix multiplication result
        /// a and b are matrices
        /// a.Shape[1] should be equal to b.Shape[0]
        /// the resulting matrix is [a.Shape[0] x b.Shape[1]] shape
        ///
        /// O(N^3)
        /// </summary>
        public static GenTensor<T, TWrapper> MatrixMultiply(GenTensor<T, TWrapper> a, GenTensor<T, TWrapper> b, Threading threading = Threading.Single)
        {
            return MatrixMultiplication<T, TWrapper>.Multiply(a, b, threading);
        }

        /// <summary>
        /// Applies matrix dot product operation for
        /// all matrices in tensors
        ///
        /// O(N^3)
        /// </summary>
        public static GenTensor<T, TWrapper> TensorMatrixMultiply(GenTensor<T, TWrapper> a, GenTensor<T, TWrapper> b, Threading threading = Threading.Single)
        {
            return MatrixMultiplication<T, TWrapper>.TensorMultiply(a, b, threading);
        }

        #endregion

        #region Piecewise arithmetics

        /// <summary>
        /// T1 + T2
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> PiecewiseAdd(GenTensor<T, TWrapper> a, GenTensor<T, TWrapper> b,
            Threading threading = Threading.Single)
        {
            return PiecewiseArithmetics<T, TWrapper>.PiecewiseAdd(a, b, threading);
        }

        /// <summary>
        /// T1 - T2
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> PiecewiseSubtract(GenTensor<T, TWrapper> a, GenTensor<T, TWrapper> b,
            Threading threading = Threading.Single)
        {
            return PiecewiseArithmetics<T, TWrapper>.PiecewiseSubtract(a, b, threading);
        }

        /// <summary>
        /// T1 * T2
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> PiecewiseMultiply(GenTensor<T, TWrapper> a, GenTensor<T, TWrapper> b,
            Threading threading = Threading.Single)
        {
            return PiecewiseArithmetics<T, TWrapper>.PiecewiseMultiply(a, b, threading);
        }

        /// <summary>
        /// T1 / T2
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> PiecewiseDivide(GenTensor<T, TWrapper> a, GenTensor<T, TWrapper> b,
            Threading threading = Threading.Single)
        {
            return PiecewiseArithmetics<T, TWrapper>.PiecewiseDivide(a, b, threading);
        }

        /// <summary>
        /// T1 + const
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> PiecewiseAdd(GenTensor<T, TWrapper> a, T b,
            Threading threading = Threading.Single)
        {
            return PiecewiseArithmetics<T, TWrapper>.PiecewiseAdd(a, b, threading);
        }

        /// <summary>
        /// T1 - const
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> PiecewiseSubtract(GenTensor<T, TWrapper> a, T b,
            Threading threading = Threading.Single)
        {
            return PiecewiseArithmetics<T, TWrapper>.PiecewiseSubtract(a, b, threading);
        }

        /// <summary>
        /// const - T1
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> PiecewiseSubtract(T a, GenTensor<T, TWrapper> b,
            Threading threading = Threading.Single)
        {
            return PiecewiseArithmetics<T, TWrapper>.PiecewiseSubtract(a, b, threading);
        }

        /// <summary>
        /// T1 * const
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> PiecewiseMultiply(GenTensor<T, TWrapper> a, T b,
            Threading threading = Threading.Single)
        {
            return PiecewiseArithmetics<T, TWrapper>.PiecewiseMultiply(a, b, threading);
        }

        /// <summary>
        /// T1 / const
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> PiecewiseDivide(GenTensor<T, TWrapper> a, T b,
            Threading threading = Threading.Single)
        {
            return PiecewiseArithmetics<T, TWrapper>.PiecewiseDivide(a, b, threading);
        }

        /// <summary>
        /// const / T1
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenTensor<T, TWrapper> PiecewiseDivide(T a, GenTensor<T, TWrapper> b,
            Threading threading = Threading.Single)
        {
            return PiecewiseArithmetics<T, TWrapper>.PiecewiseDivide(a, b, threading);
        }

        #endregion

        #region Power

        /// <summary>
        /// Binary power
        /// 
        /// n positive:
        /// A ^ n = A * A * ... * A
        ///
        /// n negative:
        /// A ^ n = (A^(-1) * A^(-1) * ... * A^(-1))
        ///
        /// n == 0:
        /// A ^ n = I
        ///
        /// O(log(power) * N^3)
        /// </summary>
        public GenTensor<T, TWrapper> MatrixPower(int power, Threading threading = Threading.Single)
        {
            return Power<T, TWrapper>.MatrixPower(this, power, threading);
        }

        /// <summary>
        /// Performs MatrixPower operation on all matrices from this tensor
        /// </summary>
        public GenTensor<T, TWrapper> TensorMatrixPower(int power, Threading threading = Threading.Single)
        {
            return Power<T, TWrapper>.TensorMatrixPower(this, power, threading);
        }

        #endregion

        #region Decompositions

        /// <summary>
        /// https://www.geeksforgeeks.org/l-u-decomposition-system-linear-equations/
        /// </summary>
        /// <returns>
        /// LU decomposition
        /// </returns>
        public (GenTensor<T, TWrapper> l, GenTensor<T, TWrapper> u) LuDecomposition()
        {
            return LuDecomposition<T, TWrapper>.Decompose(this);
        }

        /// <summary>
        /// Find PLU decomposition: matrices P, L, U such that for original matrix A: PA = LU.
        /// 
        /// P stands for permutation matrix, permutations are made during the Gauss elimination process
        /// L stands for LIBERTY lower triangle matrix
        /// U stands for upper triangle matrix
        ///
        /// Algorithm, given matrix A:
        /// 1. Form an adjacent matrix (A|E)
        /// 2. Find row echelon form of that matrix (U|L_0) and permutation of the rows
        /// 3. Form permutation matrix P such that P_ij = \delta_{}
        /// 4. Compute L = P * L_0^{-1}
        ///
        /// Results are: P, L, U
        /// </summary>
        /// <returns>
        /// LUP decomposition of given matrix
        /// </returns>
        public (GenTensor<T, TWrapper> p, GenTensor<T, TWrapper> l, GenTensor<T, TWrapper> u) PluDecomposition()
        {
            return PluDecomposition<T, TWrapper>.Decompose(this);
        }

        #endregion

        #region ToString & GetHashCode

        /// <inheritdoc/>
        public override string ToString()
        {
            return DefaultOverridings<T, TWrapper>.InToString(this);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return DefaultOverridings<T, TWrapper>.GetHashCode(this);
        }

        #endregion

        #region Vector operations

        /// <summary>
        /// Finds a perpendicular vector to two given
        /// TODO: So far only implemented for 3D vectors
        /// </summary>
        public static GenTensor<T, TWrapper> VectorCrossProduct(GenTensor<T, TWrapper> a, GenTensor<T, TWrapper> b)
        {
            return VectorProduct<T, TWrapper>.VectorCrossProduct(a, b);
        }

        /// <summary>
        /// Calls VectorCrossProduct for every vector in the tensor
        /// </summary>
        public static GenTensor<T, TWrapper> TensorVectorCrossProduct(GenTensor<T, TWrapper> a,
            GenTensor<T, TWrapper> b)
        {
            return VectorProduct<T, TWrapper>.TensorVectorCrossProduct(a, b);
        }

        /// <summary>
        /// Finds the scalar product of two vectors
        ///
        /// O(N)
        /// </summary>
        public static T VectorDotProduct(GenTensor<T, TWrapper> a, GenTensor<T, TWrapper> b)
        {
            return VectorProduct<T, TWrapper>.VectorDotProduct(a, b);
        }

        /// <summary>
        /// Applies scalar product to every vector in a tensor so that
        /// you will get a one-reduced dimensional tensor
        /// (e.g. TensorVectorDotProduct([4 x 3 x 2], [4 x 3 x 2]) -> [4 x 3]
        ///
        /// O(V)
        /// </summary>
        public static GenTensor<T, TWrapper> TensorVectorDotProduct(GenTensor<T, TWrapper> a, GenTensor<T, TWrapper> b)
        {
            return VectorProduct<T, TWrapper>.TensorVectorDotProduct(a, b);
        }

        #endregion

        #region Copy and forward

        /// <summary>
        /// Copies a tensor calling each cell with a .Copy()
        ///
        /// O(V)
        /// </summary>
        public GenTensor<T, TWrapper> Copy(bool copyElements)
        {
            return CopyAndForward<T, TWrapper>.Copy(this, copyElements);
        }

        /// <summary>
        /// You might need it to make sure you don't copy
        /// your data but recreate a wrapper (if one exists)
        ///
        /// O(V)
        /// </summary>
        public GenTensor<T, TWrapper> Forward()
        {
            return CopyAndForward<T, TWrapper>.Forward(this);
        }

        #endregion

        #region Serialization

        /*
         * Serialization protocol:
         *
         * First int n: number of dimensions
         * Next n ints Shapes: dimensions' lengths
         * Next Shapes[0] * Shapes[1] * ...:
         *     First int n: size
         *     Next n bytes: data
         *
         */

        /// <summary>
        /// Serializes this to a byte array so it can be easily
        /// transmitted or stored
        /// </summary>
        /// <returns>
        /// Byte array with the serialized object in the serialization protocol
        /// </returns>
        public byte[] Serialize()
        {
            return Serializer<T, TWrapper>.Serialize(this);
        }

        /// <summary>
        /// Deserializes data into a tensor
        /// </summary>
        /// <param name="data">
        /// Byte array which must follow the serialization protocol
        /// </param>
        /// <returns>
        /// A tensor with the same data as stored before serialization
        /// (if serialization and deserialization in TWrapper work correctly)
        /// </returns>
        public static GenTensor<T, TWrapper> Deserialize(byte[] data)
        {
            return Serializer<T, TWrapper>.Deserialize(data);
        }

        #endregion

        /// <summary>
        /// A tensor is a matrix if has two dimensions, e.g. [3 x 4]
        /// </summary>
        public bool IsMatrix
        {
            get { return Shape.Count == 2; }
        }

        /// <summary>
        /// A tensor is a vector if has one dimension
        /// </summary>
        public bool IsVector
        {
            get { return Shape.Count == 1; }
        }

        /// <summary>
        /// Determines wether one is a matrix AND its width and height are equal
        /// </summary>
        public bool IsSquareMatrix
        {
            get { return IsMatrix && Shape.Shape[0] == Shape.Shape[1]; }
        }

        /// <summary>
        /// Calls your default(TWrapper).Equals
        /// Be sure to override it when using this function or ==, != operators
        /// </summary>
        public override bool Equals(object? obj)
        {
            return obj is GenTensor<T, TWrapper> ten && Equals(ten);
        }

        /// <summary>
        /// Calls your default(TWrapper).Equals
        /// Be sure to override it when using this function or ==, != operators
        /// </summary>
        public bool Equals(GenTensor<T, TWrapper>? obj)
        {
            if (obj is null)
                return false;
            if (obj.Shape != Shape)
                return false;
            foreach (var (index, _) in obj.Iterate())
                if (!default(TWrapper).AreEqual(GetValueNoCheck(index), obj.GetValueNoCheck(index)))
                    return false;
            return true;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static bool operator ==(GenTensor<T, TWrapper>? a, GenTensor<T, TWrapper> b)
        {
            return ReferenceEquals(a, b) || (a is not null && a.Equals(b));
        }

        public static bool operator !=(GenTensor<T, TWrapper> a, GenTensor<T, TWrapper> b)
        {
            return !(a == b);
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        /// <summary>
        /// Linear offset (as in offset in the initial array)
        /// </summary>
        public int LinOffset;

        /// <summary>
        /// Slice with data sharing like in python
        /// A[3:5] in python
        /// same as
        /// A.Slice(3, 5) in GT
        ///
        /// O(N)
        /// </summary>
        // TODO: Make it O(1)
        public GenTensor<T, TWrapper> Slice(int leftIncluding, int rightExcluding)
        {
            #if ALLOW_EXCEPTIONS
            ReactIfBadBound(leftIncluding, 0);
            ReactIfBadBound(rightExcluding - 1, 0);
            if (leftIncluding >= rightExcluding)
                throw new InvalidShapeException("Slicing cannot be performed");
            #endif
            var newLength = rightExcluding - leftIncluding;
            var toStack = new GenTensor<T, TWrapper>[newLength];
            for (var i = 0; i < newLength; i++)
                toStack[i] = GetSubtensor(i + leftIncluding);
            return Stack(toStack);
        }

        /// <summary>
        /// This Subtensor is sequential Subtensor(int)
        ///
        /// O(1)
        /// </summary>
        public GenTensor<T, TWrapper> GetSubtensor(int[] indices)
        {
            return GetSubtensor(indices, 0);
        }

        internal GenTensor<T, TWrapper> GetSubtensor(int[] indices, int id)
        {
            return id == indices.Length ? this : GetSubtensor(indices[id]).GetSubtensor(indices, id + 1);
        }

        /// <summary>
        /// Get a subtensor of a tensor
        /// If you have a t = Tensor[2 x 3 x 4],
        /// t.GetSubtensor(0) will return the proper matrix [3 x 4]
        ///
        /// O(1)
        /// </summary>
        public GenTensor<T, TWrapper> GetSubtensor(int index)
        {
            #if ALLOW_EXCEPTIONS
            ReactIfBadBound(index, 0);
            #endif
            var newLinIndexDelta = GetFlattenedIndexSilent(index);
            var newBlocks = Blocks.ToList();
            var rootAxis = 0;
            newBlocks.RemoveAt(rootAxis);
            var newShape = Shape.CutOffset1();
            var result = new GenTensor<T, TWrapper>(newShape, newBlocks.ToArray(), Data)
            {
                LinOffset = newLinIndexDelta
            };
            return result;
        }

        /// <summary>
        /// Suppose you have t = tensor [2 x 3 x 4]
        /// and m = matrix[3 x 4]
        /// You need to set this matrix to t's second matrix
        /// t.SetSubtensor(m, 1);
        ///
        /// O(V)
        /// </summary>
        public void SetSubtensor(GenTensor<T, TWrapper> sub, params int[] indices)
        {
            #if ALLOW_EXCEPTIONS
            if (indices.Length >= Shape.Count)
                throw new InvalidShapeException($"Number of {nameof(indices)} should be less than number of {nameof(Shape)}");
            for (int i = 0; i < indices.Length; i++)
                if (indices[i] < 0 || indices[i] >= Shape[i])
                    throw new IndexOutOfRangeException();
            if (Shape.Count - indices.Length != sub.Shape.Count)
                throw new InvalidShapeException($"Number of {nameof(sub.Shape.Length)} + {nameof(indices.Length)} should be equal to {Shape.Count}");
            #endif
            var thisSub = GetSubtensor(indices);
            #if ALLOW_EXCEPTIONS
            if (thisSub.Shape != sub.Shape)
                throw new InvalidShapeException($"{nameof(sub.Shape)} must be equal to {nameof(Shape)}");
            #endif
            thisSub.Assign(sub);
        }

        internal void Assign(GenTensor<T, TWrapper> genTensor)
        {
            foreach (var (index, value) in genTensor.Iterate())
                SetValueNoCheck(value, index);
        }

        private void NextIndex(int[] indices, int id)
        {
            if (id == -1)
                return;
            indices[id]++;
            if (indices[id] == Shape[id])
            {
                indices[id] = 0;
                NextIndex(indices, id - 1);
            }
        }

        /// <summary>
        /// Similar to <see cref="System.Collections.Generic.List{T}.ForEach(Action{T})" />.
        /// </summary>
        /// <param name="iterator">
        /// Value function, which takes two parameters:
        /// index array and value of the tensor in this point.
        /// </param>
        /// <remarks>
        /// When used properly, it is much faster than similar <see cref="GenTensor{T, IntWrapper}.Iterate" />.
        /// </remarks>
        public void ForEach<TIterator>(TIterator iterator) where TIterator : struct, IValueAction<int[], T>
        {
            static void ForEach1D(GenTensor<T, TWrapper> t, TIterator iterator)
            {
                var index = new int[1];
                for (var i = 0; i < t.Shape[0]; i++)
                {
                    index[0] = i;
                    iterator.Invoke(index, t.GetValueNoCheck(i));
                }
            }
            static void ForEach2D(GenTensor<T, TWrapper> t, TIterator iterator)
            {
                var index = new int[2];
                for (var x = 0; x < t.Shape[0]; x++)
                {
                    index[0] = x;
                    for (var y = 0; y < t.Shape[1]; y++)
                    {
                        index[1] = y;
                        iterator.Invoke(index, t.GetValueNoCheck(index));
                    }
                }
            }
            static void ForEach3D(GenTensor<T, TWrapper> t, TIterator iterator)
            {
                var index = new int[3];
                for (var x = 0; x < t.Shape[0]; x++)
                {
                    index[0] = x;
                    for (var y = 0; y < t.Shape[1]; y++)
                    {
                        index[1] = y;
                        for (var z = 0; z < t.Shape[2]; z++)
                        {
                            index[2] = z;
                            iterator.Invoke(index, t.GetValueNoCheck(index));
                        }
                    }
                }
            }
            static void ForEach4D(GenTensor<T, TWrapper> t, TIterator iterator)
            {
                var index = new int[4];
                for (var x = 0; x < t.Shape[0]; x++)
                {
                    index[0] = x;
                    for (var y = 0; y < t.Shape[1]; y++)
                    {
                        index[1] = y;
                        for (var z = 0; z < t.Shape[2]; z++)
                        {
                            index[2] = z;
                            for (var w = 0; w < t.Shape[3]; w++)
                            {
                                index[3] = w;
                                iterator.Invoke(index, t.GetValueNoCheck(index));
                            }
                        }
                    }
                }
            }
            static bool Next(int[] index, int[] shape)
            {
                index[0]++;
                var i = 0;
                while (index[i] == shape[i])
                {
                    index[i] = 0;
                    if (i == shape.Length - 1)
                        return false;
                    i++;
                    index[i]++;
                }
                return true;
            }

            if (Shape.DimensionCount == 0 || Volume == 0) return;
            if (Shape.DimensionCount == 1) ForEach1D(this, iterator);
            else if (Shape.DimensionCount == 2) ForEach2D(this, iterator);
            else if (Shape.DimensionCount == 3) ForEach3D(this, iterator);
            else if (Shape.DimensionCount == 4) ForEach4D(this, iterator);
            else
            {
                var index = new int[Shape.DimensionCount];
                index[0] = -1;
                while (Next(index, Shape.Shape))
                    iterator.Invoke(index, GetValueNoCheck(index));
            }
        }

        /// <summary>
        /// Iterate over array of indices and a value in TPrimitive
        /// </summary>
        public IEnumerable<(int[] Index, T Value)> Iterate()
        {
            foreach (var ind in IterateOver(0))
                yield return (ind, GetValueNoCheck(ind));
        }

        /// <summary>
        /// Element-wise indexing,
        /// for example suppose you have a t = Tensor[2 x 3 x 4] of int-s
        /// A correct way to index it would be
        /// t[0, 0, 1] or t[1, 2, 3],
        /// but neither of t[0, 1] (Use GetSubtensor for this) and t[4, 5, 6] (IndexOutOfRange)
        /// </summary>
        public T this[int x, int y, int z, params int[] indices]
        {
            get { return Data[GetFlattenedIndexWithCheck(x, y, z, indices)]; }
            set { Data[GetFlattenedIndexWithCheck(x, y, z, indices)] = value; }
        }

        /// <summary>
        /// Element-wise indexing,
        /// for example suppose you have a t = Tensor[2 x 3 x 4] of int-s
        /// A correct way to index it would be
        /// t[0, 0, 1] or t[1, 2, 3],
        /// but neither of t[0, 1] (Use GetSubtensor for this) and t[4, 5, 6] (IndexOutOfRange)
        /// </summary>
        public T this[int x, int y, int z]
        {
            get { return Data[GetFlattenedIndexWithCheck(x, y, z)]; }
            set { Data[GetFlattenedIndexWithCheck(x, y, z)] = value; }
        }

        /// <summary>
        /// Element-wise indexing,
        /// for example suppose you have a t = Tensor[2 x 3 x 4] of int-s
        /// A correct way to index it would be
        /// t[0, 0, 1] or t[1, 2, 3],
        /// but neither of t[0, 1] (Use GetSubtensor for this) and t[4, 5, 6] (IndexOutOfRange)
        /// </summary>
        public T this[int x, int y]
        {
            get { return Data[GetFlattenedIndexWithCheck(x, y)]; }
            set { Data[GetFlattenedIndexWithCheck(x, y)] = value; }
        }

        /// <summary>
        /// Element-wise indexing,
        /// for example suppose you have a t = Tensor[2 x 3 x 4] of int-s
        /// A correct way to index it would be
        /// t[0, 0, 1] or t[1, 2, 3],
        /// but neither of t[0, 1] (Use GetSubtensor for this) and t[4, 5, 6] (IndexOutOfRange)
        /// </summary>
        public T this[int x]
        {
            get { return Data[GetFlattenedIndexWithCheck(x)]; }
            set { Data[GetFlattenedIndexWithCheck(x)] = value; }
        }

        /// <summary>
        /// Gets the value by an array of indices.
        /// </summary>
        public T this[int[] inds]
        {
            get { return Data[GetFlattenedIndexWithCheck(inds)]; }
            set { Data[GetFlattenedIndexWithCheck(inds)] = value; }
        }

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueNoCheck(int x)
        {
            unchecked
            {
                return Data[GetFlattenedIndexSilent(x)];
            }
        }

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueNoCheck(int x, int y)
        {
            unchecked
            {
                return Data[GetFlattenedIndexSilent(x, y)];
            }
        }

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueNoCheck(int x, int y, int z)
        {
            unchecked
            {
                return Data[GetFlattenedIndexSilent(x, y, z)];
            }
        }

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueNoCheck(int x, int y, int z, int[] indices)
        {
            return Data[GetFlattenedIndexSilent(x, y, z, indices)];
        }

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueNoCheck(int[] indices)
        {
            return Data[GetFlattenedIndexSilent(indices)];
        }

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(T value, int x)
        {
            Data[GetFlattenedIndexSilent(x)] = value;
        }

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(T value, int x, int y)
        {
            Data[GetFlattenedIndexSilent(x, y)] = value;
        }

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(T value, int x, int y, int z)
        {
            Data[GetFlattenedIndexSilent(x, y, z)] = value;
        }

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(T value, int x, int y, int z, int[] other)
        {
            Data[GetFlattenedIndexSilent(x, y, z, other)] = value;
        }

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(T value, int[] indices)
        {
            Data[GetFlattenedIndexSilent(indices)] = value;
        }

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(Func<T> valueCreator, int x)
        {
            Data[GetFlattenedIndexSilent(x)] = valueCreator();
        }

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(Func<T> valueCreator, int x, int y)
        {
            Data[GetFlattenedIndexSilent(x, y)] = valueCreator();
        }

        /// <summary>
        /// Sets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(Func<T> valueCreator, int x, int y, int z)
        {
            Data[GetFlattenedIndexSilent(x, y, z)] = valueCreator();
        }

        /// <summary>
        /// Sets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(Func<T> valueCreator, int x, int y, int z, int[] indices)
        {
            Data[GetFlattenedIndexSilent(x, y, z, indices)] = valueCreator();
        }

        /// <summary>
        /// Sets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(Func<T> valueCreator, int[] indices)
        {
            Data[GetFlattenedIndexSilent(indices)] = valueCreator();
        }

        /// <summary>
        /// If you need to set your wrapper to the tensor directly, use this function
        /// </summary>
        public void SetCell(T newWrapper, params int[] indices)
        {
            var actualIndex = GetFlattenedIndexWithCheck(indices);
            Data[actualIndex] = newWrapper;
        }

        /// <summary>
        /// Get a pointer to the wrapper in your tensor
        /// You can call its methods or set its fields, so that it will be applied to the tensor's element
        /// </summary>
        public T GetCell(params int[] indices)
        {
            var actualIndex = GetFlattenedIndexWithCheck(indices);
            return Data[actualIndex];
        }

        /// <summary>
        /// Allows to iterate on lower-dimensions,
        /// so that, for example, in tensor of [2 x 3 x 4]
        /// and offsetFromLeft = 1
        /// while iterating you will get the following arrays:
        /// {0, 0}
        /// {0, 1}
        /// {0, 2}
        /// {1, 0}
        /// {1, 1}
        /// {1, 2}
        /// </summary>
        /// <param name="offsetFromLeft"></param>
        /// <returns></returns>
        public IEnumerable<int[]> IterateOver(int offsetFromLeft)
        {
            static bool SumIsNot0(int[] arr)
            {
                foreach (var a in arr)
                    if (a != 0)
                        return true;
                return false;
            }

            if (Volume == 0) yield break;
            var indices = new int[Shape.Count - offsetFromLeft];
            do
            {
                yield return indices;
                NextIndex(indices, indices.Length - 1);
            } while (SumIsNot0(indices)); // for tensor 4 x 3 x 2 the first violating index would be 5  0  0 
        }

        /// <summary>
        /// Allows to iterate on lower-dimensions,
        /// so that, for example, in tensor of [2 x 3 x 4]
        /// and offsetFromLeft = 1
        /// while iterating you will get the following arrays:
        /// {0, 0}
        /// {0, 1}
        /// {0, 2}
        /// {1, 0}
        /// {1, 1}
        /// {1, 2}
        /// </summary>
        /// <param name="offsetFromLeft"></param>
        /// <returns></returns>
        public IEnumerable<int[]> IterateOverCopy(int offsetFromLeft)
        {
            foreach (var inds in IterateOver(offsetFromLeft))
                yield return inds.ToArray();
        }

        /// <summary>
        /// IterateOver where yourTensor[index] is always a matrix
        /// </summary>
        public IEnumerable<int[]> IterateOverMatrices()
        {
            return IterateOver(2);
        }

        /// <summary>
        /// IterateOver where yourTensor[index] is always a vector
        /// </summary>
        public IEnumerable<int[]> IterateOverVectors()
        {
            return IterateOver(1);
        }

        /// <summary>
        /// IterateOver where yourTensor[index] is always an element
        /// </summary>
        public IEnumerable<int[]> IterateOverElements()
        {
            return IterateOver(0);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public void IterateOver1(Action<int> react)
        {
            for (var x = 0; x < Shape[0]; x++)
                react(x);
        }

        public void IterateOver2(Action<int, int> react)
        {
            for (var x = 0; x < Shape[0]; x++)
            for (var y = 0; y < Shape[1]; y++)
                react(x, y);
        }

        public void IterateOver3(Action<int, int, int> react)
        {
            for (var x = 0; x < Shape[0]; x++)
            for (var y = 0; y < Shape[1]; y++)
            for (var z = 0; z < Shape[2]; z++)
                react(x, y, z);
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        /// <summary>
        /// Shape represents axes' lengths of the tensor
        /// </summary>
        public TensorShape Shape { get; }

        /// <summary>
        /// Swaps axes in tensor.
        /// 0 - the first dimension
        /// </summary>
        public void Transpose(int axis1, int axis2)
        {
            (Blocks[axis1], Blocks[axis2]) = (Blocks[axis2], Blocks[axis1]);
            UpdateBlockCache();
            Shape.Swap(axis1, axis2);
        }

        /// <summary>
        /// Swaps two last axes or throws InvalidShapeException if a tensor is too low-dimensional
        /// </summary>
        public void TransposeMatrix()
        {
            #if ALLOW_EXCEPTIONS
            if (Shape.Count < 2)
                throw new InvalidShapeException("this should be at least matrix");
            #endif
            Transpose(Shape.Count - 2, Shape.Count - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReactIfBadBound(int index, int axisId)
        {
            if (index < 0 || index >= Shape.Shape[axisId])
                throw new IndexOutOfRangeException($"Bound violating: axis {axisId} is {Shape[axisId]} long, your input is {index}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReactIfBadIndexCount(int count)
        {
            if (count != Shape.Count)
                throw new ArgumentException($"There should be {Shape.Count} indices, not {count}");
        }

        private int GetFlattenedIndexWithCheck(int[] indices)
        {
            #if ALLOW_EXCEPTIONS
            ReactIfBadIndexCount(indices.Length);
            #endif
            var res = 0;
            for (var i = 0; i < indices.Length; i++)
            {
                #if ALLOW_EXCEPTIONS
                ReactIfBadBound(indices[i], i);
                #endif
                res += Blocks[i] * indices[i];
            }
            return res + LinOffset;
        }

        private int GetFlattenedIndexWithCheck(int x, int y, int z, int[] indices)
        {
            #if ALLOW_EXCEPTIONS
            ReactIfBadIndexCount(indices.Length + 3);
            #endif
            var res = GetFlattenedIndexWithCheck(x, y, z, checkNumberOfDims: false);
            for (var i = 0; i < indices.Length; i++)
            {
                #if ALLOW_EXCEPTIONS
                ReactIfBadBound(indices[i], i + 3);
                #endif
                res += Blocks[i + 3] * indices[i];
            }
            return res;
        }

        private int GetFlattenedIndexWithCheck(int x)
        {
            #if ALLOW_EXCEPTIONS
            ReactIfBadIndexCount(1);
            ReactIfBadBound(x, 0);
            #endif
            return LinOffset + CachedBlocks0 * x;
        }

        private int GetFlattenedIndexWithCheck(int x, int y)
        {
            #if ALLOW_EXCEPTIONS
            ReactIfBadIndexCount(2);
            ReactIfBadBound(x, 0);
            ReactIfBadBound(y, 1);
            #endif
            return LinOffset + CachedBlocks0 * x + CachedBlocks1 * y;
        }

        private int GetFlattenedIndexWithCheck(int x, int y, int z, bool checkNumberOfDims = true)
        {
            #if ALLOW_EXCEPTIONS
            if (checkNumberOfDims)
                ReactIfBadIndexCount(3);
            ReactIfBadBound(x, 0);
            ReactIfBadBound(y, 1);
            ReactIfBadBound(z, 2);
            #endif
            return LinOffset + CachedBlocks0 * x + CachedBlocks1 * y + CachedBlocks2 * z;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int GetFlattenedIndexSilent(int x)
        {
            return CachedBlocks0 * x +
                   LinOffset;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int GetFlattenedIndexSilent(int x, int y)
        {
            return CachedBlocks0 * x +
                   CachedBlocks1 * y +
                   LinOffset;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int GetFlattenedIndexSilent(int x, int y, int z)
        {
            return CachedBlocks0 * x +
                   CachedBlocks1 * y +
                   CachedBlocks2 * z +
                   LinOffset;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int GetFlattenedIndexSilent(int x, int y, int z, int[] other)
        {
            var res = GetFlattenedIndexSilent(x, y, z);
            for (var i = 0; i < other.Length; i++)
                res += other[i] * Blocks[i + 3];
            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetFlattenedIndexSilent(int[] other)
        {
            var res = 0;
            for (var i = 0; i < other.Length; i++)
                res += other[i] * Blocks[i];
            return res + LinOffset;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public readonly T[] Data;
        public readonly int[] Blocks; // 3 x 4 x 5
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        internal int CachedBlocks0;
        internal int CachedBlocks1;
        internal int CachedBlocks2;
        private int _volume = -1;
        /// <summary>
        /// Number of elements in tensor overall
        /// </summary>
        public int Volume
        {
            get
            {
                if (_volume == -1)
                {
                    _volume = 1;
                    for (var i = 0; i < Shape.Length; i++)
                        _volume *= Shape[i];
                }
                return _volume;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateBlockCache()
        {
            if (Blocks.Length >= 3)
                goto More3;
            if (Blocks.Length >= 2)
                goto More2;
            if (Blocks.Length >= 1)
                goto More1;
            return;
            More3:
            CachedBlocks2 = Blocks[2];
            More2:
            CachedBlocks1 = Blocks[1];
            More1:
            CachedBlocks0 = Blocks[0];
        }

        private void BlockRecompute()
        {
            var len = 1;
            for (var i = Shape.Count - 1; i >= 0; i--)
            {
                Blocks[i] = len;
                len *= Shape[i];
            }
            UpdateBlockCache();
        }

        private GenTensor(TensorShape dimensions, int[] blocks, T[] data)
        {
            Shape = dimensions;
            Blocks = blocks;
            Data = data;
            UpdateBlockCache();
        }

        /// <summary>
        /// Clones with copying the elements
        /// </summary>
        public object Clone()
        {
            return Copy(copyElements: true);
        }

        /// <summary>
        /// Creates a tensor from the given shape
        /// </summary>
        public GenTensor(TensorShape dimensions)
        {
            Shape = dimensions;
            var len = 1;
            for (var i = 0; i < dimensions.Length; i++)
            {
                len *= dimensions[i];
            }
            var data = new T[len];
            Data = data;
            LinOffset = 0;
            Blocks = new int[dimensions.Count];
            BlockRecompute();
        }

        /// <summary>
        /// Creates a tensor from the given dimensions
        /// </summary>
        public GenTensor(params int[] dimensions) : this(new TensorShape(dimensions)) { }
    }
}
