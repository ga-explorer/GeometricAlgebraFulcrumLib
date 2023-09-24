namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.AutoDiff.Compiled
{
    internal sealed class NaryFunc : TapeElement
    {
        private readonly Func<double[], double> _eval;
        private readonly Func<double[], double[]> _diff;

        public NaryFunc(Func<double[], double> eval, Func<double[], double[]> diff)
        {
            _eval = eval;
            _diff = diff;
        }

        public override void Eval()
        {
            Value = _eval(GetArg());
        }

        public override void Diff()
        {
            var arg = GetArg();
            Value = _eval(arg);
            var grad = _diff(arg);
            for (var i = 0; i < grad.Length; ++i)
                Inputs.SetWeight(i, grad[i]);
        }

        private double[] GetArg()
        {
            var arg = new double[Inputs.Length];
            for (var i = 0; i < arg.Length; ++i)
                arg[i] = Inputs.Element(i).Value;
            return arg;
        }
    }
}
