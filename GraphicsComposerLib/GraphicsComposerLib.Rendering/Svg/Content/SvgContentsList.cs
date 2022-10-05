using System.Collections;
using GraphicsComposerLib.Rendering.Svg.Elements;
using GraphicsComposerLib.Rendering.Svg.Elements.Descriptive;
using TextComposerLib.Text;

namespace GraphicsComposerLib.Rendering.Svg.Content
{
    public sealed class SvgContentsList : IReadOnlyCollection<ISvgContent>
    {
        private readonly List<ISvgContent> _contentsList;


        public int Count
            => _contentsList.Count;


        public IEnumerable<SvgElement> GetChildElements()
        {
            return _contentsList
                .Select(c => c as SvgElement)
                .Where(c => !ReferenceEquals(c, null));
        }

        public SvgElement GetChildElement(string elementId)
        {
            return _contentsList
                .Select(c => c as SvgElement)
                .FirstOrDefault(c => !ReferenceEquals(c, null) && c.Id == elementId);
        }

        /// <summary>
        /// Obtain a list of descendant SVG elements
        /// </summary>
        /// <param name="depthFirst"></param>
        /// <returns></returns>
        public IEnumerable<SvgElement> GetDescendantElements(bool depthFirst = true)
        {

            if (depthFirst)
            {
                var elementsStack = new Stack<SvgElement>();
                foreach (var childElement in GetChildElements())
                    elementsStack.Push(childElement);

                while (elementsStack.Count > 0)
                {
                    var element = elementsStack.Pop();

                    yield return element;

                    foreach (var childElement in element.Contents.GetChildElements())
                        elementsStack.Push(childElement);
                }
            }

            var elementsQueue = new Queue<SvgElement>();
            foreach (var childElement in GetChildElements())
                elementsQueue.Enqueue(childElement);

            while (elementsQueue.Count > 0)
            {
                var element = elementsQueue.Dequeue();

                yield return element;

                foreach (var childElement in element.Contents.GetChildElements())
                    elementsQueue.Enqueue(childElement);
            }
        }

        /// <summary>
        /// Obtain the first descendant SVG element with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="depthFirst"></param>
        /// <returns></returns>
        public SvgElement GetDescendantElement(string id, bool depthFirst = true)
        {

            if (depthFirst)
            {
                var elementsStack = new Stack<SvgElement>();
                foreach (var childElement in GetChildElements())
                    if (childElement.Id == id)
                        return childElement;
                    else
                        elementsStack.Push(childElement);

                while (elementsStack.Count > 0)
                {
                    var element = elementsStack.Pop();
                    foreach (var childElement in element.Contents.GetChildElements())
                        if (childElement.Id == id)
                            return childElement;
                        else
                            elementsStack.Push(childElement);
                }

                return null;
            }

            var elementsQueue = new Queue<SvgElement>();
            foreach (var childElement in GetChildElements())
                if (childElement.Id == id)
                    return childElement;
                else
                    elementsQueue.Enqueue(childElement);

            while (elementsQueue.Count > 0)
            {
                var element = elementsQueue.Dequeue();
                foreach (var childElement in element.Contents.GetChildElements())
                    if (childElement.Id == id)
                        return childElement;
                    else
                        elementsQueue.Enqueue(childElement);
            }

            return null;
        }

        public SvgContentsList()
        {
            _contentsList = new List<ISvgContent>();
        }

        public SvgContentsList(int capacity)
        {
            _contentsList = new List<ISvgContent>(capacity);
        }

        public SvgContentsList(params ISvgContent[] childElements)
        {
            _contentsList = new List<ISvgContent>(childElements);
        }

        public SvgContentsList(IEnumerable<ISvgContent> childElements)
        {
            _contentsList = new List<ISvgContent>(childElements);
        }


        public SvgContentsList Clear()
        {
            _contentsList.Clear();

            return this;
        }

        public SvgContentsList Append(ISvgContent childElement)
        {
            _contentsList.Add(childElement);

            return this;
        }

        public SvgContentsList AppendContents(params ISvgContent[] childElements)
        {
            _contentsList.AddRange(childElements);

            return this;
        }

        public SvgContentsList AppendContents(IEnumerable<ISvgContent> childElements)
        {
            _contentsList.AddRange(childElements);

            return this;
        }

        public SvgContentsList Prepend(ISvgContent childElement)
        {
            _contentsList.Insert(0, childElement);

            return this;
        }

        public SvgContentsList PrependContents(params ISvgContent[] childElements)
        {
            _contentsList.InsertRange(0, childElements);

            return this;
        }

        public SvgContentsList PrependContents(IEnumerable<ISvgContent> childElements)
        {
            _contentsList.InsertRange(0, childElements);

            return this;
        }

        public SvgContentsList Insert(int index, ISvgContent childElement)
        {
            _contentsList.Insert(index, childElement);

            return this;
        }

        public SvgContentsList InsertContents(int index, params ISvgContent[] childElements)
        {
            _contentsList.InsertRange(index, childElements);

            return this;
        }

        public SvgContentsList InsertContents(int index, IEnumerable<ISvgContent> childElements)
        {
            _contentsList.InsertRange(index, childElements);

            return this;
        }

        public SvgContentsList RemoveFirst()
        {
            _contentsList.RemoveAt(0);

            return this;
        }

        public SvgContentsList RemoveLast()
        {
            _contentsList.RemoveAt(_contentsList.Count - 1);

            return this;
        }

        public SvgContentsList Remove(int index)
        {
            _contentsList.RemoveAt(index);

            return this;
        }


        public SvgContentsList AppendText(string text)
        {
            return Append(
                SvgContentText.Create(text)
            );
        }

        public SvgContentsList AppendComment(string commentText)
        {
            return Append(
                SvgContentComment.Create(commentText)
            );
        }

        public SvgContentsList AppendTitle(string titleText)
        {
            if (string.IsNullOrEmpty(titleText))
                return this;

            return Append(
                SvgElementTitle.Create(titleText)
            );
        }

        public SvgContentsList AppendDescription(string descriptionText)
        {
            if (string.IsNullOrEmpty(descriptionText))
                return this;

            return Append(
                SvgElementDescription.Create(descriptionText)
            );
        }

        public SvgContentsList PrependText(string text)
        {
            return Prepend(
                SvgContentText.Create(text)
            );
        }

        public SvgContentsList PrependComment(string commentText)
        {
            return Prepend(
                SvgContentComment.Create(commentText)
            );
        }

        public SvgContentsList PrependTitle(string titleText)
        {
            if (string.IsNullOrEmpty(titleText))
                return this;

            return Prepend(
                SvgElementTitle.Create(titleText)
            );
        }

        public SvgContentsList PrependDescription(string descriptionText)
        {
            if (string.IsNullOrEmpty(descriptionText))
                return this;

            return Prepend(
                SvgElementDescription.Create(descriptionText)
            );
        }

        public SvgContentsList InsertText(int index, string text)
        {
            return Insert(
                index,
                SvgContentText.Create(text)
            );
        }

        public SvgContentsList InsertComment(int index, string commentText)
        {
            return Insert(
                index,
                SvgContentComment.Create(commentText)
            );
        }

        public SvgContentsList InsertTitle(int index, string titleText)
        {
            if (string.IsNullOrEmpty(titleText))
                return this;

            return Insert(
                index,
                SvgElementTitle.Create(titleText)
            );
        }

        public SvgContentsList InsertDescription(int index, string descriptionText)
        {
            if (string.IsNullOrEmpty(descriptionText))
                return this;

            return Insert(
                index,
                SvgElementDescription.Create(descriptionText)
            );
        }


        public IEnumerator<ISvgContent> GetEnumerator()
        {
            return _contentsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _contentsList.GetEnumerator();
        }

        public override string ToString()
        {
            return _contentsList
                .Select(e => e.ToString())
                .Where(c => !string.IsNullOrEmpty(c))
                .Concatenate(Environment.NewLine);
        }
    }
}
