using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Rotors;

public abstract class XGaFloat64RotorBase : 
    XGaFloat64ScaledRotorBase, 
    IXGaFloat64Rotor
{
    protected XGaFloat64RotorBase(XGaFloat64Processor metric)
        : base(metric)
    {
    }


    public abstract IXGaFloat64Rotor GetRotorInverse();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaFloat64Versor GetVersorInverse()
    {
        return GetRotorInverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaFloat64ScaledRotor GetScaledRotorInverse()
    {
        return GetRotorInverse();
    }
}