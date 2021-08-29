using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices
{
    public interface ILaGradedStorage<out T> :
        ILaStorage<T>
    {
        int GradesCount { get; }

        IEnumerable<uint> GetGrades();
        
        uint GetMinGrade();

        uint GetMaxGrade();
        
        bool ContainsGrade(uint grade);
    }
}