using MongoDB.Driver;
using DevTeamUtils.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Repository
{
    public class ConexaoRepository : IConexaoRepository
    {
        private readonly MongoDbContext _context;

        public ConexaoRepository(MongoDbContext context)
        {
            _context = context;
            var resultado = _context.Conexoes.Find(FilterDefinition<Conexao>.Empty).Skip(1);
            //if (!resultado.Any())
            //    Add(new Conexao());
        }

        public void Add(Conexao conexao)
        {
            _context.Conexoes.InsertOne(conexao);

            //_context.Conexoes.Add(conexao);
            //_context.SaveChanges();
        }

        public Conexao Find(Guid id)
        {
            var resultado = _context.Conexoes.Find(Builders<Conexao>.Filter.Eq("_id", id)).FirstOrDefault();
            return resultado;
        }

        public IEnumerable<Conexao> GetAll()
        {
            var resultado = _context.Conexoes.Find(FilterDefinition<Conexao>.Empty).SortBy(it => it.NomeServidor);//.Skip(0).Limit(50)
            return resultado.ToList();
        }

        public void Remove(Guid id)
        {
            _context.Conexoes.DeleteOne(Builders<Conexao>.Filter.Eq(p => p.Id, id));

            //var conexao = _context.Conexoes.First(t => t.Id == id);
            //_context.Conexoes.Remove(conexao);
            // _context.SaveChanges();
        }

        public void Update(Conexao conexao)
        {
            _context.Conexoes.ReplaceOne(Builders<Conexao>.Filter.Eq(p => p.Id, conexao.Id), conexao);
            //_context.Conexoes.Update(conexao);
            //_context.SaveChanges();
        }

        private Conexao CsvToConexao(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Conexao conexao = new Conexao();
            conexao.Sistema = values[0];
            conexao.BancoDados = values[1];
            conexao.NomeServidor = values[2];
            conexao.Ip = values[3];
            conexao.Porta = Convert.ToInt32(values[4]);
            conexao.Usuario = values[5];
            conexao.Senha = values[6];
            conexao.Status = values[7];
            //conexao.DataStatus = values[8];

            return conexao;
        }
        
        public void Import(string pathAndFile)
        {
            List<Conexao> conexoes = System.IO.File.ReadAllLines(pathAndFile)
                                           .Skip(1)
                                           .Select(v => CsvToConexao(v))
                                           .ToList();
            foreach (Conexao item in conexoes)
            {
                Add(item);
            }
        }

        public System.IO.Stream DownloadIniFile(string userName, string password)
        {
            string iniText = Utils.IniTextHelper.CreateIniText(userName, password);
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(iniText);
            return new System.IO.MemoryStream(byteArray);
        }
    }
}
