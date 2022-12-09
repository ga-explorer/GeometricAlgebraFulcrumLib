using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.SignalAlgebra;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Calculus;
using NumericalGeometryLib.BasicMath.Coordinates;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems;

public class SymmetricalComponentsSignal3D :
    PowerSignal3D
{
    public static SymmetricalComponentsSignal3D Create(ScalarSignalFloat64 timeValues, double frequency, IPolarPosition2D phasor1, IPolarPosition2D phasor2, IPolarPosition2D phasor3)
    {
        var sqrt2Inv = 1d / 2d.Sqrt();

        // First component signal
        var cosFunc1 = CosFunction.Create(phasor1.R, frequency, phasor1.Theta);
        
        // Second component signal
        var cosFunc2 = CosFunction.Create(phasor2.R * sqrt2Inv, frequency, phasor2.Theta);
        var sinFunc2 = SinFunction.Create(-phasor2.R * sqrt2Inv, frequency, phasor2.Theta);
        
        // Third component signal
        var cosFunc3 = CosFunction.Create(phasor3.R * sqrt2Inv, frequency, phasor3.Theta);
        var sinFunc3 = SinFunction.Create(phasor3.R * sqrt2Inv, frequency, phasor3.Theta);

        // (x,y,z) functions of final signal curve
        var scalarFuncX = SumD3Function.Create(cosFunc2, cosFunc3);
        var scalarFuncY = SumD3Function.Create(sinFunc2, sinFunc3);
        var scalarFuncZ = cosFunc1;

        var signal = PowerSignal3D.Create(
            timeValues,
            scalarFuncX,
            scalarFuncY,
            scalarFuncZ
        );

        return new SymmetricalComponentsSignal3D(
            signal,
            cosFunc1,
            cosFunc2,
            sinFunc2,
            cosFunc3,
            sinFunc3
        );
    }

    
    public CosFunction CosFunc1 { get; }

    public CosFunction CosFunc2 { get; }

    public SinFunction SinFunc2 { get; }

    public CosFunction CosFunc3 { get; }

    public SinFunction SinFunc3 { get; }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SymmetricalComponentsSignal3D(PowerSignal3D signal, CosFunction cosFunc1, CosFunction cosFunc2, SinFunction sinFunc2, CosFunction cosFunc3, SinFunction sinFunc3)
        : base(signal)
    {
        CosFunc1 = cosFunc1;
        CosFunc2 = cosFunc2;
        SinFunc2 = sinFunc2;
        CosFunc3 = cosFunc3;
        SinFunc3 = sinFunc3;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Triplet<Float64Tuple3D> GetPhaseVectors(double t)
    {
        var x = new Float64Tuple3D(0, 0, CosFunc1.GetValue(t));
        var y = new Float64Tuple3D(CosFunc2.GetValue(t), SinFunc2.GetValue(t), 0);
        var z = new Float64Tuple3D(CosFunc3.GetValue(t), SinFunc3.GetValue(t), 0);

        return new Triplet<Float64Tuple3D>(x, y, z);
    }
    
}