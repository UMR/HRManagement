using AutoMapper;
using HRManagement.Application.Contracts.Persistence;
using HRManagement.Application.Dtos.Roles;
using HRManagement.Application.Exceptions;
using HRManagement.Application.Validators.Role;
using HRManagement.Application.Validators.Users;
using HRManagement.Application.Wrapper;
using HRManagement.Domain.Entities;

namespace HRManagement.Application.Contracts.Services
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public RoleService(IMapper mapper, IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<List<RoleForListDto>> GetRolesAsync()
        {
            var rolesFromRepo = await _roleRepository.GetRolesAsync();
            var rolesToReturn = _mapper.Map<List<RoleForListDto>>(rolesFromRepo);
            return rolesToReturn;
        }

        public async Task<RoleForListDto> GetRoleByIdAsync(int id)
        {
            var roleFromRepo = await _roleRepository.GetRoleByIdAsync(id);
            var roleToReturn = _mapper.Map<RoleForListDto>(roleFromRepo);
            return roleToReturn;
        }

        public async Task<BaseCommandResponse> CreateRole(RoleForCreateDto roleForCreateDto)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateRoleDtoValidator();
            var validationResult = await validator.ValidateAsync(roleForCreateDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Updating Failed";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var roleToCreate = new Role
            {
                Name = roleForCreateDto.Name
            };

            await _roleRepository.CreateRoleAsync(roleToCreate);

            response.Success = true;
            response.Message = "Updating Successful";
            response.Id = roleToCreate.Id;

            return response;
        }

        public async Task<BaseCommandResponse> UpdateRole(RoleForUpdateDto roleForUpdateDto)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateRoleDtoValidator();
            var validationResult = await validator.ValidateAsync(roleForUpdateDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Updating Failed";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var roleToUpdate = await _roleRepository.GetRoleByIdAsync(roleForUpdateDto.Id);

            if (roleToUpdate is null)
            {
                throw new NotFoundException(nameof(User), roleForUpdateDto.Id.ToString());
            }

            _mapper.Map(roleForUpdateDto, roleToUpdate);
            await _roleRepository.UpdateRoleAsync(roleToUpdate);

            response.Success = true;
            response.Message = "Updating Successful";
            response.Id = roleToUpdate.Id;

            return response;
        }

        public async Task<BaseCommandResponse> DeleteRole(int id)
        {
            var response = new BaseCommandResponse();
            var roleToDelete = await _roleRepository.GetRoleByIdAsync(id);

            if (roleToDelete == null)
            {
                throw new NotFoundException(nameof(User), id);
            }

            await _roleRepository.DeleteRoleAsync(roleToDelete);

            response.Success = true;
            response.Message = "Deleting Successful";
            response.Id = roleToDelete.Id;

            return response;
        }
    }
}
