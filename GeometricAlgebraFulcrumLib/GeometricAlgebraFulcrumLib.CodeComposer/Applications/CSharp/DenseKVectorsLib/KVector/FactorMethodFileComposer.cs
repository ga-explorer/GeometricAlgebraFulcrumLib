using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Storage;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class FactorMethodFileComposer : 
        GaLibrarySymbolicContextFileComposerBase
    {
        private IGaKVectorStorage<ISymbolicExpressionAtomic> _inputBlade;
        private IGaVectorTermStorage<ISymbolicExpressionAtomic>[] _inputBasisVectorsArray;
        private IGaVectorStorage<ISymbolicExpressionAtomic>[] _outputVectorsArray;

        internal int InputGrade { get; }

        internal ulong InputId { get; }


        internal FactorMethodFileComposer(GaLibraryComposer libGen, int inGrade, ulong inId)
            : base(libGen)
        {
            InputGrade = inGrade;
            InputId = inId;
        }

        
        protected override void DefineContextParameters(SymbolicContext context)
        {
            _inputBlade = 
                context
                    .ParameterVariablesFactory
                    .CreateDenseKVector(
                        VSpaceDimension,
                        InputGrade,
                        index => $"bladeScalar{index}"
                    );

            _inputBasisVectorsArray = 
                InputId
                    .BasisVectorIndexesInside()
                    .Select(index => 
                        context
                            .NumbersFactory
                            .CreateBasisVector(index)
                    )
                    .Cast<IGaVectorTermStorage<ISymbolicExpressionAtomic>>()
                    .ToArray();
        }

        protected override void DefineContextComputations(SymbolicContext context)
        {
            var vectorsCount = 
                _inputBasisVectorsArray.Length;

            _outputVectorsArray = 
                new IGaVectorStorage<ISymbolicExpressionAtomic>[vectorsCount];

            var grade = InputGrade;
            var inputBlade = _inputBlade;
            for (var index = 0; index < vectorsCount - 1; index++)
            {
                _outputVectorsArray[index] =
                    MultivectorProcessor.Lcp(
                        MultivectorProcessor.Lcp(
                            _inputBasisVectorsArray[index], 
                            inputBlade
                        ), 
                        inputBlade
                    ).GetVectorPart();

                grade--;

                inputBlade = MultivectorProcessor.Lcp(
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

            for (var i = 0; i < InputGrade; i++)
                newVectorsText.Add("new " + CurrentNamespace + "Vector()");

            TextComposer.AppendAtNewLine(
                Templates["factor"],
                "frame", CurrentNamespace,
                "id", InputId,
                "double", GaClcLanguage.ScalarTypeName,
                "newvectors", newVectorsText,
                "computations", computationsText
            );

            GenerateBladeFileFinishCode();

            FileComposer.FinalizeText();
        }

    }
}
