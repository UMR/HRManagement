using AutoMapper;
using HRManagement.Application.Contracts.Persistence;
using HRManagement.Application.Dtos.User;
using HRManagement.Application.Exceptions;
using HRManagement.Application.Features.User.Validators;
using HRManagement.Application.Wrapper;
using MediatR;
using System.Linq;

namespace HRManagement.Application.Features.User.Commands
{
    public record CreateUserCommand : IRequest<BaseCommandResponse>
    {
        public CreateUserDto CreateUserDto { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, BaseCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateUserDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateUserDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();                
            }

            var userRequest = _mapper.Map<Domain.Entities.User>(request.CreateUserDto);
            var userId = await _userRepository.CreateUserAsync(userRequest);

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = userId;

            return response;
        }
        
    }

}
