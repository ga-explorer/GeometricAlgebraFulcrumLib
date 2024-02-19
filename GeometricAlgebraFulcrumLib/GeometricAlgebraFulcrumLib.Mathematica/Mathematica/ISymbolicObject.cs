﻿namespace GeometricAlgebraFulcrumLib.Mathematica.Mathematica;

public interface ISymbolicObject
{
    MathematicaInterface CasInterface { get; }

    MathematicaConnection CasConnection { get; }

    MathematicaEvaluator CasEvaluator { get; }

    MathematicaConstants CasConstants { get; }
}