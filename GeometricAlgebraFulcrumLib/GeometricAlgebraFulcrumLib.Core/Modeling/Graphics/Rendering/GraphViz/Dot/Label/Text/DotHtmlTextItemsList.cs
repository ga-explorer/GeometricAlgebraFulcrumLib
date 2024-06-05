using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.GraphViz.Dot.Label.Text;

/// <summary>
/// This class represents an HTML test items list tag in the dot language
/// See http://www.graphviz.org/content/node-shapes#html for more details
/// </summary>
public sealed class DotHtmlTextItemsList : DotHtmlText, IList<DotHtmlTextItem>
{
    private readonly List<DotHtmlTextItem> _items = new List<DotHtmlTextItem>();

    public override string Value => _items.Concatenate(" ");


    public DotHtmlTextItemsList AddString(string text)
    {
        _items.Add(text.ToHtmlString());

        return this;
    }

    public DotHtmlTextItemsList AddLineBreak()
    {
        _items.Add(LineBreak);

        return this;
    }

    public DotHtmlTextItemsList AddLineBreakAlignCenter()
    {
        _items.Add(LineBreakAlignCenter);

        return this;
    }

    public DotHtmlTextItemsList AddLineBreakAlignLeft()
    {
        _items.Add(LineBreakAlignLeft);

        return this;
    }

    public DotHtmlTextItemsList AddLineBreakAlignRight()
    {
        _items.Add(LineBreakAlignRight);

        return this;
    }

    public DotHtmlTextItemsList AddRange(IEnumerable<DotHtmlTextItem> items)
    {
        _items.AddRange(items);

        return this;
    }

    public DotHtmlTextItemsList AddRange(params DotHtmlTextItem[] items)
    {
        _items.AddRange(items);

        return this;
    }


    public override string ToString()
    {
        return _items.Concatenate(" ");
    }

    public int IndexOf(DotHtmlTextItem item)
    {
        return _items.FindIndex(t =>  t.Value == item.Value);
    }

    public void Insert(int index, DotHtmlTextItem item)
    {
        _items.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        _items.RemoveAt(index);
    }

    public DotHtmlTextItem this[int index]
    {
        get => _items[index];
        set
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException(nameof(value));

            _items[index] = value;
        }
    }

    public void Add(DotHtmlTextItem item)
    {
        _items.Add(item);
    }

    public void Clear()
    {
        _items.Clear();
    }

    public bool Contains(DotHtmlTextItem item)
    {
        return _items.FirstOrDefault(t => t.Value == item.Value) != null;
    }

    public void CopyTo(DotHtmlTextItem[] array, int arrayIndex)
    {
        _items.CopyTo(array, arrayIndex);
    }

    public int Count => _items.Count;

    public bool IsReadOnly => false;

    public bool Remove(DotHtmlTextItem item)
    {
        return _items.Remove(item);
    }

    public IEnumerator<DotHtmlTextItem> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _items.GetEnumerator();
    }
}