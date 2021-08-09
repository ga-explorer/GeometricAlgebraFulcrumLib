using System;
using System.Collections.Generic;
using System.Text;
using GeometricAlgebraFulcrumLib.Storage.Terms;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis
{
    public sealed record GaBasisScalar
        : IGaBasisBlade
    {
        public ulong Id 
            => 0;

        public uint Grade 
            => 0;

        public ulong Index 
            => 0;

        public bool IsUniform 
            => true;

        public bool IsGraded 
            => true;

        public bool IsFull 
            => true;


        public Tuple<uint, ulong> GetGradeIndex()
        {
            return new(0, 0);
        }

        public Tuple<ulong, uint, ulong> GetIdGradeIndex()
        {
            return new(0, 0, 0);
        }

        public IReadOnlyList<ulong> GetBasisVectorIndices()
        {
            return new ulong[0];
        }


        public void GetGradeIndex(out uint grade, out ulong index)
        {
            grade = 0;
            index = 0;
        }

        public void GetIdGradeIndex(out ulong id, out uint grade, out ulong index)
        {
            id = 0;
            grade = 0;
            index = 0;
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


        public bool Equals(IGaBasisBlade other)
        {
            if (other is null)
                return false;

            other.GetGradeIndex(out var grade, out var index);

            return grade.Equals(0) && index.Equals(0);
        }

        //public override bool Equals(object obj)
        //{
        //    return obj is IGaBasisBlade other && Equals(other);
        //}

        //public override int GetHashCode()
        //{
        //    return Id.GetHashCode();
        //}

        //public static bool operator ==(GaBasisScalar left, IGaBasisBlade right)
        //{
        //    return left.Equals(right);
        //}

        //public static bool operator !=(GaBasisScalar left, IGaBasisBlade right)
        //{
        //    return !left.Equals(right);
        //}
    }
}