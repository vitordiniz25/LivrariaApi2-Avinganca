using Dapper;
using Livraria2.Domain.Entidades;
using Livraria2.Domain.Interfaces.Repositories;
using Livraria2.Domain.QueryResults;
using Livraria2.Infra.Data.DataContexts;
using Livraria2.Infra.Data.Repositories.Queries;
using System.Collections.Generic;
using System.Linq;

namespace Livraria2.Infra.Data.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly DynamicParameters _parameters = new();
        private readonly DataContext _dataContext;

        public LivroRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public long Inserir(Livro livro)
        {
            _parameters.Add("Nome", livro.Nome, System.Data.DbType.String);
            _parameters.Add("Autor", livro.Autor, System.Data.DbType.String);
            _parameters.Add("Edicao", livro.Edicao, System.Data.DbType.String);
            _parameters.Add("Isbn", livro.Isbn, System.Data.DbType.String);
            _parameters.Add("Imagem", livro.Imagem, System.Data.DbType.String);

            return _dataContext.SqlConnection.ExecuteScalar<long>(LivroQueries.Inserir, _parameters);
        }

        public void Atualizar(Livro livro)
        {
            _parameters.Add("Id", livro.Id, System.Data.DbType.Int64);
            _parameters.Add("Nome", livro.Nome, System.Data.DbType.String);
            _parameters.Add("Autor", livro.Autor, System.Data.DbType.String);
            _parameters.Add("Edicao", livro.Edicao, System.Data.DbType.String);
            _parameters.Add("Isbn", livro.Isbn, System.Data.DbType.String);
            _parameters.Add("Imagem", livro.Imagem, System.Data.DbType.String);

            _dataContext.SqlConnection.Execute(LivroQueries.Atualizar, _parameters);
        }

        public void Excluir(long id)
        {
            _parameters.Add("Id", id, System.Data.DbType.Int64);

            _dataContext.SqlConnection.Execute(LivroQueries.Excluir, _parameters);
        }

        public List<LivroQueryResult> Listar()
        {
            var livros = _dataContext.SqlConnection.Query<LivroQueryResult>(LivroQueries.Listar).ToList();

            return livros;
        }

        public LivroQueryResult Obter(long id)
        {
            _parameters.Add("Id", id, System.Data.DbType.Int64);

            var livro = _dataContext.SqlConnection.Query<LivroQueryResult>(LivroQueries.Obter, _parameters).FirstOrDefault();

            return livro;
        }

        public bool CheckId(long id)
        {
            _parameters.Add("Id", id, System.Data.DbType.Int64);

            return _dataContext.SqlConnection.Query<bool>(LivroQueries.CheckId, _parameters).FirstOrDefault();
        }
    }
}
