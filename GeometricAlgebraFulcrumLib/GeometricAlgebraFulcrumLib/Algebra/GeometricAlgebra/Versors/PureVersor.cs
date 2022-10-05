using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors
{
    /// <summary>
    /// This class represents a Householder reflection on a hyper-space represented
    /// using its dual unit vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class PureVersor<T> 
        : VersorBase<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static PureVersor<T> Create(GaVector<T> unitVectorStorage)
        {
            return new PureVersor<T>(unitVectorStorage);
        }


        public GaVector<T> Vector { get; }

        public GaVector<T> VectorInverse { get; }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private PureVersor([NotNull] GaVector<T> vector)
            : base(vector.GeometricProcessor)
        {
            Vector = vector;
            VectorInverse = vector.Inverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private PureVersor([NotNull] GaVector<T> vector, [NotNull] GaVector<T> vectorInverse)
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

            // Make sure storage gp reverse(storage) == 1
            var gp = 
                Vector.Gp(VectorInverse);

            if (!gp.IsScalar())
                return false;

            var diff = gp[0].Abs() - 1;

            return diff.IsNearZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PureVersor<T> GetPureDualVersorInverse()
        {
            return new PureVersor<T>(VectorInverse, Vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IVersor<T> GetVersorInverse()
        {
            return GetPureDualVersorInverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaMultivector<T> GetMultivector()
        {
            return Vector.AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaMultivector<T> GetMultivectorReverse()
        {
            return Vector.AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaMultivector<T> GetMultivectorInverse()
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaVector<T> OmMap(GaVector<T> mv)
        {
            return Vector.Gp(-mv).Gp(VectorInverse).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaBivector<T> OmMap(GaBivector<T> mv)
        {
            return Vector.Gp(mv).Gp(VectorInverse).GetBivectorPart();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaKVector<T> OmMap(GaKVector<T> mv)
        {
            return Vector.Gp(mv.GradeInvolution()).Gp(VectorInverse).GetKVectorPart(mv.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaMultivector<T> OmMap(GaMultivector<T> mv)
        {
            return Vector.Gp(mv.GradeInvolution()).Gp(VectorInverse);
        }
    }
}