using System.Collections;

namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems.PowerQuality;

public class PqSignal : 
    IReadOnlyList<PqSignalSegment>
{
    public static PqSignal Create(double nominalVoltage, double frequencyHz, int samplingRate)
    {
        return new PqSignal(
            nominalVoltage, 
            frequencyHz, 
            samplingRate
        );
    }


    private readonly List<PqSignalSegment> _segmentList 
        = new List<PqSignalSegment>();


    public double NominalVoltage { get; }
    
    public double FrequencyHz { get; }

    public int SamplingRate { get; }

    public double Frequency 
        => Math.Tau * FrequencyHz;

    public double NormalizationFactor 
        => 3 * NominalVoltage * NominalVoltage * FrequencyHz;

    public int Count 
        => _segmentList.Count;

    public PqSignalSegment this[int index] 
        => _segmentList[index];


    private PqSignal(double nominalVoltage, double frequencyHz, int samplingRate)
    {
        NominalVoltage = nominalVoltage;
        FrequencyHz = frequencyHz;
        SamplingRate = samplingRate;
    }


    public PqSignal ClearSegments()
    {
        _segmentList.Clear();

        return this;
    }

    public double GetSegmentStartTime(int index)
    {
        if (index < 0 || index >= _segmentList.Count)
            throw new IndexOutOfRangeException();

        return index == 0 
            ? 0 
            : _segmentList.Take(index).Select(s => s.SegmentDuration).Sum();
    }

    public IEnumerator<PqSignalSegment> GetEnumerator()
    {
        return _segmentList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    
}