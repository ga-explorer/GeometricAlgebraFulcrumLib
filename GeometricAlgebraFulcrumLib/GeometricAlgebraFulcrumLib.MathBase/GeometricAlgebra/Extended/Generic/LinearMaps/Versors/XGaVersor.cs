using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Versors
{
    public class XGaVersor<T> : 
        XGaVersorBase<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static XGaVersor<T> Create(XGaMultivector<T> multivector)
        {
            return new XGaVersor<T>(multivector);
        }


        public int Grade { get; }

        public bool IsEven 
            => Grade.IsEven();

        public bool IsOdd 
            => Grade.IsOdd();

        public XGaMultivector<T> Multivector { get; }

        public XGaMultivector<T> MultivectorInverse { get; }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private XGaVersor(XGaMultivector<T> multivector) 
            : base(multivector.Processor)
        {
            Grade = multivector.GetMaxGrade();
            Multivector = multivector;
            MultivectorInverse = Multivector.Inverse();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private XGaVersor(int grade, XGaMultivector<T> multivector, XGaMultivector<T> multivectorInverse) 
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

            var diff = gp.Scalar() - 1;

            return diff.IsNearZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> OmMap(XGaVector<T> mv)
        {
            return IsEven 
                ? Multivector.Gp(mv).Gp(MultivectorInverse).GetVectorPart() 
                : Multivector.Gp(-mv).Gp(MultivectorInverse).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> OmMap(XGaBivector<T> mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorInverse).GetBivectorPart();
        }

        public override XGaHigherKVector<T> OmMap(XGaHigherKVector<T> kVector)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> OmMap(XGaMultivector<T> mv)
        {
            var v = 
                Multivector.Gp(mv).Gp(MultivectorInverse);

            if (IsEven)
                return v;

            var (evenMv, oddMv) = 
                v.GetEvenOddParts();

            return evenMv - oddMv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IXGaVersor<T> GetVersorInverse()
        {
            return new XGaVersor<T>(
                Grade,
                MultivectorInverse, 
                Multivector
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> GetMultivector()
        {
            return Multivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> GetMultivectorReverse()
        {
            return Multivector.Reverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> GetMultivectorInverse()
        {
            return MultivectorInverse;
        }
    }
}