using System;
using System.Diagnostics;
using System.Text;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Random;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices
{
    public class Float64InvertibleArrayComposer
    {
        
        public static Float64InvertibleArrayComposer Create(int size)
        {
            return new Float64InvertibleArrayComposer(size);
        }


        public int Size { get; }

        protected double[,] Array { get; }
        
        protected double[,] ArrayInverse { get; }

        
        
        private Float64InvertibleArrayComposer(int size)
        {
            Debug.Assert(size > 1);

            Size = size;
            Array = Float64ArrayUtils.CreateIdentity(size);
            ArrayInverse = Float64ArrayUtils.CreateIdentity(size);
        }


        public bool IsValid(double zeroEpsilon = Float64Utils.ZeroEpsilon)
        {
            var array = Array.MatrixProduct(ArrayInverse);

            for (var i = 0; i < Size; i++)
            for (var j = 0; j < Size; j++)
            {
                var value = i == j 
                    ? array[i, j] - 1 
                    : array[i, j];

                if (!value.IsNearZero(zeroEpsilon))
                    return false;
            }

            return true;
        }

        
        public bool IsValidIndex(int rowIndex)
        {
            return rowIndex >= 0 && rowIndex < Size;
        }

        
        public bool IsValidScalingFactor(double scalingFactor)
        {
            return !double.IsNaN(scalingFactor) &&
                   !double.IsInfinity(scalingFactor) &&
                   !scalingFactor.IsZero();
        }


        public Float64InvertibleArrayComposer SwapRows(int i1, int i2)
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
                (Array[i1, j], Array[i2, j]) = 
                    (Array[i2, j], Array[i1, j]);

                (ArrayInverse[j, i1], ArrayInverse[j, i2]) = 
                    (ArrayInverse[j, i2], ArrayInverse[j, i1]);
            }
            
            //Console.WriteLine("a2 = " + Array.ToMatlabCode() + ";");
            //Console.WriteLine(ToString());

            Debug.Assert(IsValid());

            return this;
        }
        
        public Float64InvertibleArrayComposer SwapColumns(int j1, int j2)
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
                (Array[i, j1], Array[i, j2]) = 
                    (Array[i, j2], Array[i, j1]);

                (ArrayInverse[j1, i], ArrayInverse[j2, i]) = 
                    (ArrayInverse[j2, i], ArrayInverse[j1, i]);
            }
            
            //Console.WriteLine("a2 = " + Array.ToMatlabCode() + ";");
            //Console.WriteLine(ToString());

            Debug.Assert(IsValid());

            return this;
        }

        public Float64InvertibleArrayComposer ScaleRow(int i, double scalingFactor)
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
                Array[i, j] *= scalingFactor;
                ArrayInverse[j, i] *= scalingFactorInverse;
            }
            
            //Console.WriteLine("a2 = " + Array.ToMatlabCode() + ";");
            //Console.WriteLine(ToString());

            Debug.Assert(IsValid());

            return this;
        }
        
        public Float64InvertibleArrayComposer ScaleColumn(int j, double scalingFactor)
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
                Array[i, j] *= scalingFactor;
                ArrayInverse[j, i] *= scalingFactorInverse;
            }
            
            //Console.WriteLine("a2 = " + Array.ToMatlabCode() + ";");
            //Console.WriteLine(ToString());

            Debug.Assert(IsValid());

            return this;
        }

        
        public Float64InvertibleArrayComposer AddRow(int i, int sourceRowIndex)
        {
            return AddScaledRow(i, 1, sourceRowIndex);
        }
        
        
        public Float64InvertibleArrayComposer AddColumn(int j, int sourceRowIndex)
        {
            return AddScaledColumn(j, 1, sourceRowIndex);
        }

        
        public Float64InvertibleArrayComposer SubtractRow(int i, int sourceRowIndex)
        {
            return AddScaledRow(i, -1, sourceRowIndex);
        }
        
        
        public Float64InvertibleArrayComposer SubtractColumn(int j, int sourceRowIndex)
        {
            return AddScaledColumn(j, -1, sourceRowIndex);
        }

        public Float64InvertibleArrayComposer AddScaledRow(int i, double scalingFactor, int sourceIndex)
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
                Array[i, j] += Array[sourceIndex, j] * scalingFactor;
                ArrayInverse[j, sourceIndex] -= ArrayInverse[j, i] * scalingFactor;
            }
            
            //Console.WriteLine("a2 = " + Array.ToMatlabCode() + ";");
            //Console.WriteLine(ToString());

            Debug.Assert(IsValid());

            return this;
        }
        
        public Float64InvertibleArrayComposer AddScaledColumn(int j, double scalingFactor, int sourceIndex)
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
                Array[i, j] += Array[i, sourceIndex] * scalingFactor;
                ArrayInverse[sourceIndex, i] -= ArrayInverse[j, i] * scalingFactor;
            }
            
            //Console.WriteLine("a2 = " + Array.ToMatlabCode() + ";");
            //Console.WriteLine(ToString());

            Debug.Assert(IsValid());

            return this;
        }

        
        public Float64InvertibleArrayComposer SubtractScaledRow(int i, double scalingFactor, int sourceIndex)
        {
            return AddScaledRow(i, -scalingFactor, sourceIndex);
        }
        
        
        public Float64InvertibleArrayComposer SubtractScaledColumn(int j, double scalingFactor, int sourceIndex)
        {
            return AddScaledColumn(j, -scalingFactor, sourceIndex);
        }


        public Float64InvertibleArrayComposer ApplyRandomScalingOperations(Random randGen, int maxOpCount = 10)
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

        public Float64InvertibleArrayComposer ApplyRandomOperations(Random randGen, int maxOpCount = 10)
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

        public Float64InvertibleArrayComposer ApplyRandomRowOperations(Random randGen, int maxOpCount = 10)
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
        
        public Float64InvertibleArrayComposer ApplyRandomColumnOperations(Random randGen, int maxOpCount = 10)
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


        
        public double[,] GetArray()
        {
            return Array.GetArrayCopy();
        }
        
        
        public double[,] GetArrayInverse()
        {
            return ArrayInverse.GetArrayCopy();
        }

        
        public Matrix GetMatrix()
        {
            return (Matrix) Matrix.Build.DenseOfArray(Array);
        }
        
        
        public Matrix GetMatrixInverse()
        {
            return (Matrix) Matrix.Build.DenseOfArray(ArrayInverse);
        }


        
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
