using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Terms;

namespace GeometricAlgebraFulcrumLib.Algebra.Basis
{
    public readonly struct GaBasisUniform 
        : IGaBasisBlade
    {
        public static GaBasisUniform ScalarBasis { get; }
            = new(0);


        public ulong Id { get; }

        public int Grade
            => Id.BasisBladeGrade();

        public ulong Index
            => Id.BasisBladeIndex();

        public bool IsUniform 
            => true;

        public bool IsGraded 
            => false;

        public bool IsFull 
            => false;


        public GaBasisUniform(ulong id)
        {
            Id = id;
        }
        
        public GaBasisUniform(int grade, ulong index)
        {
            Id = GaBasisUtils.BasisBladeId(grade, index);
        }
        
        public GaBasisUniform(Tuple<int, ulong> gradeIndexTuple)
        {
            var (grade, index) = gradeIndexTuple;

            Id = GaBasisUtils.BasisBladeId(grade, index);
        }
        
        public GaBasisUniform(IGaBasisBlade basisBlade)
        {
            Id = basisBlade.Id;
        }

        
        public Tuple<int, ulong> GetGradeIndex()
        {
            Id.BasisBladeGradeIndex(out var grade, out var index);

            return new Tuple<int, ulong>(grade, index);
        }

        public Tuple<ulong, int, ulong> GetIdGradeIndex()
        {
            Id.BasisBladeGradeIndex(out var grade, out var index);

            return new Tuple<ulong, int, ulong>(Id, grade, index);
        }

        public IReadOnlyList<ulong> GetBasisVectorIndices()
        {
            return Id.BasisVectorIDsInside().ToArray();
        }


        public void GetGradeIndex(out int grade, out ulong index)
        {
            Id.BasisBladeGradeIndex(out grade, out index);
        }

        public void GetIdGradeIndex(out ulong id, out int grade, out ulong index)
        {
            Id.BasisBladeGradeIndex(out grade, out index);

            id = Id;
        }

        public GaBasisUniform ToUniformBasisBlade()
        {
            return this;
        }

        public GaBasisGraded ToGradedBasisBlade()
        {
            return new(Id);
        }

        public GaBasisFull ToFullBasisBlade()
        {
            return new(Id);
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