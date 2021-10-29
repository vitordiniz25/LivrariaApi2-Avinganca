using Livraria2.Domain.Commands.Input;
using Livraria2.Domain.Commands.Output;
using Livraria2.Domain.Entidades;
using Livraria2.Domain.Interfaces.Repositories;
using Livraria2.Infra.Interfaces.Commands;

namespace Livraria2.Domain.Handlers
{
    public class LivroHandler : ICommandHandler<AdicionarLivroCommand>, ICommandHandler<AtualizarLivroCommand>, ICommandHandler<RemoverLivroCommand>
    {
        private readonly ILivroRepository _repository;

        public LivroHandler(ILivroRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(AdicionarLivroCommand command)
        {
            if (!command.ValidarCommand())
                return new LivroCommandResult(false, "Por favor corrija as inconsistências abaixo", command.Notifications);

            long id = 0;
            Livro livro = new Livro(id, command.Nome, command.Autor, command.Edicao, command.Isbn, command.Imagem);

            id = _repository.Inserir(livro);

            var retorno = new LivroCommandResult(true, "Livro adicionado com sucesso!", livro);

            return retorno;
        }

        public ICommandResult Handle(AtualizarLivroCommand command)
        {
            if (!command.ValidarCommand())
                return new LivroCommandResult(false, "Por favor corrija as inconsistências abaixo", command.Notifications);

            if (!_repository.CheckId(command.Id))
                return new LivroCommandResult(false, "Este livro não existe.", new { });

            long id = command.Id;

            Livro livro = new Livro(id, command.Nome, command.Autor, command.Edicao, command.Isbn, command.Imagem);

            _repository.Atualizar(livro);

            return new LivroCommandResult(true, "Livro atualizado com sucesso!", livro);
        }

        public ICommandResult Handle(RemoverLivroCommand command)
        {
            if (!command.ValidarCommand())
                return new LivroCommandResult(false, "Por favor corrija as inconsistências abaixo!", command.Notifications);
            if (!_repository.CheckId(command.Id))
                return new LivroCommandResult(false, "Este livro não existe.", new { });

            long id = command.Id;
            _repository.Excluir(id);
            return new LivroCommandResult(true, "Livro apagado com sucesso!", new { });
        }
    }
}
