using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

public static class ListViewExtensions
{
    // 모델의 속성을 기반으로 ListView 컬럼을 생성하는 확장 메서드
    public static void AddColumnsFromModel<T>(this ListView listView)
    {
        if (listView == null)
        {
            throw new ArgumentNullException("ListView가 null입니다.");
        }

        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var property in properties)
        {
            listView.Columns.Add(property.Name);
        }
    }

    // ListView 컬럼의 크기와 정렬을 설정하는 확장 메서드
    public static void SetColumnWidths(this ListView listView, int[] widths, HorizontalAlignment[] alignments = null)
    {
        if (listView == null)
        {
            throw new ArgumentNullException("ListView가 null입니다.");
        }

        if (widths.Length != listView.Columns.Count)
        {
            throw new ArgumentException("너비 배열의 길이가 ListView 컬럼의 개수와 일치하지 않습니다.");
        }

        for (int i = 0; i < widths.Length; i++)
        {
            listView.Columns[i].Width = widths[i];
            listView.Columns[i].TextAlign = alignments != null && alignments.Length > i ? alignments[i] : HorizontalAlignment.Center;
        }
    }

    // 모델을 받아 ListView에 항목을 추가하는 확장 메서드
    public static void AddItem2<T>(this ListView listView, T model)
    {
        if (listView == null || model == null)
        {
            throw new ArgumentNullException("ListView 또는 모델이 null입니다.");
        }

        if (listView.InvokeRequired)
        {
            listView.Invoke(new Action(() => AddItem2(listView, model)));
        }
        else
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var values = properties.Select(p => p.GetValue(model)?.ToString() ?? string.Empty).ToArray();

            listView.AddItem(values);
        }
    }

    // 모델 리스트를 받아 ListView에 항목을 대량으로 추가하는 확장 메서드
    public static void AddItems2<T>(this ListView listView, List<T> models)
    {
        if (listView == null || models == null)
        {
            throw new ArgumentNullException("ListView 또는 모델 리스트가 null입니다.");
        }

        if (listView.InvokeRequired)
        {
            listView.Invoke(new Action(() => AddItems2(listView, models)));
        }
        else
        {
            foreach (var model in models)
            {
                listView.AddItem2(model);
            }
        }
    }

    // 특정 컬럼에 대해 오름차순 또는 내림차순으로 정렬하는 확장 메서드
    public static void SortByColumn(this ListView listView, int columnIndex, bool ascending = true)
    {
        if (listView == null)
        {
            throw new ArgumentNullException("ListView가 null입니다.");
        }

        if (columnIndex < 0 || columnIndex >= listView.Columns.Count)
        {
            throw new ArgumentOutOfRangeException("컬럼 인덱스가 유효하지 않습니다.");
        }

        if (listView.InvokeRequired)
        {
            listView.Invoke(new Action(() => SortByColumn(listView, columnIndex, ascending)));
        }
        else
        {
            listView.ListViewItemSorter = new ListViewItemComparer(columnIndex, ascending);
            listView.Sort();
        }
    }



    // ListViewItemComparer 클래스
    private class ListViewItemComparer : IComparer
    {
        private readonly int _columnIndex;
        private readonly bool _ascending;

        public ListViewItemComparer(int columnIndex, bool ascending)
        {
            _columnIndex = columnIndex;
            _ascending = ascending;
        }

        public int Compare(object x, object y)
        {
            var itemX = x as ListViewItem;
            var itemY = y as ListViewItem;

            if (itemX == null || itemY == null) return 0;

            int result = string.Compare(itemX.SubItems[_columnIndex].Text, itemY.SubItems[_columnIndex].Text);

            return _ascending ? result : -result;
        }
    }

    
    public static void AddItem(this ListView listView, string[] values)
    {
        if (listView == null || values == null)
        {
            throw new ArgumentNullException("ListView 또는 값이 null입니다.");
        }

        if (listView.InvokeRequired)
        {
            listView.Invoke(new Action(() => AddItem(listView, values)));
        }
        else
        {
            ListViewItem item = new ListViewItem(values);
            listView.Items.Add(item);
        }
    }

    public static void AddItems(this ListView listView, List<string[]> items)
    {
        if (listView == null || items == null)
        {
            throw new ArgumentNullException("ListView 또는 값이 null입니다.");
        }

        if (listView.InvokeRequired)
        {
            listView.Invoke(new Action(() => AddItems(listView, items)));
        }
        else
        {
            foreach (var values in items)
            {
                listView.AddItem(values);
            }
        }
    }

    public static void UpdateItem(this ListView listView, int index, string[] values)
    {
        if (listView == null || values == null)
        {
            throw new ArgumentNullException("ListView 또는 값이 null입니다.");
        }

        if (index >= 0 && index < listView.Items.Count)
        {
            if (listView.InvokeRequired)
            {
                listView.Invoke(new Action(() => UpdateItem(listView, index, values)));
            }
            else
            {
                ListViewItem item = listView.Items[index];
                for (int i = 0; i < values.Length; i++)
                {
                    if (i < item.SubItems.Count)
                    {
                        item.SubItems[i].Text = values[i];
                    }
                    else
                    {
                        item.SubItems.Add(values[i]);
                    }
                }
            }
        }
        else
        {
            throw new ArgumentOutOfRangeException("인덱스가 ListView 항목의 범위를 벗어났습니다.");
        }
    }

    public static void RemoveItem(this ListView listView, int index)
    {
        if (listView == null)
        {
            throw new ArgumentNullException("ListView가 null입니다.");
        }

        if (index >= 0 && index < listView.Items.Count)
        {
            if (listView.InvokeRequired)
            {
                listView.Invoke(new Action(() => RemoveItem(listView, index)));
            }
            else
            {
                listView.Items.RemoveAt(index);
            }
        }
        else
        {
            throw new ArgumentOutOfRangeException("인덱스가 ListView 항목의 범위를 벗어났습니다.");
        }
    }

    public static void ClearAllItems(this ListView listView)
    {
        if (listView == null)
        {
            throw new ArgumentNullException("ListView가 null입니다.");
        }

        if (listView.InvokeRequired)
        {
            listView.Invoke(new Action(() => ClearAllItems(listView)));
        }
        else
        {
            listView.Items.Clear();
        }
    }

    public static ListViewItem FindItemByText(this ListView listView, string text)
    {
        if (listView == null)
        {
            throw new ArgumentNullException("ListView가 null입니다.");
        }

        if (listView.InvokeRequired)
        {
            return (ListViewItem)listView.Invoke(new Func<ListViewItem>(() => FindItemByText(listView, text)));
        }
        else
        {
            return listView.Items.Cast<ListViewItem>().FirstOrDefault(item => item.Text.Equals(text, StringComparison.OrdinalIgnoreCase));
        }
    }

    public static List<ListViewItem> FindAllItemsByText(this ListView listView, string text)
    {
        if (listView == null)
        {
            throw new ArgumentNullException("ListView가 null입니다.");
        }

        if (listView.InvokeRequired)
        {
            return (List<ListViewItem>)listView.Invoke(new Func<List<ListViewItem>>(() => FindAllItemsByText(listView, text)));
        }
        else
        {
            return listView.Items.Cast<ListViewItem>().Where(item => item.Text.Equals(text, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }

    public static List<string> GetItemData(this ListView listView, int index)
    {
        if (listView == null)
        {
            throw new ArgumentNullException("ListView가 null입니다.");
        }

        if (index < 0 || index >= listView.Items.Count)
        {
            throw new ArgumentOutOfRangeException("인덱스가 ListView 항목의 범위를 벗어났습니다.");
        }

        ListViewItem item = listView.Items[index];
        List<string> itemData = new List<string>();

        for (int i = 0; i < item.SubItems.Count; i++)
        {
            itemData.Add(item.SubItems[i].Text);
        }

        return itemData;
    }
}
