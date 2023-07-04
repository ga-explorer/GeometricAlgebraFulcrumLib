using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Subspaces;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors
{
    public static class RGaFloat64VectorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[] VectorToArray1D(this RGaFloat64Vector vector, int vSpaceDimensions)
        {
            if (vSpaceDimensions < vector.VSpaceDimensions)
                throw new InvalidOperationException();

            var array = new double[vSpaceDimensions];

            foreach (var (id, scalar) in vector.IdScalarPairs)
                array[id.FirstOneBitPosition()] = scalar;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[] VectorToArray1D(this RGaFloat64Vector vector)
        {
            var array = new double[vector.VSpaceDimensions];

            foreach (var (id, scalar) in vector.IdScalarPairs)
                array[id.FirstOneBitPosition()] = scalar;

            return array;
        }
        

        public static RGaIdScalarRecord GetMinScalarMagnitudeIdScalar(this RGaFloat64Multivector mv)
        {
            if (mv.IsZero)
                throw new InvalidOperationException();

            var (minValueId, minValue) = mv.IdScalarPairs.First();
            minValue = minValue.Abs();

            foreach (var (id, scalar) in mv.IdScalarPairs)
            {
                var absNumber = scalar.Abs();

                if (absNumber >= minValue) continue;

                minValue = absNumber;
                minValueId = id;
            }

            return new RGaIdScalarRecord(minValueId, minValue);
        }

        public static RGaFloat64Multivector GetUnitNormalVector(this RGaFloat64Vector vector)
        {
            var metric = vector.Processor;

            var (minValueId, minValue) =
                vector.GetMinScalarMagnitudeIdScalar();

            var composer = vector.ToComposer();

            var sum = 0d;
            foreach (var (id, scalar) in vector.IdScalarPairs)
            {
                if (id == minValueId) continue;

                var signature = metric.Signature(id);

                if (signature.IsPositive)
                {
                    sum += scalar;
                    composer.SetTerm(id, minValue);
                }
                else if (signature.IsNegative)
                {
                    sum -= scalar;
                    composer.SetTerm(id, -minValue);
                }
            }

            composer.SetTerm(minValueId, -sum);
            composer.Divide(composer.Norm().ScalarValue);

            return composer.GetVector();
        }

        public static RGaFloat64Vector GetEUnitNormalVector(this RGaFloat64Vector vector)
        {
            var (minValueId, minValue) =
                vector.GetMinScalarMagnitudeIdScalar();

            var composer = vector.ToComposer();

            var sum = 0d;
            foreach (var (id, scalar) in vector.IdScalarPairs)
            {
                if (id == minValueId) continue;

                sum += scalar;
                composer.SetTerm(id, minValue);
            }

            composer.SetTerm(minValueId, -sum);
            composer.Divide(composer.Norm().ScalarValue);

            return composer.GetVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D GetTuple2D(this RGaFloat64Vector vector)
        {
            return Float64Vector2D.Create((Float64Scalar)vector[0],
                (Float64Scalar)vector[1]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D GetTuple3D(this RGaFloat64Vector vector)
        {
            return Float64Vector3D.Create(vector[0],
                vector[1],
                vector[2]);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetEuclideanAngle(this RGaFloat64Vector vector1, RGaFloat64Vector vector2, bool assumeUnitVectors = false)
        {
            var angle = vector1.ESp(vector2).Scalar;

            if (!assumeUnitVectors)
                angle /= (vector1.ENorm() * vector2.ENorm()).Scalar;

            return angle.ArcCos();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector GetUnitBisector(this RGaFloat64Vector vector1, RGaFloat64Vector vector2, bool assumeEqualNormVectors = false)
        {
            var v = assumeEqualNormVectors
                ? vector1 + vector2
                : vector1.DivideByENorm() + vector2.DivideByENorm();

            return v.DivideByENorm();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector OmMapUsing(this RGaFloat64Vector vector, IRGaFloat64Outermorphism om)
        {
            return om.OmMap(vector);
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static IEnumerable<Vector> OmMap(this IOutermorphism om, params Vector[] vectorsList)
        //{
        //    return vectorsList.Select(v => v.OmMapUsing(om));
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaFloat64Vector> OmMap(this IRGaFloat64Outermorphism om, IEnumerable<RGaFloat64Vector> vectorsList)
        {
            return vectorsList.Select(v => v.OmMapUsing(om));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaFloat64Vector> OmMapUsing(this IEnumerable<RGaFloat64Vector> vectorsList, IRGaFloat64Outermorphism om)
        {
            return vectorsList.Select(v => v.OmMapUsing(om));
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector Project(this IRGaSubspace subspace, Vector vector)
        //{
        //    var processor = subspace.GeometricProcessor;

        //    return new Vector(
        //        processor,
        //        subspace.Project(vector)
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector ProjectOn(this Vector vector, IRGaSubspace subspace)
        //{
        //    var processor = subspace.GeometricProcessor;

        //    return new Vector(
        //        processor,
        //        subspace.Project(vector.VectorStorage)
        //    );
        //}


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaFloat64Vector> ProjectVectors(this IRGaFloat64Subspace subspace, params RGaFloat64Vector[] vectorsList)
        {
            return vectorsList.Select(subspace.Project);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaFloat64Vector> Project(this IRGaFloat64Subspace subspace, IEnumerable<RGaFloat64Vector> vectorsList)
        {
            return vectorsList.Select(subspace.Project);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector ProjectOnVector(this RGaFloat64Vector vector, RGaFloat64Vector subspace)
        {
            return vector.ProjectOn(subspace.ToSubspace());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector ProjectOnBivector(this RGaFloat64Vector vector, RGaFloat64Bivector subspace)
        {
            return vector.ProjectOn(subspace.ToSubspace());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector ProjectOnKVector(this RGaFloat64Vector vector, RGaFloat64KVector subspace)
        {
            return vector.ProjectOn(subspace.ToSubspace());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector ProjectOn(this RGaFloat64Vector vector, IRGaFloat64Subspace subspace)
        {
            return subspace.Project(vector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector RejectOnVector(this RGaFloat64Vector vector, RGaFloat64Vector subspace)
        {
            return vector - vector.ProjectOn(subspace.ToSubspace());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector RejectOnBivector(this RGaFloat64Vector vector, RGaFloat64Bivector subspace)
        {
            return vector - vector.ProjectOn(subspace.ToSubspace());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector RejectOnKVector(this RGaFloat64Vector vector, RGaFloat64KVector subspace)
        {
            return vector - vector.ProjectOn(subspace.ToSubspace());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector RejectOn(this RGaFloat64Vector vector, IRGaFloat64Subspace subspace)
        {
            return vector - subspace.Project(vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaFloat64Vector> ProjectOn(this IEnumerable<RGaFloat64Vector> vectorsList, IRGaFloat64Subspace subspace)
        {
            return vectorsList.Select(subspace.Project);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64PureRotor GetEuclideanRotorFromBasis(this RGaFloat64Vector vector2, int index)
        {
            var metric = vector2.Processor;
            
            return metric
                .CreateVector(index)
                .CreatePureRotor(vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64PureRotor GetEuclideanRotorFrom(this RGaFloat64Vector vector2, RGaFloat64Vector vector1)
        {
            return vector1.CreatePureRotor(vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64PureRotor GetEuclideanRotorFrom(this RGaFloat64Vector vector2, RGaFloat64Vector vector1, bool assumeUnitVectors)
        {
            return vector1.CreatePureRotor(
                vector2,
                assumeUnitVectors
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64PureRotor GetEuclideanRotorToBasis(this RGaFloat64Vector vector1, int index)
        {
            var metric = vector1.Processor;
            
            return vector1.CreatePureRotor(
                metric.CreateVector(index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64PureRotor GetEuclideanRotorTo(this RGaFloat64Vector vector1, RGaFloat64Vector vector2)
        {
            return vector1.CreatePureRotor(vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64PureRotor GetEuclideanRotorTo(this RGaFloat64Vector vector1, RGaFloat64Vector vector2, bool assumeUnitVectors)
        {
            return vector1.CreatePureRotor(
                vector2,
                assumeUnitVectors
            );
        }

        /// <summary>
        /// Find a Euclidean rotor from vector1 to its projection on subspace
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="subspace"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64PureRotor GetEuclideanRotorTo(this RGaFloat64Vector vector1, RGaFloat64Subspace subspace)
        {
            return vector1.CreatePureRotor(
                subspace.Project(vector1)
            );
        }
    }
}
