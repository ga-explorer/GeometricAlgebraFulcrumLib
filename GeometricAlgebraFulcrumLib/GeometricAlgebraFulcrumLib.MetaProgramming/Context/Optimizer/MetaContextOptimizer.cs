using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Evaluation;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;
using GeometricAlgebraFulcrumLib.Utilities.Text.Loggers.Progress;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer;

internal sealed class MetaContextOptimizer : 
    MetaContextProcessorBase, 
    IProgressReportSource
{
    public static void Process(MetaContext context)
    {
        var optimizer = new MetaContextOptimizer(context);

        optimizer.BeginProcessing();
    }

    public static void Process(MetaContext context, Dictionary<string, IMetaExpressionVariableParameter> inputsWithTestValues)
    {
        var optimizer = new MetaContextOptimizer(context)
        {
            _inputsWithTestValues = inputsWithTestValues
        };

        optimizer.BeginProcessing();
    }


    private Dictionary<string, IMetaExpressionVariableParameter> _inputsWithTestValues;


    public bool EnableTestEvaluation 
        => Context.ContextOptions.EnableTestEvaluation && 
           _inputsWithTestValues.Count > 0;

    public MetaContextEvaluationDataHistory EvaluationDataHistory { get; private set; }

    public string ProgressSourceId 
        => "Symbolic Expressions Context Optimizer";

    public ProgressComposer Progress 
        => null;


    private MetaContextOptimizer(MetaContext context)
        : base(context)
    {
    }
        

    //private void OutputTrace(string traceItemTitle)
    //{
    //    if (ReferenceEquals(_progress, null))
    //        return;

    //    this.ReportNormal(traceItemTitle, CodeBlock.ToString());
    //}

    //private void OutputTrace(string traceItemTitle, string traceItemText)
    //{
    //    if (ReferenceEquals(_progress, null))
    //        return;

    //    this.ReportNormal(traceItemTitle, traceItemText);
    //}

    private void InitializeCodeBlock()
    {
        McOptDependencyUpdate.Process(Context);

        this.ReportNormal("Initialize Code Block", Context);

        if (!EnableTestEvaluation) 
            return;

        EvaluationDataHistory =
            _inputsWithTestValues == null || _inputsWithTestValues.Count == 0
                ? new MetaContextEvaluationDataHistory(Context, -5.0D, 5.0D)
                : new MetaContextEvaluationDataHistory(Context, _inputsWithTestValues);

        EvaluationDataHistory.AddEvaluation("Initialize Code Block");
    }

    private void ProcessSubExpressions()
    {
        //Use full reduction algorithm to produce less computations and simplest possible RHS expressions
        //but may take longer time and may require more temp variables
        if (Context.ContextOptions.ReduceLowLevelRhsSubExpressions)
        {
            McOptReduceRhsExpressions.Process(Context);

            this.ReportNormal("Reduce RHS Sub-expressions", Context);

            if (EnableTestEvaluation)
                EvaluationDataHistory.AddEvaluation("Reduce RHS Sub-expressions");

            return;
        }
            
        //Use partial reduction algorithm to factor out sub expressions used multiple times during 
        //computation but may produce larger RHS expressions per temp\output variable

        //Remove temp variables having duplicate RHS expressions
        McOptRemoveDuplicateTemps.Process(Context);

        this.ReportNormal("Remove Duplicate Temps", Context);

        if (EnableTestEvaluation)
            EvaluationDataHistory.AddEvaluation("Remove Duplicate Temps");

        //Factor common sub-expressions into separate low-level temp variables
        McOptFactorSubExpressions.Process(Context);

        this.ReportNormal("Factor Common Sub-expressions", Context);

        if (EnableTestEvaluation)
            EvaluationDataHistory.AddEvaluation("Factor Common Sub-expressions");
    }

    private void MergeSimpleSubexpressions()
    {
        var intermediateVariableList = 
            Context.GetIntermediateVariables().ToImmutableArray();

        // Merge RHS expressions containing a single variable
        foreach (var ctxVar in intermediateVariableList)
        {
            if (ctxVar.RhsVariables.Count() > 1) continue;

            var depVarList = 
                ctxVar.DirectDependingVariables.ToImmutableArray();

            if (depVarList.Length > 1) continue;
            
            var ctxVarName = ctxVar.InternalName;

            foreach (var depVar in depVarList)
                depVar.ReplaceRhsVariable(ctxVarName, ctxVar.RhsExpression);
        }
        
        // Merge RHS expressions containing a multiplication of atomics into 
        // expressions containing additions
        foreach (var ctxVar in intermediateVariableList)
        {
            if (!ctxVar.RhsExpression.IsTimesOfAtomics()) continue;

            var depVarList = 
                ctxVar.DirectDependingVariables.ToImmutableArray();

            if (depVarList.Length > 1) continue;
            
            depVarList = depVarList.Where(v => 
                v.RhsExpression.IsPlusContaining(ctxVar)
            ).ToImmutableArray();
            
            var ctxVarName = ctxVar.InternalName;

            foreach (var depVar in depVarList)
                depVar.ReplaceRhsVariable(ctxVarName, ctxVar.RhsExpression);
        }

        McOptDependencyUpdate.Process(Context);
    }

    private void MergeAffineSubexpressions()
    {
        var intermediateVariableList = 
            Context.GetIntermediateVariables();

        foreach (var ctxVar in intermediateVariableList)
        {
            if (!ctxVar.IsAffineCombination) continue;
            
            var depVarList = 
                ctxVar
                    .DirectDependingVariables
                    .Where(v => 
                        v.IsAffineCombination
                    ).ToImmutableArray();

            var ctxVarName = ctxVar.InternalName;

            foreach (var depVar in depVarList)
            {
                depVar.ReplaceRhsVariable(ctxVarName, ctxVar.RhsExpression);
                
                depVar.SimplifyRhsExpression();
            }
        }

        McOptDependencyUpdate.Process(Context);
    }

    //private void EnhanceRhsExpressions()
    //{
    //    foreach (var ctxVar in Context.GetComputedVariables())
    //        ctxVar.EnhanceRhsExpression();
    //}

    private void FinalizeCodeBlock()
    {
        McOptDependencyUpdate.Process(Context);

        //OutputTrace("Dependency Update");

        //Re-order computations so that less expensive output variables and temps are computed first
        McOptReOrderComputations.Process(Context);

        this.ReportNormal("Re-order Computations", Context);

        //Console.WriteLine("Before Merge Unary Expressions");
        //Console.WriteLine(Context);
        //Console.WriteLine();

        // TODO: This step produces wrong expressions, must be reviewed properly
        //Context.EnhanceRhsExpressions();
        
        //Console.WriteLine("After Enhance RHS Expressions");
        //Console.WriteLine(Context);
        //Console.WriteLine();
        
        if (EnableTestEvaluation)
            EvaluationDataHistory.AddEvaluation("Re-order computations");

        //Minimize number of temporary variables needed in the final code
        McOptReUseTempVariables.Process(Context);

        this.ReportNormal("Re-use Temp Variables", Context);

        if (!EnableTestEvaluation) 
            return;

        EvaluationDataHistory.AddEvaluation("Re-use temp variables");

        this.ReportNormal("Evaluation History", EvaluationDataHistory);
    }

    protected override void BeginProcessing()
    {
        InitializeCodeBlock();

        ProcessSubExpressions();
        
        MergeAffineSubexpressions();
        
        ProcessSubExpressions();
        
        MergeSimpleSubexpressions();
        
        FinalizeCodeBlock();
    }
}