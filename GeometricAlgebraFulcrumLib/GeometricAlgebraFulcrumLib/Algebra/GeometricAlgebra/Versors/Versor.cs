using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors
{
    public class Versor<T> : 
        VersorBase<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Versor<T> Create(Multivector<T> multivector)
        {
            return new Versor<T>(multivector);
        }


        public uint Grade { get; }

        public bool IsEven 
            => Grade.IsEven();

        public bool IsOdd 
            => Grade.IsOdd();

        public Multivector<T> Multivector { get; }

        public Multivector<T> MultivectorInverse { get; }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Versor([NotNull] Multivector<T> multivector) 
            : base(multivector.GeometricProcessor)
        {
            Grade = multivector.MultivectorStorage.GetMaxGrade();
            Multivector = multivector;
            MultivectorInverse = Multivector.Inverse();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Versor(uint grade, [NotNull] Multivector<T> multivector, [NotNull] Multivector<T> multivectorInverse) 
            : base(multivector.GeometricProcessor)
        {
            Grade = grade;
            Multivector = multivector;
            MultivectorInverse = multivectorInverse;
        }


        public override bool IsValid()
        {
            // Make sure the storage and its inverse are correct
            if (!GeometricProcessor.IsNearZero((Multivector.Inverse() - MultivectorInverse).MultivectorStorage))
                return false;

            // Make sure storage contains terms of only even or only odd grade
            var grades = Multivector.GetGrades().ToArray();
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
        public override Vector<T> OmMap(Vector<T> mv)
        {
            return IsEven 
                ? Multivector.Gp(mv).Gp(MultivectorInverse).GetVectorPart() 
                : Multivector.Gp(-mv).Gp(MultivectorInverse).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Bivector<T> OmMap(Bivector<T> mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorInverse).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVector<T> OmMap(KVector<T> mv)
        {
            return mv.Grade.IsEven() || IsEven
                ? Multivector.Gp(mv).Gp(MultivectorInverse).GetKVectorPart(mv.Grade) 
                : Multivector.Gp(-mv).Gp(MultivectorInverse).GetKVectorPart(mv.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Multivector<T> OmMap(Multivector<T> mv)
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
        public override IVersor<T> GetVersorInverse()
        {
            return new Versor<T>(
                Grade,
                MultivectorInverse, 
                Multivector
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Multivector<T> GetMultivector()
        {
            return Multivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Multivector<T> GetMultivectorReverse()
        {
            return Multivector.Reverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Multivector<T> GetMultivectorInverse()
        {
            return MultivectorInverse;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorage()
        {
            return Multivector.MultivectorStorage;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorageReverse()
        {
            return Multivector.Reverse().MultivectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorageInverse()
        {
            return MultivectorInverse.MultivectorStorage;
        }
    }
}