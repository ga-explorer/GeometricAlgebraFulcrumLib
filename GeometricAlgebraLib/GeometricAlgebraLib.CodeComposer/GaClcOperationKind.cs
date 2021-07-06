namespace GeometricAlgebraLib.CodeComposer
{
    public enum GaClcOperationKind
    {
        UnaryNegative,
        UnaryReverse,
        UnaryGradeInvolution,
        UnaryCliffordConjugate,
        //UnaryMagnitude,
        //UnaryMagnitudeSquared,
        UnaryNorm,
        UnaryNormSquared,
        UnaryNormalize,
        UnaryDual,
        UnaryUnDual,
        UnaryGeometricProductSquared,
        UnaryGeometricProductReverse,

        BinaryOuterProduct,
        BinaryGeometricProduct,
        BinaryScalarProduct,
        BinaryLeftContractionProduct,
        BinaryRightContractionProduct,
        BinaryFatDotProduct,
        BinaryHestenesInnerProduct,
        BinaryCommutatorProduct,
        BinaryAntiCommutatorProduct,
        BinaryGeometricProductDual,
        BinaryDirectSandwichProduct,
        BinaryGradeInvolutionSandwichProduct,
        BinaryTimesWithScalar,
        BinaryDivideByScalar,
        BinaryDeltaProduct,
        BinaryDeltaProductDual,

        BinaryProject,
        BinaryReflect,
        BinaryComplement,
        BinaryRotate,

    }
}