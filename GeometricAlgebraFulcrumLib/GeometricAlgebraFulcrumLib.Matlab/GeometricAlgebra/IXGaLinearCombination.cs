using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra;

public interface IXGaLinearCombination
{
    bool IsEmpty { get; }

    bool IsOutputScalar();

    bool IsOutputVector();
    
    bool IsOutputBivector();
    
    bool IsOutputKVector();

    bool IsOutputKVector(int grade);

    SortedSet<IndexSet> GetInputBasisBladeIDs();

    SortedSet<IndexSet> GetOutputBasisBladeIDs();

    IndexSet GetInputBasisBladeGrades();

    IndexSet GetOutputBasisBladeGrades();
}