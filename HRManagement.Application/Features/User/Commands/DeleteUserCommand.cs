using AutoMapper;
using HRManagement.Application.Contracts.Persistence;
using HRManagement.Application.Exceptions;
using HRManagement.Application.Wrapper;
using MediatR;

namespace HRManagement.Application.Features.User.Commands
{
    public class DeleteUserCommand : IRequest<BaseCommandResponse>
    {
        public int UserId { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, BaseCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public DeleteUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var userRequest = await _userRepository.GetUserByIdAsync(request.UserId);

            if (userRequest == null)
            {
                throw new NotFoundException(nameof(userRequest), request.UserId);
            }

            await _userRepository.DeleteUserAsync(userRequest);

            response.Success = true;
            response.Message = "Deleting Successful";
            response.Id = userRequest.UserId;

            return response;
        }
    }
}
