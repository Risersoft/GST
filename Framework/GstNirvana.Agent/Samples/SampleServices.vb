Imports Microsoft.Extensions.Logging

Public Interface ISampleServiceA
    Sub DoIt()
End Interface

Public Class SampleServiceA
        Implements ISampleServiceA

        Private ReadOnly _logger As ILogger

        Public Sub New(ByVal logger As ILogger(Of SampleServiceA))
            _logger = logger
        End Sub

        Public Sub DoIt() Implements ISampleServiceA.DoIt
            _logger.LogInformation("SampleServiceA.DoIt invoked!")
        End Sub
    End Class

Public Interface ISampleServiceB
    Sub DoIt()
End Interface

Public Class SampleServiceB
        Implements ISampleServiceB

        Private ReadOnly _logger As ILogger

        Public Sub New(ByVal logger As ILogger(Of SampleServiceB))
            _logger = logger
        End Sub

        Public Sub DoIt() Implements ISampleServiceB.DoIt
            _logger.LogInformation("SampleServiceB.DoIt invoked!")
        End Sub
    End Class
