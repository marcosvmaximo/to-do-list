using System;
using System.ComponentModel.DataAnnotations;

namespace ListaTarefaCRUD.Api.Models.Common;

public abstract class Entity
{
    [Key]
    public int Id { get; set; }
}

