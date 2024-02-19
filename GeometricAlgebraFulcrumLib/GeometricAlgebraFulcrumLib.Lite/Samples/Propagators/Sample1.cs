using GeometricAlgebraFulcrumLib.Lite.PropagatorNetworks;
using GeometricAlgebraFulcrumLib.Lite.PropagatorNetworks.Float64;

namespace GeometricAlgebraFulcrumLib.Lite.Samples.Propagators
{
    public static class Sample1
    {
        public static void Execute()
        {
            var pn = new PropagatorNetwork()
            {
                DebugMode = true
            };

            pn.BeginModify();

            var a = pn.DefineFloat64Cell("a");
            var b = pn.DefineFloat64Cell("b");
            var c = pn.DefineFloat64Cell("c");

            pn.AssignFloat64PythagoreanSum("c", "a", "b");
            
            //var a2 = pn["aSquare"];
            //var b2 = pn["bSquare"];
            //var c2 = pn["cSquare"];

            pn.EndModify();

            a.Update(3);
            b.Update(4);

            Console.WriteLine();
            Console.WriteLine(pn);
            Console.WriteLine();
        }
    }
}
