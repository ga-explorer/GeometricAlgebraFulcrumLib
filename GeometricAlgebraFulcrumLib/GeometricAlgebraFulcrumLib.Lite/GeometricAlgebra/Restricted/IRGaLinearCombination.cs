namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted;

public interface IRGaLinearCombination
{
    bool IsEmpty { get; }

    bool IsOutputScalar();

    bool IsOutputVector();
    
    bool IsOutputBivector();
    
    bool IsOutputKVector();

    bool IsOutputKVector(int grade);

    IReadOnlyList<ulong> GetInputBasisBladeIDs();

    IReadOnlyList<ulong> GetOutputBasisBladeIDs();

    IReadOnlyList<int> GetInputBasisBladeGrades();

    IReadOnlyList<int> GetOutputBasisBladeGrades();
}