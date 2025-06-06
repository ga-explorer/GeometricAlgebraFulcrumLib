﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.LinearMaps.Rotors;

public abstract class XGaFloat64ScaledRotorBase : 
    XGaFloat64VersorBase, 
    IXGaFloat64ScaledRotor
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaFloat64ScaledRotorBase(XGaFloat64Processor metric)
        : base(metric)
    {
    }


    public abstract double GetScalingFactor();

    public abstract IXGaFloat64ScaledRotor GetScaledRotorInverse();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaFloat64Versor GetVersorInverse()
    {
        return GetScaledRotorInverse();
    }
}