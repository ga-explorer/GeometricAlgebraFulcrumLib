using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Versors
{
    public class RGaFloat64Versor : 
        RGaFloat64VersorBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaFloat64Versor Create(RGaFloat64Multivector multivector)
        {
            return new RGaFloat64Versor(multivector);
        }


        public int Grade { get; }

        public bool IsEven 
            => Grade.IsEven();

        public bool IsOdd 
            => Grade.IsOdd();

        public RGaFloat64Multivector Multivector { get; }

        public RGaFloat64Multivector MultivectorInverse { get; }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RGaFloat64Versor(RGaFloat64Multivector multivector) 
            : base(multivector.Processor)
        {
            Grade = multivector.GetMaxGrade();
            Multivector = multivector;
            MultivectorInverse = Multivector.Inverse();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RGaFloat64Versor(int grade, RGaFloat64Multivector multivector, RGaFloat64Multivector multivectorInverse) 
            : base(multivector.Processor)
        {
            Grade = grade;
            Multivector = multivector;
            MultivectorInverse = multivectorInverse;
        }


        public override bool IsValid()
        {
            // Make sure the storage and its inverse are correct
            if (!(Multivector.Inverse() - MultivectorInverse).IsNearZero())
                return false;

            // Make sure storage contains terms of only even or only odd grade
            var grades = Multivector.KVectorGrades.ToArray();
            if (!grades.All(g => g.IsEven()) && !grades.All(g => g.IsOdd()))
                return false;

            // Make sure storage gp versorInverse(storage) == 1
            var gp = 
                Multivector.Gp(MultivectorInverse);

            if (!gp.IsScalar())
                return false;

            var diff = gp[0] - 1;

            return diff.IsNearZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Vector OmMap(RGaFloat64Vector mv)
        {
            return IsEven 
                ? Multivector.Gp(mv).Gp(MultivectorInverse).GetVectorPart() 
                : Multivector.Gp(-mv).Gp(MultivectorInverse).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Bivector OmMap(RGaFloat64Bivector mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorInverse).GetBivectorPart();
        }

        public override RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector kVector)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64KVector OmMap(RGaFloat64KVector mv)
        {
            return mv.Grade.IsEven() || IsEven
                ? Multivector.Gp(mv).Gp(MultivectorInverse).GetKVectorPart(mv.Grade) 
                : Multivector.Gp(-mv).Gp(MultivectorInverse).GetKVectorPart(mv.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector OmMap(RGaFloat64Multivector mv)
        {
            var v = 
                Multivector.Gp(mv).Gp(MultivectorInverse);

            if (IsEven)
                return v;

            var (evenMv, oddMv) = 
                v.GetEvenOddParts();

            return evenMv - oddMv;
        }

        public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRGaFloat64Versor GetVersorInverse()
        {
            return new RGaFloat64Versor(
                Grade,
                MultivectorInverse, 
                Multivector
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector GetMultivector()
        {
            return Multivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector GetMultivectorReverse()
        {
            return Multivector.Reverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector GetMultivectorInverse()
        {
            return MultivectorInverse;
        }
    }
}