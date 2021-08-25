using GeometricAlgebraFulcrumLib.Structures.Grids.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Graded
{
    public interface IGaGridGradedSingleGrade<T> :
        IGaGridGraded<T>
    {
        uint Grade { get; }
        
        IGaGridEven<T> EvenGrid { get; }
    }
}