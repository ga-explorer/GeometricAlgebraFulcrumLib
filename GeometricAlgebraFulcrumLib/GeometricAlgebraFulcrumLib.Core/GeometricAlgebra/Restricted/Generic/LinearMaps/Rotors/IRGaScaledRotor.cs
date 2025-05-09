﻿using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;

public interface IRGaScaledRotor<T> : 
    IRGaVersor<T>
{
    Scalar<T> GetScalingFactor();

    IRGaScaledRotor<T> GetScaledRotorInverse();
}