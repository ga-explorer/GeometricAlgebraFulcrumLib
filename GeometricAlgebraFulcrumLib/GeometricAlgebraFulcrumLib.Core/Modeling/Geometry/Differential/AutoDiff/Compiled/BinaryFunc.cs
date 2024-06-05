namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.AutoDiff.Compiled;

internal sealed class BinaryFunc : TapeElement
{
    private const int LeftIdx = 0;
    private const int RightIdx = 1;

    private TapeElement Left => Inputs.Element(LeftIdx);

    private TapeElement Right => Inputs.Element(RightIdx);

    private readonly Func<double, double, double> _eval;
    private readonly Func<double, double, Tuple<double, double>> _diff;

    public BinaryFunc(Func<double, double, double> eval, Func<double, double, Tuple<double, double>> diff)
    {
        _eval = eval;
        _diff = diff;
    }

    public override void Eval()
    {
        Value = _eval(Left.Value, Right.Value);
    }

    public override void Diff()
    {
        var left = Left.Value;
        var right = Right.Value;

        Value = _eval(left, right);
        var grad = _diff(left, right);
        Inputs.SetWeight(LeftIdx, grad.Item1);
        Inputs.SetWeight(RightIdx, grad.Item2);
    }
}