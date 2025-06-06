﻿using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Frames;

public interface IRGaFloat64KVectorFrame :
    IReadOnlyList<RGaFloat64KVector>,
    IRGaFloat64Element
{
    int VSpaceDimensions { get; }
}