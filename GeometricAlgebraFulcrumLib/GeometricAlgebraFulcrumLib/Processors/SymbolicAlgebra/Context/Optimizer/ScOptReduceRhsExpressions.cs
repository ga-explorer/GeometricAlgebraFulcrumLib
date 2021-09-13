using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Composite;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Variables;

namespace GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context.Optimizer
{
    internal sealed class ScOptReduceRhsExpressions : 
        SymbolicContextProcessorBase
    {
        public static void Process(SymbolicContext context)
        {
            var processor = new ScOptReduceRhsExpressions(context);

            processor.BeginProcessing();
        }


        private int _computationOrder;

        /// <summary>
        /// A dictionary holding all temp variables created during this stage of optimization where the key
        /// is the final RHS expression of the temp variable
        /// </summary>
        private readonly Dictionary<string, ISymbolicVariableComputed> _subExpressionsDictionary =
            new Dictionary<string, ISymbolicVariableComputed>();

        /// <summary>
        /// A cache for speeding up the ReduceSubExpression() method
        /// </summary>
        private readonly Dictionary<string, ISymbolicExpression> _reducedSubExpressionsCache =
            new Dictionary<string, ISymbolicExpression>();

        /// <summary>
        /// The intermediate variables of this context
        /// </summary>
        private readonly Dictionary<string, ISymbolicVariableComputed> _intermediateVariablesDictionary =
            new Dictionary<string, ISymbolicVariableComputed>();

        /// <summary>
        /// The output variables of the code block
        /// </summary>
        private readonly List<ISymbolicVariableComputed> _outputVariablesList = 
            new List<ISymbolicVariableComputed>();


        private ScOptReduceRhsExpressions(SymbolicContext context)
            : base(context)
        {
            
        }

        
        /// <summary>
        /// Analyze the contents of the initial expression. 
        /// If an atomic expression is found, stop and return true.
        /// If a single intermediate variable is found, analyze its RHS expression and iterate.
        /// If any other type of expression is found stop and return false.
        /// This is used to follow a chain of intermediate variables assigned
        /// to each other up to its root and later reduce the whole chain into a single
        /// intermediate variable.
        /// </summary>
        /// <param name="initialExpr"></param>
        /// <returns></returns>
        private ISymbolicExpression FollowIntermediateAssignmentsChain(ISymbolicExpression initialExpr)
        {
            //Start at the initial expression
            var finalExpr = initialExpr;

            //Iterate through the chain as long as the RHS expression is a single intermediate
            //variable.
            while (finalExpr.IsIntermediateVariable)
            {
                finalExpr = ((ISymbolicVariableComputed) finalExpr).RhsExpression;
            }

            //The final expression can only be a number, a parameter, an output variable,
            //or a composite expression
            return finalExpr;
        }

        /// <summary>
        /// Find or create a temp variable holding the given expression as its RHS
        /// </summary>
        /// <param name="subExpr"></param>
        /// <returns></returns>
        private ISymbolicVariableComputed GetOrDefineIntermediateVariable(ISymbolicExpression subExpr)
        {
            var subExprText = subExpr.ToString() ?? string.Empty;

            //A temp is found; return it
            if (_subExpressionsDictionary.TryGetValue(subExprText, out var intermediateVariable))
                return intermediateVariable;
            
            //A temp is not found; create it and return it
            intermediateVariable = Context.DefineSubExpressionVariable(
                subExpr, 
                false
            );

            _subExpressionsDictionary.Add(subExprText, intermediateVariable);

            intermediateVariable.SetComputationOrder(_computationOrder++);

            _intermediateVariablesDictionary.Add(intermediateVariable.InternalName, intermediateVariable);

            return intermediateVariable;
        }

        /// <summary>
        /// For example, convert Plus[a, b, c, d] expression into Plus[Plus[Plus[a, b], c], d]
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        private ISymbolicFunction ReshapeAssociativeFunction(ISymbolicFunction expr)
        {
            if (expr.Count < 3 || expr.IsNonAssociative)
                return expr;

            if (expr.IsLeftAssociative)
            {
                var argExpr1 = expr[0];

                for (var i = 1; i < expr.Count; i++)
                {
                    var argExpr2 = expr[i];

                    argExpr1 = SymbolicFunction.Create(
                        expr.FunctionHeadSpecs,
                        argExpr1, argExpr2
                    );
                }

                return (ISymbolicFunction) argExpr1;
            }
            else
            {
                var argExpr2 = expr[^1];

                for (var i = expr.Count - 2; i >= 0; i--)
                {
                    var argExpr1 = expr[i];

                    argExpr2 = SymbolicFunction.Create(
                        expr.FunctionHeadSpecs,
                        argExpr1, argExpr2
                    );
                }

                return (ISymbolicFunction) argExpr2;
            }
        }

        /// <summary>
        /// Reduce a complex expression into a simpler one by refactoring all of the sub-expressions into
        /// intermediate variables, parameters, or numbers. If the initial expression is already
        /// an atomic expression, just return it as is.
        /// </summary>
        /// <param name="initialExpr"></param>
        /// <returns></returns>
        private ISymbolicExpression ReduceSubExpression(ISymbolicExpression initialExpr)
        {
            var initialExprText = initialExpr.ToString() ?? string.Empty;

            //Try to find the initial expression in the cache
            if (_reducedSubExpressionsCache.TryGetValue(initialExprText, out var reducedExpr))
                return reducedExpr;

            //An atomic expression or an undefined symbol is found for the sub-expression
            reducedExpr = FollowIntermediateAssignmentsChain(initialExpr);

            if (reducedExpr.IsAtomic)
            {
                _reducedSubExpressionsCache.Add(initialExprText, reducedExpr);

                return reducedExpr;
            }

            //A compound expression is found for the sub-expression
            var compositeExpr = (ISymbolicExpressionComposite) reducedExpr;

            //For example, convert Plus[a, b, c, d] expression into Plus[Plus[Plus[a, b], c], d]
            if (compositeExpr is ISymbolicFunction functionExpr)
                compositeExpr = ReshapeAssociativeFunction(functionExpr);

            //Create a new RHS expression from the reduced arguments
            compositeExpr = compositeExpr.GetExpressionCopy(
                compositeExpr.Arguments.Select(ReduceSubExpression)
            );
            
            //Find or create a temp variable to hold the new RHS expression
            var intermediateVariable = 
                GetOrDefineIntermediateVariable(compositeExpr);

            //Add reduced expression to cache
            _reducedSubExpressionsCache.Add(initialExprText, intermediateVariable);

            return intermediateVariable;
        }

        private void AddOutputVariable(ISymbolicVariableComputed outputVar)
        {
            //An atomic expression is found for the variable's RHS side
            var rhsExpression = 
                FollowIntermediateAssignmentsChain(outputVar.RhsExpression);

            if (rhsExpression.IsAtomic)
            {
                outputVar.ResetRhsExpression(rhsExpression);

                outputVar.SetComputationOrder(_computationOrder++);

                _outputVariablesList.Add(outputVar);

                return;
            }

            //A composite expression is found for the variable's RHS side
            var compositeExpr = (ISymbolicExpressionComposite) rhsExpression;

            //For example, convert Plus[a, b, c, d] expression into Plus[Plus[Plus[a, b], c], d]
            if (compositeExpr is ISymbolicFunction functionExpr)
                compositeExpr = ReshapeAssociativeFunction(functionExpr);

            outputVar.ResetRhsExpression(
                compositeExpr.GetExpressionCopy(
                    compositeExpr.Arguments.Select(ReduceSubExpression)
                )
            );

            outputVar.SetComputationOrder(_computationOrder++);

            _outputVariablesList.Add(outputVar);
        }

        protected override void BeginProcessing()
        {
            //Add output variables and create new intermediate variables
            foreach (var outputVar in Context.GetOutputVariables().ToArray())
                AddOutputVariable(outputVar);

            var computedVariables =
                _intermediateVariablesDictionary
                    .Values
                    .Concat(_outputVariablesList)
                    .OrderBy(v => v.ComputationOrder);

            Context.ResetComputedVariables(computedVariables);

            ScOptDependencyUpdate.Process(Context);
        }
    }
}
