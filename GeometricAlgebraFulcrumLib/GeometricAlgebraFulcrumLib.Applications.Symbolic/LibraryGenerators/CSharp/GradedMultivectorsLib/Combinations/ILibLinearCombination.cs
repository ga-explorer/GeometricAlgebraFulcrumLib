using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib.Types;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib.Combinations;

public interface ILibLinearCombination
{
    int VSpaceDimensions { get; }

    int GaSpaceDimensions { get; }

    LibType OutputType { get; }

    IXGaLinearCombination InnerLinearCombination { get; }

    bool IsEmpty { get; }

    bool IsOutputScalar();

    bool IsOutputVector();

    bool IsOutputBivector();

    bool IsOutputKVector();

    bool IsOutputKVector(int grade);

    IReadOnlyList<IndexSet> GetInputBasisBladeIDs();

    IReadOnlyList<IndexSet> GetOutputBasisBladeIDs();

    IndexSet GetInputBasisBladeGrades();

    IndexSet GetOutputBasisBladeGrades();
}