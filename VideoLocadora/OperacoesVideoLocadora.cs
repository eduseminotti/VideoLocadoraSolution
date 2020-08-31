using System;
using VideoLocadora.Dominio;
using VideoLocadora.Dominio.Filmes;
using VideoLocadora.Dominio.Locatarios;
using VideoLocadora.Repositorio;

namespace VideoLocadora.Consoles
{
    internal class OperacoesVideoLocadora
    {
        private static readonly string _caminhoArquivo = @"C:\Users\Eduardo\OneDrive\Senac\C#\UC2\Atividade Video Locadora com API\VideoLocadoraSolution\VideoLocadora.Repositorio\Dados\video-locadora.json";
        private readonly FilmeRepositorio _filmeRepositorio;

        private readonly LocatarioRepositorio _locatarioRepositorio;

        private LocatarioDomainService _locatarioDomain;

        private readonly FilmeDomainService _filmeDomainService;

        public OperacoesVideoLocadora()
        {
            _filmeRepositorio = new FilmeRepositorio();

            _locatarioRepositorio = new LocatarioRepositorio();

            _filmeDomainService = new FilmeDomainService(_filmeRepositorio);

            _locatarioDomain = new LocatarioDomainService(_locatarioRepositorio);
        }

        #region Filme
        public void CadastrarFilme()
        {
            Console.WriteLine("Digite o título do filme:");
            var titulo = Console.ReadLine();

            Console.WriteLine("Digite a categoria do filme:");
            var categoria = Console.ReadLine();

            Console.WriteLine("Digite o ano de lançamento do filme:");
            var ano = Console.ReadLine();

            _filmeDomainService.CadastrarFilme(titulo, ano, categoria);

            Console.WriteLine("Filme cadastrado.");
            Console.ReadLine();
        }

        public void LocarFilme()
        {
            Console.WriteLine("Insira o título do filme:");
            var titulo = Console.ReadLine();
            var filme = _filmeRepositorio.RetornarPorTitulo(titulo);

            Console.WriteLine("Insira o nome do locatário:");
            var nomeLocatario = Console.ReadLine();
            var locatario = _locatarioRepositorio.EncontrarPorNome(nomeLocatario);

            if (filme != null && locatario != null)
            {
                Console.WriteLine("Dados do Filme:");
                Console.WriteLine(filme.ToString());
                Console.WriteLine();
                Console.WriteLine("Dados do Locatário:");
                Console.WriteLine(locatario.ToString());

                Console.WriteLine("Confirmar a locação do filme? [S] sim [N] não");
                var resposta = Console.ReadLine().ToUpper();

                if (resposta.Equals("S"))
                {
                    try
                    {
                        _filmeDomainService.LocarFilme(filme, locatario);
                        Console.WriteLine("O filme foi locado com sucesso.");
                    }
                    catch (FilmeLocadoException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public void ListarFilmes()
        {
            var list = _filmeRepositorio.RetornarTodos();
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
                Console.WriteLine("--------------------------");
            }

            Console.ReadKey();
        }

        public void DeletarFilme()
        {
            Console.Write("Informe o Titulo do filme que deseja deletar:");
            var filmeDelete = Console.ReadLine();

            var filmeDeletadoComSucesso = _filmeDomainService.DeletarFilme(filmeDelete);

            if (filmeDeletadoComSucesso == true)
            {
                Console.WriteLine();
                Console.WriteLine("Filme deletado com sucesso!");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Não foi possivel deletar!");
            }

            Console.ReadKey();
        }

        public void AtualizarFilme()
        {
            _filmeDomainService.AtualizarFilme(1, "Atualização");
        }

        #endregion

        #region Locatario
        public void CadastrarLocatario()
        {
            Console.Write("Digite o nome do locatario: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o endereço do locatario: ");
            string endereco = Console.ReadLine();

            Console.Write("Digite a data de nascimento: ");
            DateTime dataDeNascimento = Convert.ToDateTime(Console.ReadLine());

            var result = _locatarioDomain.CadastrarLocatario(nome, dataDeNascimento, endereco);

            if (result)
            {
                Console.WriteLine();
                Console.WriteLine("Locatario cadastrado com sucesso!");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Não foi possivel cadastrar o Locatario!");
            }
        }

        public void ListarLocatarios()
        {
            var list = _locatarioDomain.ListarLocatarios();
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
                Console.WriteLine("--------------------------");
            }

            Console.ReadKey();
        }

        public void RemoverLocatario()
        {
            Console.Write("Informe o Nome do Locatário que deseja deletar:");
            var nomeLocatario = Console.ReadLine();
           
            var locatarioDeleteSuccess = _locatarioDomain.DeletarLocatario(nomeLocatario);

            if (locatarioDeleteSuccess == true)
            {
                Console.WriteLine();
                Console.WriteLine("Locatário deletado com sucesso!");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Não foi possivel deletar!");
            }
        }
        #endregion
    }
}
