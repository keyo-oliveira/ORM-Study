using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace Business
{
    public class Fornecedor : Base
    {
        [OpcoesBase(ChavePrimaria =true, UsaBD =true, UsaBusca =true)]
        public int? Id { get; set; }

        [OpcoesBase(UsaBD = true, UsaBusca = true)]
        public string Razao { get; set; }

        [OpcoesBase(UsaBD = true, UsaBusca = true)]
        public string Cnpj { get; set; }

        [OpcoesBase(UsaBD = true)]
        public string Email { get; set; }

        public new List<Fornecedor> Todos()
        {
            List<Fornecedor> fornecedors = new List<Fornecedor>();
            foreach(var ibase in base.Todos())
            {
                fornecedors.Add((Fornecedor)ibase);
            }
            return fornecedors;
        }
    }
}
