Imports System.Configuration
Imports Newtonsoft.Json
Imports risersoft.app.mxengg
Imports risersoft.shared
Imports risersoft.shared.cloud.common

Public Class FMTaskProvider
    Inherits clsTaskProviderBase
    'File monitor Task Provider
    Public Overrides ReadOnly Property IsApiTask As Boolean = False
    Protected Friend fileProvider As clsFileProviderClientBase
    Public Sub New(controller As clsAppController)
        MyBase.New(controller)
    End Sub

    Public Overrides Function ExecuteServer(rTask As DataRow, input As clsProcOutput) As Task(Of clsProcOutput)
        Dim oRet As New clsProcOutput
        Try
            fileProvider = myContext.App.objAppExtender.FileProviderClient(myContext, myUtils.cStrTN(rTask("appcode")), myUtils.cStrTN(rTask("actionsubtype")))
            fileProvider.ActivateSettings(myUtils.cStrTN(rTask("fileconnectionjson")), myUtils.cStrTN(rTask("folderusername")), myUtils.cStrTN(rTask("folderpassword")), myUtils.cStrTN(rTask("foldersourcepath")))

            Dim lst = fileProvider.ListDirectory("")

            Dim sql As String = "select * from apitask where dbschedtaskid=" & myUtils.cValTN(rTask("dbschedtaskid")) & " and filename is not null"
            Dim dt1 As DataTable = myContext.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
            Dim DoneList = dt1.Select().Select(Function(r1)
                                                   Return New clsFileInfo() With {.Name = myUtils.cStrTN(r1("filename")), .LastModified = r1("filetime")}
                                               End Function).ToList
            Dim AddList = fileProvider.FindAddedFiles(lst, DoneList)
            For Each info In AddList
                Dim dic As New Dictionary(Of String, String)
                dic.Add("path", info.Name)
                Dim str1 As String = JsonConvert.SerializeObject(dic)
                Dim dicParams As New Dictionary(Of String, String) From {{"InfoJson", str1}, {"filename", info.Name}, {"filetime", info.LastModified}}
                Dim rTask2 = TaskProviderFactory.CreateApiTask(myContext.DataProvider, myUtils.cStrTN(rTask("queueactiontype")), myUtils.cStrTN(rTask("queueactionsubtype")), 0, dicParams)
                Dim queueName = myContext.Controller.CalcQueueName
                Dim oRet2 = TaskProviderFactory.Enqueue(myContext.DataProvider, rTask2, queueName)
            Next
        Catch ex As Exception
            oRet.AddException(ex)
        End Try
        Return Task.FromResult(oRet)
    End Function

End Class
