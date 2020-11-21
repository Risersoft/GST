Imports Newtonsoft.Json
Imports risersoft.shared
Imports risersoft.shared.cloud
Imports risersoft.shared.cloud.common
Imports risersoft.API.GSTN.GSTR3B
Imports System.Configuration
Imports System.Runtime.Serialization
Imports Microsoft.Owin.Infrastructure

<DataContract>
Public Class frmGSTR3BModel
    Inherits clsFormDataModel
    Dim myViewSec31, myViewSec32, myViewSec4, myViewSec5 As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Gstreg")
        myViewSec31 = Me.GetViewModel("Sec31")
        myViewSec32 = Me.GetViewModel("Sec32")
        myViewSec4 = Me.GetViewModel("Sec4")
        myViewSec5 = Me.GetViewModel("Sec5")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim Sql As String = "Select GstRegID, GSTIN from GstReg  Order by GSTIN"
        Me.AddLookupField("GstRegID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "GstReg").TableName)

        Sql = "Select PostPeriodID, ret_pd, sdate from PostPeriod  Order by sdate desc"
        Me.AddLookupField("ReturnPeriodID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "PostPeriod").TableName)

        Sql = "Select * from gstrsectiontext where returntype in ('gstr3b') and tablenum='3.2' and sectionnum is not null"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "sec32")

        Sql = "select TaxAreaID, Descrip, TaxAreaCode from TaxArea Order by Descrip"
        Me.AddLookupField("POSTaxAreaID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "POS").TableName)

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim Sql As String

        Me.FormPrepared = False
        If Not prepIDX Is Nothing Then
            prepMode = EnumfrmMode.acEditM
            Sql = "Select CompanyID,CompName,PANNum from Company Where CompanyID = " & prepIDX
            Me.InitData(Sql, "returnperiodid", oView, prepMode, prepIDX, strXML)

            Dim ReturnPeriodID As Integer = myFuncs2.ValidatedPostPeriodID(myContext, "ReturnPeriodID", myUtils.cValTN(Me.vBag("returnperiodid")), Me.dsCombo.Tables("postperiod"))
            Me.BindDataTable(myUtils.cValTN(prepIDX), ReturnPeriodID)

            myView.MainGrid.MainConf("formatxml") = "<COL KEY=""Descrip"" CAPTION=""State""/>"
            myView.MainGrid.BindGridData(Me.dsForm, 1)
            myView.MainGrid.QuickConf("", True, "1-1")

            myViewSec31.MainGrid.MainConf("formatxml") = "<COL KEY=""Description"" CAPTION=""Nature of Supplies""/><COL KEY=""Txval"" CAPTION=""Total Taxable Value""/><COL KEY=""Iamt"" CAPTION=""Integrated Tax""/><COL KEY=""camt"" CAPTION=""Central Tax""/><COL KEY=""SAMT"" CAPTION=""State/UT Tax""/><COL KEY=""csamt"" CAPTION=""CESS""/>"
            myViewSec31.MainGrid.BindGridData(Me.dsForm, 3)
            myViewSec31.MainGrid.QuickConf("", True, "3-1.2-1-1-1-1")
            Dim str1 = "<BAND INDEX=""3"" TABLE=""GSTR3B"" IDFIELD=""GSTR3BID""><COL NOEDIT=""TRUE"" KEY=""Description""/><COL KEY="" txval, iamt, camt, samt, csamt ""/></BAND>"
            myViewSec31.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

            myViewSec32.MainGrid.MainConf("formatxml") = "<COL KEY=""Description"" CAPTION=""Nature of Supplies""/><COL KEY=""Txval"" CAPTION=""Total Taxable Value""/><COL KEY=""Iamt"" CAPTION=""Amount of Integrated Tax""/>"
            myViewSec32.MainGrid.BindGridData(Me.dsForm, 4)
            myViewSec32.MainGrid.QuickConf("", True, "2-1.5-1-1")
            str1 = "<BAND INDEX=""4"" TABLE=""GSTR3B"" IDFIELD=""GSTR3BID""><COL NOEDIT=""TRUE"" KEY=""Description""/><COL KEY=""txval, iamt ""/><COL KEY=""POSTaxAreaID"" CAPTION=""Place of Supply(State/UT)"" LOOKUPSQL=""Select TaxAreaID, Descrip from TaxArea""/></BAND>"
            myViewSec32.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

            myViewSec4.MainGrid.MainConf("formatxml") = "<COL KEY=""Description"" CAPTION=""Nature of Supplies""/><COL KEY=""Txval"" CAPTION=""Total Taxable Value""/><COL KEY=""Iamt"" CAPTION=""Integrated Tax""/><COL KEY=""camt"" CAPTION=""Central Tax""/><COL KEY=""SAMT"" CAPTION=""State/UT Tax""/><COL KEY=""csamt"" CAPTION=""CESS""/>"
            myViewSec4.MainGrid.BindGridData(Me.dsForm, 5)
            myViewSec4.MainGrid.QuickConf("", True, "3-1.2-1-1-1-1")
            str1 = "<BAND INDEX=""5"" TABLE=""GSTR3B"" IDFIELD=""GSTR3BID""><COL NOEDIT=""TRUE"" KEY=""Description""/><COL KEY="" txval, iamt, camt, samt, csamt ""/></BAND>"
            myViewSec4.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

            myViewSec5.MainGrid.MainConf("formatxml") = "<COL KEY=""Description"" CAPTION=""Nature of Supplies""/><COL KEY=""InterSuply"" CAPTION=""Inter-State Supplies""/><COL KEY=""IntraSuply"" CAPTION=""Intra-State Supplies""/>"
            myViewSec5.MainGrid.BindGridData(Me.dsForm, 6)
            myViewSec5.MainGrid.QuickConf("", True, "3-1-1")
            str1 = "<BAND INDEX=""6"" TABLE=""GSTR3B"" IDFIELD=""GSTR3BID""><COL NOEDIT=""TRUE"" KEY=""Description""/><COL KEY="" intersuply, intrasuply ""/></BAND>"
            myViewSec5.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

            Me.FormPrepared = True
        End If

        Return Me.FormPrepared
    End Function

    Protected Friend Overloads Function GenerateData(ByVal CompanyID As Integer, ReturnPeriodID As Integer) As DataSet
        Dim oProc As New clsGSTNReturnGSTR3B(myContext)
        For Each r1 As DataRow In oProc.oMaster.GstRegTable.Select("companyid=" & CompanyID)
            Dim oRet = oProc.CheckGenerateGSTR3B(r1("gstregid"), ReturnPeriodID)
        Next

        Dim ds As DataSet, dic As New clsCollecString(Of String)
        dic.Add("gstreg", "Select GSTRegID, GSTIN, Descrip from GstListGSTReg() where  CompanyID = " & CompanyID)
        dic.Add("return", String.Format("select * from postperiod where postperiodid ={0}", ReturnPeriodID))
        dic.Add("Sec31", String.Format("select GSTR3BID, GSTRegID,ReturnPeriodID, SectionNum + '-' + Description as Description, txval, iamt, camt, samt, csamt from gstlistgstr3b() where  Companyid={0} and ReturnPeriodID={1} and tablenum='3.1'", CompanyID, ReturnPeriodID))
        dic.Add("Sec32", String.Format("select GSTR3BID, GSTRegID,ReturnPeriodID, TableNum, SectionNum, Description, POSTaxAreaID, txval, iamt from gstlistgstr3b() where  Companyid={0} and ReturnPeriodID={1} and tablenum='3.2'", CompanyID, ReturnPeriodID))
        dic.Add("Sec4", String.Format("select GSTR3BID, GSTRegID, ReturnPeriodID,SectionNum + '-' + Description as Description, txval, iamt, camt, samt, csamt from gstlistgstr3b() where  Companyid={0} and ReturnPeriodID={1} and tablenum in ('4A','4B','4C','4D')", CompanyID, ReturnPeriodID))
        dic.Add("Sec5", String.Format("select GSTR3BID, GSTRegID, ReturnPeriodID,SectionNum + '-' + Description as Description, intersuply, intrasuply from gstlistgstr3b() where  Companyid={0} and ReturnPeriodID={1} and tablenum='5'", CompanyID, ReturnPeriodID))
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
        Return ds
    End Function

    Private Sub BindDataTable(ByVal CompanyID As Integer, ReturnPeriodID As Integer)
        Dim ds = Me.GenerateData(CompanyID, ReturnPeriodID)
        For i As Integer = 0 To ds.Tables.Count - 1
            myUtils.AddTable(Me.dsForm, ds, ds.Tables(i).TableName, i)
        Next
    End Sub

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            Dim ReturnDescrip As String = "GSTR3B for PANNUM: " & myRow("PANNUM").ToString & ", Period: " & Me.dsForm.Tables("return").Rows(0)("ret_pd").ToString

            Try
                For Each str1 As String In New String() {"sec31", "sec32", "sec4", "sec5"}
                    myUtils.ChangeAll(Me.dsForm.Tables(str1).Select, "returnperiodid=" & Me.dsForm.Tables("return").Rows(0)("postperiodid"))
                    For Each str2 As String In New String() {"txval", "iamt", "camt", "samt", "csamt"}
                        For Each r2 As DataRow In Me.dsForm.Tables(str1).Select
                            If r2.Table.Columns.Contains(str2) Then r2(str2) = Math.Round(myUtils.cValTN(r2(str2)), 2)
                        Next
                    Next
                Next


                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "CompanyID", frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(Me.dsForm.Tables("Sec31"), "select GSTR3BID, GSTRegID,ReturnPeriodID, txval, iamt, camt, samt, csamt from gstr3b where 0=1")
                myContext.Provider.objSQLHelper.SaveResults(Me.dsForm.Tables("Sec32"), "select GSTR3BID, GSTRegID, TableNum, SectionNum, ReturnPeriodID, POSTaxAreaID, txval, iamt from gstr3b where 0=1")
                myContext.Provider.objSQLHelper.SaveResults(Me.dsForm.Tables("Sec4"), "select GSTR3BID, GSTRegID,ReturnPeriodID,txval, iamt, camt, samt, csamt from gstr3b where 0=1")
                myContext.Provider.objSQLHelper.SaveResults(Me.dsForm.Tables("Sec5"), "select GSTR3BID, GSTRegID,ReturnPeriodID,intersuply, intrasuply from gstr3b where 0=1")
                myContext.Provider.dbConn.CommitTransaction(ReturnDescrip, frmIDX)
                VSave = True
            Catch e As Exception
                myContext.Provider.dbConn.RollBackTransaction(ReturnDescrip, e.Message)
                Me.AddException("", e)
            End Try
        End If
    End Function

    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim GstRegID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@gstregid", Params))
        Dim ReturnPeriodID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ReturnPeriodID", Params))
        Dim CompanyID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@companyid", Params))
        Dim ReturnTaskID As String = myContext.SQL.ParamValue("@ApiTaskID", Params)
        Dim InfoJsonAPITaskID As String = myContext.SQL.ParamValue("@UploadType", Params)

        Dim oRet As New clsProcOutput
        If myUtils.IsInList(dataKey, "dbsumm") Then
            If CompanyID = 0 Then
                oRet.AddError("Company not selected")
            Else
                Select Case dataKey.Trim.ToLower
                    Case "dbsumm"
                        oRet.Data = Me.GenerateData(CompanyID, ReturnPeriodID)
                        Dim provider As New clsDBUserFilterProvider(myContext, False)
                        provider.AddUpdValueRow("ReturnPeriodID", ReturnPeriodID)
                        provider.SaveDBUserFilter()
                        oRet.Message = "Return Period change successful"
                End Select
            End If
        ElseIf myUtils.IsInList(dataKey, "infojson") AndAlso Not String.IsNullOrWhiteSpace(InfoJsonAPITaskID) Then
            oRet.Description = myContext.Provider.objSQLHelper.ExecuteScalar(CommandType.Text, "SELECT INFOJSON FROM APITASK WHERE APITaskID ='" & InfoJsonAPITaskID.ToString & "'").ToString
        ElseIf myUtils.IsInList(dataKey, "payloadstatus") AndAlso Not String.IsNullOrWhiteSpace(ReturnTaskID) Then
            oRet = QueueTaskProvider.GetTaskStatus(myContext, ReturnTaskID)
        Else
            If GstRegID = 0 Then
                oRet.AddError("Please select GSTIN")
            Else
                Dim oProc As New clsGSTNReturnGSTR3B(myContext)
                Select Case dataKey.Trim.ToLower
                    Case "populate"
                        oRet = oProc.CheckPopulateGSTR3B(GstRegID, ReturnPeriodID)

                End Select
            End If
        End If

        Return oRet
    End Function

End Class
