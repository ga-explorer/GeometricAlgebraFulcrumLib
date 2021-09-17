using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors
{
    public class Versor<T> : 
        VersorBase<T>
    {
        public uint Grade { get; }

        public bool IsEven 
            => Grade.IsEven();

        public bool IsOdd 
            => Grade.IsOdd();

        public IMultivectorStorage<T> Multivector { get; }

        public IMultivectorStorage<T> MultivectorInverse { get; }

        
        internal Versor([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] IMultivectorStorage<T> multivector) 
            : base(processor)
        {
            Grade = multivector.GetMaxGrade();
            Multivector = multivector;
            MultivectorInverse = processor.VersorInverse(Multivector);
        }
        
        private Versor([NotNull] IGeometricAlgebraProcessor<T> processor, uint grade, [NotNull] IMultivectorStorage<T> multivector, [NotNull] IMultivectorStorage<T> multivectorInverse) 
            : base(processor)
        {
            Grade = grade;
            Multivector = multivector;
            MultivectorInverse = multivectorInverse;
        }


        public override bool IsValid()
        {
            // Make sure the storage and its inverse are correct
            if (!GeometricProcessor.IsNearZero(GeometricProcessor.Subtract(GeometricProcessor.VersorInverse(Multivector), MultivectorInverse)))
                return false;

            // Make sure storage contains terms of only even or only odd grade
            var grades = Multivector.GetGrades().ToArray();
            if (!grades.All(g => g.IsEven()) && !grades.All(g => g.IsOdd()))
                return false;

            // Make sure storage gp versorInverse(storage) == 1
            var gp = 
                GeometricProcessor.Gp(Multivector, MultivectorInverse);

            if (!gp.IsScalar())
                return false;

            var diff =
                GeometricProcessor.Subtract(
                    GeometricProcessor.GetScalar(gp),
                    GeometricProcessor.ScalarOne
                );

            return GeometricProcessor.IsNearZero(diff);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorage<T> OmMapVector(VectorStorage<T> mv)
        {
            var v = 
                GeometricProcessor
                    .Gp(Multivector, mv, MultivectorInverse)
                    .GetVectorPart();

            return IsEven 
                ? v 
                : GeometricProcessor.Negative(v);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override BivectorStorage<T> OmMapBivector(BivectorStorage<T> mv)
        {
            return GeometricProcessor
                .Gp(Multivector, mv, MultivectorInverse)
                .GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVectorStorage<T> OmMapKVector(KVectorStorage<T> mv)
        {
            var v = 
                GeometricProcessor
                    .Gp(Multivector, mv, MultivectorInverse)
                    .GetKVectorPart(mv.Grade);

            return mv.Grade.IsEven() || IsEven
                ? v 
                : GeometricProcessor.Negative(v);
        }

        public override MultivectorGradedStorage<T> OmMapMultivector(MultivectorGradedStorage<T> mv)
        {
            var v = 
                GeometricProcessor.Gp(Multivector, mv, MultivectorInverse);

            if (IsEven)
                return v.ToMultivectorGradedStorage();

            var (evenMv, oddMv) = 
                v.SplitEvenOddParts();

            return GeometricProcessor
                .Subtract(evenMv, oddMv)
                .ToMultivectorGradedStorage();
        }

        public override MultivectorStorage<T> OmMapMultivector(MultivectorStorage<T> mv)
        {
            var v = 
                GeometricProcessor.Gp(Multivector, mv, MultivectorInverse);

            if (IsEven)
                return v.ToMultivectorStorage();

            var (evenMv, oddMv) = 
                v.SplitEvenOddParts();

            return GeometricProcessor
                .Subtract(evenMv, oddMv)
                .ToMultivectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IVersor<T> GetVersorInverse()
        {
            return new Versor<T>(
                GeometricProcessor, 
                Grade,
                MultivectorInverse, 
                Multivector
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorage()
        {
            return Multivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorInverseStorage()
        {
            return MultivectorInverse;
        }
    }
}