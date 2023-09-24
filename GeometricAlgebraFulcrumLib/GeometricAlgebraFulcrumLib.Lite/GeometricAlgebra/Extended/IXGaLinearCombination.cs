using DataStructuresLib.IndexSets;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended;

public interface IXGaLinearCombination
{
    bool IsEmpty { get; }

    bool IsOutputScalar();

    bool IsOutputVector();
    
    bool IsOutputBivector();
    
    bool IsOutputKVector();

    bool IsOutputKVector(int grade);

    IReadOnlyList<IIndexSet> GetInputBasisBladeIDs();

    IReadOnlyList<IIndexSet> GetOutputBasisBladeIDs();

    IReadOnlyList<int> GetInputBasisBladeGrades();

    IReadOnlyList<int> GetOutputBasisBladeGrades();
}