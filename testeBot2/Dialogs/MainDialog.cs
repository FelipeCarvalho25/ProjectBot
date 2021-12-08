using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using testeBot2.Dialogs;
using testeBot2.Models;
using testeBot2.Requisicao;


namespace testeBot2.Dialogs
{
    public class MainDialog : ComponentDialog
    {
        private readonly RequisicaoHttp luisReturn = new RequisicaoHttp();
        public MainDialog(Openchamado openChamado)
            : base(nameof(MainDialog))
        {

            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(openChamado);
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                IntroStepAsync,
                ActStepAsync,
                FinalStepAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> IntroStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            // Use the text provided in FinalStepAsync or the default if it is the first time.
            var messageText = stepContext.Options?.ToString() ?? $"Olá em que posso ajudar?";
            var promptMessage = MessageFactory.Text(messageText, messageText, InputHints.ExpectingInput);
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
        }

        private async Task<DialogTurnResult> ActStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            // Call LUIS and gather any potential booking details. (Note the TurnContext has the response to the prompt.)
            var luisResult = luisReturn.httpGet.get((string)stepContext.Result) ;
            if (luisResult == "Saudação")
            {
                var didntUnderstandMessageText = $"Boa noite";
                var didntUnderstandMessage = MessageFactory.Text(didntUnderstandMessageText, didntUnderstandMessageText, InputHints.IgnoringInput);
                await stepContext.Context.SendActivityAsync(didntUnderstandMessage, cancellationToken);
            }
            else if (luisResult.Equals("Abrir chamado"))
            {
                return await stepContext.BeginDialogAsync(nameof(Openchamado), new Chamado(), cancellationToken);
            }
            else if (luisResult.Equals("Jornada.MOPP"))
            {
                var didntUnderstandMessageText = $"Nossa jornada é...";
                var didntUnderstandMessage = MessageFactory.Text(didntUnderstandMessageText, didntUnderstandMessageText, InputHints.IgnoringInput);
                await stepContext.Context.SendActivityAsync(didntUnderstandMessage, cancellationToken);
                //return await stepContext.BeginDialogAsync(nameof(openChamado), cancellationToken);
            }
            else if (luisResult.Equals("Jornada.Ocorrencias"))
            {
                var didntUnderstandMessageText = $"Nossa jornada é...";
                var didntUnderstandMessage = MessageFactory.Text(didntUnderstandMessageText, didntUnderstandMessageText, InputHints.IgnoringInput);
                await stepContext.Context.SendActivityAsync(didntUnderstandMessage, cancellationToken);
                //return await stepContext.BeginDialogAsync(nameof(openChamado), cancellationToken);
            }
            else if (luisResult.Equals("Jornada.Refeição"))
            {
                var didntUnderstandMessageText = $"Nossa jornada é...";
                var didntUnderstandMessage = MessageFactory.Text(didntUnderstandMessageText, didntUnderstandMessageText, InputHints.IgnoringInput);
                await stepContext.Context.SendActivityAsync(didntUnderstandMessage, cancellationToken);
                //return await stepContext.BeginDialogAsync(nameof(openChamado), cancellationToken);
            }
            else if (luisResult.Equals("IndagaPrazo"))
            {
                var didntUnderstandMessageText = $"Nossa jornada é...";
                var didntUnderstandMessage = MessageFactory.Text(didntUnderstandMessageText, didntUnderstandMessageText, InputHints.IgnoringInput);
                await stepContext.Context.SendActivityAsync(didntUnderstandMessage, cancellationToken);
                //return await stepContext.BeginDialogAsync(nameof(openChamado), cancellationToken);
            }
            else
            {
                var didntUnderstandMessageText = $"Desculpe, não entendi o que você quis dizer";
                var didntUnderstandMessage = MessageFactory.Text(didntUnderstandMessageText, didntUnderstandMessageText, InputHints.IgnoringInput);
                await stepContext.Context.SendActivityAsync(didntUnderstandMessage, cancellationToken);
            }
            

            return await stepContext.NextAsync(null, cancellationToken);
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // If the child dialog ("BookingDialog") was cancelled, the user failed to confirm or if the intent wasn't BookFlight
            // the Result here will be null

            // Restart the main dialog with a different message the second time around
            var promptMessage = "Posso ajudar em mais alguma coisa??";
            return await stepContext.ReplaceDialogAsync(InitialDialogId, promptMessage, cancellationToken);
        }
    }
   
}
