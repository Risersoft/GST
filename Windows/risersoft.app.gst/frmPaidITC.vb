
Public Class frmPaidITC
    Inherits frmMax
    Friend fMat As frmGSTNGSTR3B

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.InitForm()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub InitForm()

    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        If Me.GetSoftData(oView, prepMode, prepIDX) Then
            PrepForm = True
        End If
    End Function

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click, btnCancel.Click
        Me.InitError()
        If Me.CanSave Then
            cm.EndCurrentEdit()
            Me.SaveSoftData()
        End If
    End Sub

End Class