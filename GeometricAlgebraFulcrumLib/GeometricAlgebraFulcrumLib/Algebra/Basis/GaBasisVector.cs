using System;
using System.Collections.Generic;
using System.Text;
using DataStructuresLib.Collections.Lists;
using GeometricAlgebraFulcrumLib.Algebra.Terms;

namespace GeometricAlgebraFulcrumLib.Algebra.Basis
{
    public readonly struct GaBasisVector 
        : IGaBasisBlade
    {
        public ulong Id => 1UL << (int) Index;

        public uint Grade => 1;

        public ulong Index { get; }

        public bool IsUniform 
            => false;

        public bool IsGraded 
            => true;

        public bool IsFull 
            => false;


        internal GaBasisVector(ulong index)
        {
            Index = index;
        }


        public Tuple<uint, ulong> GetGradeIndex()
        {
            return new(1, Index);
        }

        public Tuple<ulong, uint, ulong> GetIdGradeIndex()
        {
            return new(1UL << (int) Index, 1, Index);
        }

        public IReadOnlyList<ulong> GetBasisVectorIndices()
        {
            return new ItemAsReadOnlyList<ulong>(Index);
        }


        public void GetGradeIndex(out uint grade, out ulong index)
        {
            grade = 1;
            index = Index;
        }

        public void GetIdGradeIndex(out ulong id, out uint grade, out ulong index)
        {
            id = 1UL << (int) Index;
            grade = 1;
            index = Index;
        }


        public IEnumerable<ulong> GetBasisVectorsIndices()
        {
            yield return Index;
        }

        public GaTerm<T> CreateTerm<T>(T scalar)
        {
            return new(this, scalar);
        }


        public override string ToString()
        {
            return new StringBuilder()
                .Append('<')
                .Append(Index)
                .Append('>')
                .ToString();
        }


        public bool Equals(IGaBasisBlade other)
        {
            if (other is null)
                return false;

            other.GetGradeIndex(out var grade, out var index);

            return grade.Equals(1) && Index.Equals(index);
        }

        public override bool Equals(object obj)
        {
            return obj is IGaBasisBlade other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(GaBasisVector left, IGaBasisBlade right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GaBasisVector left, IGaBasisBlade right)
        {
            return !left.Equals(right);
        }
    }
}