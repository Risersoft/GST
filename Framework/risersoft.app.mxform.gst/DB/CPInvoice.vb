'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class CPInvoice
    Public Property CPInvoiceID As Integer
    Public Property TenantID As System.Guid
    Public Property GstRegID As Nullable(Of Integer)
    Public Property VendorID As Nullable(Of Integer)
    Public Property CustomerID As Nullable(Of Integer)
    Public Property InvoiceID As Nullable(Of Integer)
    Public Property CTIN As String
    Public Property CFS As String
    Public Property POS As String
    Public Property UpdBy As String
    Public Property Flag As String
    Public Property CFlag As String
    Public Property OPD As String
    Public Property POSTaxAreaID As Nullable(Of Integer)
    Public Property DocType As String
    Public Property GstInvoiceType As String
    Public Property Chksum As String
    Public Property INUM As String
    Public Property Idt As Nullable(Of Date)
    Public Property inv_typ As String
    Public Property val As Nullable(Of Decimal)
    Public Property BillOf As String
    Public Property RCHRG As String
    Public Property IsAmendment As Nullable(Of Boolean)
    Public Property PostingDate As Nullable(Of Date)
    Public Property CreatedOn As Nullable(Of Date)
    Public Property CreatedBy As String
    Public Property LastUpdated As Nullable(Of Date)
    Public Property ModifiedBy As String
    Public Property LastModApp As String
    Public Property Remark As String
    Public Property OINum As String
    Public Property OIdt As Nullable(Of Date)
    Public Property ETIN As String
    Public Property port_code As String
    Public Property nt_num As String
    Public Property nt_dt As Nullable(Of Date)
    Public Property ont_num As String
    Public Property ont_dt As Nullable(Of Date)
    Public Property ntty As String
    Public Property MatchCode As String
    Public Property DLTransactionId As Nullable(Of System.Guid)
    Public Property ActionFlag As String
    Public Property ActionTransactionId As Nullable(Of System.Guid)
    Public Property MarkPending As Nullable(Of Boolean)
    Public Property ReturnPeriodID As Nullable(Of Integer)
    Public Property Diff_Percent As Nullable(Of Integer)
    Public Property p_gst As String
    Public Property MMDescrip As String
    Public Property ImportFileID As Nullable(Of System.Guid)

    Public Overridable Property GSTReg As GSTReg
    Public Overridable Property CPInvoiceItem As ICollection(Of CPInvoiceItem) = New HashSet(Of CPInvoiceItem)

End Class
