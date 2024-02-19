using System;
using System.Collections.Generic;
using System.Linq;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Languages;

public static class GaFuLLanguageOperationUtils
{
        
    public static string GetName(this GaFuLLanguageOperationKind operationKind, bool isEuclidean)
    {
        var euclideanPrefix = isEuclidean 
            ? "E" : string.Empty;

        return operationKind switch
        {
            GaFuLLanguageOperationKind.UnaryNegative => "Negative",
            GaFuLLanguageOperationKind.UnaryReverse => "Reverse",
            GaFuLLanguageOperationKind.UnaryGradeInvolution => "GradeInvolution",
            GaFuLLanguageOperationKind.UnaryCliffordConjugate => "CliffordConjugate",
            //GeoClcOperationKind.UnaryMagnitude => $"{euclideanPrefix}Magnitude",
            //GeoClcOperationKind.UnaryMagnitudeSquared => $"{euclideanPrefix}MagnitudeSquared",
            GaFuLLanguageOperationKind.UnaryNorm => $"{euclideanPrefix}Norm",
            GaFuLLanguageOperationKind.UnaryNormSquared => $"{euclideanPrefix}NormSquared",
            GaFuLLanguageOperationKind.UnaryNormalize => $"{euclideanPrefix}Normalize",
            GaFuLLanguageOperationKind.UnaryDual => $"{euclideanPrefix}Dual",
            GaFuLLanguageOperationKind.UnaryUnDual => $"{euclideanPrefix}UnDual",
            GaFuLLanguageOperationKind.UnaryGeometricProductSquared => $"{euclideanPrefix}GpSquared",
            GaFuLLanguageOperationKind.UnaryGeometricProductReverse => $"{euclideanPrefix}GpReverse",

            GaFuLLanguageOperationKind.BinaryOuterProduct => "Op",
            GaFuLLanguageOperationKind.BinaryGeometricProduct => $"{euclideanPrefix}Gp",
            GaFuLLanguageOperationKind.BinaryScalarProduct => $"{euclideanPrefix}Sp",
            GaFuLLanguageOperationKind.BinaryLeftContractionProduct => $"{euclideanPrefix}Lcp",
            GaFuLLanguageOperationKind.BinaryRightContractionProduct => $"{euclideanPrefix}Rcp",
            GaFuLLanguageOperationKind.BinaryFatDotProduct => $"{euclideanPrefix}Fdp",
            GaFuLLanguageOperationKind.BinaryHestenesInnerProduct => $"{euclideanPrefix}Hip",
            GaFuLLanguageOperationKind.BinaryCommutatorProduct => $"{euclideanPrefix}Cp",
            GaFuLLanguageOperationKind.BinaryAntiCommutatorProduct => $"{euclideanPrefix}Acp",
            GaFuLLanguageOperationKind.BinaryGeometricProductDual => $"{euclideanPrefix}GpDual",
            GaFuLLanguageOperationKind.BinaryDirectSandwichProduct => $"{euclideanPrefix}Dsp",
            GaFuLLanguageOperationKind.BinaryGradeInvolutionSandwichProduct => $"{euclideanPrefix}Gwp",
            GaFuLLanguageOperationKind.BinaryTimesWithScalar => "Times",
            GaFuLLanguageOperationKind.BinaryDivideByScalar => "Divide",
            GaFuLLanguageOperationKind.BinaryDeltaProduct => "Dp",
            GaFuLLanguageOperationKind.BinaryDeltaProductDual => "DpDual",
            GaFuLLanguageOperationKind.BinaryProject => $"{euclideanPrefix}Project",
            GaFuLLanguageOperationKind.BinaryReflect => $"{euclideanPrefix}Reflect",
            GaFuLLanguageOperationKind.BinaryComplement => $"{euclideanPrefix}Complement",
            GaFuLLanguageOperationKind.BinaryRotate => $"{euclideanPrefix}Rotate",

            _ => throw new InvalidOperationException()
        };
    }
        
    public static string GetName(this GaFuLLanguageOperationKind operationKind, bool isEuclidean, int grade)
    {
        return operationKind.GetName(isEuclidean) + 
               "_" + 
               grade.ToString("X1");
    }

    public static string GetName(this GaFuLLanguageOperationKind operationKind, bool isEuclidean, params int[] gradesList)
    {
        return operationKind.GetName(isEuclidean) + 
               "_" +
               gradesList
                   .Select(g => g.ToString("X1"))
                   .Concatenate("_");
    }

    public static string GetName(this GaFuLLanguageOperationKind operationKind, bool isEuclidean, IEnumerable<int> gradesList)
    {
        return operationKind.GetName(isEuclidean) + 
               "_" +
               gradesList
                   .Select(g => g.ToString("X1"))
                   .Concatenate("_");
    }

    public static Tuple<bool, int> GetKVectorsBilinearProductGrade(this GaFuLLanguageOperationKind operationKind, int vSpaceDimensions, int inGrade1, int inGrade2)
    {
        if (inGrade1 > vSpaceDimensions)
            return new Tuple<bool, int>(false, 0);

        if (inGrade2 > vSpaceDimensions)
            return new Tuple<bool, int>(false, 0);

        var outGrade = operationKind switch
        {
            GaFuLLanguageOperationKind.BinaryScalarProduct => 
                0,

            GaFuLLanguageOperationKind.BinaryOuterProduct => 
                inGrade1 + inGrade2,

            GaFuLLanguageOperationKind.BinaryLeftContractionProduct => 
                inGrade2 - inGrade1,

            GaFuLLanguageOperationKind.BinaryRightContractionProduct => 
                inGrade1 - inGrade2,

            GaFuLLanguageOperationKind.BinaryFatDotProduct => 
                Math.Abs(inGrade1 - inGrade2),
                
            GaFuLLanguageOperationKind.BinaryHestenesInnerProduct => 
                inGrade1 == 0 || inGrade2 == 0 ? 0 : Math.Abs(inGrade1 - inGrade2),

            _ => -1
        };

        if (outGrade < 0 || outGrade > vSpaceDimensions)
            return new Tuple<bool, int>(false, 0);

        return new Tuple<bool, int>(true, outGrade);
    }

    public static GaFuLLanguageOperationSpecs CreateEuclideanOperationSpecs(this GaFuLLanguageOperationKind operationKind)
    {
        return new GaFuLLanguageOperationSpecs(operationKind, true);
    }

    public static GaFuLLanguageOperationSpecs CreateMetricOperationSpecs(this GaFuLLanguageOperationKind operationKind)
    {
        return new GaFuLLanguageOperationSpecs(operationKind, false);
    }

    public static GaFuLLanguageOperationSpecs CreateOperationSpecs(this GaFuLLanguageOperationKind operationKind, bool isEuclidean)
    {
        return new GaFuLLanguageOperationSpecs(operationKind, isEuclidean);
    }


    ///// <summary>
    ///// Names of default signature macros for computing general linear transforms on multivectors
    ///// </summary>
    //public static class LinearTransform
    //{
    //    public const string Apply = "ApplyLT";

    //    public const string Transpose = "TransLT";

    //    public const string Add = "AddLT";

    //    public const string Subtract = "SubtractLT";

    //    public const string Compose = "ComposeLT";

    //    public const string TimesWithScalar = "TimesLT";

    //    public const string DivideByScalar = "DivideLT";
    //}

    ///// <summary>
    ///// Names of default signature macros for computing general outer-morphisms on multivectors
    ///// </summary>
    //public static class Outermorphism
    //{
    //    public const string Apply = "ApplyOM";

    //    public const string ApplyToVector = "AVOM";

    //    public const string Transpose = "TransOM";

    //    public const string MetricDeterminant = "DetOM";

    //    public const string EuclideanDeterminant = "EDetOM";

    //    public const string ToLinearTransform = "OMToLT";

    //    public const string Add = "AddOM";

    //    public const string Subtract = "SubtractOM";

    //    public const string Compose = "ComposeOM";

    //    public const string TimesWithScalar = "TimesOM";

    //    public const string DivideByScalar = "DivideOM";
    //}

    //public static class EuclideanVersor
    //{
    //    public const string Apply = "ApplyEVersor";

    //    public const string ApplyRotor = "ApplyERotor";

    //    public const string ApplyReflector = "ApplyEReflector";

    //    public const string Inverse = "InvEVersor";

    //    public const string ToLinearTransform = "EVersorToLT";

    //    public const string ToOutermorphism = "EVersorToOM";
    //}

    //public static class MetricVersor
    //{
    //    public const string Apply = "ApplyVersor";

    //    public const string ApplyRotor = "ApplyRotor";

    //    public const string ApplyReflector = "ApplyReflector";

    //    public const string Inverse = "InvVersor";

    //    public const string ToLinearTransform = "VersorToLT";

    //    public const string ToOutermorphism = "VersorToOM";
    //}
}