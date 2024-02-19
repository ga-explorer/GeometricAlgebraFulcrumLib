﻿using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Frames;

public interface IXGaFloat64VectorFrame :
    IReadOnlyList<XGaFloat64Vector>,
    IXGaFloat64Element
{
    int VSpaceDimensions { get; }
}