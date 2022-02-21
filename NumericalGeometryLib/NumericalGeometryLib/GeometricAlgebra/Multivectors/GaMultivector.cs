using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.GeometricAlgebra.Basis;
using NumericalGeometryLib.GeometricAlgebra.GuidedBinaryTraversal;
using NumericalGeometryLib.GeometricAlgebra.Structures;

namespace NumericalGeometryLib.GeometricAlgebra.Multivectors
{
    /// <summary>
    /// This class represents a general GA multivector
    /// TODO: Create another class for Multivectors over rational scalars
    /// </summary>
    public sealed class GaMultivector : 
        IGeometricElement,
        IReadOnlyDictionary<ulong, double>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator +(GaMultivector mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator -(GaMultivector mv)
        {
            return new GaMultivector(
                mv.BasisSet,
                mv.ScalarList.MapNumbers(n => -n)
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator +(GaMultivector mv1, double mv2)
        {
            var scalarList = new GaMultivectorSparseList(mv1.GaSpaceDimension);

            foreach (var (id, scalar) in mv1.ScalarList.StoredIdNumberPairs)
                scalarList[id] = scalar;

            scalarList[0] += mv2;

            return new GaMultivector(mv1.BasisSet, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator +(double mv1, GaMultivector mv2)
        {
            var scalarList = new GaMultivectorSparseList(mv2.GaSpaceDimension)
            {
                [0] = mv1
            };

            foreach (var (id, scalar) in mv2.ScalarList.StoredIdNumberPairs)
                scalarList[id] += scalar;
            
            return new GaMultivector(mv2.BasisSet, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator +(GaMultivector mv1, GaTerm mv2)
        {
            if (mv1.BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var scalarList = new GaMultivectorSparseList(mv1.GaSpaceDimension);

            foreach (var (id, scalar) in mv1.ScalarList.StoredIdNumberPairs)
                scalarList[id] = scalar;

            scalarList[mv2.Id] += mv2.Scalar;

            return new GaMultivector(mv1.BasisSet, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator +(GaTerm mv1, GaMultivector mv2)
        {
            if (mv1.BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var scalarList = new GaMultivectorSparseList(mv1.GaSpaceDimension)
            {
                [mv1.Id] = mv1.Scalar
            };

            foreach (var (id, scalar) in mv2.ScalarList.StoredIdNumberPairs)
                scalarList[id] += scalar;

            return new GaMultivector(mv1.BasisSet, scalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator +(GaMultivector mv1, GaMultivector mv2)
        {
            if (mv1.BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var scalarList = new GaMultivectorSparseList(mv1.GaSpaceDimension);

            foreach (var (id, scalar) in mv1.ScalarList.StoredIdNumberPairs)
                scalarList[id] = scalar;

            foreach (var (id, scalar) in mv2.ScalarList.StoredIdNumberPairs)
                scalarList[id] += scalar;

            return new GaMultivector(mv1.BasisSet, scalarList);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator -(GaMultivector mv1, double mv2)
        {
            var scalarList = new GaMultivectorSparseList(mv1.GaSpaceDimension);

            foreach (var (id, scalar) in mv1.ScalarList.StoredIdNumberPairs)
                scalarList[id] = scalar;

            scalarList[0] -= mv2;

            return new GaMultivector(mv1.BasisSet, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator -(double mv1, GaMultivector mv2)
        {
            var scalarList = new GaMultivectorSparseList(mv2.GaSpaceDimension)
            {
                [0] = mv1
            };

            foreach (var (id, scalar) in mv2.ScalarList.StoredIdNumberPairs)
                scalarList[id] -= scalar;
            
            return new GaMultivector(mv2.BasisSet, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator -(GaMultivector mv1, GaTerm mv2)
        {
            if (mv1.BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var scalarList = new GaMultivectorSparseList(mv1.GaSpaceDimension);

            foreach (var (id, scalar) in mv1.ScalarList.StoredIdNumberPairs)
                scalarList[id] = scalar;

            scalarList[mv2.Id] -= mv2.Scalar;

            return new GaMultivector(mv1.BasisSet, scalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator -(GaTerm mv1, GaMultivector mv2)
        {
            if (mv1.BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var scalarList = new GaMultivectorSparseList(mv1.GaSpaceDimension)
            {
                [mv1.Id] = mv1.Scalar
            };

            foreach (var (id, scalar) in mv2.ScalarList.StoredIdNumberPairs)
                scalarList[id] -= scalar;

            return new GaMultivector(mv1.BasisSet, scalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator -(GaMultivector mv1, GaMultivector mv2)
        {
            if (mv1.BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var scalarList = new GaMultivectorSparseList(mv1.GaSpaceDimension);

            foreach (var (id, scalar) in mv1.ScalarList.StoredIdNumberPairs)
                scalarList[id] = scalar;

            foreach (var (id, scalar) in mv2.ScalarList.StoredIdNumberPairs)
                scalarList[id] -= scalar;

            return new GaMultivector(mv1.BasisSet, scalarList);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator *(GaMultivector mv1, double mv2)
        {
            var scalarList = new GaMultivectorSparseList(mv1.GaSpaceDimension);

            foreach (var (id, scalar) in mv1.ScalarList.StoredIdNumberPairs)
                scalarList[id] = scalar * mv2;

            return new GaMultivector(mv1.BasisSet, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator *(double mv1, GaMultivector mv2)
        {
            var scalarList = new GaMultivectorSparseList(mv2.GaSpaceDimension);

            foreach (var (id, scalar) in mv2.ScalarList.StoredIdNumberPairs)
                scalarList[id] = mv1 * scalar;

            return new GaMultivector(mv2.BasisSet, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator /(GaMultivector mv1, double mv2)
        {
            mv2 = 1d / mv2;

            var scalarList = new GaMultivectorSparseList(mv1.GaSpaceDimension);

            foreach (var (id, scalar) in mv1.ScalarList.StoredIdNumberPairs)
                scalarList[id] = scalar * mv2;

            return new GaMultivector(mv1.BasisSet, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector operator /(double mv1, GaMultivector mv2)
        {
            return mv1 * mv2.Inverse();
        }


        
        internal GaMultivectorSparseList ScalarList { get; }


        public BasisBladeSet BasisSet { get; }

        public uint VSpaceDimension 
            => BasisSet.VSpaceDimension;

        public ulong GaSpaceDimension 
            => BasisSet.GaSpaceDimension;

        public IEnumerable<ulong> Keys 
            => ScalarList.StoredIDs;

        public IEnumerable<double> Values 
            => ScalarList.StoredNumbers;

        public int Count 
            => ScalarList.StoredPairsCount;

        public double this[ulong id]
        {
            get => ScalarList[id];
            set => ScalarList[id] = value;
        }

        public double this[uint grade, ulong index]
        {
            get
            {
                var id = BasisBladeDataLookup.BasisBladeId(grade, index);
                return ScalarList[id];
            }
            set
            {
                var id = BasisBladeDataLookup.BasisBladeId(grade, index);
                ScalarList[id] = value;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector([NotNull] BasisBladeSet basisSet)
        {
            BasisSet = basisSet;
            ScalarList = new GaMultivectorSparseList(basisSet.GaSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector([NotNull] GaTerm term)
        {
            BasisSet = term.BasisSet;
            ScalarList = new GaMultivectorSparseList(BasisSet.GaSpaceDimension)
            {
                [term.Id] = term.Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GaMultivector([NotNull] BasisBladeSet basisSet, [NotNull] GaMultivectorSparseList scalarList)
        {
            BasisSet = basisSet;
            ScalarList = scalarList;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return ScalarList.StoredNumbers.All(s => s.IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Clear()
        {
            ScalarList.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector RemoveNearZeroScalars()
        {
            ScalarList.RemoveNearZeroNumbers(BasisSet.ZeroEpsilon);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalar(ulong id)
        {
            return ScalarList.TryGetStoredNumber(id, out var scalar)
                ? scalar : 0d;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalar(uint grade, ulong index)
        {
            var id = BasisBladeDataLookup.BasisBladeId(grade, index);

            return ScalarList.TryGetStoredNumber(id, out var scalar)
                ? scalar : 0d;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm GetTerm(ulong id)
        {
            return ScalarList.TryGetStoredNumber(id, out var scalar)
                ? new GaTerm(BasisSet, id, scalar)
                : new GaTerm(BasisSet, id, 0d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaTerm GetTerm(uint grade, ulong index)
        {
            var id = BasisBladeDataLookup.BasisBladeId(grade, index);

            return ScalarList.TryGetStoredNumber(id, out var scalar)
                ? new GaTerm(BasisSet, id, scalar)
                : new GaTerm(BasisSet, id, 0d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(ulong id)
        {
            return ScalarList.ContainsStoredId(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong id, out double scalar)
        {
            return ScalarList.TryGetStoredNumber(id, out scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(ulong id, out double scalar)
        {
            return ScalarList.TryGetStoredNumber(id, out scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(uint grade, ulong index, out double scalar)
        {
            var id = BasisBladeDataLookup.BasisBladeId(grade, index);

            return ScalarList.TryGetStoredNumber(id, out scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetTerm(ulong id, out GaTerm term)
        {
            if (ScalarList.TryGetStoredNumber(id, out var scalar))
            {
                term = new GaTerm(BasisSet, id, scalar);
                return true;
            }

            term = null;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetTerm(uint grade, ulong index, out GaTerm term)
        {
            var id = BasisBladeDataLookup.BasisBladeId(grade, index);

            if (ScalarList.TryGetStoredNumber(id, out var scalar))
            {
                term = new GaTerm(BasisSet, id, scalar);
                return true;
            }

            term = null;
            return false;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero()
        {
            return ScalarList.Count == 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero()
        {
            return ScalarList
                .StoredNumbers
                .All(s => s.IsNearEqual(BasisSet.ZeroEpsilon));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsScalar()
        {
            if (ScalarList.Count == 0) return true;

            return ScalarList.StoredIdNumberPairs.All(
                pair =>
                    pair.Key == 0 || 
                    pair.Value.IsNearZero(BasisSet.ZeroEpsilon)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<double> GetScalars(uint grade)
        {
            return ScalarList
                .StoredIdNumberPairs
                .Where(p => BasisBladeDataLookup.BasisBladeGrade(p.Key) == grade)
                .Select(p => p.Value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<ulong, double>> GetIdScalarPairs(uint grade)
        {
            return ScalarList
                .StoredIdNumberPairs
                .Where(p => BasisBladeDataLookup.BasisBladeGrade(p.Key) == grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaTerm> GetTerms()
        {
            return ScalarList
                .StoredIdNumberPairs
                .Select(p =>
                    new GaTerm(BasisSet, p.Key, p.Value)
                );
        }

        public IEnumerable<GaTerm> GetTermsOrderedByGrade()
        {
            var termsArray = 
                GetTerms().ToArray();

            if (termsArray.Length == 0)
                return termsArray;

            var bitsCount = termsArray
                .Max(t => t.Id)
                .LastOneBitPosition() + 1;

            if (bitsCount == 0)
                return termsArray;

            return termsArray
                .Where(t => !t.Scalar.IsNearZero(BasisSet.ZeroEpsilon))
                .OrderBy(t => t.Grade)
                .ThenByDescending(t => t.Id.ReverseBits(bitsCount));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector ScalarPart()
        {
            return new GaMultivector(BasisSet)
            {
                [0] = this[0]
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector VectorPart()
        {
            var scalarList = 
                ScalarList.FilterById(
                    id => BasisBladeDataLookup.BasisBladeGrade(id) == 1
                );

            return new GaMultivector(BasisSet, scalarList);
        }
        
        public double[] VectorPartAsArray()
        {
            var scalarsArray = new double[VSpaceDimension];

            var idScalarPairs =
                ScalarList
                    .StoredIdNumberPairs
                    .Where(p => BasisBladeDataLookup.BasisBladeGrade(p.Key) == 1);

            foreach (var (id, scalar) in idScalarPairs)
            {
                var index = BitOperations.TrailingZeroCount(id);

                scalarsArray[index] = scalar;
            }

            return scalarsArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector BivectorPart()
        {
            var scalarList = 
                ScalarList.FilterById(
                    id => BasisBladeDataLookup.BasisBladeGrade(id) == 2
                );

            return new GaMultivector(BasisSet, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector KVectorPart(uint grade)
        {
            var scalarList = 
                ScalarList.FilterById(
                    id => BasisBladeDataLookup.BasisBladeGrade(id) == grade
                );

            return new GaMultivector(BasisSet, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector ScalarBivectorPart()
        {
            var scalarList = 
                ScalarList.FilterById(
                    id => BasisBladeDataLookup.BasisBladeGrade(id) is 0 or 2
                );

            return new GaMultivector(BasisSet, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EvenPart()
        {
            var scalarList = 
                ScalarList.FilterById(
                    id => BasisBladeDataLookup.BasisBladeGrade(id).IsEven()
                );

            return new GaMultivector(BasisSet, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector OddPart()
        {
            var scalarList = 
                ScalarList.FilterById(
                    id => BasisBladeDataLookup.BasisBladeGrade(id).IsOdd()
                );

            return new GaMultivector(BasisSet, scalarList);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector GradeInvolution()
        {
            var scalarList = ScalarList.MapNumbers(
                (id, scalar) => 
                    BasisBladeDataLookup.GradeInvolutionIsNegative(id) 
                        ? -scalar : scalar
            );

            return new GaMultivector(BasisSet, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Conjugate()
        {
            var scalarList = ScalarList.MapNumbers(
                (id, scalar) => 
                    BasisSet.ConjugateIsNegative(id) 
                        ? -scalar : scalar
            );

            return new GaMultivector(BasisSet, scalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Reverse()
        {
            var scalarList = ScalarList.MapNumbers(
                (id, scalar) => 
                    BasisBladeDataLookup.ReverseIsNegative(id) 
                        ? -scalar : scalar
            );

            return new GaMultivector(BasisSet, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector CliffordConjugate()
        {
            var scalarList = ScalarList.MapNumbers(
                (id, scalar) => 
                    BasisBladeDataLookup.CliffordConjugateIsNegative(id) 
                        ? -scalar : scalar
            );

            return new GaMultivector(BasisSet, scalarList);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ENorm()
        {
            return ENormSquared().Sqrt();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Norm()
        {
            return NormSquared().SqrtOfAbs();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ENormSquared()
        {
            return BasisSet.GetENormSquaredScalars(ScalarList).Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NormSquared()
        {
            return BasisSet.GetNormSquaredScalars(ScalarList).Sum();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ESpSquared()
        {
            return BasisSet.GetESpSquaredScalars(ScalarList).Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double SpSquared()
        {
            return BasisSet.GetSpSquaredScalars(ScalarList).Sum();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EGpSquared()
        {
            return BasisSet.SumToMultivector(
                BasisSet.GetEGpIdScalarRecords(ScalarList, ScalarList)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector GpSquared()
        {
            return BasisSet.SumToMultivector(
                BasisSet.GetGpIdScalarRecords(ScalarList, ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EGpReverse()
        {
            return BasisSet.SumToMultivector(
                BasisSet.GetEGpReverseIdScalarRecords(ScalarList, ScalarList)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector GpReverse()
        {
            return BasisSet.SumToMultivector(
                BasisSet.GetGpReverseIdScalarRecords(ScalarList, ScalarList)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EBladeInverse()
        {
            return this / ESpSquared();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector BladeInverse()
        {
            return this / SpSquared();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EInverse()
        {
            return Reverse() / ENormSquared();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector PseudoInverse()
        {
            var conjugate = Conjugate();

            return conjugate / conjugate.Sp(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Inverse()
        {
            return Reverse() / NormSquared();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EDual()
        {
            return EGp(BasisSet.PseudoScalarEInverse);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Dual()
        {
            return Gp(BasisSet.PseudoScalarInverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EUnDual()
        {
            return EGp(BasisSet.PseudoScalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector UnDual()
        {
            return Gp(BasisSet.PseudoScalar);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ESp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.ESpSquaredSign(mv2.Id);

            return signature * this[mv2.Id] * mv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ESp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.GetESpScalars(ScalarList, mv2.ScalarList).Sum();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            var signature = BasisSet.SpSquaredSign(mv2.Id);

            if (signature == 0) return 0d;

            return signature * this[mv2.Id] * mv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.GetSpScalars(ScalarList, mv2.ScalarList).Sum();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Op(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetOpIdScalarRecords(ScalarList, mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Op(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();
            
            return BasisSet.SumToMultivector(
                BasisSet.GetOpIdScalarRecords(ScalarList, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector ELcp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetELcpIdScalarRecords(ScalarList, mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector ELcp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetELcpIdScalarRecords(ScalarList, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Lcp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetLcpIdScalarRecords(ScalarList, mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Lcp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetLcpIdScalarRecords(ScalarList, mv2.ScalarList)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector ERcp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetERcpIdScalarRecords(ScalarList, mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector ERcp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetERcpIdScalarRecords(ScalarList, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Rcp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetRcpIdScalarRecords(ScalarList, mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Rcp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetRcpIdScalarRecords(ScalarList, mv2.ScalarList)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EFdp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetEFdpIdScalarRecords(ScalarList, mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EFdp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetEFdpIdScalarRecords(ScalarList, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Fdp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetFdpIdScalarRecords(ScalarList, mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Fdp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetFdpIdScalarRecords(ScalarList, mv2.ScalarList)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EHip(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetEHipIdScalarRecords(ScalarList, mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EHip(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetEHipIdScalarRecords(ScalarList, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Hip(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetHipIdScalarRecords(ScalarList, mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Hip(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetHipIdScalarRecords(ScalarList, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EAcp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetEAcpIdScalarRecords(ScalarList, mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EAcp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetEAcpIdScalarRecords(ScalarList, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Acp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetAcpIdScalarRecords(ScalarList, mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Acp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetAcpIdScalarRecords(ScalarList, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector ECp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetECpIdScalarRecords(ScalarList, mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector ECp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetECpIdScalarRecords(ScalarList, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Cp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetCpIdScalarRecords(ScalarList, mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Cp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetCpIdScalarRecords(ScalarList, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EGp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetEGpIdScalarRecords(ScalarList, mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EGp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetEGpIdScalarRecords(ScalarList, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Gp(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetGpIdScalarRecords(ScalarList, mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector Gp(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetGpIdScalarRecords(ScalarList, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EGpReverse(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetEGpReverseIdScalarRecords(ScalarList, mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector EGpReverse(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetEGpReverseIdScalarRecords(ScalarList, mv2.ScalarList)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector GpReverse(GaTerm mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetGpReverseIdScalarRecords(ScalarList, mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector GpReverse(GaMultivector mv2)
        {
            if (BasisSet.BasisSetSignature != mv2.BasisSet.BasisSetSignature)
                throw new InvalidOperationException();

            return BasisSet.SumToMultivector(
                BasisSet.GetGpReverseIdScalarRecords(ScalarList, mv2.ScalarList)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector DivideByNorm()
        {
            return this / Norm();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector DivideByENorm()
        {
            return this / ENorm();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector DivideByNormSquared()
        {
            return this / NormSquared();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector DivideByENormSquared()
        {
            return this / ENormSquared();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGbtMultivectorBinaryTrieStack CreateGbtStack(int capacity)
        {
            return BasisSet.CreateGbtMultivectorStack(
                capacity, 
                ScalarList.CreateBinaryTrie()
            );
        }
        
        /// <summary>
        /// Construct a binary tree representation of this storage
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivectorSparseList GetSparseList()
        {
            return ScalarList;
        }

        /// <summary>
        /// Construct a binary tree representation of this storage
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivectorBinaryTrie GetBinaryTrie()
        {
            return ScalarList.CreateBinaryTrie();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple2D GetVectorPartAsTuple2D()
        {
            return new Tuple2D(
                ScalarList[1],
                ScalarList[2]
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetVectorPartAsTuple3D()
        {
            return new Tuple3D(
                ScalarList[1],
                ScalarList[2],
                ScalarList[4]
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<KeyValuePair<ulong, double>> GetEnumerator()
        {
            return ScalarList.StoredIdNumberPairs.GetEnumerator();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return this.GetMultivectorText();
        }
    }
}
