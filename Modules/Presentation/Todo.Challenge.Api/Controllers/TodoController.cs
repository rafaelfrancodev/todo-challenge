using Application.AppServices.Todo.Inputs;
using Application.AppServices.Todo.ViewModel;
using Application.UseCases.Todo;
using Infra.CrossCutting.Controllers;
using Infra.CrossCutting.Notification.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Todo.Challenge.Api.Controllers
{
    [Route("api/v1/todo")]
    [ApiController]
    public class TodoController : BaseController
    {

        private readonly ITodoApplication _application;
        private ILogger<TodoController> _logger;

        public TodoController(INotificationHandler<DomainNotification> notification, ITodoApplication application, ILogger<TodoController> logger) : base(notification)
        {
            _application = application;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Result<IEnumerable<TodoViewModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<IEnumerable<TodoViewModel>>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var result = await _application.GetAllAsync();
                return OkOrDefault(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to get all to do's.");
                return InternalServerError(new Exception("Fail to get all to do's."));
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Result<TodoViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<TodoViewModel>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            try
            {
                var result = await _application.GetAsync(id);
                return OkOrDefault(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to get to do by id.");
                return InternalServerError(new Exception("Fail to get to do by id."));
            }
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(Result<TodoViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<TodoViewModel>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id, [FromBody]  TodoInput input)
        {
            try
            {
                var result = await _application.UpdateAsync(id, input);
                return OkOrDefault(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to update to do.");
                return InternalServerError(new Exception("Fail to update to do."));
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Result<TodoViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<TodoViewModel>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody] TodoInput input)
        {
            try
            {
                var result = await _application.InsertAsync(input);
                return OkOrDefault(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to insert to do.");
                return InternalServerError(new Exception("Fail to insert to do."));
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(Result<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<bool>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            try
            {
                var result = await _application.DeleteAsync(id);
                return OkOrDefault(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to delete to do by id.");
                return InternalServerError(new Exception("Fail to delete to do by id."));
            }
        }
    }
}
