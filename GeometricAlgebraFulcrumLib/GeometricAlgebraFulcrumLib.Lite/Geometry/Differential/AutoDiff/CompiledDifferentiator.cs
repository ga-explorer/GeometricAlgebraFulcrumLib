﻿using TapeElement = GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.AutoDiff.Compiled.TapeElement;
using InputEdge = GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.AutoDiff.Compiled.InputEdge;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.AutoDiff;

/// <inheritdoc />
/// <summary>
/// Compiles the terms tree to a more efficient form for differentiation.
/// </summary>
internal partial class CompiledDifferentiator : ICompiledTerm
{
    private readonly TapeElement[] _tape;
    private readonly int _dimension;

    /// <summary>
    /// Initializes a new instance of the <see cref="CompiledDifferentiator"/> class.
    /// </summary>
    /// <param name="function">The function.</param>
    /// <param name="variables">The variables.</param>
    public CompiledDifferentiator(Term function, IReadOnlyList<Variable> variables)
    {
        Variables = variables.AsReadOnly();
        _dimension = variables.Count;

        if (function is Variable)
            function = new ConstPower(function, 1);

        var tapeList = new List<TapeElement>();
        var inputList = new List<InputEdge>();
        new Compiler(variables, tapeList, inputList).Compile(function);
        _tape = tapeList.ToArray();
            
        var inputEdges = inputList.ToArray();
        foreach(var te in _tape)
            te.Inputs = te.Inputs.Remap(inputEdges);
    }

    public IReadOnlyList<Variable> Variables { get; }

    public double Evaluate(double[] arg)
    {
        Guard.NotNull(arg, nameof(arg));
        Guard.MustHold(arg.Length == Variables.Count, ErrorMessages.ArgLength);
        EvaluateTape(arg);
        return _tape[_tape.Length - 1].Value;
    }

    public double Differentiate(IReadOnlyList<double> arg, IList<double> grad) 
    {
        Guard.NotNull(arg, nameof(arg));
        Guard.NotNull(grad, nameof(grad));
        Guard.MustHold(arg.Count == Variables.Count, ErrorMessages.ArgLength);
        Guard.MustHold(grad.Count == Variables.Count, ErrorMessages.GradLength);

        ForwardSweep(arg);
        ReverseSweep();

        for (var i = 0; i < _dimension; ++i)
            grad[i] = _tape[i].Adjoint;

        return _tape[_tape.Length - 1].Value;
    }

    private void ReverseSweep()
    {
        // initialize adjoints
        for (var i = 0; i < _tape.Length - 1; ++i)
            _tape[i].Adjoint = 0;
        _tape[_tape.Length - 1].Adjoint = 1;

        // accumulate adjoints
        for (var i = _tape.Length - 1; i >= _dimension; --i)
        {
            var inputs = _tape[i].Inputs;
            var adjoint = _tape[i].Adjoint;
                
            for(var j = 0; j < inputs.Length; ++j)
                inputs.Element(j).Adjoint += adjoint * inputs.Weight(j);
        }
    }

    private void ForwardSweep(IReadOnlyList<double> arg)
    {
        for (var i = 0; i < _dimension; ++i)
            _tape[i].Value = arg[i];

        for (var i = _dimension; i < _tape.Length; ++i)
            _tape[i].Diff();
    }

    private void EvaluateTape(IReadOnlyList<double> arg)
    {
        for(var i = 0; i < _dimension; ++i)
            _tape[i].Value = arg[i];
            
        for (var i = _dimension; i < _tape.Length; ++i )
            _tape[i].Eval();
    }
}