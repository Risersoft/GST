Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.Threading.Tasks
Imports AutoMapper
Imports GSTN.API.Library.Models.EWB
Imports GstNirvana
Imports Newtonsoft.Json
Imports risersoft.app
Imports risersoft.app.mxent
Imports risersoft.app.mxform
Imports risersoft.app.mxform.gst
Imports risersoft.shared
Imports risersoft.shared.portable.Model

Public Class EWBRepository
    Inherits ServerRepositoryBase(Of EWBInfo, Int64, GenerateEWBInfo, EWBPostResponseInfo)
    Implements IEWBRepository


    '' <summary>
    ''' Get All EWBs Details
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function GetAll() As ResultInfo(Of List(Of EWBInfo), HttpStatusCode)
        Try

        Catch ex As Exception
            Return BuildExceptionResponse(Of List(Of EWBInfo))(ex)
        End Try
    End Function

    ''' <summary>
    ''' Get Details
    ''' </summary>
    ''' <param name="id">EWBId</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function [Get](id As Int64) As ResultInfo(Of EWBInfo, HttpStatusCode)
        Try

            Dim str1 As String = myAssy.GetString(Assembly.GetExecutingAssembly.GetName.Name, "ewb.json")
            Dim info As EWBInfo = JsonConvert.DeserializeObject(Of EWBInfo)(str1)
            Return BuildResponse(info)
        Catch ex As Exception
            Return BuildExceptionResponse(Of EWBInfo)(ex)
        End Try
    End Function
    Protected Friend Sub AsyncSave(data As GenerateEWBInfo)
        Using ctx = Me.GetServerEntity
            Dim lst = (From obj In ctx.GSTReg Where obj.GSTIN = data.fromGstin OrElse obj.GSTIN = data.toGstin).ToList
            Dim provider = Me.WebController.CreateFlyDataProvider()
            Dim t1 = Task.Run(Function()
                                  Return Me.Save(ctx, lst(0), data)
                              End Function)
            Dim t2 = Task.Run(Function()
                                  Dim oProc As New clsGSTNEwayBill(provider)
                                  Return oProc.Generate(lst(0).GSTRegID, data)
                              End Function)
            Task.WaitAll(t1, t2)
            Dim ewb = t1.Result
            Dim oRet1 = t2.Result

        End Using


    End Sub
    ''' <summary>
    ''' Add EWB
    ''' </summary>
    ''' <param name="data">EWB Details</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Add(data As GenerateEWBInfo) As ResultInfo(Of EWBPostResponseInfo, HttpStatusCode)
        Try
            Using ctx = Me.GetServerEntity
                Dim oProc As New clsGSTNEwayBill(Me.WebController)
                Dim lst = (From obj In ctx.GSTReg Where obj.GSTIN = data.fromGstin).ToList
                If lst.Count = 0 Then lst = (From obj In ctx.GSTReg Where obj.GSTIN = data.toGstin).ToList
                If lst.Count > 0 Then
                    Dim ewb = Me.Save(ctx, lst(0), data)
                    If myUtils.cValTN(ewb.EwayBillNum) = 0 Then
                        Dim oret1 = oProc.Generate(lst(0).GSTRegID, data)
                        If (oret1.Data IsNot Nothing) AndAlso oret1.Data.ewayBillNo > 0 Then
                            If ewb IsNot Nothing Then
                                ewb.EwayBillNum = oret1.Data.ewayBillNo
                                Try
                                    ewb.EwayBillDate = Me.DateFromString(oret1.Data.ewayBillDate, "G")
                                    If Not String.IsNullOrEmpty(oret1.Data.validUpto) Then ewb.ValidUpto = Me.DateFromString(oret1.Data.validUpto, "G")
                                Catch ex As Exception
                                    Trace.WriteLine(ex.Message)
                                End Try
                                If ewb.EWayBillVehicle.Count > 0 Then
                                    ctx.SaveChanges()
                                    ewb.LastVehicleID = ewb.EWayBillVehicle(0).EWayBillVehicleID
                                End If
                                ctx.SaveChanges()
                            End If
                            Return BuildResponse(oret1.Data)
                        Else
                            Return BuildResponse(Of EWBPostResponseInfo)(Nothing, HttpStatusCode.InternalServerError, oret1.Message)
                        End If
                    Else
                        Dim dbData = New EWBPostResponseInfo() With {.ewayBillNo = ewb.EwayBillNum, .ewayBillDate = oProc.stringValue(ewb.EwayBillDate), .Status = 200, .validUpto = ewb.ValidUpto}
                        Return BuildResponse(dbData)

                    End If
                Else
                    Return BuildResponse(Of EWBPostResponseInfo)(Nothing, HttpStatusCode.InternalServerError, "GSTIN not registered")
                End If

            End Using
        Catch ex As Exception
            Return BuildExceptionResponse(Of EWBPostResponseInfo)(ex)
        End Try
    End Function

    ''' <summary>
    ''' Update EWB
    ''' </summary>
    ''' <param name="id">EWBId</param>
    ''' <param name="data">Taqg Details</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Update(id As Int64, data As GenerateEWBInfo) As ResultInfo(Of EWBPostResponseInfo, HttpStatusCode)
        Try

        Catch ex As Exception
            Return BuildExceptionResponse(Of EWBPostResponseInfo)(ex)
        End Try
    End Function

    ''' <summary>
    ''' Delete EWB
    ''' </summary>
    ''' <param name="id">EWBId</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Delete(id As Int64) As ResultInfo(Of Boolean, HttpStatusCode)
        Try

        Catch ex As Exception
            Return BuildExceptionResponse(Of Boolean)(ex)
        End Try
    End Function
    Public Function Save(ctx As mxgstEntities, reg As GSTReg, data As GenerateEWBInfo) As EwayBill
        Dim CustomerID, VendorID As Integer, DocType As String, rPartySub As DataRow
        Dim lstToPOS = (From obj In ctx.TaxArea Where obj.TINCode = data.toStateCode).ToList
        Dim lstFromPOS = (From obj In ctx.TaxArea Where obj.TINCode = data.fromStateCode).ToList

        Dim InvoiceDate = Me.DateFromString(data.docDate, "d")
        Dim PostingDate = Now.Date
        Dim oMasterdata As New clsMasterDataFICO(Me.WebController)
        Dim rPP = oMasterdata.rPostPeriod(PostingDate)
        Dim rFY As DataRow = Me.WebController.CommonData.rFinYear(rPP("finyearid"))
        Dim UniqueKey As String = ""

        If reg.GSTIN = data.toGstin Then
            'purchase
            DocType = "IP"
            VendorID = myFuncs2.AddUpdParty(Me.WebController, "V", "MS", data.fromGstin, data.fromTrdName, data.fromAddr1, data.fromAddr2, data.fromPlace, data.fromPincode, If(lstFromPOS.Count > 0, lstFromPOS(0).TaxAreaID, 0), rPartySub).ID
            UniqueKey = mxform.myFuncs.CalcUniqueKey(DocType, VendorID, rFY, data.docNo, 0)
        Else
            'sale
            DocType = "IS"
            CustomerID = myFuncs2.AddUpdParty(Me.WebController, "C", "", data.toGstin, data.toTrdname, data.toAddr1, data.toAddr2, data.toPlace, data.toPincode, If(lstToPOS.Count > 0, lstToPOS(0).TaxAreaID, 0), rPartySub).ID
            UniqueKey = mxform.myFuncs.CalcUniqueKey(DocType, reg.GSTRegID, rFY, data.docNo, 0)
        End If

        Dim lstCampus = (From obj In ctx.Campus Where obj.GSTRegID = reg.GSTRegID).ToList


        Dim lst = (From obj In ctx.Invoice Where obj.DocType = DocType AndAlso obj.InvoiceNum = data.docNo AndAlso obj.UniqueKey = UniqueKey).ToList
        Dim inv As Invoice
        If lst.Count > 0 Then
            'existing invoice
            inv = lst(0)
        ElseIf lstCampus.Count > 0 Then
            inv = New Invoice With {.InvoiceNum = data.docNo, .InvoiceDate = Me.DateFromString(data.docDate, "d"), .DocType = DocType}
            ctx.Invoice.Add(inv)
        End If

        If (Not inv Is Nothing) Then
            'Update existing invoice with new details.
            inv.CampusID = lstCampus(0).CampusID
            inv.ReturnPeriodID = rPP("PostPeriodID")
            inv.UniqueKey = UniqueKey
            If VendorID > 0 Then
                inv.VendorID = VendorID
            Else
                inv.CustomerID = CustomerID
            End If
            If lstToPOS.Count > 0 Then inv.POSTaxAreaID = lstToPOS(0).TaxAreaID
            inv.VAL = data.totInvValue
            inv.InvoiceItemGst.Clear()
            For Each ewbitem In data.ItemList
                Dim item = New InvoiceItemGst
                item.Description = ewbitem.productName
                item.Details = ewbitem.productDesc
                item.TXVAL = ewbitem.taxableAmount
                item.BasicRate = ewbitem.taxableAmount / ewbitem.quantity
                item.Qty = ewbitem.quantity
                item.Uqc = ewbitem.qtyUnit
                item.Hsn_Sc = ewbitem.hsnCode
                item.RT = ewbitem.sgstRate + ewbitem.cgstRate + ewbitem.igstRate
                item.CAMT = ewbitem.cgstRate / 100 * ewbitem.taxableAmount
                item.SAMT = ewbitem.sgstRate / 100 * ewbitem.taxableAmount
                item.IAMT = ewbitem.igstRate / 100 * ewbitem.taxableAmount
                item.CSAMT = ewbitem.cessRate / 100 * ewbitem.taxableAmount
                inv.InvoiceItemGst.Add(item)
            Next

            ctx.SaveChanges()

            Dim ewb As EwayBill
            Dim lstewb = (From obj In ctx.EwayBill Where obj.InvoiceID = inv.InvoiceID And (Not obj.cancelDate.HasValue)).ToList
            If lstewb.Count > 0 Then
                ewb = lstewb(0)
            Else
                'TODO: Use automapper and exclude date type fields (because of error in conversion)
                Dim rTrans As DataRow
                ewb = New EwayBill With {.InvoiceID = inv.InvoiceID, .CampusID = inv.CampusID, .DocNum = inv.InvoiceNum, .DocDate = inv.InvoiceDate,
                    .otherValue = data.otherValue, .cessNonAdvolValue = data.cessNonAdvolValue,
                    .actFromStateCode = data.actfromStateCode, .actToStateCode = data.actToStateCode,
                    .dispatchFromGstin = data.dispatchFromGSTIN, .dispatchFromTradeName = data.dispatchFromTradeName, .shipToGSTIN = data.shipToGSTIN, .shipToTradeName = data.shipToTradeName,
                    .DocType = data.docType, .transactionType = data.transactionType, .TransDistance = data.transDistance,
                    .fromAddr1 = data.fromAddr1, .fromAddr2 = data.fromAddr2, .fromPinCode = data.fromPincode, .fromPlace = data.fromPlace, .fromStateCode = data.fromStateCode,
                    .toAddr1 = data.toAddr1, .toAddr2 = data.toAddr2, .toPinCode = data.toPincode, .toPlace = data.toPlace, .toStateCode = data.toStateCode,
                    .SubSupplyType = data.subSupplyType, .SupplyType = data.supplyType}

                If Not String.IsNullOrEmpty(data.transporterId) Then
                    ewb.TransVendorID = myFuncs2.AddUpdParty(Me.WebController, "V", "TR", data.transporterId, data.transporterName, "", "", "", "", 0, rTrans).ID
                End If
                ewb.TransDistance = data.transDistance
                If Not String.IsNullOrEmpty(data.vehicleNo) Then
                    Dim veh As New EWayBillVehicle With {.FromPlace = data.fromPlace, .UserGSTINTransin = reg.GSTIN,
                        .TransDocNo = data.transDocNo, .TransMode = data.transMode, .VehicleNo = data.vehicleNo, .VehicleType = data.VehicleType}
                    If Not String.IsNullOrEmpty(data.transDocDate) Then veh.TransDocDate = Me.DateFromString(data.transDocDate, "d")

                    ewb.EWayBillVehicle.Add(veh)
                End If
                ctx.EwayBill.Add(ewb)
            End If

            Return ewb
        End If

    End Function
    Public Async Function GeneratePDF(ewbnum As Long) As Task(Of Stream) Implements IEWBRepository.GeneratePDF
        Try
            Using ctx = Me.GetServerEntity
                Dim lst = (From ewb In ctx.EwayBill Where ewb.EwayBillNum = ewbnum).ToList
                If lst.Count > 0 Then
                    Dim vwModel As New clsViewModel(Me.WebController)
                    Dim output = Await Me.WebController.PrintingPress.SpecReportStream(vwModel, "crpewaybill", "", lst(0).EwayBillID)
                    Return output
                Else
                    Return Me.WebController.PrintingPress.TextAndPicStream("Not found", "Ewaybill")

                End If

            End Using


        Catch ex As Exception
            Return Me.WebController.PrintingPress.TextAndPicStream(ex.Message, "Ewaybill")

        End Try
    End Function

    Public Function Cancel(info As EWBCancelRequestInfo) As ResultInfo(Of EWBCancelResponseInfo, HttpStatusCode) Implements IEWBRepository.Cancel
        Try
            Using ctx = Me.GetServerEntity
                Dim lst = (From ewb In ctx.EwayBill Where ewb.EwayBillNum = info.ewbNo).ToList
                If lst.Count > 0 Then
                    Dim CampusID = lst(0).CampusID
                    Dim lstcampus = (From obj In ctx.Campus Where obj.CampusID = CampusID).ToList
                    Dim oProc As New clsGSTNEwayBill(Me.WebController)
                    Dim oRet1 = oProc.Cancel(lstcampus(0).GSTRegID, info)
                    If (oRet1.Data IsNot Nothing) AndAlso (Not String.IsNullOrEmpty(oRet1.Data.cancelDate)) Then
                        lst(0).cancelDate = Me.DateFromString(oRet1.Data.cancelDate, "G")
                        ctx.SaveChanges()
                        Return BuildResponse(oRet1.Data)
                    Else
                        Return BuildResponse(Of EWBCancelResponseInfo)(Nothing, HttpStatusCode.InternalServerError, oRet1.Message)

                    End If
                Else
                    Return BuildResponse(Of EWBCancelResponseInfo)(Nothing, HttpStatusCode.InternalServerError, "GSTIN not registered")
                End If

            End Using
        Catch ex As Exception
            Return BuildExceptionResponse(Of EWBCancelResponseInfo)(ex)
        End Try
    End Function
    Public Function DateFromString(str1 As String, formatString As String) As DateTime
        Dim provider = New CultureInfo("en-TT")
        Dim dated = DateTime.ParseExact(str1, formatString, provider)
        Return dated
        'http://www.basicdatepicker.com/samples/cultureinfo.aspx
        'https://msdn.microsoft.com/en-us/library/5hh873ya(v=vs.100).aspx
    End Function

    Public Function Clear(GSTIN As String) As ResultInfo(Of Boolean, HttpStatusCode) Implements IEWBRepository.Clear
        Try
            Dim oMasterdata As New clsMasterDataFICO(Me.WebController)
            Dim rGstReg = oMasterdata.GstRegRow2(GSTIN)
            If rGstReg Is Nothing Then
                Return BuildResponse(Of Boolean)(False, HttpStatusCode.InternalServerError, "GSTIN Not found")
            Else
                Dim oProc As New clsGSTNEwayBill(Me.WebController)
                Dim rToken = oProc.GetActiveToken(Me.WebController.Police.UniqueUserID, rGstReg("gstregid"))
                If rToken Is Nothing Then
                    Return BuildResponse(Of Boolean)(False, HttpStatusCode.InternalServerError, "Token Not found")
                Else
                    rToken("clearedon") = Now
                    Me.WebController.DataProvider.objSQLHelper.SaveResults(rToken.Table, "select gstntokenid,clearedon from gstntoken where 0=1")
                    Return BuildResponse(Of Boolean)(True, HttpStatusCode.OK, "Token Cleared")
                End If
            End If
        Catch ex As Exception
            Return BuildExceptionResponse(Of Boolean)(ex)
        End Try

    End Function
End Class