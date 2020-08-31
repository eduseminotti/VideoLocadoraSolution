using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VideoLocadora.Dominio.Base;

namespace VideoLocadora.Repositorio
{
    public abstract class Repositorio<T> where T : Entidade
    {
        private readonly string _nomeDaColecao;
        private readonly string _caminhoDados;

        public Repositorio(string caminhoDados, string nomeDaColecao)
        {
            _nomeDaColecao = nomeDaColecao;
            _caminhoDados = caminhoDados;
        }

        protected List<T> Retornar()
        {
            // Abre o arquivo e lê o texto do arquivo json
            var json = File.ReadAllText(_caminhoDados);
            // Transforma o conteúdo do arquivo em objeto json
            var jsonObj = JObject.Parse(json);
            // Pega a propriedade locatarios e transforma em array json
            var arrayJsonLocatarios = jsonObj.GetValue(_nomeDaColecao) as JArray;

            // Transforma os itens do array json em uma lista do tipo locatário
            return arrayJsonLocatarios.ToObject<List<T>>();
        }

        protected int RetornarUltimoId()
        {
            var items = Retornar();
            return items.Max(y => y.Id);
        }

        protected T RetornarPorId(int id)
        {
            // Abre o arquivo e lê o texto do arquivo json
            var json = File.ReadAllText(_caminhoDados);
            // Transforma o conteúdo do arquivo em objeto json
            var jsonObj = JObject.Parse(json);
            // Pega a propriedade locatarios e transforma em array json
            var arrayJsonLocatarios = jsonObj.GetValue(_nomeDaColecao) as JArray;

            // Transforma os itens do array json em uma lista do tipo locatário
            var itemsEncontrados = arrayJsonLocatarios.ToObject<List<T>>();

            return itemsEncontrados.FirstOrDefault(y => y.Id == id);
        }

        protected bool Salvar(T item)
        {
            // Transformando o objeto locatário em um JObject (objeto JSON)
            var filmeEmJObject = JObject.FromObject(item);

            try
            {
                // Abre e lê o arquivo json
                var json = File.ReadAllText(_caminhoDados);
                // Tranforma o conteúdo do arquivo num objeto Json
                var jsonObj = JObject.Parse(json);
                // Pega a propriedade locatários do objeto e transforma em um array json
                var arrayLocatarios = jsonObj.GetValue(_nomeDaColecao) as JArray;
                // Adiciona o novo locatário em objeto json no array
                arrayLocatarios.Add(filmeEmJObject);
                // Atribuo o novo array com a novo locatário adicionado à propriedade locatários
                jsonObj[_nomeDaColecao] = arrayLocatarios;
                // Tranforma o objeto json (que representa todo o conteúdo do arquivo) em string
                string novoJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                // Sobrescreve o arquivo video-locadora.json com os novos dados
                File.WriteAllText(_caminhoDados, novoJsonResult);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected bool Deletar(T item)
        {
            try
            {
                // Retorno todos os locatários cadastrados no arquivo
                var locatarios = Retornar();
                // Removo da lista de locatários o locatário desejado
                locatarios.Remove(item);
                // Lê conteúdo do arquivo
                var json = File.ReadAllText(_caminhoDados);
                // Converte conteúdo do arquivo em tipo json
                var jsonObj = JObject.Parse(json);
                // Sobrescrevo os locatários do arquivo com a nova lista com o locatário removido
                jsonObj[_nomeDaColecao] = JArray.FromObject(locatarios);
                // Converto o objeto json para string
                string novoJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                // Escrevo a string no arquivo .json
                File.WriteAllText(_caminhoDados, novoJsonResult);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
