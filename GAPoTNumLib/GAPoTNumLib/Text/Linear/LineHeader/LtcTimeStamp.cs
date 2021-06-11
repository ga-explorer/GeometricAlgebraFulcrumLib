using System;
using System.Globalization;

namespace GAPoTNumLib.Text.Linear.LineHeader
{
    public sealed class LtcTimeStamp : LtcLineHeader
    {
        public string FormatString { get; } = "hh:mm:ss.fffffff";


        public LtcTimeStamp()
        {
        }

        public LtcTimeStamp(string formatString)
        {
            FormatString = formatString;
        }


        public override void Reset()
        {
        }

        public override string GetHeaderText()
        {
            return 
                string.IsNullOrEmpty(FormatString) 
                ? DateTime.Now.ToString(CultureInfo.InvariantCulture) 
                : DateTime.Now.ToString(FormatString);
        }
    }
}
