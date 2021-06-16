using System;
using GeometricAlgebraLib.Implementations.NamedScalars;
using GeometricAlgebraLib.Symbolic.NamedScalars;

namespace GeometricAlgebraLib.Samples.NamedScalars
{
    public static class Sample1
    {
        public static void Execute()
        {
            var namedScalarsCollection = 
                new GaNamedMathematicaExprCollection();

            var u =
                namedScalarsCollection.ParametersFactory.CreateVector(
                    "u1",
                    "u2",
                    "u3"
                );

            var v =
                namedScalarsCollection.ParametersFactory.CreateVector(
                    "v1",
                    "v2",
                    "v3"
                );

            var rotor = 
                namedScalarsCollection.VariablesFactory.CreateEuclideanSimpleRotor(
                    u, 
                    v
                );
            
            rotor.Storage.SetIsOutput(true);

            namedScalarsCollection.OptimizeCollection();

            v.SetFinalScalarNamesByIndex(index => $"v[{index}]");
            u.SetFinalScalarNamesByIndex(index => $"u[{index}]");
            namedScalarsCollection.SetIntermediateFinalScalarNamesByOrder(index => $"temp{index}");
            rotor.Storage.SetFinalScalarNamesById(id => $"rotor.Scalar{id}");

            Console.WriteLine("Named scalars:");
            Console.WriteLine(namedScalarsCollection.ToString());
            Console.WriteLine();
        }
    }
}
