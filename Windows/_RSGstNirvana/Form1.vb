Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
Imports risersoft.shared
Imports risersoft.shared.win
Imports System.Data.SqlClient

Public Partial Class Form1
    Inherits Form

    Private WithEvents Query As clsSQLHelperClient
    Public Sub New()
		InitializeComponent()
	End Sub

    Public Sub RunQuery()
        Query = New clsSQLHelperClient(myWinApp)
        AddHandler Query.objCallBack.QueryCompleted, AddressOf OnQueryCompleted
        AddHandler Query.objCallBack.AfterQueryChunkLoad, AddressOf OnQueryChunkCompleted
        AddHandler Query.objCallBack.BeforeQueryChunkLoad, AddressOf BeforeQueryChunkLoad

        gvMain.DataSource = Nothing
        gvMain.ResetDisplayLayout()
        gvMain.Layouts.Clear()
        Dim ds As DataSet = Query.ExecuteDatasetAsync(Me, myWinApp.Controller.DataProvider.dbConn, CommandType.Text, txtSQL.Text)
        gvMain.DataSource = ds
    End Sub

    Private Sub btnRunQuery_Click(sender As Object, e As EventArgs) Handles btnRunQuery.Click
        btnRunQuery.Enabled = False
        btnStopQuery.Enabled = True
        tcMain.SelectedIndex = 1
        lblQueryStatus.Text = "Running..."
        lblRowsReturned.Text = String.Format("Begin Execute")
        RunQuery()
    End Sub
    Public Sub OnQueryCompleted(sender As Object, args As EventArgs)

        lblQueryStatus.Text = "Query Completed"
        lblRowsReturned.Text = String.Format("{0} rows returned", Query.objCallBack.rows)
        btnStopQuery.Enabled = False
        btnRunQuery.Enabled = True
        Me.Refresh()
        'gvMain.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Automatic

    End Sub
    Public Sub BeforeQueryChunkLoad(sender As Object, args As EventArgs)
        'gvMain.SuspendLayout()


    End Sub
    Public Sub OnQueryChunkCompleted(sender As Object, args As EventArgs)
        Debug.WriteLine(String.Format("Chunks: {0}, Rows: {1}", Query.objCallBack.cnt, Query.objCallBack.rows))
        lblRowsReturned.Text = String.Format("{0} rows returned", Query.objCallBack.rows)

        'gvMain.ActiveRowScrollRegion.ResetScrollbar()
        'gvMain.ResumeLayout()
    End Sub
    Private Sub btnStopQuery_Click(sender As Object, e As EventArgs) Handles btnStopQuery.Click
        If Query IsNot Nothing Then
            Query.CancelQuery()
        End If
    End Sub
End Class
