using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Frames;

public interface IXGaFloat64KVectorFrame :
    IReadOnlyList<XGaFloat64KVector>,
    IXGaFloat64Element
{
    int VSpaceDimensions { get; }
}