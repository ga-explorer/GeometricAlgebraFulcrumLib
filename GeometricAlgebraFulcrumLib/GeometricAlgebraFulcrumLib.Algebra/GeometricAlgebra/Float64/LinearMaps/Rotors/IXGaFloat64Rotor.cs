﻿namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Rotors;

public interface IXGaFloat64Rotor : 
    IXGaFloat64ScalingRotor
{
    IXGaFloat64Rotor GetRotorInverse();
}