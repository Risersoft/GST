Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports risersoft.app.mxform.gst
Imports risersoft.app.mxform
Imports risersoft.app.shared
Imports System.Configuration
Imports Newtonsoft.Json

Public Class frmGstImportVouchNew
    Inherits frmMax
    Dim ServerPath As String = "", LocalPath As String = "", ImportFileID As String
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.InitForm()
    End Sub
    Dim f As New frmApiTaskStatus

    Private Sub InitForm()
        f.AddtoTab(Me.UltraTabControl1, "status", True)
        f.SetParent(Me)
        AddHandler f.TaskExecuted, Sub(s, ApiTaskId)
                                       Me.btnFileDetails.Visible = True
                                   End Sub
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
        Dim objModel As frmGstImportVouchNewModel = Me.InitData("frmGstImportVouchNewModel", oview, prepMode, prepIdx, strXML)

        If Me.BindModel(objModel, oview) Then
            btnFileDetails.Visible = False
            If Me.vBag("Action") = "Import" Then
                Me.cmb_DocType.Visible = True
            ElseIf Me.vBag("Action") = "Search" Then
                Me.cmb_DocType1.Visible = True
                Me.cmb_DocType1.Value = Me.vBag("DocType")
                Me.cmb_DocType.Visible = False
            ElseIf Me.vBag("Action") = "ConvertJson" Then
                Me.cmb_DocType1.Visible = True
                cmb_DocType1.ReadOnly = False
                Me.cmb_DocType.Visible = False
            End If

            InitUpLoad()

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            If Me.vBag("Action") = "Import" Then
                myWinSQL.AssignCmb(Me.dsCombo, "DocType", "", Me.cmb_DocType)
            ElseIf Me.vBag("Action") = "Search" Then
                Me.cmb_DocType1.ValueList = Me.Model.ValueLists("Search").ComboList
            ElseIf Me.vBag("Action") = "ConvertJson" Then
                Me.cmb_DocType1.ValueList = Me.Model.ValueLists("Convert").ComboList
            End If

            Return True
            End If
            Return False
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

    Private Function InitUpLoad()
        Dim str1 As String = "XLS Files|*.xls|XLSX Files|*.xlsx|All Files(*.*)|*.*"
        If myUtils.IsInList(myUtils.cStrTN(Me.vBag("Action")), "ConvertJson") Then str1 = "JSON Files|*.json"
        Me.CtlUpLoad1.InitFilter(EnumfrmMode.acEditM, "", Me.Controller.App.objAppExtender.FileServerPath, "", $"{str1}", ConfigurationManager.AppSettings("StorageContainerName"))
        AddHandler Me.CtlUpLoad1.FileSelected, Sub(SelectedFile As String)
                                                   LocalPath = SelectedFile
                                                   ImportFileID = System.Guid.NewGuid.ToString
                                                   Me.CtlUpLoad1.FileName = System.IO.Path.GetFileNameWithoutExtension(SelectedFile) & "-" & ImportFileID & System.IO.Path.GetExtension(SelectedFile)
                                               End Sub
        AddHandler Me.CtlUpLoad1.FileUploaded, Sub(ByVal localFile As String, ServerPath As String)
                                                       Me.ServerPath = ServerPath
                                                       Dim oRet = GetParams("execute", "")
                                                       MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
                                                   End Sub
            Return True
    End Function

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Dim code As String = myUtils.cStrTN(cmb_DocType.SelectedRow.Cells("Tag").Value)
        Dim assy = GetType(Import_GSTR1TaskProvider).Assembly
        Dim rawFile = myAssy.bytFromEmbed(assy.GetName.Name, code)
        Dim filename As String = Me.cmb_DocType.Text.Replace(" ", "_") & ".xlsx"
        Me.SaveFileDialog1.FileName = filename
        Me.GetExtensionName()
        If Me.SaveFileDialog1.ShowDialog = DialogResult.OK Then
            My.Computer.FileSystem.WriteAllBytes(SaveFileDialog1.FileName, rawFile, False)
        End If

    End Sub

    Private Sub btnFileDetails_Click(sender As Object, e As EventArgs) Handles btnFileDetails.Click
        Dim f As New frmGstImportVouch
        f.PrepForm(myView, EnumfrmMode.acEditM, myRow("ImportFileID").ToString)
        f.ShowDialog()

    End Sub

    Private Function GetParams(Key As String, UploadType As String)
        Dim info As New IO.FileInfo(LocalPath)
        'Case to be added for ActionType -> Convert, Search


        Dim serverPath2 = Uri.EscapeUriString(System.IO.Path.GetFileName(ServerPath))
        'ActionType = "Import"
        Dim Params As New List(Of clsSQLParam)

        Params.Add(New clsSQLParam("@serverPath", myUtils.cStrTN(serverPath2), GetType(Uri), False))
        Params.Add(New clsSQLParam("@Action", "'" & myUtils.cStrTN(Me.vBag("Action")) & "'", GetType(String), False))
        If Me.vBag("Action") = "Import" Then
            Params.Add(New clsSQLParam("@DocType", "'" & myUtils.cStrTN(Me.cmb_DocType.Value) & "'", GetType(String), False))
        Else
            Params.Add(New clsSQLParam("@DocType", "'" & myUtils.cStrTN(Me.cmb_DocType1.Value) & "'", GetType(String), False))
        End If

        Params.Add(New clsSQLParam("@Length", "'" & myUtils.cStrTN(info.Length) & "'", GetType(String), False))
        Params.Add(New clsSQLParam("@LastWriteTime", info.LastWriteTime.ToString, GetType(Date), False))
        Params.Add(New clsSQLParam("@importfileid", ImportFileID, GetType(Guid), False))
        Params.Add(New clsSQLParam("@pimportfileid", "'" & myUtils.cStrTN(Me.vBag("pimportfileid")) & "'", GetType(String), False))

        Dim oRet = Me.GenerateParamsOutput(Key, Params)
        If oRet.Success Then
            Dim rTask = oRet.GetCalcRows("task")(0)
            If myUtils.cStrTN(rTask("importfileid")).Trim.Length > 0 Then
                myRow("importfileid") = rTask("importfileid")
            End If

            Dim result As Guid
            If System.Guid.TryParse(oRet.Description, result) Then
                f.AddTask(result.ToString)
            End If
        End If
        Return oRet

    End Function


End Class
