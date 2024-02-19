﻿namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.AutoDiff.Compiled;

internal sealed class Constant : TapeElement
{
    public Constant(double value)
    {
        Value = value;
        Adjoint = 0;
    }

    public override void Eval()
    { }

    public override void Diff()
    { }
}