// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.15.0

using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using testeBot2.CognitiveModels;

namespace testeBot2.Bots
{
    public class EchoBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var replyText = $"Echo:";
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
            await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
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

        public static string get(string query)
        {
            var requisicaoWeb = WebRequest.CreateHttp("https://botproject.cognitiveservices.azure.com/luis/prediction/v3.0/apps/85f5328f-243e-44cb-bca2-a4b4b09e9ca4/slots/staging/predict?verbose=true&show-all-intents=true&log=true&subscription-key=334cda87740b46f990f42de1200c77cc&query=" + query);
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "Teste";
            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                var post = JsonConvert.DeserializeObject<ZeCognitiveModel>(objResponse.ToString());
                return post.prediction.topIntent;
                
            }
          
        }
    }
}
