using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical
{
    public class InvertibleRealMatrixComposer
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InvertibleRealMatrixComposer Create(int size)
        {
            return new InvertibleRealMatrixComposer(size);
        }


        public int Size { get; }

        protected Matrix<double> Matrix { get; }
        
        protected Matrix<double> MatrixInverse { get; }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private InvertibleRealMatrixComposer(int size)
        {
            Debug.Assert(size > 1);

            Size = size;
            Matrix = Matrix<double>.Build.DenseIdentity(size);
            MatrixInverse = Matrix<double>.Build.DenseIdentity(size);
        }


        public bool IsValid(double tolerance = Float64Utils.DefaultTolerance)
        {
            var array = Matrix.Multiply(MatrixInverse);

            for (var i = 0; i < Size; i++)
            for (var j = 0; j < Size; j++)
            {
                var value = i == j 
                    ? array[i, j] - 1 
                    : array[i, j];

                if (!value.IsNearZero(tolerance))
                    return false;
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValidIndex(int rowIndex)
        {
            return rowIndex >= 0 && rowIndex < Size;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidScalingFactor(double scalingFactor)
        {
            return !double.IsNaN(scalingFactor) &&
                   !double.IsInfinity(scalingFactor) &&
                   !scalingFactor.IsZero();
        }


        public InvertibleRealMatrixComposer SwapRows(int i1, int i2)
        {
            Debug.Assert(
                IsValidIndex(i1) &&
                IsValidIndex(i2)
            );

            if (i1 == i2) return this;

            //Console.WriteLine($"Swapping rows {rowIndex1} and {rowIndex2}");
            //Console.WriteLine("a1 = " + Array.ToMatlabCode() + ";");

            for (var j = 0; j < Size; j++)
            {
                (Matrix[i1, j], Matrix[i2, j]) = 
                    (Matrix[i2, j], Matrix[i1, j]);

                (MatrixInverse[j, i1], MatrixInverse[j, i2]) = 
                    (MatrixInverse[j, i2], MatrixInverse[j, i1]);
            }
            
            //Console.WriteLine("a2 = " + Array.ToMatlabCode() + ";");
            //Console.WriteLine(ToString());

            Debug.Assert(IsValid());

            return this;
        }
        
        public InvertibleRealMatrixComposer SwapColumns(int j1, int j2)
        {
            Debug.Assert(
                IsValidIndex(j1) &&
                IsValidIndex(j2)
            );

            if (j1 == j2) return this;

            //Console.WriteLine($"Swapping rows {rowIndex1} and {rowIndex2}");
            //Console.WriteLine("a1 = " + Array.ToMatlabCode() + ";");

            for (var i = 0; i < Size; i++)
            {
                (Matrix[i, j1], Matrix[i, j2]) = 
                    (Matrix[i, j2], Matrix[i, j1]);

                (MatrixInverse[j1, i], MatrixInverse[j2, i]) = 
                    (MatrixInverse[j2, i], MatrixInverse[j1, i]);
            }
            
            //Console.WriteLine("a2 = " + Array.ToMatlabCode() + ";");
            //Console.WriteLine(ToString());

            Debug.Assert(IsValid());

            return this;
        }

        public InvertibleRealMatrixComposer ScaleRow(int i, double scalingFactor)
        {
            Debug.Assert(
                IsValidIndex(i) &&
                IsValidScalingFactor(scalingFactor)
            );

            if (scalingFactor.IsOne()) return this;
            
            //Console.WriteLine($"Scaling row {rowIndex} by {scalingFactor:G}");
            //Console.WriteLine("a1 = " + Array.ToMatlabCode() + ";");

            var scalingFactorInverse = 1d / scalingFactor;
            
            for (var j = 0; j < Size; j++)
            {
                Matrix[i, j] *= scalingFactor;
                MatrixInverse[j, i] *= scalingFactorInverse;
            }
            
            //Console.WriteLine("a2 = " + Array.ToMatlabCode() + ";");
            //Console.WriteLine(ToString());

            Debug.Assert(IsValid());

            return this;
        }
        
        public InvertibleRealMatrixComposer ScaleColumn(int j, double scalingFactor)
        {
            Debug.Assert(
                IsValidIndex(j) &&
                IsValidScalingFactor(scalingFactor)
            );

            if (scalingFactor.IsOne()) return this;
            
            //Console.WriteLine($"Scaling row {rowIndex} by {scalingFactor:G}");
            //Console.WriteLine("a1 = " + Array.ToMatlabCode() + ";");

            var scalingFactorInverse = 1d / scalingFactor;

            for (var i = 0; i < Size; i++)
            {
                Matrix[i, j] *= scalingFactor;
                MatrixInverse[j, i] *= scalingFactorInverse;
            }
            
            //Console.WriteLine("a2 = " + Array.ToMatlabCode() + ";");
            //Console.WriteLine(ToString());

            Debug.Assert(IsValid());

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public InvertibleRealMatrixComposer AddRow(int i, int sourceRowIndex)
        {
            return AddScaledRow(i, 1, sourceRowIndex);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public InvertibleRealMatrixComposer AddColumn(int j, int sourceRowIndex)
        {
            return AddScaledColumn(j, 1, sourceRowIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public InvertibleRealMatrixComposer SubtractRow(int i, int sourceRowIndex)
        {
            return AddScaledRow(i, -1, sourceRowIndex);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public InvertibleRealMatrixComposer SubtractColumn(int j, int sourceRowIndex)
        {
            return AddScaledColumn(j, -1, sourceRowIndex);
        }

        public InvertibleRealMatrixComposer AddScaledRow(int i, double scalingFactor, int sourceIndex)
        {
            Debug.Assert(
                IsValidIndex(i) &&
                IsValidIndex(sourceIndex) &&
                IsValidScalingFactor(scalingFactor)
            );

            if (i == sourceIndex)
                return ScaleRow(i, 1 + scalingFactor);

            //Console.WriteLine($"Adding scaled row {sourceRowIndex} by {scalingFactor:G} to row {rowIndex}");
            //Console.WriteLine("a1 = " + Array.ToMatlabCode() + ";");

            for (var j = 0; j < Size; j++)
            {
                Matrix[i, j] += Matrix[sourceIndex, j] * scalingFactor;
                MatrixInverse[j, sourceIndex] -= MatrixInverse[j, i] * scalingFactor;
            }
            
            //Console.WriteLine("a2 = " + Array.ToMatlabCode() + ";");
            //Console.WriteLine(ToString());

            Debug.Assert(IsValid());

            return this;
        }
        
        public InvertibleRealMatrixComposer AddScaledColumn(int j, double scalingFactor, int sourceIndex)
        {
            Debug.Assert(
                IsValidIndex(j) &&
                IsValidIndex(sourceIndex) &&
                IsValidScalingFactor(scalingFactor)
            );

            if (j == sourceIndex)
                return ScaleColumn(j, 1 + scalingFactor);

            //Console.WriteLine($"Adding scaled row {sourceRowIndex} by {scalingFactor:G} to row {rowIndex}");
            //Console.WriteLine("a1 = " + Array.ToMatlabCode() + ";");

            for (var i = 0; i < Size; i++)
            {
                Matrix[i, j] += Matrix[i, sourceIndex] * scalingFactor;
                MatrixInverse[sourceIndex, i] -= MatrixInverse[j, i] * scalingFactor;
            }
            
            //Console.WriteLine("a2 = " + Array.ToMatlabCode() + ";");
            //Console.WriteLine(ToString());

            Debug.Assert(IsValid());

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public InvertibleRealMatrixComposer SubtractScaledRow(int i, double scalingFactor, int sourceIndex)
        {
            return AddScaledRow(i, -scalingFactor, sourceIndex);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public InvertibleRealMatrixComposer SubtractScaledColumn(int j, double scalingFactor, int sourceIndex)
        {
            return AddScaledColumn(j, -scalingFactor, sourceIndex);
        }


        public InvertibleRealMatrixComposer ApplyRandomScalingOperations(Random randGen, int maxOpCount = 10)
        {
            var operationCount = randGen.Next(maxOpCount);

            while (operationCount > 0)
            {
                var operationIndex = randGen.Next();

                if (operationIndex.IsEven())
                {
                    var (index, sourceIndex) = randGen.GetIndexPair(Size, true);
                    var scalingFactor = randGen.GetItem(-1, 1, -0.5, 0.5, -2, 2);
                    
                    AddScaledRow(index, scalingFactor, sourceIndex);
                }

                else
                {
                    var (index, sourceIndex) = randGen.GetIndexPair(Size, true);
                    var scalingFactor = randGen.GetItem(-1, 1, -0.5, 0.5, -2, 2);
                    
                    AddScaledColumn(index, scalingFactor, sourceIndex);
                }

                operationCount--;
            }

            Debug.Assert(IsValid());

            return this;
        }

        public InvertibleRealMatrixComposer ApplyRandomOperations(Random randGen, int maxOpCount = 10)
        {
            var operationCount = randGen.Next(maxOpCount);

            while (operationCount > 0)
            {
                var operationIndex = randGen.Next(6);

                if (operationIndex == 0)
                {
                    var (index1, index2) = randGen.GetIndexPair(Size, true);

                    SwapRows(index1, index2);
                }
                
                else if (operationIndex == 1)
                {
                    var (index1, index2) = randGen.GetIndexPair(Size, true);

                    SwapColumns(index1, index2);
                }

                else if (operationIndex == 2)
                {
                    var index = randGen.Next(Size);
                    var scalingFactor = randGen.GetItem(-1, 0.5, -0.5, 2, -2);

                    ScaleRow(index, scalingFactor);
                }
                
                else if (operationIndex == 3)
                {
                    var index = randGen.Next(Size);
                    var scalingFactor = randGen.GetItem(-1, 0.5, -0.5, 2, -2);

                    ScaleColumn(index, scalingFactor);
                }

                else if (operationIndex == 4)
                {
                    var (index, sourceIndex) = randGen.GetIndexPair(Size, true);
                    var scalingFactor = randGen.GetItem(-1, 1, -0.5, 0.5, -2, 2);
                    
                    AddScaledRow(index, scalingFactor, sourceIndex);
                }

                else
                {
                    var (index, sourceIndex) = randGen.GetIndexPair(Size, true);
                    var scalingFactor = randGen.GetItem(-1, 1, -0.5, 0.5, -2, 2);
                    
                    AddScaledColumn(index, scalingFactor, sourceIndex);
                }

                operationCount--;
            }

            Debug.Assert(IsValid());

            return this;
        }

        public InvertibleRealMatrixComposer ApplyRandomRowOperations(Random randGen, int maxOpCount = 10)
        {
            var operationCount = randGen.Next(maxOpCount);

            while (operationCount > 0)
            {
                var operationIndex = randGen.Next(3);

                if (operationIndex == 0)
                {
                    var (index1, index2) = randGen.GetIndexPair(Size, true);

                    SwapRows(index1, index2);
                }

                else if (operationIndex == 1)
                {
                    var index = randGen.Next(Size);
                    var scalingFactor = randGen.GetItem(-1, 0.5, -0.5, 2, -2);

                    ScaleRow(index, scalingFactor);
                }

                else
                {
                    var (index, sourceIndex) = randGen.GetIndexPair(Size, true);
                    var scalingFactor = randGen.GetItem(-1, 1, -0.5, 0.5, -2, 2);
                    
                    AddScaledRow(index, scalingFactor, sourceIndex);
                }

                operationCount--;
            }

            Debug.Assert(IsValid());

            return this;
        }
        
        public InvertibleRealMatrixComposer ApplyRandomColumnOperations(Random randGen, int maxOpCount = 10)
        {
            var operationCount = randGen.Next(maxOpCount);

            while (operationCount > 0)
            {
                var operationIndex = randGen.Next(3);

                if (operationIndex == 0)
                {
                    var (index1, index2) = randGen.GetIndexPair(Size, true);

                    SwapColumns(index1, index2);
                }

                else if (operationIndex == 1)
                {
                    var index = randGen.Next(Size);
                    var scalingFactor = randGen.GetItem(-1, 0.5, -0.5, 2, -2);

                    ScaleColumn(index, scalingFactor);
                }

                else
                {
                    var (index, sourceIndex) = randGen.GetIndexPair(Size, true);
                    var scalingFactor = randGen.GetItem(-1, 1, -0.5, 0.5, -2, 2);
                    
                    AddScaledColumn(index, scalingFactor, sourceIndex);
                }

                operationCount--;
            }

            Debug.Assert(IsValid());

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> GetMatrix()
        {
            return Matrix.Clone();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> GetMatrixInverse()
        {
            return MatrixInverse.Clone();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            var m1 = GetMatrix();
            var m2 = GetMatrixInverse();

            return new StringBuilder()
                .AppendLine(m1.ToString())
                .AppendLine()
                .AppendLine(m2.ToString())
                .ToString();
        }
    }
}
