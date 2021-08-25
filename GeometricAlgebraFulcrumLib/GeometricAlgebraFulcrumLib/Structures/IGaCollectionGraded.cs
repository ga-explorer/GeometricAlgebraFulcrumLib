using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Structures
{
    public interface IGaCollectionGraded<out T> :
        IGaCollection<T>
    {
        int GradesCount { get; }

        IEnumerable<uint> GetGrades();
        
        uint GetMinGrade();

        uint GetMaxGrade();
        
        bool ContainsGrade(uint grade);


    }
}