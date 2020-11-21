Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports risersoft.app.mxform.gst
Imports risersoft.app.shared
Imports Newtonsoft.Json
Imports risersoft.API.GSTN.GSTR1
Imports Newtonsoft.Json.Serialization
Imports risersoft.app.mxent
Imports GSTN.API.Library.Models.EWB
Imports risersoft.app.mxform

Public Class frmGstImportEWB
    Inherits frmMax

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGrid1)
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        OpenFileDialog1.FileName = ""
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim oMaster As New clsMasterDataFICO(Me.Controller)
        Dim rGstPP As DataRow = oMaster.GstRegPPRow(oview.ContextRow.CellValue("gstregid"), oview.ContextRow.CellValue("postperiodid"))
        prepIdx = rGstPP("gstregppid")
        Dim objModel As frmGstImportEWBModel = Me.InitData("frmGstImportEwBModel", oview, prepMode, prepIdx, strXML)

        If Me.BindModel(objModel, oview) Then
            myView.mainGrid.BindView(Me.Model.DatasetCollection("summ"),, 0)
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function



    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then


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
    Private Sub PrepViews(ds As DataSet)
        'bind view data
    End Sub
    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        OpenFileDialog1.Title = "Open File"
        OpenFileDialog1.DefaultExt = "json"
        OpenFileDialog1.Filter = "JSON Files|*.json"
        OpenFileDialog1.FilterIndex = 2
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim str1 As String = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)

            Dim Model = JsonConvert.DeserializeObject(Of BulkEWBInfo)(str1,
                                                                       New JsonSerializerSettings With {.Error = AddressOf HandleDeserializationError})
            Me.UltraGrid1.DataSource = Model.billLists
            'Dim oProc As New clsGSTReturnSale(Me.Controller)
            'Dim ds2 = oProc.PrepareGSTRAPayload(myRow("GstRegID"), myRow("PostPeriodID")).Clone
            'oProc.PopulateDataset(Model, ds2)
            'Dim Params As New List(Of clsSQLParam)
            'Params.Add(New clsSQLParam("@gstregid", myRow("GstRegID"), GetType(Integer), False))
            'Params.Add(New clsSQLParam("@postperiodid", myRow("postperiodID"), GetType(Integer), False))
            'Dim oRet = Me.GenerateDataParamsOutput("import", ds2, Params)
            'MsgBox(oRet.Message)
        End If
    End Sub
    Public Sub HandleDeserializationError(sender As Object, args As ErrorEventArgs)
        Dim currentError = args.ErrorContext.Error.Message
        args.ErrorContext.Handled = True
    End Sub


End Class