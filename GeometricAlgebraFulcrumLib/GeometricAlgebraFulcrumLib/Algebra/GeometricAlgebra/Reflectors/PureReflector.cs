using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Reflectors
{
    /// <summary>
    /// A pure (direct) reflector is a single vector.
    /// The reflection happens in the unit vector itself, not its dual hyperplane
    /// like the case for pure versors.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class PureReflector<T>
        : ReflectorBase<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static PureReflector<T> Create(Vector<T> vector)
        {
            return new PureReflector<T>(vector);
        }
        

        public Vector<T> Vector { get; }

        public Vector<T> VectorInverse { get; }


        private PureReflector([NotNull] Vector<T> vector)
            : base(vector.GeometricProcessor)
        {
            Vector = vector;
            VectorInverse = vector.Inverse();
        }

        private PureReflector([NotNull] Vector<T> vector, [NotNull] Vector<T> vectorInverse)
            : base(vector.GeometricProcessor)
        {
            Vector = vector;
            VectorInverse = vectorInverse;
        }


        public override bool IsValid()
        {
            // Make sure the storage and its reverse are correct
            if (!(Vector.Inverse() - VectorInverse).IsNearZero())
                return false;

            //// Make sure storage gp inverse(storage) == 1
            //var gp = 
            //    GeometricProcessor.Gp(Vector, VectorInverse);

            //if (!gp.IsScalar())
            //    return false;

            //var diff =
            //    GeometricProcessor.Subtract(
            //        GeometricProcessor.GetTermScalar(gp, 0),
            //        GeometricProcessor.ScalarOne
            //    );

            //return GeometricProcessor.IsNearZero(diff);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PureReflector<T> GetPureReflectorInverse()
        {
            return new PureReflector<T>(
                VectorInverse, 
                Vector
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IReflector<T> GetReflectorInverse()
        {
            return GetPureReflectorInverse();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector<T> OmMap(Vector<T> mv)
        {
            return Vector.Gp(mv).Gp(VectorInverse).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Bivector<T> OmMap(Bivector<T> mv)
        {
            return Vector.Gp(mv).Gp(VectorInverse).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVector<T> OmMap(KVector<T> mv)
        {
            return Vector.Gp(mv).Gp(VectorInverse).GetKVectorPart(mv.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Multivector<T> OmMap(Multivector<T> mv)
        {
            return Vector.Gp(mv).Gp(VectorInverse);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Multivector<T> GetMultivector()
        {
            return Vector.AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Multivector<T> GetMultivectorReverse()
        {
            return Vector.AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Multivector<T> GetMultivectorInverse()
        {
            return VectorInverse.AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorage()
        {
            return Vector.VectorStorage;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorageReverse()
        {
            return Vector.VectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorageInverse()
        {
            return VectorInverse.VectorStorage;
        }
    }
}