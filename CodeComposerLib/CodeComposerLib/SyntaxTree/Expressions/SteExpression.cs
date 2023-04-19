using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace CodeComposerLib.SyntaxTree.Expressions
{
    /// <summary>
    /// This class represents a universal text-based symbolic expression tree
    /// </summary>
    public sealed class SteExpression : 
        ISyntaxTreeElement
    {
        /// <summary>
        /// Create an expression from a variable name
        /// </summary>
        /// <param name="varName"></param>
        /// <returns></returns>
        public static SteExpression CreateVariable(string varName)
        {
            if (string.IsNullOrEmpty(varName))
                throw new ArgumentNullException(nameof(varName), @"Variable name not initialized");

            return new SteExpression(new SteVariableHeadSpecs(varName));
        }

        /// <summary>
        /// Create a symbolic number like "Pi" and "e".
        /// </summary>
        /// <param name="numberText"></param>
        /// <returns></returns>
        public static SteExpression CreateSymbolicNumber(string numberText)
        {
            if (string.IsNullOrEmpty(numberText))
                throw new ArgumentNullException(nameof(numberText), @"Number not initialized");

            return new SteExpression(new SteNumberHeadSpecs(numberText, true));
        }

        /// <summary>
        /// Create a literal number expression like "2", "-1.7" and "3.56e-4"
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static SteExpression CreateLiteralNumber(int number)
        {
            return new SteExpression(new SteNumberHeadSpecs(number));
        }
        
        /// <summary>
        /// Create a literal number expression like "2", "-1.7" and "3.56e-4"
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static SteExpression CreateLiteralNumber(uint number)
        {
            return new SteExpression(new SteNumberHeadSpecs(number));
        }
        
        /// <summary>
        /// Create a literal number expression like "2", "-1.7" and "3.56e-4"
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static SteExpression CreateLiteralNumber(long number)
        {
            return new SteExpression(new SteNumberHeadSpecs(number));
        }
        
        /// <summary>
        /// Create a literal number expression like "2", "-1.7" and "3.56e-4"
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static SteExpression CreateLiteralNumber(ulong number)
        {
            return new SteExpression(new SteNumberHeadSpecs(number));
        }
        
        /// <summary>
        /// Create a literal number expression like "2", "-1.7" and "3.56e-4"
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static SteExpression CreateLiteralNumber(float number)
        {
            return new SteExpression(new SteNumberHeadSpecs(number));
        }

        /// <summary>
        /// Create a literal number expression like "2", "-1.7" and "3.56e-4"
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static SteExpression CreateLiteralNumber(double number)
        {
            return new SteExpression(new SteNumberHeadSpecs(number));
        }

        /// <summary>
        /// Create a literal number expression like "2", "-1.7" and "3.56e-4"
        /// </summary>
        /// <param name="numberText"></param>
        /// <returns></returns>
        public static SteExpression CreateLiteralNumber(string numberText)
        {
            if (string.IsNullOrEmpty(numberText))
                throw new ArgumentNullException(nameof(numberText), @"Number not initialized");

            return new SteExpression(new SteNumberHeadSpecs(numberText, false));
        }

        /// <summary>
        /// Create a function expression with no arguments
        /// </summary>
        /// <param name="funcName"></param>
        /// <returns></returns>
        public static SteExpression CreateFunction(string funcName)
        {
            if (string.IsNullOrEmpty(funcName))
                throw new ArgumentNullException(nameof(funcName), @"Function name not initialized");

            return new SteExpression(new SteFunctionHeadSpecs(funcName), Enumerable.Empty<SteExpression>());
        }

        public static SteExpression CreateFunction(string funcName, int arg1)
        {
            return CreateFunction(
                funcName,
                CreateLiteralNumber(arg1)
            );
        }

        public static SteExpression CreateFunction(string funcName, float arg1)
        {
            return CreateFunction(
                funcName,
                CreateLiteralNumber(arg1)
            );
        }
        
        public static SteExpression CreateFunction(string funcName, double arg1)
        {
            return CreateFunction(
                funcName,
                CreateLiteralNumber(arg1)
            );
        }

        /// <summary>
        /// Create a function expression with one argument
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="arg1"></param>
        /// <returns></returns>
        public static SteExpression CreateFunction(string funcName, [NotNull] SteExpression arg1)
        {
            if (string.IsNullOrEmpty(funcName))
                throw new ArgumentNullException(nameof(funcName), @"Function name not initialized");

            var funcHeadSpecs = new SteFunctionHeadSpecs(funcName);

            return new SteExpression(funcHeadSpecs, new []{ arg1 });
        }
        
        /// <summary>
        /// Create a function expression with two arguments
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public static SteExpression CreateFunction(string funcName, [NotNull] SteExpression arg1, [NotNull] SteExpression arg2)
        {
            if (string.IsNullOrEmpty(funcName))
                throw new ArgumentNullException(nameof(funcName), @"Function name not initialized");

            var funcHeadSpecs = new SteFunctionHeadSpecs(funcName);

            return new SteExpression(funcHeadSpecs, new []{ arg1, arg2 });
        }
        
        /// <summary>
        /// Create a function expression with 3 arguments
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <returns></returns>
        public static SteExpression CreateFunction(string funcName, [NotNull] SteExpression arg1, [NotNull] SteExpression arg2, [NotNull] SteExpression arg3)
        {
            if (string.IsNullOrEmpty(funcName))
                throw new ArgumentNullException(nameof(funcName), @"Function name not initialized");

            var funcHeadSpecs = new SteFunctionHeadSpecs(funcName);

            return new SteExpression(funcHeadSpecs, new []{ arg1, arg2, arg3 });
        }

        /// <summary>
        /// Create a function expression with some arguments
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static SteExpression CreateFunction(string funcName, params SteExpression[] args)
        {
            if (string.IsNullOrEmpty(funcName))
                throw new ArgumentNullException(nameof(funcName), @"Function name not initialized");

            var funcHeadSpecs = new SteFunctionHeadSpecs(funcName);

            return args == null
                ? new SteExpression(funcHeadSpecs, Enumerable.Empty<SteExpression>())
                : new SteExpression(funcHeadSpecs, args);
        }

        /// <summary>
        /// Create a function expression with some arguments
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static SteExpression CreateFunction(string funcName, IEnumerable<SteExpression> args)
        {
            if (string.IsNullOrEmpty(funcName))
                throw new ArgumentNullException(nameof(funcName), @"Function name not initialized");

            var funcHeadSpecs = new SteFunctionHeadSpecs(funcName);

            return args == null
                ? new SteExpression(funcHeadSpecs, Enumerable.Empty<SteExpression>())
                : new SteExpression(funcHeadSpecs, args);
        }

        /// <summary>
        /// Create a function expression with no arguments
        /// </summary>
        /// <param name="arrayName"></param>
        /// <returns></returns>
        public static SteExpression CreateArrayAccess(string arrayName)
        {
            if (string.IsNullOrEmpty(arrayName))
                throw new ArgumentNullException(nameof(arrayName), @"Array name not initialized");

            return new SteExpression(new SteArrayAccessHeadSpecs(arrayName), Enumerable.Empty<SteExpression>());
        }

        /// <summary>
        /// Create a function expression with some arguments
        /// </summary>
        /// <param name="arrayName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static SteExpression CreateArrayAccess(string arrayName, params SteExpression[] args)
        {
            if (string.IsNullOrEmpty(arrayName))
                throw new ArgumentNullException(nameof(arrayName), @"Array name not initialized");

            var funcHeadSpecs = new SteArrayAccessHeadSpecs(arrayName);

            return args == null
                ? new SteExpression(funcHeadSpecs, Enumerable.Empty<SteExpression>())
                : new SteExpression(funcHeadSpecs, args);
        }

        /// <summary>
        /// Create a function expression with some arguments
        /// </summary>
        /// <param name="arrayName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static SteExpression CreateArrayAccess(string arrayName, IEnumerable<SteExpression> args)
        {
            if (string.IsNullOrEmpty(arrayName))
                throw new ArgumentNullException(nameof(arrayName), @"Array name not initialized");

            var funcHeadSpecs = new SteArrayAccessHeadSpecs(arrayName);

            return args == null
                ? new SteExpression(funcHeadSpecs, Enumerable.Empty<SteExpression>())
                : new SteExpression(funcHeadSpecs, args);
        }

        /// <summary>
        /// Create an operator expression without any arguments
        /// </summary>
        /// <param name="opHeadSpecs"></param>
        /// <returns></returns>
        public static SteExpression CreateOperator(SteOperatorSpecs opHeadSpecs)
        {
            if (ReferenceEquals(opHeadSpecs, null))
                throw new ArgumentNullException(nameof(opHeadSpecs), @"Expression head not initialized");

            return new SteExpression(opHeadSpecs, Enumerable.Empty<SteExpression>());
        }

        /// <summary>
        /// Create an operator expression from a head and a set of expressions as its arguments
        /// </summary>
        /// <param name="opHeadSpecs"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static SteExpression CreateOperator(SteOperatorSpecs opHeadSpecs, params SteExpression[] args)
        {
            if (ReferenceEquals(opHeadSpecs, null))
                throw new ArgumentNullException(nameof(opHeadSpecs), @"Expression head not initialized");

            return args == null
                ? new SteExpression(opHeadSpecs, Enumerable.Empty<SteExpression>())
                : new SteExpression(opHeadSpecs, args);
        }

        /// <summary>
        /// Create an operator expression from a head and a set of expressions as its arguments
        /// </summary>
        /// <param name="opHeadSpecs"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static SteExpression CreateOperator(SteOperatorSpecs opHeadSpecs, IEnumerable<SteExpression> args)
        {
            if (ReferenceEquals(opHeadSpecs, null))
                throw new ArgumentNullException(nameof(opHeadSpecs), @"Expression head not initialized");

            return args == null
                ? new SteExpression(opHeadSpecs, Enumerable.Empty<SteExpression>())
                : new SteExpression(opHeadSpecs, args);
        }


        /// <summary>
        /// The arguments of the symbolic expression (can be null).
        /// </summary>
        private List<SteExpression> _argsList;

        /// <summary>
        /// The specifications of this expression
        /// </summary>
        public ISteExpressionHeadSpecs HeadSpecs { get; private set; }

        /// <summary>
        /// The head text of this symbolic expression
        /// </summary>
        public string HeadText => HeadSpecs.HeadText;

        /// <summary>
        /// True if this is an atomic symbol expression (a variable or a symbolic number)
        /// </summary>
        public bool IsSymbolic 
        {
            get
            {
                var varHead = HeadSpecs as SteVariableHeadSpecs;

                if (ReferenceEquals(varHead, null) == false) return true;

                var numHead = HeadSpecs as SteNumberHeadSpecs;

                return ReferenceEquals(numHead, null) == false && numHead.IsSymbolic;
            }
        }

        /// <summary>
        /// True if this is an atomic number expression
        /// </summary>
        public bool IsNumber => HeadSpecs is SteNumberHeadSpecs;

        /// <summary>
        /// True if this is an atomic variable expression
        /// </summary>
        public bool IsVariable => HeadSpecs is SteVariableHeadSpecs;

        /// <summary>
        /// True for atomic number literals (i.e. non-symbolic number values like 0, 2.1, -3.2e-5, etc.)
        /// </summary>
        public bool IsNumberLiteral
        {
            get
            {
                var numHead = HeadSpecs as SteNumberHeadSpecs;

                return ReferenceEquals(numHead, null) == false && numHead.IsLiteral;
            }
        }

        /// <summary>
        /// True for atomic number symbols like Pi and e.
        /// </summary>
        public bool IsNumberSymbol
        {
            get
            {
                var numHead = HeadSpecs as SteNumberHeadSpecs;

                return ReferenceEquals(numHead, null) == false && numHead.IsSymbolic;
            }
        }

        /// <summary>
        /// True for function expressions
        /// </summary>
        public bool IsFunction => HeadSpecs is SteFunctionHeadSpecs;

        /// <summary>
        /// True for array access expressions
        /// </summary>
        public bool IsArrayAccess => HeadSpecs is SteArrayAccessHeadSpecs;

        /// <summary>
        /// True for operator expressions
        /// </summary>
        public bool IsOperator => HeadSpecs is SteOperatorSpecs;

        /// <summary>
        /// True if this is a leaf expression (it's an atomic expression with null list of arguments)
        /// </summary>
        public bool IsAtomic => _argsList == null;

        /// <summary>
        /// True if this is a non-leaf expression (it has a non-null list of arguments even if it's empty)
        /// </summary>
        public bool IsComposite => _argsList != null;

        /// <summary>
        /// True if this expression is aa operator without any arguments
        /// </summary>
        public bool IsEmptyComposite => ReferenceEquals(_argsList, null) == false
                                        && _argsList.Count == 0;

        /// <summary>
        /// True if this expression has a single argument
        /// </summary>
        public bool IsUnaryComposite => ReferenceEquals(_argsList, null) == false
                                        && _argsList.Count == 1;

        /// <summary>
        /// True if this expression has two arguments
        /// </summary>
        public bool IsBinaryComposite => ReferenceEquals(_argsList, null) == false
                                         && _argsList.Count == 2;

        /// <summary>
        /// True if this expression has 3 arguments
        /// </summary>
        public bool IsTernaryComposite => ReferenceEquals(_argsList, null) == false
                                          && _argsList.Count == 3;

        /// <summary>
        /// True for atomic expressions and empty composite expressions
        /// </summary>
        public bool HasNoArguments => ReferenceEquals(_argsList, null) || _argsList.Count == 0;

        /// <summary>
        /// True for non-empty composite expressions
        /// </summary>
        public bool HasArguments => ReferenceEquals(_argsList, null) == false && _argsList.Count > 0;

        /// <summary>
        /// The precedence of this expression. For operator expressions this returns the operator
        /// precedence, and returns 0 for all other expressions
        /// </summary>
        public int Precedence
        {
            get
            {
                var opHeadSpecs = HeadSpecs as SteOperatorSpecs;

                return opHeadSpecs?.Precedence ?? 0;
            }
        }

        /// <summary>
        /// True if the head text is identical to "0" with no arguments
        /// </summary>
        public bool IsZero => ArgumentsCount == 0 && HeadText == "0";

        /// <summary>
        /// The arguments of this expression, if any
        /// </summary>
        public IEnumerable<SteExpression> Arguments => _argsList ?? Enumerable.Empty<SteExpression>();

        /// <summary>
        /// The number of arguments of this expression, returns zero for atomic expressions
        /// </summary>
        public int ArgumentsCount => _argsList?.Count ?? 0;

        /// <summary>
        /// The first argument of this expression
        /// </summary>
        public SteExpression FirstArgument => ArgumentsCount > 0 ? _argsList[0] : null;

        /// <summary>
        /// The second argument of this expression
        /// </summary>
        public SteExpression SecondArgument => ArgumentsCount > 1 ? _argsList[1] : null;

        /// <summary>
        /// The third argument of this expression
        /// </summary>
        public SteExpression ThirdArgument => ArgumentsCount > 2 ? _argsList[2] : null;

        /// <summary>
        /// The last argument of this expression
        /// </summary>
        public SteExpression LastArgument => ArgumentsCount > 0 ? _argsList[_argsList.Count - 1] : null;

        /// <summary>
        /// Used to access the arguments of this expression, if any
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public SteExpression this[int i]
        {
            get { return _argsList[i]; }
            set { _argsList[i] = value; }
        }

        /// <summary>
        /// A rough estimate of the number of computations in this text expression tree computed by summing
        /// each tree node's number of direct child nodes
        /// </summary>
        /// <returns></returns>
        public int ComputationsCount
        {
            get
            {
                if (_argsList == null)
                    return 0;

                var count = _argsList.Count - 1;

                var stack = new Stack<SteExpression>(_argsList);

                while (stack.Count > 0)
                {
                    var expr = stack.Pop();

                    if (expr._argsList == null)
                        continue;

                    count += expr._argsList.Count - 1;

                    foreach (var subExpr in expr._argsList)
                        stack.Push(subExpr);
                }

                return count;
            }
        }

        /// <summary>
        /// Returns the names of variables used in all levels of this expression (may contain repetitions)
        /// </summary>
        public IEnumerable<string> Variables
        {
            get
            {
                if (_argsList == null)
                {
                    if (IsVariable)
                        yield return HeadText;

                    yield break;
                }

                var stack = new Stack<SteExpression>();

                stack.Push(this);

                while (stack.Count > 0)
                {
                    var expr = stack.Pop();

                    foreach (var subExpr in expr._argsList)
                        if (subExpr.IsVariable)
                            yield return subExpr.HeadText;

                        else if (subExpr._argsList != null)
                            stack.Push(subExpr);
                }
            }
        }

        /// <summary>
        /// Returns a list of all sub-expressions (sub-trees) in this expression including the main expression
        /// </summary>
        public IEnumerable<SteExpression> SubExpressions
        {
            get
            {
                yield return this;

                if (_argsList == null)
                    yield break;

                var stack = new Stack<SteExpression>(_argsList);

                while (stack.Count > 0)
                {
                    var expr = stack.Pop();

                    yield return expr;

                    if (expr._argsList == null)
                        continue;

                    foreach (var subExpr in expr._argsList)
                        stack.Push(subExpr);
                }
            }
        }

        /// <summary>
        /// Returns a list of all sub-expressions (sub-trees) in this expression 
        /// excluding the main expression tree
        /// </summary>
        public IEnumerable<SteExpression> ProperSubExpressions
        {
            get
            {
                if (_argsList == null)
                    yield break;

                var stack = new Stack<SteExpression>(_argsList);

                while (stack.Count > 0)
                {
                    var expr = stack.Pop();

                    yield return expr;

                    if (expr._argsList == null)
                        continue;

                    foreach (var subExpr in expr._argsList)
                        stack.Push(subExpr);
                }
            }
        }

        /// <summary>
        /// Returns a list of all leaf expressions in this expression
        /// </summary>
        public IEnumerable<SteExpression> AtomicSubExpressios
        {
            get
            {
                if (IsAtomic)
                {
                    yield return this;

                    yield break;
                }

                var stack = new Stack<SteExpression>();

                stack.Push(this);

                while (stack.Count > 0)
                {
                    var expr = stack.Pop();

                    foreach (var subExpr in expr._argsList)
                        if (subExpr.IsAtomic)
                            yield return subExpr;
                        else
                            stack.Push(subExpr);
                }
            }
        }

        /// <summary>
        /// Returns a list of all sub-expressions that are not leafs
        /// </summary>
        public IEnumerable<SteExpression> CompositeSubExpressions
        {
            get
            {
                if (IsAtomic)
                    yield break;

                var stack = new Stack<SteExpression>();

                stack.Push(this);

                while (stack.Count > 0)
                {
                    var expr = stack.Pop();

                    yield return expr;

                    foreach (var subExpr in expr._argsList.Where(subExpr => subExpr.IsAtomic == false))
                        stack.Push(subExpr);
                }
            }
        }

        /// <summary>
        /// Returns the number of all sub-expressions that are not leafs
        /// </summary>
        public int CompositeSubExpressionsCount => CompositeSubExpressions.Count();


        /// <summary>
        /// Constructor for atomic expressions
        /// </summary>
        /// <param name="headSpecs"></param>
        private SteExpression(ISteAtomicHeadSpecs headSpecs)
        {
            HeadSpecs = headSpecs;
            _argsList = null;
        }

        /// <summary>
        /// Constructor for composite expressions
        /// </summary>
        /// <param name="headSpecs"></param>
        private SteExpression(ISteCompositeHeadSpecs headSpecs)
        {
            HeadSpecs = headSpecs;
            _argsList = new List<SteExpression>();
        }

        /// <summary>
        /// Constructor for composite expressions
        /// </summary>
        /// <param name="headSpecs"></param>
        /// <param name="args"></param>
        private SteExpression(ISteCompositeHeadSpecs headSpecs, IEnumerable<SteExpression> args)
        {
            HeadSpecs = headSpecs;
            _argsList = new List<SteExpression>(args);
        }


        /// <summary>
        /// Create a copy of this text expression tree
        /// </summary>
        /// <returns></returns>
        public SteExpression CreateCopy()
        {
            return
                IsAtomic
                    ? new SteExpression((ISteAtomicHeadSpecs)HeadSpecs)
                    : new SteExpression((ISteCompositeHeadSpecs)HeadSpecs, _argsList.CreateCopy());
        }

        /// <summary>
        /// Clear all arguments of this expressions
        /// </summary>
        /// <returns></returns>
        public SteExpression ClearArguments()
        {
            if (ReferenceEquals(_argsList, null) == false)
                _argsList.Clear();

            return this;
        }

        /// <summary>
        /// Remove an argument in this expression
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public SteExpression RemoveArgumentAt(int index)
        {
            if (ReferenceEquals(_argsList, null) == false)
                _argsList.RemoveAt(index);

            return this;
        }

        /// <summary>
        /// Reset the head and arguments of this expression
        /// </summary>
        /// <param name="headSpecs"></param>
        /// <returns></returns>
        public SteExpression Reset(ISteAtomicHeadSpecs headSpecs)
        {
            HeadSpecs = headSpecs;
            _argsList = null;

            return this;
        }

        /// <summary>
        /// Reset the head and arguments of this expression
        /// </summary>
        /// <param name="headSpecs"></param>
        /// <returns></returns>
        public SteExpression Reset(ISteCompositeHeadSpecs headSpecs)
        {
            HeadSpecs = headSpecs;
            _argsList = new List<SteExpression>();

            return this;
        }

        /// <summary>
        /// Reset the head and arguments of this expression
        /// </summary>
        /// <param name="headSpecs"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public SteExpression Reset(ISteCompositeHeadSpecs headSpecs, IEnumerable<SteExpression> args)
        {
            HeadSpecs = headSpecs;
            _argsList = new List<SteExpression>(args);

            return this;
        }

        /// <summary>
        /// Reset the head and arguments of this expression
        /// </summary>
        /// <param name="headSpecs"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public SteExpression Reset(ISteCompositeHeadSpecs headSpecs, params SteExpression[] args)
        {
            HeadSpecs = headSpecs;
            _argsList = new List<SteExpression>(args);

            return this;
        }

        /// <summary>
        /// Reset the head of this expression without changing the arguments
        /// </summary>
        /// <param name="headSpecs"></param>
        /// <returns></returns>
        public SteExpression ResetHead(ISteCompositeHeadSpecs headSpecs)
        {
            HeadSpecs = headSpecs;

            if (ReferenceEquals(_argsList, null))
                _argsList = new List<SteExpression>();

            return this;
        }

        /// <summary>
        /// Reset the arguments of this expression without changing the head
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public SteExpression ResetArguments(params SteExpression[] args)
        {
            if (ReferenceEquals(_argsList, null))
                throw new InvalidOperationException();

            _argsList.Clear();
            _argsList.AddRange(args);

            return this;
        }

        /// <summary>
        /// Reset the arguments of this expression without changing the head
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public SteExpression ResetArguments(IEnumerable<SteExpression> args)
        {
            if (ReferenceEquals(_argsList, null))
                throw new InvalidOperationException();

            _argsList.Clear();
            _argsList.AddRange(args);

            return this;
        }

        /// <summary>
        /// Reset the head and arguments of this expression as a copy of the given expression
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public SteExpression ResetAsCopy(SteExpression expr)
        {
            HeadSpecs = expr.HeadSpecs;

            if (expr.IsAtomic)
            {
                _argsList = null;

                return this;
            }

            _argsList = new List<SteExpression>(expr._argsList.Count);

            for (var i = 0; i < expr._argsList.Count; i++)
                _argsList.Add(
                    expr._argsList[i].CreateCopy()
                    );

            return this;
        }

        /// <summary>
        /// Add the given argument to this expression
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>The argument that was added to this expression</returns>
        public SteExpression AddArgument(SteExpression arg)
        {
            if (ReferenceEquals(_argsList, null))
                throw new InvalidOperationException("Cannot add an argument to an atomic expression");

            _argsList.Add(arg);

            return this;
        }

        /// <summary>
        /// Add several arguments to this expression
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public SteExpression AddArguments(params SteExpression[] args)
        {
            if (ReferenceEquals(_argsList, null))
                throw new InvalidOperationException("Cannot add an argument to an atomic expression");

            _argsList.AddRange(args);

            return this;
        }

        /// <summary>
        /// Add several arguments to this expression
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public SteExpression AddArguments(IEnumerable<SteExpression> args)
        {
            if (ReferenceEquals(_argsList, null))
                throw new InvalidOperationException("Cannot add an argument to an atomic expression");

            _argsList.AddRange(args);

            return this;
        }

        /// <summary>
        /// Adds a copy of the input expression as an argument to this expression
        /// </summary>
        /// <param name="expr"></param>
        /// <returns>The argument that was added to this expression</returns>
        public SteExpression AddCopy(SteExpression expr)
        {
            if (ReferenceEquals(_argsList, null))
                throw new InvalidOperationException("Cannot add an argument to an atomic expression");

            var arg = expr.CreateCopy();

            _argsList.Add(arg);

            return arg;
        }

        /// <summary>
        /// Add several arguments copies to this axpression
        /// </summary>
        /// <param name="exprList"></param>
        /// <returns></returns>
        public SteExpression AddCopies(params SteExpression[] exprList)
        {
            if (ReferenceEquals(_argsList, null))
                throw new InvalidOperationException("Cannot add an argument to an atomic expression");

            foreach (var expr in exprList)
                _argsList.Add(
                    expr.CreateCopy()
                    );

            return this;
        }

        /// <summary>
        /// Add several arguments copies to this axpression
        /// </summary>
        /// <param name="exprList"></param>
        /// <returns></returns>
        public SteExpression AddCopies(IEnumerable<SteExpression> exprList)
        {
            if (ReferenceEquals(_argsList, null))
                throw new InvalidOperationException("Cannot add an argument to an atomic expression");

            foreach (var expr in exprList)
                _argsList.Add(
                    expr.CreateCopy()
                    );

            return this;
        }

        /// <summary>
        /// Insert the given argument into this expression
        /// </summary>
        /// <param name="index"></param>
        /// <param name="arg">The argument that was inserted into this expression</param>
        /// <returns></returns>
        public SteExpression InsertArgument(int index, SteExpression arg)
        {
            if (ReferenceEquals(_argsList, null))
                throw new InvalidOperationException("Cannot add an argument to an atomic expression");

            _argsList.Insert(
                index,
                arg
                );

            return arg;
        }

        /// <summary>
        /// Insert several arguments into this expression
        /// </summary>
        /// <param name="index"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public SteExpression InsertArguments(int index, params SteExpression[] args)
        {
            if (ReferenceEquals(_argsList, null))
                throw new InvalidOperationException("Cannot add an argument to an atomic expression");

            _argsList.InsertRange(
                index,
                args
                );

            return this;
        }

        /// <summary>
        /// Insert several arguments into this expression
        /// </summary>
        /// <param name="index"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public SteExpression InsertArguments(int index, IEnumerable<SteExpression> args)
        {
            if (ReferenceEquals(_argsList, null))
                throw new InvalidOperationException("Cannot add an argument to an atomic expression");

            _argsList.InsertRange(
                index,
                args
                );

            return this;
        }

        /// <summary>
        /// Insert a copy of the given argument into this expression
        /// </summary>
        /// <param name="index"></param>
        /// <param name="expr"></param>
        /// <returns>The argument that was added to this expression</returns>
        public SteExpression InsertCopy(int index, SteExpression expr)
        {
            if (ReferenceEquals(_argsList, null))
                throw new InvalidOperationException("Cannot add an argument to an atomic expression");

            var arg = expr.CreateCopy();

            _argsList.Insert(index, arg);

            return arg;
        }

        /// <summary>
        /// Insert several copies of arguments into this expression
        /// </summary>
        /// <param name="index"></param>
        /// <param name="exprList"></param>
        /// <returns></returns>
        public SteExpression InsertCopies(int index, params SteExpression[] exprList)
        {
            if (ReferenceEquals(_argsList, null))
                throw new InvalidOperationException("Cannot add an argument to an atomic expression");

            foreach (var expr in exprList)
                _argsList.Insert(
                    index,
                    expr.CreateCopy()
                    );

            return this;
        }

        /// <summary>
        /// Insert several copies of arguments into this expression
        /// </summary>
        /// <param name="index"></param>
        /// <param name="exprList"></param>
        /// <returns></returns>
        public SteExpression InsertCopies(int index, IEnumerable<SteExpression> exprList)
        {
            if (ReferenceEquals(_argsList, null))
                throw new InvalidOperationException("Cannot add an argument to an atomic expression");

            foreach (var expr in exprList)
                _argsList.Insert(
                    index,
                    expr.CreateCopy()
                    );

            return this;
        }

        /// <summary>
        /// Find all instances of the given oldExpr inside this text expression tree 
        /// and replace them with a copy of newExpr. The search is done once and only instances near the
        /// leaf of the tree are replaced (i.e. one search-replace iteration over the tree)
        /// </summary>
        /// <param name="oldExpr"></param>
        /// <param name="newExpr"></param>
        /// <returns></returns>
        public SteExpression ReplaceAllInPlace(SteExpression oldExpr, SteExpression newExpr)
        {
            if (SteExpressionUtils.Equals(this, oldExpr))
            {
                ResetAsCopy(newExpr);

                return this;
            }

            if (IsAtomic)
                return this;

            foreach (var subExpr in _argsList)
                subExpr.ReplaceAllInPlace(oldExpr, newExpr);

            return this;
        }

        /// <summary>
        /// Find all instances of the given oldExpr inside this text expression tree 
        /// and replace them with a variable symbol expression of newVarName. The search is done once and 
        /// only instances near the leaf of the tree are replaced (i.e. one search-replace iteration over the tree)
        /// </summary>
        /// <param name="oldExpr"></param>
        /// <param name="newVarName"></param>
        public SteExpression ReplaceAllByVariableInPlace(SteExpression oldExpr, string newVarName)
        {
            if (this == oldExpr)
                return this.ResetAsVariable(newVarName);

            if (IsAtomic)
                return this;

            foreach (var subExpr in _argsList)
                subExpr.ReplaceAllByVariableInPlace(oldExpr, newVarName);

            return this;
        }

        /// <summary>
        /// Replace all instances of the variable symbol oldVarName by the variable symbol newVarName
        /// </summary>
        /// <param name="oldVarName"></param>
        /// <param name="newVarName"></param>
        public SteExpression ReplaceAllVariablesInPlace(string oldVarName, string newVarName)
        {
            if (IsVariable && HeadText == oldVarName)
            {
                HeadSpecs = new SteVariableHeadSpecs(newVarName);

                return this;
            }

            if (IsAtomic)
                return this;

            foreach (var subExpr in _argsList)
                subExpr.ReplaceAllVariablesInPlace(oldVarName, newVarName);

            return this;
        }


        public override int GetHashCode()
        {
            return HeadText.GetHashCode() ^ ArgumentsCount.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(obj, null) && SteExpressionUtils.Equals(this, obj as SteExpression);
        }

        public override string ToString()
        {
            if (IsAtomic)
                return HeadText;

            var s = new StringBuilder();

            s.Append(HeadText)
                .Append("[")
                .Append(_argsList[0]);

            for (var i = 1; i < _argsList.Count; i++)
                s.Append(",").Append(_argsList[i]);

            s.Append("]");

            return s.ToString();
        }

        public static bool operator ==(SteExpression symbolicExpr1, SteExpression symbolicExpr2)
        {
            if (ReferenceEquals(symbolicExpr1, symbolicExpr2))
                return true;

            if ((object) symbolicExpr1 == null || (object) symbolicExpr2 == null)
                return false;

            if (symbolicExpr1.HeadText != symbolicExpr2.HeadText)
                return false;

            if (symbolicExpr1.ArgumentsCount == 0 && symbolicExpr2.ArgumentsCount == 0)
                return true;

            if (symbolicExpr1._argsList.Count != symbolicExpr2._argsList.Count)
                return false;

            return !symbolicExpr1._argsList.Where((t, i) => !(t == symbolicExpr2._argsList[i])).Any();
        }

        public static bool operator !=(SteExpression symbolicExpr1, SteExpression symbolicExpr2)
        {
            return !(symbolicExpr1 == symbolicExpr2);
        }
    }
}
