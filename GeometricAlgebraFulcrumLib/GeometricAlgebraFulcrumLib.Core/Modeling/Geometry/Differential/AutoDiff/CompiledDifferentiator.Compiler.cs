using TapeElement = GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.AutoDiff.Compiled.TapeElement;
using InputEdge = GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.AutoDiff.Compiled.InputEdge;
using InputEdges = GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.AutoDiff.Compiled.InputEdges;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.AutoDiff;

internal partial class CompiledDifferentiator
{
    private class Compiler : ITermVisitor<TapeElement> 
    {
        private readonly List<TapeElement> _tape;
        private readonly List<InputEdge> _edges;
        private readonly Dictionary<Term, TapeElement> _tapeElementOf;

        public Compiler(IEnumerable<Variable> variables, List<TapeElement> tape, List<InputEdge> edges)
        {
            _tape = tape;
            _edges = edges;
            _tapeElementOf = new Dictionary<Term, TapeElement>();
            foreach (var variable in variables)
            {
                var tapeVariable = new Compiled.Variable();
                tape.Add(tapeVariable);
                _tapeElementOf[variable] = tapeVariable;
            }
        }

        public void Compile(Term term)
        {
            term.Accept(this);
        }

        public TapeElement Visit(Constant constant)
        {
            return Compile(constant, () => new Compiled.Constant(constant.Value) { Inputs = new InputEdges(0,0) });
        }

        public TapeElement Visit(Zero zero)
        {
            return Compile(zero, () => new Compiled.Constant(0) { Inputs = new InputEdges(0,0) });
        }

        public TapeElement Visit(ConstPower intPower)
        {
            return Compile(intPower, () =>
            {
                var baseElement = intPower.Base.Accept(this);
                var element = new Compiled.ConstPower
                {
                    Exponent = intPower.Exponent,
                    Inputs = MakeInputEdges(() =>  
                    {
                        _edges.Add(new InputEdge { Element = baseElement });
                    }),
                };

                return element;
            });
        }

        public TapeElement  Visit(TermPower power)
        {
            return Compile(power, () =>
            {
                var baseElement = power.Base.Accept(this);
                var expElement = power.Exponent.Accept(this);
                var element = new Compiled.TermPower
                {
                    Inputs = MakeInputEdges(() => 
                    {
                        _edges.Add(new InputEdge { Element = baseElement });
                        _edges.Add(new InputEdge { Element = expElement });
                    }),
                };

                return element;
            });
        }

        public TapeElement Visit(Product product)
        {
            return Compile(product, () =>
            {
                var leftElement = product.Left.Accept(this);
                var rightElement = product.Right.Accept(this);
                var element = new Compiled.Product
                {
                    Inputs = MakeInputEdges(() => 
                    {
                        _edges.Add(new InputEdge { Element = leftElement });
                        _edges.Add(new InputEdge { Element = rightElement });
                    })
                };

                return element;
            });
        }

        public TapeElement Visit(Sum sum)
        {
            return Compile(sum, () =>
            {
                var terms = sum.Terms;
                var tapeElements = new TapeElement[terms.Count];
                for(var i = 0; i < terms.Count; ++i)
                    tapeElements[i] = terms[i].Accept(this);
                var element = new Compiled.Sum 
                { 
                    Inputs = MakeInputEdges(() => 
                    {
                        for(var i = 0; i < terms.Count; ++i)
                            _edges.Add(new InputEdge { Element = tapeElements[i], Weight = 1});
                    })
                };

                return element;
            });
        }

        public TapeElement Visit(Variable variable)
        {
            return _tapeElementOf[variable];
        }

        public TapeElement Visit(Log log)
        {
            return Compile(log, () =>
            {
                var argElement = log.Arg.Accept(this);
                var element = new Compiled.Log 
                { 
                    Inputs = MakeInputEdges(() => 
                    {
                        _edges.Add(new InputEdge { Element = argElement });
                    }),
                };

                return element;
            });
        }

        public TapeElement Visit(Exp exp)
        {
            return Compile(exp, () =>
            {
                var argElement = exp.Arg.Accept(this);
                var element = new Compiled.Exp
                {
                    Inputs = MakeInputEdges(() => 
                    {
                        _edges.Add(new InputEdge { Element = argElement });
                    }),
                };

                return element;
            });
        }

        public TapeElement Visit(UnaryFunc func)
        {
            return Compile(func, () =>
            {
                var argElement = func.Argument.Accept(this);
                var element = new Compiled.UnaryFunc(func.Eval, func.Diff)
                {
                    Inputs = MakeInputEdges(() => 
                    {
                        _edges.Add(new InputEdge { Element = argElement });
                    }),
                };

                return element;
            });
        }

        public TapeElement Visit(BinaryFunc func)
        {
            return Compile(func, () =>
            {
                var leftElement = func.Left.Accept(this);
                var rightElement = func.Right.Accept(this);

                var element = new Compiled.BinaryFunc(func.Eval, func.Diff)
                {
                    Inputs = MakeInputEdges(() => 
                    {
                        _edges.Add(new InputEdge { Element = leftElement });
                        _edges.Add(new InputEdge { Element = rightElement });
                    })
                };

                return element;
            });
        }

        public TapeElement Visit(NaryFunc func)
        {
            return Compile(func, () =>
            {
                var terms = func.Terms;
                var indices = new TapeElement[terms.Count];
                for(var i = 0; i < terms.Count; ++i)
                    indices[i] = terms[i].Accept(this);

                var element = new Compiled.NaryFunc(func.Eval, func.Diff)
                {
                    Inputs = MakeInputEdges(() => 
                    {
                        for(var i = 0; i < terms.Count; ++i)
                            _edges.Add(new InputEdge { Element = indices[i] });
                    }),
                };

                return element;
            });
        }


        private TapeElement Compile(Term term, Func<TapeElement> compiler)
        {
            if (_tapeElementOf.TryGetValue(term, out var element)) 
                return element;
                
            _tape.Add(element = compiler());
            _tapeElementOf[term] = element;

            return element;
        }
            
        private InputEdges MakeInputEdges(Action action)
        {
            var offset = _edges.Count;
            action();
            var length = _edges.Count - offset;
            return new InputEdges(offset, length);
        }
    }
}