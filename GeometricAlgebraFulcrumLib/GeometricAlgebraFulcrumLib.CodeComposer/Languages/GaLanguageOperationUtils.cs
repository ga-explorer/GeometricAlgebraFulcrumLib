using System;
using System.Collections.Generic;
using System.Linq;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Languages
{
    public static class GaLanguageOperationUtils
    {
        
        public static string GetName(this GaLanguageOperationKind operationKind, bool isEuclidean)
        {
            var euclideanPrefix = isEuclidean 
                ? "E" : string.Empty;

            return operationKind switch
            {
                GaLanguageOperationKind.UnaryNegative => "Negative",
                GaLanguageOperationKind.UnaryReverse => "Reverse",
                GaLanguageOperationKind.UnaryGradeInvolution => "GradeInvolution",
                GaLanguageOperationKind.UnaryCliffordConjugate => "CliffordConjugate",
                //GaClcOperationKind.UnaryMagnitude => $"{euclideanPrefix}Magnitude",
                //GaClcOperationKind.UnaryMagnitudeSquared => $"{euclideanPrefix}MagnitudeSquared",
                GaLanguageOperationKind.UnaryNorm => $"{euclideanPrefix}Norm",
                GaLanguageOperationKind.UnaryNormSquared => $"{euclideanPrefix}NormSquared",
                GaLanguageOperationKind.UnaryNormalize => $"{euclideanPrefix}Normalize",
                GaLanguageOperationKind.UnaryDual => $"{euclideanPrefix}Dual",
                GaLanguageOperationKind.UnaryUnDual => $"{euclideanPrefix}UnDual",
                GaLanguageOperationKind.UnaryGeometricProductSquared => $"{euclideanPrefix}GpSquared",
                GaLanguageOperationKind.UnaryGeometricProductReverse => $"{euclideanPrefix}GpReverse",

                GaLanguageOperationKind.BinaryOuterProduct => "Op",
                GaLanguageOperationKind.BinaryGeometricProduct => $"{euclideanPrefix}Gp",
                GaLanguageOperationKind.BinaryScalarProduct => $"{euclideanPrefix}Sp",
                GaLanguageOperationKind.BinaryLeftContractionProduct => $"{euclideanPrefix}Lcp",
                GaLanguageOperationKind.BinaryRightContractionProduct => $"{euclideanPrefix}Rcp",
                GaLanguageOperationKind.BinaryFatDotProduct => $"{euclideanPrefix}Fdp",
                GaLanguageOperationKind.BinaryHestenesInnerProduct => $"{euclideanPrefix}Hip",
                GaLanguageOperationKind.BinaryCommutatorProduct => $"{euclideanPrefix}Cp",
                GaLanguageOperationKind.BinaryAntiCommutatorProduct => $"{euclideanPrefix}Acp",
                GaLanguageOperationKind.BinaryGeometricProductDual => $"{euclideanPrefix}GpDual",
                GaLanguageOperationKind.BinaryDirectSandwichProduct => $"{euclideanPrefix}Dsp",
                GaLanguageOperationKind.BinaryGradeInvolutionSandwichProduct => $"{euclideanPrefix}Gwp",
                GaLanguageOperationKind.BinaryTimesWithScalar => "Times",
                GaLanguageOperationKind.BinaryDivideByScalar => "Divide",
                GaLanguageOperationKind.BinaryDeltaProduct => "Dp",
                GaLanguageOperationKind.BinaryDeltaProductDual => "DpDual",
                GaLanguageOperationKind.BinaryProject => $"{euclideanPrefix}Project",
                GaLanguageOperationKind.BinaryReflect => $"{euclideanPrefix}Reflect",
                GaLanguageOperationKind.BinaryComplement => $"{euclideanPrefix}Complement",
                GaLanguageOperationKind.BinaryRotate => $"{euclideanPrefix}Rotate",

                _ => throw new InvalidOperationException()
            };
        }
        
        public static string GetName(this GaLanguageOperationKind operationKind, bool isEuclidean, uint grade)
        {
            return operationKind.GetName(isEuclidean) + 
                   "_" + 
                   grade.ToString("X1");
        }

        public static string GetName(this GaLanguageOperationKind operationKind, bool isEuclidean, params uint[] gradesList)
        {
            return operationKind.GetName(isEuclidean) + 
                   "_" +
                   gradesList
                       .Select(g => g.ToString("X1"))
                       .Concatenate("_");
        }

        public static string GetName(this GaLanguageOperationKind operationKind, bool isEuclidean, IEnumerable<uint> gradesList)
        {
            return operationKind.GetName(isEuclidean) + 
                   "_" +
                   gradesList
                       .Select(g => g.ToString("X1"))
                       .Concatenate("_");
        }

        public static Tuple<bool, uint> GetKVectorsBilinearProductGrade(this GaLanguageOperationKind operationKind, uint vSpaceDimension, uint inGrade1, uint inGrade2)
        {
            if (inGrade1 > vSpaceDimension)
                return new Tuple<bool, uint>(false, 0);

            if (inGrade2 > vSpaceDimension)
                return new Tuple<bool, uint>(false, 0);

            var outGrade = operationKind switch
            {
                GaLanguageOperationKind.BinaryScalarProduct => 
                    0,

                GaLanguageOperationKind.BinaryOuterProduct => 
                    inGrade1 + inGrade2,

                GaLanguageOperationKind.BinaryLeftContractionProduct => 
                    inGrade2 - inGrade1,

                GaLanguageOperationKind.BinaryRightContractionProduct => 
                    inGrade1 - inGrade2,

                GaLanguageOperationKind.BinaryFatDotProduct => 
                    Math.Abs(inGrade1 - inGrade2),
                
                GaLanguageOperationKind.BinaryHestenesInnerProduct => 
                    inGrade1 == 0 || inGrade2 == 0 ? 0 : Math.Abs(inGrade1 - inGrade2),

                _ => -1
            };

            if (outGrade < 0 || outGrade > vSpaceDimension)
                return new Tuple<bool, uint>(false, 0);

            return new Tuple<bool, uint>(true, (uint) outGrade);
        }

        public static GaLanguageOperationSpecs CreateEuclideanOperationSpecs(this GaLanguageOperationKind operationKind)
        {
            return new GaLanguageOperationSpecs(operationKind, true);
        }

        public static GaLanguageOperationSpecs CreateMetricOperationSpecs(this GaLanguageOperationKind operationKind)
        {
            return new GaLanguageOperationSpecs(operationKind, false);
        }

        public static GaLanguageOperationSpecs CreateOperationSpecs(this GaLanguageOperationKind operationKind, bool isEuclidean)
        {
            return new GaLanguageOperationSpecs(operationKind, isEuclidean);
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
}