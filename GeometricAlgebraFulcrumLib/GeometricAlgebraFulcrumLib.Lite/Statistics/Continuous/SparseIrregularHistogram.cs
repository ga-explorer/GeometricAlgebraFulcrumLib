using System.Collections;

namespace GeometricAlgebraFulcrumLib.Lite.Statistics.Continuous
{
    public class SparseIrregularHistogram :
        IReadOnlyList<HistogramBinData>
    {
        public static SparseIrregularHistogram Create()
        {
            return new SparseIrregularHistogram();
        }


        private List<HistogramBinData> BinList { get; set; }

        public double DomainMinValue 
            => BinList.Min(b => b.MinValue);

        public double DomainMaxValue 
            => BinList.Max(b => b.MaxValue);
        
        public int Count 
            => BinList.Count;

        public HistogramBinData this[int index] 
            => BinList[index];


        private SparseIrregularHistogram()
        {
            BinList = new List<HistogramBinData>();
        }


        public SparseIrregularHistogram AddBin(double midValue, double width, double height)
        {
            if (height > 0)
                BinList.Add(
                    new HistogramBinData(midValue, width, height)
                );

            return this;
        }

        public IEnumerable<HistogramBinData> GetBinsContaining(double domainValue)
        {
            return BinList.Where(b => b.Contains(domainValue));
        }

        public double GetArea()
        {
            return BinList.Select(b => b.Area).Sum();
        }

        public double GetAreaBefore(double domainValue)
        {
            return BinList.Where(bin => 
                    bin.Contains(domainValue) ||
                    bin.MaxValue <= domainValue
                ).Sum(bin => bin.GetAreaBefore(domainValue));
        }
        
        public double GetAreaAfter(double domainValue)
        {
            return BinList.Where(bin => 
                bin.Contains(domainValue) ||
                bin.MinValue >= domainValue
            ).Sum(bin => bin.GetAreaAfter(domainValue));
        }

        public double GetAreaBetween(double domainValue1, double domainValue2)
        {
            var area2 = GetAreaBefore(domainValue2); 
            var area1 = GetAreaBefore(domainValue1);

            return Math.Abs(area2 - area1);
        }

        public IEnumerator<HistogramBinData> GetEnumerator()
        {
            return BinList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
