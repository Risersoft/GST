Imports System.IO
Imports System.Net
Imports GSTN.API.Library.Models.EWB
Imports risersoft.shared.portable.Model
Imports risersoft.shared.cloud.IRepository

Public Interface IEWBRepository
    Inherits IRepositoryBase(Of EWBInfo, Int64, GenerateEWBInfo, EWBPostResponseInfo, RSCallerInfo, HttpStatusCode)

    Function GeneratePDF(ewbnum As Long) As Task(Of Stream)
    Function Cancel(info As EWBCancelRequestInfo) As ResultInfo(Of EWBCancelResponseInfo, HttpStatusCode)
    Function Clear(GSTIN As String) As ResultInfo(Of Boolean, HttpStatusCode)
End Interface
