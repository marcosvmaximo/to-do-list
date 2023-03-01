using System;
using AutoMapper;
using ListaTarefaCRUD.Api.Models;

namespace ListaTarefaCRUD.Api.Mappers;

public class TarefaMapper : Profile
{
    public TarefaMapper()
    {
        CreateMap<TarefaResponse, Tarefa>();
        CreateMap<Tarefa, TarefaResponse>()
            .ForMember(x => x.UsuarioEmail, x => x.MapFrom(x => x.Usuario.Email))
            .ForMember(x => x.UsuarioNome, x => x.MapFrom(x => x.Usuario.Nome));

        CreateMap<TarefaRequest, Tarefa>();
    }
}

