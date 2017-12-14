using DevTeamUtils.Api.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DevTeamUtils.Api.Utils
{
    public static partial class Connection
    {
        static string _connectionString;
        public static string TestConnection(string connectionString, string uri)
        {
            _connectionString = connectionString;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            TimeSpan timeSpan = new TimeSpan(0, 5, 0);
            client.Timeout = timeSpan;
            HttpResponseMessage response = client.GetAsync("?connectionString=" + connectionString.Replace(" ", "%20")).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync();
                string json = data.Result;
                json = json.Replace("\\\"", "\"").Replace("\"{", "{").Replace("}\"", "}");
                //List<StatusDbDTO> statusDbDTOs = JsonConvert.DeserializeObject<List<StatusDbDTO>>(json);
                StatusDbDTO statusDbDTO = new StatusDbDTO();
                statusDbDTO.DeserializeJson(JObject.Parse(json));
                return statusDbDTO.Status;
            }
            else
                return $"Erro no Serviço {uri}";

            /*HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri + "?connectionString=" + connectionString.Replace(" ", "%20"));
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Proxy = WebRequest.GetSystemWebProxy();

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
            */
        }

        public static string GetConnectionString(Models.Conexao conexao)
        {
            if (conexao.BancoDados == "Informix")
            {
                string database = "wpdhosp";
                return $"Host={conexao.Ip}; " +
                       $"Service={conexao.Porta}; " +
                       $"Server={conexao.NomeServidor}; " +
                       $"Database={database}; " +
                       $"User Id={conexao.Usuario}; " +
                       $"Password={conexao.Senha}; ";
            }
            else
            {
                //"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=100.100.100.100)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=myservice.com)));User Id=scott;Password=tiger;"
                return "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)" +
                       $"(HOST={conexao.Ip})(PORT={conexao.Porta}))(CONNECT_DATA=(SERVICE_NAME={conexao.NomeServidor})));" +
                       $"User Id= {conexao.Usuario};Password= {conexao.Senha}; ";
            }
        }
    }
}
