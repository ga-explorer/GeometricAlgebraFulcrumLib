using System.Linq;

namespace GraphicsComposerLib.GraphViz.LaTeX.Documents
{
    public sealed class LaTeXDocument : LaTeXEnvironment
    {
        public LaTeXDocumentTopMatter TopMatter
        {
            get
            {
                var result = 
                    SubSectionsList.FirstOrDefault(c => c.IsTopMatter()) as LaTeXDocumentTopMatter;

                if (!ReferenceEquals(result, null))
                    return result;

                result = new LaTeXDocumentTopMatter();

                SubSectionsList.Add(result);

                return result;
            }
        }

        public LaTeXDocumentAbstract Abstract
        {
            get
            {
                var result =
                    SubSectionsList.FirstOrDefault(c => c.IsAbstract()) as LaTeXDocumentAbstract;

                if (!ReferenceEquals(result, null))
                    return result;

                result = new LaTeXDocumentAbstract();

                SubSectionsList.Add(result);

                return result;
            }
        }

        public LaTeXDocumentMatter FrontMatter
        {
            get
            {
                var result =
                    SubSectionsList.FirstOrDefault(c => c.IsFrontMatter()) as LaTeXDocumentMatter;

                if (!ReferenceEquals(result, null))
                    return result;

                result = LaTeXDocumentMatter.CreateFrontMatter();

                SubSectionsList.Add(result);

                return result;
            }
        }

        public LaTeXDocumentMatter MainMatter
        {
            get
            {
                var result =
                    SubSectionsList.FirstOrDefault(c => c.IsMainMatter()) as LaTeXDocumentMatter;

                if (!ReferenceEquals(result, null))
                    return result;

                result = LaTeXDocumentMatter.CreateMainMatter();

                SubSectionsList.Add(result);

                return result;
            }
        }

        public LaTeXDocumentMatter Appendix
        {
            get
            {
                var result =
                    SubSectionsList.FirstOrDefault(c => c.IsAppendix()) as LaTeXDocumentMatter;

                if (!ReferenceEquals(result, null))
                    return result;

                result = LaTeXDocumentMatter.CreateAppendix();

                SubSectionsList.Add(result);

                return result;
            }
        }

        public LaTeXDocumentMatter BackMatter
        {
            get
            {
                var result =
                    SubSectionsList.FirstOrDefault(c => c.IsBackMatter()) as LaTeXDocumentMatter;

                if (!ReferenceEquals(result, null))
                    return result;

                result = LaTeXDocumentMatter.CreateBackMatter();

                SubSectionsList.Add(result);

                return result;
            }
        }

        public bool ContainsAbstract
            => SubSectionsList.Any(c => c.IsAbstract());

        public bool ContainsTopMatter
            => SubSectionsList.Any(c => c is LaTeXDocumentTopMatter);

        public bool ContainsFrontMatter
            => SubSectionsList.Any(c => c.IsFrontMatter());

        public bool ContainsMainMatter
            => SubSectionsList.Any(c => c.IsMainMatter());

        public bool ContainsAppendix
            => SubSectionsList.Any(c => c.IsAppendix());

        public bool ContainsBackMatter
            => SubSectionsList.Any(c => c.IsBackMatter());


        public LaTeXDocument Clear()
        {
            TopMatter.Clear();

            return this;
        }

        public LaTeXDocument RemoveTopMatter()
        {
            var index = SubSectionsList.FindIndex(c => c.IsTopMatter());

            if (index < 0)
                return this;

            SubSectionsList.Remove(index);

            return this;
        }

        public LaTeXDocument RemoveAbstract()
        {
            var index = SubSectionsList.FindIndex(c => c.IsAbstract());

            if (index < 0)
                return this;

            SubSectionsList.Remove(index);

            return this;
        }

        public LaTeXDocument RemoveFrontMatter()
        {
            var index = SubSectionsList.FindIndex(c => c.IsFrontMatter());

            if (index < 0)
                return this;

            SubSectionsList.Remove(index);

            return this;
        }

        public LaTeXDocument RemoveMainMatter()
        {
            var index = SubSectionsList.FindIndex(c => c.IsMainMatter());

            if (index < 0)
                return this;

            SubSectionsList.Remove(index);

            return this;
        }

        public LaTeXDocument RemoveAppendix()
        {
            var index = SubSectionsList.FindIndex(c => c.IsAppendix());

            if (index < 0)
                return this;

            SubSectionsList.Remove(index);

            return this;
        }

        public LaTeXDocument RemoveBackMatter()
        {
            var index = SubSectionsList.FindIndex(c => c.IsBackMatter());

            if (index < 0)
                return this;

            SubSectionsList.Remove(index);

            return this;
        }
    }
}