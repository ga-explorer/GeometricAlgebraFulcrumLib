using CodeComposerLib.Irony.Semantic.Type;

namespace GeometricAlgebraLib.CodeComposer
{
    internal static class GaClcLanguageTypeExtensions
    {
        /// <summary>
        /// True for built-in types
        /// </summary>
        /// <param name="langType"></param>
        /// <returns></returns>
        public static bool IsPrimitiveType(this ILanguageType langType)
        {
            return langType is TypePrimitive;
        }

        /// <summary>
        /// True for builtin integer type
        /// </summary>
        /// <param name="langType"></param>
        /// <returns></returns>
        public static bool IsInteger(this ILanguageType langType)
        {
            if (langType is not TypePrimitive typePrimitive ) 
                return false;

            return typePrimitive.ObjectName == GaClcTypeNames.Integer;
        }

        /// <summary>
        /// True for built-in scalar type
        /// </summary>
        /// <param name="langType"></param>
        /// <returns></returns>
        public static bool IsScalar(this ILanguageType langType)
        {
            if (langType is not TypePrimitive typePrimitive)
                return false;

            return typePrimitive.ObjectName == GaClcTypeNames.Scalar;
        }

        /// <summary>
        /// True for built-in integer or scalar type
        /// </summary>
        /// <param name="langType"></param>
        /// <returns></returns>
        public static bool IsNumber(this ILanguageType langType)
        {
            if (langType is not TypePrimitive typePrimitive)
                return false;

            return typePrimitive.ObjectName == GaClcTypeNames.Integer || typePrimitive.ObjectName == GaClcTypeNames.Scalar;
        }

        /// <summary>
        /// True for built-in boolean type
        /// </summary>
        /// <param name="langType"></param>
        /// <returns></returns>
        public static bool IsBoolean(this ILanguageType langType)
        {
            if (langType is not TypePrimitive typePrimitive)
                return false;

            return typePrimitive.ObjectName == GaClcTypeNames.Bool;
        }
        
        /// <summary>
        /// Returns true if an expression of type rhs_type can be assigned to an expression of type lhs_type
        /// </summary>
        /// <param name="lhsType"></param>
        /// <param name="rhsType"></param>
        /// <returns></returns>
        public static bool CanAssignValue(this ILanguageType lhsType, ILanguageType rhsType)
        {
            return
                (IsBoolean(lhsType) && IsBoolean(rhsType)) ||
                (IsInteger(lhsType) && IsInteger(rhsType)) ||
                (IsScalar(lhsType) && IsNumber(rhsType));
        }
    }
}