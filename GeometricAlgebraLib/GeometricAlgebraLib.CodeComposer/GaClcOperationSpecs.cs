using System;
using System.Collections.Generic;
using System.Linq;
using TextComposerLib.Text;

namespace GeometricAlgebraLib.CodeComposer
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
            if (inGrade1 < 0 || inGrade1 > vSpaceDimension)
                return -1;

            if (inGrade2 < 0 || inGrade2 > vSpaceDimension)
                return -1;

            var outGrade = OperationKind switch
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
                return -1;

            return outGrade;
        }

        public string GetName()
        {
            var euclideanPrefix = IsEuclidean 
                ? "E" : string.Empty;

            return OperationKind switch
            {
                GaClcOperationKind.UnaryNegative => "Negative",
                GaClcOperationKind.UnaryReverse => "Reverse",
                GaClcOperationKind.UnaryGradeInvolution => "GradeInvolution",
                GaClcOperationKind.UnaryCliffordConjugate => "CliffordConjugate",
                GaClcOperationKind.UnaryMagnitude => $"{euclideanPrefix}Magnitude",
                GaClcOperationKind.UnaryMagnitudeSquared => $"{euclideanPrefix}MagnitudeSquared",
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

        public string GetName(int grade)
        {
            return GetName() + "_" + grade.ToString("X1");
        }

        public string GetName(params int[] gradesList)
        {
            return GetName() + 
                   "_" +
                   gradesList
                       .Select(g => g.ToString("X1"))
                       .Concatenate("_");
        }

        public string GetName(IEnumerable<int> gradesList)
        {
            return GetName() + 
                   "_" +
                   gradesList
                       .Select(g => g.ToString("X1"))
                       .Concatenate("_");
        }

        public override string ToString()
        {
            return GetName();
        }
    }
}