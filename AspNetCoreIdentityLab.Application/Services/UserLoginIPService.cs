using System;
using System.Net;
using Microsoft.Extensions.Configuration;
using AspNetCoreIdentityLab.Persistence.Mappers;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AspNetCoreIdentityLab.Application.Services
{
    public class UserLoginIPService
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly UserLoginIPMapper _userLoginIPMapper;

        public UserLoginIPService(IConfiguration configuration, IEmailSender emailSender, UserLoginIPMapper userLoginIPMapper)
        {
            _configuration = configuration;
            _emailSender = emailSender;
            _userLoginIPMapper = userLoginIPMapper;
        }

        public void VerifyLoginFromMultipleIPs(int userId, string userEmail, IPAddress remoteIpAddress)
        {
            var verifyLoginFromDifferentIPs = Convert.ToBoolean(_configuration["VerifyLoginFromDifferentIPs"]);

            if (verifyLoginFromDifferentIPs == false) return;

            var userLoginIP = _userLoginIPMapper.FindBy(userId);
            var ipFromRequest = remoteIpAddress.ToString();

            if (remoteIpAddress.IsIPv4MappedToIPv6)
            {
                ipFromRequest = remoteIpAddress.MapToIPv4().ToString();
            }

            if (userLoginIP == null)
            {
                _userLoginIPMapper.Save(userId, ipFromRequest);
            }
            else
            {
                var message = $"Your account was accessed from a different IP: {ipFromRequest}";

                userLoginIP.IP = ipFromRequest;
                userLoginIP.When = DateTime.Now;
                _userLoginIPMapper.Update(userLoginIP);
                
                if (userLoginIP.IP != ipFromRequest) _emailSender.SendEmailAsync(userEmail, "Account Login from different IP", message);
            }
        }
    }
}
