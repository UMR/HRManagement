using AutoMapper;
using HRManagement.Application.Contracts.Persistence;
using HRManagement.Application.Dtos.Users;
using HRManagement.Application.Features.Users.Validators;
using HRManagement.Application.Wrapper;
using HRManagement.Domain.Entities;
using MediatR;

namespace HRManagement.Application.Features.Users.Commands
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
                return response;
            }

            var userRequest = _mapper.Map<User>(request.CreateUserDto);
            var userId = await _userRepository.CreateUserAsync(userRequest);

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = userId;

            return response;
        }
        
    }

}
