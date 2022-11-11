using AutoMapper;
using HRManagement.Application.Contracts.Persistence;
using HRManagement.Application.Dtos.User;
using MediatR;

namespace HRManagement.Application.Features.User.Queries
{
    public record GetUsersQuery : IRequest<List<UserDto>>;
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var usersFromRepo = await _userRepository.GetUsersAsync();
            return _mapper.Map<List<UserDto>>(usersFromRepo);
        }
    }
}
