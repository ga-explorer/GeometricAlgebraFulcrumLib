﻿using System;
using System.Collections.Generic;
using System.Linq;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.CodeComposer
{
    public static class GaClcOperationUtils
    {
        
        public static string GetName(this GaClcOperationKind operationKind, bool isEuclidean)
        {
            var euclideanPrefix = isEuclidean 
                ? "E" : string.Empty;

            return operationKind switch
            {
                GaClcOperationKind.UnaryNegative => "Negative",
                GaClcOperationKind.UnaryReverse => "Reverse",
                GaClcOperationKind.UnaryGradeInvolution => "GradeInvolution",
                GaClcOperationKind.UnaryCliffordConjugate => "CliffordConjugate",
                //GaClcOperationKind.UnaryMagnitude => $"{euclideanPrefix}Magnitude",
                //GaClcOperationKind.UnaryMagnitudeSquared => $"{euclideanPrefix}MagnitudeSquared",
                GaClcOperationKind.UnaryNorm => $"{euclideanPrefix}Norm",
                GaClcOperationKind.UnaryNormSquared => $"{euclideanPrefix}NormSquared",
                GaClcOperationKind.UnaryNormalize => $"{euclideanPrefix}Normalize",
                GaClcOperationKind.UnaryDual => $"{euclideanPrefix}Dual",
                GaClcOperationKind.UnaryUnDual => $"{euclideanPrefix}UnDual",
                GaClcOperationKind.UnaryGeometricProductSquared => $"{euclideanPrefix}GpSquared",
                GaClcOperationKind.UnaryGeometricProductReverse => $"{euclideanPrefix}GpReverse",

                GaClcOperationKind.BinaryOuterProduct => "Op",
                GaClcOperationKind.BinaryGeometricProduct => $"{euclideanPrefix}Gp",
                GaClcOperationKind.BinaryScalarProduct => $"{euclideanPrefix}Sp",
                GaClcOperationKind.BinaryLeftContractionProduct => $"{euclideanPrefix}Lcp",
                GaClcOperationKind.BinaryRightContractionProduct => $"{euclideanPrefix}Rcp",
                GaClcOperationKind.BinaryFatDotProduct => $"{euclideanPrefix}Fdp",
                GaClcOperationKind.BinaryHestenesInnerProduct => $"{euclideanPrefix}Hip",
                GaClcOperationKind.BinaryCommutatorProduct => $"{euclideanPrefix}Cp",
                GaClcOperationKind.BinaryAntiCommutatorProduct => $"{euclideanPrefix}Acp",
                GaClcOperationKind.BinaryGeometricProductDual => $"{euclideanPrefix}GpDual",
                GaClcOperationKind.BinaryDirectSandwichProduct => $"{euclideanPrefix}Dsp",
                GaClcOperationKind.BinaryGradeInvolutionSandwichProduct => $"{euclideanPrefix}Gwp",
                GaClcOperationKind.BinaryTimesWithScalar => "Times",
                GaClcOperationKind.BinaryDivideByScalar => "Divide",
                GaClcOperationKind.BinaryDeltaProduct => "Dp",
                GaClcOperationKind.BinaryDeltaProductDual => "DpDual",
                GaClcOperationKind.BinaryProject => $"{euclideanPrefix}Project",
                GaClcOperationKind.BinaryReflect => $"{euclideanPrefix}Reflect",
                GaClcOperationKind.BinaryComplement => $"{euclideanPrefix}Complement",
                GaClcOperationKind.BinaryRotate => $"{euclideanPrefix}Rotate",

                _ => throw new InvalidOperationException()
            };
        }
        
        public static string GetName(this GaClcOperationKind operationKind, bool isEuclidean, uint grade)
        {
            return operationKind.GetName(isEuclidean) + 
                   "_" + 
                   grade.ToString("X1");
        }

        public static string GetName(this GaClcOperationKind operationKind, bool isEuclidean, params uint[] gradesList)
        {
            return operationKind.GetName(isEuclidean) + 
                   "_" +
                   gradesList
                       .Select(g => g.ToString("X1"))
                       .Concatenate("_");
        }

        public static string GetName(this GaClcOperationKind operationKind, bool isEuclidean, IEnumerable<uint> gradesList)
        {
            return operationKind.GetName(isEuclidean) + 
                   "_" +
                   gradesList
                       .Select(g => g.ToString("X1"))
                       .Concatenate("_");
        }

        public static Tuple<bool, uint> GetKVectorsBilinearProductGrade(this GaClcOperationKind operationKind, uint vSpaceDimension, uint inGrade1, uint inGrade2)
        {
            if (inGrade1 > vSpaceDimension)
                return new Tuple<bool, uint>(false, 0);

            if (inGrade2 > vSpaceDimension)
                return new Tuple<bool, uint>(false, 0);

            var outGrade = operationKind switch
            {
                GaClcOperationKind.BinaryScalarProduct => 
                    0,

                GaClcOperationKind.BinaryOuterProduct => 
                    inGrade1 + inGrade2,

                GaClcOperationKind.BinaryLeftContractionProduct => 
                    inGrade2 - inGrade1,

                GaClcOperationKind.BinaryRightContractionProduct => 
                    inGrade1 - inGrade2,

                GaClcOperationKind.BinaryFatDotProduct => 
                    Math.Abs(inGrade1 - inGrade2),
                
                GaClcOperationKind.BinaryHestenesInnerProduct => 
                    inGrade1 == 0 || inGrade2 == 0 ? 0 : Math.Abs(inGrade1 - inGrade2),

                _ => -1
            };

            if (outGrade < 0 || outGrade > vSpaceDimension)
                return new Tuple<bool, uint>(false, 0);

            return new Tuple<bool, uint>(true, (uint) outGrade);
        }

        public static GaClcOperationSpecs CreateEuclideanOperationSpecs(this GaClcOperationKind operationKind)
        {
            return new GaClcOperationSpecs(operationKind, true);
        }

        public static GaClcOperationSpecs CreateMetricOperationSpecs(this GaClcOperationKind operationKind)
        {
            return new GaClcOperationSpecs(operationKind, false);
        }

        public static GaClcOperationSpecs CreateOperationSpecs(this GaClcOperationKind operationKind, bool isEuclidean)
        {
            return new GaClcOperationSpecs(operationKind, isEuclidean);
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