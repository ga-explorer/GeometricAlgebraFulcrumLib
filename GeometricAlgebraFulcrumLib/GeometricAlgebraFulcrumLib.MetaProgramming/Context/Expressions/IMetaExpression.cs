using AngouriMath;
using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Evaluators;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Numbers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;

public interface IMetaExpression :
    IMetaExpressionElement
{
    MetaContext Context { get; }

    /// <summary>
    /// The specifications of this expression
    /// </summary>
    IMetaExpressionHeadSpecs HeadSpecs { get; }

    /// <summary>
    /// The head text of this symbolic expression
    /// </summary>
    string HeadText { get; }

    /// <summary>
    /// True if this is an atomic number expression
    /// </summary>
    bool IsNumber { get; }

    /// <summary>
    /// True for atomic number literals (i.e. non-symbolic number values like 0, 2.1, -3.2e-5, etc.)
    /// </summary>
    bool IsLiteralNumber { get; }

    /// <summary>
    /// True for atomic number symbols like Pi and e.
    /// </summary>
    bool IsSymbolicNumber { get; }

    /// <summary>
    /// True if this is an atomic symbol expression (a variable or a symbolic number)
    /// </summary>
    bool IsNumberSymbolOrVariable { get; }

    /// <summary>
    /// True if this is an atomic independent expression (a parameter variable or a number)
    /// </summary>
    bool IsNumberOrParameter { get; }

    /// <summary>
    /// True if this is an atomic variable expression
    /// </summary>
    bool IsVariable { get; }

    bool IsParameterVariable { get; }

    bool IsComputedVariable { get; }

    /// <summary>
    /// True if this is a computed expression (an atomic computed variable or a composite expression)
    /// </summary>
    bool IsComputedVariableOrComposite { get; }

    bool IsIntermediateVariable { get; }

    bool IsOutputVariable { get; }

    /// <summary>
    /// True if this is a composite function expression
    /// </summary>
    bool IsFunction { get; }

    /// <summary>
    /// True if this is a composite array access expression
    /// </summary>
    bool IsArrayAccess { get; }

    /// <summary>
    /// True if this is a composite operator expression
    /// </summary>
    bool IsOperator { get; }

    /// <summary>
    /// True if this is an atomic expression (a variable or number)
    /// </summary>
    bool IsAtomic { get; }

    /// <summary>
    /// True if this is a composite expression
    /// </summary>
    bool IsComposite { get; }

    /// <summary>
    /// A rough estimate of the number of computations in this expression
    /// tree computed by summing each tree node's number of direct child nodes
    /// </summary>
    /// <returns></returns>
    int ComputationsCount { get; }

    /// <summary>
    /// Traverse the whole expression tree starting at this root
    /// and return all expressions
    /// </summary>
    IEnumerable<IMetaExpression> Expressions { get; }

    /// <summary>
    /// Traverse the whole expression tree starting at this root's children
    /// and return all expressions
    /// </summary>
    IEnumerable<IMetaExpression> SubExpressions { get; }

    /// <summary>
    /// Traverse the whole expression tree starting at this root
    /// and return all atomic expressions
    /// </summary>
    IEnumerable<IMetaExpressionAtomic> AtomicExpressions { get; }

    /// <summary>
    /// Traverse the whole expression tree starting at this root's children
    /// and return all atomic expressions
    /// </summary>
    IEnumerable<IMetaExpressionAtomic> AtomicSubExpressions { get; }

    /// <summary>
    /// Traverse the whole expression tree starting at this root
    /// and return all composite expressions
    /// </summary>
    IEnumerable<IMetaExpressionComposite> CompositeExpressions { get; }

    /// <summary>
    /// Traverse the whole expression tree starting at this root's children
    /// and return all composite expressions
    /// </summary>
    IEnumerable<IMetaExpressionComposite> CompositeSubExpressions { get; }

    /// <summary>
    /// Traverse the whole expression tree starting at this root
    /// and return all number expressions
    /// </summary>
    IEnumerable<IMetaExpressionNumber> NumberExpressions { get; }

    /// <summary>
    /// Traverse the whole expression tree starting at this root's children
    /// and return all number expressions
    /// </summary>
    IEnumerable<IMetaExpressionNumber> NumberSubExpressions { get; }

    /// <summary>
    /// Traverse the whole expression tree starting at this root
    /// and return all variables
    /// </summary>
    IEnumerable<IMetaExpressionVariable> VariableExpressions { get; }

    /// <summary>
    /// Traverse the whole expression tree starting at this root's children
    /// and return all variables
    /// </summary>
    IEnumerable<IMetaExpressionVariable> VariableSubExpressions { get; }

    /// <summary>
    /// Traverse the whole expression tree starting at this root
    /// and return all parameter variables
    /// </summary>
    IEnumerable<IMetaExpressionVariableParameter> VariableParameterExpressions { get; }

    /// <summary>
    /// Traverse the whole expression tree starting at this root's children
    /// and return all parameter variables
    /// </summary>
    IEnumerable<IMetaExpressionVariableParameter> VariableParameterSubExpressions { get; }

    /// <summary>
    /// Traverse the whole expression tree starting at this root
    /// and return all computed variables
    /// </summary>
    IEnumerable<IMetaExpressionVariableComputed> VariableComputedExpressions { get; }

    /// <summary>
    /// Traverse the whole expression tree starting at this root's children
    /// and return all computed variables
    /// </summary>
    IEnumerable<IMetaExpressionVariableComputed> VariableComputedSubExpressions { get; }

    IMetaExpression Simplify(IMetaExpressionEvaluator symbolicEvaluator);


    /// <summary>
    /// Find all instances of the given oldExpr inside this expression tree 
    /// and replace them with a copy of newExpr. The search is done once and
    /// only instances near the leaf of the tree are replaced (i.e. one
    /// search-replace iteration over the tree)
    /// </summary>
    /// <param name="oldExpr"></param>
    /// <param name="newExpr"></param>
    /// <returns>A tuple with the first item indicating if the expression is found and the second item the modified expression</returns>
    Tuple<bool, IMetaExpression> ReplaceAllExpressionByExpression(IMetaExpression oldExpr, IMetaExpression newExpr);

    /// <summary>
    /// Find all instances of the given oldExpr inside this expression tree 
    /// and replace them with a given variable. The search is done once and 
    /// only instances near the leaf of the tree are replaced (i.e. one
    /// search-replace iteration over the tree). The variable must be already
    /// defined inside the context.
    /// </summary>
    /// <param name="oldExpr"></param>
    /// <param name="variableName"></param>
    /// <param name="context"></param>
    /// <returns>A tuple with the first item indicating if the expression is found and the second item the modified expression</returns>
    Tuple<bool, IMetaExpression> ReplaceAllExpressionByVariable(IMetaExpression oldExpr, string variableName, MetaContext context);

    /// <summary>
    /// Find all instances of the given old variable inside this expression tree 
    /// and replace them with a given new variable. The search is done once and 
    /// only instances near the leaf of the tree are replaced (i.e. one
    /// search-replace iteration over the tree). The variables must be already
    /// defined inside the context.
    /// </summary>
    /// <param name="oldVariableName"></param>
    /// <param name="newVariableName"></param>
    /// <param name="context"></param>
    /// <returns>A tuple with the first item indicating if the expression is found and the second item the modified expression</returns>
    Tuple<bool, IMetaExpression> ReplaceAllVariableByVariable(string oldVariableName, string newVariableName, MetaContext context);

    /// <summary>
    /// Create a deep copy of this expression into a new context
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    IMetaExpression CopyToContext(MetaContext context);

    ///// <summary>
    ///// Create a deep copy of this expression within a new context
    ///// </summary>
    ///// <returns></returns>
    //IMetaExpression GetExpressionCopy(MetaContext context);

    /// <summary>
    /// Construct an equivalent copy of this expression tree as an AngouriMath Entity object
    /// https://am.angouri.org/
    /// </summary>
    /// <returns></returns>
    Entity ToAngouriMathEntity();

    /// <summary>
    /// Construct an equivalent copy of this expression tree as a simple text expression
    /// tree
    /// </summary>
    /// <returns></returns>
    SteExpression ToSimpleTextExpression();
}