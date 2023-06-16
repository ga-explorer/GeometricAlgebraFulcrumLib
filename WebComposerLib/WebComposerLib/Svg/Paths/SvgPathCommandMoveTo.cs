using System.Text;
using DataStructuresLib.Basic;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Paths
{
    public sealed class SvgPathCommandMoveTo : SvgPathCommand
    {
        public static SvgPathCommandMoveTo Create()
        {
            return new SvgPathCommandMoveTo();
        }

        public static SvgPathCommandMoveTo Create(bool isRelative)
        {
            return new SvgPathCommandMoveTo() { IsRelative = isRelative };
        }

        public static SvgPathCommandMoveTo Create(bool isRelative, SvgValueLengthUnit unit)
        {
            var command = new SvgPathCommandMoveTo { IsRelative = isRelative };

            command.Points.Unit = unit;

            return command;
        }

        public static SvgPathCommandMoveTo Create(double x, double y)
        {
            var command = new SvgPathCommandMoveTo();

            command.Points.AddPoint(x, y);

            return command;
        }

        public static SvgPathCommandMoveTo Create(bool isRelative, double x, double y)
        {
            var command = new SvgPathCommandMoveTo { IsRelative = isRelative };

            command.Points.AddPoint(x, y);

            return command;
        }

        public static SvgPathCommandMoveTo Create(bool isRelative, SvgValueLengthUnit unit, double x, double y)
        {
            var command = new SvgPathCommandMoveTo { IsRelative = isRelative };

            command.Points.Unit = unit;
            command.Points.AddPoint(x, y);

            return command;
        }

        public static SvgPathCommandMoveTo Create(IEnumerable<IPair<double>> points)
        {
            var command = new SvgPathCommandMoveTo();

            command.Points.AddPoints(points);

            return command;
        }

        public static SvgPathCommandMoveTo Create(bool isRelative, IEnumerable<IPair<double>> points)
        {
            var command = new SvgPathCommandMoveTo { IsRelative = isRelative };

            command.Points.AddPoints(points);

            return command;
        }

        public static SvgPathCommandMoveTo Create(bool isRelative, SvgValueLengthUnit unit, IEnumerable<IPair<double>> points)
        {
            var command = new SvgPathCommandMoveTo { IsRelative = isRelative };

            command.Points.Unit = unit;
            command.Points.AddPoints(points);

            return command;
        }

        public static SvgPathCommandMoveTo Create(params IPair<double>[] points)
        {
            var command = new SvgPathCommandMoveTo();

            command.Points.AddPoints(points);

            return command;
        }

        public static SvgPathCommandMoveTo Create(bool isRelative, params IPair<double>[] points)
        {
            var command = new SvgPathCommandMoveTo { IsRelative = isRelative };

            command.Points.AddPoints(points);

            return command;
        }

        public static SvgPathCommandMoveTo Create(bool isRelative, SvgValueLengthUnit unit, params IPair<double>[] points)
        {
            var command = new SvgPathCommandMoveTo { IsRelative = isRelative };

            command.Points.Unit = unit;
            command.Points.AddPoints(points);

            return command;
        }

        public static SvgPathCommandMoveTo Create(params double[] points)
        {
            var command = new SvgPathCommandMoveTo();

            command.Points.AddPoints(points);

            return command;
        }

        public static SvgPathCommandMoveTo Create(bool isRelative, params double[] points)
        {
            var command = new SvgPathCommandMoveTo { IsRelative = isRelative };

            command.Points.AddPoints(points);

            return command;
        }

        public static SvgPathCommandMoveTo Create(bool isRelative, SvgValueLengthUnit unit, params double[] points)
        {
            var command = new SvgPathCommandMoveTo { IsRelative = isRelative };

            command.Points.Unit = unit;
            command.Points.AddPoints(points);

            return command;
        }


        public bool IsRelative { get; set; }

        public bool IsAbsolute
        {
            get => !IsRelative;
            set => IsRelative = !value;
        }

        public SvgValuePointsList Points { get; } 
            = SvgValuePointsList.Create();

        public override string ValueText
        {
            get
            {
                var composer = new StringBuilder();

                composer
                    .Append(IsRelative ? "m " : "M ")
                    .Append(Points.ValueText);

                return composer.ToString();
            }
        }


        private SvgPathCommandMoveTo()
        {
        }
    }
}