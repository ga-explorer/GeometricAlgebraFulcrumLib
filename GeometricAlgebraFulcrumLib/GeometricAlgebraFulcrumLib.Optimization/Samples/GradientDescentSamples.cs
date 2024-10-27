using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Optimization.GradientDescent;

namespace GeometricAlgebraFulcrumLib.Optimization.Samples
{
    public static class GradientDescentSamples
    {

        public static void Sample1()
        {
            // double[,] a = new[,] {{4.2}, {5.43}, {3.221}, {7.34235}, {1.931}, {1.2}, {5.43}, {8.0}, {7.34235}, {1.931}};
            // double[] b = new double[a.GetLength(0)];
            // for (int i = 0; i < b.GetLength(0); i++) {
            //   b[i] = (a[i,0] * .498);
            // }
            const int numRows = 100000; // number of rows in the 2D array
            const int numCols = 1000; // number of columns in the 2D array
            var x = new double[numRows, numCols];
            var y = new double[numRows];
            var w = new double[numCols];

            var rand = new Random(); // create a random number generator

            // fill the 2D array x with random values
            for (var i = 0; i < numRows; i++)
            {

                for (var j = 0; j < numCols; j++)
                {
                    x[i, j] = rand.NextDouble(); // assign a random value to the current element

                }
            }
            for (var i = 0; i < numCols; i++)
            {
                w[i] = rand.NextDouble() * 10;
            }
            for (var i = 0; i < numRows; i++)
            {
                y[i] = 0;
                for (var j = 0; j < numCols; j++)
                {
                    y[i] += x[i, j] * w[j]; // assign a random value to the current element

                }
            }

            var stop = new Stopwatch();
            var sgd = new GpuSgd(x, y);
            // stop.Start();
            // sgd.SGDfit(100,1000, 0.00001);
            // stop.Stop();
            // Console.Write("Elapsed time for GPU: ");
            // Console.WriteLine(stop.ElapsedMilliseconds);
            // stop.Reset();
            // Console.WriteLine("Here");
            // Console.Write("GPU average error: ");
            // Console.WriteLine(sgd.compareWeights(w,sgd.getWeights()));

            // //sgd.print1d(sgd.getWeights());
            // Console.WriteLine("_________________");
            // Console.WriteLine("Real Weights:");
            //sgd.print1d(w);

            Console.WriteLine("_________________");
            // // stop.Start();
            // // //sgd.print1d(sgd.BatchGradientDescent(x,y,0.1,1000));
            // // stop.Stop();
            // // Console.Write("Elapsed time for Reg: ");
            // // Console.WriteLine(stop.ElapsedMilliseconds);
            // // stop.Reset();
            // // Console.WriteLine("_________________");
            // //             stop.Start();
            // Console.Write("ADJ average error: ");
            // Console.WriteLine(sgd.compareWeights(w,sgd.BatchGradientDescent(x,y,0.001,1000)));
            // // //sgd.print1d(BatchGradientDescentADJ(x,y,0.1,1000));
            // // stop.Stop();
            // // Console.Write("Elapsed time ADJ: ");
            // // Console.WriteLine(stop.ElapsedMilliseconds);
            // stop.Reset();

            stop.Start();
            //sgd.print1d(sgd.StochasticGradientDescent(x,y,0.1,1000, 10000));
            Console.Write("SGD weights: ");
            //sgd.print1d(sgd.StochasticGradientDescentADJ(x,y,0.1,1000, 1000));
            Console.WriteLine(sgd.CompareWeights(w, sgd.StochasticGradientDescentAdj(x, y, 0.01, 10000, 1000)));

            Console.WriteLine("_________________");
            stop.Stop();
            Console.Write("Elapsed time SGD: ");
            Console.WriteLine(stop.ElapsedMilliseconds);
            stop.Reset();

            stop.Start();
            //sgd.print1d(sgd.StochasticGradientDescent(x,y,0.1,1000, 10000));
            Console.Write("GPU weights: ");
            //sgd.print1d(sgd.SGDgpu(x,y,0.1,1000, 1000));
            Console.WriteLine(sgd.CompareWeights(w, sgd.SgDgpu(x, y, 0.01, 10000, 1000)));
            stop.Stop();
            Console.Write("Elapsed time GPU: ");
            Console.WriteLine(stop.ElapsedMilliseconds);
            stop.Reset();
            //BatchGradientDescentADJ

        }
    }
}
