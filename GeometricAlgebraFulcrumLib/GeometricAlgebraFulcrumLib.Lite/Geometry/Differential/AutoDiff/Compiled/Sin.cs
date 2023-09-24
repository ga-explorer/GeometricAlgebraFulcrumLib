namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.AutoDiff.Compiled
{
    internal sealed class Sin : TapeElement
    {
        private const int ArgIdx = 0;
        private TapeElement Arg => Inputs.Element(ArgIdx);

        public override void Eval()
        {
            Value = Math.Sin(Arg.Value);
        }

        public override void Diff()
        {
            Value = Math.Cos(Arg.Value);
            Inputs.SetWeight(ArgIdx, Value);
        }
    }
}