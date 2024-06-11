using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted;
using GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib.Types;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib.Combinations;

public interface ILibLinearCombination
{
    int VSpaceDimensions { get; }

    int GaSpaceDimensions { get; }

    LibType OutputType { get; }

    IRGaLinearCombination InnerLinearCombination { get; }

    bool IsEmpty { get; }

    bool IsOutputScalar();

    bool IsOutputVector();

    bool IsOutputBivector();

    bool IsOutputKVector();

    bool IsOutputKVector(int grade);

    IReadOnlyList<int> GetInputBasisBladeIDs();

    IReadOnlyList<int> GetOutputBasisBladeIDs();

    IReadOnlyList<int> GetInputBasisBladeGrades();

    IReadOnlyList<int> GetOutputBasisBladeGrades();
}