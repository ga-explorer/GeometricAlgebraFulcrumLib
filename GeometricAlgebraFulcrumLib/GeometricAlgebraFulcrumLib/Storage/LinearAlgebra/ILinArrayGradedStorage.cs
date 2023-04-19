using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra
{
    public interface ILinArrayGradedStorage<out T> :
        ILinArrayStorage<T>
    {
        int GradesCount { get; }

        IEnumerable<uint> GetGrades();
        
        IEnumerable<uint> GetEmptyGrades(uint vSpaceDimensions);

        uint GetMinGrade();

        uint GetMaxGrade();
        
        bool ContainsGrade(uint grade);
    }
}