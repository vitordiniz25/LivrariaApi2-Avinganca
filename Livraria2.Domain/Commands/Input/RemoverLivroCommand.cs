using Flunt.Notifications;
using Livraria2.Infra.Interfaces.Commands;
using System.Text.Json.Serialization;

namespace Livraria2.Domain.Commands.Input
{
    public class RemoverLivroCommand : Notifiable, ICommandPadrao
    {
        [JsonIgnore]
        public long Id { get; set; }

        public bool ValidarCommand()
        {
            if (string.IsNullOrEmpty(Id.ToString()))
                AddNotification("Id", "ID é um campo obrigatório");
            if (Id < 0)
                AddNotification("Id", "ID inválido (ID deve ser maior que zero)");

            return Valid;
        }
    }
}
