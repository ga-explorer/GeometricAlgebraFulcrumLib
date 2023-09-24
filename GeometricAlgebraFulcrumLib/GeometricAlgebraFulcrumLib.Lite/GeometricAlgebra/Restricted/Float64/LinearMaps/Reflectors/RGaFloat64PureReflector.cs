using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.LinearMaps.Reflectors
{
    /// <summary>
    /// A pure (direct) reflector is a single vector.
    /// The reflection happens in the unit vector itself, not its dual hyperplane
    /// like the case for pure versors.
    /// </summary>
    public sealed class RGaFloat64PureReflector : 
        RGaFloat64ReflectorBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaFloat64PureReflector Create(RGaFloat64Vector vector)
        {
            return new RGaFloat64PureReflector(vector);
        }

        public override RGaFloat64Processor Processor 
            => Vector.Processor;

        public RGaFloat64Vector Vector { get; }

        public RGaFloat64Vector VectorInverse { get; }


        private RGaFloat64PureReflector(RGaFloat64Vector vector)
            : base(vector.Processor)
        {
            Vector = vector;
            VectorInverse = vector.Inverse();
        }

        private RGaFloat64PureReflector(RGaFloat64Vector vector, RGaFloat64Vector vectorInverse)
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
        public RGaFloat64PureReflector GetPureReflectorInverse()
        {
            return new RGaFloat64PureReflector(
                VectorInverse, 
                Vector
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRGaFloat64Reflector GetReflectorInverse()
        {
            return GetPureReflectorInverse();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Vector OmMap(RGaFloat64Vector mv)
        {
            return Vector.Gp(mv).Gp(VectorInverse).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Bivector OmMap(RGaFloat64Bivector mv)
        {
            return Vector.Gp(mv).Gp(VectorInverse).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector kVector)
        {
            return Vector.Gp(kVector).Gp(VectorInverse).GetHigherKVectorPart(kVector.Grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector OmMap(RGaFloat64Multivector mv)
        {
            return Vector.Gp(mv).Gp(VectorInverse);
        }

        public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
        {
            throw new NotImplementedException();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector GetMultivector()
        {
            return Vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector GetMultivectorReverse()
        {
            return Vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector GetMultivectorInverse()
        {
            return VectorInverse;
        }
        
    }
}