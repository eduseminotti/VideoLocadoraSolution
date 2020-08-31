using System;
using System.Net;
using VideoLocadora.Dominio;
using VideoLocadora.Repositorio;

namespace VideoLocadora.Consoles
{
    class Program
    {
        static void Main(string[] args)
        {
            int opcaoMenu;
            var operacoes = new OperacoesVideoLocadora();

            //operacoes.AtualizarFilme();

            do
            {
                Console.Clear();
                Console.WriteLine($"----- Seja bem vindo à Video Locadora -----");
                Console.WriteLine();
                Console.WriteLine("[1] Cadastrar filme");
                Console.WriteLine("[2] Locar filme");
                Console.WriteLine("[3] Listas filmes");
                Console.WriteLine("[4] Deletar filme");
                Console.WriteLine("[5] Cadastrar locatário");
                Console.WriteLine("[6] Listar locatário");
                Console.WriteLine("[7] Remover locatário");
                Console.WriteLine("[0] Sair");
                Console.WriteLine();
                opcaoMenu = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                switch (opcaoMenu)
                {
                    case 1:
                        operacoes.CadastrarFilme();
                        break;
                    case 2:
                        operacoes.LocarFilme();
                        break;
                    case 3:
                        operacoes.ListarFilmes();
                        break;
                    case 4:
                        operacoes.DeletarFilme();
                        break;
                    case 5:
                        operacoes.CadastrarLocatario();
                        break;
                    case 6:
                        operacoes.ListarLocatarios();
                        break;
                    case 7:
                        operacoes.RemoverLocatario();
                        break;
                    default:
                        break;
                }
                
                Console.ReadKey();

            } while (opcaoMenu != 0);

            Console.WriteLine("Obrigada pela preferência. Volte sempre ;)");
            Console.ReadKey();
        }
    }
}
