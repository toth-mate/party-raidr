using PartyRaidR.Backend.Repos.Promises;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Dtos.AuthenticationRequests;
using PartyRaidR.Backend.Exceptions;
using System.Text.RegularExpressions;
using PartyRaidR.Backend.Models.Responses;
using PartyRaidR.Backend.Assemblers;
using PartyRaidR.Backend.Models;

namespace PartyRaidR.Backend.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IUserRepo _userRepo;
        private readonly ITokenService _tokenService;
        private readonly UserRegistrationAssembler _userRegistrationAssembler;

        public UserAuthService(IUserRepo userRepo, ITokenService tokenService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            _userRegistrationAssembler = new UserRegistrationAssembler();
        }

        public async Task<ServiceResponse<string>> LoginAsync(UserLoginDto request)
        {
            User? user = await _userRepo.GetByEmailAsync(request.Email);

            if(user is null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Login failed: Incorrect email or password.",
                    StatusCode = 401
                };
            }
            else
            {
                if(BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                {
                    string token = _tokenService.GenerateToken(user);

                    return new ServiceResponse<string>
                    {
                        Data = token,
                        Success = true,
                        StatusCode = 200,
                        Message = "Login successful."
                    };
                }
                else
                {
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        StatusCode = 401,
                        Message = "Login failed: Incorrect email or password."
                    };
                }
            }
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
                    newUser.Id = Guid.CreateVersion7().ToString();

                    Console.WriteLine(newUser.Id);

                    await _userRepo.InsertAsync(newUser);
                    await _userRepo.SaveChangesAsync();

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

        private async Task<bool> IsUserValid(UserRegistrationDto request)
        {
            bool emailExists = await _userRepo.EmailExistsAsync(request.Email),
                 usernameExists = await _userRepo.GetByUsernameAsync(request.Username) is not null;

            if (emailExists)
                throw new RegistrationWithTakenEmailAddressException($"Email address {request.Email} is already in use.");

            if (usernameExists)
                throw new RegistrationWithTakenUsernameException("The given username is already in use.");

            if (!IsEmailValid(request.Email))
                throw new InvalidEmailAddressException("Invalid email address.");

            if (!IsPasswordValid(request.Password))
                throw new InvalidPasswordException("Invalid password.");

            // Check if user is at least 16 years old
            // Temporary solution
            if (request.BirthDate > DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-16)))
                throw new UserDoesNotMeetRequiredAgeException("User must be at least 16 years old to register.");

            return true;
        }

        private static bool IsEmailValid(string email) =>
            email.Length > 0 && email != string.Empty && email.Contains('@') && email.Contains('.') && Regex.IsMatch(email, @"(^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$)");

        private static bool IsPasswordValid(string password) =>
            password.Length >= 8 && password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit);
    }
}
