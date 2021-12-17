using System.Windows.Forms;

public static class MsgBox
{
    public static void Required(string body, string title)
    {
        MessageBox.Show(body + " is requried", title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    public static void Success(string title)
    {
        MessageBox.Show("Request Submitted Successfully", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    public static void Failed(string body, string title)
    {
        MessageBox.Show(body, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    public static DialogResult Confirm(string title)
    {
        return MessageBox.Show("Do you want to Submit?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
    }
    public static DialogResult Dialog(string body, string title)
    {
        return MessageBox.Show(body, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
    }
    public static DialogResult Exit(string title)
    {
        return MessageBox.Show("Do you want to close?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
    }
    public static void Notification(string body, ToolTipIcon icon = ToolTipIcon.Info)
    {
        var notification = new NotifyIcon()
        {
            Visible = true,
            Icon = System.Drawing.SystemIcons.Application,
            BalloonTipIcon = icon,
            BalloonTipTitle = AioData.ApplicationName,
            BalloonTipText = body,
        };
        notification.ShowBalloonTip(10000);
        notification.Dispose();
    }
}