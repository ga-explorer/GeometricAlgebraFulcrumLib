using System;
using System.Collections.Generic;
using System.Text;
using GeometricAlgebraLib.Multivectors.Terms;

namespace GeometricAlgebraLib.Multivectors.Bases
{
    public readonly struct GaBasisScalar
        : IGaBasis
    {
        public static GaBasisScalar ScalarBasis { get; }
            = new GaBasisScalar();


        public ulong Id 
            => 0;

        public int Grade 
            => 0;

        public ulong Index 
            => 0;

        public bool IsUniform 
            => true;

        public bool IsGraded 
            => true;

        public bool IsFull 
            => true;



        public Tuple<int, ulong> GetGradeIndex()
        {
            return new(0, 0);
        }

        public Tuple<ulong, int, ulong> GetIdGradeIndex()
        {
            return new(0, 0, 0);
        }

        
        public void GetGradeIndex(out int grade, out ulong index)
        {
            grade = 0;
            index = 0;
        }

        public void GetIdGradeIndex(out ulong id, out int grade, out ulong index)
        {
            id = 0;
            grade = 0;
            index = 0;
        }


        public GaBasisUniform ToUniformBasisBlade()
        {
            return new(0);
        }

        public GaBasisGraded ToGradedBasisBlade()
        {
            return new(0, 0);
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
                .Append(0)
                .Append('>')
                .ToString();
        }


        public bool Equals(IGaBasis other)
        {
            if (other is null)
                return false;

            other.GetGradeIndex(out var grade, out var index);

            return grade.Equals(0) && index.Equals(0);
        }

        public override bool Equals(object obj)
        {
            return obj is IGaBasis other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(GaBasisScalar left, IGaBasis right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GaBasisScalar left, IGaBasis right)
        {
            return !left.Equals(right);
        }
    }
}