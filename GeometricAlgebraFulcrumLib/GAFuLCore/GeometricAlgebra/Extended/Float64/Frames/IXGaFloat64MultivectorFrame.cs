﻿using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Frames;

public interface IXGaFloat64MultivectorFrame :
    IReadOnlyList<XGaFloat64Multivector>,
    IXGaFloat64Element
{
    int VSpaceDimensions { get; }
}