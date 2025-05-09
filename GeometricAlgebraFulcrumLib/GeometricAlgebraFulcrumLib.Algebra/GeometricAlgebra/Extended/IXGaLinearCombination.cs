using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended;

public interface IXGaLinearCombination
{
    bool IsEmpty { get; }

    bool IsOutputScalar();

    bool IsOutputVector();
    
    bool IsOutputBivector();
    
    bool IsOutputKVector();

    bool IsOutputKVector(int grade);

    IReadOnlyList<IndexSet> GetInputBasisBladeIDs();

    IReadOnlyList<IndexSet> GetOutputBasisBladeIDs();

    IReadOnlyList<int> GetInputBasisBladeGrades();

    IReadOnlyList<int> GetOutputBasisBladeGrades();
}