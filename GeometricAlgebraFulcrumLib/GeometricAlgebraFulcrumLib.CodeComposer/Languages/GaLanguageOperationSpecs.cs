using System;
using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Languages
{
    public readonly struct GaLanguageOperationSpecs
    {
        public GaLanguageOperationKind OperationKind { get; }

        public bool IsEuclidean { get; }


        internal GaLanguageOperationSpecs(GaLanguageOperationKind operationKind, bool isEuclidean)
        {
            OperationKind = operationKind;
            IsEuclidean = isEuclidean;
        }


        public Tuple<bool, uint> GetKVectorsBilinearProductGrade(uint vSpaceDimension, uint inGrade1, uint inGrade2)
        {
            return OperationKind.GetKVectorsBilinearProductGrade(
                vSpaceDimension,
                inGrade1,
                inGrade2
            );
        }

        public string GetName()
        {
            return OperationKind.GetName(IsEuclidean);
        }

        public string GetName(uint grade)
        {
            return OperationKind.GetName(IsEuclidean, grade);
        }

        public string GetName(params uint[] gradesList)
        {
            return OperationKind.GetName(IsEuclidean, gradesList);
        }

        public string GetName(IEnumerable<uint> gradesList)
        {
            return OperationKind.GetName(IsEuclidean, gradesList);
        }

        public override string ToString()
        {
            return GetName();
        }
    }
}