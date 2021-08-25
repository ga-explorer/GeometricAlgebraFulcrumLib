using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Graded
{
    public interface IGaListGradedSingleGrade<T> :
        IGaListGraded<T>
    {
        uint Grade { get; }
        
        IGaListEven<T> EvenList { get; }
    }
}