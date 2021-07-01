using System;
using System.Collections.Generic;
using System.Linq;
using TextComposerLib.Text;

namespace GeometricAlgebraLib.CodeComposer
{
    public static class GaClcOperationUtils
    {
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
        ///// Names of default frame macros for computing general linear transforms on multivectors
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
        ///// Names of default frame macros for computing general outer-morphisms on multivectors
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