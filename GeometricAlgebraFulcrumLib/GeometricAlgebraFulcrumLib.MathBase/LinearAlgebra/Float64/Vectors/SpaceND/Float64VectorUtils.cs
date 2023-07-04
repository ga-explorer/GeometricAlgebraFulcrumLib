using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.Dictionary;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.SpaceND.Rotation;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.SpaceND
{
    public static class Float64VectorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidLinVectorDictionary(this IReadOnlyDictionary<int, double> indexScalarDictionary)
        {
            return indexScalarDictionary.Count switch
            {
                0 => indexScalarDictionary is EmptyDictionary<int, double>,

                1 => indexScalarDictionary is SingleItemDictionary<int, double> dict &&
                     dict.Key >= 0 &&
                     dict.Value.IsValid() &&
                     !dict.Value.IsZero(),

                _ => indexScalarDictionary.All(p =>
                    p.Key >= 0 &&
                    p.Value.IsValid() &&
                    !p.Value.IsZero()
                )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetVectorTermScalar(this IReadOnlyList<double> vector, int index)
        {
            return index < 0 || index >= vector.Count
                ? 0d : vector[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetVectorTermScalar(this IReadOnlyDictionary<int, double> vector, int index)
        {
            return vector.TryGetValue(index, out var scalar)
                ? scalar
                : 0d;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64RandomComposer CreateLinRandomComposer(this int vSpaceDimensions)
        {
            return new LinFloat64RandomComposer(vSpaceDimensions);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64RandomComposer CreateLinRandomComposer(this int vSpaceDimensions, int seed)
        {
            return new LinFloat64RandomComposer(vSpaceDimensions, seed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64RandomComposer CreateLinRandomComposer(this int vSpaceDimensions, System.Random randomGenerator)
        {
            return new LinFloat64RandomComposer(vSpaceDimensions, randomGenerator);
        }


        public static double[] ToArray(this Float64Vector vector, int vSpaceDimensions)
        {
            if (vSpaceDimensions < vector.VSpaceDimensions)
                throw new ArgumentException(nameof(vSpaceDimensions));

            var array = new double[vSpaceDimensions];

            foreach (var (index, scalar) in vector.IndexScalarPairs)
                array[index] = scalar;

            return array;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ENorm(this Float64Vector mv)
        {
            return mv.ENormSquared().Sqrt();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector EInverse(this Float64Vector mv1)
        {
            return mv1.Divide(
                mv1.ESp(mv1)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector MapScalars(this Float64Vector mv, Func<double, double> scalarMapping)
        {
            var termList =
                mv.IndexScalarPairs.Select(
                    term => new KeyValuePair<int, double>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return Float64VectorComposer.Create()
                .SetTerms(termList)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector MapScalars(this Float64Vector mv, Func<int, double, double> scalarMapping)
        {
            var termList =
                mv.IndexScalarPairs.Select(
                    term => new KeyValuePair<int, double>(
                        term.Key,
                        scalarMapping(term.Key, term.Value)
                    )
                );

            return Float64VectorComposer.Create()
                .AddTerms(termList)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector MapBasisVectors(this Float64Vector mv, Func<int, int> basisMapping, bool simplify = true)
        {
            var termList =
                mv.IndexScalarPairs.Select(
                    term => new KeyValuePair<int, double>(
                        basisMapping(term.Key),
                        term.Value
                    )
                );

            return Float64VectorComposer.Create()
                .AddTerms(termList)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector MapBasisVectors(this Float64Vector mv, Func<int, double, int> basisMapping, bool simplify = true)
        {
            var termList =
                mv.IndexScalarPairs.Select(
                    term => new KeyValuePair<int, double>(
                        basisMapping(term.Key, term.Value),
                        term.Value
                    )
                );

            return Float64VectorComposer.Create()
                .AddTerms(termList)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector MapTerms(this Float64Vector mv, Func<int, double, KeyValuePair<int, double>> termMapping, bool simplify = true)
        {
            var termList =
                mv.IndexScalarPairs.Select(
                    term => termMapping(term.Key, term.Value)
                );

            return Float64VectorComposer.Create()
                .AddTerms(termList)
                .GetVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetEuclideanAngle(this Float64Vector v1, Float64Vector v2, bool assumeUnitVectors = false)
        {
            var v12Sp = v1.ESp(v2);

            var angle = assumeUnitVectors
                ? v12Sp
                : v12Sp / (v1.ENormSquared() * v2.ENormSquared()).Sqrt();

            return angle.ArcCos();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector GetBisector(this Float64Vector v1, Float64Vector v2, bool assumeEqualNormVectors = false)
        {
            return (v1 + v2).Times(05d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector GetUnitBisector(this Float64Vector v1, Float64Vector v2, bool assumeEqualNormVectors = false)
        {
            var bisector = assumeEqualNormVectors
                ? v1 + v2
                : v1.DivideByENorm() + v2.DivideByENorm();

            return bisector.DivideByENorm();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector GetUnitNormal(this ILinSignedBasisVector vector)
        {
            var rotatedVector = vector.ToVector();

            if (rotatedVector.IsNearVectorBasis(0))
                return 1.CreateLinVector();

            // For smoother motions, find the quaternion q that
            // rotates e1 to vector, then use q to rotate e2
            return LinFloat64AxisToVectorRotation
                .CreateFromRotatedVector(0.ToLinBasisVector(), rotatedVector)
                .MapBasisVector(1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector GetUnitNormal(this Float64Vector vector)
        {
            var rotatedVector = vector.DivideByENorm();

            if (rotatedVector.IsNearVectorBasis(0))
                return 1.CreateLinVector();

            // For smoother motions, find the quaternion q that
            // rotates e1 to vector, then use q to rotate e2
            return LinFloat64AxisToVectorRotation
                .CreateFromRotatedVector(0.ToLinBasisVector(), rotatedVector)
                .MapBasisVector(1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetDistanceSquaredToPoint(this Float64Vector point1, Float64Vector point2)
        {
            return point1.Subtract(point2).ENormSquared();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetDistanceToPoint(this Float64Vector point1, Float64Vector point2)
        {
            return point1.Subtract(point2).ENormSquared().Sqrt();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector ToUnitVector(this Float64Vector vector)
        {
            var length = vector.ENorm();

            return length.IsZero()
                ? new Float64Vector()
                : vector.Times(1d / length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector ToNegativeUnitVector(this Float64Vector vector)
        {
            var length = vector.ENorm();

            return length.IsZero()
                ? new Float64Vector()
                : vector.Times(-1d / length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorDot(this Float64Vector vector1, Float64Vector vector2)
        {
            return vector1.ESp(vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorDot(this Float64Vector vector1, LinSignedBasisVector vector2)
        {
            return vector1.GetTermScalar(vector2.Index) * vector2.Sign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetAngleCos(this Float64Vector vector1, Float64Vector vector2)
        {
            var uuDot = vector1.ENormSquared();
            var vvDot = vector2.ENormSquared();
            var uvDot = vector1.ESp(vector2);

            var norm = (uuDot * vvDot).Sqrt();

            return norm.IsZero()
                ? 0d
                : (uvDot / norm).Clamp(-1, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetAngleCosWithUnit(this Float64Vector vector1, ILinSignedBasisVector vector2)
        {
            Debug.Assert(
                vector2.Sign.IsNotZero
            );

            var uuDot = vector1.ENormSquared();
            var uvDot = vector1.ESp(vector2);

            var norm = uuDot.Sqrt();

            return norm.IsZero()
                ? 0d
                : (uvDot / norm).Clamp(-1, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetAngleCosWithUnit(this Float64Vector vector1, Float64Vector vector2)
        {
            Debug.Assert(
                vector2.IsNearUnit()
            );

            var uuDot = vector1.ENormSquared();
            var uvDot = vector1.ESp(vector2);

            var norm = uuDot.Sqrt();

            return norm.IsZero()
                ? 0d
                : (uvDot / norm).Clamp(-1, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetUnitVectorsAngleCos(this Float64Vector vector1, Float64Vector vector2)
        {
            return vector1.ESp(vector2).Clamp(-1, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle GetAngle(this Float64Vector vector1, Float64Vector vector2)
        {
            return vector1.GetAngleCos(vector2).ArcCos();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle GetAngleWithUnit(this Float64Vector vector1, ILinSignedBasisVector vector2)
        {
            return vector1.GetAngleCosWithUnit(vector2).ArcCos();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle GetAngleWithUnit(this Float64Vector vector1, Float64Vector vector2)
        {
            return vector1.GetAngleCosWithUnit(vector2).ArcCos();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle GetUnitVectorsAngle(this Float64Vector vector1, Float64Vector vector2)
        {
            return vector1.GetUnitVectorsAngleCos(vector2).ArcCos();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector ProjectOnUnitVector(this Float64Vector vector1, Float64Vector vector2)
        {
            Debug.Assert(
                vector2.IsNearUnit()
            );

            return vector2.Times(vector1.ESp(vector2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector ProjectOnVector(this Float64Vector vector1, Float64Vector vector2)
        {
            var uuDot = vector1.ENormSquared();
            var xuDot = vector1.ESp(vector2);

            return vector2.Times(xuDot / uuDot);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector RejectOnUnitVector(this Float64Vector vector1, Float64Vector vector2)
        {
            Debug.Assert(
                vector2.IsNearUnit()
            );

            return vector1.Subtract(
                vector2.Times(
                    vector1.ESp(vector2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector RejectOnVector(this Float64Vector vector1, Float64Vector vector2)
        {
            var uuDot = vector1.ENormSquared();
            var xuDot = vector1.ESp(vector2);

            return vector1.Subtract(
                vector2.Times(xuDot / uuDot)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector ReflectOnUnitVector(this Float64Vector vector1, Float64Vector vector2)
        {
            Debug.Assert(
                vector1.IsNearUnit()
            );

            return vector1.Times(
                2d * vector1.ESp(vector2)
            ).Subtract(vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector ReflectOnUnitNormalHyperPlane(this Float64Vector vector1, Float64Vector unitNormal)
        {
            Debug.Assert(
                unitNormal.IsNearUnit()
            );

            return vector1.Subtract(
                unitNormal.Times(
                    2d * vector1.ESp(unitNormal)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector RotateToUnitVector(this Float64Vector vector1, Float64Vector unitVector, Float64PlanarAngle angle)
        {
            Debug.Assert(
                vector1.IsNearUnit() &&
                unitVector.IsNearUnit()
            );

            // Create a unit normal to u in the u-v rotational plane
            var v1 = unitVector.Subtract(vector1.Times(unitVector.VectorDot(vector1)));
            var v1Length = v1.ENorm();

            Debug.Assert(
                !v1Length.IsNearZero()
            );

            // Compute a rotated version of v in the u-v rotational plane by the given angle
            return vector1
                .Times(angle.Cos())
                .Add(v1.Times(angle.Sin() / v1Length));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector RotateFromUnitVector(this Float64Vector vector1, Float64Vector unitVector, Float64PlanarAngle angle)
        {
            Debug.Assert(
                unitVector.IsNearUnit() &&
                vector1.IsNearUnit()
            );

            // Create a unit normal to u in the u-v rotational plane
            var v1 = vector1.Subtract(unitVector.Times(vector1.VectorDot(unitVector)));
            var v1Length = v1.ENorm();

            Debug.Assert(
                !v1Length.IsNearZero()
            );

            // Compute a rotated version of v in the u-v rotational plane by the given angle
            return unitVector
                .Times(angle.Cos())
                .Add(v1.Times(angle.Sin() / v1Length));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D ToTuple2D(this Float64Vector vector)
        {
            return Float64Vector2D.Create((Float64Scalar)vector.X,
                (Float64Scalar)vector.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D ToTuple3D(this Float64Vector vector)
        {
            return Float64Vector3D.Create(vector.X,
                vector.Y,
                vector.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion ToTuple4D(this Float64Vector vector)
        {
            return Float64Quaternion.Create(vector.X,
                vector.Y,
                vector.Z,
                vector.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ToColorRgba(this Float64Vector vector)
        {
            Debug.Assert(
                vector[0] >= 0d && vector[0] <= 1d &&
                vector[1] >= 0d && vector[1] <= 1d &&
                vector[2] >= 0d && vector[2] <= 1d &&
                vector[3] >= 0d && vector[3] <= 1d
            );

            return Color.FromRgba(
                (byte)(vector[0] * 255),
                (byte)(vector[1] * 255),
                (byte)(vector[2] * 255),
                (byte)(vector[3] * 255)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ToColorRgb(this Float64Vector vector)
        {
            Debug.Assert(
                vector[0] >= 0d && vector[0] <= 1d &&
                vector[1] >= 0d && vector[1] <= 1d &&
                vector[2] >= 0d && vector[2] <= 1d
            );

            return Color.FromRgb(
                (byte)(vector[0] * 255),
                (byte)(vector[1] * 255),
                (byte)(vector[2] * 255)
            );
        }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector GetFloat64Tuple(this System.Random random, int dimensions)
    {
        var vector =
            MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(
                dimensions,
                _ => random.NextDouble()
            );

        return Float64Vector.Create(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector GetFloat64Tuple(this System.Random random, int dimensions, double minValue, double maxValue)
    {
        var vector =
            MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(
                dimensions,
                _ => random.NextDouble(minValue, maxValue)
            );

        return Float64Vector.Create(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector GetSparseFloat64Tuple(this System.Random random, int dimensions)
    {
        var vector =
            MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Sparse(dimensions);

        var count = random.Next(1, dimensions);
        var indexList = random.GetUniqueIndices(count, dimensions);

        foreach (var index in indexList)
            vector[index] = random.NextDouble();

        return Float64Vector.Create(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector GetSparseFloat64Tuple(this System.Random random, int dimensions, int count)
    {
        if (count > dimensions)
            count = dimensions;

        var vector =
            MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Sparse(dimensions);

        var indexList = random.GetUniqueIndices(count, dimensions);

        foreach (var index in indexList)
            vector[index] = random.NextDouble();

        return Float64Vector.Create(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector GetSparseFloat64Tuple(this System.Random random, int dimensions, double minValue, double maxValue)
    {
        var vector =
            MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Sparse(dimensions);

        var count = random.Next(1, dimensions);
        var indexList = random.GetUniqueIndices(count, dimensions);

        foreach (var index in indexList)
            vector[index] = random.NextDouble(minValue, maxValue);

        return Float64Vector.Create(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector CreateVector(this double[] itemArray, bool normalize = false)
    {
        if (normalize)
            itemArray.VectorNormalizeInPlace();

        return Float64Vector.Create(itemArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<bool, double, LinSignedBasisVector> TryToAxis(this Float64Vector vector)
    {
        if (vector.Count != 1)
            return new Tuple<bool, double, LinSignedBasisVector>(
                false,
                0d,
                LinSignedBasisVector.PositiveX
            );

        var (basisIndex, scalar) = vector.First();

        return new Tuple<bool, double, LinSignedBasisVector>(
            true,
            scalar.Abs(),
            new LinSignedBasisVector(basisIndex, scalar < 0)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<bool, double, LinSignedBasisVector> TryToNearAxis(this Float64Vector vector, double epsilon = 1e-12)
    {
        return vector
            .GetCopyByScalar(s => !s.IsNearZero(epsilon))
            .TryToAxis();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<bool, double, LinSignedBasisVector> TryVectorToAxis(this IReadOnlyDictionary<int, double> itemArray)
    {
        if (itemArray.Count != 1)
            return new Tuple<bool, double, LinSignedBasisVector>(
                false,
                0d,
                LinSignedBasisVector.PositiveX
            );

        var (basisIndex, scalar) = itemArray.First();

        return new Tuple<bool, double, LinSignedBasisVector>(
            true,
            scalar.Abs(),
            new LinSignedBasisVector(basisIndex, scalar < 0)
        );
    }

    public static Tuple<bool, double, LinSignedBasisVector> TryVectorToAxis(this IReadOnlyList<double> itemArray)
    {
        var dimensions = itemArray.Count;

        // Find if the given scaling vector is parallel to a basis vector
        var basisIndex = -1;
        for (var i = 0; i < dimensions; i++)
        {
            if (itemArray[i].IsZero()) continue;

            if (basisIndex >= 0)
            {
                basisIndex = -2;
                break;
            }

            basisIndex = i;
        }

        if (basisIndex < 0)
            return new Tuple<bool, double, LinSignedBasisVector>(
                false,
                0d,
                LinSignedBasisVector.PositiveX
            );

        var scalar = itemArray[basisIndex];

        return new Tuple<bool, double, LinSignedBasisVector>(
            true,
            scalar.Abs(),
            new LinSignedBasisVector(basisIndex, scalar < 0)
        );
    }

    public static Tuple<bool, double, LinSignedBasisVector> TryVectorToNearAxis(this double[] itemArray, double epsilon = 1e-12)
    {
        var dimensions = itemArray.Length;

        // Find if the given scaling vector is parallel to a basis vector
        var basisIndex = -1;
        for (var i = 0; i < dimensions; i++)
        {
            if (itemArray[i].IsNearZero(epsilon)) continue;

            if (basisIndex >= 0)
            {
                basisIndex = -2;
                break;
            }

            basisIndex = i;
        }

        if (basisIndex < 0)
            return new Tuple<bool, double, LinSignedBasisVector>(
                false,
                0d,
                LinSignedBasisVector.PositiveX
            );

        var scalar = itemArray[basisIndex];
        return new Tuple<bool, double, LinSignedBasisVector>(
            true,
            scalar.Abs(),
            new LinSignedBasisVector(basisIndex, scalar < 0)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector Lerp(this double t, Float64Vector v1, Float64Vector v2)
    {
        Debug.Assert(
            v1.IsValid() &&
            v2.IsValid() && t.IsValid()
        );

        return (1.0d - t) * v1 + t * v2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> GetTupleItemPair(this IReadOnlyList<Float64Vector> itemArray, int index, int itemIndex)
    {
        return new Pair<double>(
            itemArray[index][itemIndex],
            itemArray[index + 1][itemIndex]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<double> GetTupleItemTriplet(this IReadOnlyList<Float64Vector> itemArray, int index, int itemIndex)
    {
        return new Triplet<double>(
            itemArray[index][itemIndex],
            itemArray[index + 1][itemIndex],
            itemArray[index + 2][itemIndex]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<double> GetTupleItemQuad(this IReadOnlyList<Float64Vector> itemArray, int index, int itemIndex)
    {
        return new Quad<double>(
            itemArray[index][itemIndex],
            itemArray[index + 1][itemIndex],
            itemArray[index + 2][itemIndex],
            itemArray[index + 3][itemIndex]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<double> GetTupleItemQuint(this IReadOnlyList<Float64Vector> itemArray, int index, int itemIndex)
    {
        return new Quint<double>(
            itemArray[index][itemIndex],
            itemArray[index + 1][itemIndex],
            itemArray[index + 2][itemIndex],
            itemArray[index + 3][itemIndex],
            itemArray[index + 4][itemIndex]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<double> GetTupleItemHexad(this IReadOnlyList<Float64Vector> itemArray, int index, int itemIndex)
    {
        return new Hexad<double>(
            itemArray[index][itemIndex],
            itemArray[index + 1][itemIndex],
            itemArray[index + 2][itemIndex],
            itemArray[index + 3][itemIndex],
            itemArray[index + 4][itemIndex],
            itemArray[index + 5][itemIndex]
        );
    }

    }
}
