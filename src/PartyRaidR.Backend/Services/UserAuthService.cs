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
        private readonly UserAssembler _userAssembler;
        private readonly UserRegistrationAssembler _userRegistrationAssembler;

        public UserAuthService(IUserRepo userRepo, ITokenService tokenService, UserAssembler? userAssembler)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            _userRegistrationAssembler = new UserRegistrationAssembler();
            _userAssembler = userAssembler ?? throw new ArgumentNullException(nameof(userAssembler));
        }

        public async Task<ServiceResponse<string>> LoginAsync(UserLoginDto request)
        {
            User? user = await _userRepo.GetByEmailAsync(request.Email);

            if(user is null)
                return CreateResponse<string>(false, 400, message: "Login failed: Incorrect email or password.");
            else
            {
                if(BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                {
                    string token = _tokenService.GenerateToken(user);
                    return CreateResponse(true, 200, token, "Login successful.");
                }
                else
                    return CreateResponse<string>(false, 400, message: "Login failed: Incorrect email or password.");
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

                    await _userRepo.InsertAsync(newUser);
                    await _userRepo.SaveChangesAsync();

                    return CreateResponse<UserDto>(true, 201, _userAssembler.ConvertToDto(newUser), message: "Registration successful.");
                }

                return CreateResponse<UserDto>(false, 400, message: "Registration failed: Invalid user data.");
            }
            catch(Exception e)
            {
                return CreateResponse<UserDto>(false, 500, message: $"Registration failure: {e.Message}");
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

        private ServiceResponse<T> CreateResponse<T>(bool isSuccess, int statusCode, T? data = default, string? message = null)
        {
            return new ServiceResponse<T>
            {
                Success = isSuccess,
                StatusCode = statusCode,
                Data = data,
                Message = message ?? string.Empty
            };
        }

        private static bool IsEmailValid(string email) =>
            email.Length > 0 && email != string.Empty && email.Contains('@') && email.Contains('.') && Regex.IsMatch(email, @"(^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$)");

        private static bool IsPasswordValid(string password) =>
            password.Length >= 8 && password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit);
    }
}
