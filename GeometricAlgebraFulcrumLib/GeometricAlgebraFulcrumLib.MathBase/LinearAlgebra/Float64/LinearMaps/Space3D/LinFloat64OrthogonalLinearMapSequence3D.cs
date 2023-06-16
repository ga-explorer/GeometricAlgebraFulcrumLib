using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space3D.Composers;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space3D.Reflection;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.SubSpaces.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space3D
{
    /// <summary>
    /// A basic linear map is either a simple vector-to-vector rotation or a
    /// directional vector scaling
    /// </summary>
    public sealed class LinFloat64OrthogonalLinearMapSequence3D :
        ILinFloat64UnilinearMap3D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64OrthogonalLinearMapSequence3D Create()
        {
            return new LinFloat64OrthogonalLinearMapSequence3D(
                LinFloat64RotationComposer3D.Create(), 
                LinFloat64HyperPlaneNormalReflectionSequence3D.Create()
            );
        }

        public static LinFloat64OrthogonalLinearMapSequence3D CreateFromMatrix(SquareMatrix3 matrix)
        {
            Debug.Assert(
                matrix.Determinant.Abs().IsNearOne()
            );

            var rotationSequence = LinFloat64RotationComposer3D.Create();
            var reflectionSequence = LinFloat64HyperPlaneNormalReflectionSequence3D.Create();

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
                        (LinFloat64PlaneSubspace3D) subspace.Subspace;

                    var sourceVector = planeSubspace.BasisVector1;
                    var targetVector = planeSubspace.BasisVector2;

                    rotationSequence.AppendRotation(sourceVector, targetVector, angle);
                }
                else if (subspace.SubspaceDimensions == 1)
                {
                    var scalingFactor =
                        subspace.EigenValue.Real;

                    var scalingVector =
                        ((LinFloat64LineSubspace3D)subspace.Subspace).BasisVector;

                    if (scalingFactor.IsNearMinusOne())
                        reflectionSequence.AppendMap(scalingVector);
                    else
                        throw new InvalidOperationException();
                }
                else
                    throw new InvalidOperationException();
            }

            return new LinFloat64OrthogonalLinearMapSequence3D(
                rotationSequence,
                reflectionSequence
            );
        }
        
        public static LinFloat64OrthogonalLinearMapSequence3D CreateRandom(System.Random random, int count)
        {
            var matrix = 
                LinFloat64HyperPlaneNormalReflectionSequence3D
                    .CreateRandom(random, count)
                    .ToSquareMatrix3();

            return CreateFromMatrix(matrix);
        }

        public static LinFloat64OrthogonalLinearMapSequence3D CreateRandomOrthogonal(System.Random random, int count)
        {
            var mapSequence = Create();

            var vectorList =
                random.GetOrthonormalVectors(3, count);

            for (var i = 0; i < count / 2; i++)
            {
                var u = vectorList[2 * i].ToVector3D();
                var v = vectorList[2 * i + 1].ToVector3D();
                var angle = random.GetAngle();

                mapSequence.RotationSequence.AppendRotation(u, v, angle);
            }

            if (count.IsOdd())
            {
                var u = vectorList[^1].ToVector3D();

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
        
        public LinFloat64RotationComposer3D RotationSequence { get; }

        public LinFloat64HyperPlaneNormalReflectionSequence3D ReflectionSequence { get; }
        
        public int MapCount 
            => RotationSequence.Count +
               ReflectionSequence.Count;

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64OrthogonalLinearMapSequence3D(LinFloat64RotationComposer3D rotationSequence, LinFloat64HyperPlaneNormalReflectionSequence3D reflectionSequence)
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
        
        ///// <summary>
        ///// Test if all rotation planes and reflection normals in this sequence
        ///// are nearly pair-wise orthogonal
        ///// </summary>
        ///// <returns></returns>
        //public bool IsNearOrthogonalMapsSequence(double epsilon = 1e-12)
        //{
        //    if (ReflectionSequence.Count + 2 * RotationSequence.Count > VSpaceDimensions)
        //        return false;

        //    for (var i = 0; i < RotationSequence.Count; i++)
        //    {
        //        var u1 = RotationSequence[i].BasisVector1;
        //        var v1 = RotationSequence[i].GetRotatedBasisVector1();

        //        foreach (var reflection in ReflectionSequence)
        //        {
        //            var u2 = reflection.ReflectionNormal;

        //            if (!u1.IsNearOrthogonalTo(u2, epsilon)) 
        //                return false;
        //            if (!v1.IsNearOrthogonalTo(u2, epsilon)) 
        //                return false;
        //        }

        //        for (var j = i + 1; j < RotationSequence.Count; j++)
        //        {
        //            var u2 = RotationSequence[j].BasisVector1;
        //            var v2 = RotationSequence[j].GetRotatedBasisVector1();

        //            if (!u1.IsNearOrthogonalTo(u2, epsilon)) 
        //                return false;
        //            if (!u1.IsNearOrthogonalTo(v2, epsilon)) 
        //                return false;
        //            if (!v1.IsNearOrthogonalTo(u2, epsilon)) 
        //                return false;
        //            if (!v1.IsNearOrthogonalTo(v2, epsilon)) 
        //                return false;
        //        }
        //    }

        //    for (var i = 0; i < ReflectionSequence.Count; i++)
        //    {
        //        var u1 = ReflectionSequence[i].ReflectionNormal;

        //        for (var j = i + 1; j < ReflectionSequence.Count; j++)
        //        {
        //            var u2 = ReflectionSequence[j].ReflectionNormal;

        //            if (!u1.IsNearOrthogonalTo(u2, epsilon)) 
        //                return false;
        //        }
        //    }

        //    return true;
        //}


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public double[] MapVectorInPlace(double[] vector)
        //{
        //    if (RotationSequence.Count > 0) 
        //        vector = RotationSequence.MapVectorInPlace(vector);

        //    if (ReflectionSequence.Count > 0)
        //        vector = ReflectionSequence.MapVectorInPlace(vector);
            
        //    return vector;
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D MapBasisVector(int basisIndex)
        {
            var vector = 
                Float64Vector3D.BasisVectors[basisIndex];
            
            if (RotationSequence.Count > 0) 
                vector = RotationSequence.MapVector(vector);

            if (ReflectionSequence.Count > 0)
                vector = ReflectionSequence.MapVector(vector);
            
            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D MapVector(IFloat64Tuple3D vector)
        {
            var vector1 = 
                vector.ToVector3D();

            if (RotationSequence.Count > 0) 
                vector1 = RotationSequence.MapVector(vector1);

            if (ReflectionSequence.Count > 0)
                vector1 = ReflectionSequence.MapVector(vector1);
            
            return vector1;
        }
        
        /// <summary>
        /// Create a new sequence containing the minimum number of pair-wise
        /// orthogonal rotations and reflections equivalent to this one
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64OrthogonalLinearMapSequence3D ReduceSequence()
        {
            return CreateFromMatrix(this.ToSquareMatrix3());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64OrthogonalLinearMapSequence3D GetBasicLinearMapSequenceInverse()
        {
            var rotationSequence = 
                RotationSequence.SelfInverse();

            var reflectionSequence = 
                ReflectionSequence.GetHyperPlaneReflectionSequenceInverse();
            
            return new LinFloat64OrthogonalLinearMapSequence3D(
                rotationSequence,
                reflectionSequence
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinFloat64UnilinearMap3D GetInverseMap()
        {
            return GetBasicLinearMapSequenceInverse();
        }
    }
}
