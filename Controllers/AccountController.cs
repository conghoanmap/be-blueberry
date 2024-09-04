using blueberry.Interfaces;
using blueberry.Dtos.Account;
using blueberry.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using blueberry.Extensions;
using Microsoft.AspNetCore.Authorization;
using blueberry.Mappers;

namespace blueberry.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenRepository _tokenRepository;
        public AccountController(IEmailSender emailSender, IAccountRepository accountRepository, UserManager<AppUser> userManager, ITokenRepository tokenRepository)
        {
            this._emailSender = emailSender;
            this._accountRepository = accountRepository;
            this._tokenRepository = tokenRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var AppUser = new AppUser
                {
                    UserName = registerRequest.Username,
                    FullName = registerRequest.FullName,
                    Email = registerRequest.Email
                };

                var createUser = await _accountRepository.CreateAsync(AppUser, registerRequest.Password);

                if (createUser.Succeeded)
                {
                    var roleResult = await _accountRepository.AddToRoleAsync(AppUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok("Đăng ký tài khoản thành công");
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = await _accountRepository.FirstOrDefaultAsync(loginRequest.Username);
                if (user == null)
                {
                    return Unauthorized("Tài khoản không tồn tại");
                }
                // Kiểm tra mật khẩu
                var result = await _accountRepository.CheckPasswordSignInAsync(user, loginRequest.Password);
                if (!result.Succeeded)
                {
                    return Unauthorized("Mật khẩu không chính xác");
                }
                else
                {
                    return Ok(new LogRegResponse
                    {
                        Username = user.UserName,
                        FullName = user.FullName,
                        Token = await _tokenRepository.createToken(user),
                        Roles = await _accountRepository.GetRolesAsync(user)
                    });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        
        [HttpGet]
        [Authorize]
        [Route("profile")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var username = User.GetUserName();
                var users = await _accountRepository.FirstOrDefaultAsync(username);
                return Ok(users.ModelToDisplay());
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("update")]
        public async Task<IActionResult> Update(AccountUpdate accountUpdate)
        {
            try
            {
                var username = User.GetUserName();
                var user = await _accountRepository.FirstOrDefaultAsync(username);
                if (user == null)
                {
                    return NotFound("Tài khoản không tồn tại");
                }
                user.FullName = accountUpdate.FullName;
                user.Address = accountUpdate.Address;
                user.Phone = accountUpdate.Phone;
                var result = await _accountRepository.UpdateAsync(user);
                if (result == null)
                {
                    return StatusCode(500, "Cập nhật thông tin thất bại");
                }
                return Ok("Cập nhật thông tin thành công");
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        
        [HttpPut]
        [Authorize]
        [Route("recharge")]
        public async Task<IActionResult> Recharge(decimal amount)
        {
            try
            {
                var username = User.GetUserName();
                var user = await _accountRepository.FirstOrDefaultAsync(username);
                if (user == null)
                {
                    return NotFound("Tài khoản không tồn tại");
                }
                user.AccountBalance += amount;
                var result = await _accountRepository.UpdateAsync(user);
                if (result == null)
                {
                    return StatusCode(500, "Nạp tiền thất bại");
                }
                return Ok("Nạp tiền thành công");
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        
        [HttpPut]
        [Authorize]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            try
            {
                var username = User.GetUserName();
                var user = await _accountRepository.FirstOrDefaultAsync(username);
                if (user == null)
                {
                    return NotFound("Tài khoản không tồn tại");
                }

                var checkPassword = await _accountRepository.CheckPasswordSignInAsync(user, changePasswordRequest.OldPassword);
                if (!checkPassword.Succeeded)
                {
                    return Unauthorized("Mật khẩu cũ không chính xác");
                }

                if (changePasswordRequest.NewPassword != changePasswordRequest.ConfirmPassword)
                {
                    return BadRequest("Mật khẩu mới không khớp");
                }

                var result = await _accountRepository.ChangePasswordAsync(user, changePasswordRequest.OldPassword, changePasswordRequest.NewPassword);
                if (result.Succeeded)
                {
                    return Ok("Đổi mật khẩu thành công");
                }
                return StatusCode(500, result.Errors);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        
        [HttpPut]
        [Authorize]
        [Route("verify-email")]
        public async Task<IActionResult> VerifyEmail(string token, string email)
        {
            try
            {
                var user = await _accountRepository.FindByEmailAsync(email);
                if (user == null)
                {
                    return NotFound("Tài khoản không tồn tại");
                }
                var result = await _accountRepository.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return Ok("Xác thực email thành công");
                }
                return StatusCode(500, result.Errors);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("send-mail")]
        public async Task<IActionResult> SendMail()
        {
            try
            {
                var username = User.GetUserName();
                var user = await _accountRepository.FirstOrDefaultAsync(username);
                if (user == null)
                {
                    return NotFound("Tài khoản không tồn tại");
                }
                var token = await _accountRepository.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("VerifyEmail", "Account", new { token = token, email = user.Email }, Request.Scheme);
                await _emailSender.SendEmailAsync("hoan39800@gmail.com", user.Email, "Xác thực email", $"Vui lòng xác thực email bằng cách <a href='{callbackUrl}'>nhấn vào đây</a>");
                return Ok("Gửi email xác thực thành công");
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("grant-role")]
        public async Task<IActionResult> GrantRole(string username, string roleName)
        {
            try
            {
                var user = await _accountRepository.FirstOrDefaultAsync(username);
                if (user == null)
                {
                    return NotFound("Tài khoản không tồn tại");
                }
                var result = await _accountRepository.AddToRoleAsync(user, roleName);
                if (result.Succeeded)
                {
                    return Ok(new LogRegResponse
                    {
                        Username = user.UserName,
                        FullName = user.FullName,
                        Token = "",
                        Roles = await _accountRepository.GetRolesAsync(user)
                    });
                }
                return StatusCode(500, result.Errors);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("revoke-role")]
        public async Task<IActionResult> RevokeRole(string username, string roleName)
        {
            try
            {
                var user = await _accountRepository.FirstOrDefaultAsync(username);
                if (user == null)
                {
                    return NotFound("Tài khoản không tồn tại");
                }
                var result = await _accountRepository.RemoveFromRoleAsync(user, roleName);
                if (result.Succeeded)
                {
                    return Ok(new LogRegResponse
                    {
                        Username = user.UserName,
                        FullName = user.FullName,
                        Token = "",
                        Roles = await _accountRepository.GetRolesAsync(user)
                    });
                }
                return StatusCode(500, result.Errors);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}