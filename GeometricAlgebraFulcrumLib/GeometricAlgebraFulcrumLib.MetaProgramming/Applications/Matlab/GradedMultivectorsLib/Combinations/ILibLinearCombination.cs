using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.Matlab.GradedMultivectorsLib.Types;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.Matlab.GradedMultivectorsLib.Combinations;

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