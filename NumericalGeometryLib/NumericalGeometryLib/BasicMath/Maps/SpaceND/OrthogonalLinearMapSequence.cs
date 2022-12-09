using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Random;
using MathNet.Numerics.LinearAlgebra;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Reflection;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Rotation;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.SubSpaces;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps.SpaceND
{
    /// <summary>
    /// A basic linear map is either a simple vector-to-vector rotation or a
    /// directional vector scaling
    /// </summary>
    public sealed class OrthogonalLinearMapSequence :
        ILinearMap
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OrthogonalLinearMapSequence Create(int dimensions)
        {
            return new OrthogonalLinearMapSequence(
                VectorToVectorRotationSequence.Create(dimensions), 
                HyperPlaneNormalReflectionSequence.Create(dimensions)
            );
        }

        public static OrthogonalLinearMapSequence CreateFromMatrix(Matrix<double> matrix)
        {
            if (matrix.RowCount != matrix.ColumnCount)
                throw new InvalidOperationException();

            Debug.Assert(
                matrix.Determinant().Abs().IsNearOne()
            );

            var dimensions = matrix.RowCount;
            var rotationSequence = VectorToVectorRotationSequence.Create(dimensions);
            var reflectionSequence = HyperPlaneNormalReflectionSequence.Create(dimensions);

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
                        (Float64PlaneSubspace) subspace.Subspace;

                    var sourceVector = planeSubspace.BasisVector1;
                    var targetVector = planeSubspace.BasisVector2;

                    rotationSequence.AppendMap(sourceVector, targetVector, angle);
                }
                else if (subspace.SubspaceDimensions == 1)
                {
                    var scalingFactor =
                        subspace.EigenValue.Real;

                    var scalingVector =
                        ((Float64LineSubspace)subspace.Subspace).BasisVector;

                    if (scalingFactor.IsNearMinusOne())
                        reflectionSequence.AppendMap(scalingVector);
                    else
                        throw new InvalidOperationException();
                }
                else
                    throw new InvalidOperationException();
            }

            return new OrthogonalLinearMapSequence(
                rotationSequence,
                reflectionSequence
            );
        }
        
        public static OrthogonalLinearMapSequence CreateRandom(System.Random random, int dimensions, int count)
        {
            var matrix = HyperPlaneNormalReflectionSequence.CreateRandom(
                random,
                dimensions,
                count
            ).GetMatrix();

            return CreateFromMatrix(matrix);
        }

        public static OrthogonalLinearMapSequence CreateRandomOrthogonal(System.Random random, int dimensions, int count)
        {
            var mapSequence = OrthogonalLinearMapSequence.Create(dimensions);

            var vectorList =
                random.GetOrthonormalVectors(dimensions, count);

            for (var i = 0; i < count / 2; i++)
            {
                var u = vectorList[2 * i].ToTuple();
                var v = vectorList[2 * i + 1].ToTuple();
                var angle = random.GetAngle();

                mapSequence.RotationSequence.AppendMap(u, v, angle);
            }

            if (count.IsOdd())
            {
                var u = vectorList[^1].ToTuple();

                mapSequence.ReflectionSequence.AppendMap(u);
            }
            
            return mapSequence;
        }


        public int Dimensions 
            => RotationSequence.Dimensions;

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
        
        public VectorToVectorRotationSequence RotationSequence { get; }

        public HyperPlaneNormalReflectionSequence ReflectionSequence { get; }
        
        public int MapCount 
            => RotationSequence.Count +
               ReflectionSequence.Count;

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private OrthogonalLinearMapSequence(VectorToVectorRotationSequence rotationSequence, HyperPlaneNormalReflectionSequence reflectionSequence)
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

            for (var basisIndex = 0; basisIndex < Dimensions; basisIndex++)
            {
                var isSameVectorBasis =
                    MapVectorBasis(basisIndex).IsVectorBasis(basisIndex);

                if (!isSameVectorBasis) return false;
            }

            return true;
        }

        public bool IsNearIdentity(double epsilon = 1E-12)
        {
            for (var basisIndex = 0; basisIndex < Dimensions; basisIndex++)
            {
                var isSameVectorBasis =
                    MapVectorBasis(basisIndex).IsNearVectorBasis(basisIndex, epsilon);

                if (!isSameVectorBasis) return false;
            }

            return true;
        }
        
        /// <summary>
        /// Test if all rotation planes and reflection normals in this sequence
        /// are nearly pair-wise orthogonal
        /// </summary>
        /// <returns></returns>
        public bool IsNearOrthogonalMapsSequence(double epsilon = 1e-12)
        {
            if (ReflectionSequence.Count + 2 * RotationSequence.Count > Dimensions)
                return false;

            for (var i = 0; i < RotationSequence.Count; i++)
            {
                var u1 = RotationSequence[i].SourceVector;
                var v1 = RotationSequence[i].TargetVector;

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
                    var u2 = RotationSequence[j].SourceVector;
                    var v2 = RotationSequence[j].TargetVector;

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
        public Float64Tuple MapVectorBasis(int basisIndex)
        {
            if (RotationSequence.Count == 0 && ReflectionSequence.Count == 0)
                return Float64Tuple.CreateBasis(Dimensions, basisIndex);

            var x = new double[Dimensions];
            x[basisIndex] = 1d;

            return Float64Tuple.Create(
                MapVectorInPlace(x)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple MapVector(Float64Tuple vector)
        {
            if (RotationSequence.Count == 0 && ReflectionSequence.Count == 0)
                return vector;

            var x = 
                vector.GetScalarArrayCopy();

            return Float64Tuple.Create(
                MapVectorInPlace(x)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> GetMatrix()
        {
            var columnList =
                Dimensions
                    .GetRange()
                    .Select(i => MapVectorBasis(i).ScalarArray);

            return Matrix<double>
                .Build
                .DenseOfColumnArrays(columnList);
        }

        public double[,] GetArray()
        {
            var array = new double[Dimensions, Dimensions];

            for (var j = 0; j < Dimensions; j++)
            {
                var columnVector = MapVectorBasis(j).ScalarArray;

                for (var i = 0; i < Dimensions; i++) 
                    array[i, j] = columnVector[i];
            }

            return array;
        }

        /// <summary>
        /// Create a new sequence containing the minimum number of pair-wise
        /// orthogonal rotations and reflections equivalent to this one
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OrthogonalLinearMapSequence ReduceSequence()
        {
            return CreateFromMatrix(GetMatrix());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OrthogonalLinearMapSequence GetBasicLinearMapSequenceInverse()
        {
            var rotationSequence = 
                RotationSequence.GetVectorToVectorRotationSequenceInverse();

            var reflectionSequence = 
                ReflectionSequence.GetHyperPlaneReflectionSequenceInverse();
            
            return new OrthogonalLinearMapSequence(
                rotationSequence,
                reflectionSequence
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinearMap GetLinearMapInverse()
        {
            return GetBasicLinearMapSequenceInverse();
        }
    }
}
