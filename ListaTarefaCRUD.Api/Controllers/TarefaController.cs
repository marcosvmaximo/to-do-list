using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using ListaTarefaCRUD.Api.Interfaces.Repositories;
using ListaTarefaCRUD.Api.Models;
using ListaTarefaCRUD.Api.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace ListaTarefaCRUD.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TarefaController : ControllerBase
{
    private readonly ITarefaRepository _repository;
    private readonly IMapper _mapper;

    public TarefaController(ITarefaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(TarefaResponse))]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IReadOnlyCollection<TarefaResponse>>> ObterTodasTarefas()
    {
        var request = await _repository.GetAll();

        if (request.Count == 0)
            return NotFound();

        var response = _mapper.Map<IReadOnlyCollection<TarefaResponse>>(request);

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(TarefaResponse))]
    [ProducesResponseType(404)]
    public async Task<ActionResult<TarefaResponse>> ObterTarefaPorId([FromRoute]int id)
    {
        if (id == 0)
            return BadRequest();

        var request = await _repository.GetById(id);

        if (request is null)
            return NotFound();

        TarefaResponse response = _mapper.Map<TarefaResponse>(request);

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(TarefaResponse))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> InserirTarefa([FromBody]TarefaRequest tarefa)
    {
        if (!ModelState.IsValid)
            return BadRequest(tarefa);

        var entity = new Tarefa(tarefa.Titulo, tarefa.Detalhes, tarefa.UsuarioId);

        var request = await _repository.AddAsync(entity);
        var response = _mapper.Map<TarefaResponse>(request);

        return CreatedAtAction(nameof(InserirTarefa), response);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DeletarTarefa([FromRoute]int id)
    {
        if (id == 0)
            return BadRequest();

        var tarefaDelete = _repository.GetById(id);

        if (tarefaDelete == null)
            return NotFound(new { message = "Tarefa não encontrada." });

        await _repository.DeleteAsync(tarefaDelete.Result);

        return Ok();
    }

    [HttpPatch("{id:int}")]
    [ProducesResponseType(202)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<TarefaResponse>> AlterarStatusTarefa([FromRoute]int id)
    {
        if (id == 0)
            return BadRequest();

        var tarefa = _repository.GetById(id).Result;

        if (tarefa is null)
            return NotFound();

        tarefa.AlterarCheck();
        await _repository.ChangeAsync(tarefa);

        var response = _mapper.Map<TarefaResponse>(tarefa);
        return Ok(response);
    }

    [HttpGet("concluidas")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<TarefaResponse>))]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IEnumerable<TarefaResponse>>> ObterTarefasConcluidas()
    {
        var tarefas = _repository.GetByCheckStatus(true);

        if (tarefas.Result.Count() == 0)
            return NotFound();

        var response = new List<TarefaResponse>();
        foreach (var tarefa in tarefas.Result)
        {
            response.Add(_mapper.Map<TarefaResponse>(tarefa));
        }

        return response;
    }

    [HttpGet("pendentes")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<TarefaResponse>))]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IEnumerable<TarefaResponse>>> ObterTarefasPendentes()
    {
        var tarefas = _repository.GetByCheckStatus(false);

        if (tarefas.Result.Count() == 0)
            return NotFound();

        var response = new List<TarefaResponse>();

        foreach (var tarefa in tarefas.Result)
        {
            response.Add(_mapper.Map<TarefaResponse>(tarefa));
        }

        return response;
    }
}

