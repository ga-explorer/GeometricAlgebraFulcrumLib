using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D.Reflection;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D.Rotation;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D.Composers
{
    public sealed class LinFloat64RotationComposer3D :
        LinFloat64Rotation3D,
        IReadOnlyList<LinFloat64Rotation3D>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64RotationComposer3D Create()
        {
            return new LinFloat64RotationComposer3D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64RotationComposer3D CreateFromMatrix(SquareMatrix3 matrix)
        {
            var composer = new LinFloat64RotationComposer3D();

            composer.AppendRotation(matrix.GetRotationPart());

            return composer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64RotationComposer3D CreateFromRotation(LinFloat64Rotation3D rotation)
        {
            var composer = new LinFloat64RotationComposer3D();

            composer.AppendRotation(rotation);

            return composer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64RotationComposer3D CreateRandom(System.Random random)
        {
            var composer = new LinFloat64RotationComposer3D();
            
            var u = random.GetVector3D();
            var v = random.GetVector3D();
            var angle = random.GetAngle();

            composer.AppendRotation(
                LinFloat64PlanarRotation3D.CreateFromSpanningVectors(u, v, angle)
            );

            return composer;
        }
        

        private readonly List<LinFloat64Rotation3D> _rotationList
            = new List<LinFloat64Rotation3D>();


        public int Count
            => _rotationList.Count;

        public LinFloat64Rotation3D this[int index]
            => _rotationList[index];


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64RotationComposer3D()
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64RotationComposer3D(IEnumerable<LinFloat64Rotation3D> rotationList)
        {
            _rotationList = rotationList.ToList();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _rotationList.All(r => r.IsValid());
        }

        public override bool IsIdentity()
        {
            for (var basisIndex = 0; basisIndex < VSpaceDimensions; basisIndex++)
            {
                var isSameVectorBasis =
                    MapBasisVector(basisIndex).IsVectorBasis(basisIndex);

                if (!isSameVectorBasis) return false;
            }

            return true;
        }

        public override bool IsNearIdentity(double epsilon = 1E-12)
        {
            for (var basisIndex = 0; basisIndex < VSpaceDimensions; basisIndex++)
            {
                var isSameVectorBasis =
                    MapBasisVector(basisIndex).IsNearVectorBasis(basisIndex, epsilon);

                if (!isSameVectorBasis) return false;
            }

            return true;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64RotationComposer3D AppendRotation(LinFloat64Rotation3D rotation)
        {
            if (rotation.IsIdentity())
                return this;
            
            _rotationList.Add(rotation);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64RotationComposer3D AppendRotation(IFloat64Vector3D vector, IFloat64Vector3D rotatedVector, bool useShortArc = true)
        {
            return AppendRotation(
                LinFloat64PlanarRotation3D.CreateFromRotatedVector(
                    vector, 
                    rotatedVector,
                    useShortArc
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64RotationComposer3D AppendRotation(IFloat64Vector3D spanningVector1, IFloat64Vector3D spanningVector2, Float64PlanarAngle rotationAngle)
        {
            return AppendRotation(
                LinFloat64PlanarRotation3D.CreateFromSpanningVectors(
                    spanningVector1, 
                    spanningVector2, 
                    rotationAngle
                )
            );
        }
        
        public LinFloat64RotationComposer3D AppendBasisAlignmentRotation(IFloat64Vector3D vector1, IFloat64Vector3D vector2)
        {
            if (vector1.IsNearZero() && vector2.IsNearZero())
                return this;

            if (vector2.IsNearZero())
                return AppendRotation(
                    LinFloat64PlanarRotation3D.CreateFromRotatedVector(
                        vector1,
                        Float64Vector3D.E1
                    )
                );

            if (vector1.IsNearZero())
                return AppendRotation(
                    LinFloat64PlanarRotation3D.CreateFromRotatedVector(
                        vector2,
                        Float64Vector3D.E2
                    )
                );

            // TODO: This needs handling of case where vector1 = -e1
            var rotation1 = LinFloat64PlanarRotation3D.CreateFromRotatedVector(
                vector1,
                LinUnitBasisVector3D.PositiveX.ToVector3D()
            );

            vector2 = 
                rotation1
                    .MapVector(vector2)
                    .RejectOnAxis(LinUnitBasisVector3D.PositiveX);

            var rotation2 = LinFloat64PlanarRotation3D.CreateFromRotatedVector(
                vector2,
                LinUnitBasisVector3D.PositiveY.ToVector3D()
            );

            AppendRotation(rotation1);
            AppendRotation(rotation2);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64RotationComposer3D PrependRotation(LinFloat64Rotation3D rotation)
        {
            if (rotation.IsIdentity())
                return this;
            
            _rotationList.Insert(0, rotation);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64RotationComposer3D InsertRotation(int index, LinFloat64Rotation3D rotation)
        {
            if (rotation.IsIdentity())
                return this;
            
            _rotationList.Insert(index, rotation);

            return this;
        }
        
        //public double[] MapVectorInPlace(double[] vector)
        //{
        //    foreach (var rotation in _rotationList)
        //    {
        //        var u = rotation.BasisVector1;
        //        var t = rotation.BasisVector2;
        //        var v = rotation.GetRotatedBasisVector1();

        //        //var r = vector.ESp(TargetOrthogonalVector);
        //        //var s = vector.ESp(SourceVector);

        //        //return vector - (r + s) * SourceVector - (r - s) * TargetVector;

        //        var r = vector.VectorDot(t);
        //        var s = vector.VectorDot(u);
        //        var rsPlus = r + s;
        //        var rsMinus = r - s;

        //        for (var i = 0; i < VSpaceDimensions; i++)
        //            vector[i] -= rsPlus * u[i] + rsMinus * v[i];
        //    }

        //    return vector;
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector3D MapBasisVector(int basisIndex)
        {
            return basisIndex switch
            {
                0 => MapVector(Float64Vector3D.E1),
                1 => MapVector(Float64Vector3D.E2),
                2 => MapVector(Float64Vector3D.E3),
                _ => throw new IndexOutOfRangeException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector3D MapVector(IFloat64Vector3D vector)
        {
            return _rotationList.Aggregate(
                vector.ToVector3D(), 
                (rotatedVector, rotation) => 
                    rotation.MapVector(rotatedVector)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64RotationComposer3D SelfInverse()
        {
            if (_rotationList.Count == 0)
                return this;

            if (_rotationList.Count == 0)
                return this;

            var rotation = GetInverseRotation();

            _rotationList.Clear();

            return AppendRotation(rotation);
        }
        
        /// <summary>
        /// Reduce this sequence to the minimum number of pair-wise
        /// orthogonal rotations equivalent to this one
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64RotationComposer3D SelfReduce()
        {
            if (_rotationList.Count == 0)
                return this;

            var rotation = GetRotation();

            _rotationList.Clear();

            return AppendRotation(rotation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Quaternion GetQuaternion()
        {
            return GetRotation().GetQuaternion();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Rotation3D GetRotation()
        {
            return _rotationList.Count switch
            {
                0 => LinFloat64IdentityLinearMap3D.Instance,
                1 => _rotationList[0],
                _ => this.ToSquareMatrix3().GetPlanarRotation3D()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64PlanarRotation3D GetPlanarRotation()
        {
            return _rotationList.Count == 0
                ? LinFloat64PlanarRotation3D.Identity
                : this.ToSquareMatrix3().GetPlanarRotation3D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Rotation3D GetInverseRotation()
        {
            return _rotationList.Count switch
            {
                0 => LinFloat64IdentityLinearMap3D.Instance,
                1 => _rotationList[0].GetInverseRotation(),
                _ => this.ToSquareMatrix3().Transpose().GetRotationPart()
            };
        }

        public override LinFloat64HyperPlaneNormalReflectionSequence3D ToHyperPlaneReflectionSequence()
        {
            var reflection =
                LinFloat64HyperPlaneNormalReflectionSequence3D.Create();
            
            var (r1, r2) =
                GetPlanarRotation().GetHyperPlaneReflectionPair();

            reflection
                .AppendMap(r1)
                .AppendMap(r2);

            return reflection;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<LinFloat64Rotation3D> GetEnumerator()
        {
            return _rotationList.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}