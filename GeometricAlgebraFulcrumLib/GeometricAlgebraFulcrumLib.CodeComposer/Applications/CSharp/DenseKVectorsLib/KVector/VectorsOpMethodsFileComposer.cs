using System;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    /// <summary>
    /// This class generates a single macro into a code file using several related bindings and target variable
    /// namings.
    /// </summary>
    internal sealed class VectorsOpMethodsFileComposer : 
        GaFuLLibrarySymbolicContextFileComposerBase
    {
        private uint _outGrade;
        private Vector<ISymbolicExpressionAtomic>[] _inputVectorsArray;
        private KVector<ISymbolicExpressionAtomic> _outputKVector;


        internal VectorsOpMethodsFileComposer(GaFuLLibraryComposer libGen)
            : base(libGen)
        {
        }


        protected override void DefineContextParameters(SymbolicContext context)
        {
            _inputVectorsArray = 
                new Vector<ISymbolicExpressionAtomic>[_outGrade];

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
            _outputKVector = _inputVectorsArray.Op();

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
            //Each time this protected method is called the internal GeoClcSymbolicContextCodeComposer is initialized,
            //the bindings and target names are set, and the macro code is generated automatically.
            var computationsText = GenerateCode();

            TextComposer.Append(
                Templates["op_vectors"],
                "signature", CurrentNamespace,
                "double", GeoLanguage.ScalarTypeName,
                "grade", _outGrade,
                "num", this.KVectorSpaceDimension(_outGrade),
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
