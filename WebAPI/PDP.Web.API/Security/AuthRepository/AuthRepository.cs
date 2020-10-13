using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PDP.Web.API.Data;
using PDP.Web.API.Dtos.User;
using PDP.Web.API.Models;


namespace PDP.Web.API.Security
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration configuration;
        private readonly DataContext context;

        private const string loginErrMsg = "The username or password is incorrect.";

        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<Response<string>> Login(UserLoginDto login)
        {
            var user = await context.Users.Include(u => u.Password).FirstOrDefaultAsync(x => x.Username == login.Username);
            if (user == null) return Response<string>.Fail(loginErrMsg);
            if (user.Password == null) return Response<string>.Fail(loginErrMsg);
            return VerifyPasswordHash(login.Password, user.Password.Hash, user.Password.Salt) ?
                Response<string>.Succeed(CreateToken(user)) :
                Response<string>.Fail(loginErrMsg);
        }

        public async Task<Response<string>> Register(UserRegisterDto registration)
        {
            if (await UserExist(registration.Username))
            {
                return Response<string>.Fail("User already exists.");
            }

            var password = new UserPassword();
            CreatePassordHash(registration.Password, out byte[] passwordHash, out byte[] passwordSalt);
            password.Hash = passwordHash;
            password.Salt = passwordSalt;

            var user = new User();
            user.Username = registration.Username;
            user.Password = password;
            user.Role = registration.Role;
            await context.AddAsync(user);
            await context.SaveChangesAsync();
            return Response<string>.Succeed(CreateToken(user));
        }

        public async Task<bool> UserExist(string username)
        {
            return await context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }

        private void CreatePassordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            passwordHash = null;
            passwordSalt = null;
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Pw2Bytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;

            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var hash = hmac.ComputeHash(Pw2Bytes(password));

                if (hash.Length != passwordHash.Length) return false;

                for (int i = 0; i < hash.Length; i++)
                    if (hash[i] != passwordHash[i]) return false;
            }

            return true;
        }

        private byte[] Pw2Bytes(string password)
        {
            return System.Text.Encoding.UTF8.GetBytes(password)
                .Concat(new byte[] { 187, 196, 42, 237, 209, 243, 120, 38, 31, 195, 242, 63 })
                .ToArray();
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString ()),
                new Claim (ClaimTypes.Name, user.Username),
                new Claim (ClaimTypes.Role, user.Role)
            };

            var tokenKey = configuration.GetSection("AppSettings:Token").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            int.TryParse(configuration.GetSection("AppSettings:TokenExpirationMinutes").Value, out int tokenMinutes);
            if (tokenMinutes < 1) tokenMinutes = 30;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(tokenMinutes).ToUniversalTime(),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}