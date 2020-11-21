Imports risersoft.shared
Imports risersoft.API.GSTN

Public Class clsJINTFuncsGSTN
    Dim myContext As IProviderContext
    Public Sub New(context As IProviderContext)
        myContext = context
    End Sub
    Public Function IsActiveGSTIN(gstin As String) As Boolean
        Return GSTUtils.ValidateGSTIN(gstin) OrElse (GSTUtils.ValidateUIN(gstin))
    End Function
End Class
