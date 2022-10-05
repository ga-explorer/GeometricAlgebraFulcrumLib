using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class BinaryGpFilesComposer : 
        GaFuLLibraryPartComposerBase
    {
        private readonly GaFuLLanguageOperationSpecs _operationSpecs;
        private readonly bool _dualFlag;
        private GpMethodsFileComposer _mainFileComposer;


        internal BinaryGpFilesComposer(GaFuLLibraryComposer libGen, GaFuLLanguageOperationSpecs opSpecs, bool dualFlag)
            : base(libGen)
        {
            _operationSpecs = opSpecs;
            _dualFlag = dualFlag;
        }


        private void GenerateMethods(uint inGrade1, uint inGrade2)
        {
            var gpCaseText = new ListTextComposer("," + Environment.NewLine);

            var gradesList = 
                _dualFlag
                    ? this
                        .GradesOfEGp(inGrade1, inGrade2)
                        .Select(grade => VSpaceDimension - grade)
                    : this.GradesOfEGp(inGrade1, inGrade2);

            foreach (var outGrade in gradesList)
            {
                DenseKVectorsLibraryComposer.GenerateBilinearProductMethodFile(
                    _operationSpecs,
                    inGrade1,
                    inGrade2,
                    outGrade
                );

                var funcName = _operationSpecs.GetName(
                    inGrade1, inGrade2, outGrade
                );

                gpCaseText.Add(Templates["gp_case"],
                    "signature", CurrentNamespace,
                    "grade", outGrade,
                    "name", funcName
                );
            }

            var name = _operationSpecs.GetName(
                inGrade1, inGrade2
            );

            _mainFileComposer.GenerateIntermediateMethod(gpCaseText.ToString(), name);
        }

        internal void Generate()
        {
            DenseKVectorsLibraryComposer.CodeFilesComposer.InitalizeFile(
                _operationSpecs.GetName() + ".cs"
            );

            _mainFileComposer = new GpMethodsFileComposer(
                DenseKVectorsLibraryComposer, 
                _operationSpecs
            );

            _mainFileComposer.GenerateKVectorFileStartCode();

            DenseKVectorsLibraryComposer.CodeFilesComposer.UnselectActiveFile();

            foreach (var grade1 in GeometricProcessor.Grades)
                foreach (var grade2 in GeometricProcessor.Grades)
                    GenerateMethods(grade1, grade2);

            _mainFileComposer.GenerateMainMethod();

            _mainFileComposer.GenerateKVectorFileFinishCode();

            DenseKVectorsLibraryComposer.CodeFilesComposer.UnselectActiveFile();
        }
    }
}
