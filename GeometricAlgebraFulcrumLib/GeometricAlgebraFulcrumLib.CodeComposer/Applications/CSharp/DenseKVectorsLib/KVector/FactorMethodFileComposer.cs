using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class FactorMethodFileComposer : 
        GaFuLLibrarySymbolicContextFileComposerBase
    {
        private readonly uint _inputGrade;
        private readonly ulong _inputId;
        private KVector<ISymbolicExpressionAtomic> _inputBlade;
        private Vector<ISymbolicExpressionAtomic>[] _inputBasisVectorsArray;
        private Vector<ISymbolicExpressionAtomic>[] _outputVectorsArray;


        internal FactorMethodFileComposer(GaFuLLibraryComposer libGen, uint inGrade, ulong inId)
            : base(libGen)
        {
            _inputGrade = inGrade;
            _inputId = inId;
        }

        
        protected override void DefineContextParameters(SymbolicContext context)
        {
            _inputBlade = 
                context
                    .ParameterVariablesFactory
                    .CreateDenseKVector(
                        VSpaceDimension,
                        _inputGrade,
                        index => $"bladeScalar{index}"
                    );

            _inputBasisVectorsArray = 
                _inputId
                    .BasisBladeIdToBasisVectorIndices()
                    .Select(index => 
                        context
                            .NumbersFactory
                            .CreateBasisVector(index).CreateVector(GeometricProcessor)
                    ).ToArray();
        }

        protected override void DefineContextComputations(SymbolicContext context)
        {
            var vectorsCount = 
                _inputBasisVectorsArray.Length;

            _outputVectorsArray = 
                new Vector<ISymbolicExpressionAtomic>[vectorsCount];

            var inputBlade = _inputBlade;
            for (var index = 0; index < vectorsCount - 1; index++)
            {
                _outputVectorsArray[index] =
                    _inputBasisVectorsArray[index].Lcp(inputBlade).Lcp(inputBlade).AsVector();

                inputBlade = _outputVectorsArray[index].Lcp(inputBlade);
            }

            _outputVectorsArray[^1] = inputBlade.AsVector();

            _outputVectorsArray.SetIsOutput(true);
        }

        protected override void DefineContextExternalNames(SymbolicContext context)
        {
            context.SetExternalNamesByTermIndex(
                _inputBlade.KVectorStorage,
                index => $"scalars[{index}]"
            );

            var vectorsCount = _outputVectorsArray.Length;
            for (var i = 0; i < vectorsCount; i++)
            {
                var j = i;

                context.SetExternalNamesByTermIndex(
                    _outputVectorsArray[i].VectorStorage,
                    index => $"vectors[{j}].C{index}"
                );
            }
            
            context.SetIntermediateExternalNamesByNameIndex(
                DenseKVectorsLibraryComposer.MaxTargetLocalVars,
                index => $"tempVar{index:X4}",
                index => $"tempArray[{index}]"
            );
        }

        public override void Generate()
        {
            GenerateBladeFileStartCode();

            var computationsText = GenerateCode();

            var newVectorsText = new ListTextComposer("," + Environment.NewLine);

            for (var i = 0; i < _inputGrade; i++)
                newVectorsText.Add("new " + CurrentNamespace + "Vector()");

            TextComposer.AppendAtNewLine(
                Templates["factor"],
                "signature", CurrentNamespace,
                "id", _inputId,
                "double", GeoLanguage.ScalarTypeName,
                "newvectors", newVectorsText,
                "computations", computationsText
            );

            GenerateBladeFileFinishCode();

            FileComposer.FinalizeText();
        }

    }
}
