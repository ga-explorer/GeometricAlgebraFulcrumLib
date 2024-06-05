namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Constants;

public static class MathMlConstantsUtils
{
    private static readonly string[] BooleanNames =
        { "true", "false", "" };

    private static readonly string[] OperatorFormNames =
        { "prefix", "infix", "postfix", "" };

    private static readonly string[] TextDirectionNames =
        { "ltr", "rtl", "" };

    private static readonly string[] TextVariantNames =
    { 
        "normal", 
        "bold", 
        "italic", 
        "bold-italic", 
        "double-struck", 
        "bold-fraktur", 
        "script", 
        "bold-script", 
        "fraktur", 
        "sans-serif", 
        "bold-sans-serif", 
        "sans-serif-italic", 
        "sans-serif-bold-italic", 
        "monospace", 
        "initial", 
        "tailed", 
        "looped", 
        "stretched",
        ""
    };

    private static readonly string[] RelativeLengthUnitNames =
        { "ch", "em", "ex", "rem" };

    private static readonly string[] PercentageLengthUnitNames =
        { "vh", "vw", "vmin", "vmax" };

    private static readonly string[] AbsoluteLengthUnitNames =
        { "px", "cm", "mm", "in", "pc", "pt" };

    private static readonly string[] MathMlDisplayNames =
        { "inline", "bloxk", "" };

    private static readonly string[] MathMlOverflowNames =
        { "linebreak", "scroll", "elide", "truncate", "scale", "" };


    internal static string GetName(this MathMlBoolean value)
        => BooleanNames[(int)value];

    internal static string GetName(this MathMlOperatorForm value)
        => OperatorFormNames[(int)value];

    internal static string GetName(this MathMlTextDirection value)
        => TextDirectionNames[(int)value];

    internal static string GetName(this MathMlTextVariant value)
        => TextVariantNames[(int)value];

    internal static string GetName(this MathMlRelativeLengthUnit value)
        => RelativeLengthUnitNames[(int)value];

    internal static string GetName(this MathMlPercentageLengthUnit value)
        => PercentageLengthUnitNames[(int)value];

    internal static string GetName(this MathMlAbsoluteLengthUnit value)
        => AbsoluteLengthUnitNames[(int)value];

    internal static string GetName(this MathMlDisplay value)
        => MathMlDisplayNames[(int)value];

    internal static string GetName(this MathMlOverflow value)
        => MathMlOverflowNames[(int)value];
}