using ILGPU;
using ILGPU.Runtime;

namespace GeometricAlgebraFulcrumLib.Optimization.GradientDescent;

public class GpuSgd
{
    private readonly Context _context;
    private readonly Device _dev;
    private Accelerator _accelerate;
    private readonly double[,] _arr;
    private double[] _weights;
    private readonly double[] _y;


    public GpuSgd(double[,] array, double[] y)
    {
        _arr = array;
        _y = y;
        //this.distributions = new int[10];
        _context = Context.Create(builder => builder.AllAccelerators().EnableAlgorithms());

        _dev = _context.GetPreferredDevice(preferCPU: false);


    }

    public void SgDfit(int epoch, double learningRate = 0.1)
    {
        return;
    }

    public void Fit(int epochs, double learningRate = 0.01)
    {
        var accelerate = _dev.CreateAccelerator(_context);
        _weights = new double[_arr.GetLength(1)];
        _weights = InitializeGaussian(_weights);
        Print1d(_weights);
        var xBuffer = accelerate.Allocate2DDenseX<double>(new Index2D(_arr.GetLength(0), _arr.GetLength(1)));
        var yBuffer = accelerate.Allocate1D<double>(new Index1D(_y.GetLength(0)));
        var weightsBuffer = accelerate.Allocate1D<double>(new Index1D(_weights.GetLength(0)));
        var gradBuffer = accelerate.Allocate1D<double>(new Index1D(_weights.GetLength(0)));
        var hBuffer = accelerate.Allocate1D<double>(new Index1D(_weights.GetLength(0)));


        xBuffer.CopyFromCPU(_arr);
        yBuffer.CopyFromCPU(_y);
        weightsBuffer.CopyFromCPU(_weights);
        var setBuffToValueKern = accelerate.LoadAutoGroupedStreamKernel<
            Index1D,
            ArrayView1D<double, Stride1D.Dense>,
            double>(SetBuffToValueKernel);
        var gradientKern = accelerate.LoadAutoGroupedStreamKernel<
            Index2D,
            ArrayView2D<double, Stride2D.DenseX>,
            ArrayView1D<double, Stride1D.Dense>,
            ArrayView1D<double, Stride1D.Dense>,
            ArrayView1D<double, Stride1D.Dense>, int
        >(GradientKernel);
        var thetaKern = accelerate.LoadAutoGroupedStreamKernel<
            Index1D,
            ArrayView1D<double, Stride1D.Dense>,
            ArrayView1D<double, Stride1D.Dense>,
            double,
            int>(ThetaKernel);
        for (var i = 0; i < epochs; i++)
        {
            setBuffToValueKern(gradBuffer.Extent.ToIntIndex(), gradBuffer.View, 0.0);
            gradientKern(xBuffer.Extent.ToIntIndex(), xBuffer.View, weightsBuffer.View, gradBuffer.View, yBuffer.View, _arr.GetLength(0));
            accelerate.Synchronize();
            thetaKern(weightsBuffer.Extent.ToIntIndex(), weightsBuffer.View, gradBuffer.View, learningRate, _y.GetLength(0));

        }
        _weights = weightsBuffer.GetAsArray1D();


    }

    private static double[] BatchGradientDescentAdj(double[,] x, double[] y, double alpha, int epochs)
    {
        var m = x.GetLength(0);
        var n = x.GetLength(1);
        var theta = new double[n];
        var grad = new double[n];
        for (var epoch = 0; epoch < epochs; epoch++)
        {
            var h = new double[m];
            for (var i = 0; i < m; i++)
            {
                double sum = 0;
                for (var j = 0; j < n; j++)
                {
                    sum += x[i, j] * theta[j] - y[i] / n;
                }
                h[i] = sum;

            }

            // double[] error = new double[m];
            // for (int i = 0; i < m; i++)
            // {
            //     error[i] = h[i] - y[i];
            // }

            for (var i = 0; i < n; i++)
            {
                double sum1 = 0;
                for (var j = 0; j < m; j++)
                {
                    sum1 += h[j] * x[j, i] / m;
                }
                grad[i] = sum1;
            }

            for (var j = 0; j < n; j++)
            {
                theta[j] = theta[j] - alpha * grad[j];
            }
        }
        return theta;
    }

    public double[] StochasticGradientDescent(double[,] x, double[] y, double learningRate, int epochs, int batch)
    {
        var numFeatures = x.GetLength(1);
        var weights = new double[numFeatures];
        int index;
        var rand = new Random();

        // Train the model
        for (var i = 0; i < epochs; i++)
        {
            for (var j = 0; j < batch; j++)
            {
                // Compute the prediction
                index = rand.Next(x.GetLength(0));
                double prediction = 0;
                for (var k = 0; k < numFeatures; k++)
                {
                    prediction += weights[k] * x[index, k];
                }
                // Console.Write("Prediction: ");
                // Console.WriteLine(prediction);
                // Console.ReadLine();
                // Compute the error
                var error = y[index] - prediction;
                // Console.Write("Error: ");
                // Console.WriteLine(error);
                // Console.ReadLine();
                // Update the weights
                //weights[0] += learningRate * error * X[index,0] ;
                for (var k = 0; k < numFeatures; k++)
                {
                    weights[k] += learningRate * (error * x[index, k]) / numFeatures;
                }
                // Console.Write("Weights[0]: ");
                // Console.WriteLine(weights[0]);
                // Console.ReadLine();
            }
        }
        return weights;
    }

    public double[] StochasticGradientDescentAdj(double[,] x, double[] y, double learningRate, int epochs, int batch)
    {
        var numFeatures = x.GetLength(1);
        var weights = new double[numFeatures];
        var rand = new Random();

        // Train the model
        for (var i = 0; i < epochs; i++)
        {
            for (var j = 0; j < batch; j++)
            {
                // Compute the prediction
                var index = rand.Next(x.GetLength(0));
                double error = 0;
                for (var k = 0; k < numFeatures; k++)
                {
                    error += y[index] / numFeatures - weights[k] * x[index, k];
                }
                // Console.Write("Prediction: ");
                // Console.WriteLine(prediction);
                // Console.ReadLine();
                // Compute the error
                //double error =  Y[index] -  prediction ;
                // Console.Write("Error: ");
                // Console.WriteLine(error);
                // Console.ReadLine();
                // Update the weights
                //weights[0] += learningRate * error * X[index,0] ;
                for (var k = 0; k < numFeatures; k++)
                {


                    weights[k] += learningRate * (error * x[index, k]) / numFeatures;
                }
                // Console.Write("Weights[0]: ");
                // Console.WriteLine(weights[0]);
                // Console.ReadLine();
            }
        }
        return weights;
    }

    //GPU SGD final
    public double[] SgDgpu(double[,] x, double[] y, double learningRate, int epochs, int batch)
    {
        var accelerate = _dev.CreateAccelerator(_context);

        //this.weights = InitializeGaussian(this.weights);
        //print1d(this.weights);\
        var m = x.GetLength(0);
        var n = x.GetLength(1);
        var weights = new double[n];

        var xBuffer = accelerate.Allocate2DDenseX<double>(new Index2D(m, n));
        xBuffer.CopyFromCPU(x);
        var yBuffer = accelerate.Allocate1D<double>(new Index1D(m));
        yBuffer.CopyFromCPU(y);
        var weightsBuffer = accelerate.Allocate1D<double>(new Index1D(n));
        weightsBuffer.CopyFromCPU(weights);

        var errorBuffer = accelerate.Allocate1D<double>(new Index1D(1));

        int index;
        var rand = new Random();

        var part1 = accelerate.LoadAutoGroupedStreamKernel<
            Index1D,
            ArrayView2D<double, Stride2D.DenseX>,
            ArrayView1D<double, Stride1D.Dense>,
            ArrayView1D<double, Stride1D.Dense>,
            ArrayView1D<double, Stride1D.Dense>,
            int,
            int>(BatchRunPart1Kernel);
        var part2 = accelerate.LoadAutoGroupedStreamKernel<
            Index1D,
            ArrayView2D<double, Stride2D.DenseX>,
            ArrayView1D<double, Stride1D.Dense>,
            ArrayView1D<double, Stride1D.Dense>,
            int,
            int,
            double>(BatchRunPart2Kernel);
        var setBuffToValueKern = accelerate.LoadAutoGroupedStreamKernel<
            Index1D,
            ArrayView1D<double, Stride1D.Dense>,
            double>(SetBuffToValueKernel);


        for (var i = 0; i < epochs; i++)
        {
            for (var j = 0; j < batch; j++)
            {
                index = rand.Next(m);
                setBuffToValueKern(errorBuffer.Extent.ToIntIndex(), errorBuffer.View, 0.0);
                part1(weightsBuffer.Extent.ToIntIndex(), xBuffer.View, weightsBuffer.View, yBuffer.View, errorBuffer.View, index, n);
                part2(weightsBuffer.Extent.ToIntIndex(), xBuffer.View, weightsBuffer.View, errorBuffer.View, index, n, learningRate);
            }
        }
        return weightsBuffer.GetAsArray1D();
    }

    private static void BatchRunPart1Kernel(Index1D index, ArrayView2D<double, Stride2D.DenseX> xView, ArrayView1D<double, Stride1D.Dense> weightView, ArrayView1D<double, Stride1D.Dense> yView, ArrayView1D<double, Stride1D.Dense> errorView, int randIndex, int numFeatures)
    {

        Atomic.Add(ref errorView[new Index1D(0)], yView[randIndex] / numFeatures - weightView[index] * xView[new Index2D(randIndex, index.X)]);
    }

    private static void BatchRunPart2Kernel(Index1D index,
        ArrayView2D<double, Stride2D.DenseX> xView,
        ArrayView1D<double, Stride1D.Dense> weightView,
        ArrayView1D<double, Stride1D.Dense> errorView,

        int randIndex,

        int numFeatures,
        double lr)
    {

        Atomic.Add(ref weightView[index], lr * (errorView[new Index1D(0)] * xView[new Index2D(randIndex, index.X)]) / numFeatures);
    }
    public double[] BatchGradientDescent(double[,] x, double[] y, double alpha, int epochs)
    {
        var m = x.GetLength(0);
        var n = x.GetLength(1);
        var theta = new double[n];
        var grad = new double[n];
        //Console.WriteLine("BATCH");
        for (var epoch = 0; epoch < epochs; epoch++)
        {
            var h = new double[m];
            for (var i = 0; i < m; i++)
            {
                double sum = 0;
                for (var j = 0; j < n; j++)
                {
                    sum += x[i, j] * theta[j];
                }
                h[i] = sum;
            }

            var error = new double[m];
            for (var i = 0; i < m; i++)
            {
                error[i] = h[i] - y[i];
            }
            // Console.WriteLine("Error");
            // print1d(error);

            for (var j = 0; j < n; j++)
            {
                double sum = 0;
                for (var i = 0; i < m; i++)
                {
                    sum += error[i] * x[i, j];
                }
                grad[j] = sum / m;
            }
            // Console.WriteLine("Grad");
            // print1d(grad);
            for (var j = 0; j < n; j++)
            {
                theta[j] = theta[j] - alpha * grad[j];
            }
            // Console.WriteLine("Theta");
            // print1d(theta);
        }
        return theta;
    }
    public void Fitv2(int epochs, double learningRate = 0.01)
    {
        var accelerate = _dev.CreateAccelerator(_context);
        _weights = new double[_arr.GetLength(1)];
        //this.weights = InitializeGaussian(this.weights);
        //print1d(this.weights);\
        var m = _arr.GetLength(0);
        var n = _arr.GetLength(1);
        var xBuffer = accelerate.Allocate2DDenseX<double>(new Index2D(_arr.GetLength(0), _arr.GetLength(1)));
        var yBuffer = accelerate.Allocate1D<double>(new Index1D(_y.GetLength(0)));
        var weightsBuffer = accelerate.Allocate1D<double>(new Index1D(_weights.GetLength(0)));
        var gradBuffer = accelerate.Allocate1D<double>(new Index1D(_weights.GetLength(0)));
        var errorBuffer = accelerate.Allocate1D<double>(new Index1D(_arr.GetLength(0)));


        xBuffer.CopyFromCPU(_arr);
        yBuffer.CopyFromCPU(_y);
        weightsBuffer.CopyFromCPU(_weights);
        var setBuffToValueKern = accelerate.LoadAutoGroupedStreamKernel<
            Index1D,
            ArrayView1D<double, Stride1D.Dense>,
            double>(SetBuffToValueKernel);
        var gradientKern = accelerate.LoadAutoGroupedStreamKernel<
            Index2D,
            ArrayView2D<double, Stride2D.DenseX>,
            ArrayView1D<double, Stride1D.Dense>,
            ArrayView1D<double, Stride1D.Dense>, int
        >(GradKernel);
        var thetaKern = accelerate.LoadAutoGroupedStreamKernel<
            Index1D,
            ArrayView1D<double, Stride1D.Dense>,
            ArrayView1D<double, Stride1D.Dense>,
            double,
            int>(ThetaKernel);

        var errorKern = accelerate.LoadAutoGroupedStreamKernel<Index2D,
            ArrayView2D<double, Stride2D.DenseX>,
            ArrayView1D<double, Stride1D.Dense>,
            ArrayView1D<double, Stride1D.Dense>,
            ArrayView1D<double, Stride1D.Dense>,
            int>(ErrorKernel);


        for (var i = 0; i < epochs; i++)
        {
            setBuffToValueKern(gradBuffer.Extent.ToIntIndex(), gradBuffer.View, 0.0);
            setBuffToValueKern(errorBuffer.Extent.ToIntIndex(), errorBuffer.View, 0.0);
            errorKern(xBuffer.Extent.ToIntIndex(), xBuffer.View, weightsBuffer.View, errorBuffer.View, yBuffer.View, n);
            // Console.WriteLine("errorBuffer");
            // print1d(errorBuffer.GetAsArray1D());
            gradientKern(xBuffer.Extent.ToIntIndex(), xBuffer.View, errorBuffer.View, gradBuffer.View, m);
            // Console.WriteLine("gradBuffer");
            // print1d(gradBuffer.GetAsArray1D());
            //accelerate.Synchronize();
            thetaKern(weightsBuffer.Extent.ToIntIndex(), weightsBuffer.View, gradBuffer.View, learningRate, m);
            // Console.WriteLine("WeightsBuffer");
            // print1d(WeightsBuffer.GetAsArray1D());

        }
        _weights = weightsBuffer.GetAsArray1D();


    }

    // public void SGDfit(int epochs, int batch, double learning_rate=0.01){
    //     Accelerator accelerate = this.dev.CreateAccelerator(this.context);
    //     this.weights = new double[this.arr.GetLength(1)];
    //     //this.weights = InitializeGaussian(this.weights);
    //     //print1d(this.weights);\
    //     int m = this.arr.GetLength(0);
    //     int n = this.arr.GetLength(1);
    //     Index2D sub2d = new Index2D(batch, n);
    //     Index1D mInd = new Index1D(m);
    //     Index1D nInd = new Index1D(n);
    //     Index1D batchInd = new Index1D(batch);

    //     var XBuffer = accelerate.Allocate2DDenseX<double>(new Index2D(m,n));
    //     //var SubXBuffer = accelerate.Allocate2DDenseX<double>(sub2d);

    //     var YBuffer = accelerate.Allocate1D<double>(mInd);
    //     //var SubYBuffer = accelerate.Allocate1D<double>(batchInd);
    //     var WeightsBuffer = accelerate.Allocate1D<double>(nInd);
    //     var gradBuffer = accelerate.Allocate1D<double>(nInd);
    //     var errorBuffer = accelerate.Allocate1D<double>(mInd);
    //     //var SuberrorBuffer = accelerate.Allocate1D<double>(batchInd);

    //     Random rand = new Random();

    //     XBuffer.CopyFromCPU(this.arr);
    //     YBuffer.CopyFromCPU(this.y);
    //     WeightsBuffer.CopyFromCPU(this.weights);
    //     var setBuffToValueKern = accelerate.LoadAutoGroupedStreamKernel<
    //         Index1D,
    //         ArrayView1D<double, Stride1D.Dense>,
    //         double>(setBuffToValueKernel);
    //     var GradientKern = accelerate.LoadAutoGroupedStreamKernel<
    //         Index2D,
    //         ArrayView2D<double, Stride2D.DenseX>,
    //         ArrayView1D<double, Stride1D.Dense> ,
    //         ArrayView1D<double, Stride1D.Dense> , int
    //         >(GradKernel);
    //     var ThetaKern = accelerate.LoadAutoGroupedStreamKernel<
    //         Index1D,
    //         ArrayView1D<double, Stride1D.Dense>,
    //         ArrayView1D<double, Stride1D.Dense> ,
    //         double,
    //         int>(ThetaKernel);

    //     var errorKern = accelerate.LoadAutoGroupedStreamKernel<Index2D,
    //         ArrayView2D<double, Stride2D.DenseX>,
    //         ArrayView1D<double, Stride1D.Dense> ,
    //         ArrayView1D<double, Stride1D.Dense> ,
    //         ArrayView1D<double, Stride1D.Dense> ,
    //         int>(errorKernel);
    //     //print2d(XBuffer.View.SubView((0,0), (batch,n)).GetAsArray2D());
    //     //Console.ReadLine();

    //     for(int i = 0; i < epochs; i++){
    //         int index = rand.Next(m-batch);

    //         var SubXBuffer = XBuffer.View.SubView((index,0), (batch,n));
    //         var SuberrorBuffer = errorBuffer.View.SubView(index, batch);
    //         setBuffToValueKern(gradBuffer.Extent.ToIntIndex(), gradBuffer.View, 0.0);
    //         setBuffToValueKern(errorBuffer.Extent.ToIntIndex(), errorBuffer.View, 0.0);
    //         //errorKern(sub2d, XBuffer.View.SubView((index,0), (batch,n)), WeightsBuffer.View, errorBuffer.View.SubView(index, batch), YBuffer.View.SubView(index, batch), n);
    //         errorKern(sub2d, SubXBuffer, WeightsBuffer.View, SuberrorBuffer, YBuffer.View.SubView(index, batch), n);

    //             // Console.WriteLine("errorBuffer");
    //             // print1d(errorBuffer.GetAsArray1D());
    //         GradientKern(sub2d, SubXBuffer, SuberrorBuffer, gradBuffer.View, batch);
    //             // Console.WriteLine("gradBuffer");
    //             // print1d(gradBuffer.GetAsArray1D());
    //             //accelerate.Synchronize();
    //         ThetaKern(WeightsBuffer.Extent.ToIntIndex(), WeightsBuffer.View, gradBuffer.View, learning_rate,batch);


    //         // Console.WriteLine("WeightsBuffer");
    //         // print1d(WeightsBuffer.GetAsArray1D());

    //     }
    //     this.weights = WeightsBuffer.GetAsArray1D();


    // }
    public double[] GetWeights()
    {
        return _weights;
    }
    public double[] Predict(double[,] inputs)
    {
        var outputs = new double[inputs.GetLength(0)];
        for (var i = 0; i < inputs.GetLength(0); i++)
        {
            outputs[i] = 0;
            for (var j = 0; j < inputs.GetLength(1); j++)
            {
                outputs[i] += inputs[i, j] * _weights[j];
            }
        }
        return outputs;

    }

    /// <summary>Sets every element in buff to setValue</summary>
    /// <param name="index"></param>
    /// <param name="weightView"></param>
    /// <param name="gradView"></param>
    /// <param name="learningRate"></param>
    private static void UpdateWeights(Index1D index, ArrayView1D<double, Stride1D.Dense> weightView, ArrayView1D<double, Stride1D.Dense> gradView, double learningRate)
    {
        weightView[index] -= learningRate * gradView[index];
    }

    /// <summary>Sets every element in buff to setValue</summary>
    /// <param name="index"></param>
    /// <param name="buff">buff</param>
    /// <param name="setValue">setValue</param>
    private static void SetBuffToValueKernel(Index1D index, ArrayView1D<double, Stride1D.Dense> buff, double setValue)
    {
        buff[index] = setValue;
    }

    ///<summary> Does Matrix Multiplication on two arrayViews, and then stores in a new arrayView </summary>
    ///<param name="index">Index to iterate through the ArrayView</param>
    ///<param name="aView">1st ArrayView being multiplied</param>
    ///<param name="bView">2nd ArrayView being multiplied</param>
    ///<param name="cView">Buffer where new value goes</param>
    private static void MatrixMultiply2DKernel(Index2D index, ArrayView2D<float, Stride2D.DenseX> aView, ArrayView2D<float, Stride2D.DenseX> bView, ArrayView2D<float, Stride2D.DenseX> cView)
    {
        var x = index.X;
        var y = index.Y;
        var sum = 0.0f;
        for (var i = 0; i < aView.IntExtent.Y; i++)
            sum += aView[new Index2D(x, i)] * bView[new Index2D(i, y)];

        cView[index] = sum;
    }

    //Index3D in format (epoch, batch)
    // static void SGDKernel(Index2D index,
    //     ArrayView2D<float, Stride2D.DenseX> inputView,
    //     ArrayView1D<double, Stride1D.Dense>  weightView,


    //     ArrayView1D<double, Stride1D.Dense>  yView,


    //     double alpha,
    //     int features,
    //     int inputlength

    //     ){
    //     double slope = 0;
    //     double intercept = 0;
    //     double gradient = 0;
    //     double diff = 0;
    //     for(int i = 0; i < features; i++){
    //         diff += weightView[i] * inputView[randIndex, i];  
    //     }
    //     diff = diff - yView[randIndex];
    //     weightView[index] = weightView[index] - (alpha * );
    // }
    //Index2D format(row, columns)
    private static void GradientKernel(Index2D index,
        ArrayView2D<double, Stride2D.DenseX> inputView,
        ArrayView1D<double, Stride1D.Dense> thetaView,
        ArrayView1D<double, Stride1D.Dense> gradView,
        ArrayView1D<double, Stride1D.Dense> yView,
        int m

    )
    {

        Atomic.Add(ref gradView[index.Y], (inputView[index] * thetaView[index.Y] - yView[index.X]) * inputView[index]);
    }

    private static void ErrorKernel(Index2D index,
        ArrayView2D<double, Stride2D.DenseX> inputView,
        ArrayView1D<double, Stride1D.Dense> thetaView,
        ArrayView1D<double, Stride1D.Dense> errorView,
        ArrayView1D<double, Stride1D.Dense> yView,
        int n

    )
    {

        Atomic.Add(ref errorView[index.X], inputView[index] * thetaView[index.Y] - yView[index.X] / n);
    }

    private static void GradKernel(Index2D index,
        ArrayView2D<double, Stride2D.DenseX> inputView,
        ArrayView1D<double, Stride1D.Dense> errorView,
        ArrayView1D<double, Stride1D.Dense> gradView,
        int m

    )
    {

        Atomic.Add(ref gradView[index.Y], errorView[index.X] * inputView[index] / m);
    }

    private static void ThetaKernel(Index1D index,
        ArrayView1D<double, Stride1D.Dense> thetaView,
        ArrayView1D<double, Stride1D.Dense> gradView,
        double alpha,
        int ylength)
    {
        Atomic.Add(ref thetaView[index], -1.0 * (alpha * gradView[index]));

    }

    private void Print1d(double[] array)
    {
        Console.Write("[");
        for (var j = 0; j < array.GetLength(0); j++)
        {
            Console.Write("{0}, ", array[j]);
        }
        Console.WriteLine("]");

    }

    private void Print2d(double[,] array)
    {
        Console.WriteLine(array);

        for (var i = 0; i < array.GetLength(0); i++)
        {
            Console.Write("[");
            for (var j = 0; j < array.GetLength(1); j++)
            {
                Console.Write("{0}, ", array[i, j]);
            }
            Console.Write("]");
            Console.WriteLine(", ");
        }
        Console.WriteLine("]");
    }

    public static double[] InitializeGaussian(double[] weights)
    {
        var random = new Random();
        var mean = 0.0;
        var stdDev = 0.01;
        for (var i = 0; i < weights.GetLength(0); i++)
        {
            var u1 = 1.0 - random.NextDouble(); // uniform(0,1] random doubles
            var u2 = 1.0 - random.NextDouble();
            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                Math.Sin(2.0 * Math.PI * u2); // random normal(0,1)
            var randGaussian = mean + stdDev * randStdNormal; // random normal(mean,stdDev^2)
            weights[i] = randGaussian;
        }
        return weights;
    }

    public double CompareWeights(double[] w1, double[] w2)
    {
        var sum = 0.0;
        for (var i = 0; i < w1.GetLength(0); i++)
        {
            sum += Math.Abs(w1[i] - w2[i]);
        }
        return sum / w1.GetLength(0);

    }


}