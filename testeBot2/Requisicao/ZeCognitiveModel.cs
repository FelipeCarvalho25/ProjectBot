using Microsoft.Bot.Builder;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace testeBot2.CognitiveModels
{
    public class ZeCognitiveModel
    {
        public string query { get; set; }
        public string AlteredText;

        public class _Prediction
        {
            public string topIntent { get; set; }
        }
        public _Prediction prediction;
        public class _Entities
        {
            // Built-in entities
            public string acao;
            public string chamado;
            public string incidente;
            public string Mopp;
            public string ocorrencia;
            public string prazo;
            public string problema;
            public string refeicao;
            public string saber;
            public string tempo;
            // Lists
            public string[][] Abertura;
            public string[][] Prazo;
            public string[][] Saber;


            // Instance
            public class _Instance
            {
                public string[] acao;
                public string[] chamado;
                public string[] incidente;
                public string[] Mopp;
                public string[] ocorrencia;
                public string[] prazo;
                public string[] problema;
                public string[] refeicao;
                public string[] saber;
                public string[] tempo;
                public string[] Abertura;
                public string[] Prazo;
                public string[] Saber;
            }
            [JsonProperty("$instance")]
            public _Instance _instance;
        }
        public _Entities Entities;

    }
}
