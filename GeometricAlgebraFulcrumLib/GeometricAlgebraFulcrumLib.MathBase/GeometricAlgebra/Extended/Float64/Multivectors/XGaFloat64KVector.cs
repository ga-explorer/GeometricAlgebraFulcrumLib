using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors
{
    public abstract partial class XGaFloat64KVector :
        XGaFloat64Multivector
    {
        public abstract int Grade { get; }

        public ulong KvSpaceDimensions
            => IsZero ? 1 : VSpaceDimensions.GetBinomialCoefficient(Grade);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected XGaFloat64KVector(XGaFloat64Processor processor)
            : base(processor)
        {
        }
    
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsScalar()
        {
            return IsZero || Grade == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsVector()
        {
            return IsZero || Grade == 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsBivector()
        {
            return IsZero || Grade == 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsKVector(int grade)
        {
            return IsZero || Grade == grade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsOdd()
        {
            return Grade.IsOdd();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsOdd(int maxGrade)
        {
            return Grade.IsOdd(maxGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEven()
        {
            return Grade.IsEven();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEven(int maxGrade)
        {
            return Grade.IsEven(maxGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsScalarPart()
        {
            return !IsZero && Grade == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsVectorPart()
        {
            return !IsZero && Grade == 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsBivectorPart()
        {
            return !IsZero && Grade == 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKVectorPart(int grade)
        {
            return !IsZero && Grade == grade;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsOddPart()
        {
            return !IsZero && Grade.IsOdd();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsOddPart(int maxGrade)
        {
            return !IsZero && Grade.IsOdd(maxGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsEvenPart()
        {
            return !IsZero && Grade.IsEven();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsEvenPart(int maxGrade)
        {
            return !IsZero && Grade.IsEven(maxGrade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetMaxGrade()
        {
            return IsZero ? 0 : Grade;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetKVectorCount()
        {
            return IsZero ? 0 : 1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector GetKVectorPart(int grade)
        {
            return grade == Grade
                ? this
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector GetEvenPart()
        {
            return IsEven()
                ? this
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector GetEvenPart(int maxGrade)
        {
            return IsEven(maxGrade)
                ? this
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector GetOddPart()
        {
            return IsOdd()
                ? this
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector GetOddPart(int maxGrade)
        {
            return IsOdd(maxGrade)
                ? this
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<XGaFloat64KVector> GetKVectorParts()
        {
            if (!IsZero) yield return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<ulong, double>> GetKVectorArrayItems()
        {
            return IdScalarPairs.Select(
                term => 
                    new KeyValuePair<ulong, double>(
                        term.Key.ToUInt64().BasisBladeIdToIndex(), 
                        term.Value
                    )
            );
        }
    }
}