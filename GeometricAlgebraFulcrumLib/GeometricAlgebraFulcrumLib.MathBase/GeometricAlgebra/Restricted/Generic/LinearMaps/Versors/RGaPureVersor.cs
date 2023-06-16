using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Versors
{
    /// <summary>
    /// This class represents a Householder reflection on a hyper-space represented
    /// using its dual unit vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class RGaPureVersor<T> 
        : RGaVersorBase<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaPureVersor<T> Create(RGaVector<T> unitVectorStorage)
        {
            return new RGaPureVersor<T>(unitVectorStorage);
        }


        public RGaVector<T> Vector { get; }

        public RGaVector<T> VectorInverse { get; }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RGaPureVersor(RGaVector<T> vector)
            : base(vector.Processor)
        {
            Vector = vector;
            VectorInverse = vector.Inverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RGaPureVersor(RGaVector<T> vector, RGaVector<T> vectorInverse)
            : base(vector.Processor)
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
        public RGaPureVersor<T> GetPureDualVersorInverse()
        {
            return new RGaPureVersor<T>(VectorInverse, Vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRGaVersor<T> GetVersorInverse()
        {
            return GetPureDualVersorInverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> GetMultivector()
        {
            return Vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> GetMultivectorReverse()
        {
            return Vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> GetMultivectorInverse()
        {
            return VectorInverse;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaVector<T> OmMap(RGaVector<T> mv)
        {
            return Vector.Gp(-mv).Gp(VectorInverse).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaBivector<T> OmMap(RGaBivector<T> mv)
        {
            return Vector.Gp(mv).Gp(VectorInverse).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaHigherKVector<T> OmMap(RGaHigherKVector<T> kVector)
        {
            return Vector.Gp(kVector.GradeInvolution()).Gp(VectorInverse).GetHigherKVectorPart(kVector.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaKVector<T> OmMap(RGaKVector<T> mv)
        {
            return Vector.Gp(mv.GradeInvolution()).Gp(VectorInverse).GetKVectorPart(mv.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> OmMap(RGaMultivector<T> mv)
        {
            return Vector.Gp(mv.GradeInvolution()).Gp(VectorInverse);
        }

        public override LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions)
        {
            throw new NotImplementedException();
        }
    }
}