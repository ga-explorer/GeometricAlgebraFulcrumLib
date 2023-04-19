using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Reflectors
{
    /// <summary>
    /// A pure (direct) reflector is a single vector.
    /// The reflection happens in the unit vector itself, not its dual hyperplane
    /// like the case for pure versors.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class RGaPureReflector<T> : 
        RGaReflectorBase<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaPureReflector<T> Create(RGaVector<T> vector)
        {
            return new RGaPureReflector<T>(vector);
        }
        

        public RGaVector<T> Vector { get; }

        public RGaVector<T> VectorInverse { get; }


        private RGaPureReflector(RGaVector<T> vector)
            : base(vector.Processor)
        {
            Vector = vector;
            VectorInverse = vector.Inverse();
        }

        private RGaPureReflector(RGaVector<T> vector, RGaVector<T> vectorInverse)
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
        public RGaPureReflector<T> GetPureReflectorInverse()
        {
            return new RGaPureReflector<T>(
                VectorInverse, 
                Vector
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRGaReflector<T> GetReflectorInverse()
        {
            return GetPureReflectorInverse();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaVector<T> OmMap(RGaVector<T> mv)
        {
            return Vector.Gp(mv).Gp(VectorInverse).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaBivector<T> OmMap(RGaBivector<T> mv)
        {
            return Vector.Gp(mv).Gp(VectorInverse).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaHigherKVector<T> OmMap(RGaHigherKVector<T> kVector)
        {
            return Vector.Gp(kVector).Gp(VectorInverse).GetHigherKVectorPart(kVector.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaKVector<T> OmMap(RGaKVector<T> mv)
        {
            return Vector.Gp(mv).Gp(VectorInverse).GetKVectorPart(mv.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> OmMap(RGaMultivector<T> mv)
        {
            return Vector.Gp(mv).Gp(VectorInverse);
        }

        public override LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions)
        {
            throw new NotImplementedException();
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
        
    }
}