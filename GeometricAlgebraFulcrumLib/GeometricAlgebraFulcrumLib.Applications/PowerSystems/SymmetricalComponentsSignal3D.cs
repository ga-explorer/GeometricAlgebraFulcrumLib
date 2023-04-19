using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Coordinates;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Differential.Functions.Phasors;
using GeometricAlgebraFulcrumLib.MathBase.Signals;

namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems
{
    public class SymmetricalComponentsSignal3D :
        PowerSignal3D
    {
        public static SymmetricalComponentsSignal3D Create(ScalarSignalFloat64 timeValues, double frequency, IPolarPosition2D phasor1, IPolarPosition2D phasor2, IPolarPosition2D phasor3)
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

    
        public DfCosPhasor CosFunc1 { get; }

        public DfCosPhasor CosFunc2 { get; }

        public DfSinPhasor SinFunc2 { get; }

        public DfCosPhasor CosFunc3 { get; }

        public DfSinPhasor SinFunc3 { get; }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SymmetricalComponentsSignal3D(PowerSignal3D signal, DfCosPhasor cosFunc1, DfCosPhasor cosFunc2, DfSinPhasor sinFunc2, DfCosPhasor cosFunc3, DfSinPhasor sinFunc3)
            : base(signal)
        {
            CosFunc1 = cosFunc1;
            CosFunc2 = cosFunc2;
            SinFunc2 = sinFunc2;
            CosFunc3 = cosFunc3;
            SinFunc3 = sinFunc3;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Triplet<Float64Tuple3D> GetComponentVectors(double t)
        {
            var x = new Float64Tuple3D(0, 0, CosFunc1.GetValue(t));
            var y = new Float64Tuple3D(CosFunc2.GetValue(t), SinFunc2.GetValue(t), 0);
            var z = new Float64Tuple3D(CosFunc3.GetValue(t), SinFunc3.GetValue(t), 0);

            return new Triplet<Float64Tuple3D>(x, y, z);
        }
    
    }
}