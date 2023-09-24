using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Composers;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Reflection;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.SubSpaces.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND
{
    /// <summary>
    /// A basic linear map is either a simple vector-to-vector rotation or a
    /// directional vector scaling
    /// </summary>
    public sealed class LinFloat64OrthogonalLinearMapSequence :
        ILinFloat64UnilinearMap
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64OrthogonalLinearMapSequence Create(int dimensions)
        {
            return new LinFloat64OrthogonalLinearMapSequence(
                LinFloat64PlanarRotationSequence.Create(dimensions),
                LinFloat64HyperPlaneNormalReflectionSequence.Create(dimensions)
            );
        }

        public static LinFloat64OrthogonalLinearMapSequence CreateFromMatrix(Matrix<double> matrix)
        {
            if (matrix.RowCount != matrix.ColumnCount)
                throw new InvalidOperationException();

            Debug.Assert(
                matrix.Determinant().Abs().IsNearOne()
            );

            var dimensions = matrix.RowCount;
            var rotationSequence = LinFloat64PlanarRotationSequence.Create(dimensions);
            var reflectionSequence = LinFloat64HyperPlaneNormalReflectionSequence.Create(dimensions);

            var subspaceList =
                matrix.GetSimpleEigenSubspaces();

            foreach (var subspace in subspaceList)
            {
                if (subspace.SubspaceDimensions == 2)
                {
                    var angle = Math.Atan2(
                        subspace.EigenValue.Imaginary,
                        subspace.EigenValue.Real
                    );

                    if (angle.IsNearZero())
                        continue;

                    var planeSubspace =
                        (LinFloat64PlaneSubspace)subspace.Subspace;

                    var sourceVector = planeSubspace.BasisVector1;
                    var targetVector = planeSubspace.BasisVector2;

                    rotationSequence.AppendMap(sourceVector, targetVector, angle);
                }
                else if (subspace.SubspaceDimensions == 1)
                {
                    var scalingFactor =
                        subspace.EigenValue.Real;

                    var scalingVector =
                        ((LinFloat64LineSubspace)subspace.Subspace).BasisVector;

                    if (scalingFactor.IsNearMinusOne())
                        reflectionSequence.AppendMap(scalingVector);
                    else
                        throw new InvalidOperationException();
                }
                else
                    throw new InvalidOperationException();
            }

            return new LinFloat64OrthogonalLinearMapSequence(
                rotationSequence,
                reflectionSequence
            );
        }

        public static LinFloat64OrthogonalLinearMapSequence CreateRandom(System.Random random, int dimensions, int count)
        {
            var matrix = LinFloat64HyperPlaneNormalReflectionSequence.CreateRandom(
                random,
                dimensions,
                count
            ).ToMatrix(dimensions, dimensions);

            return CreateFromMatrix(matrix);
        }

        public static LinFloat64OrthogonalLinearMapSequence CreateRandomOrthogonal(System.Random random, int dimensions, int count)
        {
            var mapSequence = Create(dimensions);

            var vectorList =
                random.GetMathNetOrthonormalVectors(dimensions, count);

            for (var i = 0; i < count / 2; i++)
            {
                var u = vectorList[2 * i].CreateLinVector();
                var v = vectorList[2 * i + 1].CreateLinVector();
                var angle = random.GetAngle();

                mapSequence.RotationSequence.AppendMap(u, v, angle);
            }

            if (count.IsOdd())
            {
                var u = vectorList[^1].CreateLinVector();

                mapSequence.ReflectionSequence.AppendMap(u);
            }

            return mapSequence;
        }


        public int VSpaceDimensions
            => RotationSequence.VSpaceDimensions;

        public bool SwapsHandedness
            => ReflectionSequence.Count.IsOdd();

        public bool IsRotation
            => ReflectionSequence.Count == 0;

        public bool IsReflection
            => RotationSequence.Count == 0;

        public bool IsScaling
            => RotationSequence.Count == 0 &&
               ReflectionSequence.Count == 0;

        public bool HasRotation
            => RotationSequence.Count > 0;

        public bool HasReflection
            => ReflectionSequence.Count > 0;

        public LinFloat64PlanarRotationSequence RotationSequence { get; }

        public LinFloat64HyperPlaneNormalReflectionSequence ReflectionSequence { get; }

        public int MapCount
            => RotationSequence.Count +
               ReflectionSequence.Count;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64OrthogonalLinearMapSequence(LinFloat64PlanarRotationSequence rotationSequence, LinFloat64HyperPlaneNormalReflectionSequence reflectionSequence)
        {
            RotationSequence = rotationSequence;
            ReflectionSequence = reflectionSequence;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return RotationSequence.IsValid() &&
                   ReflectionSequence.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsIdentity()
        {
            if (RotationSequence.Count == 0 && ReflectionSequence.Count == 0)
                return true;

            for (var basisIndex = 0; basisIndex < VSpaceDimensions; basisIndex++)
            {
                var isSameVectorBasis =
                    MapBasisVector(basisIndex).IsVectorBasis(basisIndex);

                if (!isSameVectorBasis) return false;
            }

            return true;
        }

        bool ILinFloat64UnilinearMap.IsReflection()
        {
            throw new NotImplementedException();
        }

        public bool IsNearIdentity(double epsilon = 1E-12)
        {
            for (var basisIndex = 0; basisIndex < VSpaceDimensions; basisIndex++)
            {
                var isSameVectorBasis =
                    MapBasisVector(basisIndex).IsNearVectorBasis(basisIndex, epsilon);

                if (!isSameVectorBasis) return false;
            }

            return true;
        }

        public bool IsNearReflection(double epsilon = 1E-12)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Test if all rotation planes and reflection normals in this sequence
        /// are nearly pair-wise orthogonal
        /// </summary>
        /// <returns></returns>
        public bool IsNearOrthogonalMapsSequence(double epsilon = 1e-12)
        {
            if (ReflectionSequence.Count + 2 * RotationSequence.Count > VSpaceDimensions)
                return false;

            for (var i = 0; i < RotationSequence.Count; i++)
            {
                var u1 = RotationSequence[i].BasisVector1;
                var v1 = RotationSequence[i].MapBasisVector1();

                foreach (var reflection in ReflectionSequence)
                {
                    var u2 = reflection.ReflectionNormal;

                    if (!u1.IsNearOrthogonalTo(u2, epsilon))
                        return false;
                    if (!v1.IsNearOrthogonalTo(u2, epsilon))
                        return false;
                }

                for (var j = i + 1; j < RotationSequence.Count; j++)
                {
                    var u2 = RotationSequence[j].BasisVector1;
                    var v2 = RotationSequence[j].MapBasisVector1();

                    if (!u1.IsNearOrthogonalTo(u2, epsilon))
                        return false;
                    if (!u1.IsNearOrthogonalTo(v2, epsilon))
                        return false;
                    if (!v1.IsNearOrthogonalTo(u2, epsilon))
                        return false;
                    if (!v1.IsNearOrthogonalTo(v2, epsilon))
                        return false;
                }
            }

            for (var i = 0; i < ReflectionSequence.Count; i++)
            {
                var u1 = ReflectionSequence[i].ReflectionNormal;

                for (var j = i + 1; j < ReflectionSequence.Count; j++)
                {
                    var u2 = ReflectionSequence[j].ReflectionNormal;

                    if (!u1.IsNearOrthogonalTo(u2, epsilon))
                        return false;
                }
            }

            return true;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] MapVectorInPlace(double[] vector)
        {
            if (RotationSequence.Count > 0)
                vector = RotationSequence.MapVectorInPlace(vector);

            if (ReflectionSequence.Count > 0)
                vector = ReflectionSequence.MapVectorInPlace(vector);

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector MapBasisVector(int basisIndex)
        {
            var vector = basisIndex.CreateLinVector();

            if (RotationSequence.Count > 0)
                vector = RotationSequence.MapVector(vector);

            if (ReflectionSequence.Count > 0)
                vector = ReflectionSequence.MapVector(vector);

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector MapVector(Float64Vector vector)
        {
            var vector1 = vector;

            if (RotationSequence.Count > 0)
                vector1 = RotationSequence.MapVector(vector1);

            if (ReflectionSequence.Count > 0)
                vector1 = ReflectionSequence.MapVector(vector1);

            return vector1;
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

                foreach (var (i, scalar) in columnVector)
                    array[i, j] = scalar;
            }

            return array;
        }

        /// <summary>
        /// Create a new sequence containing the minimum number of pair-wise
        /// orthogonal rotations and reflections equivalent to this one
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64OrthogonalLinearMapSequence ReduceSequence()
        {
            return CreateFromMatrix(ToMatrix(VSpaceDimensions, VSpaceDimensions));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64OrthogonalLinearMapSequence GetBasicLinearMapSequenceInverse()
        {
            var rotationSequence =
                RotationSequence.GetVectorToVectorRotationSequenceInverse();

            var reflectionSequence =
                ReflectionSequence.GetHyperPlaneReflectionSequenceInverse();

            return new LinFloat64OrthogonalLinearMapSequence(
                rotationSequence,
                reflectionSequence
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinFloat64UnilinearMap GetInverseMap()
        {
            return GetBasicLinearMapSequenceInverse();
        }
    }
}
