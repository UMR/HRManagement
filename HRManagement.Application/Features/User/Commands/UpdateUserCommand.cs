﻿using AutoMapper;
using HRManagement.Application.Contracts.Persistence;
using HRManagement.Application.Dtos.User;
using HRManagement.Application.Exceptions;
using HRManagement.Application.Features.User.Validators;
using HRManagement.Application.Wrapper;
using MediatR;

namespace HRManagement.Application.Features.User.Commands
{
    public record UpdateUserCommand : IRequest<BaseCommandResponse>
    {
        public UpdateUserDto UpdateUserDto { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateUserDtoValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateUserDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Updating Failed";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            }

            var userRequest = await _userRepository.GetUserByIdAsync(request.UpdateUserDto.UserId);

            if (userRequest is null)
            {
                throw new NotFoundException(nameof(userRequest), request.UpdateUserDto.UserId.ToString());
            }

            _mapper.Map(request.UpdateUserDto, userRequest);
            await _userRepository.UpdateUserAsync(userRequest);

            response.Success = true;
            response.Message = "Updating Successful";
            response.Id = userRequest.UserId;

            return response;
        }
        
    }

}