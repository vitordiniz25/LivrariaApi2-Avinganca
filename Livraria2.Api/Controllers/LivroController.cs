using Livraria2.Domain.Commands.Input;
using Livraria2.Domain.Handlers;
using Livraria2.Domain.Interfaces.Repositories;
using Livraria2.Domain.QueryResults;
using Livraria2.Infra.Interfaces.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Livraria2.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/v1")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _repository;
        private readonly LivroHandler _handler;

        public LivroController(ILivroRepository repository, LivroHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpPost]
        [Route("livros")]
        public ICommandResult InserirLivro([FromBody] AdicionarLivroCommand command)
        {
            return _handler.Handle(command);
        }

        [HttpPut]
        [Route("livros/{id}")]
        public ICommandResult AtualizarLivro(long id, [FromBody] AtualizarLivroCommand command)
        {
            command.Id = id;
            return _handler.Handle(command);
        }

        [HttpDelete]
        [Route("livros/{id}")]
        public ICommandResult ApagarLivro(long id)
        {
            return _handler.Handle(new RemoverLivroCommand() { Id = id });
        }

        [HttpGet]
        [Route("livro/{id}")]
        public LivroQueryResult ObterLivro(long id)
        {
            return _repository.Obter(id);
        }

        [HttpGet]
        [Route("livros")]
        public List<LivroQueryResult> ListarLivros(long id)
        {
            return _repository.Listar();
        }
    }
}
