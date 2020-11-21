Imports Serilog
Imports Serilog.Events

Public Class TraceListener2
    Inherits System.Diagnostics.TraceListener

    Public Overrides Sub Write(message As String)
        LogMessage(message)
    End Sub

    Public Overrides Sub WriteLine(message As String)
        LogMessage(message)
    End Sub

    Private Sub LogMessage(message As String)
        Log.Logger.Write(LogEventLevel.Information, message)
    End Sub
End Class
