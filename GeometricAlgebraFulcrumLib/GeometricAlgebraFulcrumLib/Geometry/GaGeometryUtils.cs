using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Rotors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry
{
    public static class GaGeometryUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetAngle<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> v1, IGaStorageVector<T> v2)
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
        public static bool IsEuclideanRotor<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> storage)
        {
            if (storage.GetGrades().Any(grade => grade % 2 != 0))
                return false;

            return scalarProcessor.IsZero(
                scalarProcessor
                    .Subtract(
                        scalarProcessor.EGpReverse(storage), 
                        scalarProcessor.OneScalar
                    )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSimpleEuclideanRotor<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> storage)
        {
            if (storage.GetGrades().Any(grade => grade != 0 && grade != 2))
                return false;

            return scalarProcessor.IsZero(
                scalarProcessor
                    .Subtract(
                        scalarProcessor.EGpReverse(storage), 
                        scalarProcessor.OneScalar
                    )
            );
        }

        
        public static GaPureRotor<T> ComplexEigenPairToEuclideanSimpleRotor<T>(this IGaProcessor<T> processor, T realValue, T imagValue, T[] realVector, T[] imagVector)
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
