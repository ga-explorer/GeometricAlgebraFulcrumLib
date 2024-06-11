using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;

public sealed class LinFloat64HyperPlaneNormalReflectionSequence :
    LinFloat64ReflectionBase,
    IReadOnlyList<LinFloat64HyperPlaneNormalReflection>
{
    //public static int MatrixEigenDecomposition(Matrix<double> matrix, out Tuple<double, double[]>[] realPairs, out Tuple<double, double[]>[] imagPairs)
    //{
    //    var sysExpr = matrix.ToComplex().Evd();

    //    var count = sysExpr.EigenValues.Count;

    //    realPairs = new Tuple<double, double[]>[count];
    //    imagPairs = new Tuple<double, double[]>[count];

    //    //Console.WriteLine("Eigen Vectors Matrix");
    //    //Console.WriteLine(
    //    //    GeoTextComposerFloat64.DefaultComposer.GetArrayText(
    //    //        sysExpr.EigenVectors.Real().ToArray()
    //    //    )
    //    //);
    //    //Console.WriteLine();

    //    //Console.WriteLine(
    //    //    GeoTextComposerFloat64.DefaultComposer.GetArrayText(
    //    //        sysExpr.EigenVectors.Imaginary().ToArray()
    //    //    )
    //    //);
    //    //Console.WriteLine();

    //    for (var j = 0; j < count; j++)
    //    {
    //        var complexEigenValue = sysExpr.EigenValues[j];
    //        var complexEigenVector = sysExpr.EigenVectors.Column(j);

    //        realPairs[j] = new Tuple<double, double[]>(
    //            complexEigenValue.Real,
    //            complexEigenVector.Real().ToArray()
    //        );

    //        imagPairs[j] = new Tuple<double, double[]>(
    //            complexEigenValue.Imaginary,
    //            complexEigenVector.Imaginary().ToArray()
    //        );
    //    }

    //    return count;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Pair<HyperPlaneNormalReflection> ComplexEigenPairToHyperPlaneReflections(double realValue, double imagValue, double[] realVector, double[] imagVector)
    //{
    //    return VectorToVectorRotationSequence
    //        .ComplexEigenPairToSimpleVectorRotation(
    //            realValue, 
    //            imagValue, 
    //            realVector, 
    //            imagVector
    //        ).GetHyperPlaneReflectionPair();
    //}

    //public static LinFloat64HyperPlaneNormalReflectionSequence CreateFromReflectionMatrix(Matrix<double> matrix)
    //{
    //    // Make sure it's a reflection matrix
    //    Debug.Assert(
    //        matrix.RowCount == matrix.ColumnCount &&
    //        matrix.Determinant().Abs().IsNearOne()
    //    );

    //    var reflectionSequence = 
    //        new LinFloat64HyperPlaneNormalReflectionSequence(matrix.RowCount);

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

    //        Console.WriteLine($"Real Eigen Value {i + 1}: {realValue}");
    //        Console.WriteLine($"Imag Eigen Value {i + 1}: {imagValue}");
    //        Console.WriteLine();

    //        Console.WriteLine($"Real Eigen Vector {i + 1}: {realVector.CreateTuple()}");
    //        Console.WriteLine($"Imag Eigen Vector {i + 1}: {imagVector.CreateTuple()}");
    //        Console.WriteLine();

    //        if (imagValue.IsNearZero())
    //        {
    //            // Ignore identity reflections
    //            if (realValue.IsNearOne())
    //                continue;

    //            var v1 = realVector.VectorDivide(realVector.GetVectorNorm()).CreateTuple();
    //            var v2 = imagVector.VectorDivide(imagVector.GetVectorNorm()).CreateTuple();

    //            // Make sure both eigen vector parts encode the same 1-dimensional subspace
    //            Debug.Assert(
    //                v1.VectorDot(v2).Abs().IsNearOne()
    //            );

    //            Console.WriteLine("Hyper Plane Reflection: ");
    //            Console.WriteLine($"   Reflection Unit Normal: {v1}");
    //            Console.WriteLine();

    //            reflectionSequence.AppendMap(v1);

    //            continue;
    //        }

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

    //        var (r1, r2) =
    //            ComplexEigenPairToHyperPlaneReflections(
    //                realValue,
    //                imagValue,
    //                realVector,
    //                imagVector
    //            );

    //        reflectionSequence
    //            .AppendMap(r1)
    //            .AppendMap(r2);
    //    }

    //    return reflectionSequence;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64HyperPlaneNormalReflectionSequence CreateFromReflectionMatrix(Matrix<double> matrix)
    {
        return matrix.GetHyperPlaneNormalReflectionSequence();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64HyperPlaneNormalReflectionSequence Create(int dimensions)
    {
        return new LinFloat64HyperPlaneNormalReflectionSequence(dimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64HyperPlaneNormalReflectionSequence Create(ILinFloat64HyperPlaneNormalReflectionLinearMap reflection)
    {
        var reflectionSequence =
            new LinFloat64HyperPlaneNormalReflectionSequence(reflection.VSpaceDimensions);

        reflectionSequence.AppendMap(reflection);

        return reflectionSequence;
    }

    public static LinFloat64HyperPlaneNormalReflectionSequence CreateRandom(Random random, int dimensions, int count)
    {
        var rotationSequence = new LinFloat64HyperPlaneNormalReflectionSequence(dimensions);

        for (var i = 0; i < count; i++)
        {
            var u = random.GetLinVector(dimensions).CreateUnitLinVector();

            rotationSequence.AppendMap(
                LinFloat64HyperPlaneNormalReflection.Create(u)
            );
        }

        return rotationSequence;
    }

    public static LinFloat64HyperPlaneNormalReflectionSequence CreateRandomOrthogonal(Random random, int dimensions, int count)
    {
        if (count > dimensions)
            throw new ArgumentOutOfRangeException(nameof(count));

        var rotationSequence = new LinFloat64HyperPlaneNormalReflectionSequence(dimensions);

        var vectorList =
            random.GetMathNetOrthonormalVectors(dimensions, count);

        for (var i = 0; i < count; i++)
        {
            var u = vectorList[i].CreateLinVector();

            rotationSequence.AppendMap(
                LinFloat64HyperPlaneNormalReflection.Create(u)
            );
        }

        return rotationSequence;
    }


    private readonly List<LinFloat64HyperPlaneNormalReflection> _mapList
        = new List<LinFloat64HyperPlaneNormalReflection>();


    public int Count
        => _mapList.Count;

    public LinFloat64HyperPlaneNormalReflection this[int index]
        => _mapList[index];

    public override int VSpaceDimensions { get; }

    public override bool SwapsHandedness
        => _mapList.Count.IsOdd();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64HyperPlaneNormalReflectionSequence(int dimensions)
    {
        if (dimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(dimensions));

        VSpaceDimensions = dimensions;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64HyperPlaneNormalReflectionSequence(int dimensions, List<LinFloat64HyperPlaneNormalReflection> reflectionNormalList)
    {
        if (dimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(dimensions));

        VSpaceDimensions = dimensions;
        _mapList = reflectionNormalList;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64HyperPlaneNormalReflectionSequence AppendMap(LinFloat64Vector reflectionNormal)
    {
        _mapList.Add(
            LinFloat64HyperPlaneNormalReflection.Create(reflectionNormal)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64HyperPlaneNormalReflectionSequence AppendMap(LinFloat64HyperPlaneNormalReflection reflection)
    {
        if (reflection.VSpaceDimensions != VSpaceDimensions)
            throw new ArgumentException();

        _mapList.Add(reflection);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64HyperPlaneNormalReflectionSequence AppendMap(ILinFloat64HyperPlaneNormalReflectionLinearMap reflection)
    {
        var r2 =
            reflection as LinFloat64HyperPlaneNormalReflection
            ?? reflection.ToHyperPlaneNormalReflection();

        _mapList.Add(r2);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64HyperPlaneNormalReflectionSequence PrependMap(LinFloat64Vector reflectionNormal)
    {
        _mapList.Insert(
            0,
            LinFloat64HyperPlaneNormalReflection.Create(reflectionNormal)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64HyperPlaneNormalReflectionSequence PrependMap(LinFloat64HyperPlaneNormalReflection reflection)
    {
        _mapList.Insert(
            0,
            reflection
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64HyperPlaneNormalReflectionSequence InsertMap(int index, LinFloat64Vector reflectionNormal)
    {
        _mapList.Insert(
            index,
            LinFloat64HyperPlaneNormalReflection.Create(reflectionNormal)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64HyperPlaneNormalReflectionSequence InsertMap(int index, LinFloat64HyperPlaneNormalReflection reflection)
    {
        _mapList.Insert(
            index,
            reflection
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64HyperPlaneNormalReflectionSequence AppendMaps(IEnumerable<LinFloat64HyperPlaneNormalReflection> reflectionList)
    {
        _mapList.AddRange(reflectionList);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64HyperPlaneNormalReflectionSequence PrependMaps(IEnumerable<LinFloat64HyperPlaneNormalReflection> reflectionList)
    {
        _mapList.InsertRange(0, reflectionList);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64HyperPlaneNormalReflectionSequence InsertMaps(int index, IEnumerable<LinFloat64HyperPlaneNormalReflection> reflectionList)
    {
        _mapList.InsertRange(index, reflectionList);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _mapList.All(a => a.IsValid());
    }

    public override bool IsIdentity()
    {
        if (_mapList.Count == 0)
            return true;

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
    /// Test if all reflection normals in this sequence are nearly pair-wise orthogonal
    /// </summary>
    /// <returns></returns>
    public bool IsNearOrthogonalReflectionsSequence(double epsilon = 1e-12)
    {
        if (_mapList.Count > VSpaceDimensions)
            return false;

        for (var i = 0; i < _mapList.Count; i++)
        {
            var u1 = _mapList[i].ReflectionNormal;

            for (var j = i + 1; j < _mapList.Count; j++)
            {
                var u2 = _mapList[j].ReflectionNormal;

                if (!u1.IsNearOrthogonalTo(u2, epsilon)) return false;
            }
        }

        return true;
    }


    public double[] MapVectorInPlace(double[] vector)
    {
        foreach (var reflection in _mapList)
        {
            var u = reflection.ReflectionNormal;

            var s = -2d * vector.VectorDot(u);

            for (var i = 0; i < VSpaceDimensions; i++)
                vector[i] += s * u[i];
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

        foreach (var reflection in _mapList)
        {
            var u = reflection.ReflectionNormal;
            var s = -2d * composer.VectorDot(u);

            composer.AddVector(u, s);
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
            .SetVector(vector, 1d);

        foreach (var reflection in _mapList)
        {
            var u = reflection.ReflectionNormal;
            var s = -2d * composer.VectorDot(u);

            composer.AddVector(u, s);
        }

        return composer.GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64HyperPlaneNormalReflectionSequence GetHyperPlaneReflectionSequenceInverse()
    {
        if (_mapList.Count == 0)
            return this;

        var reflectionNormalList =
            ((IEnumerable<LinFloat64HyperPlaneNormalReflection>)_mapList)
            .Reverse()
            .ToList();

        return new LinFloat64HyperPlaneNormalReflectionSequence(VSpaceDimensions, reflectionNormalList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64ReflectionBase GetReflectionLinearMapInverse()
    {
        return GetHyperPlaneReflectionSequenceInverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
    {
        return this;
    }

    /// <summary>
    /// Create a new sequence containing the minimum number of pair-wise
    /// orthogonal rotations and reflections equivalent to this one
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64OrthogonalLinearMapSequence ReduceSequence()
    {
        return LinFloat64OrthogonalLinearMapSequence.CreateFromMatrix(ToMatrix(VSpaceDimensions, VSpaceDimensions));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<LinFloat64HyperPlaneNormalReflection> GetEnumerator()
    {
        return _mapList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}