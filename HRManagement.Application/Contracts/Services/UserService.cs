using AutoMapper;
using HRManagement.Application.Contracts.Persistence;
using HRManagement.Application.Dtos.Users;
using HRManagement.Application.Exceptions;
using HRManagement.Application.Features.Users.Commands;
using HRManagement.Application.Features.Users.Validators;
using HRManagement.Application.Wrapper;
using HRManagement.Domain.Entities;

namespace HRManagement.Application.Contracts.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper,IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<UserForListDto>> GetUsersAsync()
        {
            var usersFromRepo = await _userRepository.GetUsersAsync();
            var usersToReturn = _mapper.Map<List<UserForListDto>>(usersFromRepo);
            return usersToReturn;
        }

        public async Task<UserForListDto> GetUserByIdAsync(int id)
        {
            var userFromRepo = await _userRepository.GetUserByIdAsync(id);
            var userToReturn = _mapper.Map<UserForListDto>(userFromRepo);
            return userToReturn;
        }

        public async Task<BaseCommandResponse> UpdateUser(UserForUpdaterDto userForUpdaterDto)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateUserDtoValidator();
            var validationResult = await validator.ValidateAsync(userForUpdaterDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Updating Failed";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var userRequest = await _userRepository.GetUserByIdAsync(userForUpdaterDto.Id);

            if (userRequest is null)
            {
                throw new NotFoundException(nameof(User), userForUpdaterDto.Id.ToString());
            }

            _mapper.Map(userForUpdaterDto, userRequest);
            await _userRepository.UpdateUserAsync(userRequest);

            response.Success = true;
            response.Message = "Updating Successful";
            response.Id = userRequest.Id;

            return response;
        }

        public async Task<BaseCommandResponse> DeleteUser(int userId)
        {
            var response = new BaseCommandResponse();
            var userRequest = await _userRepository.GetUserByIdAsync(userId);

            if (userRequest == null)
            {
                throw new NotFoundException(nameof(User), userId);
            }

            await _userRepository.DeleteUserAsync(userRequest);

            response.Success = true;
            response.Message = "Deleting Successful";
            response.Id = userRequest.Id;

            return response;
        }        
    }
}
