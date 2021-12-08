// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.15.0

using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using testeBot2.CognitiveModels;
using Microsoft.Bot.Builder.Dialogs;
using System;


namespace testeBot2.Bots
{
    public class Zebot<T>: ActivityHandler
        where T : Dialog
    {
        protected readonly Dialog Dialog;
        protected readonly BotState ConversationState;
        protected readonly BotState UserState;

        public Zebot(ConversationState conversationState, UserState userState, T dialog)
        {
            ConversationState = conversationState;
            UserState = userState;
            Dialog = dialog;
            
        }
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            /* var replyText = $"Echo:";
             var respLUIS = get(turnContext.Activity.Text);
             if (respLUIS == "Saudação")
             {
                 replyText = $"{turnContext.Activity.Text}";
             }
             else if (respLUIS.Equals("Abrir chamado"))
             {
                 replyText = $"Descreva seu problema:";
             }
             else if(respLUIS.Equals("Jornada.MOPP"))
             {
                 replyText = $"Qual a sua dúvida?:";
             }
             else if(respLUIS.Equals("Jornada.Ocorrencias"))
             {
                 replyText = $"Qual a sua dúvida?:";
             }
             else if(respLUIS.Equals("Jornada.Refeição"))
             {
                 replyText = $"Qual a sua dúvida?:";
             }
             else if(respLUIS.Equals("IndagaPrazo"))
             {
                 replyText = $"Qual a requisição?:";
             }
             else
             {
                 replyText = $"Desculpe, não entendi o que falou, tente 'Quero abrir um chamado':";
             }
             await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);*/
            // Run the Dialog with the new message Activity.
            await Dialog.RunAsync(turnContext, ConversationState.CreateProperty<DialogState>("DialogState"), cancellationToken);
        }
        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            await base.OnTurnAsync(turnContext, cancellationToken);

            // Save any state changes that might have occurred during the turn.
            await ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            await UserState.SaveChangesAsync(turnContext, false, cancellationToken);
        }
        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }

        
    }
}
