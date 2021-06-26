using System.Collections.Generic;

namespace GeometricAlgebraLib.CodeComposer
{
    /// <summary>
    /// GaClc built-in type names
    /// </summary>
    public static class GaClcTypeNames
    {
        public static string Scalar => "scalar";
        
        public static string Integer => "int";
        
        public static string Bool => "bool";

        public static string Unit => "unit";

        public static IReadOnlyList<string> BuiltinTypeNames { get; } 
            = new[]{ Unit, Scalar, Integer, Bool };
    }
}