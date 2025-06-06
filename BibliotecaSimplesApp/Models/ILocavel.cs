﻿using System;
using BibliotecaSimplesApp.Models;

namespace BibliotecaSimplesApp.Models
{
    public interface ILocavel
    {
        bool EstaEmprestado { get; }
        Membro EmprestadoPara { get; }
        bool Emprestar(Membro membro);
        bool Devolver();
        DateTime? DataEmprestimo { get; }
        DateTime? DataDevolucaoPrevista { get; }
    }
}
