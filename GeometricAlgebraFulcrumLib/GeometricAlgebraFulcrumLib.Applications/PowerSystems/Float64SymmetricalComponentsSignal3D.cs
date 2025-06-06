﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Phasors;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems;

public class Float64SymmetricalComponentsSignal3D :
    Float64PowerSignal3D
{
    public static Float64SymmetricalComponentsSignal3D Create(Float64SampledTimeSignal timeValues, double frequency, ILinFloat64PolarVector2D phasor1, ILinFloat64PolarVector2D phasor2, ILinFloat64PolarVector2D phasor3)
    {
        var sqrt2Inv = 1d / 2d.Sqrt();

        // First component signal
        var cosFunc1 = DfCosPhasor.Create(phasor1.R, frequency, phasor1.Theta);
        
        // Second component signal
        var cosFunc2 = DfCosPhasor.Create(phasor2.R * sqrt2Inv, frequency, phasor2.Theta);
        var sinFunc2 = DfSinPhasor.Create(-phasor2.R * sqrt2Inv, frequency, phasor2.Theta);
        
        // Third component signal
        var cosFunc3 = DfCosPhasor.Create(phasor3.R * sqrt2Inv, frequency, phasor3.Theta);
        var sinFunc3 = DfSinPhasor.Create(phasor3.R * sqrt2Inv, frequency, phasor3.Theta);

        // (x,y,z) functions of final signal curve
        var scalarFuncX = cosFunc2 + cosFunc3;
        var scalarFuncY = sinFunc2 + sinFunc3;
        var scalarFuncZ = cosFunc1;

        var signal = Float64PowerSignal3D.Create(
            timeValues,
            scalarFuncX,
            scalarFuncY,
            scalarFuncZ
        );

        return new Float64SymmetricalComponentsSignal3D(
            signal,
            cosFunc1,
            cosFunc2,
            sinFunc2,
            cosFunc3,
            sinFunc3
        );
    }

    
    public DfCosPhasor CosFunc1 { get; }

    public DfCosPhasor CosFunc2 { get; }

    public DfSinPhasor SinFunc2 { get; }

    public DfCosPhasor CosFunc3 { get; }

    public DfSinPhasor SinFunc3 { get; }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SymmetricalComponentsSignal3D(Float64PowerSignal3D signal, DfCosPhasor cosFunc1, DfCosPhasor cosFunc2, DfSinPhasor sinFunc2, DfCosPhasor cosFunc3, DfSinPhasor sinFunc3)
        : base(signal)
    {
        CosFunc1 = cosFunc1;
        CosFunc2 = cosFunc2;
        SinFunc2 = sinFunc2;
        CosFunc3 = cosFunc3;
        SinFunc3 = sinFunc3;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Triplet<LinFloat64Vector3D> GetComponentVectors(double t)
    {
        var x = LinFloat64Vector3D.Create(0, 0, CosFunc1.GetValue(t));
        var y = LinFloat64Vector3D.Create(CosFunc2.GetValue(t), SinFunc2.GetValue(t), 0);
        var z = LinFloat64Vector3D.Create(CosFunc3.GetValue(t), SinFunc3.GetValue(t), 0);

        return new Triplet<LinFloat64Vector3D>(x, y, z);
    }
    
}