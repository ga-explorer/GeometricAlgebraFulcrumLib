namespace GeometricAlgebraFulcrumLib.Modeling.Signals.Interpolators.SavitzkyGolay;

/**
* This interface represents types which are able to perform data processing in
* place. Useful examples include: eliminating zeros, padding etc.
*
* @author Marcin Rzeźnicki
* @see SGFilter#appendPreprocessor(Preprocessor)
*/
public interface ISgPreprocessor
{

    /**
 * Data processing method. Called on Preprocessor instance when its
 * processing is needed
 *
 * @param data
 */
    void Apply(double[] data);
}