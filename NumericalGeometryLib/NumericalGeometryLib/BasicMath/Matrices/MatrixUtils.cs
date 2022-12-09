using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using NumericalGeometryLib.BasicMath.Maps.SpaceND;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Reflection;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Rotation;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Scaling;
using NumericalGeometryLib.BasicMath.SubSpaces;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;
using NumericalGeometryLib.GeometricAlgebra.Basis;
using NumericalGeometryLib.GeometricAlgebra.EuclideanND;

namespace NumericalGeometryLib.BasicMath.Matrices
{
    public static class MatrixUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MathNet.Numerics.LinearAlgebra.Vector<double> ToVector(this double[] array)
        {
            return MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfArray(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MathNet.Numerics.LinearAlgebra.Vector<double> ToVector(this IEnumerable<double> scalarList)
        {
            return MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfEnumerable(scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple ToTuple(this MathNet.Numerics.LinearAlgebra.Vector<double> vector)
        {
            return vector.ToArray().CreateTuple();
        }

        public static IReadOnlyList<Float64Tuple> ColumnsToTuples(this Matrix<double> matrix)
        {
            var tupleList = new Float64Tuple[matrix.ColumnCount];

            for (var i = 0; i < matrix.ColumnCount; i++)
                tupleList[i] = matrix.Column(i).ToTuple();

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
        public static Float64Tuple MapVector(this Matrix<double> matrix, IEnumerable<double> vector)
        {
            return (matrix * MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfEnumerable(vector)).ToTuple();
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
        

        public static Matrix<double> GetOutermorphismMatrix(this Matrix<double> matrix, int grade)
        {
            if (matrix.RowCount != matrix.ColumnCount)
                throw new InvalidOperationException();

            if (grade == 0)
                return Matrix<double>.Build.DenseOfArray(new [,]{{1d}});

            if (grade == 1)
                return matrix;

            var dimensions = matrix.RowCount;
            var kVectorDimension = dimensions.GetBinomialCoefficient(grade);
            var columnList = matrix.ColumnsToTuples();

            var array = new double[kVectorDimension, kVectorDimension];

            for (var index = 0UL; index < kVectorDimension; index++)
            {
                var vectorList = 
                    BasisBladeUtils
                        .BasisBladeGradeIndexToId((uint) grade, index)
                        .PatternToPositions()
                        .Select(i => EgaKVector.CreateVector(columnList[i].ScalarArray))
                        .ToArray();

                var column = 
                    vectorList
                        .Skip(1)
                        .Aggregate(vectorList[0], (a, b) => a.Op(b))
                        .ScalarArray;

                for (var i = 0UL; i < kVectorDimension; i++)
                    array[i, index] = column[i];
            }

            return Matrix<double>.Build.DenseOfArray(array);
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

        public static IReadOnlyList<Float64SimpleEigenSubspace> GetSimpleEigenSubspaces(this Matrix<double> matrix)
        {
            var list1 = new List<Float64SimpleEigenSubspace>(matrix.RowCount);
            
            var sysExpr = matrix.ToComplex().Evd();
            var count = sysExpr.EigenValues.Count;

            for (var j = 0; j < count; j++)
            {
                var eigenValue = sysExpr.EigenValues[j];

                if (eigenValue.IsNearOne())
                    continue;

                var eigenVector = sysExpr.EigenVectors.Column(j);

                var subspace = new Float64SimpleEigenSubspace(
                    eigenValue, 
                    eigenVector
                );

                if (subspace.Dimensions > 0)
                    list1.Add(subspace);
            }

            list1 = list1
                .OrderByDescending(s => s.SubspaceDimensions)
                .ToList();
            
            var subspaceList = new List<Float64SimpleEigenSubspace>
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
        public static VectorDirectionalScalingSequence GetVectorDirectionalScalingSequence(this Matrix<double> matrix)
        {
            var scaling = VectorDirectionalScalingSequence.Create(matrix.RowCount);

            var mapList =
                matrix
                    .GetSimpleEigenSubspaces()
                    .SelectMany(s => s.GetVectorDirectionalScalingMaps());

            scaling.AppendMaps(mapList);

            return scaling;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HyperPlaneNormalReflectionSequence GetHyperPlaneNormalReflectionSequence(this Matrix<double> matrix)
        {
            Debug.Assert(
                matrix.Determinant().Abs().IsNearOne()
            );

            var reflection = HyperPlaneNormalReflectionSequence.Create(matrix.RowCount);

            var mapList =
                matrix
                    .GetSimpleEigenSubspaces()
                    .SelectMany(s => s.GetHyperPlaneNormalReflectionMaps());

            reflection.AppendMaps(mapList);

            return reflection;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorToVectorRotationSequence GetVectorToVectorRotationSequence(this Matrix<double> matrix)
        {
            Debug.Assert(
                matrix.Determinant().IsNearOne()
            );

            var rotation = VectorToVectorRotationSequence.Create(matrix.RowCount);

            var mapList =
                matrix
                    .GetSimpleEigenSubspaces()
                    .SelectMany(s => s.GetVectorToVectorRotationMaps());

            rotation.AppendMaps(mapList);

            return rotation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OrthogonalLinearMapSequence GetBasicLinearMapSequence(this Matrix<double> matrix)
        {
            return OrthogonalLinearMapSequence.CreateFromMatrix(matrix);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearNearZeroItems(this MathNet.Numerics.LinearAlgebra.Vector<double> vector, double epsilon = 1e-12)
        {
            for (var i = 0; i < vector.Count; i++)
                if (vector[i].IsNearZero(epsilon))
                    vector[i] = 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZero(this MathNet.Numerics.LinearAlgebra.Vector<double> v1, double epsilon = 1e-12d)
        {
            return v1.DotProduct(v1).IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearParallelTo(this MathNet.Numerics.LinearAlgebra.Vector<double> v1, MathNet.Numerics.LinearAlgebra.Vector<double> v2, double epsilon = 1e-12d)
        {
            var cosAngle = 
                v1.DotProduct(v2) / (v1.L2Norm() * v2.L2Norm());

            return cosAngle.Abs().IsNearOne(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearOrthogonalTo(this MathNet.Numerics.LinearAlgebra.Vector<double> v1, MathNet.Numerics.LinearAlgebra.Vector<double> v2, double epsilon = 1e-12d)
        {
            return v1.DotProduct(v2).IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D TimesVector(this SquareMatrix4 matrix, IFloat64Tuple3D vector)
        {
            return new Float64Tuple3D(
                matrix[0, 0] * vector.X + matrix[0, 1] * vector.Y + matrix[0, 2] * vector.Z,
                matrix[1, 0] * vector.X + matrix[1, 1] * vector.Y + matrix[1, 2] * vector.Z,
                matrix[2, 0] * vector.X + matrix[2, 1] * vector.Y + matrix[2, 2] * vector.Z
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<Float64Tuple3D> TimesVectors(this SquareMatrix4 matrix, IFloat64Tuple3D vector1, IFloat64Tuple3D vector2)
        {
            return new Pair<Float64Tuple3D>(
                matrix.TimesVector(vector1),
                matrix.TimesVector(vector2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<Float64Tuple3D> TimesVectors(this SquareMatrix4 matrix, IFloat64Tuple3D vector1, IFloat64Tuple3D vector2, IFloat64Tuple3D vector3)
        {
            return new Triplet<Float64Tuple3D>(
                matrix.TimesVector(vector1),
                matrix.TimesVector(vector2),
                matrix.TimesVector(vector3)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D TimesPoint(this SquareMatrix4 matrix, IFloat64Tuple3D point)
        {
            return new Float64Tuple3D(
                matrix[0, 0] * point.X + matrix[0, 1] * point.Y + matrix[0, 2] * point.Z + matrix[0, 3],
                matrix[1, 0] * point.X + matrix[1, 1] * point.Y + matrix[1, 2] * point.Z + matrix[1, 3],
                matrix[2, 0] * point.X + matrix[2, 1] * point.Y + matrix[2, 2] * point.Z + matrix[2, 3]
            );
        }

   
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetComponents(this SquareMatrix4 matrix, bool columnMajorOrder = true)
        {
            if (columnMajorOrder)
            {
                yield return matrix[0];
                yield return matrix[1];
                yield return matrix[2];
                yield return matrix[3];

                yield return matrix[4];
                yield return matrix[5];
                yield return matrix[6];
                yield return matrix[7];

                yield return matrix[8];
                yield return matrix[9];
                yield return matrix[10];
                yield return matrix[11];

                yield return matrix[12];
                yield return matrix[13];
                yield return matrix[14];
                yield return matrix[15];
            }
            else
            {
                yield return matrix[0];
                yield return matrix[4];
                yield return matrix[8];
                yield return matrix[12];

                yield return matrix[1];
                yield return matrix[5];
                yield return matrix[9];
                yield return matrix[13];

                yield return matrix[2];
                yield return matrix[6];
                yield return matrix[10];
                yield return matrix[14];

                yield return matrix[3];
                yield return matrix[7];
                yield return matrix[11];
                yield return matrix[15];
            }
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SquareMatrix2 Lerp(this double t, SquareMatrix2 v1, SquareMatrix2 v2)
        {
            var s = 1.0d - t;

            return new SquareMatrix2()
            {
                [0] = s * v1[0] + t * v2[0],
                [1] = s * v1[1] + t * v2[1],
                [2] = s * v1[2] + t * v2[2],
                [3] = s * v1[3] + t * v2[3]
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
    }
}
