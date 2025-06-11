using System.Collections.Immutable;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Reflection;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Rotation;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Scaling;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Scaling;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.SubSpaces.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.SubSpaces.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.SubSpaces.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;

public static class MatrixUtils
{
    
    public static IReadOnlyList<LinFloat64Vector> ColumnsToTuples(this Matrix<double> matrix)
    {
        var tupleList = new LinFloat64Vector[matrix.ColumnCount];

        for (var i = 0; i < matrix.ColumnCount; i++)
            tupleList[i] = matrix.Column(i).ToVector();

        return tupleList;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Matrix<double> ToMatrix(this double[,] array)
    {
        return Matrix<double>.Build.DenseOfArray(array);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Determinant(this double[,] array)
    {
        return Matrix<double>.Build.DenseOfArray(array).Determinant();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector MapVector(this Matrix<double> matrix, LinFloat64Vector vector)
    {
        return (matrix * vector.ToMathNetVector()).ToVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Matrix<double> CreateDenseIdentityMatrix(this int size)
    {
        return Matrix<double>.Build.DenseIdentity(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Matrix<double> CreateClarkeRotationMatrix(this int size)
    {
        return Matrix<double>.Build.DenseOfArray(
            Float64ArrayUtils.CreateClarkeRotationArray(size)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Matrix<double> CreateCirculantColumnMatrix(this IReadOnlyList<double> column)
    {
        return Matrix<double>.Build.DenseOfArray(
            Float64ArrayUtils.CreateCirculantColumnArray(column)
        );
    }

    public static Matrix<Complex> CreateUnitaryDftMatrix(this int size, bool inverse = false)
    {
        var nSqrtInv = 1d / Math.Sqrt(size);
        var array = new Complex[size, size];

        for (var i = 0; i < size; i++)
            for (var j = 0; j < size; j++)
            {
                var c = size.NthRootOfOne(i * j);

                array[i, j] = nSqrtInv * (inverse ? c.Conjugate() : c);
            }

        return Matrix<Complex>.Build.DenseOfArray(array);
    }



    public static int MatrixEigenDecomposition(this Matrix<double> matrix, out Tuple<double, double[]>[] realPairs, out Tuple<double, double[]>[] imagPairs)
    {
        var sysExpr = matrix.ToComplex().Evd();

        var count = sysExpr.EigenValues.Count;

        realPairs = new Tuple<double, double[]>[count];
        imagPairs = new Tuple<double, double[]>[count];

        //Console.WriteLine("Eigen Vectors Matrix");
        //Console.WriteLine(
        //    GeoTextComposerFloat64.DefaultComposer.GetArrayText(
        //        sysExpr.EigenVectors.Real().ToArray()
        //    )
        //);
        //Console.WriteLine();

        //Console.WriteLine(
        //    GeoTextComposerFloat64.DefaultComposer.GetArrayText(
        //        sysExpr.EigenVectors.Imaginary().ToArray()
        //    )
        //);
        //Console.WriteLine();

        for (var j = 0; j < count; j++)
        {
            var complexEigenValue = sysExpr.EigenValues[j];
            var complexEigenVector = sysExpr.EigenVectors.Column(j);

            realPairs[j] = new Tuple<double, double[]>(
                complexEigenValue.Real,
                complexEigenVector.Real().ToArray()
            );

            imagPairs[j] = new Tuple<double, double[]>(
                complexEigenValue.Imaginary,
                complexEigenVector.Imaginary().ToArray()
            );
        }

        return count;
    }


    public static IReadOnlyList<Tuple<Complex, MathNet.Numerics.LinearAlgebra.Vector<Complex>>> GetComplexEigenPairs(this Matrix<double> matrix)
    {
        var eigenPairList = new List<Tuple<Complex, MathNet.Numerics.LinearAlgebra.Vector<Complex>>>(matrix.RowCount);

        var sysExpr = matrix.ToComplex().Evd();
        var count = sysExpr.EigenValues.Count;

        for (var j = 0; j < count; j++)
        {
            var eigenValue = sysExpr.EigenValues[j];
            var eigenVector = sysExpr.EigenVectors.Column(j);

            eigenPairList.Add(new Tuple<Complex, MathNet.Numerics.LinearAlgebra.Vector<Complex>>(
                eigenValue,
                eigenVector
            ));
        }

        return eigenPairList;
    }

    public static IReadOnlyList<Tuple<Complex, MathNet.Numerics.LinearAlgebra.Vector<Complex>>> GetComplexEigenPairs(this Matrix<Complex> matrix)
    {
        var eigenPairList = new List<Tuple<Complex, MathNet.Numerics.LinearAlgebra.Vector<Complex>>>(matrix.RowCount);

        var sysExpr = matrix.Evd();
        var count = sysExpr.EigenValues.Count;

        for (var j = 0; j < count; j++)
        {
            var eigenValue = sysExpr.EigenValues[j];
            var eigenVector = sysExpr.EigenVectors.Column(j);

            eigenPairList.Add(new Tuple<Complex, MathNet.Numerics.LinearAlgebra.Vector<Complex>>(
                eigenValue,
                eigenVector
            ));
        }

        return eigenPairList;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Rotation3D GetRotationPart(this SquareMatrix3 matrix)
    {
        var subspaceList =
            matrix
                .GetSimpleEigenSubspaces()
                .Where(s =>
                    s.SubspaceDimensions == 2
                ).ToImmutableArray();

        if (subspaceList.Length == 0)
            return LinFloat64IdentityLinearMap3D.Instance;

        Debug.Assert(subspaceList.Length == 1);

        return subspaceList[0].GetPlanarRotation();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<LinFloat64PlaneSubspace3D> GetPlaneEigenSubspaces(this SquareMatrix3 matrix)
    {
        return matrix
            .GetSimpleEigenSubspaces()
            .Where(s => s.Subspace is LinFloat64PlaneSubspace3D)
            .Select(s => (LinFloat64PlaneSubspace3D)s.Subspace)
            .ToImmutableArray();
    }

    public static IReadOnlyList<LinFloat64SimpleEigenSubspace3D> GetSimpleEigenSubspaces(this SquareMatrix3 matrix)
    {
        var list1 = new List<LinFloat64SimpleEigenSubspace3D>();

        var sysExpr = matrix.GetComplexMatrix().Evd();
        var count = sysExpr.EigenValues.Count;

        for (var j = 0; j < count; j++)
        {
            var eigenValue = sysExpr.EigenValues[j];

            if (eigenValue.IsNearOne())
                continue;

            var eigenVector = sysExpr.EigenVectors.Column(j);

            var subspace = new LinFloat64SimpleEigenSubspace3D(
                eigenValue,
                eigenVector
            );

            if (subspace.VSpaceDimensions > 0)
                list1.Add(subspace);
        }

        if (list1.Count == 0)
            return list1;

        list1 = list1
            .OrderByDescending(s => s.SubspaceDimensions)
            .ToList();

        var subspaceList = new List<LinFloat64SimpleEigenSubspace3D>
        {
            list1[0]
        };

        foreach (var subspace in list1.Skip(1))
        {
            if (subspaceList.Any(s => s.NearContains(subspace)))
                continue;

            subspaceList.Add(subspace);
        }

        return subspaceList;
    }

    public static IReadOnlyList<LinFloat64SimpleEigenSubspace4D> GetSimpleEigenSubspaces(this SquareMatrix4 matrix)
    {
        var list1 = new List<LinFloat64SimpleEigenSubspace4D>();

        var sysExpr = matrix.GetComplexMatrix().Evd();
        var count = sysExpr.EigenValues.Count;

        for (var j = 0; j < count; j++)
        {
            var eigenValue = sysExpr.EigenValues[j];

            if (eigenValue.IsNearOne())
                continue;

            var eigenVector = sysExpr.EigenVectors.Column(j);

            var subspace = new LinFloat64SimpleEigenSubspace4D(
                eigenValue,
                eigenVector
            );

            if (subspace.VSpaceDimensions > 0)
                list1.Add(subspace);
        }

        list1 = list1
            .OrderByDescending(s => s.SubspaceDimensions)
            .ToList();

        var subspaceList = new List<LinFloat64SimpleEigenSubspace4D>
        {
            list1[0]
        };

        foreach (var subspace in list1.Skip(1))
        {
            if (subspaceList.Any(s => s.NearContains(subspace)))
                continue;

            subspaceList.Add(subspace);
        }

        return subspaceList;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<LinFloat64PlaneSubspace> GetPlaneEigenSubspaces(this Matrix<double> matrix)
    {
        return matrix
            .GetSimpleEigenSubspaces()
            .Where(s => s.Subspace is LinFloat64PlaneSubspace)
            .Select(s => (LinFloat64PlaneSubspace)s.Subspace)
            .ToImmutableArray();
    }

    public static IReadOnlyList<LinFloat64SimpleEigenSubspace> GetSimpleEigenSubspaces(this Matrix<double> matrix)
    {
        var list1 = new List<LinFloat64SimpleEigenSubspace>(matrix.RowCount);

        var sysExpr = matrix.ToComplex().Evd();
        var count = sysExpr.EigenValues.Count;

        for (var j = 0; j < count; j++)
        {
            var eigenValue = sysExpr.EigenValues[j];

            if (eigenValue.IsNearOne())
                continue;

            var eigenVector = sysExpr.EigenVectors.Column(j);

            var subspace = new LinFloat64SimpleEigenSubspace(
                eigenValue,
                eigenVector
            );

            if (subspace.VSpaceDimensions > 0)
                list1.Add(subspace);
        }

        list1 = list1
            .OrderByDescending(s => s.SubspaceDimensions)
            .ToList();

        var subspaceList = new List<LinFloat64SimpleEigenSubspace>
        {
            list1[0]
        };

        foreach (var subspace in list1.Skip(1))
        {
            if (subspaceList.Any(s => s.NearContains(subspace)))
                continue;

            subspaceList.Add(subspace);
        }

        return subspaceList;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorDirectionalScalingSequence3D GetVectorDirectionalScalingSequence3D(this SquareMatrix3 matrix)
    {
        var scaling = LinFloat64VectorDirectionalScalingSequence3D.Create();

        var mapList =
            matrix
                .GetSimpleEigenSubspaces()
                .SelectMany(s => s.GetVectorDirectionalScalingMaps());

        scaling.AppendMaps(mapList);

        return scaling;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorDirectionalScalingSequence4D GetVectorDirectionalScalingSequence4D(this SquareMatrix4 matrix)
    {
        var scaling = LinFloat64VectorDirectionalScalingSequence4D.Create();

        var mapList =
            matrix
                .GetSimpleEigenSubspaces()
                .SelectMany(s => s.GetVectorDirectionalScalingMaps());

        scaling.AppendMaps(mapList);

        return scaling;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorDirectionalScalingSequence GetVectorDirectionalScalingSequence(this Matrix<double> matrix)
    {
        var scaling = LinFloat64VectorDirectionalScalingSequence.Create(matrix.RowCount);

        var mapList =
            matrix
                .GetSimpleEigenSubspaces()
                .SelectMany(s => s.GetVectorDirectionalScalingMaps());

        scaling.AppendMaps(mapList);

        return scaling;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64HyperPlaneNormalReflectionSequence3D GetHyperPlaneNormalReflectionSequence(this SquareMatrix3 matrix)
    {
        Debug.Assert(
            matrix.Determinant.Abs().IsNearOne()
        );

        var reflection = LinFloat64HyperPlaneNormalReflectionSequence3D.Create();

        var mapList =
            matrix
                .GetSimpleEigenSubspaces()
                .SelectMany(s => s.GetHyperPlaneNormalReflectionMaps());

        reflection.AppendMaps(mapList);

        return reflection;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64HyperPlaneNormalReflectionSequence4D GetHyperPlaneNormalReflectionSequence(this SquareMatrix4 matrix)
    {
        Debug.Assert(
            matrix.Determinant.Abs().IsNearOne()
        );

        var reflection = LinFloat64HyperPlaneNormalReflectionSequence4D.Create();

        var mapList =
            matrix
                .GetSimpleEigenSubspaces()
                .SelectMany(s => s.GetHyperPlaneNormalReflectionMaps());

        reflection.AppendMaps(mapList);

        return reflection;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64HyperPlaneNormalReflectionSequence GetHyperPlaneNormalReflectionSequence(this Matrix<double> matrix)
    {
        Debug.Assert(
            matrix.Determinant().Abs().IsNearOne()
        );

        var reflection = LinFloat64HyperPlaneNormalReflectionSequence.Create(matrix.RowCount);

        var mapList =
            matrix
                .GetSimpleEigenSubspaces()
                .SelectMany(s => s.GetHyperPlaneNormalReflectionMaps());

        reflection.AppendMaps(mapList);

        return reflection;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlanarRotation3D GetPlanarRotation3D(this SquareMatrix3 matrix)
    {
        var planarSubspace =
            matrix
                .GetSimpleEigenSubspaces()
                .FirstOrDefault(s =>
                    s.Subspace is LinFloat64PlaneSubspace3D
                );

        return planarSubspace is null
            ? LinFloat64PlanarRotation3D.Identity
            : planarSubspace.GetPlanarRotation();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToVectorRotationSequence4D GetVectorToVectorRotationSequence4D(this SquareMatrix4 matrix)
    {
        Debug.Assert(
            matrix.Determinant.IsNearOne()
        );

        var rotation = LinFloat64VectorToVectorRotationSequence4D.Create();

        var mapList =
            matrix
                .GetSimpleEigenSubspaces()
                .SelectMany(s => s.GetVectorToVectorRotationMaps());

        rotation.AppendMaps(mapList);

        return rotation;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlanarRotationSequence GetVectorToVectorRotationSequence(this Matrix<double> matrix)
    {
        //Debug.Assert(
        //    matrix.Determinant().IsNearOne()
        //);

        var rotation = LinFloat64PlanarRotationSequence.Create(matrix.RowCount);

        var mapList =
            matrix
                .GetSimpleEigenSubspaces()
                .SelectMany(s => s.GetVectorToVectorRotationMaps());

        rotation.AppendMaps(mapList);

        return rotation;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64OrthogonalLinearMapSequence GetBasicLinearMapSequence(this Matrix<double> matrix)
    {
        return LinFloat64OrthogonalLinearMapSequence.CreateFromMatrix(matrix);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ClearNearZeroItems(this MathNet.Numerics.LinearAlgebra.Vector<double> vector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        for (var i = 0; i < vector.Count; i++)
            if (vector[i].IsNearZero(zeroEpsilon))
                vector[i] = 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero(this MathNet.Numerics.LinearAlgebra.Vector<double> v1, double zeroEpsilon = 1e-12d)
    {
        return v1.DotProduct(v1).IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearParallelTo(this MathNet.Numerics.LinearAlgebra.Vector<double> v1, MathNet.Numerics.LinearAlgebra.Vector<double> v2, double zeroEpsilon = 1e-12d)
    {
        var cosAngle =
            v1.DotProduct(v2) / (v1.L2Norm() * v2.L2Norm());

        return cosAngle.Abs().IsNearOne(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOrthogonalTo(this MathNet.Numerics.LinearAlgebra.Vector<double> v1, MathNet.Numerics.LinearAlgebra.Vector<double> v2, double zeroEpsilon = 1e-12d)
    {
        return v1.DotProduct(v2).IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D MapAffineVector(this SquareMatrix3 matrix, ILinFloat64Vector2D vector)
    {
        return LinFloat64Vector2D.Create(matrix[0, 0] * vector.X + matrix[0, 1] * vector.Y,
            matrix[1, 0] * vector.X + matrix[1, 1] * vector.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D MapAffineVector(this SquareMatrix4 matrix, ILinFloat64Vector3D vector)
    {
        return LinFloat64Vector3D.Create(matrix[0, 0] * vector.X + matrix[0, 1] * vector.Y + matrix[0, 2] * vector.Z,
            matrix[1, 0] * vector.X + matrix[1, 1] * vector.Y + matrix[1, 2] * vector.Z,
            matrix[2, 0] * vector.X + matrix[2, 1] * vector.Y + matrix[2, 2] * vector.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<LinFloat64Vector2D> MapAffineVectors(this SquareMatrix3 matrix, ILinFloat64Vector2D vector1, ILinFloat64Vector2D vector2)
    {
        return new Pair<LinFloat64Vector2D>(
            matrix.MapAffineVector(vector1),
            matrix.MapAffineVector(vector2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<LinFloat64Vector3D> MapAffineVectors(this SquareMatrix4 matrix, ILinFloat64Vector3D vector1, ILinFloat64Vector3D vector2)
    {
        return new Pair<LinFloat64Vector3D>(
            matrix.MapAffineVector(vector1),
            matrix.MapAffineVector(vector2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<LinFloat64Vector2D> MapAffineVectors(this SquareMatrix3 matrix, ILinFloat64Vector2D vector1, ILinFloat64Vector2D vector2, ILinFloat64Vector2D vector3)
    {
        return new Triplet<LinFloat64Vector2D>(
            matrix.MapAffineVector(vector1),
            matrix.MapAffineVector(vector2),
            matrix.MapAffineVector(vector3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<LinFloat64Vector3D> MapAffineVectors(this SquareMatrix4 matrix, ILinFloat64Vector3D vector1, ILinFloat64Vector3D vector2, ILinFloat64Vector3D vector3)
    {
        return new Triplet<LinFloat64Vector3D>(
            matrix.MapAffineVector(vector1),
            matrix.MapAffineVector(vector2),
            matrix.MapAffineVector(vector3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D MapAffinePoint(this SquareMatrix3 matrix, ILinFloat64Vector2D point)
    {
        var x = matrix[0, 0] * point.X + matrix[0, 1] * point.Y + matrix[0, 2];
        var y = matrix[1, 0] * point.X + matrix[1, 1] * point.Y + matrix[1, 2];
        var w = matrix[2, 0] * point.X + matrix[2, 1] * point.Y + matrix[2, 2];

        w = 1d / w;

        return LinFloat64Vector2D.Create(x * w, y * w);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D MapAffinePoint(this SquareMatrix4 matrix, ILinFloat64Vector3D point)
    {
        var x = matrix[0, 0] * point.X + matrix[0, 1] * point.Y + matrix[0, 2] * point.Z + matrix[0, 3];
        var y = matrix[1, 0] * point.X + matrix[1, 1] * point.Y + matrix[1, 2] * point.Z + matrix[1, 3];
        var z = matrix[2, 0] * point.X + matrix[2, 1] * point.Y + matrix[2, 2] * point.Z + matrix[2, 3];
        var w = matrix[3, 0] * point.X + matrix[3, 1] * point.Y + matrix[3, 2] * point.Z + matrix[3, 3];

        w = 1d / w;

        return LinFloat64Vector3D.Create(x * w, y * w, z * w);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<double> GetComponents(this SquareMatrix4 matrix, bool columnMajorOrder = true)
    {
        if (columnMajorOrder)
        {
            yield return matrix.Scalar00;//matrix[0];
            yield return matrix.Scalar10;//matrix[1];
            yield return matrix.Scalar20;//matrix[2];
            yield return matrix.Scalar30;//matrix[3];

            yield return matrix.Scalar01;//matrix[4];
            yield return matrix.Scalar11;//matrix[5];
            yield return matrix.Scalar21;//matrix[6];
            yield return matrix.Scalar31;//matrix[7];

            yield return matrix.Scalar02;//matrix[8];
            yield return matrix.Scalar12;//matrix[9];
            yield return matrix.Scalar22;//matrix[10];
            yield return matrix.Scalar32;//matrix[11];

            yield return matrix.Scalar03;//matrix[12];
            yield return matrix.Scalar13;//matrix[13];
            yield return matrix.Scalar23;//matrix[14];
            yield return matrix.Scalar33;//matrix[15];
        }
        else
        {
            yield return matrix.Scalar00;//matrix[0];
            yield return matrix.Scalar01;//matrix[4];
            yield return matrix.Scalar02;//matrix[8];
            yield return matrix.Scalar03;//matrix[12];

            yield return matrix.Scalar10;//matrix[1];
            yield return matrix.Scalar11;//matrix[5];
            yield return matrix.Scalar12;//matrix[9];
            yield return matrix.Scalar13;//matrix[13];

            yield return matrix.Scalar20;//matrix[2];
            yield return matrix.Scalar21;//matrix[6];
            yield return matrix.Scalar22;//matrix[10];
            yield return matrix.Scalar23;//matrix[14];

            yield return matrix.Scalar30;//matrix[3];
            yield return matrix.Scalar31;//matrix[7];
            yield return matrix.Scalar32;//matrix[11];
            yield return matrix.Scalar33;//matrix[15];
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2 Lerp(this double t, SquareMatrix2 v1, SquareMatrix2 v2)
    {
        var s = 1.0d - t;

        return new SquareMatrix2()
        {
            Scalar00 = s * v1.Scalar00 + t * v2.Scalar00,
            Scalar01 = s * v1.Scalar01 + t * v2.Scalar01,
            Scalar10 = s * v1.Scalar10 + t * v2.Scalar10,
            Scalar11 = s * v1.Scalar11 + t * v2.Scalar11
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix3 Lerp(this double t, SquareMatrix3 v1, SquareMatrix3 v2)
    {
        var s = 1.0d - t;

        return new SquareMatrix3()
        {
            Scalar00 = s * v1.Scalar00 + t * v2.Scalar00,
            Scalar10 = s * v1.Scalar10 + t * v2.Scalar10,
            Scalar20 = s * v1.Scalar20 + t * v2.Scalar20,
            Scalar01 = s * v1.Scalar01 + t * v2.Scalar01,
            Scalar11 = s * v1.Scalar11 + t * v2.Scalar11,
            Scalar21 = s * v1.Scalar21 + t * v2.Scalar21,
            Scalar02 = s * v1.Scalar02 + t * v2.Scalar02,
            Scalar12 = s * v1.Scalar12 + t * v2.Scalar12,
            Scalar22 = s * v1.Scalar22 + t * v2.Scalar22
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<SquareMatrix2> Lerp(this IEnumerable<double> tList, SquareMatrix2 v1, SquareMatrix2 v2)
    {
        return tList.Select(t => t.Lerp(v1, v2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<SquareMatrix3> Lerp(this IEnumerable<double> tList, SquareMatrix3 v1, SquareMatrix3 v2)
    {
        return tList.Select(t => t.Lerp(v1, v2));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Matrix> GetInvertibleMatrixPair(this Random randomGenerator, int n, int maxOpCount = 10)
    {
        var arrayComposer = Float64InvertibleArrayComposer.Create(n);

        arrayComposer.ApplyRandomOperations(randomGenerator, maxOpCount);

        var matrix = arrayComposer.GetMatrix();
        var matrixInverse = arrayComposer.GetMatrixInverse();

        return new Pair<Matrix>(matrix, matrixInverse);
    }

}