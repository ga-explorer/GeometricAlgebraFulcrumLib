using System;
using System.ComponentModel;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Random;

/// <summary>
/// Implementation is based on paper:
/// "Computer simulation of Lévy α-stable variables and processes"
/// </summary>
public sealed class StableDistributionRandomGenerator : 
    global::System.Random
{
    public global::System.Random UniformRandomGenerator { get; }

    /// <summary>
    /// The characteristic exponent alpha lies in the range (0, 2] and determines
    /// the rate at which the tails of the distribution taper off. When alpha = 2,
    /// a Gaussian distribution results, with mean mu and variance 2 sigma squared.
    /// When alpha is less than 2, the variance is infinite. When alpha is more than 1,
    /// the mean of the distribution exists and is equal to mu.
    /// </summary>
    public double Alpha { get; private set; } 
        = 2d;

    /// <summary>
    /// The parameter beta determines the skewness of the distribution and lies in the
    /// range [-1, 1]. When beta is positive, the distribution is skewed to the right.
    /// When beta is negative, it is skewed to the left. When beta = 0, the distribution
    /// is symmetrical. As alpha approaches 2, beta loses its effect and the distribution
    /// approaches the symmetrical Gaussian distribution regardless of beta.
    /// </summary>
    public double Beta { get; private set; } 
        = 0d;

    /// <summary>
    /// The scale parameter sigma compresses or extends the distribution about mu
    /// </summary>
    public double Sigma { get; private set; } 
        = 1d;

    /// <summary>
    /// The location parameter mu shifts the distribution to the left or right
    /// </summary>
    public double Mu { get; private set; } 
        = 0d;

    public bool IsStandard
        => Sigma == 1d && Mu == 0d;

    public bool IsSymmetric
        => Beta == 0d;


    public StableDistributionRandomGenerator(global::System.Random uniformRandomGenerator)
    {
        UniformRandomGenerator = uniformRandomGenerator;
    }


    public void SetAsGaussian(double mean = 0d, double standardDeviation = 1d)
    {
        SetParameters(2d, standardDeviation / Math.Sqrt(2), 0d, mean);
    }

    public void SetAsCauchy(double peakLocation = 0d, double scale = 1d)
    {
        SetParameters(1d, scale, 0d, peakLocation);
    }

    public void SetAsPositiveLevy(double peakLocation = 0d, double scale = 1d)
    {
        SetParameters(0.5d, scale, 1d, peakLocation);
    }

    public void SetAsNegativeLevy(double peakLocation = 0d, double scale = 1d)
    {
        SetParameters(0.5d, scale, -1d, peakLocation);
    }

    public void SetParameters(double alpha, double sigma, double beta, double mu)
    {
        if (alpha <= 0d || alpha > 2d)
            throw new InvalidEnumArgumentException(nameof(alpha));

        if (beta < -1d || beta > 1d)
            throw new InvalidEnumArgumentException(nameof(beta));

        if (sigma <= 0d)
            throw new InvalidEnumArgumentException(nameof(sigma));

        Alpha = alpha;
        Beta = beta;
        Sigma = sigma;
        Mu = mu;
    }

    public double GenerateStandardValue()
    {
        //Generate a uniformly distributed random value in the range (-pi / 2, pi / 2)
        var v = 0.5d * (UniformRandomGenerator.NextDouble() * 2d - 1d) * Math.PI;

        //Generate an exponentially distributed random value with mean 1
        var w = -Math.Log(UniformRandomGenerator.NextDouble());

        var invAlpha = 1d / Alpha;

        if (Beta == 0d)
            return Math.Sin(Alpha * v) / Math.Pow(Math.Cos(v), invAlpha) *
                   Math.Pow(Math.Cos((1d - Alpha) * v) / w, invAlpha - 1d);
            
        var halfPi = 0.5d * Math.PI;

        if (Alpha == 1d)
        {
            var v1 = halfPi + Beta * v;

            return v1 * Math.Tan(v) - Beta * Math.Log10(halfPi * w * Math.Cos(v) / v1);
        }

        var t = Math.Tan(halfPi * Alpha);
        var b = invAlpha * Math.Atan(Beta * t);

        return
            Math.Pow(1d + Beta * Beta * t * t, 0.5d * invAlpha) * 
            Math.Sin(Alpha * (v + b)) / 
            Math.Pow(Math.Cos(v), invAlpha) *
            Math.Pow(Math.Cos(v - Alpha * (v + b)) / w, invAlpha - 1d);
    }

    public override double NextDouble()
    {
        var x = GenerateStandardValue();

        if (IsStandard)
            return x;

        x = Sigma * x + Mu;

        if (Alpha == 1d)
            x += 2 / Math.PI * Beta * Sigma * Math.Log10(Sigma);

        return x;
    }
}