using System;
using System.Drawing;
using System.Windows.Forms;

public static class TreeViewExtensions
{
    /// <summary>
    /// 노드를 추가하는 메서드
    /// </summary>
    /// <param name="treeView">설정할 TreeView</param>
    /// <param name="parentNode">부모 노드</param>
    /// <param name="text">추가할 노드의 텍스트</param>
    public static void AddNode(this TreeView treeView, TreeNode parentNode, string text)
    {
        TreeNode newNode = new TreeNode(text);
        if (parentNode == null)
        {
            treeView.Nodes.Add(newNode);
        }
        else
        {
            parentNode.Nodes.Add(newNode);
        }
    }

    /// <summary>
    /// 노드를 삭제하는 메서드
    /// </summary>
    /// <param name="treeView">설정할 TreeView</param>
    /// <param name="node">삭제할 노드</param>
    public static void RemoveNode(this TreeView treeView, TreeNode node)
    {
        if (node != null)
        {
            treeView.Nodes.Remove(node);
        }
    }

    /// <summary>
    /// 노드 데이터를 바인딩하는 메서드
    /// </summary>
    /// <param name="treeView">설정할 TreeView</param>
    /// <param name="data">바인딩할 데이터</param>
    public static void BindData(this TreeView treeView, TreeNode[] data)
    {
        treeView.Nodes.Clear();
        treeView.Nodes.AddRange(data);
    }

    /// <summary>
    /// 드래그 앤 드롭 기능을 설정하는 메서드
    /// </summary>
    /// <param name="treeView">설정할 TreeView</param>
    public static void EnableDragAndDrop(this TreeView treeView)
    {
        treeView.ItemDrag += (s, e) =>
        {
            treeView.DoDragDrop(e.Item, DragDropEffects.Move);
        };

        treeView.DragEnter += (s, e) =>
        {
            e.Effect = DragDropEffects.Move;
        };

        treeView.DragDrop += (s, e) =>
        {
            if (e.Data.GetData(typeof(TreeNode)) is TreeNode newNode)
            {
                TreeNode targetNode = treeView.GetNodeAt(treeView.PointToClient(new Point(e.X, e.Y)));
                if (targetNode != null)
                {
                    newNode.Remove();
                    targetNode.Nodes.Add(newNode);
                    targetNode.Expand();
                }
            }
        };
    }
}

public static class ProgressBarExtensions
{
    /// <summary>
    /// 애니메이션 효과를 설정하는 메서드
    /// </summary>
    /// <param name="progressBar">설정할 ProgressBar</param>
    /// <param name="value">설정할 값</param>
    /// <param name="duration">애니메이션 시간(밀리초)</param>
    public static void SetAnimatedValue(this ProgressBar progressBar, int value, int duration)
    {
        Timer timer = new Timer { Interval = duration / Math.Abs(value - progressBar.Value) };
        timer.Tick += (s, e) =>
        {
            if (progressBar.Value < value)
            {
                progressBar.Value++;
            }
            else if (progressBar.Value > value)
            {
                progressBar.Value--;
            }
            else
            {
                timer.Stop();
            }
        };
        timer.Start();
    }

    /// <summary>
    /// 단계별 진행 표시를 설정하는 메서드
    /// </summary>
    /// <param name="progressBar">설정할 ProgressBar</param>
    /// <param name="steps">단계 개수</param>
    public static void SetStepProgress(this ProgressBar progressBar, int steps)
    {
        progressBar.Minimum = 0;
        progressBar.Maximum = steps;
        progressBar.Step = 1;
    }

    /// <summary>
    /// 진행률을 업데이트하는 메서드
    /// </summary>
    /// <param name="progressBar">설정할 ProgressBar</param>
    /// <param name="value">설정할 값</param>
    public static void UpdateProgress(this ProgressBar progressBar, int value)
    {
        progressBar.Value = value;
    }
}

public static class DateTimePickerExtensions
{
    /// <summary>
    /// 날짜 범위를 설정하는 메서드
    /// </summary>
    /// <param name="dateTimePicker">설정할 DateTimePicker</param>
    /// <param name="minDate">최소 날짜</param>
    /// <param name="maxDate">최대 날짜</param>
    public static void SetDateRange(this DateTimePicker dateTimePicker, DateTime minDate, DateTime maxDate)
    {
        dateTimePicker.MinDate = minDate;
        dateTimePicker.MaxDate = maxDate;
    }

    /// <summary>
    /// 사용자 정의 날짜 형식을 설정하는 메서드
    /// </summary>
    /// <param name="dateTimePicker">설정할 DateTimePicker</param>
    /// <param name="format">사용자 정의 형식</param>
    public static void SetCustomFormat(this DateTimePicker dateTimePicker, string format)
    {
        dateTimePicker.Format = DateTimePickerFormat.Custom;
        dateTimePicker.CustomFormat = format;
    }

    /// <summary>
    /// 날짜 유효성을 검사하는 메서드
    /// </summary>
    /// <param name="dateTimePicker">설정할 DateTimePicker</param>
    /// <param name="validationFunc">유효성 검사 함수</param>
    /// <returns>유효한 경우 true, 그렇지 않으면 false</returns>
    public static bool ValidateDate(this DateTimePicker dateTimePicker, Func<DateTime, bool> validationFunc)
    {
        return validationFunc(dateTimePicker.Value);
    }
}

public static class MessageBoxExtensions
{
    /// <summary>
    /// 사용자 정의 메시지 박스를 표시하는 메서드
    /// </summary>
    /// <param name="message">메시지 내용</param>
    /// <param name="title">메시지 제목</param>
    /// <param name="icon">아이콘</param>
    /// <param name="buttons">버튼</param>
    /// <returns>메시지 박스 결과</returns>
    public static DialogResult ShowCustomMessageBox(string message, string title, MessageBoxIcon icon, MessageBoxButtons buttons)
    {
        return MessageBox.Show(message, title, buttons, icon);
    }

    /// <summary>
    /// 예/아니요 메시지 박스를 표시하는 메서드
    /// </summary>
    /// <param name="message">메시지 내용</param>
    /// <param name="title">메시지 제목</param>
    /// <returns>메시지 박스 결과</returns>
    public static DialogResult ShowYesNoMessageBox(string message, string title)
    {
        return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
    }

    /// <summary>
    /// 오류 메시지 박스를 표시하는 메서드
    /// </summary>
    /// <param name="message">메시지 내용</param>
    /// <param name="title">메시지 제목</param>
    public static void ShowErrorMessageBox(string message, string title)
    {
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    /// <summary>
    /// 정보 메시지 박스를 표시하는 메서드
    /// </summary>
    /// <param name="message">메시지 내용</param>
    /// <param name="title">메시지 제목</param>
    public static void ShowInfoMessageBox(string message, string title)
    {
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    /// 경고 메시지 박스를 표시하는 메서드
    /// </summary>
    /// <param name="message">메시지 내용</param>
    /// <param name="title">메시지 제목</param>
    public static void ShowWarningMessageBox(string message, string title)
    {
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
