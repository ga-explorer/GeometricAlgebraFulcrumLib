using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GeometricAlgebraFulcrumLib.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            //var testClass = new BasisBladeTests();
            //testClass.TestGpReverse();

            //var testClass = new MultivectorStoragesTests();
            //testClass.ClassInit();

            //testClass.AssertCorrectInitialization();

            //testClass.AssertBinaryWithSelfOperations();

            //var functionNames = new[] { "add", "subtract", "op", "gp", "lcp", "rcp", "fdp", "hip", "cp", "acp" };
            //foreach (var functionName in functionNames)
            //    testClass.AssertCorrectBinaryOperations(functionName);

            //var functionNames = new[] { "leftTimesScalar", "rightTimesScalar", "divideByScalar", "gpSquared", "gpReverse" };
            //foreach (var functionName in functionNames)
            //    testClass.AssertCorrectUnaryOperations(functionName);

            //var functionNames = new[] { "spSquared", "spReverse" };
            //foreach (var functionName in functionNames)
            //    testClass.AssertCorrectUnaryOperationsWithScalarOutput(functionName);

            //GeometricAlgebraFulcrumLib.Samples.CodeComposer.Sample1.Execute2();
            //EuclideanMultivectorOperations3D.Execute();

            //HilbertTransform.Execute();

            //MultiDerivativeSample.Execute();

            //Symbolic.AngouriMath.Sample1.Execute();
            //CodeComposer.Sample1.Execute();

            //Symbolic.SymbolicRotorsSample.Example11();
            //Graphics.Basic.QuaternionRotationSample.Example4();
            //Graphics.BabylonJs.Sample1.Example2();
            //Graphics.Animations.Sample1.Example2();
            //BabylonJsSnapshotsSample.TakeSnapshots();
            //Browser.Sample1.Example5();

            //{
            //    var chromeOptions = new ChromeOptions
            //    {
            //        PageLoadStrategy = PageLoadStrategy.Normal,
            //        UnhandledPromptBehavior = UnhandledPromptBehavior.Accept
            //    };

            //    chromeOptions.AddUserProfilePreference("download.default_directory", @"D:\Projects\Study\Babylon.js");
            //    chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            //    chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");

            //    //chromeOptions.AddAdditionalChromeOption("window-size", "1920,1080");
            //    //chromeOptions.AddArgument("headless");

            //    var driver = new ChromeDriver(chromeOptions);

            //    driver.Manage().Window.Position = new System.Drawing.Point(0, 0);
            //    driver.Manage().Window.Maximize();
            //    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);

            //    driver.Navigate().GoToUrl(@$"http://localhost:5200/Frame-{0:D6}.html");
            //    Thread.Sleep(2000);

            //    Console.WriteLine("Press key to close browser");
            //    Console.ReadKey();

            //    driver.Quit();
            //}

            //Numeric.InterpolationSample.Example6();
            GeometricFrequency.PowerSignalVisualizationSample2.Execute();

            //Symbolic.SymbolicGramSchmidtSample.Example1();

            //GeometricFrequency.NumericGeometricFrequencySample.Example2();
            //GeometricFrequency.SymbolicAngularVelocitySample.Example1();
            //GeometricFrequency.NumericGeometricFrequencySample.SymbolicNumericValidationExample2();
            //GeometricFrequency.SymbolicGeometricFrequencySample.Example1();
            //GeometricFrequency.SymbolicHarmonicsSample.Example2();
            //GeometricFrequency.LargeDataSetSample.GenerateDownSampledSignal(8);
            //GeometricFrequency.LargeDataSetSample.PlotData();
            //GeometricFrequency.RlCircuitPaperSample.Example4();
            //GeometricFrequency.SymbolicValidationSample.Example3D();
            //GeometricFrequency.NumericValidationSample.Example1();

            //SymbolicBSplineSample.Example2();
            //NumericPhCurveSamples.Example3();
            //SymbolicBernsteinPolynomialsSample.Example5();
            //SymbolicPhConstruction3DSample.Example2();
            //SymbolicPhConstruction2DSample.Example2();
            //SymbolicRotorsSample.Example9();
            //NumericRotorsSample.Example12();
            //SymbolicScaledRotors3DSample.Example3();
            //SymbolicScaledRotors2DSample.Example3();
            //NumericPhSplineCurveSamples.Example1();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit..");
            Console.ReadKey();
        }
    }
}
