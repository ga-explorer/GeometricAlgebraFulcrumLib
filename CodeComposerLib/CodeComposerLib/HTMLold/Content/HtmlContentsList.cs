using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.HTMLold.Elements;
using CodeComposerLib.HTMLold.Elements.Descriptive;
using TextComposerLib.Text;

namespace CodeComposerLib.HTMLold.Content
{
    public sealed class HtmlContentsList : IReadOnlyCollection<IHtmlContent>
    {
        private readonly List<IHtmlContent> _contentsList;


        public int Count
            => _contentsList.Count;


        public IEnumerable<HtmlElement> GetChildElements()
        {
            return _contentsList
                .Select(c => c as HtmlElement)
                .Where(c => !ReferenceEquals(c, null));
        }

        public HtmlElement GetChildElement(string elementId)
        {
            return _contentsList
                .Select(c => c as HtmlElement)
                .FirstOrDefault(c => !ReferenceEquals(c, null) && c.Id == elementId);
        }

        /// <summary>
        /// Obtain a list of descendant SVG elements
        /// </summary>
        /// <param name="depthFirst"></param>
        /// <returns></returns>
        public IEnumerable<HtmlElement> GetDescendantElements(bool depthFirst = true)
        {

            if (depthFirst)
            {
                var elementsStack = new Stack<HtmlElement>();
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

            var elementsQueue = new Queue<HtmlElement>();
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
        public HtmlElement GetDescendantElement(string id, bool depthFirst = true)
        {

            if (depthFirst)
            {
                var elementsStack = new Stack<HtmlElement>();
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

            var elementsQueue = new Queue<HtmlElement>();
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

        public HtmlContentsList()
        {
            _contentsList = new List<IHtmlContent>();
        }

        public HtmlContentsList(int capacity)
        {
            _contentsList = new List<IHtmlContent>(capacity);
        }

        public HtmlContentsList(params IHtmlContent[] childElements)
        {
            _contentsList = new List<IHtmlContent>(childElements);
        }

        public HtmlContentsList(IEnumerable<IHtmlContent> childElements)
        {
            _contentsList = new List<IHtmlContent>(childElements);
        }


        public HtmlContentsList Clear()
        {
            _contentsList.Clear();

            return this;
        }

        public HtmlContentsList Append(IHtmlContent childElement)
        {
            _contentsList.Add(childElement);

            return this;
        }

        public HtmlContentsList AppendContents(params IHtmlContent[] childElements)
        {
            _contentsList.AddRange(childElements);

            return this;
        }

        public HtmlContentsList AppendContents(IEnumerable<IHtmlContent> childElements)
        {
            _contentsList.AddRange(childElements);

            return this;
        }

        public HtmlContentsList Prepend(IHtmlContent childElement)
        {
            _contentsList.Insert(0, childElement);

            return this;
        }

        public HtmlContentsList PrependContents(params IHtmlContent[] childElements)
        {
            _contentsList.InsertRange(0, childElements);

            return this;
        }

        public HtmlContentsList PrependContents(IEnumerable<IHtmlContent> childElements)
        {
            _contentsList.InsertRange(0, childElements);

            return this;
        }

        public HtmlContentsList Insert(int index, IHtmlContent childElement)
        {
            _contentsList.Insert(index, childElement);

            return this;
        }

        public HtmlContentsList InsertContents(int index, params IHtmlContent[] childElements)
        {
            _contentsList.InsertRange(index, childElements);

            return this;
        }

        public HtmlContentsList InsertContents(int index, IEnumerable<IHtmlContent> childElements)
        {
            _contentsList.InsertRange(index, childElements);

            return this;
        }

        public HtmlContentsList RemoveFirst()
        {
            _contentsList.RemoveAt(0);

            return this;
        }

        public HtmlContentsList RemoveLast()
        {
            _contentsList.RemoveAt(_contentsList.Count - 1);

            return this;
        }

        public HtmlContentsList Remove(int index)
        {
            _contentsList.RemoveAt(index);

            return this;
        }


        public HtmlContentsList AppendText(string text)
        {
            return Append(
                HtmlContentText.Create(text)
            );
        }

        public HtmlContentsList AppendComment(string commentText)
        {
            return Append(
                HtmlContentComment.Create(commentText)
            );
        }

        public HtmlContentsList AppendTitle(string titleText)
        {
            if (string.IsNullOrEmpty(titleText))
                return this;

            return Append(
                HtmlElementTitle.Create(titleText)
            );
        }

        public HtmlContentsList AppendDescription(string descriptionText)
        {
            if (string.IsNullOrEmpty(descriptionText))
                return this;

            return Append(
                HtmlElementDescription.Create(descriptionText)
            );
        }

        public HtmlContentsList PrependText(string text)
        {
            return Prepend(
                HtmlContentText.Create(text)
            );
        }

        public HtmlContentsList PrependComment(string commentText)
        {
            return Prepend(
                HtmlContentComment.Create(commentText)
            );
        }

        public HtmlContentsList PrependTitle(string titleText)
        {
            if (string.IsNullOrEmpty(titleText))
                return this;

            return Prepend(
                HtmlElementTitle.Create(titleText)
            );
        }

        public HtmlContentsList PrependDescription(string descriptionText)
        {
            if (string.IsNullOrEmpty(descriptionText))
                return this;

            return Prepend(
                HtmlElementDescription.Create(descriptionText)
            );
        }

        public HtmlContentsList InsertText(int index, string text)
        {
            return Insert(
                index,
                HtmlContentText.Create(text)
            );
        }

        public HtmlContentsList InsertComment(int index, string commentText)
        {
            return Insert(
                index,
                HtmlContentComment.Create(commentText)
            );
        }

        public HtmlContentsList InsertTitle(int index, string titleText)
        {
            if (string.IsNullOrEmpty(titleText))
                return this;

            return Insert(
                index,
                HtmlElementTitle.Create(titleText)
            );
        }

        public HtmlContentsList InsertDescription(int index, string descriptionText)
        {
            if (string.IsNullOrEmpty(descriptionText))
                return this;

            return Insert(
                index,
                HtmlElementDescription.Create(descriptionText)
            );
        }


        public IEnumerator<IHtmlContent> GetEnumerator()
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
