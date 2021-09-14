using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GeoGeometryUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetAngle<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> v1, VectorStorage<T> v2)
        {
            return scalarProcessor.ArcCos(
                scalarProcessor.Divide(
                    scalarProcessor.ESp(v1, v2),
                    scalarProcessor.Sqrt(
                        scalarProcessor.Times(
                            scalarProcessor.ESp(v1), 
                            scalarProcessor.ESp(v2)
                        )
                    )
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEuclideanRotor<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> storage)
        {
            if (storage.GetGrades().Any(grade => grade % 2 != 0))
                return false;

            return scalarProcessor.IsZero(
                scalarProcessor
                    .Subtract(
                        scalarProcessor.EGpReverse(storage), 
                        scalarProcessor.ScalarOne
                    )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSimpleEuclideanRotor<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> storage)
        {
            if (storage.GetGrades().Any(grade => grade != 0 && grade != 2))
                return false;

            return scalarProcessor.IsZero(
                scalarProcessor
                    .Subtract(
                        scalarProcessor.EGpReverse(storage), 
                        scalarProcessor.ScalarOne
                    )
            );
        }

        
        public static PureRotor<T> ComplexEigenPairToEuclideanSimpleRotor<T>(this IGeometricAlgebraProcessor<T> processor, T realValue, T imagValue, ILinVectorStorage<T> realVector, ILinVectorStorage<T> imagVector)
        {
            //var scalar = scalarProcessor.Add(
            //    scalarProcessor.Times(realValue, realValue),
            //    scalarProcessor.Times(imagValue, imagValue)
            //);

            var angle = processor.ArcTan2(
                realValue, 
                imagValue
            );

            var blade = processor.VectorsOp(
                realVector, 
                imagVector
            );

            return processor.CreateEuclideanRotor(angle, blade);

            //Console.WriteLine($"Eigen value real part: {realValue.GetLaTeXDisplayEquation()}");
            //Console.WriteLine();

            //Console.WriteLine($"Eigen value imag part: {imagValue.GetLaTeXDisplayEquation()}");
            //Console.WriteLine();

            //Console.WriteLine($"Eigen value length: {scalar.GetLaTeXDisplayEquation()}");
            //Console.WriteLine();

            //Console.WriteLine($"Eigen value angle: {angle.GetLaTeXDisplayEquation()}");
            //Console.WriteLine();

            //Console.WriteLine("Eigen vector real part:");
            //Console.WriteLine(realVector.TermsToLaTeX().GetLaTeXDisplayEquation());
            //Console.WriteLine();

            //Console.WriteLine("Eigen vector imag part:");
            //Console.WriteLine(imagVector.TermsToLaTeX().GetLaTeXDisplayEquation());
            //Console.WriteLine();

            //Console.WriteLine("Blade:");
            //Console.WriteLine(blade.ToLaTeXEquationsArray("B", @"\mu"));
            //Console.WriteLine();

            //Console.WriteLine("Final rotor:");
            //Console.WriteLine(rotor.ToLaTeXEquationsArray("R", @"\mu"));
            //Console.WriteLine();

            //Console.WriteLine($"Is simple rotor? {rotor.IsSimpleRotor()}");
            //Console.WriteLine();

            //Console.WriteLine();

            //return rotor;
        }

    }
}
