using System.Text;

namespace GeometricAlgebraFulcrumLib.Core.Utilities.Text.Markdown.Tables;

public sealed class MarkdownTable
{
    private readonly Dictionary<string, MarkdownTableColumn> _columnsDictionary
        = new Dictionary<string, MarkdownTableColumn>();


    public IEnumerable<MarkdownTableColumn> Columns
        => _columnsDictionary.Values.OrderBy(c => c.ColumnIndex);

    public int ColumnsCount 
        => _columnsDictionary.Count;

    public string MarkdownTitle
        => Columns
            .Select(c => c.MarkdownTitle)
            .Concatenate("|", "|", "|");

    public string MarkdownSeparator
        => Columns
            .Select(c => c.MarkdownSeparator)
            .Concatenate("|", "|", "|");

    public int RowsCount
        => _columnsDictionary.Max(p => p.Value.Count);

    public int MarkdownWidth
        => 1 + _columnsDictionary.Count + _columnsDictionary.Sum(p => p.Value.MarkdownWidth);

    public int MarkdownHeight
        => 2 + RowsCount;

    public IEnumerable<string> MarkdownRows
    {
        get
        {
            if (_columnsDictionary.Count == 0) yield break;

            var rowsCount = _columnsDictionary.Max(p => p.Value.Count);

            for (var r = 0; r < rowsCount; r++)
            {
                var r1 = r; //To prevent 'Access to modified closure' warning

                yield return
                    Columns
                        .Select(c => c.MarkdownItem(r1))
                        .Concatenate("|", "|", "|");
            }
        }
    }

    public MarkdownTableColumn this[string name] 
        => _columnsDictionary[name];

    public MarkdownTableColumn this[int index]
        => _columnsDictionary.First(p => p.Value.ColumnIndex == index).Value;


    public MarkdownTable Clear()
    {
        _columnsDictionary.Clear();

        return this;
    }

    public MarkdownTableColumn AddColumn(string name, string title = "")
    {
        if (_columnsDictionary.TryGetValue(name, out var column))
        {
            var order = column.ColumnIndex;
            column = new MarkdownTableColumn(order, name, title);
            _columnsDictionary[name] = column;
        }
        else
        {
            var order = _columnsDictionary.Count;
            column = new MarkdownTableColumn(order, name, title);
            _columnsDictionary.Add(name, column);
        }
                
        return column;
    }

    public MarkdownTableColumn AddColumn(string name, MarkdownTableColumnAlignment alignment, string title = "")
    {
        if (_columnsDictionary.TryGetValue(name, out var column))
        {
            var order = column.ColumnIndex;
            column = new MarkdownTableColumn(order, name, title) { ColumnAlignment = alignment };
            _columnsDictionary[name] = column;
        }
        else
        {
            var order = _columnsDictionary.Count;
            column = new MarkdownTableColumn(order, name, title) { ColumnAlignment = alignment };
            _columnsDictionary.Add(name, column);
        }

        return column;
    }

    public bool TryGetColumn(string name, out MarkdownTableColumn column)
    {
        return _columnsDictionary.TryGetValue(name, out column);
    }

    public bool TryGetColumn(int index, out MarkdownTableColumn column)
    {
        column = _columnsDictionary.FirstOrDefault(p => p.Value.ColumnIndex == index).Value;

        return column != null;
    }

    public MarkdownTable SetColumnIndex(string name, int newIndex)
    {
        var column = _columnsDictionary[name];
        var oldIndex = column.ColumnIndex;

        if (newIndex == column.ColumnIndex) return this;

        //Modify indices of columns between new and old column index
        if (newIndex > oldIndex)
        {
            foreach (var col in _columnsDictionary.Values)
                if (col.ColumnIndex > oldIndex && col.ColumnIndex < newIndex)
                    col.ColumnIndex--;
        }
        else
        {
            foreach (var col in _columnsDictionary.Values)
                if (col.ColumnIndex < oldIndex && col.ColumnIndex > newIndex)
                    col.ColumnIndex++;
        }

        column.ColumnIndex = newIndex;

        return this;
    }

    public MarkdownTable SetColumnIndex(int oldIndex, int newIndex)
    {
        var column = this[oldIndex];

        if (newIndex == column.ColumnIndex) return this;

        //Modify indices of columns between new and old column index
        if (newIndex > oldIndex)
        {
            foreach (var col in _columnsDictionary.Values)
                if (col.ColumnIndex > oldIndex && col.ColumnIndex < newIndex)
                    col.ColumnIndex--;
        }
        else
        {
            foreach (var col in _columnsDictionary.Values)
                if (col.ColumnIndex < oldIndex && col.ColumnIndex > newIndex)
                    col.ColumnIndex++;
        }

        column.ColumnIndex = newIndex;

        return this;
    }

    public bool RemoveColumn(string name)
    {
        return _columnsDictionary.Remove(name);
    }

    public bool RemoveColumn(int index)
    {
        return _columnsDictionary.Remove(this[index].ColumnName);
    }

    public override string ToString()
    {
        var s = new StringBuilder();

        s.AppendLine(MarkdownTitle);
        s.AppendLine(MarkdownSeparator);

        foreach (var row in MarkdownRows)
            s.AppendLine(row);

        return s.ToString();
    }
}