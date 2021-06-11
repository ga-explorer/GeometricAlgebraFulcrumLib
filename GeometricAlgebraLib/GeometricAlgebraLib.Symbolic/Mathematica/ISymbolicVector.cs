using System.Collections.Generic;
using GeometricAlgebraLib.Symbolic.Mathematica.Expression;

namespace GeometricAlgebraLib.Symbolic.Mathematica
{
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
}
