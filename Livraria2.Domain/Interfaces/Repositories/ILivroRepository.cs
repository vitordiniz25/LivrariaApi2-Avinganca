using Livraria2.Domain.Entidades;
using Livraria2.Domain.QueryResults;
using System.Collections.Generic;

namespace Livraria2.Domain.Interfaces.Repositories
{
    public interface ILivroRepository
    {
        long Inserir(Livro livro);
        void Atualizar(Livro livro);
        void Excluir(long id);
        List<LivroQueryResult> Listar();
        LivroQueryResult Obter(long id);
        bool CheckId(long id);
    }
}
