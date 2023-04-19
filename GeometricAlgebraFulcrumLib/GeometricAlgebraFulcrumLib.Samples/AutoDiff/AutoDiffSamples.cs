using System;
using GeometricAlgebraFulcrumLib.MathBase.Differential.AutoDiff;

namespace GeometricAlgebraFulcrumLib.Samples.AutoDiff
{
    public static class AutoDiffSamples
    {
        public static class RootFinder
        {
            /// <summary>
            /// Attempts to solve an equation f(x) = 0, given f, using the newton-raphson method.
            /// </summary>
            /// <param name="func">The function</param>
            /// <param name="x">The variable used in the function.</param>
            /// <param name="initGuess">An initial guess where to start the iterations.</param>
            /// <param name="maxIterations">The number of iterations to perform</param>
            /// <returns>The approximated solution of f(x) = 0</returns>
            public static double NewtonRaphson(Term func, Variable x, double initGuess, int maxIterations = 10)
            {
                var guess = initGuess;
                var compiledFunc = func.Compile(x);
                for (var i = 0; i < maxIterations; ++i) // perform maxIterations iterations
                {
                    // perform differentiation
                    var diffResult = compiledFunc.Differentiate(guess);
                
                    // extract function value + derivative (the first element of the gradient)
                    var dfx = diffResult.Item1[0];
                    var fx = diffResult.Item2;

                    // newton-raphson iteration: x <- x - f(x) / f'(x)
                    guess = guess - fx / dfx;
                }
                return guess;
            }
        }

        public static void Example1()
        {
            // we will use a function of two variables
            var x = new Variable();
            var y = new Variable();

            // func(x, y) = (x + y) * exp(x - y)
            var func = (x + y) * TermBuilder.Exp(x - y);

            // define the ordering of variables, and a point where the function will be evaluated/differentiated
            Variable[] vars = { x, y };
            double[] point = { 1, -2 };
            
            // calculate the value and the gradient at the point (x, y) = (1, -2)
            var eval = func.Evaluate(vars, point);
            var gradient = func.Differentiate(vars, point);

            // write the results
            Console.WriteLine("f(1, -2) = " + eval);
            Console.WriteLine("Gradient of f at (1, -2) = ({0}, {1})", gradient[0], gradient[1]);
        }

        public static void Example2()
        {
            // we will use a function of two variables
            var x = new Variable();
            var y = new Variable();

            // func(x, y) = (x + y) * exp(x - y)
            var func = (x + y) * TermBuilder.Exp(x - y);

            // create compiled term and use it for many gradient/value evaluations
            var compiledFunc = func.Compile(x, y);

            // we can now efficiently compute the gradient of "func" 64 times with different inputs.
            // compilation helps when we need many evaluations of the same function.
            for (var i = 0; i < 64; ++i)
            {
                var xVal = i / 64.0;
                var yVal = 1 + i / 128.0;

                var diffResult = compiledFunc.Differentiate(xVal, yVal);
                var gradient = diffResult.Item1;
                var value = diffResult.Item2;

                Console.WriteLine("The value of func at x = {0}, y = {1} is {2} and the gradient is ({3}, {4})", 
                    xVal, yVal, value, gradient[0], gradient[1]);
            }
        }

        
        private static double[] GradientDescent(ICompiledTerm func, double[] init, double stepSize, int iterations)
        {
            // clone the initial argument
            var x = (double[])init.Clone();
            var gradient = new double[x.Length];

            // perform the iterations
            for (var i = 0; i < iterations; ++i)
            {
                // compute the gradient - fill the gradient array
                func.Differentiate(x, gradient);

                // perform a descent step
                for (var j = 0; j < x.Length; ++j)
                    x[j] -= stepSize * gradient[j];
            }

            return x;
        }

        public static void Example3()
        {
            var x = new Variable();
            var y = new Variable();
            var z = new Variable();

            // f(x, y, z) = (x-2)² + (y+4)² + (z-1)²
            // the min should be (x, y, z) = (2, -4, 1)
            var func = TermBuilder.Power(x - 2, 2) + TermBuilder.Power(y + 4, 2) + TermBuilder.Power(z - 1, 2);
            var compiled = func.Compile(x, y, z);

            // perform optimization
            var vec = new double[3];
            vec = GradientDescent(compiled, vec, stepSize: 0.01, iterations: 1000);

            Console.WriteLine("The approx. minimizer is: {0}, {1}, {2}", vec[0], vec[1], vec[2]);
        }

        public static void Example4()
        {
            // create function factory for arctangent
            var arctan = UnaryFunc.Factory(
                x => Math.Atan(x),      // evaluate
                x => 1 / (1 + x * x));  // derivative of atan

            // create function factory for atan2
            var atan2 = BinaryFunc.Factory(
                (x, y) => Math.Atan2(y, x),
                (x, y) => Tuple.Create(
                    -y / (x*x + y*y),  // d/dx (from wikipedia)
                    x / (x*x + y*y))); // d/dy (from wikipedia)

        
            // define variables
            var u = new Variable();
            var v = new Variable();
            var w = new Variable();

            // create and compile a term
            var term = atan2(u, v) - arctan(w) * atan2(v, w);
            var compiled = term.Compile(u, v, w);

            // compute value + gradient
            var diff = compiled.Differentiate(1, 2, -2);

            Console.WriteLine("The value at (1, 2, -2) is {0}", diff.Item2);
            Console.WriteLine("The gradient at (1, 2, -2) is ({0}, {1}, {2})", 
                diff.Item1[0], 
                diff.Item1[1], 
                diff.Item1[2]);
        }

        public static void Example5()
        {
            var x = new Variable();

            // f(x) = e^(-x) + x - 2
            // it has two roots. See plot.png image attached to this project.    
            var func = TermBuilder.Exp(-x) + x - 2;

            // find the root near 2
            var root1 = RootFinder.NewtonRaphson(func, x, 2);
            
            // find the root near -1
            var root2 = RootFinder.NewtonRaphson(func, x, -1);

            Console.WriteLine("The equation e^(-x) + x - 2 = 0 has two solutions:\nx = {0}\nx = {1}", root1, root2);
        }
    }
}
