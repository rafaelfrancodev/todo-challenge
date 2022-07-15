using Application.AppServices.Todo.Inputs;
using Application.AppServices.Todo.Validators;
using Application.AppServices.Todo.ViewModel;
using AutoMapper;
using Domain.Interfaces.Services;
using Infra.CrossCutting.Notification.Interfaces;
using Infra.CrossCutting.UoW.Models;

namespace Application.UseCases.Todo
{
    public class TodoApplication : BaseValidationService, ITodoApplication
    {
        private readonly ISmartNotification _notification;
        private readonly ITodoDomainService _todoDomainService;
        private readonly IMapper _mapper;


        public TodoApplication(ITodoDomainService todoDomainService, ISmartNotification notification, IMapper mapper) : base(notification)
        {
            _todoDomainService = todoDomainService;
            _notification = notification;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (!id.IsValid(_notification)) return default;            
            return await _todoDomainService.DeleteAsync(id);
        }

        public async Task<IEnumerable<TodoViewModel>> GetAllAsync()
        {
            var result = await _todoDomainService.SelectFilterAsync(x => x.Id != null);
            return _mapper.Map<IEnumerable<TodoViewModel>>(result.OrderByDescending(x => x.CreatedAt));
        }

        public async Task<TodoViewModel> GetAsync(Guid id)
        {
            if (!id.IsValid(_notification)) return default;

            var result = await _todoDomainService.SelectByIdAsync(id);
            return _mapper.Map<TodoViewModel>(result);
        }

        public async Task<TodoViewModel> InsertAsync(TodoInput input)
        {
            if (!input.IsValid())
            {
                var viewModel = _mapper.Map<TodoViewModel>(input);
                NotifyErrorsAndValidation(_notification.EmptyPositions(), viewModel);
                return default;
            }

            var entity = _mapper.Map<Domain.Entities.Todo>(input);           
            var result = await _todoDomainService.InsertAsync(entity);          
            return  _mapper.Map<TodoViewModel>(result);          
        }

        public async Task<TodoViewModel> UpdateAsync(Guid id, TodoInput input)
        {
            if (!id.IsValid(_notification)) return default;

            if (!input.IsValid())
            {
                var viewModel = _mapper.Map<TodoViewModel>(input);
                NotifyErrorsAndValidation(_notification.EmptyPositions(), viewModel);
                return default;
            }

            var entity = _mapper.Map<Domain.Entities.Todo>(input);
            entity.Id = id;
            var result = await _todoDomainService.UpdateAsync(entity);
            return _mapper.Map<TodoViewModel>(result);
        }
    }
}
