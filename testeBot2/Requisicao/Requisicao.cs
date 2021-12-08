
using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using testeBot2.CognitiveModels;

namespace testeBot2.Requisicao
{
    public class RequisicaoHttp
    {
        public ZeCognitiveModel model = new ZeCognitiveModel();
        public class  HttpGet
        {
            public HttpGet()
            {
               
            }
            public string get(string query_)
            {
                var requisicaoWeb = WebRequest.CreateHttp("https://botproject.cognitiveservices.azure.com/luis/prediction/v3.0/apps/85f5328f-243e-44cb-bca2-a4b4b09e9ca4/slots/staging/predict?verbose=true&show-all-intents=true&log=true&subscription-key=334cda87740b46f990f42de1200c77cc&query=" + query_);
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
        public HttpGet httpGet;
    }
}
