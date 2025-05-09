using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            // TODO: Buscar o Id no banco utilizando o EF
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada
            
            var tarefa = _context.Tarefas.Find(id); //Resolvido por Roberto Emilio - 09/05/2025

            if (tarefa == null)//Resolvido por Roberto Emilio - 09/05/2025
              return NotFound();//Resolvido por Roberto Emilio - 09/05/2025

            return Ok(tarefa);//Resolvido por Roberto Emilio - 09/05/2025
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF

            var tarefa = _context.Tarefas;//Resolvido por Roberto Emilio - 09/05/2025
            return Ok(tarefa);//Resolvido por Roberto Emilio - 09/05/2025
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData

            var tarefa = _context.Tarefas.Where (x => x.Titulo.Contains (titulo));//Resolvido por Roberto Emilio - 09/05/2025
            return Ok (tarefa);//Resolvido por Roberto Emilio - 09/05/2025
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(tarefa);//Resolvido por Roberto Emilio - 09/05/2025
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var tarefa = _context.Tarefas.Where(x => x.Status == status);
            return Ok(tarefa); //Resolvido por Roberto Emilio - 09/05/2025
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
            _context.Add(tarefa); //Resolvido por Roberto Emilio - 09/05/2025
            _context.SaveChanges();//Resolvido por Roberto Emilio - 09/05/2025
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            tarefaBanco.Descricao = tarefa.Descricao;//Resolvido por Roberto Emilio - 09/05/2025
            tarefaBanco.Titulo = tarefa.Titulo;//Resolvido por Roberto Emilio - 09/05/2025
            tarefaBanco.Data = tarefa.Data;//Resolvido por Roberto Emilio - 09/05/2025
            tarefaBanco.Status = tarefa.Status;//Resolvido por Roberto Emilio - 09/05/2025

            _context.Tarefas.Update(tarefaBanco);//Resolvido por Roberto Emilio - 09/05/2025
            _context.SaveChanges();//Resolvido por Roberto Emilio - 09/05/2025

            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            _context.Tarefas.Remove(tarefaBanco);//Resolvido por Roberto Emilio - 09/05/2025
            _context.SaveChanges();//Resolvido por Roberto Emilio - 09/05/2025
            return NoContent();
        }
    }
}
