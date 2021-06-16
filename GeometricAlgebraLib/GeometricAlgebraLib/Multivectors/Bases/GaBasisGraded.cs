using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStructuresLib;
using GeometricAlgebraLib.Frames;
using GeometricAlgebraLib.Multivectors.Terms;

namespace GeometricAlgebraLib.Multivectors.Bases
{
    public readonly struct GaBasisGraded 
        : IGaBasis
    {
        public static GaBasisGraded ScalarBasis { get; }
            = new(0, 0);


        public ulong Id 
            => GaFrameUtils.BasisBladeId(Grade, Index);

        public int Grade { get; }

        public ulong Index { get; }

        public bool IsUniform 
            => false;

        public bool IsGraded 
            => true;

        public bool IsFull 
            => false;


        public GaBasisGraded(ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            Grade = grade;
            Index = index;
        }
        
        public GaBasisGraded(int grade, ulong index)
        {
            Grade = grade;
            Index = index;
        }
        
        public GaBasisGraded(Tuple<int, ulong> gradeIndexTuple)
        {
            var (grade, index) = gradeIndexTuple;

            Grade = grade;
            Index = index;
        }
        
        public GaBasisGraded(IGaBasis basisBlade)
        {
            basisBlade.GetGradeIndex(out var grade, out var index);

            Grade = grade;
            Index = index;
        }


        public Tuple<int, ulong> GetGradeIndex()
        {
            return new(Grade, Index);
        }

        public Tuple<ulong, int, ulong> GetIdGradeIndex()
        {
            return new(Id, Grade, Index);
        }

        public IReadOnlyList<ulong> GetBasisVectorIndices()
        {
            return Id.BasisVectorIDsInside().ToArray();
        }


        public void GetGradeIndex(out int grade, out ulong index)
        {
            grade = Grade;
            index = Index;
        }

        public void GetIdGradeIndex(out ulong id, out int grade, out ulong index)
        {
            id = Id;
            grade = Grade;
            index = Index;
        }


        public GaBasisUniform ToUniformBasisBlade()
        {
            return new(Id);
        }

        public GaBasisGraded ToGradedBasisBlade()
        {
            return this;
        }

        public GaBasisFull ToFullBasisBlade()
        {
            return new(Grade, Index);
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


        public bool Equals(IGaBasis other)
        {
            if (other is null)
                return false;

            other.GetGradeIndex(out var grade, out var index);

            return Grade.Equals(grade) && Index.Equals(index);
        }

        public override bool Equals(object obj)
        {
            return obj is IGaBasis other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(GaBasisGraded left, IGaBasis right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GaBasisGraded left, IGaBasis right)
        {
            return !left.Equals(right);
        }
    }
}