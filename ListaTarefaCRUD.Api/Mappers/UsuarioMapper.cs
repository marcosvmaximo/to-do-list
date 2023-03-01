using System;
using AutoMapper;
using ListaTarefaCRUD.Api.Models;

namespace ListaTarefaCRUD.Api.Mappers;

public class UsuarioMapper : Profile
{
    public UsuarioMapper()
    {
        CreateMap<UsuarioResponse, Usuario>();
        CreateMap<Usuario, UsuarioResponse>();
        CreateMap<UsuarioRequest, Usuario>();
    }
}

