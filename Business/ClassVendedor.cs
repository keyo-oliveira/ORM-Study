using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace Business
{
    public class ClassVendedor : Base
    {
        [OpcoesBase(ChavePrimaria = true, UsaBD = true, UsaBusca = true)]
        public int? Id { get; set; }

        [OpcoesBase(UsaBD = true, UsaBusca = true)]
        public string Nome { get; set; }

        [OpcoesBase(UsaBD = true)]
        public string CPF { get; set; }

        [OpcoesBase(UsaBD = true)]
        public string Email { get; set; }

        [OpcoesBase(UsaBD = true)]
        public string Area{ get; set; }

        public new List<ClassVendedor> Todos()
        {
            List<ClassVendedor> vendedor = new List<ClassVendedor>();
            foreach (var ibase in base.Todos())
            {
                vendedor.Add((ClassVendedor)ibase);
            }
            return vendedor;
        }
    }
}
