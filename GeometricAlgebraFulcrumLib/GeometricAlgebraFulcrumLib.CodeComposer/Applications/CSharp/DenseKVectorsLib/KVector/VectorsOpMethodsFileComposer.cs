using System;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    /// <summary>
    /// This class generates a single macro into a code file using several related bindings and target variable
    /// namings.
    /// </summary>
    internal sealed class VectorsOpMethodsFileComposer : 
        GaLibrarySymbolicContextFileComposerBase
    {
        private uint _outGrade;
        private IGaStorageVector<ISymbolicExpressionAtomic>[] _inputVectorsArray;
        private IGaStorageKVector<ISymbolicExpressionAtomic> _outputKVector;


        internal VectorsOpMethodsFileComposer(GaLibraryComposer libGen)
            : base(libGen)
        {
        }


        protected override void DefineContextParameters(SymbolicContext context)
        {
            _inputVectorsArray = 
                new IGaStorageVector<ISymbolicExpressionAtomic>[_outGrade];

            for (var g = 0; g < _outGrade; g++)
            {
                var grade = g;

                _inputVectorsArray[grade] =
                    context.ParameterVariablesFactory.CreateDenseVector(
                        VSpaceDimension,
                    index => $"vector{grade}Scalar{index}"
                );
            }
        }

        protected override void DefineContextComputations(SymbolicContext context)
        {
            _outputKVector = Processor.Op(_inputVectorsArray);

            _outputKVector.SetIsOutput(true);
        }

        protected override void DefineContextExternalNames(SymbolicContext context)
        {
            for (var g = 0; g < _outGrade; g++)
            {
                var grade = g;

                _inputVectorsArray[grade].SetExternalNamesByTermIndex(
                    index => $"vectors[{grade}].C{index}"
                );
            }

            _outputKVector.SetExternalNamesByTermIndex(
                index => $"scalars[{index}]"
            );

            context.SetIntermediateExternalNamesByNameIndex(
                DenseKVectorsLibraryComposer.MaxTargetLocalVars,
                index => $"tempVar{index:X4}",
                index => $"tempArray[{index}]"
            );
        }

        private void GenerateVectorsOpFunction()
        {
            //Each time this protected method is called the internal GaClcSymbolicContextCodeComposer is initialized,
            //the bindings and target names are set, and the macro code is generated automatically.
            var computationsText = GenerateCode();

            TextComposer.Append(
                Templates["op_vectors"],
                "signature", CurrentNamespace,
                "double", GaLanguage.ScalarTypeName,
                "grade", _outGrade,
                "num", this.KvSpaceDimension(_outGrade),
                "computations", computationsText
            );
        }

        public override void Generate()
        {
            GenerateBladeFileStartCode();

            var casesText = new ListTextComposer(Environment.NewLine);

            for (var grade = 2U; grade <= VSpaceDimension; grade++)
            {
                _outGrade = grade;

                GenerateVectorsOpFunction();

                casesText.Add(
                    Templates["op_vectors_main_case"].GenerateUsing(grade)
                );
            }

            TextComposer.Append(
                Templates["op_vectors_main"],
                "signature", CurrentNamespace,
                "op_vectors_main_case", casesText
            );

            GenerateBladeFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
