Public Class ScanList
    'Appui long sur supprimer > supprimer toutes les listes ?

    ' Var : type (s > string, i > integer, etc...) ou form (pnl > panel, btn > button, etc...) + ecran sur lequel il se trouve + nom
    ' Methode : but + ecran

    ' S/ tous les écrans
    Private _sAllCurrentScreen As String
    Private _sAllPreviousScreen As String
    Dim lListeParDefaut As New List(Of String)
    Dim lMaListe As List(Of String)
    Dim lMesListes As New List(Of List(Of String))
    Dim pnlAllHeader As Panel
    Dim picAllHeaderBack As PictureBox
    Dim bAllowModification = False

    ' Ecran principal
    Dim pnlMainBody As Panel
    Dim pnlMainFooter As Panel
    Dim WithEvents pnlMainBodyFavListBackground As Panel
    Dim WithEvents lblMainBodyFavListName As Label
    Dim WithEvents pnlMainBodyList2 As Panel
    Dim WithEvents lblMainBodyList2Name As Label
    Dim WithEvents pnlMainBodyList3 As Panel
    Dim WithEvents lblMainBodyList3Name As Label
    Dim lstMainBodyProducts As ListBox
    Dim bListBoxDisplay As Boolean = True
    Private _sMainListBoxPosition As String = "fav" 'Indique la pos de la list box

    ' Ecran création
    Dim pnlCreateBody As Panel
    Dim pnlCreateFooter As Panel
    Dim lstCreateProduct As ListBox
    Dim txtCreateNomListe As TextBox
    Dim WithEvents picCreatePlus As New PictureBox
    Dim WithEvents picCreateMinus As New PictureBox
    Dim WithEvents picCreateCheckmark As New PictureBox

    ' Ecran modifier
    Dim pnlModifyBody As Panel
    Dim pnlModifyFooter As Panel
    Dim lstModifyProduct As ListBox
    Dim txtModifyNomListe As TextBox
    Dim WithEvents picModifyPlus As New PictureBox
    Dim WithEvents picModifyMinus As New PictureBox
    Dim WithEvents picModifyCheckmark As New PictureBox
    Dim iListeSelectionnee As Integer

    ' Ecran supprimer
    Dim pnlDeleteBody As Panel
    Dim pnlDeleteFooter As Panel
    Dim lstDeleteLists As ListBox

    ' Ecran paramètre
    Dim pnlSettingBody As Panel
    Dim pnlSettingFooter As Panel

    Private Sub ScanList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        StartPosition = FormStartPosition.CenterScreen
        Size = New Size(360, 640)

        InitializeHeaderAll()
        DisplayMainScreen()

    End Sub

    Public Property sAllCurrentScreenGetSet As String
        Get
            Return _sAllCurrentScreen
        End Get
        Set(value As String)
            _sAllCurrentScreen = value
        End Set
    End Property

    Public Property sAllPreviousScreenGetSet As String
        Get
            Return _sAllPreviousScreen
        End Get
        Set(value As String)
            _sAllPreviousScreen = value
        End Set
    End Property

    Private Sub InitializeHeaderAll()
        Dim picAllHeaderSettings As New PictureBox
        Dim picAllHeaderLogo As New PictureBox

        picAllHeaderSettings.Image = Image.FromFile("GearScanList.png")
        picAllHeaderSettings.Size = picAllHeaderSettings.Image.Size
        picAllHeaderSettings.Location = New Point(295, 20)
        picAllHeaderSettings.Cursor = Cursors.Hand

        picAllHeaderLogo.Image = Image.FromFile("AlternateLogoScanList.png")
        picAllHeaderLogo.Size = picAllHeaderLogo.Image.Size
        picAllHeaderLogo.Location = New Point(picAllHeaderSettings.Left - 200, -7)

        picAllHeaderBack = New PictureBox
        picAllHeaderBack.Image = Image.FromFile("BackScanList.png")
        picAllHeaderBack.Size = picAllHeaderBack.Image.Size
        picAllHeaderBack.Location = New Point(picAllHeaderLogo.Left - 100, 20)
        picAllHeaderBack.Cursor = Cursors.Hand
        picAllHeaderBack.Visible = False

        pnlAllHeader = New Panel
        pnlAllHeader.AutoSize = True        '
        pnlAllHeader.Controls.Add(picAllHeaderSettings)
        pnlAllHeader.Controls.Add(picAllHeaderLogo)
        pnlAllHeader.Controls.Add(picAllHeaderBack)

        Controls.Add(pnlAllHeader)

        ' Gestion des événements
        AddHandler picAllHeaderSettings.Click,
            AddressOf DisplaySettings
    End Sub

    Private Sub DisplayMainScreen()
        _sAllCurrentScreen = "MAIN"
        picAllHeaderBack.Hide()
        Dim lblMainBodyMesListes As New Label
        pnlMainBodyFavListBackground = New Panel
        lblMainBodyFavListName = New Label
        pnlMainBodyList2 = New Panel
        lblMainBodyList2Name = New Label
        pnlMainBodyList3 = New Panel
        lblMainBodyList3Name = New Label
        Dim btnMainBodyAjouterAuPanier As New Button
        Dim btnMainFooterCreer As New Button
        Dim btnMainFooterModifier As New Button
        Dim btnMainFooterSupprimer As New Button

        lblMainBodyMesListes.Text = "Mes listes :"
        lblMainBodyMesListes.Location = New Point(0, pnlAllHeader.Bottom + 10)
        lblMainBodyMesListes.AutoSize = True
        lblMainBodyMesListes.Font = New Drawing.Font("Tahoma", 14, FontStyle.Underline)

        pnlMainBodyFavListBackground.Location = New Point(0, lblMainBodyMesListes.Bottom + 20)
        pnlMainBodyFavListBackground.Size = New Size(360, 40)
        pnlMainBodyFavListBackground.BackColor = Color.FromArgb(152, 204, 253)
        pnlMainBodyFavListBackground.SendToBack()
        pnlMainBodyFavListBackground.Cursor = Cursors.Hand

        lblMainBodyFavListName.Text = "Votre première liste" 'A modifier en fonction du nom de la liste de l'user
        lblMainBodyFavListName.Location = New Point(2, pnlMainBodyFavListBackground.Location.Y + 5)
        lblMainBodyFavListName.AutoSize = True
        lblMainBodyFavListName.Font = New Drawing.Font("Tahoma", 16)
        lblMainBodyFavListName.BackColor = pnlMainBodyFavListBackground.BackColor
        lblMainBodyFavListName.BringToFront()
        lblMainBodyFavListName.Cursor = Cursors.Hand

        lstMainBodyProducts = New ListBox
        lstMainBodyProducts.Location = New Point(10, pnlMainBodyFavListBackground.Bottom)
        lstMainBodyProducts.BorderStyle = BorderStyle.FixedSingle
        lstMainBodyProducts.Size = New Size(340, 200)
        lstMainBodyProducts.ItemHeight = 40
        lstMainBodyProducts.Font = New Drawing.Font("Arial", 14)

        pnlMainBodyList2.Location = New Point(0, lstMainBodyProducts.Bottom)
        pnlMainBodyList2.Size = New Size(360, 40)
        pnlMainBodyList2.BorderStyle = BorderStyle.FixedSingle
        pnlMainBodyList2.Cursor = Cursors.Hand

        lblMainBodyList2Name.Text = "Votre deuxième liste" 'A modifier en fonction du nom de la liste 2 de l'user
        lblMainBodyList2Name.Location = New Point(2, pnlMainBodyList2.Location.Y + 5)
        lblMainBodyList2Name.AutoSize = True
        lblMainBodyList2Name.Font = New Drawing.Font("Tahoma", 16)
        lblMainBodyList2Name.Cursor = Cursors.Hand

        pnlMainBodyList3.Location = New Point(0, pnlMainBodyList2.Bottom)
        pnlMainBodyList3.Size = New Size(360, 40)
        pnlMainBodyList3.BorderStyle = BorderStyle.FixedSingle
        pnlMainBodyList3.Cursor = Cursors.Hand

        lblMainBodyList3Name.Text = "Votre troisième liste" 'A modifier en fonction du nom de la liste 3 de l'user
        lblMainBodyList3Name.Location = New Point(2, pnlMainBodyList3.Location.Y + 5)
        lblMainBodyList3Name.AutoSize = True
        lblMainBodyList3Name.Font = New Drawing.Font("Tahoma", 16)
        lblMainBodyList3Name.Cursor = Cursors.Hand

        btnMainBodyAjouterAuPanier.Text = "AJOUTER AU PANIER"
        btnMainBodyAjouterAuPanier.AutoSize = True
        btnMainBodyAjouterAuPanier.Location = New Point(100, 500)
        btnMainBodyAjouterAuPanier.Font = New Drawing.Font("Arial", 14, FontStyle.Bold, FontHeight = 22)
        btnMainBodyAjouterAuPanier.FlatStyle = FlatStyle.Flat
        btnMainBodyAjouterAuPanier.Cursor = Cursors.Hand

        pnlMainBody = New Panel
        pnlMainBody.AutoSize = True
        pnlMainBody.Controls.Add(lblMainBodyMesListes)
        pnlMainBody.Controls.Add(lblMainBodyFavListName)
        pnlMainBody.Controls.Add(pnlMainBodyFavListBackground)
        pnlMainBody.Controls.Add(lstMainBodyProducts)
        pnlMainBody.Controls.Add(lblMainBodyList2Name)
        pnlMainBody.Controls.Add(pnlMainBodyList2)
        pnlMainBody.Controls.Add(lblMainBodyList3Name)
        pnlMainBody.Controls.Add(pnlMainBodyList3)
        pnlMainBody.Controls.Add(btnMainBodyAjouterAuPanier)

        Controls.Add(pnlMainBody)

        'Footer

        btnMainFooterCreer.Text = "CREER"
        btnMainFooterCreer.Location = New Point(-1, 605)
        btnMainFooterCreer.AutoSize = True
        btnMainFooterCreer.Font = New Font("Arial", 21, FontStyle.Regular, FontHeight = 22)
        btnMainFooterCreer.FlatStyle = FlatStyle.Flat
        btnMainFooterCreer.Cursor = Cursors.Hand

        btnMainFooterModifier.Text = "MODIFIER"
        btnMainFooterModifier.Location = New Point(btnMainFooterCreer.Width + 18, btnMainFooterCreer.Location.Y)
        btnMainFooterModifier.AutoSize = True
        btnMainFooterModifier.Font = New Font("Arial", 21, FontStyle.Regular, FontHeight = 22)
        btnMainFooterModifier.FlatStyle = FlatStyle.Flat
        btnMainFooterModifier.Cursor = Cursors.Hand

        btnMainFooterSupprimer.Text = "SUPPRIMER"
        btnMainFooterSupprimer.AutoSize = True
        btnMainFooterSupprimer.Location = New Point(btnMainFooterModifier.Width + btnMainFooterCreer.Width + 66, btnMainFooterCreer.Location.Y)
        btnMainFooterSupprimer.Font = New Font("Arial", 21, FontStyle.Regular, FontHeight = 22)
        btnMainFooterSupprimer.FlatStyle = FlatStyle.Flat
        btnMainFooterSupprimer.Cursor = Cursors.Hand

        pnlMainFooter = New Panel
        pnlMainFooter.AutoSize = True
        pnlMainFooter.Controls.Add(btnMainFooterCreer)
        pnlMainFooter.Controls.Add(btnMainFooterModifier)
        pnlMainFooter.Controls.Add(btnMainFooterSupprimer)

        Controls.Add(pnlMainFooter)

        ' Gest des evts
        AddHandler picAllHeaderBack.Click,
            AddressOf DisplayPreviousScreen

        AddHandler btnMainFooterCreer.Click,
            AddressOf DisplayCreate

        AddHandler btnMainFooterModifier.Click,
            AddressOf DisplayModify

        AddHandler btnMainFooterSupprimer.Click,
            AddressOf DisplayDelete

    End Sub

    Private Sub DisplayListBox(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlMainBodyFavListBackground.Click, lblMainBodyFavListName.Click,
            pnlMainBodyList2.Click, lblMainBodyList2Name.Click,
            pnlMainBodyList3.Click, lblMainBodyList3Name.Click

        Dim widgetSender
        pnlMainBodyFavListBackground.Name = "fav"
        lblMainBodyFavListName.Name = "fav"
        pnlMainBodyList2.Name = "lst2"
        lblMainBodyList2Name.Name = "lst2"

        If sender.GetType.ToString = "System.Windows.Forms.Panel" Then
            widgetSender = New Panel
            widgetSender = CType(sender, Panel)
        Else
            widgetSender = New Label
            widgetSender = CType(sender, Label)
        End If

        Select Case widgetSender.Name
            Case "fav"

                ' Gérer les listes à afficher

                If _sMainListBoxPosition = "fav" And bListBoxDisplay Then
                    bListBoxDisplay = False
                    lstMainBodyProducts.Hide()
                    pnlMainBodyList2.Location = New Point(0, pnlMainBodyFavListBackground.Bottom)
                    lblMainBodyList2Name.Location = New Point(2, pnlMainBodyFavListBackground.Bottom)
                    pnlMainBodyList3.Location = New Point(0, pnlMainBodyList2.Bottom)
                    lblMainBodyList3Name.Location = New Point(2, pnlMainBodyList2.Bottom)

                Else
                    bListBoxDisplay = True
                    lstMainBodyProducts.Show()
                    lstMainBodyProducts.Location = New Point(10, pnlMainBodyFavListBackground.Bottom)
                    pnlMainBodyList2.Location = New Point(0, lstMainBodyProducts.Bottom)
                    lblMainBodyList2Name.Location = New Point(2, pnlMainBodyList2.Location.Y + 5)
                    pnlMainBodyList3.Location = New Point(0, pnlMainBodyList2.Bottom)
                    lblMainBodyList3Name.Location = New Point(2, pnlMainBodyList3.Location.Y + 5)
                    _sMainListBoxPosition = "fav"
                    If lblMainBodyFavListName.Text = "Votre première liste" Or lblMainBodyFavListName.Text = "Votre liste" Then
                        lstMainBodyProducts.Items.Clear()
                    Else
                        FillList()
                    End If


                End If
            Case "lst2"

                ' Gérer les listes à afficher

                If _sMainListBoxPosition = "lst2" And bListBoxDisplay Then
                    bListBoxDisplay = False
                    lstMainBodyProducts.Hide()
                    pnlMainBodyList3.Location = New Point(0, pnlMainBodyList2.Bottom)
                    lblMainBodyList3Name.Location = New Point(2, pnlMainBodyList2.Bottom)
                Else
                    bListBoxDisplay = True
                    lstMainBodyProducts.Show()
                    pnlMainBodyList2.Location = New Point(0, pnlMainBodyFavListBackground.Bottom)
                    lblMainBodyList2Name.Location = New Point(2, pnlMainBodyFavListBackground.Bottom)
                    lstMainBodyProducts.Location = New Point(10, pnlMainBodyList2.Bottom)
                    pnlMainBodyList3.Location = New Point(0, lstMainBodyProducts.Bottom)
                    lblMainBodyList3Name.Location = New Point(2, lstMainBodyProducts.Bottom)
                    _sMainListBoxPosition = "lst2"
                    If lblMainBodyList2Name.Text = "Votre deuxième liste" Or lblMainBodyList2Name.Text = "Votre liste" Then
                        lstMainBodyProducts.Items.Clear()
                    Else
                        FillList()
                    End If
                End If
            Case Else

                ' Gérer les listes à afficher

                If _sMainListBoxPosition = "lst3" And bListBoxDisplay Then
                    bListBoxDisplay = False
                    lstMainBodyProducts.Hide()
                Else
                    pnlMainBodyList2.Location = New Point(0, pnlMainBodyFavListBackground.Bottom)
                    lblMainBodyList2Name.Location = New Point(2, pnlMainBodyFavListBackground.Bottom)
                    pnlMainBodyList3.Location = New Point(0, pnlMainBodyList2.Bottom)
                    lblMainBodyList3Name.Location = New Point(2, pnlMainBodyList2.Bottom)
                    lstMainBodyProducts.Show()
                    bListBoxDisplay = True
                    lstMainBodyProducts.Location = New Point(10, pnlMainBodyList3.Bottom)
                    _sMainListBoxPosition = "lst3"
                    If lblMainBodyList3Name.Text = "Votre troisième liste" Or lblMainBodyList3Name.Text = "Votre liste" Then
                        lstMainBodyProducts.Items.Clear()
                    Else
                        FillList()
                    End If
                End If
        End Select
    End Sub


    Private Sub FillList()
        Select Case _sMainListBoxPosition
            Case "fav"

                Dim i = 0
                While lblMainBodyFavListName.Text <> lMesListes.Item(i).Item(0)
                    i = i + 1
                End While

                lstMainBodyProducts.Items.Clear()

                For Each prod In lMesListes.Item(i)
                    If prod <> lblMainBodyFavListName.Text Then
                        lstMainBodyProducts.Items.Add(prod)
                    End If
                Next

            Case "lst2"

                Dim i = 0
                While lblMainBodyList2Name.Text <> lMesListes.Item(i).Item(0)
                    i = i + 1
                End While

                lstMainBodyProducts.Items.Clear()

                For Each prod In lMesListes.Item(i)
                    If prod <> lblMainBodyList2Name.Text Then
                        lstMainBodyProducts.Items.Add(prod)
                    End If
                Next

            Case "lst3"

                Dim i = 0
                While lblMainBodyList3Name.Text <> lMesListes.Item(i).Item(0)
                    i = i + 1
                End While

                lstMainBodyProducts.Items.Clear()

                For Each prod In lMesListes.Item(i)
                    If prod <> lblMainBodyList3Name.Text Then
                        lstMainBodyProducts.Items.Add(prod)
                    End If
                Next

        End Select
    End Sub

    Private Sub DisplaySettings()

        Dim picSettingExit As New PictureBox
        pnlSettingBody = New Panel
        pnlSettingFooter = New Panel


        If _sAllCurrentScreen = "MAIN" Then
            pnlMainBody.Hide()
            pnlMainFooter.Hide()
        ElseIf _sAllCurrentScreen = "CREATE" Then
            pnlCreateBody.Hide()
            pnlCreateFooter.Hide()
        ElseIf _sAllCurrentScreen = "MODIFY" Then
            pnlModifyBody.Hide()
            pnlModifyFooter.Hide()
        ElseIf _sAllCurrentScreen = "DELETE" Then
            pnlDeleteBody.Hide()
            pnlDeleteFooter.Hide()
        End If

        If Not _sAllCurrentScreen = "SETTINGS" Then
            _sAllPreviousScreen = _sAllCurrentScreen
            _sAllCurrentScreen = "SETTINGS"
        End If

        picAllHeaderBack.Show()

        picSettingExit.Image = Image.FromFile("ExitScanList.png")
        picSettingExit.Size = picSettingExit.Image.Size
        picSettingExit.Location = New Point(155, 585)
        picSettingExit.Cursor = Cursors.Hand

        AddHandler picSettingExit.Click,
            AddressOf Application.Exit

        pnlSettingBody.AutoSize = True
        pnlSettingBody.Controls.Add(picSettingExit)
        Controls.Add(pnlSettingBody)
    End Sub

    Private Sub DisplayCreate()

        pnlCreateBody = New Panel
        pnlCreateFooter = New Panel
        txtCreateNomListe = New TextBox
        Dim lblCreateSeparator As New Label
        lstCreateProduct = New ListBox
        picCreatePlus = New PictureBox
        picCreateMinus = New PictureBox
        picCreateCheckmark = New PictureBox

        If _sAllCurrentScreen = "MAIN" Then
            pnlMainBody.Hide()
            pnlMainFooter.Hide()
        ElseIf _sAllCurrentScreen = "SETTINGS" Then
            pnlSettingBody.Hide()
            pnlSettingFooter.Hide()
        End If
        If Not _sAllCurrentScreen = "CREATE" Then
            _sAllPreviousScreen = _sAllCurrentScreen
            _sAllCurrentScreen = "CREATE"
        End If

        picAllHeaderBack.Show()

        pnlCreateBody.AutoSize = True

        txtCreateNomListe.Text = "Nom de la liste"
        txtCreateNomListe.TextAlign = HorizontalAlignment.Center
        txtCreateNomListe.ForeColor = Color.DarkGray
        txtCreateNomListe.Font = New Font("Arial", 16, FontStyle.Italic)
        txtCreateNomListe.Size = New Size(200, 50)
        txtCreateNomListe.Location = New Point(pnlAllHeader.Width / 2 - txtCreateNomListe.Width / 2, pnlAllHeader.Bottom)
        txtCreateNomListe.Cursor = Cursors.IBeam


        lblCreateSeparator.Text = "____________________________________________________________________"
        lblCreateSeparator.ForeColor = Color.DarkGray
        lblCreateSeparator.Location = New Point((txtCreateNomListe.Left + txtCreateNomListe.Right) / 2 - lblCreateSeparator.Width / 2, txtCreateNomListe.Bottom + 15)

        lstCreateProduct.Size = New Size(300, 300)
        lstCreateProduct.Location = New Point(30, lblCreateSeparator.Bottom + 15)
        lstCreateProduct.Font = New Font("Arial", 14)

        picCreatePlus.Image = Image.FromFile("PlusScanList.png")
        picCreatePlus.Size = picCreatePlus.Image.Size
        picCreatePlus.Location = New Point((lstCreateProduct.Left + lstCreateProduct.Right) / 2 - (picCreatePlus.Width + 15), lstCreateProduct.Bottom + 15)
        picCreatePlus.Cursor = Cursors.Hand

        picCreateMinus.Image = Image.FromFile("MinusScanList.png")
        picCreateMinus.Size = picCreateMinus.Image.Size
        picCreateMinus.Location = New Point(picCreatePlus.Right + 15, picCreatePlus.Location.Y)
        picCreateMinus.Cursor = Cursors.Hand

        pnlCreateBody.Controls.Add(txtCreateNomListe)
        pnlCreateBody.Controls.Add(lblCreateSeparator)
        pnlCreateBody.Controls.Add(lstCreateProduct)
        pnlCreateBody.Controls.Add(picCreatePlus)
        pnlCreateBody.Controls.Add(picCreateMinus)

        Controls.Add(pnlCreateBody)

        pnlCreateFooter.AutoSize = True

        picCreateCheckmark.Image = Image.FromFile("CheckmarkScanList.png")
        picCreateCheckmark.Size = picCreateCheckmark.Image.Size
        picCreateCheckmark.Location = New Point(picCreatePlus.Right + picCreateCheckmark.Left / 2 - 15, picCreatePlus.Bottom + 25)
        picCreateCheckmark.Cursor = Cursors.Hand

        pnlCreateFooter.Controls.Add(picCreateCheckmark)

        Controls.Add(pnlCreateFooter)

        AddHandler txtCreateNomListe.Click,
            AddressOf txtCreateNomListe.Clear

        lMaListe = New List(Of String)
        lMaListe.Add(txtCreateNomListe.Text) 'Initialise & réserve le premier emplacement de la futur liste au nom de celle-ci

    End Sub

    Private Sub DisplayModify()

        pnlModifyBody = New Panel
        pnlModifyFooter = New Panel
        txtModifyNomListe = New TextBox
        Dim lblModifySeparator As New Label
        lstModifyProduct = New ListBox
        picModifyPlus = New PictureBox
        picModifyMinus = New PictureBox
        picModifyCheckmark = New PictureBox
        iListeSelectionnee = New Integer

        If _sAllCurrentScreen = "MAIN" Then
            pnlMainBody.Hide()
            pnlMainFooter.Hide()
        ElseIf _sAllCurrentScreen = "SETTINGS" Then
            pnlSettingBody.Hide()
            pnlSettingFooter.Hide()
        End If
        If Not _sAllCurrentScreen = "MODIFY" Then
            _sAllPreviousScreen = _sAllCurrentScreen
            _sAllCurrentScreen = "MODIFY"
        End If

        picAllHeaderBack.Show()

        Select Case _sMainListBoxPosition
            Case "fav"
                If lblMainBodyFavListName.Text = "Votre première liste" Then
                    MsgBox("Vous ne pouvez pas modifier cette liste, vous devez d'abord en créer une !", MsgBoxStyle.Exclamation, "Modification de liste")
                    DisplayPreviousScreen()
                Else
                    txtModifyNomListe.Text = lblMainBodyFavListName.Text
                    iListeSelectionnee = 0
                End If
            Case "lst2"
                If lblMainBodyList2Name.Text = "Votre deuxième liste" Then
                    MsgBox("Vous ne pouvez pas modifier cette liste, vous devez d'abord en créer une !", MsgBoxStyle.Exclamation, "Modification de liste")
                    DisplayPreviousScreen()
                Else
                    txtModifyNomListe.Text = lblMainBodyList2Name.Text
                    iListeSelectionnee = 1
                End If
            Case "lst3"
                If lblMainBodyList3Name.Text = "Votre troisième liste" Then
                    MsgBox("Vous ne pouvez pas modifier cette liste, vous devez d'abord en créer une !", MsgBoxStyle.Exclamation, "Modification de liste")
                    DisplayPreviousScreen()
                Else
                    txtModifyNomListe.Text = lblMainBodyList3Name.Text
                    iListeSelectionnee = 2
                End If
        End Select
        For Each prod In lstMainBodyProducts.Items
            lstModifyProduct.Items.Add(prod)
        Next

        txtModifyNomListe.TextAlign = HorizontalAlignment.Center
        txtModifyNomListe.ForeColor = Color.DarkGray
        txtModifyNomListe.Font = New Font("Arial", 16, FontStyle.Regular)
        txtModifyNomListe.Size = New Size(200, 50)
        txtModifyNomListe.Location = New Point(pnlAllHeader.Width / 2 - txtModifyNomListe.Width / 2, pnlAllHeader.Bottom)
        txtModifyNomListe.Cursor = Cursors.IBeam

        lblModifySeparator.Text = "____________________________________________________________________"
        lblModifySeparator.ForeColor = Color.DarkGray
        lblModifySeparator.Location = New Point((txtModifyNomListe.Left + txtModifyNomListe.Right) / 2 - lblModifySeparator.Width / 2, txtModifyNomListe.Bottom + 15)

        lstModifyProduct.Size = New Size(300, 300)
        lstModifyProduct.Location = New Point(30, lblModifySeparator.Bottom + 15)
        lstModifyProduct.Font = New Font("Arial", 14)

        picModifyPlus.Image = Image.FromFile("PlusScanList.png")
        picModifyPlus.Size = picModifyPlus.Image.Size
        picModifyPlus.Location = New Point((lstModifyProduct.Left + lstModifyProduct.Right) / 2 - (picModifyPlus.Width + 15), lstModifyProduct.Bottom + 15)
        picModifyPlus.Cursor = Cursors.Hand

        picModifyMinus.Image = Image.FromFile("MinusScanList.png")
        picModifyMinus.Size = picModifyMinus.Image.Size
        picModifyMinus.Location = New Point(picModifyPlus.Right + 15, picModifyPlus.Location.Y)
        picModifyMinus.Cursor = Cursors.Hand

        pnlModifyBody.AutoSize = True
        pnlModifyBody.Controls.Add(txtModifyNomListe)
        pnlModifyBody.Controls.Add(lblModifySeparator)
        pnlModifyBody.Controls.Add(lstModifyProduct)
        pnlModifyBody.Controls.Add(picModifyPlus)
        pnlModifyBody.Controls.Add(picModifyMinus)

        Controls.Add(pnlModifyBody)

        picModifyCheckmark.Image = Image.FromFile("CheckmarkScanList.png")
        picModifyCheckmark.Size = picModifyCheckmark.Image.Size
        picModifyCheckmark.Location = New Point(picModifyPlus.Right + picModifyCheckmark.Left / 2 - 15, picModifyPlus.Bottom + 25)
        picModifyCheckmark.Cursor = Cursors.Hand

        pnlModifyFooter.AutoSize = True
        pnlModifyFooter.Controls.Add(picModifyCheckmark)

        Controls.Add(pnlModifyFooter)

    End Sub

#Region "Partie suppression de liste"
    Private Sub DisplayDelete()

        pnlDeleteBody = New Panel
        pnlDeleteFooter = New Panel
        Dim lblDeleteMesListes As New Label
        Dim lblDeleteSeparator As New Label
        lstDeleteLists = New ListBox
        Dim picDeleteRem As New PictureBox

        If _sAllCurrentScreen = "MAIN" Then
            pnlMainBody.Hide()
            pnlMainFooter.Hide()
        ElseIf _sAllCurrentScreen = "SETTINGS" Then
            pnlSettingBody.Hide()
            pnlSettingFooter.Hide()
        End If
        If Not _sAllCurrentScreen = "DELETE" Then
            _sAllPreviousScreen = _sAllCurrentScreen
            _sAllCurrentScreen = "DELETE"
        End If

        picAllHeaderBack.Show()

        lblDeleteMesListes.AutoSize = True
        lblDeleteMesListes.Text = "MES LISTES"
        lblDeleteMesListes.Font = New Font("Segoe UI", 16, FontStyle.Italic)
        lblDeleteMesListes.Height = 20
        lblDeleteMesListes.ForeColor = Color.DarkGray
        lblDeleteMesListes.Location = New Point(120, pnlAllHeader.Bottom + 15)

        lblDeleteSeparator.Text = "____________________________________________________________________"
        lblDeleteSeparator.ForeColor = Color.DarkGray
        lblDeleteSeparator.Location = New Point(130, lblDeleteMesListes.Bottom + 15)

        lstDeleteLists.Size = New Size(300, 300)
        lstDeleteLists.Location = New Point(30, lblDeleteSeparator.Bottom + 15)
        lstDeleteLists.Font = New Font("Arial", 14)

        pnlDeleteBody.AutoSize = True
        pnlDeleteBody.Controls.Add(lblDeleteMesListes)
        pnlDeleteBody.Controls.Add(lblDeleteSeparator)
        pnlDeleteBody.Controls.Add(lstDeleteLists)

        Controls.Add(pnlDeleteBody)


        picDeleteRem.Image = Image.FromFile("DeleteScanList.png")
        picDeleteRem.Size = picDeleteRem.Image.Size
        picDeleteRem.Location = New Point(Width / 2 - picDeleteRem.Width / 2, lstDeleteLists.Bottom + 40)
        picDeleteRem.Cursor = Cursors.Hand

        pnlDeleteFooter.AutoSize = True
        pnlDeleteFooter.Controls.Add(picDeleteRem)

        Controls.Add(pnlDeleteFooter)

        For Each Liste In lMesListes
            lstDeleteLists.Items.Add(Liste.Item(0))
        Next

        AddHandler picDeleteRem.Click,
            AddressOf DeleteList

    End Sub

    Private Sub DeleteList()
        Dim bListeDansAccueil = False
        Dim positionDansAccueil As Control
        Dim listeSelec = lstDeleteLists.SelectedItem
        Dim i = 0
        Dim listeTrouvee = True

        Select Case listeSelec

            Case lblMainBodyFavListName.Text
                bListeDansAccueil = True
                positionDansAccueil = lblMainBodyFavListName

            Case lblMainBodyList2Name.Text
                bListeDansAccueil = True
                positionDansAccueil = lblMainBodyList2Name

            Case lblMainBodyList3Name.Text
                bListeDansAccueil = True
                positionDansAccueil = lblMainBodyList3Name

        End Select

        If lstDeleteLists.SelectedItem Is Nothing Then
            MsgBox("Erreur aucune liste sélectionnée !", MsgBoxStyle.Exclamation, "Erreur lors de la recherche de liste")
            DisplayPreviousScreen()
        Else
            While Not listeSelec = lMesListes.Item(i).Item(0)
                If i = lMesListes.Count - 1 Then
                    listeTrouvee = False
                    Return
                Else
                    i = i + 1
                End If
            End While

            If listeTrouvee = False Then
                MsgBox("Erreur liste non trouvée !", MsgBoxStyle.Critical, "Erreur lors de la recherche de liste")
                DisplayPreviousScreen()
            ElseIf listeTrouvee Then
                If bListeDansAccueil Then
                    positionDansAccueil.Text = "Votre liste"
                    lstMainBodyProducts.Items.Clear()
                End If
                lMesListes.Remove(lMesListes.Item(i))
                lstDeleteLists.Items.Remove(lstDeleteLists.SelectedItem)
                MsgBox("Liste supprimée", MsgBoxStyle.Information, "Suppression de liste")
                RemplaceDefaultList()
            End If
        End If
    End Sub
#End Region


    Private Sub AddRemOrCreate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picCreatePlus.Click, picCreateMinus.Click, picCreateCheckmark.Click
        Dim picClicked As New PictureBox

        picCreatePlus.Name = "PLUS"
        picCreateMinus.Name = "MINUS"
        picCreateCheckmark.Name = "CREATE"

        picClicked = CType(sender, PictureBox)

        Select Case picClicked.Name
            Case "PLUS"
                Dim sProductName As String
                sProductName = InputBox("Entrez le nom du produit : ", MsgBoxStyle.OkCancel)
                lstCreateProduct.Items.Add(sProductName)
            Case "MINUS"
                If lstCreateProduct.SelectedItem = Nothing Then
                    MsgBox("Aucun produit sélectionné.", MsgBoxStyle.Exclamation, "Création de liste")
                Else
                    lstCreateProduct.Items.Remove(lstCreateProduct.SelectedItem)
                End If
            Case "CREATE"
                If txtCreateNomListe.Text = "Nom de la liste" Or txtCreateNomListe.Text = "" Or txtCreateNomListe.Text = "Votre première liste" Or txtCreateNomListe.Text = "Votre deuxième liste" Or txtCreateNomListe.Text = "Votre troisième liste" Then
                    MsgBox("Veuillez indiquer un nom pour la liste.", MsgBoxStyle.Exclamation, "Création de liste")
                Else
                    For Each nomListe In lMesListes
                        If txtCreateNomListe.Text = nomListe.Item(0) Then
                            MsgBox("Nom de liste déjà pris !", MsgBoxStyle.Exclamation, "Création de liste")
                            Return
                        End If
                    Next
                    lMaListe.Item(0) = txtCreateNomListe.Text
                    For Each prod In lstCreateProduct.Items
                        lMaListe.Add(prod)
                    Next
                    lMesListes.Add(lMaListe)
                    MsgBox("Liste bien créée", MsgBoxStyle.Information, "Création de liste")
                    _sMainListBoxPosition = "fav"
                    RemplaceDefaultList()
                    FillList()
                    DisplayPreviousScreen()
                End If
        End Select
    End Sub

    Private Sub AddRemOrModify(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picModifyPlus.Click, picModifyMinus.Click, picModifyCheckmark.Click
        Dim picClicked As New PictureBox

        picModifyPlus.Name = "PLUS"
        picModifyMinus.Name = "MINUS"
        picModifyCheckmark.Name = "MODIFY"

        picClicked = CType(sender, PictureBox)

        Select Case picClicked.Name
            Case "PLUS"
                Dim sProductName As String
                sProductName = InputBox("Entrez le nom du produit : ", MsgBoxStyle.OkCancel)
                lstModifyProduct.Items.Add(sProductName)
            Case "MINUS"
                If lstModifyProduct.SelectedItem = Nothing Then
                    MsgBox("Aucun produit sélectionné.", MsgBoxStyle.Exclamation, "Modification de liste")
                Else
                    lstModifyProduct.Items.Remove(lstModifyProduct.SelectedItem)
                End If
            Case "MODIFY"
                If txtModifyNomListe.Text = "" Then
                    MsgBox("Veuillez indiquer un nom pour la liste.", MsgBoxStyle.Exclamation, "Modification de liste")
                Else
                    lMesListes.Item(iListeSelectionnee).Clear()
                    lMesListes.Item(iListeSelectionnee).Add(txtModifyNomListe.Text)
                    For Each prod In lstModifyProduct.Items
                        lMesListes.Item(iListeSelectionnee).Add(prod)
                    Next
                    MsgBox("Liste bien modifiée", MsgBoxStyle.Information, "Modification de liste")
                    _sMainListBoxPosition = "fav"
                    RemplaceDefaultList()
                    FillList()
                    DisplayPreviousScreen()
                End If
        End Select
    End Sub

    Private Sub RemplaceDefaultList()
        If lMesListes.Count <= 3 Then
            Dim i = 0
            For Each list In lMesListes
                Select Case i
                    Case 0
                        lblMainBodyFavListName.Text = list.Item(0)
                        i = i + 1
                    Case 1
                        lblMainBodyList2Name.Text = list.Item(0)
                        i = i + 1
                    Case 2
                        lblMainBodyList3Name.Text = list.Item(0)
                End Select
            Next

        End If
    End Sub

    'Méthode pour retour en arrière, trouver le screen à afficher en fonction de la variable currentScreen 
    Private Sub DisplayPreviousScreen()
        Select Case _sAllCurrentScreen
            Case "SETTINGS"
                pnlSettingBody.Hide()
                pnlSettingFooter.Hide()
            Case "CREATE"
                pnlCreateBody.Hide()
                pnlCreateFooter.Hide()
            Case "MODIFY"
                pnlModifyBody.Hide()
                pnlModifyFooter.Hide()
            Case "DELETE"
                pnlDeleteBody.Hide()
                pnlDeleteFooter.Hide()
        End Select
        Select Case _sAllPreviousScreen
            Case "MAIN"
                pnlMainBody.Show()
                pnlMainFooter.Show()
                picAllHeaderBack.Hide()
                _sAllCurrentScreen = "MAIN"
            Case "CREATE"
                pnlCreateBody.Show()
                pnlCreateFooter.Show()
                _sAllPreviousScreen = "MAIN"
                _sAllCurrentScreen = "CREATE"
            Case "MODIFY"
                pnlModifyBody.Show()
                pnlModifyFooter.Show()
                _sAllPreviousScreen = "MAIN"
                _sAllCurrentScreen = "MODIFY"
            Case "DELETE"
                pnlDeleteBody.Show()
                pnlDeleteFooter.Show()
                _sAllPreviousScreen = "MAIN"
                _sAllCurrentScreen = "DELETE"
        End Select
    End Sub
End Class