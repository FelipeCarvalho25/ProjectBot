using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System.Threading;
using System.Threading.Tasks;
using testeBot2.Models;

namespace testeBot2.Dialogs
{
    public class Openchamado : ComponentDialog
    {
        private const string DestinationStepMsgText = "Where would you like to travel to?";
        private const string OriginStepMsgText = "Where are you traveling from?";

        public Openchamado()
            : base(nameof(Openchamado))
        {
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                NameStepAsync,
                ProblemaStepAsync,
                ConfirmStepAsync,
                FinalStepAsync

            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> NameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var chamadoDetails = (Chamado)stepContext.Options;

            if (chamadoDetails.Solicitante == null)
            {
                var promptMessage = MessageFactory.Text(DestinationStepMsgText, DestinationStepMsgText, InputHints.ExpectingInput);
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
            }

            return await stepContext.NextAsync(chamadoDetails.Solicitante, cancellationToken);
        }

        private async Task<DialogTurnResult> ProblemaStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var chamadoDetails = (Chamado)stepContext.Options;

            chamadoDetails.Problema = (string)stepContext.Result;

            if (chamadoDetails.Problema == null)
            {
                var promptMessage = MessageFactory.Text(OriginStepMsgText, OriginStepMsgText, InputHints.ExpectingInput);
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
            }

            return await stepContext.NextAsync(chamadoDetails.Problema, cancellationToken);
        }

        private async Task<DialogTurnResult> ConfirmStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var chamadoDetails = (Chamado)stepContext.Options;

            chamadoDetails.Problema = (string)stepContext.Result;

            var messageText = $"Por favor confirme seu nome: {chamadoDetails.Solicitante} seu problema: {chamadoDetails.Problema}. Está correto?";
            var promptMessage = MessageFactory.Text(messageText, messageText, InputHints.ExpectingInput);

            return await stepContext.PromptAsync(nameof(ConfirmPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
        }


        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((bool)stepContext.Result)
            {
                var chamadoDetails = (Chamado)stepContext.Options;

                return await stepContext.EndDialogAsync(chamadoDetails, cancellationToken);
            }

            return await stepContext.EndDialogAsync(null, cancellationToken);
        }

    }
}
