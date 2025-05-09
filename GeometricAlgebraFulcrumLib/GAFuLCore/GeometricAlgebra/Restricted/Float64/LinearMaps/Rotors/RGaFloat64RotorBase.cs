using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors;

public abstract class RGaFloat64RotorBase : 
    RGaFloat64ScaledRotorBase, 
    IRGaFloat64Rotor
{
    protected RGaFloat64RotorBase(RGaFloat64Processor metric)
        : base(metric)
    {
    }


    public abstract IRGaFloat64Rotor GetRotorInverse();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IRGaFloat64Versor GetVersorInverse()
    {
        return GetRotorInverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IRGaFloat64ScaledRotor GetScaledRotorInverse()
    {
        return GetRotorInverse();
    }
}