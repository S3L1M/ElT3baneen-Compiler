Public Class Form1

    Private isEdited = False

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Button2.Enabled = False
    End Sub

    Private Function createCode() As String
        Dim r As New Random
        Dim path As String = My.Computer.FileSystem.SpecialDirectories.Temp + "\" + r.Next.ToString + ".tmp"
        My.Computer.FileSystem.WriteAllText(path, RichTextBox1.Text, False)

        Return path
    End Function

    Private Sub callBackend(ByVal path As String)
        Dim backendProcess As New Process
        With backendProcess.StartInfo
            .FileName = "CMD.EXE"
            .Arguments = "/c java Main " + path.Replace("\", "\\").Replace(" ", "/*")
            .RedirectStandardOutput = True
            .UseShellExecute = False
            .CreateNoWindow = True
        End With
        backendProcess.Start()

        MessageBox.Show(backendProcess.StandardOutput.ReadToEnd, "Output")
        My.Computer.FileSystem.DeleteFile(path)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If RichTextBox1.Text = "" Or RichTextBox1.Text = "// Write some code here" Then
            MessageBox.Show("Please write code first", "Missing code", 0, MessageBoxIcon.Exclamation)
        Else : callBackend(createCode)
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If OpenFileDialog1.CheckFileExists = True Then callBackend(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub RichTextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles RichTextBox1.KeyDown
        If isEdited 
        Else
            RichTextBox1.Clear()
            RichTextBox1.ForeColor = Color.Black
            isEdited = True
        End If
    End Sub

    Private Sub RichTextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles RichTextBox1.KeyUp
        If RichTextBox1.Text = "" Then
            RichTextBox1.Text = "// Write some code here"
            RichTextBox1.ForeColor = Color.LightGray
            isEdited = False
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Button1_Click(Me, EventArgs.Empty)
    End Sub
End Class
