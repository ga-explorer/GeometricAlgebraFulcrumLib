using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.Matlab.GradedMultivectorsLib.Types;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.Matlab.GradedMultivectorsLib.Combinations;

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

    ImmutableSortedSet<IndexSet> GetInputBasisBladeIDs();

    ImmutableSortedSet<IndexSet> GetOutputBasisBladeIDs();

    IndexSet GetInputBasisBladeGrades();

    IndexSet GetOutputBasisBladeGrades();
}