using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.Expression;

namespace GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;

public interface ISymbolicVector : ISymbolicObject, IEnumerable<MathematicaScalar>
{
    int Size { get; }

    MathematicaScalar this[int index] { get; }


    bool IsFullVector();

    bool IsSparseVector();


    ISymbolicVector Times(ISymbolicMatrix m);


    MathematicaVector ToMathematicaVector();

    MathematicaVector ToMathematicaFullVector();

    MathematicaVector ToMathematicaSparseVector();
}