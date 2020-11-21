Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmCPInvoiceModel
    Inherits clsFormDataModel
    Dim PPFinal As Boolean = False

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("ItemList")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select GstRegID, GSTIN from GstReg  Order by GSTIN"
        Me.AddLookupField("GstRegID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GstReg").TableName)

        sql = "SELECT  VendorID, PartyName, TaxAreaCode, GSTRegType, GSTIN, VendorCode FROM  PurListVendor() Order By PartyName"
        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)

        sql = "SELECT  CustomerID, PartyName, TaxAreaCode, GSTIN, TaxAreaID FROM  slsListCustomer() Order By PartyName"
        Me.AddLookupField("CustomerID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Customer").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "B2BType", "")
        Me.AddLookupField("inv_typ", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "B2BType").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "CDNType", "")
        Me.AddLookupField("ntty", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "CDNType").TableName)

        sql = "select TaxAreaID, Descrip, TaxAreaCode from TaxArea Order by Descrip"
        Me.AddLookupField("POSTaxAreaID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "POS").TableName)

        Dim vlist As New clsValueList
        vlist.Add("Y", "Yes")
        vlist.Add("N", "No")
        Me.ValueLists.Add("RCHRG", vlist)
        Me.AddLookupField("RCHRG", "RCHRG")

        Dim vlist1 As New clsValueList
        vlist1.Add(False, "Debit/Credit Note")
        vlist1.Add(True, "Amendment")
        Me.ValueLists.Add("IsAmendment", vlist1)
        Me.AddLookupField("IsAmendment", "IsAmendment")

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, str1 As String

        Dim ds As DataSet
        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from CPInvoice Where CPInvoiceID = " & prepIDX
        Me.InitData(sql, "", oView, prepMode, prepIDX, strXML)

        Me.BindDataTable(myUtils.cValTN(prepIDX))

        myView.MainGrid.BindGridData(Me.dsForm, 1)
        sql = "Select * from GSTNAction Where GSTRegID = " & myRow("GSTRegID") & " And ReturnPeriodID =" & myRow("ReturnPeriodID") & " And ctin='" & myRow("ctin") & "' And InvoiceDate='" & myRow("Idt") & "' And InvoiceNum='" & myRow("INUM") & "'"
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)
        Me.AddDataSet("GSTNAction", sql)

        If (ds.Tables(0).Rows.Count = 0) Then
            Dim vlist As New clsValueList
            vlist.Add("ActionFlag", "NOTP")
            Me.ValueLists.Add("GSTNAction", vlist)
            Me.AddLookupField("GSTNAction", "GSTNAction")
        Else
            Dim vlist As New clsValueList
            vlist.Add("ActionFlag", "P")
            Me.ValueLists.Add("GSTNAction", vlist)
            Me.AddLookupField("GSTNAction", "GSTNAction")
        End If

        myView.MainGrid.QuickConf("", True, "1-1-1-1-1-1", True)
        Dim str2 As String = "<BAND INDEX = ""0"" TABLE = ""InvoiceItemGst"" IDFIELD=""InvoiceItemID""><COL KEY ="" InvoiceItemID, InvoiceID,GstTaxType,elg,tx_i,tx_c,tx_s,tx_cs,Description,ty,Qty,SortIndex""/><COL KEY=""Hsn_sc"" CAPTION=""HSN""/><COL KEY=""RT"" CAPTION=""Tax Rate""/><COL KEY=""TXVAL"" CAPTION=""Taxable Value""/><COL KEY=""Uqc"" CAPTION=""Unit Name""/><COL KEY=""BasicRate"" CAPTION=""Basic Rate""/><COL KEY=""CSAMT"" CAPTION=""CESS""/><COL KEY=""IAMT"" CAPTION=""Integrated Tax""/><COL KEY=""CAMT"" CAPTION=""Central Tax""/><COL KEY=""SAMT"" CAPTION=""State Tax""/></BAND>"
        myView.MainGrid.PrepEdit(str2, EnumEditType.acCommandOnly)

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Private Sub BindDataTable(ByVal CPInvoiceID As Integer)

        Dim ds As DataSet, Sql As String

        Sql = " Select CPInvoiceItemID, CPInvoiceID,TY, TXVAL, RT, IAMT, CAMT, SAMT, CSAMT from CPInvoiceItem  Where CPInvoiceID = " & CPInvoiceID & " "
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(Me.dsForm, ds, "CPInvoiceItem", 0)
    End Sub

    Public Overrides Function GenerateIDOutput(dataKey As String, frmIDX As Integer) As clsProcOutput
        Dim oRet As New clsProcOutput
        Dim sql, str As String

        sql = "Select * from CPInvoice Where CPInvoiceID = " & frmIDX
        Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        Dim r1 = dt1.Rows(0)
        Select Case dataKey.Trim.ToLower
            Case "mark"
                str = "Insert into GSTNAction(GSTRegID,ReturnPeriodID,DocType,GSTInvoiceType,ctin,InvoiceNum,InvoiceDate,ActionFlag) values (" & Convert.ToInt16(r1("GSTRegID").ToString()) & " ," & Convert.ToInt16(r1("ReturnPeriodID").ToString()) & ",'" & r1("DocType").ToString() & "','" & r1("GstInvoiceType").ToString() & "','" & r1("ctin").ToString() & "','" & r1("INUM").ToString() & "','" & r1("Idt").ToString() & "','P')"
                oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, str)
                oRet.AddMessage("Marked Sucessfully")
            Case "unmark"
                str = CType("Delete from GSTNAction Where GSTRegID = " & Convert.ToInt16(r1("GSTRegID").ToString()) & " And ReturnPeriodID =" & Convert.ToInt16(r1("ReturnPeriodID").ToString()) & " And ctin='" & r1("ctin").ToString() & "' And InvoiceDate='" & r1("Idt").ToString() & "' And InvoiceNum='" & r1("INUM").ToString() & "'", String)
                oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, str)
                oRet.AddMessage("UnMarked Sucessfully")
        End Select
        Return oRet
    End Function

End Class
