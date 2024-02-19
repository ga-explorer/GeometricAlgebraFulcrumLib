﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps.Reflectors;

public abstract class XGaFloat64ReflectorBase : 
    XGaFloat64VersorBase, 
    IXGaFloat64Reflector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaFloat64ReflectorBase(XGaFloat64Processor metric)
        : base(metric)
    {
    }


    public abstract IXGaFloat64Reflector GetReflectorInverse();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaFloat64Versor GetVersorInverse()
    {
        return GetReflectorInverse();
    }
}