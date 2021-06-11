using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DataStructuresLib.Combinations;
using GeometricAlgebraLib.Multivectors.Terms;

namespace GeometricAlgebraLib.Multivectors.Bases
{
    public readonly struct GaBasisBivector 
        : IGaBasis
    {
        public int BasisVectorIndex1 { get; }

        public int BasisVectorIndex2 { get; }

        public ulong Id 
            => (1UL << BasisVectorIndex1) | (1UL << BasisVectorIndex2);

        public int Grade => 2;

        public ulong Index
        {
            get
            {
                var n1 = (ulong) BasisVectorIndex1;
                var n2 = (ulong) BasisVectorIndex2;

                return n1 + ((n2 * (n2 - 1UL)) >> 1);
            }
        }

        public bool IsUniform 
            => false;

        public bool IsGraded 
            => true;

        public bool IsFull 
            => false;


        internal GaBasisBivector(int basisVectorIndex1, int basisVectorIndex2)
        {
            Debug.Assert(basisVectorIndex1 >= 0 && basisVectorIndex1 < basisVectorIndex2);

            BasisVectorIndex1 = basisVectorIndex1;
            BasisVectorIndex2 = basisVectorIndex2;
        }

        internal GaBasisBivector(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            Debug.Assert(basisVectorIndex1 < basisVectorIndex2);

            BasisVectorIndex1 = (int) basisVectorIndex1;
            BasisVectorIndex2 = (int) basisVectorIndex2;
        }

        internal GaBasisBivector(ulong index)
        {
            var (n1, n2) = BinaryCombinationsUtilsUInt64.IndexToCombinadic(index);

            BasisVectorIndex1 = (int) n1;
            BasisVectorIndex2 = (int) n2;
        }
        


        public Tuple<int, ulong> GetGradeIndex()
        {
            return new(2, Index);
        }

        public Tuple<ulong, int, ulong> GetIdGradeIndex()
        {
            return new(Id, 2, Index);
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
            yield return (ulong) BasisVectorIndex1;
            yield return (ulong) BasisVectorIndex2;
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


        public bool Equals(IGaBasis other)
        {
            return other is not null && Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            return obj is IGaBasis other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(GaBasisBivector left, IGaBasis right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GaBasisBivector left, IGaBasis right)
        {
            return !left.Equals(right);
        }
    }
}