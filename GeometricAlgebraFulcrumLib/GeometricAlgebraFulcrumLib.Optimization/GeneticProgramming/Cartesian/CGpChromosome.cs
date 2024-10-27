using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian;

public class CGpChromosome : 
    IEquatable<CGpChromosome>
{
    public static CGpChromosome Create(CGpContext context)
    {
        return new CGpChromosome(context);
    }

    //public static CGpChromosome CreateFromFile(string file)
    //{
    //    int i;

    //    var funcName = new string(new char[CGpConstants.FunctionNameLength]);
    //    var buffer = new string(new char[8192]);

    //    /* open the chromosome file */
    //    FILE fp = fopen(file, "r");

    //    /* ensure that the file was opened correctly */
    //    if (fp == null)
    //    {
    //        Console.WriteLine("Warning: cannot open chromosome: '%s'. Chromosome was not open.\n", file);
    //        return null;
    //    }

    //    /* get num inputs */
    //    string line = fgets(buffer, sizeof(char), fp);
    //    if (line == null)
    //    {
    //    }
    //    string record = strtok(line, ",");
    //    record = strtok(null, ",");
    //    int numInputs = atoi(record);

    //    /* get num nodes */
    //    line = fgets(buffer, sizeof(char), fp);
    //    if (line == null)
    //    {
    //    }
    //    record = strtok(line, ",");
    //    record = strtok(null, ",");
    //    int numNodes = atoi(record);

    //    /* get num outputs */
    //    line = fgets(buffer, sizeof(char), fp);
    //    if (line == null)
    //    {
    //    }
    //    record = strtok(line, ",");
    //    record = strtok(null, ",");
    //    int numOutputs = atoi(record);

    //    /* get arity */
    //    line = fgets(buffer, sizeof(char), fp);
    //    if (line == null)
    //    {
    //    }
    //    record = strtok(line, ",");
    //    record = strtok(null, ",");
    //    int arity = atoi(record);

    //    /* initialise parameters  */
    //    var cgpParameters = CGpParameters.Create(numInputs, numNodes, numOutputs, arity);

    //    /* get and set node functions */
    //    line = fgets(buffer, sizeof(char), fp);
    //    if (line == null)
    //    {
    //    }
    //    record = strtok(line, ",\n");
    //    record = strtok(null, ",\n");

    //    /* for each function name */
    //    while (record != null)
    //    {

    //        funcName = record;

    //        /* can only load functions defined within CGP-Library */
    //        if (cgpParameters.AddPresetFunctionToFunctionSet(funcName) == 0)
    //        {
    //            throw new InvalidOperationException("Error: cannot load chromosome which contains custom node functions.\n");
    //            Console.WriteLine("Terminating CGP-Library.\n");
    //            Globals.FreeParameters(cgpParameters);
    //            return null;
    //        }

    //        record = strtok(null, ",\n");
    //    }

    //    /* initialise a chromosome beased on the parameters accociated with given chromosome */
    //    var chromosome = Create(cgpParameters);

    //    /* set the node parameters */
    //    for (i = 0; i < numNodes; i++)
    //    {

    //        /* get the function gene */
    //        line = fgets(buffer, sizeof(char), fp);
    //        record = strtok(line, ",\n");
    //        chromosome.Nodes[i].FunctionIndex = atoi(record);

    //        for (var j = 0; j < arity; j++)
    //        {
    //            line = fgets(buffer, sizeof(char), fp);
    //            sscanf(line, "%d,%lf", chromosome.Nodes[i].Inputs[j], chromosome.Nodes[i].Weights[j]);
    //        }
    //    }

    //    /* set the outputs */
    //    line = fgets(buffer, sizeof(char), fp);
    //    record = strtok(line, ",\n");
    //    chromosome.OutputNodes[0] = atoi(record);

    //    for (i = 1; i < numOutputs; i++)
    //    {
    //        record = strtok(null, ",\n");
    //        chromosome.OutputNodes[i] = atoi(record);
    //    }

    //    fclose(fp);
    //    Globals.FreeParameters(cgpParameters);

    //    /* set the active nodes the copied chromosome */
    //    chromosome.SetChromosomeActiveNodes();

    //    return chromosome;
    //}

    public static CGpChromosome CreateFromChromosome(CGpChromosome chromosome)
    {
        /* allocate memory for chromosome */
        var chromosomeNew = new CGpChromosome(chromosome.Context)
        {
            /* allocate memory for nodes */
            Nodes = new CGpNode[chromosome.NodeCount],
        };

        /* Initialise each of the chromosomes nodes */
        for (var i = 0; i < chromosome.NodeCount; i++)
        {
            chromosomeNew.Nodes[i] = chromosome.Nodes[i].GetCopy(chromosomeNew, i);

            //chromosomeNew.Nodes[i] = CGpNode.CreateUnitWeightsNonRecurrent(chromosome, i);

            //chromosome.Nodes[i].CopyToNode(chromosomeNew.Nodes[i]);
        }

        /* set each of the chromosomes outputs */
        for (var i = 0; i < chromosome.Parameters.OutputSize; i++) 
            chromosomeNew.OutputNodeIndices[i] = chromosome.OutputNodeIndices[i];

        /* set the number of inputs, nodes and outputs */
        chromosomeNew.NodeCount = chromosome.NodeCount;
        
        /* copy over the chromosome fitness */
        chromosomeNew.Cost = chromosome.Cost;

        /* copy over the number of generations to find a solution */
        chromosomeNew.Generation = chromosome.Generation;

        /* set the active nodes the newly generated chromosome */
        chromosomeNew.UpdateActiveNodes();

        return chromosomeNew;
    }

    
    public static bool operator ==(CGpChromosome? left, CGpChromosome? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(CGpChromosome? left, CGpChromosome? right)
    {
        return !Equals(left, right);
    }


    public CGpContext Context { get; }

    public CGpParameters Parameters 
        => Context.Parameters;
    
    public int Generation { get; internal set; }

    public int NodeCount { get; private set; }

    public int ActiveNodeCount { get; private set; }

    public CGpNode[] Nodes { get; private init; }

    public int[] OutputNodeIndices { get; }

    public int[] ActiveNodeIndices { get; }

    public IEnumerable<CGpNode> ActiveNodes 
        => ActiveNodeIndices.Select(i => Nodes[i]);

    public double Cost { get; private set; }

    public double[] CostGradient { get; private set; }

    public double[] OutputValues { get; }
    
    public IEnumerable<CGpNodeInputWeight> NodeParametricWeights
        => Nodes
            .SelectMany(node => node.Weights.Take(node.ActualArity))
            .Where(w => w.IsParametric);

    public IEnumerable<CGpNodeInputWeight> ActiveNodeParametricWeights
        => ActiveNodes
            .SelectMany(node => node.Weights.Take(node.ActualArity))
            .Where(w => w.IsParametric);
    
    public bool ContainsParametricWeights
        => ActiveNodeParametricWeights.Any();

    public bool ContainsActiveParametricWeights
        => ActiveNodeParametricWeights.Any();


    private CGpChromosome(CGpContext context)
    {
        Context = context;

        // check that funcSet contains functions
        if (Parameters.FunctionSet.Count < 1)
            throw new InvalidOperationException(
                "Error: chromosome not initialised due to empty function set.\nTerminating CGP-Library.\n"
            );
        
        NodeCount = Parameters.MaxNodeCount;
        ActiveNodeCount = Parameters.MaxNodeCount;
        Cost = double.PositiveInfinity;
        Nodes = new CGpNode[Parameters.MaxNodeCount];
        OutputNodeIndices = new int[Parameters.OutputSize];
        ActiveNodeIndices = new int[Parameters.MaxNodeCount];
        OutputValues = new double[Parameters.OutputSize];
        
        for (var i = 0; i < Parameters.MaxNodeCount; i++) 
            Nodes[i] = CGpNode.Create(this, i);

        for (var i = 0; i < Parameters.OutputSize; i++)
        {
            SetOutputNodeIndexToRandom(i);
        }

        // set the active nodes
        UpdateActiveNodes();
    }


    public double GetNodeOutputValue(int nodeIndex)
    {
        return Nodes[nodeIndex].OutputValue;
    }

    public int GetRandomOutputIndex()
    {
        return Parameters.ShortcutConnections
            ? CGpParameters.GetRandomIndex(Parameters.InputSize + NodeCount) 
            : CGpParameters.GetRandomIndex(NodeCount) + Parameters.InputSize;
    }

    public void SetOutputNodeIndexToRandom(int outputNodeIndex)
    {
        OutputNodeIndices[outputNodeIndex] = GetRandomOutputIndex();
    }

    private void UpdateActiveNodes()
    {
        ActiveNodeCount = 0;

        // reset the active nodes
        for (var i = 0; i < NodeCount; i++) 
            Nodes[i].IsActive = false;

        // start the recursive search for active nodes from the output nodes
        for (var i = 0; i < Parameters.OutputSize; i++)
        {
            var outputNodeIndex = OutputNodeIndices[i];

            // if the output connects to a chromosome input, skip
            if (outputNodeIndex < Parameters.InputSize)
                continue;

            // begin a recursive search for active nodes
            UpdateActiveNodesRecursive(outputNodeIndex);
        }

        // place active nodes order
        Array.Sort(ActiveNodeIndices);
    }

    private void UpdateActiveNodesRecursive(int nodeIndex)
    {
        // the given node is an input
        if (nodeIndex < Parameters.InputSize)
            return;

        nodeIndex -= Parameters.InputSize;
        var node = Nodes[nodeIndex];

        // the given node has already been flagged as active
        if (node.IsActive)
            return;

        // flag the node as active
        node.IsActive = true;
        ActiveNodeIndices[ActiveNodeCount] = nodeIndex;
        ActiveNodeCount++;

        // set the node's actual arity
        //node.UpdateActualArity();

        // recursively flag all the nodes to which the current node connects as active
        for (var i = 0; i < node.ActualArity; i++)
        {
            var inputNodeIndex = node.InputIndices[i];

            // if the node connects to a chromosome input, skip
            if (inputNodeIndex < Parameters.InputSize)
                continue;
            
            UpdateActiveNodesRecursive(inputNodeIndex);
        }
    }


    public void PrintChromosome(bool showWeights)
    {
        /* set the active nodes the given chromosome */
        UpdateActiveNodes();

        /* for all the chromosome inputs*/
        for (var i = 0; i < Parameters.InputSize; i++) 
            Console.WriteLine("({0}):\tinput\n", i);

        /* for all the hidden nodes */
        for (var i = 0; i < NodeCount; i++)
        {
            /* print the node function */
            Console.WriteLine("({0}):\t{1}\t", Parameters.InputSize + i, Nodes[i].FunctionName);

            /* for the arity of the node */
            var nodeActualArity = Nodes[i].ActualArity;
            for (var j = 0; j < nodeActualArity; j++)
            {
                /* print the node input information */
                if (showWeights)
                    Console.WriteLine("{0},{1}\t", Nodes[i].InputIndices[j], Nodes[i].Weights[j]);
                else
                    Console.WriteLine("{0} ", Nodes[i].InputIndices[j]);
            }

            /* Highlight active nodes */
            if (Nodes[i].IsActive) 
                Console.WriteLine("*");

            Console.WriteLine("\n");
        }

        /* for all outputs */
        Console.WriteLine("outputs: ");
        for (var i = 0; i < Parameters.OutputSize; i++)
        {
            /* print the output node locations */
            Console.WriteLine("{0} ", OutputNodeIndices[i]);
        }

        Console.WriteLine("\n\n");
    }

    public void ExecuteChromosome(IReadOnlyList<double> chromosomeInputValues)
    {
        // Compute outputs of all active nodes
        foreach (var node in ActiveNodes)
            node.ExecuteNode(chromosomeInputValues);

        //foreach (var node in ActiveNodes)
        //{
        //    var nodeInputValues = new double[Parameters.MaxArity];

        //    // for each of the active node's inputs
        //    for (var j = 0; j < Parameters.MaxArity; j++)
        //    {
        //        // gather the nodes input locations
        //        var nodeInputIndex = node.InputIndices[j];

        //        // Store the actual value of the node's input
        //        nodeInputValues[j] =
        //            nodeInputIndex < Parameters.InputSize
        //                ? chromosomeInputValues[nodeInputIndex]
        //                : Nodes[nodeInputIndex - Parameters.InputSize].OutputValue;
        //    }

        //    // calculate the output of the active node under evaluation
        //    node.UpdateOutputValue(nodeInputValues);
        //}

        // Update the chromosome outputs
        for (var i = 0; i < Parameters.OutputSize; i++)
        {
            var outputNodeIndex = OutputNodeIndices[i];

            OutputValues[i] = 
                outputNodeIndex < Parameters.InputSize
                    ? chromosomeInputValues[outputNodeIndex]
                    : Nodes[outputNodeIndex - Parameters.InputSize].OutputValue;
        }
    }

    public void MutateChromosome(bool parametric)
    {
        Parameters.MutationType.ApplyMutation(this, false);
        UpdateActiveNodes();

        //Parameters.MutationType.ApplyMutation(this, parametric);

        //if (!parametric)
        //{
        //    UpdateActiveNodes();

        //    if (ContainsActiveParameters)
        //    {
        //        var optimizedChromosome = Context.OptimizeParameters(
        //            this, 
        //            1000
        //        );

        //        optimizedChromosome.CopyToChromosome(this);
        //    }
        //}
    }

    public void RemoveInactiveNodes()
    {
        /* set the active nodes */
        UpdateActiveNodes();

        /* for all nodes */
        for (var i = 0; i < NodeCount - 1; i++)
        {
            if (Nodes[i].IsActive) continue;

            /* if the node is inactive */
            /* set the node to be the next node */
            for (var j = i; j < NodeCount - 1; j++)
            {
                //Nodes[j] = Nodes[j + 1].GetCopy();

                Nodes[j + 1].CopyToNode(Nodes[j]);
            }

            /* */
            for (var j = 0; j < NodeCount; j++)
            {
                for (var k = 0; k < Parameters.MaxArity; k++)
                {
                    if (Nodes[j].InputIndices[k] >= i + Parameters.InputSize)
                    {
                        Nodes[j].InputIndices[k]--;
                    }
                }
            }

            /* for the number of chromosome outputs */
            for (var j = 0; j < Parameters.OutputSize; j++)
            {

                if (OutputNodeIndices[j] >= i + Parameters.InputSize)
                {
                    OutputNodeIndices[j]--;
                }
            }

            /* de-increment the number of nodes */
            NodeCount--;

            /* made the newly assigned node be evaluated */
            i--;
        }

        // TODO: Fix this
//        for (var i = NodeCount; i < originalNumNodes; i++)
//        {
//            Nodes[i].FreeNode();
//        }

//        if (!Nodes[NodeCount - 1].IsActive)
//        {
//            Nodes[NodeCount - 1].FreeNode();
//            NodeCount--;
//        }

//        /* reallocate the memory associated with the chromosome */
//// C++ TO C# CONVERTER TASK: The memory management function 'realloc' has no equivalent C#:
//        Nodes = (CGpNode)realloc(Nodes, NodeCount * sizeof(CGpNode));
//// C++ TO C# CONVERTER TASK: The memory management function 'realloc' has no equivalent C#:
//        ActiveNodes = (int)realloc(ActiveNodes, NodeCount * sizeof(int));

        /* set the active nodes */
        UpdateActiveNodes();
    }

    public void UpdateCost(CGpChromosomeEvaluator evaluator)
    {
        UpdateActiveNodes();

        ResetNodeOutputValues();

        Cost = evaluator.ComputeCost(this);

        //evaluator.OptimizeCost(this);
    }

    public void ResetNodeOutputValues()
    {
        for (var i = 0; i < NodeCount; i++) 
            Nodes[i].ResetOutputValue();
    }

    public void CopyToChromosome(CGpChromosome dstChromosome)
    {
        /* error checking  */
        if (dstChromosome.Parameters.InputSize != Parameters.InputSize)
            throw new InvalidOperationException(
                "Error: cannot copy a chromosome to a chromosome of different dimensions. The number of chromosome inputs do not match.\n"
            );

        if (dstChromosome.NodeCount != NodeCount)
            throw new InvalidOperationException("Error: cannot copy a chromosome to a chromosome of different dimensions. The number of chromosome nodes do not match.\n");

        if (dstChromosome.Parameters.OutputSize != Parameters.OutputSize)
            throw new InvalidOperationException("Error: cannot copy a chromosome to a chromosome of different dimensions. The number of chromosome outputs do not match.\n");

        if (dstChromosome.Parameters.MaxArity != Parameters.MaxArity)
            throw new InvalidOperationException("Error: cannot copy a chromosome to a chromosome of different dimensions. The arity of the chromosome nodes do not match.\n");

        /* copy nodes and which are active */
        for (var i = 0; i < NodeCount; i++)
        {
            //dstChromosome.Nodes[i] = Nodes[i].GetCopy(dstChromosome, i);
            Nodes[i].CopyToNode(dstChromosome.Nodes[i]);
            dstChromosome.ActiveNodeIndices[i] = ActiveNodeIndices[i];
        }

        /* copy each of the chromosomes outputs */
        for (var i = 0; i < Parameters.OutputSize; i++) 
            dstChromosome.OutputNodeIndices[i] = OutputNodeIndices[i];

        /* copy the number of active node */
        dstChromosome.ActiveNodeCount = ActiveNodeCount;

        /* copy the fitness */
        dstChromosome.Cost = Cost;

        /* copy generation */
        dstChromosome.Generation = Generation;
    }

    public int GetActiveConnectionCount()
    {
        return ActiveNodeCount.GetRange(
            i => Nodes[ActiveNodeIndices[i]].ActualArity
        ).Sum();

        //var count = 0;

        //for (var i = 0; i < ActiveNodeCount; i++) 
        //    count += Nodes[ActiveNodes[i]].ActualArity;

        //return count;
    }


    public bool Equals(CGpChromosome? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        /* Check the high level parameters */
        if (Parameters.InputSize != other.Parameters.InputSize) return false;
        if (Parameters.OutputSize != other.Parameters.OutputSize) return false;
        if (Parameters.MaxArity != other.Parameters.MaxArity) return false;
        if (NodeCount != other.NodeCount) return false;

        /* for each node */
        for (var i = 0; i < NodeCount; i++)
        {
            /* Check the function genes */
            if (Nodes[i].Function != other.Nodes[i].Function)
                return false;

            /* for each node input */
            for (var j = 0; j < Parameters.MaxArity; j++)
            {
                /* Check the node inputs */
                if (Nodes[i].InputIndices[j] != other.Nodes[i].InputIndices[j])
                    return false;
            }
        }

        /* for outputs */
        for (var i = 0; i < Parameters.OutputSize; i++)
        {
            /* Check the outputs */
            if (OutputNodeIndices[i] != other.OutputNodeIndices[i])
                return false;
        }

        return true;
    }
    
    public bool EqualsAnn(CGpChromosome? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        /* Check the high level parameters */
        if (Parameters.InputSize != other.Parameters.InputSize) return false;
        if (Parameters.OutputSize != other.Parameters.OutputSize) return false;
        if (Parameters.MaxArity != other.Parameters.MaxArity) return false;
        if (NodeCount != other.NodeCount) return false;

        /* for each node */
        for (var i = 0; i < NodeCount; i++)
        {
            /* Check the function genes */
            if (Nodes[i].Function != other.Nodes[i].Function)
                return false;

            /* for each node input */
            for (var j = 0; j < Parameters.MaxArity; j++)
            {
                // Check the node inputs
                if (Nodes[i].InputIndices[j] != other.Nodes[i].InputIndices[j])
                    return false;
                
                // Check the connection weights inputs
                //if (Nodes[i].Weights[j] != other.Nodes[i].Weights[j])
                //    return false;
            }
        }

        /* for outputs */
        for (var i = 0; i < Parameters.OutputSize; i++)
        {
            /* Check the outputs */
            if (OutputNodeIndices[i] != other.OutputNodeIndices[i])
                return false;
        }

        return true;
    }
    
    public bool EqualsActiveNodes(CGpChromosome? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        /* Check the high level parameters */
        if (Parameters.InputSize != other.Parameters.InputSize) return false;
        if (Parameters.OutputSize != other.Parameters.OutputSize) return false;
        if (Parameters.MaxArity != other.Parameters.MaxArity) return false;
        if (NodeCount != other.NodeCount) return false;

        /* for each node*/
        for (var i = 0; i < NodeCount; i++)
        {
            /* The node is inactive in both chromosomes; do nothing * /
            if (Nodes[i].IsActive != other.Nodes[i].IsActive)
                return false;

            /* if the node is inactive in both chromosomes */
            if (!Nodes[i].IsActive && !other.Nodes[i].IsActive) continue;

            /* if the node is active in both chromosomes */
            /* Check the function genes */
            if (Nodes[i].Function != other.Nodes[i].Function)
                return false;

            /* for each node input */
            for (var j = 0; j < Parameters.MaxArity; j++)
            {
                /* Check the node inputs */
                if (Nodes[i].InputIndices[j] != other.Nodes[i].InputIndices[j])
                    return false;
            }
        }

        /* for outputs */
        for (var i = 0; i < Parameters.OutputSize; i++)
        {
            /* Check the outputs */
            if (OutputNodeIndices[i] != other.OutputNodeIndices[i])
                return false;
        }

        return true;
    }

    public bool EqualsActiveNodesAnn(CGpChromosome? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        /* Check the high level parameters */
        if (Parameters.InputSize != other.Parameters.InputSize) return false;
        if (Parameters.OutputSize != other.Parameters.OutputSize) return false;
        if (Parameters.MaxArity != other.Parameters.MaxArity) return false;
        if (NodeCount != other.NodeCount) return false;

        /* for each node*/
        for (var i = 0; i < NodeCount; i++)
        {
            /* The node is inactive in both chromosomes; do nothing * /
            if (Nodes[i].IsActive != other.Nodes[i].IsActive)
                return false;

            /* if the node is inactive in both chromosomes */
            if (!Nodes[i].IsActive && !other.Nodes[i].IsActive) continue;

            /* if the node is active in both chromosomes */
            /* Check the function genes */
            if (Nodes[i].Function != other.Nodes[i].Function)
                return false;

            /* for each node input */
            for (var j = 0; j < Parameters.MaxArity; j++)
            {
                /* Check the node inputs */
                if (Nodes[i].InputIndices[j] != other.Nodes[i].InputIndices[j])
                    return false;

                // Check the connection weights inputs
                //if (Nodes[i].Weights[j] != other.Nodes[i].Weights[j])
                //    return false;
            }
        }

        /* for outputs */
        for (var i = 0; i < Parameters.OutputSize; i++)
        {
            /* Check the outputs */
            if (OutputNodeIndices[i] != other.OutputNodeIndices[i])
                return false;
        }

        return true;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;

        return Equals((CGpChromosome)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Parameters.InputSize, Parameters.OutputSize, Parameters.MaxArity, OutputNodeIndices);
    }


    //public void SaveChromosome(string fileName)
    //{

    //    int i;

    //    FILE fp =
    //        /* create the chromosome file */
    //        fopen(fileName, "w");

    //    /* ensure that the file was created correctly */
    //    if (fp == null)
    //    {
    //        Console.WriteLine("Warning: cannot save chromosome to '%s'. Chromosome was not saved.\n", fileName);
    //        return;
    //    }

    //    /* save meta information */
    //    fp.AppendLine("numInputs,%d\n", Parameters.InputSize);
    //    fp.AppendLine("numNodes,%d\n", NodeCount);
    //    fp.AppendLine("numOutputs,%d\n", Parameters.OutputSize);
    //    fp.AppendLine("arity,%d\n", Arity);

    //    fp.AppendLine("functionSet");

    //    for (i = 0; i < FuncSet.NumFunctions; i++)
    //    {
    //        fp.AppendLine(",%s", FuncSet.FunctionNames[i]);
    //    }
    //    fp.AppendLine("\n");

    //    /* save the chromosome structure */
    //    for (i = 0; i < NodeCount; i++)
    //    {

    //        fp.AppendLine("%d\n", Nodes[i].FunctionIndex);

    //        for (var j = 0; j < Arity; j++)
    //        {
    //            fp.AppendLine("%d,%f\n", Nodes[i].Inputs[j], Nodes[i].Weights[j]);
    //        }
    //    }

    //    for (i = 0; i < Parameters.OutputSize; i++)
    //    {
    //        fp.AppendLine("%d,", OutputNodes[i]);
    //    }

    //    fclose(fp);
    //}
    
    //public void SaveChromosomeLatex(int weights, string fileName)
    //{
    //    FILE fp = fopen(fileName, "w");

    //    if (fp == null)
    //    {
    //        return;
    //    }

    //    /* document header */
    //    fp.AppendLine("\\documentclass{article}\n");
    //    fp.AppendLine("\\begin{document}\n");

    //    for (var output = 0; output < Parameters.OutputSize; output++)
    //    {

    //        fp.AppendLine("\\begin{equation}\n");

    //        /* function inputs */
    //        if (Parameters.InputSize == 0)
    //        {
    //            fp.AppendLine("f()=");
    //        }
    //        else
    //        {

    //            fp.AppendLine("f_%d(x_0", output);

    //            for (var i = 1; i < Parameters.InputSize; i++)
    //            {

    //                fp.AppendLine(",x_%d", i);
    //            }

    //            fp.AppendLine(")=");
    //        }

    //        SaveChromosomeLatexRecursive(OutputNodes[output], fp);

    //        fp.AppendLine("\n\\end{equation}");
    //    }


    //    /* document footer */
    //    fp.AppendLine("\n\\end{document}");

    //    fclose(fp);
    //}

    //public void SaveChromosomeLatexRecursive(int index, FILE fp)
    //{

    //    int i;

    //    if (index < Parameters.InputSize)
    //    {
    //        fp.AppendLine("x_%d", index);
    //        return;
    //    }

    //    /* add */
    //    if (strncmp(FuncSet.FunctionNames[Nodes[index - Parameters.InputSize].FunctionIndex], "add", CGpConstants.FunctionNameLength) == 0)
    //    {

    //        fp.AppendLine("\\left(");

    //        SaveChromosomeLatexRecursive(Nodes[index - Parameters.InputSize].Inputs[0], fp);

    //        for (i = 1; i < GetChromosomeNodeArity(index - Parameters.InputSize); i++)
    //        {

    //            fp.AppendLine(" + ");

    //            SaveChromosomeLatexRecursive(Nodes[index - Parameters.InputSize].Inputs[i], fp);
    //        }

    //        fp.AppendLine("\\right)");
    //    }


    //    /* sub */
    //    else if (strncmp(FuncSet.FunctionNames[Nodes[index - Parameters.InputSize].FunctionIndex], "sub", CGpConstants.FunctionNameLength) == 0)
    //    {

    //        fp.AppendLine("\\left(");

    //        SaveChromosomeLatexRecursive(Nodes[index - Parameters.InputSize].Inputs[0], fp);

    //        for (i = 1; i < GetChromosomeNodeArity(index - Parameters.InputSize); i++)
    //        {

    //            fp.AppendLine(" - ");

    //            SaveChromosomeLatexRecursive(Nodes[index - Parameters.InputSize].Inputs[i], fp);
    //        }

    //        fp.AppendLine("\\right)");
    //    }

    //    /* mul */
    //    else if (strncmp(FuncSet.FunctionNames[Nodes[index - Parameters.InputSize].FunctionIndex], "mul", CGpConstants.FunctionNameLength) == 0)
    //    {

    //        fp.AppendLine("\\left(");

    //        SaveChromosomeLatexRecursive(Nodes[index - Parameters.InputSize].Inputs[0], fp);

    //        for (i = 1; i < GetChromosomeNodeArity(index - Parameters.InputSize); i++)
    //        {

    //            fp.AppendLine(" \\times ");

    //            SaveChromosomeLatexRecursive(Nodes[index - Parameters.InputSize].Inputs[i], fp);
    //        }

    //        fp.AppendLine("\\right)");
    //    }

    //    /* div (change to frac)*/
    //    else if (strncmp(FuncSet.FunctionNames[Nodes[index - Parameters.InputSize].FunctionIndex], "div", CGpConstants.FunctionNameLength) == 0)
    //    {

    //        if (GetChromosomeNodeArity(index - Parameters.InputSize) == 1)
    //        {
    //            SaveChromosomeLatexRecursive(Nodes[index - Parameters.InputSize].Inputs[0], fp);
    //        }
    //        else
    //        {

    //            for (i = 0; i < GetChromosomeNodeArity(index - Parameters.InputSize); i++)
    //            {

    //                if (i + 1 < GetChromosomeNodeArity(index - Parameters.InputSize))
    //                {
    //                    fp.AppendLine("\\frac{");
    //                    SaveChromosomeLatexRecursive(Nodes[index - Parameters.InputSize].Inputs[i], fp);
    //                    fp.AppendLine("}{");
    //                }
    //                else if (i + 1 == GetChromosomeNodeArity(index - Parameters.InputSize) && GetChromosomeNodeArity(index - Parameters.InputSize) > 2)
    //                {
    //                    SaveChromosomeLatexRecursive(Nodes[index - Parameters.InputSize].Inputs[i], fp);
    //                    fp.AppendLine("}}");
    //                }
    //                else
    //                {
    //                    SaveChromosomeLatexRecursive(Nodes[index - Parameters.InputSize].Inputs[i], fp);
    //                    fp.AppendLine("}");
    //                }
    //            }
    //        }
    //    }

    //    /* abs */
    //    else if (FuncSet.FunctionNames[Nodes[index - Parameters.InputSize].FunctionIndex] == "abs")
    //    {

    //        fp.AppendLine(" \\left|");

    //        SaveChromosomeLatexRecursive(Nodes[index - Parameters.InputSize].Inputs[0], fp);

    //        fp.AppendLine(" \\right|");

    //    }

    //    /* sqrt */
    //    else if (FuncSet.FunctionNames[Nodes[index - Parameters.InputSize].FunctionIndex] == "sqrt")
    //    {

    //        fp.AppendLine(" \\sqrt{");

    //        SaveChromosomeLatexRecursive(Nodes[index - Parameters.InputSize].Inputs[0], fp);

    //        fp.AppendLine(" }");

    //    }


    //    /* sq */
    //    else if (FuncSet.FunctionNames[Nodes[index - Parameters.InputSize].FunctionIndex] == "sq")
    //    {

    //        fp.AppendLine(" (");

    //        SaveChromosomeLatexRecursive(Nodes[index - Parameters.InputSize].Inputs[0], fp);

    //        fp.AppendLine(" )^2");

    //    }

    //    /* cube */
    //    else if (FuncSet.FunctionNames[Nodes[index - Parameters.InputSize].FunctionIndex] == "cube")
    //    {

    //        fp.AppendLine(" (");

    //        SaveChromosomeLatexRecursive(Nodes[index - Parameters.InputSize].Inputs[0], fp);

    //        fp.AppendLine(" )^3");

    //    }

    //    /* exp */
    //    else if (FuncSet.FunctionNames[Nodes[index - Parameters.InputSize].FunctionIndex] == "exp")
    //    {

    //        fp.AppendLine(" e^{");

    //        SaveChromosomeLatexRecursive(Nodes[index - Parameters.InputSize].Inputs[0], fp);

    //        fp.AppendLine(" }");

    //    }

    //    /* sin */
    //    else if (FuncSet.FunctionNames[Nodes[index - Parameters.InputSize].FunctionIndex] == "sin")
    //    {

    //        fp.AppendLine("\\sin(");

    //        SaveChromosomeLatexRecursive(Nodes[index - Parameters.InputSize].Inputs[0], fp);

    //        fp.AppendLine(" )");

    //    }

    //    /* cos */
    //    else if (FuncSet.FunctionNames[Nodes[index - Parameters.InputSize].FunctionIndex] == "cos")
    //    {

    //        fp.AppendLine(" \\cos(");

    //        SaveChromosomeLatexRecursive(Nodes[index - Parameters.InputSize].Inputs[0], fp);

    //        fp.AppendLine(" )");

    //    }

    //    /* tan */
    //    else if (FuncSet.FunctionNames[Nodes[index - Parameters.InputSize].FunctionIndex] == "tan")
    //    {

    //        fp.AppendLine(" \\tan(");

    //        SaveChromosomeLatexRecursive(Nodes[index - Parameters.InputSize].Inputs[0], fp);

    //        fp.AppendLine(" )");

    //    }

    //    /* rand */
    //    else if (FuncSet.FunctionNames[Nodes[index - Parameters.InputSize].FunctionIndex] == "rand")
    //    {

    //        fp.AppendLine(" rand()");
    //    }

    //    /* pi */
    //    else if (FuncSet.FunctionNames[Nodes[index - Parameters.InputSize].FunctionIndex] == "pi")
    //    {

    //        fp.AppendLine("\\pi");
    //    }

    //    /* 0 */
    //    else if (FuncSet.FunctionNames[Nodes[index - Parameters.InputSize].FunctionIndex] == "0")
    //    {

    //        fp.AppendLine(" 0");
    //    }

    //    /* 1 */
    //    else if (FuncSet.FunctionNames[Nodes[index - Parameters.InputSize].FunctionIndex] == "1")
    //    {

    //        fp.AppendLine(" 1");
    //    }

    //    /* other */
    //    else
    //    {

    //        fp.AppendLine("%s(", FuncSet.FunctionNames[Nodes[index - Parameters.InputSize].FunctionIndex]);

    //        for (i = 0; i < GetChromosomeNodeArity(index - Parameters.InputSize); i++)
    //        {

    //            SaveChromosomeLatexRecursive(Nodes[index - Parameters.InputSize].Inputs[i], fp);

    //            if (i < GetChromosomeNodeArity(index - Parameters.InputSize) - 1)
    //            {
    //                fp.AppendLine(", ");
    //            }
    //        }

    //        fp.AppendLine(")");
    //    }

    //}

    public string GetGraphVizCode(bool activeOnly = true, bool showWeights = true)
    {
        var composer = new LinearTextComposer();

        composer.AppendLine("digraph NeuralNetwork {").IncreaseIndentation();

        /* landscape, square and centre */
        composer.AppendLine("rankdir=TD;");
        composer.AppendLine("size=\"4,3\";");
        composer.AppendLine("center = true;");

        /* for all the inputs */
        for (var i = 0; i < Parameters.InputSize; i++)
        {
            composer.AppendLine(
                $"node{i} [label=\"({i}) Input\", color=black, labelfontcolor=black, fontcolor=black];"
            );
        }

        /* for all nodes */
        for (var i = 0; i < NodeCount; i++)
        {
            var node = Nodes[i];

            if (activeOnly && !node.IsActive) continue;

            var colorText = node.IsActive ? "black" : "lightgrey";

            composer.AppendLine(
                $"node{i + Parameters.InputSize} [label=\"({i + Parameters.InputSize}) {Nodes[i].FunctionName}\", color={colorText}, labelfontcolor={colorText}, fontcolor={colorText}];"
            );

            /* for each node input */
            var nodeActualArity = Nodes[i].ActualArity;
            for (var j = 0; j < nodeActualArity; j++)
            {
                var weightText = 
                    showWeights 
                        ? node.Weights[j].Value.ToString("F3") 
                        : j.ToString();

                composer.AppendLine(
                    $"node{node.InputIndices[j]} -> node{i + Parameters.InputSize} [label=\"{weightText}\", labelfontcolor={colorText}, fontcolor={colorText}, bold=true, color={colorText}];"
                );
            }
        }

        for (var i = 0; i < Parameters.OutputSize; i++)
        {
            composer.AppendLine(
                $"node{i + Parameters.InputSize + NodeCount} [label=\"Output {i}\", color=black, labelfontcolor=black, fontcolor=black];"
            );

            composer.AppendLine(
                $"node{OutputNodeIndices[i]} -> node{i + Parameters.InputSize + NodeCount} [labelfontcolor=black, fontcolor=black, bold=true, color=black];");
        }


        /* place inputs  on same line */
        composer.AppendLine("{ rank = source;");

        for (var i = 0; i < Parameters.InputSize; i++)
        {
            composer.AppendLine($" \"node{i}\";");
        }
        composer.AppendLine(" }");


        /* place outputs  on same line */
        composer.AppendLine("{ rank = max;");

        for (var i = 0; i < Parameters.OutputSize; i++)
        {
            composer.AppendLine($"\"node{i + Parameters.InputSize + NodeCount}\";");
        }
        composer.AppendLine(" }");


        /* last line of dot file */
        composer.AppendLine("}");

        return composer.ToString();
    }

    public override string ToString()
    {
        return GetGraphVizCode();
    }
}