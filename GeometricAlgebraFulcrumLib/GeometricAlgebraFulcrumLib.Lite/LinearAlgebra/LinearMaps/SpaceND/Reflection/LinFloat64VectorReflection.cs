using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Reflection
{
    public sealed class LinFloat64VectorReflection :
        ILinFloat64UnilinearMap
    {
        public Float64Vector ReflectionVector { get; }

        public int VSpaceDimensions { get; }

        public bool SwapsHandedness
            => true;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorReflection(int dimensions, Float64Vector vector)
        {
            Debug.Assert(vector.IsNearUnit());

            VSpaceDimensions = dimensions;
            ReflectionVector = vector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return ReflectionVector.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsIdentity()
        {
            return false;
        }

        public bool IsReflection()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearIdentity(double epsilon = 1E-12)
        {
            return false;
        }

        public bool IsNearReflection(double epsilon = 1E-12)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector MapBasisVector(int basisIndex)
        {
            Debug.Assert(basisIndex >= 0);

            var s = 2d * ReflectionVector[basisIndex];

            return Float64VectorComposer
                .Create()
                .SetVector(ReflectionVector, s)
                .SubtractTerm(basisIndex, 1d)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector MapVector(Float64Vector vector)
        {
            var s = 2d * Float64ArrayUtils.VectorDot(vector, ReflectionVector);

            return Float64VectorComposer
                .Create()
                .SetVector(ReflectionVector, s)
                .SubtractVector(vector)
                .GetVector();
        }

        public IEnumerable<KeyValuePair<int, Float64Vector>> GetMappedBasisVectors(int vSpaceDimensions)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> ToMatrix(int rowCount, int colCount)
        {
            var columnList =
                colCount
                    .GetRange()
                    .Select(i => MapBasisVector(i).ToArray(rowCount));

            return Matrix<double>
                .Build
                .DenseOfColumnArrays(columnList);
        }

        public LinFloat64UnilinearMap ToUnilinearMap(int vSpaceDimensions)
        {
            throw new NotImplementedException();
        }

        public double[,] ToArray(int rowCount, int colCount)
        {
            var array = new double[rowCount, colCount];

            for (var j = 0; j < colCount; j++)
            {
                var columnVector = MapBasisVector(j);

                if (columnVector.IsZero)
                    continue;

                foreach (var (i, s) in columnVector)
                    array[i, j] = s;
            }

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorReflection GetVectorReflectionInverse()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinFloat64UnilinearMap GetInverseMap()
        {
            return GetVectorReflectionInverse();
        }
    }
}
