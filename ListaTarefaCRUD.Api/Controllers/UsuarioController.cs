using System;
using AutoMapper;
using ListaTarefaCRUD.Api.Interfaces.Repositories;
using ListaTarefaCRUD.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListaTarefaCRUD.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _repository;
    private readonly IMapper _mapper;

    public UsuarioController(IUsuarioRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioResponse>> ObterUsuario([FromRoute]int id)
    {
        var request = await _repository.GetById(id);
        if (request is null)
            return NotFound(id);

        var response = _mapper.Map<UsuarioResponse>(request);
        return response;
    }

    [HttpGet]
    public IEnumerable<UsuarioResponse> ObterTodosUsuarios()
    {
         var request = _repository.GetAll();

        if (request.Result.Count == 0)
            yield break;

        foreach (var user in request.Result)
        {
            var response = _mapper.Map<UsuarioResponse>(user);
            yield return response;
        }
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioResponse>> CadastrarUsuario([FromBody]UsuarioRequest usuario)
    {
        if (!ModelState.IsValid)
            return BadRequest(usuario);

        Usuario entity = new Usuario(usuario.Nome, usuario.Email);
        
        var request = await _repository.AddAsync(entity);
        var response = _mapper.Map<UsuarioResponse>(request);

        return CreatedAtAction(nameof(CadastrarUsuario), response);
    }

    //[HttpPut]
    //public async Task<ActionResult<UsuarioResponse>> AtualizarDadosUsuario([FromBody]UsuarioRequest usuario)
    //{

    //}
}

