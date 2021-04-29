using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Receive.Domain
{
    [Table("Mensagem")]
    public class Mensagem
    {
        public Mensagem(string corpoMensagem)
        {
            Id = Guid.NewGuid();
            CorpoMensagem = corpoMensagem;
        }

        public Mensagem()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
        public string CorpoMensagem { get; }
    }
}
