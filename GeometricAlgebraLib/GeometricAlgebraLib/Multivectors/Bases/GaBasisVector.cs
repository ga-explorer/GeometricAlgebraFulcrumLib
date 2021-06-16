using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DataStructuresLib.Collections.Lists;
using GeometricAlgebraLib.Multivectors.Terms;

namespace GeometricAlgebraLib.Multivectors.Bases
{
    public readonly struct GaBasisVector 
        : IGaBasis
    {
        public ulong Id => 1UL << (int) Index;

        public int Grade => 1;

        public ulong Index { get; }

        public bool IsUniform 
            => false;

        public bool IsGraded 
            => true;

        public bool IsFull 
            => false;


        public GaBasisVector(int index)
        {
            Debug.Assert(index >= 0);

            Index = (ulong) index;
        }

        public GaBasisVector(ulong index)
        {
            Index = index;
        }


        public Tuple<int, ulong> GetGradeIndex()
        {
            return new(1, Index);
        }

        public Tuple<ulong, int, ulong> GetIdGradeIndex()
        {
            return new(1UL << (int) Index, 1, Index);
        }

        public IReadOnlyList<ulong> GetBasisVectorIndices()
        {
            return new ItemAsReadOnlyList<ulong>(Index);
        }


        public void GetGradeIndex(out int grade, out ulong index)
        {
            grade = 1;
            index = Index;
        }

        public void GetIdGradeIndex(out ulong id, out int grade, out ulong index)
        {
            id = 1UL << (int) Index;
            grade = 1;
            index = Index;
        }


        public GaBasisUniform ToUniformBasisBlade()
        {
            return new(1UL << (int) Index);
        }

        public GaBasisGraded ToGradedBasisBlade()
        {
            return new(1, Index);
        }

        public GaBasisFull ToFullBasisBlade()
        {
            return new(this);
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


        public bool Equals(IGaBasis other)
        {
            if (other is null)
                return false;

            other.GetGradeIndex(out var grade, out var index);

            return grade.Equals(1) && Index.Equals(index);
        }

        public override bool Equals(object obj)
        {
            return obj is IGaBasis other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(GaBasisVector left, IGaBasis right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GaBasisVector left, IGaBasis right)
        {
            return !left.Equals(right);
        }
    }
}