Imports System.Data.Entity
Imports AutoMapper
Imports GSTN.API.Library.Models.GstNirvana

Public Class InvoiceRepo
    Dim _ctx As mxgstEntities, mapper As IMapper
    Public Sub New(context As mxgstEntities)
        _ctx = context
        Dim config = myFuncs2.InitializeMapper()
        mapper = config.CreateMapper()
    End Sub
    Public Function GetInvoice(Id As Integer) As GstInvoiceInfo
        Dim _invoice = (From obj In _ctx.Invoice Where (obj.InvoiceID = Id)).Include(Function(b) b.InvoiceItemGst).FirstOrDefault()
        Dim proxy = Mapper.Map(Of GstInvoiceInfo)(_invoice)
        Return proxy
    End Function
    Public Sub SaveInvoice(ByRef info As GstInvoiceInfo)

        Dim id As Integer = myUtilsBase.cValTN(info.InvoiceID)
        Dim _dbinvoice = (From obj In _ctx.Invoice Where (obj.InvoiceID = id)).Include(Function(b) b.InvoiceItemGst).FirstOrDefault()

        If _dbinvoice Is Nothing Then
            'add
            _dbinvoice = Mapper.Map(Of Invoice)(info)
            _ctx.Invoice.Add(_dbinvoice)
            'automatically maps gstinvoiceiteminfo --> InvoiceItemGst
        Else
            ' Update parent
            _ctx.Entry(_dbinvoice).CurrentValues.SetValues(info)

            ' Delete children
            For Each existingChild In _dbinvoice.InvoiceItemGst.ToList()
                If Not info.InvoiceItems.Any(Function(c) c.InvoiceItemGstID = existingChild.InvoiceItemGSTID) Then
                    _ctx.InvoiceItemGst.Remove(existingChild)
                End If
            Next

            ' Update and Insert children
            For Each childModel In info.InvoiceItems
                Dim existingChild = _dbinvoice.InvoiceItemGst.Where(Function(c) c.InvoiceItemGSTID = childModel.InvoiceItemGstID).SingleOrDefault()

                If existingChild IsNot Nothing Then
                    ' Update child
                    _ctx.Entry(existingChild).CurrentValues.SetValues(childModel)
                Else
                    ' Insert child
                    _dbinvoice.InvoiceItemGst.Add(Mapper.Map(Of InvoiceItemGst)(childModel))
                End If
            Next

        End If
        _ctx.SaveChanges()


        info = Mapper.Map(Of GstInvoiceInfo)(_dbinvoice)

        'https://stackoverflow.com/questions/27176014/how-to-add-update-child-entities-when-updating-a-parent-entity-in-ef

    End Sub
End Class
