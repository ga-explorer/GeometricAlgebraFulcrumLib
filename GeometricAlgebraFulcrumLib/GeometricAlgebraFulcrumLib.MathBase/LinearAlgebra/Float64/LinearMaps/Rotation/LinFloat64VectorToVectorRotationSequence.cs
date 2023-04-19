using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Arrays.Float64;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Reflection;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Rotation
{
    public sealed class LinFloat64VectorToVectorRotationSequence :
        LinFloat64RotationBase,
        IReadOnlyList<LinFloat64VectorToVectorRotation>
    {
        //public static VectorToVectorRotationSequence CreateFromRotationMatrix(Matrix<double> matrix)
        //{
        //    // Make sure it's a rotation matrix
        //    Debug.Assert(
        //        matrix.RowCount == matrix.ColumnCount &&
        //        matrix.Determinant().IsNearOne()
        //    );

        //    var rotationSequence = 
        //        new VectorToVectorRotationSequence(matrix.RowCount);

        //    var eigenPairsCount = MatrixEigenDecomposition(
        //        matrix,
        //        out var realPairs,
        //        out var imagPairs
        //    );

        //    var eigenValueList = new List<System.Numerics.Complex>(eigenPairsCount / 2);
        //    for (var i = 0; i < eigenPairsCount; i++)
        //    {
        //        var realValue = realPairs[i].Item1;
        //        var imagValue = imagPairs[i].Item1;

        //        var realVector = realPairs[i].Item2;
        //        var imagVector = imagPairs[i].Item2;

        //        //Console.WriteLine($"Real Eigen Value {i + 1}: {realValue}");
        //        //Console.WriteLine($"Imag Eigen Value {i + 1}: {imagValue}");
        //        //Console.WriteLine();

        //        //Console.WriteLine($"Real Eigen Vector {i + 1}: {realVector.CreateTuple()}");
        //        //Console.WriteLine($"Imag Eigen Vector {i + 1}: {imagVector.CreateTuple()}");
        //        //Console.WriteLine();

        //        // Ignore identity rotations
        //        if ((realValue - 1d).IsNearZero() && imagValue.IsNearZero())
        //            continue;

        //        // Ignore complex conjugate eigen values (only take first one of the pair)
        //        var conjIndex = eigenValueList.FindIndex(
        //            c => c.IsNearConjugateTo(realValue, imagValue)
        //        );

        //        if (conjIndex >= 0)
        //        {
        //            eigenValueList.RemoveAt(conjIndex);

        //            continue;
        //        }

        //        eigenValueList.Add(
        //            new System.Numerics.Complex(realValue, imagValue)
        //        );

        //        var rotation =
        //            ComplexEigenPairToSimpleVectorRotation(
        //                realValue,
        //                imagValue,
        //                realVector,
        //                imagVector
        //            );

        //        rotationSequence.AppendMap(rotation);
        //    }

        //    return rotationSequence;
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotationSequence CreateFromRotationMatrix(Matrix<double> matrix)
        {
            return matrix.GetVectorToVectorRotationSequence();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotationSequence Create(int dimensions)
        {
            return new LinFloat64VectorToVectorRotationSequence(dimensions);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotationSequence Create(LinFloat64VectorToVectorRotationBase rotation)
        {
            var rotationSequence =
                new LinFloat64VectorToVectorRotationSequence(rotation.VSpaceDimensions);

            rotationSequence.AppendMap(rotation);

            return rotationSequence;
        }

        public static LinFloat64VectorToVectorRotationSequence CreateRandom(System.Random random, int dimensions, int count)
        {
            var rotationSequence = new LinFloat64VectorToVectorRotationSequence(dimensions);

            for (var i = 0; i < count; i++)
            {
                var u = random.GetFloat64Tuple(dimensions).CreateUnitLinVector();
                var v = random.GetFloat64Tuple(dimensions).CreateUnitLinVector();
                var angle = random.GetAngle();

                rotationSequence.AppendMap(
                    LinFloat64VectorToVectorRotation.Create(u, v, angle)
                );
            }

            return rotationSequence;
        }

        public static LinFloat64VectorToVectorRotationSequence CreateRandomOrthogonal(System.Random random, int dimensions, int count)
        {
            if (count > dimensions / 2)
                throw new ArgumentOutOfRangeException(nameof(count));

            var rotationSequence = new LinFloat64VectorToVectorRotationSequence(dimensions);

            var vectorList =
                random.GetOrthonormalVectors(dimensions, 2 * count);

            for (var i = 0; i < count; i++)
            {
                var u = vectorList[2 * i].CreateLinVector();
                var v = vectorList[2 * i + 1].CreateLinVector();
                var angle = random.GetAngle();

                rotationSequence.AppendMap(
                    LinFloat64VectorToVectorRotation.Create(u, v, angle)
                );
            }

            return rotationSequence;
        }


        private readonly List<LinFloat64VectorToVectorRotation> _mapList
            = new List<LinFloat64VectorToVectorRotation>();


        public override int VSpaceDimensions { get; }

        public int Count
            => _mapList.Count;

        public LinFloat64VectorToVectorRotation this[int index] 
            => _mapList[index];
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64VectorToVectorRotationSequence(int dimensions)
        {
            VSpaceDimensions = dimensions;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64VectorToVectorRotationSequence(int dimensions, List<LinFloat64VectorToVectorRotation> rotationList)
        {
            VSpaceDimensions = dimensions;
            _mapList = rotationList;
        }
    

        //private static Triplet<double[]> GetRotationVectorsTriplet(double[] sourceVector, double[] targetVector)
        //{
        //    Debug.Assert(
        //        sourceVector.Length == targetVector.Length &&
        //        sourceVector.GetVectorNormSquared().IsNearOne() &&
        //        targetVector.GetVectorNormSquared().IsNearOne()
        //    );

        //    var angleCos = targetVector.VectorDot(sourceVector).Clamp(-1d, 1d);

        //    Debug.Assert(
        //        !angleCos.IsNearMinusOne()
        //    );

        //    var targetOrthogonalVector = 
        //        targetVector
        //            .VectorSubtract(sourceVector.VectorTimes(angleCos))
        //            .VectorDivideInPlace(1d + angleCos);

        //    return new Triplet<double[]>(
        //        sourceVector,
        //        targetOrthogonalVector,
        //        targetVector
        //    );
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorToVectorRotationSequence AppendMap(LinFloat64Vector sourceVector, LinFloat64Vector targetVector)
        {
            _mapList.Add(
                LinFloat64VectorToVectorRotation.Create(sourceVector, targetVector)
            );

            return this;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorToVectorRotationSequence AppendMap(LinFloat64Vector sourceVector, LinFloat64Vector targetVector, Float64PlanarAngle angle)
        {
            _mapList.Add(
                LinFloat64VectorToVectorRotation.Create(sourceVector, targetVector, angle)
            );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorToVectorRotationSequence AppendMap(LinFloat64VectorToVectorRotationBase rotation)
        {
            if (rotation.VSpaceDimensions != VSpaceDimensions)
                throw new ArgumentException();

            var r2 = 
                rotation as LinFloat64VectorToVectorRotation 
                ?? LinFloat64VectorToVectorRotation.Create(rotation.SourceVector, rotation.TargetVector);

            _mapList.Add(r2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorToVectorRotationSequence PrependMap(LinFloat64Vector sourceVector, LinFloat64Vector targetVector)
        {
            _mapList.Insert(
                0, 
                LinFloat64VectorToVectorRotation.Create(sourceVector, targetVector)
            );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorToVectorRotationSequence PrependMap(LinFloat64VectorToVectorRotationBase rotation)
        {
            if (rotation.VSpaceDimensions != VSpaceDimensions)
                throw new ArgumentException();
        
            var r2 = 
                rotation as LinFloat64VectorToVectorRotation 
                ?? LinFloat64VectorToVectorRotation.Create(rotation.SourceVector, rotation.TargetVector);

            _mapList.Insert(0, r2);

            return this;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorToVectorRotationSequence InsertMap(int index, LinFloat64Vector sourceVector, LinFloat64Vector targetVector)
        {
            _mapList.Insert(
                index, 
                LinFloat64VectorToVectorRotation.Create(sourceVector, targetVector)
            );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorToVectorRotationSequence InsertMap(int index, LinFloat64VectorToVectorRotationBase rotation)
        {
            if (rotation.VSpaceDimensions != VSpaceDimensions)
                throw new ArgumentException();
        
            var r2 = 
                rotation as LinFloat64VectorToVectorRotation 
                ?? LinFloat64VectorToVectorRotation.Create(rotation.SourceVector, rotation.TargetVector);

            _mapList.Insert(index, r2);

            return this;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorToVectorRotationSequence AppendMaps(IEnumerable<LinFloat64VectorToVectorRotation> rotationList)
        {
            _mapList.AddRange(rotationList);

            return this;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorToVectorRotationSequence PrependMaps(IEnumerable<LinFloat64VectorToVectorRotation> rotationList)
        {
            _mapList.InsertRange(0, rotationList);

            return this;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorToVectorRotationSequence InsertMaps(int index, IEnumerable<LinFloat64VectorToVectorRotation> rotationList)
        {
            _mapList.InsertRange(index, rotationList);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _mapList.All(r => r.IsValid());
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

        /// <summary>
        /// Test if all rotation planes in this sequence are nearly pair-wise orthogonal
        /// </summary>
        /// <returns></returns>
        public bool IsNearOrthogonalRotationsSequence(double epsilon = 1e-12)
        {
            if (_mapList.Count > VSpaceDimensions / 2)
                return false;

            for (var i = 0; i < _mapList.Count; i++)
            {
                var u1 = _mapList[i].SourceVector;
                var v1 = _mapList[i].TargetVector;

                for (var j = i + 1; j < _mapList.Count; j++)
                {
                    var u2 = _mapList[j].SourceVector;
                    var v2 = _mapList[j].TargetVector;

                    if (!u1.IsNearOrthogonalTo(u2, epsilon)) return false;
                    if (!u1.IsNearOrthogonalTo(v2, epsilon)) return false;
                    if (!v1.IsNearOrthogonalTo(u2, epsilon)) return false;
                    if (!v1.IsNearOrthogonalTo(v2, epsilon)) return false;
                }
            }

            return true;
        }


        public double[] MapVectorInPlace(double[] vector)
        {
            foreach (var rotation in _mapList)
            {
                var u = rotation.SourceVector;
                var t = rotation.TargetOrthogonalVector;
                var v = rotation.TargetVector;

                //var r = vector.VectorDot(TargetOrthogonalVector);
                //var s = vector.VectorDot(SourceVector);

                //return vector - (r + s) * SourceVector - (r - s) * TargetVector;

                var r = vector.VectorDot(t);
                var s = vector.VectorDot(u);
                var rsPlus = r + s;
                var rsMinus = r - s;

                for (var i = 0; i < VSpaceDimensions; i++)
                    vector[i] -= rsPlus * u[i] + rsMinus * v[i];
            }

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Vector MapBasisVector(int basisIndex)
        {
            Debug.Assert(
                basisIndex >= 0
            );

            if (_mapList.Count == 0)
                return basisIndex.CreateLinVector();
            
            var composer = LinFloat64VectorComposer
                .Create()
                .SetTerm(basisIndex, 1d);

            foreach (var rotation in _mapList)
            {
                var u = rotation.SourceVector;
                var t = rotation.TargetOrthogonalVector;
                var v = rotation.TargetVector;

                //var r = vector.VectorDot(TargetOrthogonalVector);
                //var s = vector.VectorDot(SourceVector);

                //return vector - (r + s) * SourceVector - (r - s) * TargetVector;

                var r = composer.VectorDot(t);
                var s = composer.VectorDot(u);
                var rsPlus = r + s;
                var rsMinus = r - s;

                composer
                    .AddVector(u, -rsPlus)
                    .AddVector(v, -rsMinus);
            }

            return composer.GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Vector MapVector(LinFloat64Vector vector)
        {
            if (_mapList.Count == 0)
                return vector;

            var composer = LinFloat64VectorComposer
                .Create()
                .SetVector(vector);

            foreach (var rotation in _mapList)
            {
                var u = rotation.SourceVector;
                var t = rotation.TargetOrthogonalVector;
                var v = rotation.TargetVector;

                //var r = vector.VectorDot(TargetOrthogonalVector);
                //var s = vector.VectorDot(SourceVector);

                //return vector - (r + s) * SourceVector - (r - s) * TargetVector;

                var r = composer.VectorDot(t);
                var s = composer.VectorDot(u);
                var rsPlus = r + s;
                var rsMinus = r - s;

                composer
                    .AddVector(u, -rsPlus)
                    .AddVector(v, -rsMinus);
            }

            return composer.GetVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorToVectorRotationSequence GetVectorToVectorRotationSequenceInverse()
        {
            if (_mapList.Count == 0)
                return this;

            var rotationList =
                ((IEnumerable<LinFloat64VectorToVectorRotation>)_mapList)
                .Reverse()
                .Select(r => r.GetVectorToVectorRotationInverse())
                .ToList();

            return new LinFloat64VectorToVectorRotationSequence(VSpaceDimensions, rotationList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64RotationBase GetVectorRotationInverse()
        {
            return GetVectorToVectorRotationSequenceInverse();
        }

        /// <summary>
        /// Create a new sequence containing the minimum number of pair-wise
        /// orthogonal rotations equivalent to this one
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorToVectorRotationSequence ReduceSequence()
        {
            return ToMatrix(VSpaceDimensions, VSpaceDimensions).GetVectorToVectorRotationSequence();
        }

        public override LinFloat64HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
        {
            var reflection = 
                LinFloat64HyperPlaneNormalReflectionSequence.Create(VSpaceDimensions);

            foreach (var rotation in _mapList)
            {
                var (r1, r2) = 
                    rotation.GetHyperPlaneReflectionPair();

                reflection
                    .AppendMap(r1)
                    .AppendMap(r2);
            }

            return reflection;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64VectorToVectorRotationSequence ToVectorToVectorRotationSequence()
        {
            return this;
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public override SimpleRotationSequence ToSimpleVectorRotationSequence()
        //{
        //    var rotation = SimpleRotationSequence.Create(Dimensions);

        //    foreach (var rotationVectors in _rotationList)
        //        rotation.AppendRotation(rotationVectors);

        //    return rotation;
        //}


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public Matrix<double> GetRotationMatrix()
        //{
        //    var columnList =
        //        Dimensions
        //            .GetRange()
        //            .Select(i => MapVectorBasis(i).MathNetVector);

        //    return Matrix<double>
        //        .Build
        //        .SparseOfColumnVectors(columnList);
        //}


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<LinFloat64VectorToVectorRotation> GetEnumerator()
        {
            return _mapList.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}