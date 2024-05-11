Imports System.Drawing
Imports System.Reflection.Emit

Public Class Form1
    Private drawing As Boolean = False
    Private brushSize As Integer = 30
    Private lastPoint As Point
    Private currentColor As Color = Color.Black
    Private pointList As New List(Of Tuple(Of Point, Point, Color))()

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        drawing = True
        lastPoint = e.Location
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If drawing Then
            pointList.Add(Tuple.Create(lastPoint, e.Location, currentColor))
            lastPoint = e.Location
            PictureBox1.Invalidate()
        End If
    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
        drawing = False
    End Sub

    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        Using pen As New Pen(Color.Black, 10)
            pen.LineJoin = Drawing2D.LineJoin.Round
            pen.EndCap = Drawing2D.LineCap.Round
            pen.StartCap = Drawing2D.LineCap.Round
            For Each pointColor In pointList
                pen.Color = pointColor.Item3
                e.Graphics.DrawLine(pen, pointColor.Item1, pointColor.Item2)
            Next
        End Using
    End Sub

    Private Sub ColorBlue_Click(sender As Object, e As EventArgs) Handles ColorBlue.Click
        Label3.Text = "Blue"
        Label3.ForeColor = Color.Blue
        currentColor = Color.Blue
    End Sub

    Private Sub ColorRed_Click(sender As Object, e As EventArgs) Handles ColorRed.Click
        Label3.Text = "Red"
        Label3.ForeColor = Color.Red
        currentColor = Color.Red
    End Sub

    Private Sub ColorGreen_Click(sender As Object, e As EventArgs) Handles ColorGreen.Click
        Label3.Text = "Green"
        Label3.ForeColor = Color.Green
        currentColor = Color.Green
    End Sub

    Private Sub ColorOrange_Click(sender As Object, e As EventArgs) Handles ColorOrange.Click
        Label3.Text = "Orange"
        Label3.ForeColor = Color.Orange
        currentColor = Color.Orange
    End Sub

    Private Sub ColorYellow_Click(sender As Object, e As EventArgs) Handles ColorYellow.Click
        Label3.Text = "Yellow"
        Label3.ForeColor = Color.Yellow
        currentColor = Color.Yellow
    End Sub

    Private Sub ColorPink_Click(sender As Object, e As EventArgs) Handles ColorPink.Click
        Label3.Text = "Pink"
        Label3.ForeColor = Color.Pink
        currentColor = Color.Pink
    End Sub

    Private Sub ColorBlack_Click(sender As Object, e As EventArgs) Handles ColorBlack.Click
        Label3.Text = "Black"
        Label3.ForeColor = Color.Black
        currentColor = Color.Black
    End Sub

    Private Sub Eraser_Click(sender As Object, e As EventArgs) Handles Eraser.Click
        Label3.Text = "Eraser"
        Label3.ForeColor = PictureBox1.BackColor
        currentColor = PictureBox1.BackColor
    End Sub

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        Dim circleCursor As New Cursor(CreateCircleCursor(brushSize, currentColor))
        PictureBox1.Cursor = circleCursor
    End Sub

    Private Function CreateCircleCursor(size As Integer, color As Color) As IntPtr
        Dim smallerSize As Integer = size - 10 ' Adjust the size here
        Dim bm As New Bitmap(size, size)
        Dim g As Graphics = Graphics.FromImage(bm)
        Dim borderPen As New Pen(Color.Black, 2) ' Specify the border color and width
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.FillEllipse(New SolidBrush(color), (size - smallerSize) \ 2, (size - smallerSize) \ 2, smallerSize, smallerSize)
        g.DrawEllipse(borderPen, (size - smallerSize) \ 2, (size - smallerSize) \ 2, smallerSize, smallerSize) ' Draw the border
        g.Dispose()
        Return bm.GetHicon()
    End Function

    Private Sub EraseAll_Click(sender As Object, e As EventArgs) Handles EraseAll.Click
        pointList.Clear()
        PictureBox1.Invalidate()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Save_Click(sender As Object, e As EventArgs) Handles Save.Click
        Using dialog As New SaveFileDialog()
            dialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|BMP Image|*.bmp"
            dialog.Title = "Save an Image File"
            dialog.ShowDialog()

            If dialog.FileName <> "" Then
                Dim bmp As New Bitmap(PictureBox1.Width, PictureBox1.Height)
                PictureBox1.DrawToBitmap(bmp, New Rectangle(0, 0, PictureBox1.Width, PictureBox1.Height))
                bmp.Save(dialog.FileName)
            End If
        End Using
    End Sub

End Class
