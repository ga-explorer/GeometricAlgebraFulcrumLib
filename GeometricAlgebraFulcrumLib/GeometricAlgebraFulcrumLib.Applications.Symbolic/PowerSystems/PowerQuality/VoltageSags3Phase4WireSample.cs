using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.PowerSystems.PowerQuality
{
    public static class VoltageSags3Phase4WireSample
    {
        // This is a pre-defined scalar processor for symbolic
        // Wolfram Mathematica scalars using Expr objects
        public static ScalarProcessorOfWolframExpr ScalarProcessor { get; }
            = ScalarProcessorOfWolframExpr.Instance;

        // Create a 6-dimensional Euclidean geometric algebra processor based on the
        // selected scalar processor
        public static XGaProcessor<Expr> GeometricProcessor { get; }
            = ScalarProcessor.CreateEuclideanXGaProcessor();

        public static int VSpaceDimensions
            => 3;

        // This is a pre-defined text generator for displaying multivectors
        // with symbolic Wolfram Mathematica scalars using Expr objects
        public static TextComposerExpr TextComposer { get; }
            = TextComposerExpr.DefaultComposer;

        // This is a pre-defined LaTeX generator for displaying multivectors
        // with symbolic Wolfram Mathematica scalars using Expr objects
        public static LaTeXComposerOfWolframExpr LaTeXComposer { get; }
            = LaTeXComposerOfWolframExpr.DefaultComposer;


        public sealed class FaultSpecs
        {
            public static FaultSpecs CreateNoFault(string frequency, string maxA)
            {
                var specs = new FaultSpecs(
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

            public static FaultSpecs CreateA2GFault(string frequency, string maxA, string maxB, string phaseAngleJump)
            {
                var specs = new FaultSpecs(
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

            public static FaultSpecs CreateB2GFault(string frequency, string maxA, string maxB, string phaseAngleJump)
            {
                var specs = new FaultSpecs(
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

            public static FaultSpecs CreateC2GFault(string frequency, string maxA, string maxB, string phaseAngleJump)
            {
                var specs = new FaultSpecs(
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

            public static FaultSpecs CreateAB2GFault(string frequency, string maxA, string maxB, string phaseAngleJump)
            {
                var specs = new FaultSpecs(
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

            public static FaultSpecs CreateAC2GFault(string frequency, string maxA, string maxB, string phaseAngleJump)
            {
                var specs = new FaultSpecs(
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

            public static FaultSpecs CreateBC2GFault(string frequency, string maxA, string maxB, string phaseAngleJump)
            {
                var specs = new FaultSpecs(
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

            public static FaultSpecs CreateABC2GFault(string frequency, string maxA, string maxB, string phaseAngleJump)
            {
                var specs = new FaultSpecs(
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

            public XGaVector<Expr> Signal { get; private set; }

            public XGaVector<Expr> SignalDerivative { get; private set; }

            public XGaVector<Expr> Normal { get; private set; }


            private FaultSpecs(string faultDescription, string frequency, string maxA, string maxB, string phaseAngleJump)
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

        public static void DisplayFault(FaultSpecs faultSpecs)
        {
            LaTeXComposer.BasisName = @"\boldsymbol{\mu}";

            Console.WriteLine(faultSpecs.FaultDescription);
            Console.WriteLine();

            Console.WriteLine($@"$\boldsymbol{{v}} = {LaTeXComposer.GetMultivectorText(faultSpecs.Signal)}$");
            Console.WriteLine();

            Console.WriteLine($@"$\frac{{d}}{{dt}}\left(\boldsymbol{{v}}\right) = {LaTeXComposer.GetMultivectorText(faultSpecs.SignalDerivative)}$");
            Console.WriteLine();

            Console.WriteLine($@"$n = {LaTeXComposer.GetMultivectorText(faultSpecs.Normal)}$");
            Console.WriteLine();
        }


        public static void Execute()
        {
            LaTeXComposer.BasisName = @"\boldsymbol{\mu}";

            DisplayFault(
                FaultSpecs.CreateNoFault(@"\[Omega]", "A")
            );

            DisplayFault(
                FaultSpecs.CreateA2GFault(
                    @"\[Omega]",
                    "A",
                    "B",
                    @"\[Phi]"
                )
            );

            DisplayFault(
                FaultSpecs.CreateB2GFault(
                    @"\[Omega]",
                    "A",
                    "B",
                    @"\[Phi]"
                )
            );

            DisplayFault(
                FaultSpecs.CreateC2GFault(
                    @"\[Omega]",
                    "A",
                    "B",
                    @"\[Phi]"
                )
            );

            DisplayFault(
                FaultSpecs.CreateAB2GFault(
                    @"\[Omega]",
                    "A",
                    "B",
                    @"\[Phi]"
                )
            );

            DisplayFault(
                FaultSpecs.CreateAC2GFault(
                    @"\[Omega]",
                    "A",
                    "B",
                    @"\[Phi]"
                )
            );

            DisplayFault(
                FaultSpecs.CreateBC2GFault(
                    @"\[Omega]",
                    "A",
                    "B",
                    @"\[Phi]"
                )
            );

            DisplayFault(
                FaultSpecs.CreateABC2GFault(
                    @"\[Omega]",
                    "A",
                    "B",
                    @"\[Phi]"
                )
            );

        }
    }
}
