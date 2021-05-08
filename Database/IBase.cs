﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public interface IBase
    {
        int Key { get; }

        void Salvar();

        List<IBase> Todos();

        List<IBase> Busca();

        void Excluir();

        void CreateTable();

    }
}
