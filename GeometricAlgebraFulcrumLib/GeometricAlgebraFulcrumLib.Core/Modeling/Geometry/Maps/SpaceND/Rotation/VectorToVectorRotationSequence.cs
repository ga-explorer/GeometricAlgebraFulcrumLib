//using System.Collections;
//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Arrays.Float64;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Maps.SpaceND.Reflection;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Matrices;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Tuples;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Tuples.Mutable;
//using MathNet.Numerics.LinearAlgebra;

//namespace GeometricAlgebraFulcrumLib.Algebra.BasicMath.Maps.SpaceND.Rotation
//{
//    public sealed class VectorToVectorRotationSequence :
//        RotationLinearMap,
//        IReadOnlyList<VectorToVectorRotation>
//    {
//        //public static VectorToVectorRotationSequence CreateFromRotationMatrix(Matrix<double> matrix)
//        //{
//        //    // Make sure it's a rotation matrix
//        //    Debug.Assert(
//        //        matrix.RowCount == matrix.ColumnCount &&
//        //        matrix.Determinant().IsNearOne()
//        //    );

//        //    var rotationSequence = 
//        //        new VectorToVectorRotationSequence(matrix.RowCount);

//        //    var eigenPairsCount = MatrixEigenDecomposition(
//        //        matrix,
//        //        out var realPairs,
//        //        out var imagPairs
//        //    );

//        //    var eigenValueList = new List<System.Numerics.Complex>(eigenPairsCount / 2);
//        //    for (var i = 0; i < eigenPairsCount; i++)
//        //    {
//        //        var realValue = realPairs[i].Item1;
//        //        var imagValue = imagPairs[i].Item1;

//        //        var realVector = realPairs[i].Item2;
//        //        var imagVector = imagPairs[i].Item2;

//        //        //Console.WriteLine($"Real Eigen Value {i + 1}: {realValue}");
//        //        //Console.WriteLine($"Imag Eigen Value {i + 1}: {imagValue}");
//        //        //Console.WriteLine();

//        //        //Console.WriteLine($"Real Eigen Vector {i + 1}: {realVector.CreateTuple()}");
//        //        //Console.WriteLine($"Imag Eigen Vector {i + 1}: {imagVector.CreateTuple()}");
//        //        //Console.WriteLine();

//        //        // Ignore identity rotations
//        //        if ((realValue - 1d).IsNearZero() && imagValue.IsNearZero())
//        //            continue;

//        //        // Ignore complex conjugate eigen values (only take first one of the pair)
//        //        var conjIndex = eigenValueList.FindIndex(
//        //            c => c.IsNearConjugateTo(realValue, imagValue)
//        //        );

//        //        if (conjIndex >= 0)
//        //        {
//        //            eigenValueList.RemoveAt(conjIndex);

//        //            continue;
//        //        }

//        //        eigenValueList.Add(
//        //            new System.Numerics.Complex(realValue, imagValue)
//        //        );

//        //        var rotation =
//        //            ComplexEigenPairToSimpleVectorRotation(
//        //                realValue,
//        //                imagValue,
//        //                realVector,
//        //                imagVector
//        //            );

//        //        rotationSequence.AppendMap(rotation);
//        //    }

//        //    return rotationSequence;
//        //}

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static VectorToVectorRotationSequence CreateFromRotationMatrix(Matrix<double> matrix)
//        {
//            return matrix.GetVectorToVectorRotationSequence();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static VectorToVectorRotationSequence Create(int dimensions)
//        {
//            return new VectorToVectorRotationSequence(dimensions);
//        }
    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static VectorToVectorRotationSequence Create(VectorToVectorRotationLinearMap rotation)
//        {
//            var rotationSequence =
//                new VectorToVectorRotationSequence(rotation.VSpaceDimensions);

//            rotationSequence.AppendMap(rotation);

//            return rotationSequence;
//        }

//        public static VectorToVectorRotationSequence CreateRandom(System.Random random, int dimensions, int count)
//        {
//            var rotationSequence = new VectorToVectorRotationSequence(dimensions);

//            for (var i = 0; i < count; i++)
//            {
//                var u = random.GetFloat64Tuple(dimensions).InPlaceNormalize();
//                var v = random.GetFloat64Tuple(dimensions).InPlaceNormalize();
//                var angle = random.GetAngle();

//                rotationSequence.AppendMap(
//                    VectorToVectorRotation.Create(u, v, angle)
//                );
//            }

//            return rotationSequence;
//        }

//        public static VectorToVectorRotationSequence CreateRandomOrthogonal(System.Random random, int dimensions, int count)
//        {
//            if (count > dimensions / 2)
//                throw new ArgumentOutOfRangeException(nameof(count));

//            var rotationSequence = new VectorToVectorRotationSequence(dimensions);

//            var vectorList =
//                random.GetOrthonormalVectors(dimensions, 2 * count);

//            for (var i = 0; i < count; i++)
//            {
//                var u = vectorList[2 * i].ToTuple();
//                var v = vectorList[2 * i + 1].ToTuple();
//                var angle = random.GetAngle();

//                rotationSequence.AppendMap(
//                    VectorToVectorRotation.Create(u, v, angle)
//                );
//            }

//            return rotationSequence;
//        }


//        private readonly List<VectorToVectorRotation> _mapList
//            = new List<VectorToVectorRotation>();


//        public override int VSpaceDimensions { get; }

//        public int Count
//            => _mapList.Count;

//        public VectorToVectorRotation this[int index] 
//            => _mapList[index];
    

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private VectorToVectorRotationSequence(int dimensions)
//        {
//            VSpaceDimensions = dimensions;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private VectorToVectorRotationSequence(int dimensions, List<VectorToVectorRotation> rotationList)
//        {
//            VSpaceDimensions = dimensions;
//            _mapList = rotationList;
//        }
    

//        //private static Triplet<double[]> GetRotationVectorsTriplet(double[] sourceVector, double[] targetVector)
//        //{
//        //    Debug.Assert(
//        //        sourceVector.Length == targetVector.Length &&
//        //        sourceVector.GetVectorNormSquared().IsNearOne() &&
//        //        targetVector.GetVectorNormSquared().IsNearOne()
//        //    );

//        //    var angleCos = targetVector.VectorDot(sourceVector).Clamp(-1d, 1d);

//        //    Debug.Assert(
//        //        !angleCos.IsNearMinusOne()
//        //    );

//        //    var targetOrthogonalVector = 
//        //        targetVector
//        //            .VectorSubtract(sourceVector.VectorTimes(angleCos))
//        //            .VectorDivideInPlace(1d + angleCos);

//        //    return new Triplet<double[]>(
//        //        sourceVector,
//        //        targetOrthogonalVector,
//        //        targetVector
//        //    );
//        //}

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public VectorToVectorRotationSequence AppendMap(Float64Tuple sourceVector, Float64Tuple targetVector)
//        {
//            if (sourceVector.Dimensions != VSpaceDimensions || targetVector.Dimensions != VSpaceDimensions)
//                throw new ArgumentException();

//            _mapList.Add(
//                VectorToVectorRotation.Create(sourceVector, targetVector)
//            );

//            return this;
//        }
    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public VectorToVectorRotationSequence AppendMap(Float64Tuple sourceVector, Float64Tuple targetVector, Float64PlanarAngle angle)
//        {
//            if (sourceVector.Dimensions != VSpaceDimensions || targetVector.Dimensions != VSpaceDimensions)
//                throw new ArgumentException();

//            _mapList.Add(
//                VectorToVectorRotation.Create(sourceVector, targetVector, angle)
//            );

//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public VectorToVectorRotationSequence AppendMap(VectorToVectorRotationLinearMap rotation)
//        {
//            if (rotation.VSpaceDimensions != VSpaceDimensions)
//                throw new ArgumentException();

//            var r2 = 
//                rotation as VectorToVectorRotation 
//                ?? VectorToVectorRotation.Create(rotation.SourceVector, rotation.TargetVector);

//            _mapList.Add(r2);

//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public VectorToVectorRotationSequence PrependMap(Float64Tuple sourceVector, Float64Tuple targetVector)
//        {
//            if (sourceVector.Dimensions != VSpaceDimensions || targetVector.Dimensions != VSpaceDimensions)
//                throw new ArgumentException();

//            _mapList.Insert(
//                0, 
//                VectorToVectorRotation.Create(sourceVector, targetVector)
//            );

//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public VectorToVectorRotationSequence PrependMap(VectorToVectorRotationLinearMap rotation)
//        {
//            if (rotation.VSpaceDimensions != VSpaceDimensions)
//                throw new ArgumentException();
        
//            var r2 = 
//                rotation as VectorToVectorRotation 
//                ?? VectorToVectorRotation.Create(rotation.SourceVector, rotation.TargetVector);

//            _mapList.Insert(0, r2);

//            return this;
//        }
    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public VectorToVectorRotationSequence InsertMap(int index, Float64Tuple sourceVector, Float64Tuple targetVector)
//        {
//            if (sourceVector.Dimensions != VSpaceDimensions || targetVector.Dimensions != VSpaceDimensions)
//                throw new ArgumentException();

//            _mapList.Insert(
//                index, 
//                VectorToVectorRotation.Create(sourceVector, targetVector)
//            );

//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public VectorToVectorRotationSequence InsertMap(int index, VectorToVectorRotationLinearMap rotation)
//        {
//            if (rotation.VSpaceDimensions != VSpaceDimensions)
//                throw new ArgumentException();
        
//            var r2 = 
//                rotation as VectorToVectorRotation 
//                ?? VectorToVectorRotation.Create(rotation.SourceVector, rotation.TargetVector);

//            _mapList.Insert(index, r2);

//            return this;
//        }
    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public VectorToVectorRotationSequence AppendMaps(IEnumerable<VectorToVectorRotation> rotationList)
//        {
//            _mapList.AddRange(rotationList);

//            return this;
//        }
    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public VectorToVectorRotationSequence PrependMaps(IEnumerable<VectorToVectorRotation> rotationList)
//        {
//            _mapList.InsertRange(0, rotationList);

//            return this;
//        }
    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public VectorToVectorRotationSequence InsertMaps(int index, IEnumerable<VectorToVectorRotation> rotationList)
//        {
//            _mapList.InsertRange(index, rotationList);

//            return this;
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override bool IsValid()
//        {
//            return _mapList.All(r => r.IsValid());
//        }

//        public override bool IsIdentity()
//        {
//            for (var basisIndex = 0; basisIndex < VSpaceDimensions; basisIndex++)
//            {
//                var isSameVectorBasis = 
//                    MapBasisVector(basisIndex).IsVectorBasis(basisIndex);

//                if (!isSameVectorBasis) return false;
//            }
        
//            return true;
//        }

//        public override bool IsNearIdentity(double epsilon = 1E-12)
//        {
//            for (var basisIndex = 0; basisIndex < VSpaceDimensions; basisIndex++)
//            {
//                var isSameVectorBasis = 
//                    MapBasisVector(basisIndex).IsNearVectorBasis(basisIndex, epsilon);

//                if (!isSameVectorBasis) return false;
//            }
        
//            return true;
//        }

//        /// <summary>
//        /// Test if all rotation planes in this sequence are nearly pair-wise orthogonal
//        /// </summary>
//        /// <returns></returns>
//        public bool IsNearOrthogonalRotationsSequence(double epsilon = 1e-12)
//        {
//            if (_mapList.Count > VSpaceDimensions / 2)
//                return false;

//            for (var i = 0; i < _mapList.Count; i++)
//            {
//                var u1 = _mapList[i].SourceVector;
//                var v1 = _mapList[i].TargetVector;

//                for (var j = i + 1; j < _mapList.Count; j++)
//                {
//                    var u2 = _mapList[j].SourceVector;
//                    var v2 = _mapList[j].TargetVector;

//                    if (!u1.IsNearOrthogonalTo(u2, epsilon)) return false;
//                    if (!u1.IsNearOrthogonalTo(v2, epsilon)) return false;
//                    if (!v1.IsNearOrthogonalTo(u2, epsilon)) return false;
//                    if (!v1.IsNearOrthogonalTo(v2, epsilon)) return false;
//                }
//            }

//            return true;
//        }


//        public double[] MapVectorInPlace(double[] vector)
//        {
//            foreach (var rotation in _mapList)
//            {
//                var u = rotation.SourceVector.ScalarArray;
//                var t = rotation.TargetOrthogonalVector.ScalarArray;
//                var v = rotation.TargetVector.ScalarArray;

//                //var r = vector.VectorDot(TargetOrthogonalVector);
//                //var s = vector.VectorDot(SourceVector);

//                //return vector - (r + s) * SourceVector - (r - s) * TargetVector;

//                var (r, s) = vector.VectorDot(t, u);
//                var rsPlus = r + s;
//                var rsMinus = r - s;

//                for (var i = 0; i < VSpaceDimensions; i++)
//                    vector[i] -= rsPlus * u[i] + rsMinus * v[i];
//            }

//            return vector;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override Float64Tuple MapBasisVector(int basisIndex)
//        {
//            Debug.Assert(
//                basisIndex >= 0 && basisIndex < VSpaceDimensions
//            );

//            if (_mapList.Count == 0)
//                return Float64Tuple.CreateBasis(VSpaceDimensions, basisIndex);

//            var x = new double[VSpaceDimensions];
//            x[basisIndex] = 1d;

//            return Float64Tuple.Create(
//                MapVectorInPlace(x)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override Float64Tuple MapVector(Float64Tuple vector)
//        {
//            Debug.Assert(
//                vector.Dimensions == VSpaceDimensions
//            );

//            if (_mapList.Count == 0)
//                return vector;

//            var x = vector.GetScalarArrayCopy();

//            return Float64Tuple.Create(
//                MapVectorInPlace(x)
//            );
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public VectorToVectorRotationSequence GetVectorToVectorRotationSequenceInverse()
//        {
//            if (_mapList.Count == 0)
//                return this;

//            var rotationList =
//                ((IEnumerable<VectorToVectorRotation>)_mapList)
//                .Reverse()
//                .Select(r => r.GetVectorToVectorRotationInverse())
//                .ToList();

//            return new VectorToVectorRotationSequence(VSpaceDimensions, rotationList);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override RotationLinearMap GetVectorRotationInverse()
//        {
//            return GetVectorToVectorRotationSequenceInverse();
//        }

//        /// <summary>
//        /// Create a new sequence containing the minimum number of pair-wise
//        /// orthogonal rotations equivalent to this one
//        /// </summary>
//        /// <returns></returns>
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public VectorToVectorRotationSequence ReduceSequence()
//        {
//            return ToMatrix().GetVectorToVectorRotationSequence();
//        }

//        public override HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
//        {
//            var reflection = 
//                HyperPlaneNormalReflectionSequence.Create(VSpaceDimensions);

//            foreach (var rotation in _mapList)
//            {
//                var (r1, r2) = 
//                    rotation.GetHyperPlaneReflectionPair();

//                reflection
//                    .AppendMap(r1)
//                    .AppendMap(r2);
//            }

//            return reflection;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override VectorToVectorRotationSequence ToVectorToVectorRotationSequence()
//        {
//            return this;
//        }

//        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//        //public override SimpleRotationSequence ToSimpleVectorRotationSequence()
//        //{
//        //    var rotation = SimpleRotationSequence.Create(Dimensions);

//        //    foreach (var rotationVectors in _rotationList)
//        //        rotation.AppendRotation(rotationVectors);

//        //    return rotation;
//        //}


//        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//        //public Matrix<double> GetRotationMatrix()
//        //{
//        //    var columnList =
//        //        Dimensions
//        //            .GetRange()
//        //            .Select(i => MapVectorBasis(i).MathNetVector);

//        //    return Matrix<double>
//        //        .Build
//        //        .SparseOfColumnVectors(columnList);
//        //}


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public IEnumerator<VectorToVectorRotation> GetEnumerator()
//        {
//            return _mapList.GetEnumerator();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }
//    }
//}