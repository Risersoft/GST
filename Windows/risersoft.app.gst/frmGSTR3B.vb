Imports risersoft.API.GSTN
Imports System.Text
Imports risersoft.app.mxform.gst
Imports System.Configuration
Imports Infragistics.Win.UltraWinGrid

Public Class frmGSTR3B
    Inherits frmMax
    Dim myViewSec31, myViewSec32, myViewSec4, myViewSec5 As New clsWinView
    Dim f As New frmApiTaskStatus

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGridGSTReg)
        myViewSec31.SetGrid(Me.UltraGridSec31)
        myViewSec32.SetGrid(Me.UltraGridSec32)
        myViewSec4.SetGrid(Me.UltraGridSec4)
        myViewSec5.SetGrid(Me.UltraGridSec5)
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)

        f.AddtoTab(Me.UltraTabControl1, "status", True)
        f.SetParent(Me)
        AddHandler f.LinkClicked, Sub(s, filePath)
                                      Me.SaveFileDialog1.FileName = System.IO.Path.GetFileName(filePath)
                                      Me.GetExtensionName()
                                      If Me.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                          Dim client = Me.Controller.App.objAppExtender.FileProviderClient(Me.Controller, ConfigurationManager.AppSettings("StorageContainerName"))
                                          client.DownloadFile(filePath, Me.SaveFileDialog1.FileName)
                                      End If
                                  End Sub
    End Sub

    Public Function GetExtensionName()
        Dim FileExtCode = System.IO.Path.GetExtension(myUtils.cStrTN(Me.SaveFileDialog1.FileName))
        Dim FileExt = myWinFtp.FileExtText(FileExtCode)
        Dim strFilter As String = FileExt & " (*" & FileExtCode & ")|*" & FileExtCode
        Me.SaveFileDialog1.Filter = strFilter
    End Function

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmGSTR3BModel = Me.InitData("frmGSTR3BModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            UltraGridGSTReg.Text = myUtils.cStrTN(myRow("CompName"))

            If myUtils.NullNot(cmb_ReturnPeriodID.Value) Then
                Dim ReturnPeriodID As Integer = myFuncs2.ValidatedPostPeriodID(Me.Controller, "ReturnPeriodID", myUtils.cValTN(Me.vBag("returnperiodid")), Me.dsCombo.Tables("postperiod"))
                cmb_ReturnPeriodID.Value = ReturnPeriodID
            End If

            cmb_ReturnPeriodID.ReadOnly = True

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("Gstreg"))
            myViewSec31.PrepEdit(Me.Model.GridViews("Sec31"))
            myViewSec32.PrepEdit(Me.Model.GridViews("Sec32"))
            myViewSec4.PrepEdit(Me.Model.GridViews("Sec4"))
            myViewSec5.PrepEdit(Me.Model.GridViews("Sec5"))
            myWinSQL.AssignCmb(Me.Model.dsCombo, "PostPeriod", "", Me.cmb_ReturnPeriodID)

            Return True
        End If
        Return False
    End Function

    Private Sub btnAutoFill_Click(sender As Object, e As EventArgs) Handles btnAutoFill.Click
        Dim oret = GetParams("populate", "")
        If oret.success Then
            Dim oret2 = Me.GetParams("dbsumm", "")
            Me.reBindView(oret2.Data)
            MsgBox("Autofill Successfully...", MsgBoxStyle.Information, myWinApp.Vars("appname"))
        End If
    End Sub

    Private Sub reBindView(ds As DataSet)
        myView.mainGrid.BindView(ds,, 0)
        myView.mainGrid.Genwidth(True)
        myViewSec31.mainGrid.BindView(ds,, 2)
        myViewSec31.mainGrid.Genwidth(True)
        myViewSec32.mainGrid.BindView(ds,, 3)
        myViewSec32.mainGrid.Genwidth(True)
        myViewSec4.mainGrid.BindView(ds,, 4)
        myViewSec4.mainGrid.Genwidth(True)
        myViewSec5.mainGrid.BindView(ds,, 5)
        myViewSec5.mainGrid.Genwidth(True)
    End Sub

    Private Sub btnChange_Click(sender As Object, e As EventArgs) Handles btnChange.Click
        cmb_ReturnPeriodID.ReadOnly = False
    End Sub

    Private Sub cmb_ReturnPeriodID_Leave(sender As Object, e As EventArgs) Handles cmb_ReturnPeriodID.Leave, cmb_ReturnPeriodID.AfterCloseUp
        Dim oRet = Me.GetParams("dbsumm", "")
        Me.reBindView(oRet.Data)
        cmb_ReturnPeriodID.ReadOnly = True
    End Sub

    Private Sub SuppliesMadetoUnregisteredToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SuppliesMadetoUnregisteredToolStripMenuItem.Click
        If Not IsNothing(myView.mainGrid.myGrid.ActiveRow) Then
            Dim gr As UltraGridRow
            gr = myViewSec32.mainGrid.ButtonAction("add")
            If Not gr Is Nothing Then
                gr.Cells("GstRegID").Value = myUtils.cValTN(myView.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value)
                gr.Cells("TableNum").Value = "3.2"
                gr.Cells("SectionNum").Value = "A"
                gr.Cells("Description").Value = "Supplies made to Unregistered Persons"
            End If
        Else
            MsgBox("Please Select GSTIN")
        End If

    End Sub

    Private Sub SuppliesMadeToCompositionTaxablePersonsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SuppliesMadeToCompositionTaxablePersonsToolStripMenuItem.Click
        If Not IsNothing(myView.mainGrid.myGrid.ActiveRow) Then
            Dim gr As UltraGridRow
            gr = myViewSec32.mainGrid.ButtonAction("add")
            If Not gr Is Nothing Then
                gr.Cells("GstRegID").Value = myUtils.cValTN(myView.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value)
                gr.Cells("TableNum").Value = "3.2"
                gr.Cells("SectionNum").Value = "B"
                gr.Cells("Description").Value = "Supplies made to Composition Taxable Persons"
            End If
        Else
            MsgBox("Please Select GSTIN")
        End If

    End Sub

    Private Sub SuppliesMadeToUINHoldersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SuppliesMadeToUINHoldersToolStripMenuItem.Click
        If Not IsNothing(myView.mainGrid.myGrid.ActiveRow) Then
            Dim gr As UltraGridRow
            gr = myViewSec32.mainGrid.ButtonAction("add")
            If Not gr Is Nothing Then
                gr.Cells("GstRegID").Value = myUtils.cValTN(myView.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value)
                gr.Cells("TableNum").Value = "3.2"
                gr.Cells("SectionNum").Value = "C"
                gr.Cells("Description").Value = "Supplies made to UIN holders"
            End If
        Else
            MsgBox("Please Select GSTIN")
        End If

    End Sub

    Private Function GetParams(Key As String, UploadType As String)
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@gstregid", myUtils.cValTN(myView.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@ReturnPeriodID", myUtils.cValTN(cmb_ReturnPeriodID.SelectedRow.Cells("PostPeriodID").Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@CompanyID", myUtils.cValTN(myRow("CompanyID")), GetType(Integer), False))

        Dim oRet = Me.GenerateParamsOutput(Key, Params)
        Dim result As Guid
        If System.Guid.TryParse(oRet.Description, result) Then
            f.AddTask(result.ToString)
        End If
        Return oRet

    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        cm.EndCurrentEdit()
        If Me.ValidateData() Then
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub UltraGridGSTReg_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridGSTReg.AfterRowActivate
        If Not IsNothing(myView.mainGrid.myGrid.ActiveRow) Then
            myViewSec31.mainGrid.myDv.RowFilter = "GstRegID = " & myUtils.cValTN(myView.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value)
            myViewSec32.mainGrid.myDv.RowFilter = "GstRegID = " & myUtils.cValTN(myView.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value)
            myViewSec4.mainGrid.myDv.RowFilter = "GstRegID = " & myUtils.cValTN(myView.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value)
            myViewSec5.mainGrid.myDv.RowFilter = "GstRegID = " & myUtils.cValTN(myView.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value)
        End If
    End Sub

End Class