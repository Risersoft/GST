Imports System.Configuration
Imports System.IO
Imports risersoft.app.config
Imports risersoft.app.exvat
Imports risersoft.app.mxform.gst

Public Class frmGstImportVouch
    Inherits frmMax
    Dim myVueSummary, myVueTaxSummary, myVueError As New clsWinView
    Dim f As New frmApiTaskStatus

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGridFile)
        myVueSummary.SetGrid(Me.UltraGridSummary)
        myVueTaxSummary.SetGrid(Me.UltraGridTaxSummary)
        myVueError.SetGrid(Me.UltraGridError)

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
        Dim objModel As frmGstImportVouchModel = Me.InitData("frmGstImportVouchModel", oView, prepMode, prepIDX, strXML)

        If Me.BindModel(objModel, oView) Then
            If frmMode = EnumfrmMode.acEditM Then
                txt_FileName.Text = myRow("FileName")
                cmb_DocType.Value = myRow("DocType")
            End If

            Me.FormPrepared = True

        End If

        Return Me.FormPrepared

    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myWinSQL.AssignCmb(Me.dsCombo, "DocType", "", Me.cmb_DocType)
            myView.PrepEdit(Me.Model.GridViews("File"))
            myVueSummary.PrepEdit(Me.Model.GridViews("Summary"))
            myVueTaxSummary.PrepEdit(Me.Model.GridViews("TaxSummary"))
            myVueError.PrepEdit(Me.Model.GridViews("Error"))
            Dim dt As DataTable = Me.dsForm.Tables("count")
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Me.txt_Record.Text = dt.Rows(0)("column1")
            End If
            Return True
        End If
        Return False
    End Function

    Private Sub UltraGridFile_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridFile.AfterRowActivate
        If Not IsNothing(myView.mainGrid.myGrid.ActiveRow) Then
            Dim oRet = Me.GetParams("getrecordcount", "")
            Me.reBindView(oRet.Data)

        End If
    End Sub

    Private Sub reBindView(ds As DataSet)
        myView.mainGrid.BindView(ds,, 0)
        myView.mainGrid.Genwidth(True)
        myVueError.mainGrid.BindView(ds,, 1)
        myVueError.mainGrid.Genwidth(True)
        myVueSummary.mainGrid.BindView(ds,, 3)
        myVueSummary.mainGrid.Genwidth(True)
        myVueTaxSummary.mainGrid.BindView(ds,, 3)
        myVueTaxSummary.mainGrid.Genwidth(True)
        Dim dt As DataTable = ds.Tables("count")
        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
            Me.txt_Record.Text = dt.Rows(0)("column1")
        End If

    End Sub

    Private Function GetParams(Key As String, UploadType As String)
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@ImportFileID", "'" & myUtils.cStrTN(myRow("ImportFileID")) & "'", GetType(String), False))
        Params.Add(New clsSQLParam("@DocType", "'" & myUtils.cStrTN(myRow("DocType")) & "'", GetType(String), False))

        Dim oRet = Me.GenerateParamsOutput(Key, Params)
        Dim result As Guid
        If System.Guid.TryParse(oRet.Description, result) Then
            f.AddTask(result.ToString)
        End If
        Return oRet

    End Function

    Private Sub btnDownloadOriginal_Click(sender As Object, e As EventArgs) Handles btnDownloadOriginal.Click
        Dim ServerPath As String = myRow("filename").ToString
        Me.SaveFileDialog1.FileName = System.IO.Path.GetFileName(ServerPath)
        Me.GetExtensionName()
        If Me.SaveFileDialog1.ShowDialog = DialogResult.OK Then
            Dim client = Me.Controller.App.objAppExtender.FileProviderClient(Me.Controller, ConfigurationManager.AppSettings("StorageContainerName"))
            client.DownloadFile(ServerPath, Me.SaveFileDialog1.FileName)
        End If

    End Sub

    Private Sub btnDownloadError_Click(sender As Object, e As EventArgs) Handles btnDownloadError.Click
        If Not myUtils.NullNot(myRow("ErrorFileURL")) Then
            Dim ServerPath As String = myRow("ErrorFileURL").ToString
            Me.SaveFileDialog1.FileName = System.IO.Path.GetFileName(ServerPath)
            Me.GetExtensionName()
            If Me.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                Dim client = Me.Controller.App.objAppExtender.FileProviderClient(Me.Controller, ConfigurationManager.AppSettings("StorageContainerName"))
                client.DownloadFile(ServerPath, Me.SaveFileDialog1.FileName)
            End If
        End If
    End Sub

    Private Sub btnAction_Click(sender As Object, e As EventArgs) Handles btnAction.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@ImportFileID", myUtils.cStrTN(myRow("ImportFileID")), GetType(Guid), False))
        Dim oRet = Me.GenerateParamsOutput("executeagain", Params)
        If oRet.Success Then
            Dim rTask = oRet.GetCalcRows("task")(0)
            Dim result As Guid
            If System.Guid.TryParse(oRet.Description, result) Then
                f.AddTask(result.ToString)
            End If
        End If
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))

    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim f As frmMax
        myVueError.ContextRow = myVueError.mainGrid.ActiveRow
        If Not myVueError.ContextRow Is Nothing Then
            f = myFuncs.GetFormFunc(myUtils.cStrTN(myRow("doctype")))
            If f IsNot Nothing Then
                Dim strXML As String = $"<PARAMS IMPORTFILEVOUCHID=""{myUtils.cValTN(myVueError.mainGrid.myGrid.ActiveRow.Cells("ImportFileVouchID").Value)}""/>"
                If f.PrepForm(Me.myVueError, EnumfrmMode.acAddM, "", strXML) Then f.ShowDialog()

            End If


        End If
    End Sub

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.ValidateData() Then
            cm.EndCurrentEdit()
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()

    End Function

    Private Sub btnDeleteImportFile_Click(sender As Object, e As EventArgs) Handles btnDeleteImportFile.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@ImportFileID", myUtils.cStrTN(myRow("ImportFileID")), GetType(Guid), False))
        Dim oRet = Me.GenerateParamsOutput("delete", Params)
        If oRet.Success Then
            Dim rTask = oRet.GetCalcRows("task")(0)
            Dim result As Guid
            If System.Guid.TryParse(oRet.Description, result) Then
                f.AddTask(result.ToString)
            End If
        End If
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))

    End Sub

    Private Sub btnUploadCorrectFile_Click(sender As Object, e As EventArgs) Handles btnUploadCorrectFile.Click
        Dim f As New frmGstImportVouchNew
        Dim strXML As String = $"<PARAMS pIMPORTFILEID=""{myUtils.cStrTN(myRow("ImportFileID"))} "" DocType=""{myRow("DocType")} "" Action=""Import""/>"
        f.PrepForm(myView, EnumfrmMode.acEditM, frmIDX, strXML)
        f.ShowDialog()
    End Sub

    Public Overrides Function ChangeForm(oView As clsView, prepMode As EnumfrmMode, prepIDX As String, strXML As String) As IForm
        If prepMode = EnumfrmMode.acAddM Then
            Return New frmGstImportVouchNew()
        End If
    End Function
End Class
