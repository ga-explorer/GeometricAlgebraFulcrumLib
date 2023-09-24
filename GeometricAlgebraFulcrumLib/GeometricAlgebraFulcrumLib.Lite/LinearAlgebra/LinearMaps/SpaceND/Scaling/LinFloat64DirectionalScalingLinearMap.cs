using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Scaling
{
    /// <summary>
    /// This class represents the most basic kind of linear operations:
    /// Scaling by a factor in a given direction.
    /// </summary>
    public abstract class LinFloat64DirectionalScalingLinearMap :
        ILinFloat64DirectionalScalingLinearMap
    {
        public abstract int VSpaceDimensions { get; }

        public bool SwapsHandedness
            => ScalingFactor < 0;

        public abstract double ScalingFactor { get; }

        public abstract Float64Vector ScalingVector { get; }


        public abstract bool IsValid();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsIdentity()
        {
            return ScalingFactor.IsOne();
        }

        public bool IsReflection()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearIdentity(double epsilon = 1E-12)
        {
            return ScalingFactor.IsNearOne(epsilon);
        }

        public bool IsNearReflection(double epsilon = 1E-12)
        {
            throw new NotImplementedException();
        }

        public abstract Float64Vector MapBasisVector(int basisIndex);

        public abstract Float64Vector MapVector(Float64Vector vector);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<int, Float64Vector>> GetMappedBasisVectors(int vSpaceDimensions)
        {
            return vSpaceDimensions
                .GetRange(i =>
                    new KeyValuePair<int, Float64Vector>(
                        i,
                        MapBasisVector(i)
                    )
                ).Where(p => !p.Value.IsZero);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual Matrix<double> ToMatrix(int rowCount, int colCount)
        {
            var columnList =
                colCount
                    .GetRange()
                    .Select(i => MapBasisVector(i).ToArray(rowCount));

            return Matrix<double>
                .Build
                .DenseOfColumnArrays(columnList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual LinFloat64UnilinearMap ToUnilinearMap(int vSpaceDimensions)
        {
            return vSpaceDimensions.CreateLinUnilinearMap(MapBasisVector);
        }

        public virtual double[,] ToArray(int rowCount, int colCount)
        {
            var array = new double[rowCount, colCount];

            for (var j = 0; j < colCount; j++)
            {
                var columnVector = MapBasisVector(j);

                foreach (var (i, scalar) in columnVector)
                {
                    if (i >= rowCount)
                        throw new InvalidOperationException();

                    array[i, j] = scalar;
                }
            }

            return array;
        }

        public abstract ILinFloat64DirectionalScalingLinearMap GetDirectionalScalingInverse();

        public abstract LinFloat64VectorDirectionalScaling ToVectorDirectionalScaling();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinFloat64UnilinearMap GetInverseMap()
        {
            return GetDirectionalScalingInverse();
        }
    }
}