using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Languages
{
    /// <summary>
    /// Built-in type names
    /// </summary>
    public static class GaLanguageTypeNames
    {
        public static string Scalar => "scalar";
        
        public static string Integer => "int";
        
        public static string Bool => "bool";

        public static string Unit => "unit";

        public static IReadOnlyList<string> BuiltinTypeNames { get; } 
            = new[]{ Unit, Scalar, Integer, Bool };
    }
}