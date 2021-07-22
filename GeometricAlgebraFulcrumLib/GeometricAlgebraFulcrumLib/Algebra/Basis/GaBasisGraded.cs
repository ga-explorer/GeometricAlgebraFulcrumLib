using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Terms;

namespace GeometricAlgebraFulcrumLib.Algebra.Basis
{
    public readonly struct GaBasisGraded 
        : IGaBasisBlade
    {
        public static GaBasisGraded ScalarBasis { get; }
            = new(0, 0);


        public ulong Id 
            => GaBasisUtils.BasisBladeId(Grade, Index);

        public uint Grade { get; }

        public ulong Index { get; }

        public bool IsUniform 
            => false;

        public bool IsGraded 
            => true;

        public bool IsFull 
            => false;

        
        internal GaBasisGraded(uint grade, ulong index)
        {
            Grade = grade;
            Index = index;
        }
        

        public Tuple<uint, ulong> GetGradeIndex()
        {
            return new(Grade, Index);
        }

        public Tuple<ulong, uint, ulong> GetIdGradeIndex()
        {
            return new(Id, Grade, Index);
        }

        public IReadOnlyList<ulong> GetBasisVectorIndices()
        {
            return Id.BasisVectorIDsInside().ToArray();
        }


        public void GetGradeIndex(out uint grade, out ulong index)
        {
            grade = Grade;
            index = Index;
        }

        public void GetIdGradeIndex(out ulong id, out uint grade, out ulong index)
        {
            id = Id;
            grade = Grade;
            index = Index;
        }

        
        public IEnumerable<ulong> GetBasisVectorsIndices()
        {
            return Id.PatternToPositions().Select(i => (ulong) i);
        }

        public GaTerm<T> CreateTerm<T>(T scalar)
        {
            return new(this, scalar);
        }


        public override string ToString()
        {
            var basisVectorIndicesText = 
                string.Join(',', GetBasisVectorsIndices());

            return new StringBuilder()
                .Append('<')
                .Append(basisVectorIndicesText)
                .Append('>')
                .ToString();
        }


        public bool Equals(IGaBasisBlade other)
        {
            if (other is null)
                return false;

            other.GetGradeIndex(out var grade, out var index);

            return Grade.Equals(grade) && Index.Equals(index);
        }

        public override bool Equals(object obj)
        {
            return obj is IGaBasisBlade other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(GaBasisGraded left, IGaBasisBlade right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GaBasisGraded left, IGaBasisBlade right)
        {
            return !left.Equals(right);
        }
    }
}