using AutoMapper;
using HRManagement.Application.Contracts.Persistence;
using HRManagement.Application.Dtos.Users;
using MediatR;

namespace HRManagement.Application.Features.Users.Queries
{
    public record GetUserByIdQuery : IRequest<UserDto>
    {
        public int Id { get; set; }
    }
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var userFromRepo = await _userRepository.GetUserByIdAsync(request.Id);
            return _mapper.Map<UserDto>(userFromRepo);
        }
    }
}
