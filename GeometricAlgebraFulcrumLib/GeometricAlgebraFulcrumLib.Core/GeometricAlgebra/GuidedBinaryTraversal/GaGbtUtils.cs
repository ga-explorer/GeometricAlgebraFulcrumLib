using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Structures;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.GuidedBinaryTraversal;

public static class GaGbtUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GaGbtMultivectorBinaryTrieStack CreateGbtMultivectorStack(this RGaFloat64Processor basisSet, int capacity, GaMultivectorBinaryTrie binaryTrie)
    {
        return new GaGbtMultivectorBinaryTrieStack(basisSet, capacity, binaryTrie);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GaGbtMultivectorProductsBinaryTrieStack CreateGbtProductsStack(this RGaFloat64Processor basisSet, int treeDepth, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
    {
        //var treeDepth = (int) basisSet.VSpaceDimensions;

        var capacity = (treeDepth + 1) * (treeDepth + 1);
            
        var stack1 = basisSet.CreateGbtMultivectorStack(capacity, mvBinaryTrie1);
        var stack2 = basisSet.CreateGbtMultivectorStack(capacity, mvBinaryTrie2);

        return new GaGbtMultivectorProductsBinaryTrieStack(stack1, stack2);
    }
}