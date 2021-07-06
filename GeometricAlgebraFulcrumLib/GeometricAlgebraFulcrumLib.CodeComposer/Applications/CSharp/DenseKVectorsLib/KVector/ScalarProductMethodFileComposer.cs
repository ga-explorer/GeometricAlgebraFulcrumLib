using System;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Variables;
using GeometricAlgebraFulcrumLib.Storage;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class ScalarProductMethodFileComposer : 
        GaLibrarySymbolicContextFileComposerBase
    {
        private IGaKVectorStorage<ISymbolicExpressionAtomic> _inputKVector1;
        private IGaKVectorStorage<ISymbolicExpressionAtomic> _inputKVector2;
        private SymbolicVariableComputed _outputScalar;


        internal GaClcOperationSpecs OperationSpecs { get; }

        internal int InputGrade { get; }

        internal int OutputGrade => 0;


        internal ScalarProductMethodFileComposer(GaLibraryComposer libGen, GaClcOperationSpecs opSpecs, int inGrade)
            : base(libGen)
        {
            OperationSpecs = opSpecs;
            InputGrade = inGrade;
        }


        protected override void DefineContextParameters(SymbolicContext context)
        {
            _inputKVector1 = context.ParameterVariablesFactory.CreateDenseKVector(
                VSpaceDimension,
                InputGrade,
                index => $"kVector1Scalar{index}"
            );

            _inputKVector2 = context.ParameterVariablesFactory.CreateDenseKVector(
                VSpaceDimension,
                InputGrade,
                index => $"kVector2Scalar{index}"
            );
        }

        protected override void DefineContextComputations(SymbolicContext context)
        {
            var outputScalar = OperationSpecs.OperationKind switch
            {
                GaClcOperationKind.BinaryScalarProduct =>
                    OperationSpecs.IsEuclidean
                        ? _inputKVector1.ESp(_inputKVector2)
                        : MultivectorProcessor.Sp(_inputKVector1, _inputKVector2),

                _ => throw new InvalidOperationException()
            };

            _outputScalar = (SymbolicVariableComputed) outputScalar;

            _outputScalar.IsOutputVariable = true;
        }

        protected override void DefineContextExternalNames(SymbolicContext context)
        {
            context.SetExternalNamesByTermIndex(
                _inputKVector1,
                index => $"mv1[{index}]"
            );

            context.SetExternalNamesByTermIndex(
                _inputKVector2,
                index => $"mv2[{index}]"
            );

            _outputScalar.ExternalName = "c";
            
            context.SetIntermediateExternalNamesByNameIndex(
                DenseKVectorsLibraryComposer.MaxTargetLocalVars,
                index => $"tempVar{index:X4}",
                index => $"tempArray[{index}]"
            );
        }
        
        public override void Generate()
        {
            GenerateBladeFileStartCode();

            var computationsText = 
                GenerateCode();

            var kvSpaceDimension = 
                MultivectorProcessor.BasisSet.KvSpaceDimension(OutputGrade);

            var methodName =
                OperationSpecs.GetName(InputGrade, InputGrade);

            TextComposer.AppendAtNewLine(
                Templates["bilinearproduct"],
                "name", methodName,
                "num", kvSpaceDimension,
                "double", GaClcLanguage.ScalarTypeName,
                "computations", computationsText
            );

            GenerateBladeFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}