using AutoMapper;
using HRManagement.Application.Contracts.Persistence;
using HRManagement.Application.Dtos.Users;
using MediatR;

namespace HRManagement.Application.Features.Users.Queries
{
    public record GetUsersQuery : IRequest<List<UserForListDto>>;
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserForListDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserForListDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var usersFromRepo = await _userRepository.GetUsersAsync();
            return _mapper.Map<List<UserForListDto>>(usersFromRepo);
        }
    }
}
