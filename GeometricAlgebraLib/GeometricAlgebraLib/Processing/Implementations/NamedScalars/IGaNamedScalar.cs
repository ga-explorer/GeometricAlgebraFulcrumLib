using GeometricAlgebraLib.Processing.Scalars;

namespace GeometricAlgebraLib.Processing.Implementations.NamedScalars
{
    public interface IGaNamedScalar<TScalar>
    {
        GaNamedScalarsCollection<TScalar> NamedScalarsCollection { get; }

        IGaScalarProcessorNamedScalar<TScalar> NamedScalarProcessor { get; }

        IGaSymbolicScalarProcessor<TScalar> SymbolicScalarProcessor { get; }

        int ScalarId { get; }

        string ScalarName { get; }

        string ExternalName { get; set; }

        TScalar LhsScalarValue { get; }

        TScalar RhsScalarValue { get; }

        string RhsScalarValueText { get; }

        TScalar FinalRhsScalarValue { get; }

        string FinalRhsScalarValueText { get; }

        bool IsConstant { get; }

        bool IsParameter { get; }

        bool IsIntermediate { get; }

        bool IsOutput { get; }

        bool IsDependent { get; }

        bool IsIndependent { get; }

        bool IsUsedForOutputVariables { get; set; }

        TScalar GetScalarValue(bool useRhsScalarValue);
    }
}
