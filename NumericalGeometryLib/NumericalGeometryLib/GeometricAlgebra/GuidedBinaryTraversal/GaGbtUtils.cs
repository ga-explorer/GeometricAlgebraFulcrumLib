using System.Runtime.CompilerServices;
using NumericalGeometryLib.GeometricAlgebra.Basis;
using NumericalGeometryLib.GeometricAlgebra.Structures;

namespace NumericalGeometryLib.GeometricAlgebra.GuidedBinaryTraversal
{
    public static class GaGbtUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGbtMultivectorBinaryTrieStack CreateGbtMultivectorStack(this GaBasisSet basisSet, int capacity, GaMultivectorBinaryTrie binaryTrie)
        {
            return new GaGbtMultivectorBinaryTrieStack(basisSet, capacity, binaryTrie);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGbtMultivectorProductsBinaryTrieStack CreateGbtProductsStack(this GaBasisSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            var treeDepth = (int) basisSet.VSpaceDimension;

            var capacity = (treeDepth + 1) * (treeDepth + 1);
            
            var stack1 = basisSet.CreateGbtMultivectorStack(capacity, mvBinaryTrie1);
            var stack2 = basisSet.CreateGbtMultivectorStack(capacity, mvBinaryTrie2);

            return new GaGbtMultivectorProductsBinaryTrieStack(stack1, stack2);
        }
    }
}
