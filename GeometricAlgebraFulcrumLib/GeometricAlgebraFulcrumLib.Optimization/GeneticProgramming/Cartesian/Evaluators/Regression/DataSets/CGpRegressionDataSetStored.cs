using System.Collections.Immutable;
using System.Diagnostics;

namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Regression.DataSets;

public class CGpRegressionDataSetStored :
    CGpRegressionDataSet
{
    public static CGpRegressionDataSetStored Create(int inputSize, int outputSize)
    {
        return new CGpRegressionDataSetStored(inputSize, outputSize);
    }

    public static CGpRegressionDataSetStored CreateFromFile(string filePath)
    {
        var textLines =
            File.ReadAllLines(filePath)
                .Select(t => t.Trim())
                .Where(t => !string.IsNullOrEmpty(t))
                .ToImmutableArray();

        var firstLineData =
            textLines[0].Split(
                ",",
                StringSplitOptions.RemoveEmptyEntries
            ).Select(int.Parse).ToImmutableArray();

        var inputSize = firstLineData[0];
        var outputSize = firstLineData[1];
        var sampleCount = firstLineData[2];

        var dataSet = new CGpRegressionDataSetStored(inputSize, outputSize);

        for (var i = 1; i < textLines.Length; i++)
        {
            var dataValues =
                textLines[i].Split(
                    ",",
                    StringSplitOptions.RemoveEmptyEntries
                ).Select(double.Parse).ToImmutableArray();

            var input = dataValues[0..inputSize];
            var output = dataValues[inputSize..(inputSize + outputSize)];

            dataSet.AddSample(input, output);
        }

        Debug.Assert(sampleCount == dataSet.Count);

        return dataSet;
    }

    //public static CGpDataSet CreateFromFile(string file)
    //{
    //    string line;
    //    var buffer = new string(new char[8192]);
    //    var lineNum = -1;

    //    /* attempt to open the given file */
    //    FILE fp = fopen(file, "r");

    //    /* if the file cannot be found */
    //    if (fp == null)
    //    {
    //        Console.WriteLine("Error: file '%s' cannot be found.\nTerminating CGP-Library.\n", file);
    //        return;
    //    }

    //    /* initialise memory for data structure */
    //    var data = new CGpDataSet();

    //    /* for every line the given file */
    //    while ((line = fgets(buffer, sizeof(char), fp)) != null)
    //    {

    //        /* deal with the first line containing meta data */
    //        if (lineNum == -1)
    //        {

    //            sscanf(line, "%d,%d,%d", data.InputCount, data.OutputCount, data.SampleCount);

    //            data.InputData = new double[data.SampleCount];
    //            data.OutputData = new double[data.SampleCount];

    //            for (var i = 0; i < data.SampleCount; i++)
    //            {
    //                // C++ TO C# CONVERTER TASK: The memory management function 'malloc' has no equivalent C#:
    //                data.InputData[i] = (double)malloc(data.InputCount * sizeof(double));
    //                // C++ TO C# CONVERTER TASK: The memory management function 'malloc' has no equivalent C#:
    //                data.OutputData[i] = (double)malloc(data.OutputCount * sizeof(double));
    //            }
    //        }
    //        /* the other lines contain input output pairs */
    //        else
    //        {
    //            /* get the first value on the given line */
    //            string record = strtok(line, " ,\n");
    //            var col = 0;

    //            /* until end of line */
    //            while (record != null)
    //            {

    //                /* if its an input value */
    //                if (col < data.InputCount)
    //                {
    //                    data.InputData[lineNum][col] = atof(record);
    //                }

    //                /* if its an output value */
    //                else
    //                {

    //                    data.OutputData[lineNum][col - data.InputCount] = atof(record);
    //                }

    //                /* get the next value on the given line */
    //                record = strtok(null, " ,\n");

    //                /* increment the current col index */
    //                col++;
    //            }
    //        }

    //        /* increment the current line index */
    //        lineNum++;
    //    }

    //    fclose(fp);

    //    return data;
    //}


    private readonly List<CGpRegressionDataSetSample> _sampleList
        = new List<CGpRegressionDataSetSample>();

    public override int SampleCount
        => _sampleList.Count;


    private CGpRegressionDataSetStored(int inputSize, int outputSize)
        : base(inputSize, outputSize)
    {
    }


    public CGpRegressionDataSetStored Clear()
    {
        _sampleList.Clear();

        return this;
    }

    public override CGpRegressionDataSetSample GetSample(int sampleIndex)
    {
        return _sampleList[sampleIndex];
    }

    public CGpRegressionDataSetStored SetSample(int sampleIndex, IReadOnlyList<double> sampleInput, IReadOnlyList<double> sampleOutput)
    {
        if (sampleInput.Count != InputSize)
            throw new ArgumentException(nameof(sampleInput));

        if (sampleOutput.Count != OutputSize)
            throw new ArgumentException(nameof(sampleOutput));

        _sampleList[sampleIndex] = new CGpRegressionDataSetSample(sampleInput, sampleOutput);

        return this;
    }

    public CGpRegressionDataSetStored AddSample(IReadOnlyList<double> sampleInput, IReadOnlyList<double> sampleOutput)
    {
        if (sampleInput.Count != InputSize)
            throw new ArgumentException(nameof(sampleInput));

        if (sampleOutput.Count != OutputSize)
            throw new ArgumentException(nameof(sampleOutput));

        _sampleList.Add(
            new CGpRegressionDataSetSample(sampleInput, sampleOutput)
        );

        return this;
    }


    //public void SaveDataSet(string fileName)
    //{
    //    int j;

    //    FILE fp = fopen(fileName, "w");

    //    if (fp == null)
    //    {
    //        Console.WriteLine("Warning: cannot save data set to %s. Data set was not saved.\n", fileName);
    //        return;
    //    }

    //    fprintf(fp, "%d,", InputCount);
    //    fprintf(fp, "%d,", OutputCount);
    //    fprintf(fp, "%d,", SampleCount);
    //    fprintf(fp, "\n");


    //    for (var i = 0; i < SampleCount; i++)
    //    {

    //        for (j = 0; j < InputCount; j++)
    //        {
    //            fprintf(fp, "%f,", InputData[i][j]);
    //        }

    //        for (j = 0; j < OutputCount; j++)
    //        {
    //            fprintf(fp, "%f,", OutputData[i][j]);
    //        }

    //        fprintf(fp, "\n");
    //    }

    //    fclose(fp);
    //}

    public override IEnumerator<CGpRegressionDataSetSample> GetEnumerator()
    {
        return _sampleList.GetEnumerator();
    }
}