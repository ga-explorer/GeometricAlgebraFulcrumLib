namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.AutoDiff.Compiled
{
    internal sealed class UnaryFunc : TapeElement
    {
        private const int ArgIdx = 0;
        private TapeElement Arg => Inputs.Element(ArgIdx);

        private readonly Func<double, double> _eval;
        private readonly Func<double, double> _diff;

        public UnaryFunc(Func<double, double> eval, Func<double, double> diff)
        {
            _eval = eval;
            _diff = diff;
        }

        public override void Eval()
        {
            Value = _eval(Arg.Value);
        }

        public override void Diff()
        {
            var arg = Arg.Value;
            Value = _eval(arg);
            Inputs.SetWeight(ArgIdx, _diff(arg));
        }
    }
}