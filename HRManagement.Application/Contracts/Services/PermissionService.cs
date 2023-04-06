using AutoMapper;
using HRManagement.Application.Contracts.Persistence;
using HRManagement.Application.Dtos.Permissions;
using HRManagement.Application.Exceptions;
using HRManagement.Application.Validators.Permission;
using HRManagement.Application.Wrapper;
using HRManagement.Domain.Entities;

namespace HRManagement.Application.Contracts.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IMapper _mapper;
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IMapper mapper, IPermissionRepository permissionRepository)
        {
            _mapper = mapper;
            _permissionRepository = permissionRepository;
        }

        public async Task<List<PermissionForListDto>> GetPermissionsAsync()
        {
            var permissionsFromRepo = await _permissionRepository.GetPermissionsAsync();
            var permissionsToReturn = _mapper.Map<List<PermissionForListDto>>(permissionsFromRepo);
            return permissionsToReturn;
        }

        public async Task<PermissionForListDto> GetPermissionByIdAsync(int id)
        {
            var permissionFromRepo = await _permissionRepository.GetPermissionByIdAsync(id);
            var permissionToReturn = _mapper.Map<PermissionForListDto>(permissionFromRepo);
            return permissionToReturn;
        }

        public async Task<BaseCommandResponse> CreatePermission(PermissionForCreateDto permissionForCreateDto)
        {
            var response = new BaseCommandResponse();
            var validator = new CreatePermissionDtoValidator();
            var validationResult = await validator.ValidateAsync(permissionForCreateDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Updating Failed";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var permissionToCreate = new Permission
            {
                Name = permissionForCreateDto.Name
            };

            await _permissionRepository.CreatePermissionAsync(permissionToCreate);

            response.Success = true;
            response.Message = "Updating Successful";
            response.Id = permissionToCreate.Id;

            return response;
        }

        public async Task<BaseCommandResponse> UpdatePermission(PermissionForUpdateDto permissionForUpdateDto)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdatePermissionDtoValidator();
            var validationResult = await validator.ValidateAsync(permissionForUpdateDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Updating Failed";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var permissionToUpdate = await _permissionRepository.GetPermissionByIdAsync(permissionForUpdateDto.Id);

            if (permissionToUpdate is null)
            {
                throw new NotFoundException(nameof(User), permissionForUpdateDto.Id.ToString());
            }

            _mapper.Map(permissionForUpdateDto, permissionToUpdate);
            await _permissionRepository.UpdatePermissionAsync(permissionToUpdate);

            response.Success = true;
            response.Message = "Updating Successful";
            response.Id = permissionToUpdate.Id;

            return response;
        }

        public async Task<BaseCommandResponse> DeletePermission(int id)
        {
            var response = new BaseCommandResponse();
            var permissionToDelete = await _permissionRepository.GetPermissionByIdAsync(id);

            if (permissionToDelete == null)
            {
                throw new NotFoundException(nameof(User), id);
            }

            await _permissionRepository.DeletePermissionAsync(permissionToDelete);

            response.Success = true;
            response.Message = "Deleting Successful";
            response.Id = permissionToDelete.Id;

            return response;
        }
    }
}
