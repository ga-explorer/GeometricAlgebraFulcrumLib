using System.Collections.Generic;
using System.Linq;
using TextComposerLib.Text;

namespace GraphicsComposerLib.Svg.Transforms
{
    public sealed class SvgTransformComposer : SvgTransform
    {
        public static SvgTransformComposer Create()
        {
            return new SvgTransformComposer();
        }

        public static SvgTransformComposer Create(IEnumerable<SvgTransform> transformsList)
        {
            return new SvgTransformComposer(transformsList);
        }

        public static SvgTransformComposer Create(params SvgTransform[] transformsList)
        {
            return new SvgTransformComposer(transformsList);
        }



        private readonly List<SvgTransform> _transformsList
            = new List<SvgTransform>();

        public override string ValueText
            => _transformsList.Select(t => t.ValueText).Concatenate(" ");


        public SvgTransform this[int index]
        {
            get { return _transformsList[index]; }
            set { _transformsList[index] = value; }
        }

        public IEnumerable<SvgTransform> Transforms 
            => _transformsList;


        public SvgTransformComposer()
        {
        }

        public SvgTransformComposer(IEnumerable<SvgTransform> transformsList)
        {
            _transformsList.AddRange(transformsList);
        }

        public SvgTransformComposer(params SvgTransform[] transformsList)
        {
            _transformsList.AddRange(transformsList);
        }


        public SvgTransformComposer Clear()
        {
            _transformsList.Clear();

            return this;
        }

        public SvgTransformComposer Append(SvgTransform transform)
        {
            _transformsList.Add(transform);

            return this;
        }

        public SvgTransformComposer AppendRange(IEnumerable<SvgTransform> transformsList)
        {
            _transformsList.AddRange(transformsList);

            return this;
        }

        public SvgTransformComposer AppendRange(params SvgTransform[] transformsList)
        {
            _transformsList.AddRange(transformsList);

            return this;
        }

        public SvgTransformComposer AppendMatrix(double[,] matrix)
        {
            _transformsList.Add(SvgTransformMatrix.Create(matrix));

            return this;
        }

        public SvgTransformComposer AppendTranslate(double tx, double ty = 0.0d)
        {
            _transformsList.Add(SvgTransformTranslate.Create(tx, ty));

            return this;
        }

        public SvgTransformComposer AppendScale(double sx, double sy = 1.0d)
        {
            _transformsList.Add(SvgTransformScale.Create(sx, sy));

            return this;
        }

        public SvgTransformComposer AppendRotate(double angle, double cx = 0.0d, double cy = 0.0d)
        {
            _transformsList.Add(SvgTransformRotate.Create(angle, cx, cy));

            return this;
        }

        public SvgTransformComposer AppendSkewX(double angle)
        {
            _transformsList.Add(SvgTransformSkewX.Create(angle));

            return this;
        }

        public SvgTransformComposer AppendSkewY(double angle)
        {
            _transformsList.Add(SvgTransformSkewY.Create(angle));

            return this;
        }

        public SvgTransformComposer Prepend(SvgTransform transform)
        {
            _transformsList.Insert(0, transform);

            return this;
        }

        public SvgTransformComposer PrependRange(IEnumerable<SvgTransform> transformsList)
        {
            _transformsList.InsertRange(0, transformsList);

            return this;
        }

        public SvgTransformComposer PrependRange(params SvgTransform[] transformsList)
        {
            _transformsList.InsertRange(0, transformsList);

            return this;
        }

        public SvgTransformComposer PrependMatrix(double[,] matrix)
        {
            _transformsList.Insert(0, SvgTransformMatrix.Create(matrix));

            return this;
        }

        public SvgTransformComposer PrependTranslate(double tx, double ty = 0.0d)
        {
            _transformsList.Insert(0, SvgTransformTranslate.Create(tx, ty));

            return this;
        }

        public SvgTransformComposer PrependScale(double sx, double sy = 1.0d)
        {
            _transformsList.Insert(0, SvgTransformScale.Create(sx, sy));

            return this;
        }

        public SvgTransformComposer PrependRotate(double angle, double cx = 0.0d, double cy = 0.0d)
        {
            _transformsList.Insert(0, SvgTransformRotate.Create(angle, cx, cy));

            return this;
        }

        public SvgTransformComposer PrependSkewX(double angle)
        {
            _transformsList.Insert(0, SvgTransformSkewX.Create(angle));

            return this;
        }

        public SvgTransformComposer PrependSkewY(double angle)
        {
            _transformsList.Insert(0, SvgTransformSkewY.Create(angle));

            return this;
        }
    }
}
