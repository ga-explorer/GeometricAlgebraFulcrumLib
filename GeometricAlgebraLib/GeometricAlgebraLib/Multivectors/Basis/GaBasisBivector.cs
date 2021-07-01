using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DataStructuresLib.Collections.Lists;
using DataStructuresLib.Combinations;
using GeometricAlgebraLib.Multivectors.Terms;

namespace GeometricAlgebraLib.Multivectors.Basis
{
    public readonly struct GaBasisBivector 
        : IGaBasisBlade
    {
        public ulong BasisVectorIndex1 { get; }

        public ulong BasisVectorIndex2 { get; }

        public ulong Id 
            => (1UL << (int) BasisVectorIndex1) | (1UL << (int) BasisVectorIndex2);

        public int Grade => 2;

        public ulong Index 
            => BasisVectorIndex1 + ((BasisVectorIndex2 * (BasisVectorIndex2 - 1UL)) >> 1);

        public bool IsUniform 
            => false;

        public bool IsGraded 
            => true;

        public bool IsFull 
            => false;


        internal GaBasisBivector(int basisVectorIndex1, int basisVectorIndex2)
        {
            Debug.Assert(basisVectorIndex1 >= 0 && basisVectorIndex1 < basisVectorIndex2);

            BasisVectorIndex1 = (ulong) basisVectorIndex1;
            BasisVectorIndex2 = (ulong) basisVectorIndex2;
        }

        internal GaBasisBivector(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            Debug.Assert(basisVectorIndex1 < basisVectorIndex2);

            BasisVectorIndex1 = basisVectorIndex1;
            BasisVectorIndex2 = basisVectorIndex2;
        }

        internal GaBasisBivector(ulong index)
        {
            var (n1, n2) = BinaryCombinationsUtilsUInt64.IndexToCombinadic(index);

            BasisVectorIndex1 = n1;
            BasisVectorIndex2 = n2;
        }
        


        public Tuple<int, ulong> GetGradeIndex()
        {
            return new(2, Index);
        }

        public Tuple<ulong, int, ulong> GetIdGradeIndex()
        {
            return new(Id, 2, Index);
        }

        public IReadOnlyList<ulong> GetBasisVectorIndices()
        {
            return new ItemsPairAsReadOnlyList<ulong>(
                BasisVectorIndex1, 
                BasisVectorIndex2
            );
        }


        public void GetGradeIndex(out int grade, out ulong index)
        {
            grade = 2;
            index = Index;
        }

        public void GetIdGradeIndex(out ulong id, out int grade, out ulong index)
        {
            id = Id;
            grade = 2;
            index = Index;
        }

        public GaBasisUniform ToUniformBasisBlade()
        {
            return new(Id);
        }

        public GaBasisGraded ToGradedBasisBlade()
        {
            return new(2, Index);
        }

        public GaBasisFull ToFullBasisBlade()
        {
            return new(this);
        }


        public IEnumerable<ulong> GetBasisVectorsIndices()
        {
            yield return BasisVectorIndex1;
            yield return BasisVectorIndex2;
        }

        public GaTerm<T> CreateTerm<T>(T scalar)
        {
            return new(this, scalar);
        }


        public override string ToString()
        {
            return new StringBuilder()
                .Append('<')
                .Append(BasisVectorIndex1)
                .Append(',')
                .Append(BasisVectorIndex2)
                .Append('>')
                .ToString();
        }


        public bool Equals(IGaBasisBlade other)
        {
            return other is not null && Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            return obj is IGaBasisBlade other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(GaBasisBivector left, IGaBasisBlade right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GaBasisBivector left, IGaBasisBlade right)
        {
            return !left.Equals(right);
        }
    }
}