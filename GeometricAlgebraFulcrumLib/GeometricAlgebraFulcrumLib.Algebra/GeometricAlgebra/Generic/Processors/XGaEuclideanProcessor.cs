using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Spaces;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;

public class XGaEuclideanProcessor<T> :
    XGaProcessor<T>
{
    internal XGaEuclideanProcessor(IScalarProcessor<T> scalarProcessor)
        : base(scalarProcessor, 0, 0)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaEuclideanSpace<T> CreateSpace(int vSpaceDimensions)
    {
        return new XGaEuclideanSpace<T>(this, vSpaceDimensions);
    }
}