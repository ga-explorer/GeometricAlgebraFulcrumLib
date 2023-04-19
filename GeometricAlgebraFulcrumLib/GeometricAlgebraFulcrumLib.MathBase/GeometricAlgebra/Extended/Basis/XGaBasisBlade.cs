using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Basis
{
    /// <summary>
    /// This represents a basis blade of arbitrary grade for an extended GA
    /// </summary>
    public sealed record XGaBasisBlade : 
        IXGaSignedBasisBlade
    {
        public XGaMetric Metric { get; }

        public IIndexSet Id { get; }

        public IntegerSign Sign 
            => IntegerSign.Positive;

        public XGaBasisBlade GetBasisBlade() => this;

        public bool IsNegative 
            => false;

        public bool IsZero 
            => false;

        public bool IsPositive 
            => true;

        public bool IsNonNegative 
            => true;

        public bool IsNonZero 
            => true;

        public bool IsNonPositive 
            => false;

        public bool IsScalar 
            => Id.IsEmptySet;

        public bool IsVector 
            => Id.IsSingleIndexSet;

        public bool IsBivector 
            => Id.IsIndexPairSet;

        public int VSpaceDimensions 
            => Id.IsEmptySet 
                ? 0 
                : Id.VSpaceDimensions();

        public int Grade 
            => Id.Grade();

        public ulong KvSpaceDimensions 
            => Id.KvSpaceDimensions();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaBasisBlade(XGaMetric metric, IIndexSet basisBladeId)
        {
            Metric = metric;
            Id = basisBladeId;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return true;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsKVector(int grade)
        {
            return Id.Count == grade;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign Signature()
        {
            return Metric.Signature(Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign NegativeSign()
        {
            return IntegerSign.Negative;
        }

        public IXGaSignedBasisBlade ShiftIndices(int offset)
        {
            if (IsScalar || offset == 0) return this;

            return new XGaBasisBlade(
                Metric, 
                Id.ShiftIndices(offset)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Negative()
        {
            return new XGaSignedBasisBlade(this, IntegerSign.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Times(IntegerSign sign)
        {
            return sign.Value switch
            {
                > 0 => this,
                < 0 => new XGaSignedBasisBlade(this, IntegerSign.Negative),
                _ => new XGaSignedBasisBlade(this, IntegerSign.Zero)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Reverse()
        {
            return Grade.ReverseIsNegativeOfGrade() 
                ? Negative()
                : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign ReverseSign()
        {
            return Grade.ReverseIsNegativeOfGrade()
                ? IntegerSign.Negative
                : IntegerSign.Positive;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade GradeInvolution()
        {
            return Grade.GradeInvolutionIsNegativeOfGrade()
                ? Negative()
                : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign GradeInvolutionSign()
        {
            return Grade.GradeInvolutionIsNegativeOfGrade()
                ? IntegerSign.Negative
                : IntegerSign.Positive;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade CliffordConjugate()
        {
            return Grade.CliffordConjugateIsNegativeOfGrade()
                ? Negative()
                : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign CliffordConjugateSign()
        {
            return Grade.CliffordConjugateIsNegativeOfGrade()
                ? IntegerSign.Negative
                : IntegerSign.Positive;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade EConjugate()
        {
            return Reverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign EConjugateSign()
        {
            return ReverseSign();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Conjugate()
        {
            return Metric.IsEuclidean 
                ? Reverse() 
                : Reverse().Times(Signature());
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign ConjugateSign()
        {
            return Metric.IsEuclidean 
                ? ReverseSign() 
                : ReverseSign() * Signature();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign TimesSign(IntegerSign sign)
        {
            return sign;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade EGp(IIndexSet id2)
        {
            var (isNegative, id) = 
                Id.EGpIsNegativeId(id2);

            var basisBlade = new XGaBasisBlade(Metric, id);

            return isNegative
                ? basisBlade.Negative()
                : basisBlade;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool EGpIsNegative(IIndexSet id2)
        {
            return Id.EGpIsNegative(id2);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign EGpSign(IIndexSet id2)
        {
            return Id.EGpSign(id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign EGpSign(IXGaSignedBasisBlade basisBlade)
        {
            return basisBlade.IsZero
                ? IntegerSign.Zero 
                : Id.EGpSign(basisBlade.Id, basisBlade.IsNegative);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Op(IIndexSet id2)
        {
            return Id.OpIsNonZero(id2)
                ? EGp(id2)
                : Metric.ZeroBasisScalar;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade ESp(IIndexSet id2)
        {
            return Id.ESpIsNonZero(id2)
                ? EGp(id2)
                : Metric.ZeroBasisScalar;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade ELcp(IIndexSet id2)
        {
            return Id.ELcpIsNonZero(id2)
                ? EGp(id2)
                : Metric.ZeroBasisScalar;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade ERcp(IIndexSet id2)
        {
            return Id.ERcpIsNonZero(id2)
                ? EGp(id2)
                : Metric.ZeroBasisScalar;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade EFdp(IIndexSet id2)
        {
            return Id.EFdpIsNonZero(id2)
                ? EGp(id2)
                : Metric.ZeroBasisScalar;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade EHip(IIndexSet id2)
        {
            return Id.EHipIsNonZero(id2)
                ? EGp(id2)
                : Metric.ZeroBasisScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade EAcp(IIndexSet id2)
        {
            return Id.EAcpIsNonZero(id2)
                ? EGp(id2)
                : Metric.ZeroBasisScalar;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade ECp(IIndexSet id2)
        {
            return Id.ECpIsNonZero(id2)
                ? EGp(id2)
                : Metric.ZeroBasisScalar;
        }


        public IXGaSignedBasisBlade Gp(IIndexSet id2)
        {
            if (Metric.IsEuclidean) 
                return EGp(id2);
        
            var meetBasisBladeSignature = 
                Metric.Signature(
                    Id.Intersect(id2)
                );

            if (meetBasisBladeSignature.IsZero)
                return Metric.ZeroBasisScalar;

            var egpBasisBlade = EGp(id2);

            return meetBasisBladeSignature.IsPositive
                ? egpBasisBlade
                : egpBasisBlade.Negative();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Sp(IIndexSet id2)
        {
            return Id.ESpIsNonZero(id2)
                ? Gp(id2)
                : Metric.ZeroBasisScalar;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Lcp(IIndexSet id2)
        {
            return Id.ELcpIsNonZero(id2)
                ? Gp(id2)
                : Metric.ZeroBasisScalar;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Rcp(IIndexSet id2)
        {
            return Id.ERcpIsNonZero(id2)
                ? Gp(id2)
                : Metric.ZeroBasisScalar;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Fdp(IIndexSet id2)
        {
            return Id.EFdpIsNonZero(id2)
                ? Gp(id2)
                : Metric.ZeroBasisScalar;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Hip(IIndexSet id2)
        {
            return Id.EHipIsNonZero(id2)
                ? Gp(id2)
                : Metric.ZeroBasisScalar;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Acp(IIndexSet id2)
        {
            return Id.EAcpIsNonZero(id2)
                ? Gp(id2)
                : Metric.ZeroBasisScalar;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Cp(IIndexSet id2)
        {
            return Id.ECpIsNonZero(id2)
                ? Gp(id2)
                : Metric.ZeroBasisScalar;
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade EGp(IXGaSignedBasisBlade basisBlade)
        {
            return EGp(basisBlade.Id).Times(basisBlade.Sign);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade ESp(IXGaSignedBasisBlade basisBlade)
        {
            return ESp(basisBlade.Id).Times(basisBlade.Sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Op(IXGaSignedBasisBlade basisBlade)
        {
            return Op(basisBlade.Id).Times(basisBlade.Sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade ELcp(IXGaSignedBasisBlade basisBlade)
        {
            return ELcp(basisBlade.Id).Times(basisBlade.Sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade ERcp(IXGaSignedBasisBlade basisBlade)
        {
            return ERcp(basisBlade.Id).Times(basisBlade.Sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade EFdp(IXGaSignedBasisBlade basisBlade)
        {
            return EFdp(basisBlade.Id).Times(basisBlade.Sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade EHip(IXGaSignedBasisBlade basisBlade)
        {
            return EHip(basisBlade.Id).Times(basisBlade.Sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade EAcp(IXGaSignedBasisBlade basisBlade)
        {
            return EAcp(basisBlade.Id).Times(basisBlade.Sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade ECp(IXGaSignedBasisBlade basisBlade)
        {
            return ECp(basisBlade.Id).Times(basisBlade.Sign);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Gp(IXGaSignedBasisBlade basisBlade)
        {
            return Gp(basisBlade.Id).Times(basisBlade.Sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Sp(IXGaSignedBasisBlade basisBlade)
        {
            return Sp(basisBlade.Id).Times(basisBlade.Sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Lcp(IXGaSignedBasisBlade basisBlade)
        {
            return Lcp(basisBlade.Id).Times(basisBlade.Sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Rcp(IXGaSignedBasisBlade basisBlade)
        {
            return Rcp(basisBlade.Id).Times(basisBlade.Sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Fdp(IXGaSignedBasisBlade basisBlade)
        {
            return Fdp(basisBlade.Id).Times(basisBlade.Sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Hip(IXGaSignedBasisBlade basisBlade)
        {
            return Hip(basisBlade.Id).Times(basisBlade.Sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Acp(IXGaSignedBasisBlade basisBlade)
        {
            return Acp(basisBlade.Id).Times(basisBlade.Sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaSignedBasisBlade Cp(IXGaSignedBasisBlade basisBlade)
        {
            return Cp(basisBlade.Id).Times(basisBlade.Sign);
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign EGpReverseSign(IIndexSet id2)
        {
            var reverseSign = id2.ReverseSignOfBasisBladeId();

            return Id.EGpIsNegative(id2) 
                ? -reverseSign 
                : reverseSign;
        }

        public IntegerSign GpSign(IXGaSignedBasisBlade basisBlade)
        {
            if (Metric.IsEuclidean) 
                return EGpSign(basisBlade);

            if (basisBlade.IsZero)
                return IntegerSign.Zero;

            var id2 = basisBlade.Id;

            var meetSignature = 
                Metric.Signature(
                    Id.Intersect(id2)
                );

            return meetSignature.IsZero
                ? IntegerSign.Zero
                : Id.EGpSign(
                    id2,
                    (basisBlade.Sign * meetSignature).IsNegative
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign OpSign(IIndexSet id2)
        {
            return Id.OpIsNonZero(id2) 
                ? EGpSign(id2) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign OpSign(IXGaSignedBasisBlade basisBlade)
        {
            if (basisBlade.IsZero) 
                return IntegerSign.Zero;

            return Id.OpIsNonZero(basisBlade.Id) 
                ? EGpSign(basisBlade) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign ESpSign(IIndexSet id2)
        {
            return Id.ESpIsNonZero(id2) 
                ? EGpSign(id2) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign ESpSign(IXGaSignedBasisBlade basisBlade)
        {
            if (basisBlade.IsZero) 
                return IntegerSign.Zero;

            return Id.ESpIsNonZero(basisBlade.Id) 
                ? EGpSign(basisBlade) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign SpSign(IXGaSignedBasisBlade basisBlade)
        {
            if (basisBlade.IsZero) 
                return IntegerSign.Zero;

            return Id.ESpIsNonZero(basisBlade.Id) 
                ? GpSign(basisBlade) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign ELcpSign(IIndexSet id2)
        {
            return Id.ELcpIsNonZero(id2) 
                ? EGpSign(id2) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign ELcpSign(IXGaSignedBasisBlade basisBlade)
        {
            if (basisBlade.IsZero) 
                return IntegerSign.Zero;

            return Id.ELcpIsNonZero(basisBlade.Id) 
                ? EGpSign(basisBlade) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign LcpSign(IXGaSignedBasisBlade basisBlade)
        {
            if (basisBlade.IsZero) 
                return IntegerSign.Zero;

            return Id.ELcpIsNonZero(basisBlade.Id) 
                ? GpSign(basisBlade) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign ERcpSign(IIndexSet id2)
        {
            return Id.ERcpIsNonZero(id2) 
                ? EGpSign(id2) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign ERcpSign(IXGaSignedBasisBlade basisBlade)
        {
            if (basisBlade.IsZero) 
                return IntegerSign.Zero;

            return Id.ERcpIsNonZero(basisBlade.Id) 
                ? EGpSign(basisBlade) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign RcpSign(IXGaSignedBasisBlade basisBlade)
        {
            if (basisBlade.IsZero) 
                return IntegerSign.Zero;

            return Id.ERcpIsNonZero(basisBlade.Id) 
                ? GpSign(basisBlade) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign EFdpSign(IIndexSet id2)
        {
            return Id.EFdpIsNonZero(id2) 
                ? EGpSign(id2) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign EFdpSign(IXGaSignedBasisBlade basisBlade)
        {
            if (basisBlade.IsZero) 
                return IntegerSign.Zero;

            return Id.EFdpIsNonZero(basisBlade.Id) 
                ? EGpSign(basisBlade) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign FdpSign(IXGaSignedBasisBlade basisBlade)
        {
            if (basisBlade.IsZero) 
                return IntegerSign.Zero;

            return Id.EFdpIsNonZero(basisBlade.Id) 
                ? GpSign(basisBlade) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign EHipSign(IIndexSet id2)
        {
            return Id.EHipIsNonZero(id2) 
                ? EGpSign(id2) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign EHipSign(IXGaSignedBasisBlade basisBlade)
        {
            if (basisBlade.IsZero) 
                return IntegerSign.Zero;

            return Id.EHipIsNonZero(basisBlade.Id) 
                ? EGpSign(basisBlade) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign HipSign(IXGaSignedBasisBlade basisBlade)
        {
            if (basisBlade.IsZero) 
                return IntegerSign.Zero;

            return Id.EHipIsNonZero(basisBlade.Id) 
                ? GpSign(basisBlade) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign EAcpSign(IIndexSet id2)
        {
            return Id.EAcpIsNonZero(id2) 
                ? EGpSign(id2) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign EAcpSign(IXGaSignedBasisBlade basisBlade)
        {
            if (basisBlade.IsZero) 
                return IntegerSign.Zero;

            return Id.EAcpIsNonZero(basisBlade.Id) 
                ? EGpSign(basisBlade) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign AcpSign(IXGaSignedBasisBlade basisBlade)
        {
            if (basisBlade.IsZero) 
                return IntegerSign.Zero;

            return Id.EAcpIsNonZero(basisBlade.Id) 
                ? GpSign(basisBlade) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign ECpSign(IIndexSet id2)
        {
            return Id.ECpIsNonZero(id2) 
                ? EGpSign(id2) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign ECpSign(IXGaSignedBasisBlade basisBlade)
        {
            if (basisBlade.IsZero) 
                return IntegerSign.Zero;

            return Id.ECpIsNonZero(basisBlade.Id) 
                ? EGpSign(basisBlade) 
                : IntegerSign.Zero;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign CpSign(IXGaSignedBasisBlade basisBlade)
        {
            if (basisBlade.IsZero) 
                return IntegerSign.Zero;

            return Id.ECpIsNonZero(basisBlade.Id) 
                ? GpSign(basisBlade) 
                : IntegerSign.Zero;
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign GpSign(IIndexSet id2)
        {
            if (Metric.IsEuclidean) return EGpSign(id2);
        
            var meetBladeSignature = 
                GetMeetBladeSignature(id2);

            return EGpIsNegative(id2) 
                ? -meetBladeSignature 
                : meetBladeSignature;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign SpSign(IIndexSet id2)
        {
            return Id.ESpIsNonZero(id2) 
                ? GpSign(id2) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign LcpSign(IIndexSet id2)
        {
            return Id.ELcpIsNonZero(id2) 
                ? GpSign(id2) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign RcpSign(IIndexSet id2)
        {
            return Id.ERcpIsNonZero(id2) 
                ? GpSign(id2) 
                : IntegerSign.Zero;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign FdpSign(IIndexSet id2)
        {
            return Id.EFdpIsNonZero(id2) 
                ? GpSign(id2) 
                : IntegerSign.Zero;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign HipSign(IIndexSet id2)
        {
            return Id.EHipIsNonZero(id2) 
                ? GpSign(id2) 
                : IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign AcpSign(IIndexSet id2)
        {
            return Id.EAcpIsNonZero(id2) 
                ? GpSign(id2) 
                : IntegerSign.Zero;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign CpSign(IIndexSet id2)
        {
            return Id.ECpIsNonZero(id2) 
                ? GpSign(id2) 
                : IntegerSign.Zero;
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign GetMeetBladeSignature(IIndexSet id2)
        {
            return Metric.Signature(
                Id.Intersect(id2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBasisBlade GetMeetBlade(IIndexSet id2)
        {
            return new XGaBasisBlade(
                Metric,
                Id.Intersect(id2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBasisBlade GetJoinBlade(IIndexSet id2)
        {
            return new XGaBasisBlade(
                Metric,
                Id.Join(id2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBasisBlade GetEGpBlade(IIndexSet id2)
        {
            return new XGaBasisBlade(
                Metric,
                Id.SymmetricDifference(id2)
            );
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return Id
                .Select(i => (i + 1).ToString())
                .Concatenate(", ", "<", ">");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(XGaBasisBlade? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(Id, other.Id);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(IXGaSignedBasisBlade? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return other.Sign.IsPositive && Equals(Id, other.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return HashCode.Combine(Sign, Id);
        }
    }
}