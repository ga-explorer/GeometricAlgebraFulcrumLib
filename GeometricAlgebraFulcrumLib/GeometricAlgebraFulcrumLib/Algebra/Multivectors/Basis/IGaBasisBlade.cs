using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Storage.Terms;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis
{
    public interface IGaBasisBlade : 
        IEquatable<IGaBasisBlade>
    {
        ulong Id { get; }

        uint Grade { get; }

        ulong Index { get; }
        
        bool IsUniform { get; }
        
        bool IsGraded { get; }

        bool IsFull { get; }

        Tuple<uint, ulong> GetGradeIndex();

        Tuple<ulong, uint, ulong> GetIdGradeIndex();

        IReadOnlyList<ulong> GetBasisVectorIndices();

        void GetGradeIndex(out uint grade, out ulong index);

        void GetIdGradeIndex(out ulong id, out uint grade, out ulong index);
        
        IEnumerable<ulong> GetBasisVectorsIndices();

        GaTerm<T> CreateTerm<T>(T scalar);
    }
}