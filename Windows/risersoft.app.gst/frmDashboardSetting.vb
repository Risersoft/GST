Imports System.Drawing
Imports System.Windows.Forms
Imports Infragistics.Win.Misc
Imports Infragistics.Win.UltraWinCarousel
Imports Infragistics.Win.UltraWinEditors
Imports Infragistics.Win.UltraWinGrid
Imports Newtonsoft.Json
Imports risersoft.app.mxform.gst
Imports risersoft.shared.Extensions
Imports uwg = Infragistics.Win.UltraWinGrid

Public Class frmDashboardSetting
    Inherits frmMax
    Dim lst2 As New DashboardSettingModel, lst As List(Of DashletSettingModel)
    Private Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        Me.UltraTilePanel1.Tiles.Clear()
        Me.UltraTilePanel1.MaximumColumns = 3
        Me.UltraTilePanel1.MinimumColumns = 3
        Me.UltraTilePanel1.TileSettings.ShowCloseButton = Infragistics.Win.DefaultableBoolean.True
        Me.UltraTilePanel1.MinimumTileSize = New Drawing.Size(100, 100)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmDashboardSettingModel = Me.InitData("frmDashboardSettingModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            lst = myFuncs2.AddChartStaticList(Me.Controller)

            'UltraTilePanel1.BringToFront()

            'Basic config of carousel control
            UltraCarousel1.Items.Clear()
            UltraCarousel1.ItemSize = New Drawing.Size(300, 200)
            UltraCarousel1.ItemSettings.ImageSize = New Drawing.Size(300, 150)

            For Each obj In lst
                Dim item As New CarouselItem()
                item.Text = obj.Name
                Dim img = myAssy2.ImageFromEmbed("risersoft.shared.resource", obj.ImageFile)
                item.Settings.Appearance.Image = img
                item.Tag = obj.Code
                'Add entry to carousel
                UltraCarousel1.Items.Add(item)
            Next

            If frmMode = EnumfrmMode.acAddM Then
                cmb_CompanyID.Visible = False
                UltraLabelCompany.Visible = False
                cmb_GSTRegID.Visible = False
                UltraLabelGSTReg.Visible = False
            End If

            If frmMode = EnumfrmMode.acEditM Then
                If myRow("FieldName") = "CompanyID" Then
                    cmb_CompanyID.Visible = True
                    UltraLabelCompany.Visible = True
                    cmb_CompanyID.Value = myRow("FieldValue")
                Else
                    cmb_CompanyID.Visible = False
                    UltraLabelCompany.Visible = False
                End If
                If myRow("FieldName") = "GSTRegID" Then
                    cmb_GSTRegID.Visible = True
                    UltraLabelGSTReg.Visible = True
                    cmb_GSTRegID.Value = myRow("FieldValue")
                Else
                    cmb_GSTRegID.Visible = False
                    UltraLabelGSTReg.Visible = False
                End If
                If myRow("FieldName") = "TenantID" Then
                    cmb_CompanyID.Visible = False
                    UltraLabelCompany.Visible = False
                    cmb_GSTRegID.Visible = False
                    UltraLabelGSTReg.Visible = False
                End If
            End If

            If myUtils.cStrTN(myRow("layoutjson")).Trim.Length > 0 Then
                lst2 = JsonConvert.DeserializeObject(Of DashboardSettingModel)(myUtils.cStrTN(myRow("layoutjson")))
                If (Not lst2 Is Nothing) AndAlso (Not lst2.WidgetSettings Is Nothing) Then
                    For Each obj In lst2.WidgetSettings
                        Dim obj1 = lst.Where(Function(x) myUtils.IsInList(x.Code, obj.ChartCode)).FirstOrDefault
                        If Not obj1 Is Nothing Then
                            Dim img = myAssy2.ImageFromEmbed("risersoft.shared.resources", obj1.ImageFile)
                            AddWidget(obj.ChartCode, obj.ChartName, img)
                        End If
                    Next

                End If
            End If


            Me.FormPrepared = True

        End If
            Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myWinSQL.AssignCmb(Me.Model.dsCombo, "Company", "", Me.cmb_CompanyID)
            myWinSQL.AssignCmb(Me.Model.dsCombo, "GstReg", "", Me.cmb_GSTRegID)
            Me.cmb_FieldName.ValueList = Me.Model.ValueLists("FieldName").ComboList

            Return True
        End If
        Return False
    End Function

    Private Sub cmb_FieldName_Leave(sender As Object, e As EventArgs) Handles cmb_FieldName.Leave, cmb_FieldName.AfterCloseUp
        GetLevels(myUtils.cStrTN(Me.cmb_FieldName.Value), myUtils.cValTN(cmb_CompanyID.Value), myUtils.cValTN(Me.cmb_GSTRegID.Value))
    End Sub

    Private Sub cmb_CompanyID_Leave(sender As Object, e As EventArgs) Handles cmb_CompanyID.Leave, cmb_CompanyID.AfterCloseUp
        GetLevels(myUtils.cStrTN(Me.cmb_FieldName.Value), myUtils.cValTN(cmb_CompanyID.Value), myUtils.cValTN(Me.cmb_GSTRegID.Value))
    End Sub

    Private Sub cmb_GSTRegID_Leave(sender As Object, e As EventArgs) Handles cmb_GSTRegID.Leave, cmb_GSTRegID.AfterCloseUp
        GetLevels(myUtils.cStrTN(Me.cmb_FieldName.Value), myUtils.cValTN(cmb_CompanyID.Value), myUtils.cValTN(Me.cmb_GSTRegID.Value))
    End Sub

    Private Function GetLevels(FieldName As String, CompanyID As Integer, GSTRegID As Integer)
        If myUtils.IsInList(myUtils.cStrTN(FieldName), "TenantID") Then
            cmb_CompanyID.Visible = False
            UltraLabelCompany.Visible = False
            cmb_GSTRegID.Visible = False
            UltraLabelGSTReg.Visible = False
        End If
        If myUtils.IsInList(myUtils.cStrTN(FieldName), "CompanyID") Then
            cmb_CompanyID.Visible = True
            UltraLabelCompany.Visible = True
            cmb_GSTRegID.Visible = False
            UltraLabelGSTReg.Visible = False
        End If
        If myUtils.IsInList(myUtils.cStrTN(FieldName), "GSTRegID") Then
            cmb_GSTRegID.Visible = True
            UltraLabelGSTReg.Visible = True
            cmb_CompanyID.Visible = False
            UltraLabelCompany.Visible = False
        End If

    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        cm.EndCurrentEdit()
        If Me.ValidateData() Then

            If Not cmb_FieldName.SelectedItem Is Nothing Then
                If cmb_FieldName.Value = "CompanyID" Then
                    myRow("FieldValue") = myUtils.cValTN(cmb_CompanyID.Value)
                ElseIf cmb_FieldName.Value = "GSTRegID" Then
                    myRow("FieldValue") = myUtils.cValTN(cmb_GSTRegID.Value)
                Else
                    myRow("FieldValue") = 0
                End If
            End If

            lst2.WidgetSettings = New List(Of WidgetSetting)
            lst2.WidgetSettings.Clear()
            For Each t As UltraTile In Me.UltraTilePanel1.Tiles
                    Dim obj1 = lst.Where(Function(x) myUtils.IsInList(x.Code, t.Tag)).FirstOrDefault
                    If Not obj1 Is Nothing Then
                        lst2.WidgetSettings.Add(New WidgetSetting() With {.ChartCode = obj1.Code, .ChartName = obj1.Name, .ChartType = obj1.ChartType})
                    End If
                Next
                myRow("LayoutJson") = JsonConvert.SerializeObject(lst2)



            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub UltraTilePanel1_TileStateChanged(sender As Object, e As TileStateChangedEventArgs)

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If Not myUtils.NullNot(UltraCarousel1.ActiveItem) Then
            Me.AddWidget(UltraCarousel1.ActiveItem.Tag, UltraCarousel1.ActiveItem.Text, UltraCarousel1.ActiveItem.Settings.Appearance.Image)
        End If
    End Sub

    Public Sub AddWidget(Code As String, Name As String, img As Image)
        Dim t = New UltraTile()
        't.PositionInNormalMode = New System.Drawing.Point(0, 0)
        Me.UltraTilePanel1.Tiles.Add(t)
        t.Tag = Code
        t.Caption = Name
        'AddHandler pb.Click, Sub(s, e2)
        '                         lstpb.ForEach(Sub(x) x.BorderStyle = BorderStyle.None)
        '                         pb.BorderStyle = BorderStyle.FixedSingle
        '                         spb = pb
        '                     End Sub
        Dim pb As New PictureBox
        pb.Image = img
        pb.SizeMode = PictureBoxSizeMode.Zoom
        t.Control = pb
        t.SetState(TileState.Normal, True)

    End Sub

    Private Sub UltraTilePanel1_TileClosed(sender As Object, e As TileClosedEventArgs)
    End Sub

    'Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
    '    If spb IsNot Nothing Then
    '        FlowLayoutPanel1.Controls.Remove(spb)
    '        lstpb.Remove(spb)
    '        spb = Nothing
    '    End If
    'End Sub

End Class