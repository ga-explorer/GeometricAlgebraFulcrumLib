using System.Collections.Generic;
using GeometricAlgebraLib.Processors.Scalars;

namespace GeometricAlgebraLib.Implementations.NamedScalars
{
    public interface IGaNamedScalar<TScalar>
    {
        GaNamedScalarsCollection<TScalar> NamedScalarsCollection { get; }

        IGaScalarProcessorNamedScalar<TScalar> NamedScalarProcessor { get; }

        IGaSymbolicScalarProcessor<TScalar> SymbolicScalarProcessor { get; }

        int ScalarId { get; }

        string ScalarName { get; }

        string FinalScalarName { get; set; }

        TScalar LhsScalarValue { get; }

        TScalar RhsScalarValue { get; }

        string RhsScalarValueText { get; }

        bool IsConstant { get; }

        bool IsParameter { get; }

        bool IsInput { get; }

        bool IsIntermediate { get; }

        bool IsOutput { get; }

        bool IsVariable { get; }

        bool IsUsedForOutputVariables { get; set; }

        IEnumerable<IGaNamedScalar<TScalar>> DependsOnScalars { get; }
    }
}
