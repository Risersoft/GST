Imports System.Reflection
Imports System.Threading
Imports System.Threading.Tasks
Imports AdaptiveCards
Imports Microsoft.Bot.Builder
Imports Microsoft.Bot.Builder.Dialogs
Imports Microsoft.Bot.Builder.Dialogs.Choices
Imports Microsoft.Bot.Schema
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.Logging
Imports Newtonsoft.Json.Linq
Imports risersoft.shared
Imports risersoft.shared.bot
Imports risersoft.shared.cloud.Providers
Imports risersoft.shared.portable.Models.Auth
Imports risersoft.shared.web
Imports risersoft.API.GSTN

Public Class PublicDialog
    Inherits ComponentDialog
    Protected ReadOnly _logger As ILogger
    Private Const ErrorMessage As String = "Not a valid option"
    Public Sub New(ByVal configuration As IConfiguration, ByVal logger As ILogger)
        MyBase.New("public")
        _logger = logger
        AddDialog(New TextPrompt(NameOf(TextPrompt)))
        AddDialog(New WaterfallDialog(NameOf(WaterfallDialog),
        New WaterfallStep() {AddressOf PromptStepAsync, AddressOf ProcessStepAsync}))
        InitialDialogId = NameOf(WaterfallDialog)
    End Sub

    Protected Friend Async Function PromptStepAsync(stepContext As WaterfallStepContext, cancellationToken As CancellationToken) As Task(Of DialogTurnResult)

        Dim bot = stepContext.Context.TurnState.Get(Of ActivityBotBase)("bot")
        If bot.Chat Is Nothing Then
            Await stepContext.Context.SendActivityAsync(MessageFactory.Text("Please register first."), cancellationToken)
            Return Await stepContext.EndDialogAsync(cancellationToken:=cancellationToken)
        Else
            Dim attrib = bot.GetType.GetCustomAttribute(Of BotId)

            Dim GreetMessage = $"Welcome to **{attrib.BotName} chat bot from {bot.myHostedController.myWebController.CalcPublisher}**." & vbLf & vbLf & "Please enter GSTIN:"
            Return Await stepContext.PromptAsync(NameOf(TextPrompt), New PromptOptions() With {
                .Prompt = MessageFactory.Text(GreetMessage),
                .RetryPrompt = MessageFactory.Text(ErrorMessage)
            }, cancellationToken)
        End If
    End Function
    Private Async Function ProcessStepAsync(ByVal stepContext As WaterfallStepContext, ByVal cancellationToken As CancellationToken) As Task(Of DialogTurnResult)
        If stepContext.Result IsNot Nothing Then
            Dim GSTIN As String = myUtils.cStrTN(stepContext.Result)
            Dim bot = stepContext.Context.TurnState.Get(Of ActivityBotBase)("bot")
            If Await bot.myHostedController.myWebController.CheckInitModel(bot.myHostedController.myWebController.GetAppInfo, False) Then
                If GSTUtils.ValidateGSTIN(GSTIN) Then
                    Dim oProc As New clsGSTNPublic(bot.myHostedController)
                    oProc.ForceEnv = "Prod"
                    oProc.DefaultGSP = Function(x) "KPMG"
                    Dim result = oProc.SearchGSTIN(GSTIN).Data
                    Dim card = New AdaptiveCard(New AdaptiveSchemaVersion(1, 0))
                    card.Body.Add(New AdaptiveTextBlock() With {.Text = result.gstin, .Size = AdaptiveTextSize.Large})
                    card.Body.Add(New AdaptiveTextBlock() With {.Text = result.lgnm, .Size = AdaptiveTextSize.Medium})
                    card.Body.Add(New AdaptiveTextBlock() With {.Text = result.dty, .Size = AdaptiveTextSize.Small})
                    card.Body.Add(New AdaptiveTextBlock() With {.Text = result.ctb, .Size = AdaptiveTextSize.Small})
                    card.Body.Add(New AdaptiveTextBlock() With {.Text = myUtils.MakeCSV(result.nba, vbCrLf), .Size = AdaptiveTextSize.Small})
                    card.Body.Add(New AdaptiveImage() With {.Url = New Uri("http://www.risersoft.com/Content/images/mpc.png"), .Size = AdaptiveImageSize.Medium})
                    Await stepContext.Context.SendActivityAsync(MessageFactory.Attachment(New Attachment With
                        {.ContentType = AdaptiveCard.ContentType, .Content = JObject.FromObject(card)}), cancellationToken:=cancellationToken)
                Else
                    Await stepContext.Context.SendActivityAsync("Invalid GSTIN format.", cancellationToken:=cancellationToken)
                End If
            Else
                Await stepContext.Context.SendActivityAsync("Cannot Init.", cancellationToken:=cancellationToken)
            End If
        Else
            Await stepContext.Context.SendActivityAsync(MessageFactory.Text("We couldn't log you in. Please try again later."), cancellationToken)
        End If
        Return Await stepContext.EndDialogAsync(cancellationToken:=cancellationToken)

    End Function

End Class
