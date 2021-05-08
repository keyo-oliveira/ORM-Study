using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace Business
{
    public class ClassProduto : Base
    {
        [OpcoesBase(ChavePrimaria =true , UsaBD = true, UsaBusca = true)]
        public int? Id { get; set; }
        [OpcoesBase(UsaBD = true, UsaBusca = true)]
        public string Nome { get; set; }
        [OpcoesBase(UsaBD = true)]
        public string Valor { get; set; }
        [OpcoesBase(UsaBD = true)]
        public string Quantidade { get; set; }

        public new List<ClassProduto> Todos()
        {
            List<ClassProduto> produtos = new List<ClassProduto>();
            foreach (var ibase in base.Todos())
            {
                produtos.Add((ClassProduto)ibase);
            }
            return produtos;
        }
    }
}
