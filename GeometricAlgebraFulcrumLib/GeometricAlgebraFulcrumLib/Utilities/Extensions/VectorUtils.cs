using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class VectorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] GetArray<T>(this GaVector<T> vector)
        {
            return vector.VectorStorage.GetLinVectorIndexScalarStorage().ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64DenseTuple GetDenseTuple(this GaVector<double> vector)
        {
            return new Float64DenseTuple(
                vector.VectorStorage.GetLinVectorIndexScalarStorage().ToArray()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> GetEuclideanAngle<T>(this GaVector<T> vector1, GaVector<T> vector2, bool assumeUnitVectors = false)
        {
            var processor = vector1.GeometricProcessor;

            var angle = processor.GetEuclideanAngle(
                vector1.VectorStorage, 
                vector2.VectorStorage,
                assumeUnitVectors
            );

            return processor.CreateScalar(angle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> GetUnitBisector<T>(this GaVector<T> vector1, GaVector<T> vector2, bool assumeEqualNormVectors = false)
        {
            var processor = vector1.GeometricProcessor;

            var unitBisector = processor.GetUnitBisector(
                vector1.VectorStorage, 
                vector2.VectorStorage,
                assumeEqualNormVectors
            );

            return new GaVector<T>(processor, unitBisector);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> OmMapUsing<T>(this GaVector<T> vector, IOutermorphism<T> om)
        {
            return om.OmMap(vector);
        }

                
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static IEnumerable<Vector<T>> OmMap<T>(this IOutermorphism<T> om, params Vector<T>[] vectorsList)
        //{
        //    return vectorsList.Select(v => v.OmMapUsing(om));
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaVector<T>> OmMap<T>(this IOutermorphism<T> om, IEnumerable<GaVector<T>> vectorsList)
        {
            return vectorsList.Select(v => v.OmMapUsing(om));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaVector<T>> OmMapUsing<T>(this IEnumerable<GaVector<T>> vectorsList, IOutermorphism<T> om)
        {
            return vectorsList.Select(v => v.OmMapUsing(om));
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector<T> Project<T>(this ISubspace<T> subspace, Vector<T> vector)
        //{
        //    var processor = subspace.GeometricProcessor;

        //    return new Vector<T>(
        //        processor,
        //        subspace.Project(vector)
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector<T> ProjectOn<T>(this Vector<T> vector, ISubspace<T> subspace)
        //{
        //    var processor = subspace.GeometricProcessor;

        //    return new Vector<T>(
        //        processor,
        //        subspace.Project(vector.VectorStorage)
        //    );
        //}


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaVector<T>> Project<T>(this ISubspace<T> subspace, params GaVector<T>[] vectorsList)
        {
            return vectorsList.Select(subspace.Project);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaVector<T>> Project<T>(this ISubspace<T> subspace, IEnumerable<GaVector<T>> vectorsList)
        {
            return vectorsList.Select(subspace.Project);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> ProjectOn<T>(this GaVector<T> vector, ISubspace<T> subspace)
        {
            return subspace.Project(vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaVector<T>> ProjectOn<T>(this IEnumerable<GaVector<T>> vectorsList, ISubspace<T> subspace)
        {
            return vectorsList.Select(subspace.Project);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureRotor<T> GetEuclideanRotorFromBasis<T>(this GaVector<T> vector2, ulong index)
        {
            var processor = (IGeometricAlgebraEuclideanProcessor<T>) vector2.GeometricProcessor;

            return processor.CreatePureRotor(
                processor.CreateVectorBasis(index),
                vector2
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureRotor<T> GetEuclideanRotorFrom<T>(this GaVector<T> vector2, GaVector<T> vector1)
        {
            var processor = (IGeometricAlgebraEuclideanProcessor<T>) vector2.GeometricProcessor;

            return processor.CreatePureRotor(vector1, vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureRotor<T> GetEuclideanRotorFrom<T>(this GaVector<T> vector2, GaVector<T> vector1, bool assumeUnitVectors)
        {
            var processor = (IGeometricAlgebraEuclideanProcessor<T>) vector2.GeometricProcessor;

            return processor.CreatePureRotor(
                vector1,
                vector2,
                assumeUnitVectors
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureRotor<T> GetEuclideanRotorToBasis<T>(this GaVector<T> vector1, ulong index)
        {
            var processor = (IGeometricAlgebraEuclideanProcessor<T>) vector1.GeometricProcessor;

            return processor.CreatePureRotor(
                vector1,
                processor.CreateVectorBasis(index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureRotor<T> GetEuclideanRotorTo<T>(this GaVector<T> vector1, GaVector<T> vector2)
        {
            var processor = (IGeometricAlgebraEuclideanProcessor<T>) vector1.GeometricProcessor;

            return processor.CreatePureRotor(vector1, vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureRotor<T> GetEuclideanRotorTo<T>(this GaVector<T> vector1, GaVector<T> vector2, bool assumeUnitVectors)
        {
            var processor = (IGeometricAlgebraEuclideanProcessor<T>) vector1.GeometricProcessor;

            return processor.CreatePureRotor(
                vector1,
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
        public static PureRotor<T> GetEuclideanRotorTo<T>(this GaVector<T> vector1, Subspace<T> subspace)
        {
            var processor = (IGeometricAlgebraEuclideanProcessor<T>) vector1.GeometricProcessor;

            var vector2 = subspace.Project(vector1);

            return processor.CreatePureRotor(
                vector1,
                vector2
            );
        }
    }
}