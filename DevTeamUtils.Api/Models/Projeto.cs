using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DevTeamUtils.Api.Models
{
    [DataContract]
    public class Projeto : BaseModel
    {
        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string Codigo { get; set; }

        [DataMember]
        public string Autor { get; set; }

        [DataMember]
        public int Tarefa { get; set; }

        [DataMember]
        public string Descricao { get; set; }

        [DataMember]
        public string Passo { get; set; }

        [DataMember]
        public bool Validado {
            get
            {
                bool validado = true;
                if (Tabelas != null)
                {
                    for (int i = 0; i <= Tabelas.Count - 1; i++)
                    {
                        if (!Tabelas[i].Validado)
                        {
                            validado = Tabelas[i].Validado;
                        }
                    }
                }
                else
                {
                    validado = false;
                }
                return validado;
            }
            private set { } }

        [DataMember]
        public List<Tabela> Tabelas { get; private set; }

        [BsonConstructor]
        public Projeto() : base()
        {
            List<Tabela> Tabelas = new List<Tabela>();
        }

        public void DeserializeJson(JObject json)
        {
            //Id = ((JValue)json.SelectToken("bairro")).Value.ToBson();
            Nome = getTokenValue(json, "nome");
            Codigo = getTokenValue(json, "codigo");
            Autor = getTokenValue(json, "autor");            
            Tarefa = int.Parse(getTokenValue(json, "tarefa"));
            Descricao = getTokenValue(json, "descricao");
            Passo = getTokenValue(json, "passo");
            Validado = false;
            if (!string.IsNullOrEmpty(getTokenValue(json, "validado")))
                Validado = System.Convert.ToBoolean(getTokenValue(json, "validado"));
        }
    }
}
