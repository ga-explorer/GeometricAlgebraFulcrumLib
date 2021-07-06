using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.CodeComposer
{
    public readonly struct GaClcOperationSpecs
    {
        public GaClcOperationKind OperationKind { get; }

        public bool IsEuclidean { get; }


        internal GaClcOperationSpecs(GaClcOperationKind operationKind, bool isEuclidean)
        {
            OperationKind = operationKind;
            IsEuclidean = isEuclidean;
        }


        public int GetKVectorsBilinearProductGrade(int vSpaceDimension, int inGrade1, int inGrade2)
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

        public string GetName(int grade)
        {
            return OperationKind.GetName(IsEuclidean, grade);
        }

        public string GetName(params int[] gradesList)
        {
            return OperationKind.GetName(IsEuclidean, gradesList);
        }

        public string GetName(IEnumerable<int> gradesList)
        {
            return OperationKind.GetName(IsEuclidean, gradesList);
        }

        public override string ToString()
        {
            return GetName();
        }
    }
}