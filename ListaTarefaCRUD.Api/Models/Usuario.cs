using System;
using System.ComponentModel.DataAnnotations;
using ListaTarefaCRUD.Api.Models.Common;

namespace ListaTarefaCRUD.Api.Models;

public class Usuario : Entity
{
    private List<Tarefa> _tarefas;

    public Usuario(string nome, string email)
    {
        _tarefas = new List<Tarefa>();
        Nome = nome;
        Email = email;
    }

    public string Nome { get; private set; }
    public string Email { get; private set; }
    public virtual IReadOnlyCollection<Tarefa> Tarefas => _tarefas;
}

public class UsuarioRequest
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "O nome deve conter entre 2 e 20 caracteres.")]
    public string Nome { get; set; }
    [DataType(DataType.EmailAddress, ErrorMessage = "O campo deve ser do tipo Email")]
    public string Email { get; set; }
}

public class UsuarioResponse
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public IReadOnlyCollection<Tarefa> Tarefas { get; set; }
}

