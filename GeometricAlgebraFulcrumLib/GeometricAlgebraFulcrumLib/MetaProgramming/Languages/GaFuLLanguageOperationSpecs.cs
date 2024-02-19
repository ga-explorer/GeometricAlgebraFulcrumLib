using System;
using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Languages;

public sealed record GaFuLLanguageOperationSpecs
{
    public GaFuLLanguageOperationKind OperationKind { get; }

    public bool IsEuclidean { get; }


    internal GaFuLLanguageOperationSpecs(GaFuLLanguageOperationKind operationKind, bool isEuclidean)
    {
        OperationKind = operationKind;
        IsEuclidean = isEuclidean;
    }


    public Tuple<bool, int> GetKVectorsBilinearProductGrade(int vSpaceDimensions, int inGrade1, int inGrade2)
    {
        return OperationKind.GetKVectorsBilinearProductGrade(
            vSpaceDimensions,
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