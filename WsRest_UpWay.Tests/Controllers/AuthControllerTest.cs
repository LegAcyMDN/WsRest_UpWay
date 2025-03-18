using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OtpNet;
using WsRest_UpWay.Controllers;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;
using WsRest_UpWay.Models.Requests;
using WsRest_UpWay.Models.Responses;

namespace WsRest_UpWay.Tests.Controllers;

[TestClass]
[TestSubject(typeof(AuthController))]
public class AuthControllerTest
{
    private IConfiguration _config;

    [TestInitialize]
    public void InitializeTests()
    {
        _config = new ConfigurationManager();
        _config["JWT_SECRET_KEY"] = "c8qWCt7UFn3DASzZHD0vBhNcpNlZsQeszbpYkw28Cb3BLkzf7By6VPHQSnd0iMg";
        _config["JWT_ISSUER"] = "http://localhost:5194/";
        _config["JWT_AUDIENCE"] = "http://localhost:5194/";
    }

    [TestMethod]
    public void TestRegisterMoq()
    {
        var mockRepo = new Mock<IDataRepository<CompteClient>>();
        mockRepo.Setup(r => r.GetByStringAsync("jean.patrick@gmail.com").Result)
            .Returns(new ActionResult<CompteClient>((CompteClient)null));
        var controller = new AuthController(mockRepo.Object, _config);

        var res = controller.Register(new UserRegistrationRequest
        {
            Login = "jean.patrick",
            Email = "jean.patrick@gmail.com",
            FirstName = "Jean",
            LastName = "Patrick",
            Password = "Jean@Patrick!"
        }).Result;

        Assert.IsInstanceOfType<OkObjectResult>(res.Result);
        Assert.IsInstanceOfType<UserAuthResponse>(((OkObjectResult)res.Result).Value);
        var res2 = (UserAuthResponse)((OkObjectResult)res.Result).Value;

        Assert.IsNotNull(res2);
        Assert.IsNotNull(res2.Token);
    }

    [TestMethod]
    public void TestRegisterFailMoq()
    {
        var mockRepo = new Mock<IDataRepository<CompteClient>>();
        mockRepo.Setup(r => r.GetByStringAsync("jean.patrick@gmail.com").Result)
            .Returns(new ActionResult<CompteClient>(new CompteClient
            {
                EmailClient = "jean.patrick@gmail.com"
            }));
        var controller = new AuthController(mockRepo.Object, _config);

        var res = controller.Register(new UserRegistrationRequest
        {
            Login = "jean.patrick",
            Email = "jean.patrick@gmail.com",
            FirstName = "Jean",
            LastName = "Patrick",
            Password = "Jean@Patrick!"
        }).Result;

        Assert.IsInstanceOfType<BadRequestObjectResult>(res.Result);
        Assert.IsInstanceOfType<UserAuthResponse>(((BadRequestObjectResult)res.Result).Value);
        var res2 = (UserAuthResponse)((BadRequestObjectResult)res.Result).Value;
        Assert.IsNotNull(res2);
        Assert.IsNull(res2.Token);
        Assert.AreEqual("Email already exists", res2.Message);
    }

    [TestMethod]
    public void TestLoginMoq()
    {
        var password = "Jean@Patrick!";

        var user = new CompteClient
        {
            LoginClient = "jean.patrick",
            EmailClient = "jean.patrick@gmail.com",
            PrenomClient = "Jean",
            NomClient = "Patrick",
            MotDePasseClient = new PasswordHasher<CompteClient>().HashPassword(null, password),
            Usertype = Policies.User
        };

        var mockRepo = new Mock<IDataRepository<CompteClient>>();
        mockRepo.Setup(r => r.GetByStringAsync("jean.patrick@gmail.com").Result)
            .Returns(new ActionResult<CompteClient>(user));
        var controller = new AuthController(mockRepo.Object, _config);

        var res = controller.Login(new UserLoginRequest
        {
            Login = user.EmailClient,
            Password = password
        }).Result;

        Assert.IsInstanceOfType<OkObjectResult>(res.Result);
        Assert.IsInstanceOfType<UserAuthResponse>(((OkObjectResult)res.Result).Value);
        var res2 = (UserAuthResponse)((OkObjectResult)res.Result).Value;

        Assert.IsNotNull(res2);
        Assert.IsNotNull(res2.Token);
        Assert.IsNull(res2.RequireOTP);
    }

    [TestMethod]
    public void TestLoginWrongPasswordMoq()
    {
        var user = new CompteClient
        {
            LoginClient = "jean.patrick",
            EmailClient = "jean.patrick@gmail.com",
            PrenomClient = "Jean",
            NomClient = "Patrick",
            MotDePasseClient = new PasswordHasher<CompteClient>().HashPassword(null, "Jean@Patrick!"),
            Usertype = Policies.User
        };

        var mockRepo = new Mock<IDataRepository<CompteClient>>();
        mockRepo.Setup(r => r.GetByStringAsync("jean.patrick@gmail.com").Result)
            .Returns(new ActionResult<CompteClient>(user));
        var controller = new AuthController(mockRepo.Object, _config);

        var res = controller.Login(new UserLoginRequest
        {
            Login = user.EmailClient,
            Password = "password1337"
        }).Result;

        Assert.IsInstanceOfType<BadRequestObjectResult>(res.Result);
        Assert.IsInstanceOfType<UserAuthResponse>(((BadRequestObjectResult)res.Result).Value);
        var res2 = (UserAuthResponse)((BadRequestObjectResult)res.Result).Value;

        Assert.IsNotNull(res2);
        Assert.IsNull(res2.Token);
        Assert.AreEqual("Wrong password!", res2.Message);
    }

    [TestMethod]
    public void TestLoginWrongEmailMoq()
    {
        var password = "Jean@Patrick!";
        var user = new CompteClient
        {
            LoginClient = "jean.patrick",
            EmailClient = "jean.patrick@gmail.com",
            PrenomClient = "Jean",
            NomClient = "Patrick",
            MotDePasseClient = new PasswordHasher<CompteClient>().HashPassword(null, password),
            Usertype = Policies.User
        };

        var mockRepo = new Mock<IDataRepository<CompteClient>>();
        mockRepo.Setup(r => r.GetByStringAsync("jean.patrick@gmail.com").Result)
            .Returns(new ActionResult<CompteClient>((CompteClient)null));
        var controller = new AuthController(mockRepo.Object, _config);

        var res = controller.Login(new UserLoginRequest
        {
            Login = user.EmailClient,
            Password = password
        }).Result;

        Assert.IsInstanceOfType<BadRequestObjectResult>(res.Result);
        Assert.IsInstanceOfType<UserAuthResponse>(((BadRequestObjectResult)res.Result).Value);
        var res2 = (UserAuthResponse)((BadRequestObjectResult)res.Result).Value;

        Assert.IsNotNull(res2);
        Assert.IsNull(res2.Token);
        Assert.AreEqual("Email Does not exist!", res2.Message);
    }

    [TestMethod]
    public void TestLoginRequireOtpMoq()
    {
        var password = "Jean@Patrick!";
        var user = new CompteClient
        {
            LoginClient = "jean.patrick",
            EmailClient = "jean.patrick@gmail.com",
            PrenomClient = "Jean",
            NomClient = "Patrick",
            MotDePasseClient = new PasswordHasher<CompteClient>().HashPassword(null, password),
            Usertype = Policies.User,
            TwoFactorSecret = Convert.ToBase64String(KeyGeneration.GenerateRandomKey(2048)),
            TwoFactorConfirmedAt = DateTime.Now
        };

        var mockRepo = new Mock<IDataRepository<CompteClient>>();
        mockRepo.Setup(r => r.GetByStringAsync("jean.patrick@gmail.com").Result)
            .Returns(new ActionResult<CompteClient>(user));
        var controller = new AuthController(mockRepo.Object, _config);

        var res = controller.Login(new UserLoginRequest
        {
            Login = user.EmailClient,
            Password = password
        }).Result;

        Assert.IsInstanceOfType<OkObjectResult>(res.Result);
        Assert.IsInstanceOfType<UserAuthResponse>(((OkObjectResult)res.Result).Value);
        var res2 = (UserAuthResponse)((OkObjectResult)res.Result).Value;

        Assert.IsNotNull(res2);
        Assert.IsNull(res2.Token);
        Assert.IsNull(res2.Message);
        Assert.IsTrue(res2.RequireOTP);
    }

    [TestMethod]
    public void TestLoginOtpMoq()
    {
        var password = "Jean@Patrick!";
        var user = new CompteClient
        {
            LoginClient = "jean.patrick",
            EmailClient = "jean.patrick@gmail.com",
            PrenomClient = "Jean",
            NomClient = "Patrick",
            MotDePasseClient = new PasswordHasher<CompteClient>().HashPassword(null, password),
            Usertype = Policies.User,
            TwoFactorSecret = Convert.ToBase64String(KeyGeneration.GenerateRandomKey(2048)),
            TwoFactorConfirmedAt = DateTime.Now
        };

        var mockRepo = new Mock<IDataRepository<CompteClient>>();
        mockRepo.Setup(r => r.GetByStringAsync("jean.patrick@gmail.com").Result)
            .Returns(new ActionResult<CompteClient>(user));
        var controller = new AuthController(mockRepo.Object, _config);

        var totp = new Totp(Convert.FromBase64String(user.TwoFactorSecret));

        var res = controller.LoginOtp(new UserLoginOTPRequest
        {
            Login = user.EmailClient,
            Password = password,
            Code = totp.ComputeTotp()
        }).Result;

        Assert.IsInstanceOfType<OkObjectResult>(res.Result);
        Assert.IsInstanceOfType<UserAuthResponse>(((OkObjectResult)res.Result).Value);
        var res2 = (UserAuthResponse)((OkObjectResult)res.Result).Value;

        Assert.IsNotNull(res2);
        Assert.IsNotNull(res2.Token);
        Assert.IsNull(res2.Message);
        Assert.IsNull(res2.RequireOTP);
    }

    [TestMethod]
    public void TestLoginOtpWrongCodeMoq()
    {
        var password = "Jean@Patrick!";
        var user = new CompteClient
        {
            LoginClient = "jean.patrick",
            EmailClient = "jean.patrick@gmail.com",
            PrenomClient = "Jean",
            NomClient = "Patrick",
            MotDePasseClient = new PasswordHasher<CompteClient>().HashPassword(null, password),
            Usertype = Policies.User,
            TwoFactorSecret = Convert.ToBase64String(KeyGeneration.GenerateRandomKey(2048)),
            TwoFactorConfirmedAt = DateTime.Now
        };

        var mockRepo = new Mock<IDataRepository<CompteClient>>();
        mockRepo.Setup(r => r.GetByStringAsync("jean.patrick@gmail.com").Result)
            .Returns(new ActionResult<CompteClient>(user));
        var controller = new AuthController(mockRepo.Object, _config);

        var res = controller.LoginOtp(new UserLoginOTPRequest
        {
            Login = user.EmailClient,
            Password = password,
            Code = "pingpong"
        }).Result;

        Assert.IsInstanceOfType<BadRequestObjectResult>(res.Result);
        Assert.IsInstanceOfType<UserAuthResponse>(((BadRequestObjectResult)res.Result).Value);
        var res2 = (UserAuthResponse)((BadRequestObjectResult)res.Result).Value;

        Assert.IsNotNull(res2);
        Assert.IsNull(res2.Token);
        Assert.AreEqual("OTP code doesn't match!", res2.Message);
    }

    [TestMethod]
    public void TestSetupOtpMoq()
    {
        var user = new CompteClient
        {
            LoginClient = "jean.patrick",
            EmailClient = "jean.patrick@gmail.com",
            PrenomClient = "Jean",
            NomClient = "Patrick",
            MotDePasseClient = new PasswordHasher<CompteClient>().HashPassword(null, "Jean@Patrick!"),
            Usertype = Policies.User
        };

        var mockRepo = new Mock<IDataRepository<CompteClient>>();
        mockRepo.Setup(r => r.GetByStringAsync("jean.patrick@gmail.com").Result)
            .Returns(new ActionResult<CompteClient>(user));
        var controller = new AuthController(mockRepo.Object, _config);
        var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };

        var res = controller.SetupOtp().Result;

        Assert.IsInstanceOfType<OkObjectResult>(res.Result);
        Assert.IsInstanceOfType<SetupOTPResponse>(((OkObjectResult)res.Result).Value);
        var res2 = (SetupOTPResponse)((OkObjectResult)res.Result).Value;

        Assert.IsNotNull(res2);
        Assert.IsNotNull(res2.Secret);
    }

    [TestMethod]
    public void TestSetupOtpAlreadyEnabledMoq()
    {
        var user = new CompteClient
        {
            LoginClient = "jean.patrick",
            EmailClient = "jean.patrick@gmail.com",
            PrenomClient = "Jean",
            NomClient = "Patrick",
            MotDePasseClient = new PasswordHasher<CompteClient>().HashPassword(null, "Jean@Patrick!"),
            Usertype = Policies.User,
            TwoFactorSecret = Convert.ToBase64String(KeyGeneration.GenerateRandomKey(2048)),
            TwoFactorConfirmedAt = DateTime.Now
        };

        var mockRepo = new Mock<IDataRepository<CompteClient>>();
        mockRepo.Setup(r => r.GetByStringAsync("jean.patrick@gmail.com").Result)
            .Returns(new ActionResult<CompteClient>(user));
        var controller = new AuthController(mockRepo.Object, _config);
        var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };

        var res = controller.SetupOtp().Result;

        Assert.IsInstanceOfType<BadRequestObjectResult>(res.Result);
        Assert.IsInstanceOfType<SetupOTPResponse>(((BadRequestObjectResult)res.Result).Value);
        var res2 = (SetupOTPResponse)((BadRequestObjectResult)res.Result).Value;

        Assert.IsNotNull(res2);
        Assert.IsNull(res2.Secret);
        Assert.AreEqual("OTP Already enabled!", res2.Message);
    }

    [TestMethod]
    public void TestOtpConfirmationMoq()
    {
        var password = "Jean@Patrick!";
        var user = new CompteClient
        {
            LoginClient = "jean.patrick",
            EmailClient = "jean.patrick@gmail.com",
            PrenomClient = "Jean",
            NomClient = "Patrick",
            MotDePasseClient = new PasswordHasher<CompteClient>().HashPassword(null, password),
            Usertype = Policies.User,
            TwoFactorSecret = Convert.ToBase64String(KeyGeneration.GenerateRandomKey(2048))
        };

        var mockRepo = new Mock<IDataRepository<CompteClient>>();
        mockRepo.Setup(r => r.GetByStringAsync("jean.patrick@gmail.com").Result)
            .Returns(new ActionResult<CompteClient>(user));
        var controller = new AuthController(mockRepo.Object, _config);
        var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };

        var totp = new Totp(Convert.FromBase64String(user.TwoFactorSecret));

        var res = controller.ConfirmOtpSetup(new ConfirmOTPSetupRequest
        {
            Code = totp.ComputeTotp()
        }).Result;

        Assert.IsInstanceOfType<OkResult>(res.Result);
    }

    [TestMethod]
    public void TestOtpConfirmationAlreadyEnabledMoq()
    {
        var password = "Jean@Patrick!";
        var user = new CompteClient
        {
            LoginClient = "jean.patrick",
            EmailClient = "jean.patrick@gmail.com",
            PrenomClient = "Jean",
            NomClient = "Patrick",
            MotDePasseClient = new PasswordHasher<CompteClient>().HashPassword(null, password),
            Usertype = Policies.User,
            TwoFactorSecret = Convert.ToBase64String(KeyGeneration.GenerateRandomKey(2048)),
            TwoFactorConfirmedAt = DateTime.Now
        };

        var mockRepo = new Mock<IDataRepository<CompteClient>>();
        mockRepo.Setup(r => r.GetByStringAsync("jean.patrick@gmail.com").Result)
            .Returns(new ActionResult<CompteClient>(user));
        var controller = new AuthController(mockRepo.Object, _config);
        var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };

        var res = controller.ConfirmOtpSetup(new ConfirmOTPSetupRequest
        {
            Code = "Doesn't matter what we put here"
        }).Result;

        Assert.IsInstanceOfType<BadRequestObjectResult>(res.Result);
        Assert.IsInstanceOfType<SetupOTPResponse>(((BadRequestObjectResult)res.Result).Value);
        var res2 = (SetupOTPResponse)((BadRequestObjectResult)res.Result).Value;

        Assert.IsNotNull(res2);
        Assert.IsNull(res2.Secret);
        Assert.AreEqual("OTP Already enabled!", res2.Message);
    }
}