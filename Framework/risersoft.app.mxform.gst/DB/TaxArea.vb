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

Partial Public Class TaxArea
    Public Property TaxAreaID As Integer
    Public Property TaxAreaCode As String
    Public Property Descrip As String
    Public Property TINCode As String
    Public Property TaxAreaCode2 As String

    Public Overridable Property GSTReg As ICollection(Of GSTReg) = New HashSet(Of GSTReg)

End Class
