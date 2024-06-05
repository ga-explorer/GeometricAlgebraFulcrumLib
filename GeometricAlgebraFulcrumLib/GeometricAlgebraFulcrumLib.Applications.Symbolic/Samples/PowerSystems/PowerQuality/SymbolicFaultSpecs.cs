using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.Samples.PowerSystems.PowerQuality
{
    public class SymbolicFaultSpecs
    {
        public static ScalarProcessorOfWolframExpr ScalarProcessor { get; }
            = ScalarProcessorOfWolframExpr.Instance;

        public static RGaProcessor<Expr> GeometricProcessor { get; }
            = ScalarProcessor.CreateEuclideanRGaProcessor();

        public static int VSpaceDimensions
            => 3;


        public static SymbolicFaultSpecs CreateNoFault(string frequency, string maxA)
        {
            var specs = new SymbolicFaultSpecs(
                "Normal operation (no fault)",
                frequency,
                maxA,
                "1",
                "0"
            );

            var va = $"Sqrt[2] {maxA} Cos[{frequency} t]".ToExpr();
            var vb = $"Sqrt[2] {maxA} Cos[{frequency} t - 2 Pi / 3]".ToExpr();
            var vc = $"Sqrt[2] {maxA} Cos[{frequency} t - 4 Pi / 3]".ToExpr();

            specs.DefineSignal(va, vb, vc);

            return specs;
        }

        public static SymbolicFaultSpecs CreateA2GFault(string frequency, string maxA, string maxB, string phaseAngleJump)
        {
            var specs = new SymbolicFaultSpecs(
                "Phase A to ground fault",
                frequency,
                maxA,
                maxB,
                phaseAngleJump
            );

            var va = $"Sqrt[2] * {maxA} * {maxB} * Cos[{frequency} t - {phaseAngleJump}]".ToExpr();
            var vb = $"Sqrt[2] * {maxA} * Cos[{frequency} t - 2 Pi / 3]".ToExpr();
            var vc = $"Sqrt[2] * {maxA} * Cos[{frequency} t - 4 Pi / 3]".ToExpr();

            specs.DefineSignal(va, vb, vc);

            return specs;
        }

        public static SymbolicFaultSpecs CreateB2GFault(string frequency, string maxA, string maxB, string phaseAngleJump)
        {
            var specs = new SymbolicFaultSpecs(
                "Phase B to ground fault",
                frequency,
                maxA,
                maxB,
                phaseAngleJump
            );

            var va = $"Sqrt[2] * {maxA} * Cos[{frequency} t]".ToExpr();
            var vb = $"Sqrt[2] * {maxA} * {maxB} * Cos[{frequency} t - 2 Pi / 3 - {phaseAngleJump}]".ToExpr();
            var vc = $"Sqrt[2] * {maxA} * Cos[{frequency} t - 4 Pi / 3]".ToExpr();

            specs.DefineSignal(va, vb, vc);

            return specs;
        }

        public static SymbolicFaultSpecs CreateC2GFault(string frequency, string maxA, string maxB, string phaseAngleJump)
        {
            var specs = new SymbolicFaultSpecs(
                "Phase C to ground fault",
                frequency,
                maxA,
                maxB,
                phaseAngleJump
            );

            var va = $"Sqrt[2] * {maxA} * Cos[{frequency} t]".ToExpr();
            var vb = $"Sqrt[2] * {maxA} * Cos[{frequency} t - 2 Pi / 3]".ToExpr();
            var vc = $"Sqrt[2] * {maxA} * {maxB} * Cos[{frequency} t - 4 Pi / 3 - {phaseAngleJump}]".ToExpr();

            specs.DefineSignal(va, vb, vc);

            return specs;
        }

        public static SymbolicFaultSpecs CreateAb2GFault(string frequency, string maxA, string maxB, string phaseAngleJump)
        {
            var specs = new SymbolicFaultSpecs(
                "Phase A,B to ground fault",
                frequency,
                maxA,
                maxB,
                phaseAngleJump
            );

            var va = $"Sqrt[2] * {maxA} * {maxB} * Cos[{frequency} t - {phaseAngleJump}]".ToExpr();
            var vb = $"Sqrt[2] * {maxA} * {maxB} * Cos[{frequency} t - 2 Pi / 3 - {phaseAngleJump}]".ToExpr();
            var vc = $"Sqrt[2] * {maxA} * Cos[{frequency} t - 4 Pi / 3]".ToExpr();

            specs.DefineSignal(va, vb, vc);

            return specs;
        }

        public static SymbolicFaultSpecs CreateAc2GFault(string frequency, string maxA, string maxB, string phaseAngleJump)
        {
            var specs = new SymbolicFaultSpecs(
                "Phase A,C to ground fault",
                frequency,
                maxA,
                maxB,
                phaseAngleJump
            );

            var va = $"Sqrt[2] * {maxA} * {maxB} * Cos[{frequency} t - {phaseAngleJump}]".ToExpr();
            var vb = $"Sqrt[2] * {maxA} * Cos[{frequency} t - 2 Pi / 3]".ToExpr();
            var vc = $"Sqrt[2] * {maxA} * {maxB} * Cos[{frequency} t - 4 Pi / 3 - {phaseAngleJump}]".ToExpr();

            specs.DefineSignal(va, vb, vc);

            return specs;
        }

        public static SymbolicFaultSpecs CreateBc2GFault(string frequency, string maxA, string maxB, string phaseAngleJump)
        {
            var specs = new SymbolicFaultSpecs(
                "Phase B,C to ground fault",
                frequency,
                maxA,
                maxB,
                phaseAngleJump
            );

            var va = $"Sqrt[2] * {maxA} * Cos[{frequency} t]".ToExpr();
            var vb = $"Sqrt[2] * {maxA} * {maxB} * Cos[{frequency} t - 2 Pi / 3 - {phaseAngleJump}]".ToExpr();
            var vc = $"Sqrt[2] * {maxA} * {maxB} * Cos[{frequency} t - 4 Pi / 3 - {phaseAngleJump}]".ToExpr();

            specs.DefineSignal(va, vb, vc);

            return specs;
        }

        public static SymbolicFaultSpecs CreateAbc2GFault(string frequency, string maxA, string maxB, string phaseAngleJump)
        {
            var specs = new SymbolicFaultSpecs(
                "Phase A,B,C to ground fault",
                frequency,
                maxA,
                maxB,
                phaseAngleJump
            );

            var va = $"Sqrt[2] * {maxA} * {maxB} * Cos[{frequency} t - {phaseAngleJump}]".ToExpr();
            var vb = $"Sqrt[2] * {maxA} * {maxB} * Cos[{frequency} t - 2 Pi / 3 - {phaseAngleJump}]".ToExpr();
            var vc = $"Sqrt[2] * {maxA} * {maxB} * Cos[{frequency} t - 4 Pi / 3 - {phaseAngleJump}]".ToExpr();

            specs.DefineSignal(va, vb, vc);

            return specs;
        }


        public string FaultDescription { get; }

        public Expr Frequency { get; }

        public Expr MaxA { get; }

        public Expr MaxB { get; }

        public Expr PhaseAngleJump { get; }

        public Expr NormalizationFactor { get; }

        public Expr AssumeExpr { get; }
            = @"And[And[Element[{{A, B, t, \[Omega], \[Phi]}}, Reals], A > 0, B > 0, t >= 0, \[Omega] > 0]".ToExpr();

        public RGaVector<Expr>? Signal { get; private set; }

        public RGaVector<Expr>? SignalDerivative { get; private set; }

        public RGaVector<Expr>? Normal { get; private set; }


        private SymbolicFaultSpecs(string faultDescription, string frequency, string maxA, string maxB, string phaseAngleJump)
        {
            FaultDescription = faultDescription;
            Frequency = frequency.ToExpr();
            MaxA = maxA.ToExpr();
            MaxB = maxB.ToExpr();
            PhaseAngleJump = phaseAngleJump.ToExpr();
            NormalizationFactor = $"3 * {maxA}^2 * {frequency}".ToExpr();
        }


        private void DefineSignal(Expr va, Expr vb, Expr vc)
        {
            Signal =
                GeometricProcessor
                    .Vector(va, vb, vc)
                    .SimplifyScalars(AssumeExpr);

            SignalDerivative =
                Signal
                    .DifferentiateScalars("t")
                    .SimplifyScalars(AssumeExpr);

            var e1 =
                GeometricProcessor.Vector(1);

            var e2 =
                GeometricProcessor.Vector(0, 1);

            var e3 =
                GeometricProcessor.Vector(0, 0, 1);

            var i3 = e1.Op(e2).Op(e3);

            Normal = Signal.Op(SignalDerivative).EDual(i3).GetVectorPart().Divide(NormalizationFactor).FullSimplifyScalars(AssumeExpr);
        }

    }
}
