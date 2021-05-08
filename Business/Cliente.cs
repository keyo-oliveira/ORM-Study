using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace Business
{
    public class Cliente : Base
    {
        [OpcoesBase(ChavePrimaria =true, UsaBD =true, UsaBusca =true)]
        public int? Id { get; set; }

        [OpcoesBase(UsaBD = true, UsaBusca =true)]
        public string Nome { get; set; }

        [OpcoesBase(UsaBD = true)]
        public string Telefone { get; set; }

        [OpcoesBase(UsaBD = true)]
        public string Email { get; set; }

        [OpcoesBase(UsaBD = true)]
        public string Endereco { get; set; }

        public new List<Cliente> Todos()
        {
            List<Cliente> cli = new List<Cliente>();
            foreach (var ibase in base.Todos())
            {
                cli.Add((Cliente)ibase);
            }
            return cli;
        }
    }
}
