using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ListaTarefaCRUD.Api.Models.Common;

namespace ListaTarefaCRUD.Api.Models;

public class Tarefa : Entity
{
    public Tarefa(string titulo, string detalhes, int usuarioId)
    {
        Titulo = titulo;
        Detalhes = detalhes;
        UsuarioId = usuarioId;
        Check = false;
    }

    public string Titulo { get; private set; }
    public string Detalhes { get; private set; }
    public bool Check { get; private set; }
    public int UsuarioId { get; private set; }
    [JsonIgnore]
    public virtual Usuario Usuario { get; private set; }

    public Tarefa AtualizarTarefa(TarefaRequest tarefa, int id)
    {
        base.Id = id;
        Titulo = tarefa.Titulo;
        Detalhes = tarefa.Detalhes;
        return this;
    }

    public void AlterarCheck()
    {
        if (Check)
            Check = false;
        else
            Check = true;
    }
}

public class TarefaRequest
{
    [Required(ErrorMessage = "Campo obrigatório")]
    [StringLength(maximumLength: 35, MinimumLength = 2, ErrorMessage = "O Titulo deve possuir entre 2 e 35 caracteres")]
    public string Titulo { get; set; }
    [Editable(true)]
    [StringLength(maximumLength: 750, MinimumLength = 2, ErrorMessage = "O Datelhe deve possuir entre 2 e 750 caracteres")]
    public string Detalhes { get; set; }
    [Required(ErrorMessage = "É necessário um usuario para cadastrar uma Tarefa")]
    public int UsuarioId { get; set; }
}

public class TarefaResponse
{
    public string UsuarioNome {get; set;}
    public string UsuarioEmail { get; set;}
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Detalhes { get; set; }
    public bool Check { get; set; }
}
