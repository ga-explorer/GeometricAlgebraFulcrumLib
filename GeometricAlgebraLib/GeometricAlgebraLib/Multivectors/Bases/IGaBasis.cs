using System;
using System.Collections.Generic;
using GeometricAlgebraLib.Multivectors.Terms;

namespace GeometricAlgebraLib.Multivectors.Bases
{
    public interface IGaBasis : IEquatable<IGaBasis>
    {
        ulong Id { get; }

        int Grade { get; }

        ulong Index { get; }
        
        bool IsUniform { get; }
        
        bool IsGraded { get; }

        bool IsFull { get; }

        Tuple<int, ulong> GetGradeIndex();

        Tuple<ulong, int, ulong> GetIdGradeIndex();

        void GetGradeIndex(out int grade, out ulong index);

        void GetIdGradeIndex(out ulong id, out int grade, out ulong index);

        GaBasisUniform ToUniformBasisBlade();

        GaBasisGraded ToGradedBasisBlade();

        GaBasisFull ToFullBasisBlade();

        IEnumerable<ulong> GetBasisVectorsIndices();

        GaTerm<T> CreateTerm<T>(T scalar);
    }
}