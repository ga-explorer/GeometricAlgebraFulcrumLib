using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Dictionary;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64
{
    public static class LinFloat64VectorUtils
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


        public static double[] ToArray(this LinFloat64Vector vector, int vSpaceDimensions)
        {
            if (vSpaceDimensions < vector.VSpaceDimensions)
                throw new ArgumentException(nameof(vSpaceDimensions));

            var array = new double[vSpaceDimensions];

            foreach (var (index, scalar) in vector.IndexScalarPairs)
                array[index] = scalar;

            return array;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ENorm(this LinFloat64Vector mv)
        {
            return mv.ENormSquared().Sqrt();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector EInverse(this LinFloat64Vector mv1)
        {
            return mv1.Divide(
                mv1.ESp(mv1)
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector MapScalars(this LinFloat64Vector mv, Func<double, double> scalarMapping)
        {
            var termList =
                mv.IndexScalarPairs.Select(
                    term => new KeyValuePair<int, double>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return new LinFloat64VectorComposer()
                .SetTerms(termList)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector MapScalars(this LinFloat64Vector mv, Func<int, double, double> scalarMapping)
        {
            var termList =
                mv.IndexScalarPairs.Select(
                    term => new KeyValuePair<int, double>(
                        term.Key,
                        scalarMapping(term.Key, term.Value)
                    )
                );

            return new LinFloat64VectorComposer()
                .AddTerms(termList)
                .GetVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector MapBasisVectors(this LinFloat64Vector mv, Func<int, int> basisMapping, bool simplify = true)
        {
            var termList =
                mv.IndexScalarPairs.Select(
                    term => new KeyValuePair<int, double>(
                        basisMapping(term.Key),
                        term.Value
                    )
                );

            return new LinFloat64VectorComposer()
                .AddTerms(termList)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector MapBasisVectors(this LinFloat64Vector mv, Func<int, double, int> basisMapping, bool simplify = true)
        {
            var termList =
                mv.IndexScalarPairs.Select(
                    term => new KeyValuePair<int, double>(
                        basisMapping(term.Key, term.Value),
                        term.Value
                    )
                );

            return new LinFloat64VectorComposer()
                .AddTerms(termList)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector MapTerms(this LinFloat64Vector mv, Func<int, double, KeyValuePair<int, double>> termMapping, bool simplify = true)
        {
            var termList =
                mv.IndexScalarPairs.Select(
                    term => termMapping(term.Key, term.Value)
                );

            return new LinFloat64VectorComposer()
                .AddTerms(termList)
                .GetVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetEuclideanAngle(this LinFloat64Vector v1, LinFloat64Vector v2, bool assumeUnitVectors = false)
        {
            var v12Sp = v1.ESp(v2);

            var angle = assumeUnitVectors
                ? v12Sp
                : v12Sp / (v1.ENormSquared() * v2.ENormSquared()).Sqrt();

            return angle.ArcCos();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector GetBisector(this LinFloat64Vector v1, LinFloat64Vector v2, bool assumeEqualNormVectors = false)
        {
            return (v1 + v2).Times(05d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector GetUnitBisector(this LinFloat64Vector v1, LinFloat64Vector v2, bool assumeEqualNormVectors = false)
        {
            var bisector = assumeEqualNormVectors
                ? v1 + v2
                : v1.DivideByENorm() + v2.DivideByENorm();
            
            return bisector.DivideByENorm();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetDistanceSquaredToPoint(this LinFloat64Vector point1, LinFloat64Vector point2)
        {
            return point1.Subtract(point2).ENormSquared();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetDistanceToPoint(this LinFloat64Vector point1, LinFloat64Vector point2)
        {
            return point1.Subtract(point2).ENormSquared().Sqrt();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector ToUnitVector(this LinFloat64Vector vector)
        {
            var length = vector.ENorm();

            return length.IsZero()
                ? new LinFloat64Vector()
                : vector.Times(1d / length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorDot(this LinFloat64Vector vector1, LinFloat64Vector vector2)
        {
            return vector1.ESp(vector2);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorDot(this LinFloat64Vector vector1, LinSignedBasisVector vector2)
        {
            return vector1.GetTermScalar(vector2.Index) * vector2.Sign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetAngleCos(this LinFloat64Vector vector1, LinFloat64Vector vector2)
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
        public static double GetAngleCosWithUnit(this LinFloat64Vector vector1, LinFloat64Vector vector2)
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
        public static Float64PlanarAngle GetAngle(this LinFloat64Vector vector1, LinFloat64Vector vector2)
        {
            return vector1.GetAngleCos(vector2).ArcCos();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector ProjectOnUnitVector(this LinFloat64Vector vector1, LinFloat64Vector vector2)
        {
            Debug.Assert(
                vector2.IsNearUnit()
            );

            return vector2.Times(vector1.ESp(vector2));
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector ProjectOnVector(this LinFloat64Vector vector1, LinFloat64Vector vector2)
        {
            var uuDot = vector1.ENormSquared();
            var xuDot = vector1.ESp(vector2);

            return vector2.Times(xuDot / uuDot);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector RejectOnUnitVector(this LinFloat64Vector vector1, LinFloat64Vector vector2)
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
        public static LinFloat64Vector RejectOnVector(this LinFloat64Vector vector1, LinFloat64Vector vector2)
        {
            var uuDot = vector1.ENormSquared();
            var xuDot = vector1.ESp(vector2);

            return vector1.Subtract(
                vector2.Times(xuDot / uuDot)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector ReflectOnUnitVector(this LinFloat64Vector vector1, LinFloat64Vector vector2)
        {
            Debug.Assert(
                vector1.IsNearUnit()
            );

            return vector1.Times(
                2d * vector1.ESp(vector2)
            ).Subtract(vector2);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector ReflectOnUnitNormalHyperPlane(this LinFloat64Vector vector1, LinFloat64Vector unitNormal)
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
        public static LinFloat64Vector RotateToUnitVector(this LinFloat64Vector vector1, LinFloat64Vector unitVector, Float64PlanarAngle angle)
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
        public static LinFloat64Vector RotateFromUnitVector(this LinFloat64Vector vector1, LinFloat64Vector unitVector, Float64PlanarAngle angle)
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
        public static Float64Tuple2D ToTuple2D(this LinFloat64Vector vector)
        {
            return new Float64Tuple2D(
                vector.X, 
                vector.Y
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ToTuple3D(this LinFloat64Vector vector)
        {
            return new Float64Tuple3D(
                vector.X, 
                vector.Y, 
                vector.Z
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D ToTuple4D(this LinFloat64Vector vector)
        {
            return new Float64Tuple4D(
                vector.X, 
                vector.Y, 
                vector.Z,
                vector.W
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ToColorRgba(this LinFloat64Vector vector)
        {
            Debug.Assert(
                vector[0] >= 0d && vector[0] <= 1d &&
                vector[1] >= 0d && vector[1] <= 1d &&
                vector[2] >= 0d && vector[2] <= 1d &&
                vector[3] >= 0d && vector[3] <= 1d
            );

            return Color.FromRgba(
                (byte) (vector[0] * 255),
                (byte) (vector[1] * 255),
                (byte) (vector[2] * 255),
                (byte) (vector[3] * 255)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ToColorRgb(this LinFloat64Vector vector)
        {
            Debug.Assert(
                vector[0] >= 0d && vector[0] <= 1d &&
                vector[1] >= 0d && vector[1] <= 1d &&
                vector[2] >= 0d && vector[2] <= 1d
            );

            return Color.FromRgb(
                (byte) (vector[0] * 255),
                (byte) (vector[1] * 255),
                (byte) (vector[2] * 255)
            );
        }

    }
}
