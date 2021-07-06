using System;
using System.Collections.Generic;
using GeometricAlgebraLib.Algebra.Multivectors.Terms;

namespace GeometricAlgebraLib.Algebra.Basis
{
    public interface IGaBasisBlade : 
        IEquatable<IGaBasisBlade>
    {
        ulong Id { get; }

        int Grade { get; }

        ulong Index { get; }
        
        bool IsUniform { get; }
        
        bool IsGraded { get; }

        bool IsFull { get; }

        Tuple<int, ulong> GetGradeIndex();

        Tuple<ulong, int, ulong> GetIdGradeIndex();

        IReadOnlyList<ulong> GetBasisVectorIndices();

        void GetGradeIndex(out int grade, out ulong index);

        void GetIdGradeIndex(out ulong id, out int grade, out ulong index);

        GaBasisUniform ToUniformBasisBlade();

        GaBasisGraded ToGradedBasisBlade();

        GaBasisFull ToFullBasisBlade();

        IEnumerable<ulong> GetBasisVectorsIndices();

        GaTerm<T> CreateTerm<T>(T scalar);
    }
}