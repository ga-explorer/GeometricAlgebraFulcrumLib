namespace GeometricAlgebraFulcrumLib.Algebra.SignalAlgebra.Interpolators.SavitzkyGolay
{
    /**
 * This interface represents types which are able to filter data, for example:
 * eliminate redundant points.
 * 
 * @author Marcin Rzeźnicki
 * @see SGFilter#appendPreprocessor(Preprocessor)
 */
    public interface ISgDataFilter
    {

        double[] Filter(double[] data);
    }
}