using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DataStructuresLib.Collections.Lists;
using GeometricAlgebraFulcrumLib.Storage.Terms;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis
{
    public sealed record GaBasisBivector 
        : IGaBasisBlade
    {
        public ulong BasisVectorIndex1 { get; }

        public ulong BasisVectorIndex2 { get; }

        public ulong Id 
            => (1UL << (int) BasisVectorIndex1) | (1UL << (int) BasisVectorIndex2);

        public uint Grade => 2;

        public ulong Index 
            => BasisVectorIndex1 + ((BasisVectorIndex2 * (BasisVectorIndex2 - 1UL)) >> 1);

        public bool IsUniform 
            => false;

        public bool IsGraded 
            => true;

        public bool IsFull 
            => false;


        internal GaBasisBivector(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            Debug.Assert(basisVectorIndex1 < basisVectorIndex2);

            BasisVectorIndex1 = basisVectorIndex1;
            BasisVectorIndex2 = basisVectorIndex2;
        }


        public Tuple<uint, ulong> GetGradeIndex()
        {
            return new(2, Index);
        }

        public Tuple<ulong, uint, ulong> GetIdGradeIndex()
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


        public void GetGradeIndex(out uint grade, out ulong index)
        {
            grade = 2;
            index = Index;
        }

        public void GetIdGradeIndex(out ulong id, out uint grade, out ulong index)
        {
            id = Id;
            grade = 2;
            index = Index;
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

        //public override bool Equals(object obj)
        //{
        //    return obj is IGaBasisBlade other && Equals(other);
        //}

        //public override int GetHashCode()
        //{
        //    return Id.GetHashCode();
        //}

        //public static bool operator ==(GaBasisBivector left, IGaBasisBlade right)
        //{
        //    return !ReferenceEquals(left, null) &&
        //           !ReferenceEquals(right, null) &&
        //            left.Equals(right);
        //}

        //public static bool operator !=(GaBasisBivector left, IGaBasisBlade right)
        //{
        //    return !(left == right);
        //}
    }
}