using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;

public interface IXGaLinearCombination
{
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