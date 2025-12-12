using PartyRaidR.Backend.Repos.Promises;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Assemblers;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Dtos.AuthenticationRequests;
using PartyRaidR.Shared.Models;
using PartyRaidR.Shared.Models.Responses;
using PartyRaidR.Backend.Exceptions;
using System.Text.RegularExpressions;
using BCrypt.Net;

namespace PartyRaidR.Backend.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepo _userRepo;
        private readonly UserAssembler _userAssembler;
        private readonly UserRegistrationAssembler _userRegistrationAssembler;

        public AuthenticationService(IUserRepo userRepo, UserAssembler userAssembler)
        {
            _userRepo = userRepo;
            _userAssembler = userAssembler;
            _userRegistrationAssembler = new UserRegistrationAssembler();
        }

        public async Task<ServiceResponse<string>> LoginAsync(UserLoginDto user)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<UserDto>> RegisterAsync(UserRegistrationDto userRequest)
        {
            try
            {
                bool isUserValid = await IsUserValid(userRequest);

                if (isUserValid)
                {
                    User newUser = _userRegistrationAssembler.ConvertToModel(userRequest);

                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(userRequest.Password);
                    newUser.PasswordHash = passwordHash;

                    await _userRepo.InsertAsync(newUser);

                    return new ServiceResponse<UserDto>
                    {
                        Success = true,
                        Message = "Registration successful.",
                        StatusCode = 201
                    };
                }

                return new ServiceResponse<UserDto>
                {
                    Success = false,
                    Message = "Unknown error.",
                    StatusCode = 500
                };
            }
            catch(Exception e)
            {
                return new ServiceResponse<UserDto>
                {
                    Success = false,
                    Message = $"Registration failure: {e.Message}",
                    StatusCode = 400
                };

            }
        }

        private async Task<bool> IsUserValid(UserRegistrationDto userRequest)
        {
            bool userExists = await _userRepo.EmailExistsAsync(userRequest.Email);

            if (userExists)
                throw new RegistrationWithTakenEmailAddressException($"Email address {userRequest.Email} is already in use.");

            if (!IsEmailValid(userRequest.Email))
                throw new InvalidEmailAddressException("Invalid email address.");

            if (!IsPasswordValid(userRequest.Password))
                throw new InvalidPasswordException("Invalid password.");

            return true;
        }

        private static bool IsEmailValid(string email) =>
            email.Length > 0 && email != string.Empty && email.Contains('@') && email.Contains('.') && Regex.IsMatch(email, @"(^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$)");

        private static bool IsPasswordValid(string password) =>
            password.Length >= 8 && password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit);
    }
}
