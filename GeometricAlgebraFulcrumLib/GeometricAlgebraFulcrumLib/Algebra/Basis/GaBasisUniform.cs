using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Terms;

namespace GeometricAlgebraFulcrumLib.Algebra.Basis
{
    public readonly struct GaBasisUniform 
        : IGaBasisBlade
    {
        public ulong Id { get; }

        public uint Grade
            => Id.BasisBladeGrade();

        public ulong Index
            => Id.BasisBladeIndex();

        public bool IsUniform 
            => true;

        public bool IsGraded 
            => false;

        public bool IsFull 
            => false;


        internal GaBasisUniform(ulong id)
        {
            Id = id;
        }
        
        
        public Tuple<uint, ulong> GetGradeIndex()
        {
            Id.BasisBladeGradeIndex(out var grade, out var index);

            return new Tuple<uint, ulong>(grade, index);
        }

        public Tuple<ulong, uint, ulong> GetIdGradeIndex()
        {
            Id.BasisBladeGradeIndex(out var grade, out var index);

            return new Tuple<ulong, uint, ulong>(Id, grade, index);
        }

        public IReadOnlyList<ulong> GetBasisVectorIndices()
        {
            return Id.BasisVectorIDsInside().ToArray();
        }


        public void GetGradeIndex(out uint grade, out ulong index)
        {
            Id.BasisBladeGradeIndex(out grade, out index);
        }

        public void GetIdGradeIndex(out ulong id, out uint grade, out ulong index)
        {
            Id.BasisBladeGradeIndex(out grade, out index);

            id = Id;
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

        public static bool operator ==(GaBasisUniform left, IGaBasisBlade right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GaBasisUniform left, IGaBasisBlade right)
        {
            return !left.Equals(right);
        }
    }
}