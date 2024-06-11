using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.Utilities.Text
{
    public static class TextUtils
    {
        public static StringBuilder AppendComplexNumber<T>(this StringBuilder s, IScalarProcessor<T> scalarProcessor, T numberReal, T numberImaginary)
        {
            if (!scalarProcessor.IsZero(numberReal))
                s.Append(scalarProcessor.ToText(numberReal));

            if (!scalarProcessor.IsZero(numberImaginary))
                s.Append(scalarProcessor.ToText(numberImaginary)).Append(" i");

            return s;
        }
    }
}
