
using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using testeBot2.CognitiveModels;

namespace testeBot2.Requisicao
{
    public class Program
    {
        public ZeCognitiveModel model = new ZeCognitiveModel();
        public static void get(string query)
        {
            var requisicaoWeb = WebRequest.CreateHttp("https://botproject.cognitiveservices.azure.com/luis/prediction/v3.0/apps/85f5328f-243e-44cb-bca2-a4b4b09e9ca4/slots/staging/predict?verbose=true&show-all-intents=true&log=true&subscription-key=334cda87740b46f990f42de1200c77cc&query="+query);
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "Teste";
            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                var post = JsonConvert.DeserializeObject<ZeCognitiveModel>(objResponse.ToString());

                //Console.WriteLine(post.Id + " " + post.title + " " + post.body);
                Console.ReadLine();
                streamDados.Close();
                resposta.Close();
            }
            Console.ReadLine();
        }
    }
}
