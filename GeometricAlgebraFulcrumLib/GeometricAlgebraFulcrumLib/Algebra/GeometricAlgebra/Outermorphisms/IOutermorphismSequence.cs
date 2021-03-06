using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms
{
    public interface IOutermorphismSequence<T>
    {
        IEnumerable<IOutermorphism<T>> GetLeafOutermorphisms();
    }
}