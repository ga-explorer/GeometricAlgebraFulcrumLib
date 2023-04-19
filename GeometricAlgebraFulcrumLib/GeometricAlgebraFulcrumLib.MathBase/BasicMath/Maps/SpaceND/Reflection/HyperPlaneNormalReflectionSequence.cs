//using System.Collections;
//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using DataStructuresLib.BitManipulation;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Arrays.Float64;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Mutable;
//using MathNet.Numerics.LinearAlgebra;

//namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.SpaceND.Reflection
//{
//    public sealed class HyperPlaneNormalReflectionSequence :
//        ReflectionLinearMap,
//        IReadOnlyList<HyperPlaneNormalReflection>
//    {
//        //public static int MatrixEigenDecomposition(Matrix<double> matrix, out Tuple<double, double[]>[] realPairs, out Tuple<double, double[]>[] imagPairs)
//        //{
//        //    var sysExpr = matrix.ToComplex().Evd();

//        //    var count = sysExpr.EigenValues.Count;

//        //    realPairs = new Tuple<double, double[]>[count];
//        //    imagPairs = new Tuple<double, double[]>[count];

//        //    //Console.WriteLine("Eigen Vectors Matrix");
//        //    //Console.WriteLine(
//        //    //    GeoTextComposerFloat64.DefaultComposer.GetArrayText(
//        //    //        sysExpr.EigenVectors.Real().ToArray()
//        //    //    )
//        //    //);
//        //    //Console.WriteLine();

//        //    //Console.WriteLine(
//        //    //    GeoTextComposerFloat64.DefaultComposer.GetArrayText(
//        //    //        sysExpr.EigenVectors.Imaginary().ToArray()
//        //    //    )
//        //    //);
//        //    //Console.WriteLine();

//        //    for (var j = 0; j < count; j++)
//        //    {
//        //        var complexEigenValue = sysExpr.EigenValues[j];
//        //        var complexEigenVector = sysExpr.EigenVectors.Column(j);

//        //        realPairs[j] = new Tuple<double, double[]>(
//        //            complexEigenValue.Real,
//        //            complexEigenVector.Real().ToArray()
//        //        );

//        //        imagPairs[j] = new Tuple<double, double[]>(
//        //            complexEigenValue.Imaginary,
//        //            complexEigenVector.Imaginary().ToArray()
//        //        );
//        //    }

//        //    return count;
//        //}

//        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//        //public static Pair<HyperPlaneNormalReflection> ComplexEigenPairToHyperPlaneReflections(double realValue, double imagValue, double[] realVector, double[] imagVector)
//        //{
//        //    return VectorToVectorRotationSequence
//        //        .ComplexEigenPairToSimpleVectorRotation(
//        //            realValue, 
//        //            imagValue, 
//        //            realVector, 
//        //            imagVector
//        //        ).GetHyperPlaneReflectionPair();
//        //}

//        //public static HyperPlaneNormalReflectionSequence CreateFromReflectionMatrix(Matrix<double> matrix)
//        //{
//        //    // Make sure it's a reflection matrix
//        //    Debug.Assert(
//        //        matrix.RowCount == matrix.ColumnCount &&
//        //        matrix.Determinant().Abs().IsNearOne()
//        //    );

//        //    var reflectionSequence = 
//        //        new HyperPlaneNormalReflectionSequence(matrix.RowCount);

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

//        //        Console.WriteLine($"Real Eigen Value {i + 1}: {realValue}");
//        //        Console.WriteLine($"Imag Eigen Value {i + 1}: {imagValue}");
//        //        Console.WriteLine();

//        //        Console.WriteLine($"Real Eigen Vector {i + 1}: {realVector.CreateTuple()}");
//        //        Console.WriteLine($"Imag Eigen Vector {i + 1}: {imagVector.CreateTuple()}");
//        //        Console.WriteLine();

//        //        if (imagValue.IsNearZero())
//        //        {
//        //            // Ignore identity reflections
//        //            if (realValue.IsNearOne())
//        //                continue;

//        //            var v1 = realVector.VectorDivide(realVector.GetVectorNorm()).CreateTuple();
//        //            var v2 = imagVector.VectorDivide(imagVector.GetVectorNorm()).CreateTuple();

//        //            // Make sure both eigen vector parts encode the same 1-dimensional subspace
//        //            Debug.Assert(
//        //                v1.VectorDot(v2).Abs().IsNearOne()
//        //            );

//        //            Console.WriteLine("Hyper Plane Reflection: ");
//        //            Console.WriteLine($"   Reflection Unit Normal: {v1}");
//        //            Console.WriteLine();

//        //            reflectionSequence.AppendMap(v1);

//        //            continue;
//        //        }

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

//        //        var (r1, r2) =
//        //            ComplexEigenPairToHyperPlaneReflections(
//        //                realValue,
//        //                imagValue,
//        //                realVector,
//        //                imagVector
//        //            );

//        //        reflectionSequence
//        //            .AppendMap(r1)
//        //            .AppendMap(r2);
//        //    }

//        //    return reflectionSequence;
//        //}

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static HyperPlaneNormalReflectionSequence CreateFromReflectionMatrix(Matrix<double> matrix)
//        {
//            return matrix.GetHyperPlaneNormalReflectionSequence();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static HyperPlaneNormalReflectionSequence Create(int dimensions)
//        {
//            return new HyperPlaneNormalReflectionSequence(dimensions);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static HyperPlaneNormalReflectionSequence Create(IHyperPlaneNormalReflectionLinearMap reflection)
//        {
//            var reflectionSequence =
//                new HyperPlaneNormalReflectionSequence(reflection.VSpaceDimensions);

//            reflectionSequence.AppendMap(reflection);

//            return reflectionSequence;
//        }
    
//        public static HyperPlaneNormalReflectionSequence CreateRandom(System.Random random, int dimensions, int count)
//        {
//            var rotationSequence = new HyperPlaneNormalReflectionSequence(dimensions);
        
//            for (var i = 0; i < count; i++)
//            {
//                var u = random.GetFloat64Tuple(dimensions).InPlaceNormalize();

//                rotationSequence.AppendMap(
//                    HyperPlaneNormalReflection.Create(u)
//                );
//            }

//            return rotationSequence;
//        }

//        public static HyperPlaneNormalReflectionSequence CreateRandomOrthogonal(System.Random random, int dimensions, int count)
//        {
//            if (count > dimensions)
//                throw new ArgumentOutOfRangeException(nameof(count));

//            var rotationSequence = new HyperPlaneNormalReflectionSequence(dimensions);

//            var vectorList =
//                random.GetOrthonormalVectors(dimensions, count);

//            for (var i = 0; i < count; i++)
//            {
//                var u = vectorList[i].ToTuple();

//                rotationSequence.AppendMap(
//                    HyperPlaneNormalReflection.Create(u)
//                );
//            }

//            return rotationSequence;
//        }


//        private readonly List<HyperPlaneNormalReflection> _mapList
//            = new List<HyperPlaneNormalReflection>();

    
//        public int Count
//            => _mapList.Count;

//        public HyperPlaneNormalReflection this[int index] 
//            => _mapList[index];
    
//        public override int VSpaceDimensions { get; }

//        public override bool SwapsHandedness 
//            => _mapList.Count.IsOdd();


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private HyperPlaneNormalReflectionSequence(int dimensions)
//        {
//            if (dimensions < 1)
//                throw new ArgumentOutOfRangeException(nameof(dimensions));

//            VSpaceDimensions = dimensions;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private HyperPlaneNormalReflectionSequence(int dimensions, List<HyperPlaneNormalReflection> reflectionNormalList)
//        {
//            if (dimensions < 1)
//                throw new ArgumentOutOfRangeException(nameof(dimensions));

//            VSpaceDimensions = dimensions;
//            _mapList = reflectionNormalList;

//            Debug.Assert(IsValid());
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public HyperPlaneNormalReflectionSequence AppendMap(Float64Tuple reflectionNormal)
//        {
//            if (reflectionNormal.Dimensions != VSpaceDimensions)
//                throw new ArgumentException();

//            _mapList.Add(
//                HyperPlaneNormalReflection.Create(reflectionNormal)
//            );

//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public HyperPlaneNormalReflectionSequence AppendMap(HyperPlaneNormalReflection reflection)
//        {
//            if (reflection.VSpaceDimensions != VSpaceDimensions)
//                throw new ArgumentException();

//            _mapList.Add(reflection);

//            return this;
//        }
    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public HyperPlaneNormalReflectionSequence AppendMap(IHyperPlaneNormalReflectionLinearMap reflection)
//        {
//            if (reflection.VSpaceDimensions != VSpaceDimensions)
//                throw new ArgumentException();

//            var r2 =
//                reflection as HyperPlaneNormalReflection 
//                ?? reflection.ToHyperPlaneNormalReflection();

//            _mapList.Add(r2);

//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public HyperPlaneNormalReflectionSequence PrependMap(Float64Tuple reflectionNormal)
//        {
//            if (reflectionNormal.Dimensions != VSpaceDimensions)
//                throw new ArgumentException();

//            _mapList.Insert(
//                0, 
//                HyperPlaneNormalReflection.Create(reflectionNormal)
//            );

//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public HyperPlaneNormalReflectionSequence PrependMap(HyperPlaneNormalReflection reflection)
//        {
//            if (reflection.VSpaceDimensions != VSpaceDimensions)
//                throw new ArgumentException();

//            _mapList.Insert(
//                0, 
//                reflection
//            );

//            return this;
//        }
    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public HyperPlaneNormalReflectionSequence InsertMap(int index, Float64Tuple reflectionNormal)
//        {
//            if (reflectionNormal.Dimensions != VSpaceDimensions)
//                throw new ArgumentException();

//            _mapList.Insert(
//                index, 
//                HyperPlaneNormalReflection.Create(reflectionNormal)
//            );

//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public HyperPlaneNormalReflectionSequence InsertMap(int index, HyperPlaneNormalReflection reflection)
//        {
//            if (reflection.VSpaceDimensions != VSpaceDimensions)
//                throw new ArgumentException();

//            _mapList.Insert(
//                index, 
//                reflection
//            );

//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public HyperPlaneNormalReflectionSequence AppendMaps(IEnumerable<HyperPlaneNormalReflection> reflectionList)
//        {
//            _mapList.AddRange(reflectionList);

//            return this;
//        }
    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public HyperPlaneNormalReflectionSequence PrependMaps(IEnumerable<HyperPlaneNormalReflection> reflectionList)
//        {
//            _mapList.InsertRange(0, reflectionList);

//            return this;
//        }
    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public HyperPlaneNormalReflectionSequence InsertMaps(int index, IEnumerable<HyperPlaneNormalReflection> reflectionList)
//        {
//            _mapList.InsertRange(index, reflectionList);

//            return this;
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override bool IsValid()
//        {
//            return _mapList.All(a => a.IsValid());
//        }

//        public override bool IsIdentity()
//        {
//            if (_mapList.Count == 0)
//                return true;

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
//        /// Test if all reflection normals in this sequence are nearly pair-wise orthogonal
//        /// </summary>
//        /// <returns></returns>
//        public bool IsNearOrthogonalReflectionsSequence(double epsilon = 1e-12)
//        {
//            if (_mapList.Count > VSpaceDimensions)
//                return false;

//            for (var i = 0; i < _mapList.Count; i++)
//            {
//                var u1 = _mapList[i].ReflectionNormal;

//                for (var j = i + 1; j < _mapList.Count; j++)
//                {
//                    var u2 = _mapList[j].ReflectionNormal;

//                    if (!u1.IsNearOrthogonalTo(u2, epsilon)) return false;
//                }
//            }

//            return true;
//        }


//        public double[] MapVectorInPlace(double[] vector)
//        {
//            foreach (var reflection in _mapList)
//            {
//                var u = reflection.ReflectionNormal.ScalarArray;

//                var s = -2d * vector.VectorDot(u);

//                for (var i = 0; i < VSpaceDimensions; i++)
//                    vector[i] += s * u[i];
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
//        public HyperPlaneNormalReflectionSequence GetHyperPlaneReflectionSequenceInverse()
//        {
//            if (_mapList.Count == 0)
//                return this;

//            var reflectionNormalList =
//                ((IEnumerable<HyperPlaneNormalReflection>) _mapList)
//                .Reverse()
//                .ToList();

//            return new HyperPlaneNormalReflectionSequence(VSpaceDimensions, reflectionNormalList);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override ReflectionLinearMap GetReflectionLinearMapInverse()
//        {
//            return GetHyperPlaneReflectionSequenceInverse();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
//        {
//            return this;
//        }
    
//        /// <summary>
//        /// Create a new sequence containing the minimum number of pair-wise
//        /// orthogonal rotations and reflections equivalent to this one
//        /// </summary>
//        /// <returns></returns>
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public OrthogonalLinearMapSequence ReduceSequence()
//        {
//            return OrthogonalLinearMapSequence.CreateFromMatrix(ToMatrix());
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public IEnumerator<HyperPlaneNormalReflection> GetEnumerator()
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