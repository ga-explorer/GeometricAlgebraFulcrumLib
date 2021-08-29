using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class FactorMethodFileComposer : 
        GaFuLLibrarySymbolicContextFileComposerBase
    {
        private readonly uint _inputGrade;
        private readonly ulong _inputId;
        private IGaKVectorStorage<ISymbolicExpressionAtomic> _inputBlade;
        private IGaVectorStorage<ISymbolicExpressionAtomic>[] _inputBasisVectorsArray;
        private IGaVectorStorage<ISymbolicExpressionAtomic>[] _outputVectorsArray;


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
                            .CreateBasisVector(index)
                    )
                    .Cast<IGaVectorStorage<ISymbolicExpressionAtomic>>()
                    .ToArray();
        }

        protected override void DefineContextComputations(SymbolicContext context)
        {
            var vectorsCount = 
                _inputBasisVectorsArray.Length;

            _outputVectorsArray = 
                new IGaVectorStorage<ISymbolicExpressionAtomic>[vectorsCount];

            var grade = _inputGrade;
            var inputBlade = _inputBlade;
            for (var index = 0; index < vectorsCount - 1; index++)
            {
                _outputVectorsArray[index] =
                    Processor.Lcp(
                        Processor.Lcp(
                            _inputBasisVectorsArray[index], 
                            inputBlade
                        ), 
                        inputBlade
                    ).GetVectorPart();

                grade--;

                inputBlade = Processor.Lcp(
                    _outputVectorsArray[index],
                    inputBlade
                ).GetKVectorPart(grade);
            }

            _outputVectorsArray[^1] = inputBlade.GetVectorPart();

            _outputVectorsArray.SetIsOutput(true);
        }

        protected override void DefineContextExternalNames(SymbolicContext context)
        {
            context.SetExternalNamesByTermIndex(
                _inputBlade,
                index => $"scalars[{index}]"
            );

            var vectorsCount = _outputVectorsArray.Length;
            for (var i = 0; i < vectorsCount; i++)
            {
                var j = i;

                context.SetExternalNamesByTermIndex(
                    _outputVectorsArray[i],
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
                "double", GaLanguage.ScalarTypeName,
                "newvectors", newVectorsText,
                "computations", computationsText
            );

            GenerateBladeFileFinishCode();

            FileComposer.FinalizeText();
        }

    }
}
