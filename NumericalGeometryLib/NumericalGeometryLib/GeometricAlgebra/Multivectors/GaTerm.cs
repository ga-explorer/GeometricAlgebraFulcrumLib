using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.GeometricAlgebra.Basis;
using NumericalGeometryLib.GeometricAlgebra.Structures;

namespace NumericalGeometryLib.GeometricAlgebra.Multivectors
{
    public sealed record GaTerm :
        IGeometricElement
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator GaMultivector(GaTerm mv)
        {
            return new GaMultivector(mv);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm operator +(GaTerm mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm operator -(GaTerm mv)
        {
            return new GaTerm(
                mv.BasisSet,
                mv.Id,
                -mv.Scalar
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator +(GaTerm mv1, double mv2)
        {
            if (mv1.Id == 0)
                return mv1.BasisSet.CreateScalar(mv1.Scalar + mv2);

            var scalarList = new GaMultivectorSparseList(mv1.GaSpaceDimension)
            {
                [0] = mv2,
                [mv1.Id] = mv1.Scalar
            };

            return new GaMultivector(mv1.BasisSet, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator +(double mv1, GaTerm mv2)
        {
            if (mv2.Id == 0)
                return mv2.BasisSet.CreateScalar(mv1 + mv2.Scalar);
            
            var scalarList = new GaMultivectorSparseList(mv2.GaSpaceDimension)
            {
                [0] = mv1,
                [mv2.Id] = mv2.Scalar
            };
            
            return new GaMultivector(mv2.BasisSet, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator +(GaTerm mv1, GaTerm mv2)
        {
            if (mv1.BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            if (mv1.Id == mv2.Id)
                return mv2.BasisSet.CreateTerm(mv1.Id, mv1.Scalar + mv2.Scalar);

            var scalarList = new GaMultivectorSparseList(mv2.GaSpaceDimension)
            {
                [mv1.Id] = mv1.Scalar,
                [mv2.Id] = mv2.Scalar
            };

            return new GaMultivector(mv1.BasisSet, scalarList);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator -(GaTerm mv1, double mv2)
        {
            if (mv1.Id == 0)
                return mv1.BasisSet.CreateScalar(mv1.Scalar - mv2);

            var scalarList = new GaMultivectorSparseList(mv1.GaSpaceDimension)
            {
                [0] = -mv2,
                [mv1.Id] = mv1.Scalar
            };

            return new GaMultivector(mv1.BasisSet, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator -(double mv1, GaTerm mv2)
        {
            if (mv2.Id == 0)
                return mv2.BasisSet.CreateScalar(mv1 - mv2.Scalar);
            
            var scalarList = new GaMultivectorSparseList(mv2.GaSpaceDimension)
            {
                [0] = mv1,
                [mv2.Id] = -mv2.Scalar
            };
            
            return new GaMultivector(mv2.BasisSet, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator -(GaTerm mv1, GaTerm mv2)
        {
            if (mv1.BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            if (mv1.Id == mv2.Id)
                return mv2.BasisSet.CreateTerm(mv1.Id, mv1.Scalar - mv2.Scalar);

            var scalarList = new GaMultivectorSparseList(mv2.GaSpaceDimension)
            {
                [mv1.Id] = mv1.Scalar,
                [mv2.Id] = -mv2.Scalar
            };

            return new GaMultivector(mv1.BasisSet, scalarList);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm operator *(GaTerm mv1, double mv2)
        {
            return new GaTerm(
                mv1.BasisSet,
                mv1.Id,
                mv1.Scalar * mv2
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm operator *(double mv1, GaTerm mv2)
        {
            return new GaTerm(
                mv2.BasisSet,
                mv2.Id,
                mv1 * mv2.Scalar
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm operator /(GaTerm mv1, double mv2)
        {
            return new GaTerm(
                mv1.BasisSet,
                mv1.Id,
                mv1.Scalar / mv2
            );
        }


        public BasisBladeSet BasisSet { get; }
        
        public uint VSpaceDimension 
            => BasisSet.VSpaceDimension;

        public ulong GaSpaceDimension 
            => BasisSet.GaSpaceDimension;

        public ulong Id { get; }

        public uint Grade { get; }

        public ulong Index { get; }

        public double Scalar { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GaTerm([NotNull] BasisBladeSet basisSet, ulong id, double scalar)
        {
            Debug.Assert(
                id < basisSet.GaSpaceDimension && 
                scalar.IsValid()
            );

            BasisSet = basisSet;
            Id = id;
            (Grade, Index) = BasisBladeDataLookup.BasisBladeGradeIndex(id);
            Scalar = scalar;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GaTerm([NotNull] BasisBladeSet basisSet, uint grade, ulong index, double scalar)
        {
            BasisSet = basisSet;
            Id = BasisBladeDataLookup.BasisBladeId(grade, index);
            Grade = grade;
            Index = index;
            Scalar = scalar;

            Debug.Assert(
                Id < basisSet.GaSpaceDimension && 
                scalar.IsValid()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Id < BasisSet.GaSpaceDimension &&
                   Scalar.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero()
        {
            return Scalar == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsScalar()
        {
            return Id == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<double> GetScalars(uint grade)
        {
            yield return Scalar;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<ulong, double>> GetIdScalarPairs(uint grade)
        {
            if (grade == Grade)
                yield return new KeyValuePair<ulong, double>(Id, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaTerm> GetTerms()
        {
            yield return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaTerm> GetTermsOrderedByGrade()
        {
            yield return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm ScalarPart()
        {
            return Id == 0
                ? this
                : new GaTerm(BasisSet, 0, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm VectorPart()
        {
            return Grade == 1
                ? this
                : new GaTerm(BasisSet, 1, 0, 0);
        }
        
        public double[] VectorPartAsArray()
        {
            var scalarsArray = new double[VSpaceDimension];

            if (Grade == 1)
                scalarsArray[Index] = Scalar;

            return scalarsArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm BivectorPart()
        {
            return Grade == 2
                ? this
                : new GaTerm(BasisSet, 2, 0, 0);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm KVectorPart(uint grade)
        {
            return Grade == grade
                ? this
                : new GaTerm(BasisSet, grade, 0, 0);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm ScalarBivectorPart()
        {
            return Grade is 0 or 2
                ? this
                : new GaTerm(BasisSet, 0, 0);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm EvenPart()
        {
            return Grade.IsEven()
                ? this
                : new GaTerm(BasisSet, 0, 0);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm OddPart()
        {
            return Grade.IsOdd()
                ? this
                : new GaTerm(BasisSet, 1, 0, 0);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm GradeInvolution()
        {
            return Grade.GradeInvolutionIsNegativeOfGrade()
                ? -this : this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm Conjugate()
        {
            return BasisSet.ConjugateIsNegative(Id)
                ? -this : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm Reverse()
        {
            return Grade.ReverseIsNegativeOfGrade()
                ? -this : this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm CliffordConjugate()
        {
            return Grade.CliffordConjugateIsNegativeOfGrade()
                ? -this : this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ENorm()
        {
            return Math.Abs(Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Norm()
        {
            return Math.Abs(Scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ENormSquared()
        {
            return Scalar * Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NormSquared()
        {
            var signature = BasisSet.NormSquaredSign(Id);

            return signature == 0
                ? 0
                : signature * Scalar * Scalar;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ESpSquared()
        {
            var signature = BasisSet.ESpSquaredSign(Id);

            return signature * Scalar * Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double SpSquared()
        {
            var signature = BasisSet.SpSquaredSign(Id);

            return signature == 0
                ? 0
                : signature * Scalar * Scalar;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm EGpSquared()
        {
            var signature = BasisSet.EGpSquaredSign(Id);

            return new GaTerm(BasisSet, 0, signature * Scalar * Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm GpSquared()
        {
            var signature = BasisSet.GpSquaredSign(Id);
            
            return signature == 0 
                ? new GaTerm(BasisSet, 0, 0) 
                : new GaTerm(BasisSet, 0, signature * Scalar * Scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm EGpReverse()
        {
            return new GaTerm(BasisSet, 0, Scalar * Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm GpReverse()
        {
            var signature = BasisSet.GpReverseSign(Id);
            
            return signature == 0 
                ? new GaTerm(BasisSet, 0, 0) 
                : new GaTerm(BasisSet, 0, signature * Scalar * Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm EBladeInverse()
        {
            return this / ESpSquared();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm BladeInverse()
        {
            return this / SpSquared();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm EInverse()
        {
            return Reverse() / ENormSquared();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm PseudoInverse()
        {
            return Reverse() / NormSquared();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm Inverse()
        {
            var conjugate = Conjugate();

            return conjugate / conjugate.Sp(this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm EDual()
        {
            return EGp(BasisSet.PseudoScalarEInverse);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm Dual()
        {
            return Gp(BasisSet.PseudoScalarInverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm EUnDual()
        {
            return EGp(BasisSet.PseudoScalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm UnDual()
        {
            return Gp(BasisSet.PseudoScalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ESp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            if (Id != mv2.Id) return 0;

            var signature = BasisSet.ESpSquaredSign(mv2.Id);

            return signature * Scalar * mv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ESp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.ESpSquaredSign(Id);

            return signature * Scalar * mv2.ScalarList[Id];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            if (Id != mv2.Id) return 0;
            
            var signature = BasisSet.SpSquaredSign(Id);

            return signature == 0 
                ? 0d 
                : signature * Scalar * mv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.SpSquaredSign(Id);

            if (signature == 0) return 0;

            return signature * Scalar * mv2.ScalarList[Id];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm Op(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.OpSign(Id, mv2.Id);

            return signature == 0 
                ? new GaTerm(BasisSet, 0, 0) 
                : new GaTerm(BasisSet, Id ^ mv2.Id, signature * Scalar * mv2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Op(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetOpIdScalarRecords(this, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm ELcp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.ELcpSign(Id, mv2.Id);

            return signature == 0 
                ? new GaTerm(BasisSet, 0, 0) 
                : new GaTerm(BasisSet, Id ^ mv2.Id, signature * Scalar * mv2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector ELcp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetELcpIdScalarRecords(this, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm Lcp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.LcpSign(Id, mv2.Id);

            return signature == 0 
                ? new GaTerm(BasisSet, 0, 0) 
                : new GaTerm(BasisSet, Id ^ mv2.Id, signature * Scalar * mv2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Lcp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetLcpIdScalarRecords(this, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm ERcp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.ERcpSign(Id, mv2.Id);

            return signature == 0 
                ? new GaTerm(BasisSet, 0, 0) 
                : new GaTerm(BasisSet, Id ^ mv2.Id, signature * Scalar * mv2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector ERcp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetERcpIdScalarRecords(this, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm Rcp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.RcpSign(Id, mv2.Id);

            return signature == 0 
                ? new GaTerm(BasisSet, 0, 0) 
                : new GaTerm(BasisSet, Id ^ mv2.Id, signature * Scalar * mv2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Rcp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetRcpIdScalarRecords(this, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm EFdp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.EFdpSign(Id, mv2.Id);

            return signature == 0 
                ? new GaTerm(BasisSet, 0, 0) 
                : new GaTerm(BasisSet, Id ^ mv2.Id, signature * Scalar * mv2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EFdp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetEFdpIdScalarRecords(this, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm Fdp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.FdpSign(Id, mv2.Id);

            return signature == 0 
                ? new GaTerm(BasisSet, 0, 0) 
                : new GaTerm(BasisSet, Id ^ mv2.Id, signature * Scalar * mv2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Fdp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetFdpIdScalarRecords(this, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm EHip(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.EHipSign(Id, mv2.Id);

            return signature == 0 
                ? new GaTerm(BasisSet, 0, 0) 
                : new GaTerm(BasisSet, Id ^ mv2.Id, signature * Scalar * mv2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EHip(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetEHipIdScalarRecords(this, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm Hip(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.HipSign(Id, mv2.Id);

            return signature == 0 
                ? new GaTerm(BasisSet, 0, 0) 
                : new GaTerm(BasisSet, Id ^ mv2.Id, signature * Scalar * mv2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Hip(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetHipIdScalarRecords(this, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm EAcp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.EAcpSign(Id, mv2.Id);

            return signature == 0 
                ? new GaTerm(BasisSet, 0, 0) 
                : new GaTerm(BasisSet, Id ^ mv2.Id, signature * Scalar * mv2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EAcp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetEAcpIdScalarRecords(this, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm Acp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.AcpSign(Id, mv2.Id);

            return signature == 0 
                ? new GaTerm(BasisSet, 0, 0) 
                : new GaTerm(BasisSet, Id ^ mv2.Id, signature * Scalar * mv2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Acp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetAcpIdScalarRecords(this, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm ECp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.ECpSign(Id, mv2.Id);

            return signature == 0 
                ? new GaTerm(BasisSet, 0, 0) 
                : new GaTerm(BasisSet, Id ^ mv2.Id, signature * Scalar * mv2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector ECp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetECpIdScalarRecords(this, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm Cp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.CpSign(Id, mv2.Id);

            return signature == 0 
                ? new GaTerm(BasisSet, 0, 0) 
                : new GaTerm(BasisSet, Id ^ mv2.Id, signature * Scalar * mv2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Cp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetCpIdScalarRecords(this, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm EGp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.EGpSign(Id, mv2.Id);

            return new GaTerm(BasisSet, Id ^ mv2.Id, signature * Scalar * mv2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EGp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetEGpIdScalarRecords(this, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm Gp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.GpSign(Id, mv2.Id);

            return signature == 0 
                ? new GaTerm(BasisSet, 0, 0) 
                : new GaTerm(BasisSet, Id ^ mv2.Id, signature * Scalar * mv2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Gp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetGpIdScalarRecords(this, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm EGpReverse(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.EGpReverseSign(Id, mv2.Id);

            return new GaTerm(BasisSet, Id ^ mv2.Id, signature * Scalar * mv2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EGpReverse(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetEGpReverseIdScalarRecords(this, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm GpReverse(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.GpReverseSign(Id, mv2.Id);

            return signature == 0 
                ? new GaTerm(BasisSet, 0, 0) 
                : new GaTerm(BasisSet, Id ^ mv2.Id, signature * Scalar * mv2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector GpReverse(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetGpReverseIdScalarRecords(this, mv2.ScalarList)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return this.GetTermText();
        }
    }
}