Public Class Load

    'Dim timerLoading As Timer | Form
    Dim pLoadingBar As ProgressBar

    Private Sub Load_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        pLoadingBar = New ProgressBar
        Dim pbLogo As New PictureBox

        Text = "Chargement de ScanList"
        FormBorderStyle = FormBorderStyle.None
        StartPosition = FormStartPosition.CenterScreen
        Size = New Size(360, 640)
        Controls.Add(pbLogo)
        Controls.Add(pLoadingBar)

        pLoadingBar.Size = New Size(300, 30)
        pLoadingBar.Location = New Point(30, 475)

        pbLogo.Image = Image.FromFile("LogoScanList.png")
        pbLogo.Size = pbLogo.Image.Size
        pbLogo.Location = New Point(pLoadingBar.Location.X + 60, pLoadingBar.Location.Y - 300)

        timerLoading.Interval = 10
        timerLoading.Start()

    End Sub

    Private Sub timerLoading_Tick(sender As Object, e As EventArgs) Handles timerLoading.Tick

        pLoadingBar.Value += 1

        If pLoadingBar.Value = 100 Then
            timerLoading.Stop()
            Hide()
            Dim sc As New ScanList
            sc.Show()
        End If

    End Sub
End Class
