using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Subspaces;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors
{
    public static class RGaVectorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] VectorToArray1D<T>(this RGaVector<T> vector, int vSpaceDimensions)
        {
            if (vSpaceDimensions < vector.VSpaceDimensions)
                throw new InvalidOperationException();

            var array = vector
                .ScalarProcessor
                .CreateArrayZero1D(vSpaceDimensions);

            foreach (var (id, scalar) in vector.IdScalarPairs)
                array[id.FirstOneBitPosition()] = scalar;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] VectorToArray1D<T>(this RGaVector<T> vector)
        {
            var array = vector
                .ScalarProcessor
                .CreateArrayZero1D(vector.VSpaceDimensions);

            foreach (var (id, scalar) in vector.IdScalarPairs)
                array[id.FirstOneBitPosition()] = scalar;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D GetTuple3D(this RGaVector<double> vector)
        {
            return Float64Vector3D.Create(
                vector[0],
                vector[1],
                vector[2]
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector GetTuple(this RGaVector<double> vector)
        {
            return Float64Vector.Create(
                vector.VectorToArray1D()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> GetEuclideanAngle<T>(this RGaVector<T> vector1, RGaVector<T> vector2, bool assumeUnitVectors = false)
        {
            var angle = vector1.ESp(vector2).Scalar();

            if (!assumeUnitVectors)
                angle /= (vector1.ENorm() * vector2.ENorm()).Scalar();

            return angle.ArcCos();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVector<T> GetUnitBisector<T>(this RGaVector<T> vector1, RGaVector<T> vector2, bool assumeEqualNormVectors = false)
        {
            var v = assumeEqualNormVectors
                ? vector1 + vector2
                : vector1.DivideByENorm() + vector2.DivideByENorm();

            return v.DivideByENorm();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVector<T> OmMapUsing<T>(this RGaVector<T> vector, IRGaOutermorphism<T> om)
        {
            return om.OmMap(vector);
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static IEnumerable<Vector<T>> OmMap<T>(this IOutermorphism<T> om, params Vector<T>[] vectorsList)
        //{
        //    return vectorsList.Select(v => v.OmMapUsing(om));
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaVector<T>> OmMap<T>(this IRGaOutermorphism<T> om, IEnumerable<RGaVector<T>> vectorsList)
        {
            return vectorsList.Select(v => v.OmMapUsing(om));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaVector<T>> OmMapUsing<T>(this IEnumerable<RGaVector<T>> vectorsList, IRGaOutermorphism<T> om)
        {
            return vectorsList.Select(v => v.OmMapUsing(om));
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector<T> Project<T>(this IRGaSubspace<T> subspace, Vector<T> vector)
        //{
        //    var processor = subspace.GeometricProcessor;

        //    return new Vector<T>(
        //        processor,
        //        subspace.Project(vector)
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector<T> ProjectOn<T>(this Vector<T> vector, IRGaSubspace<T> subspace)
        //{
        //    var processor = subspace.GeometricProcessor;

        //    return new Vector<T>(
        //        processor,
        //        subspace.Project(vector.VectorStorage)
        //    );
        //}


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaVector<T>> ProjectVectors<T>(this IRGaSubspace<T> subspace, params RGaVector<T>[] vectorsList)
        {
            return vectorsList.Select(subspace.Project);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaVector<T>> Project<T>(this IRGaSubspace<T> subspace, IEnumerable<RGaVector<T>> vectorsList)
        {
            return vectorsList.Select(subspace.Project);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVector<T> ProjectOnVector<T>(this RGaVector<T> vector, RGaVector<T> subspace)
        {
            return vector.ProjectOn(subspace.ToSubspace());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVector<T> ProjectOnBivector<T>(this RGaVector<T> vector, RGaBivector<T> subspace)
        {
            return vector.ProjectOn(subspace.ToSubspace());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVector<T> ProjectOnKVector<T>(this RGaVector<T> vector, RGaKVector<T> subspace)
        {
            return vector.ProjectOn(subspace.ToSubspace());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVector<T> ProjectOn<T>(this RGaVector<T> vector, IRGaSubspace<T> subspace)
        {
            return subspace.Project(vector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVector<T> RejectOnVector<T>(this RGaVector<T> vector, RGaVector<T> subspace)
        {
            return vector - vector.ProjectOn(subspace.ToSubspace());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVector<T> RejectOnBivector<T>(this RGaVector<T> vector, RGaBivector<T> subspace)
        {
            return vector - vector.ProjectOn(subspace.ToSubspace());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVector<T> RejectOnKVector<T>(this RGaVector<T> vector, RGaKVector<T> subspace)
        {
            return vector - vector.ProjectOn(subspace.ToSubspace());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVector<T> RejectOn<T>(this RGaVector<T> vector, IRGaSubspace<T> subspace)
        {
            return vector - subspace.Project(vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaVector<T>> ProjectOn<T>(this IEnumerable<RGaVector<T>> vectorsList, IRGaSubspace<T> subspace)
        {
            return vectorsList.Select(subspace.Project);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaPureRotor<T> GetEuclideanRotorFromBasis<T>(this RGaVector<T> vector2, int index)
        {
            var metric = vector2.Processor;

            return metric
                .CreateTermVector(index)
                .CreatePureRotor(vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaPureRotor<T> GetEuclideanRotorFrom<T>(this RGaVector<T> vector2, RGaVector<T> vector1)
        {
            return vector1.CreatePureRotor(vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaPureRotor<T> GetEuclideanRotorFrom<T>(this RGaVector<T> vector2, RGaVector<T> vector1, bool assumeUnitVectors)
        {
            return vector1.CreatePureRotor(
                vector2,
                assumeUnitVectors
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaPureRotor<T> GetEuclideanRotorToBasis<T>(this RGaVector<T> vector1, int index)
        {
            var metric = vector1.Processor;

            return vector1.CreatePureRotor(
                metric.CreateTermVector(index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaPureRotor<T> GetEuclideanRotorTo<T>(this RGaVector<T> vector1, RGaVector<T> vector2)
        {
            return vector1.CreatePureRotor(vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaPureRotor<T> GetEuclideanRotorTo<T>(this RGaVector<T> vector1, RGaVector<T> vector2, bool assumeUnitVectors)
        {
            return vector1.CreatePureRotor(
                vector2,
                assumeUnitVectors
            );
        }

        /// <summary>
        /// Find a Euclidean rotor from vector1 to its projection on subspace
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vector1"></param>
        /// <param name="subspace"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaPureRotor<T> GetEuclideanRotorTo<T>(this RGaVector<T> vector1, RGaSubspace<T> subspace)
        {
            return vector1.CreatePureRotor(
                subspace.Project(vector1)
            );
        }
    }
}
