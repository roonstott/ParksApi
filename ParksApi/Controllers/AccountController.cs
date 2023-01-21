using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ParksApi.Models;

namespace ParksApi.Controllers;

  [Route("api/[controller]")]
  // [ApiController]
  // [Authorize]
  public class AccountController : Controller
  {
    private readonly ParksApiContext _db;

    public AccountController(ParksApiContext db)
    {
      _db = db;
    }

    [HttpGet]
    public IActionResult Index(string message)
    {
        ViewBag.Message = message;
        return View();
    }
 
    [HttpPost]
    public IActionResult Index(string username, string password)
    {
      if ((username != "secret") || (password != "secret"))
      {
        return View((object)"Login Failed");
      }        
  
      var accessToken = GenerateJSONWebToken();
      SetJWTCookie(accessToken);
  
      return RedirectToAction("GoToSwagger");
    }

    private string GenerateJSONWebToken()
    {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MynameisJamesBond007"));

      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
  
      var token = new JwtSecurityToken(
        issuer: "http://localhost:5000",
        audience: "http://localhost:5000",
        expires: DateTime.Now.AddHours(3),
        signingCredentials: credentials
        );
  
      return new JwtSecurityTokenHandler().WriteToken(token);
    } 
    private void SetJWTCookie(string token)
    {
      var cookieOptions = new CookieOptions
      {
        HttpOnly = true,
        Expires = DateTime.UtcNow.AddHours(3),
      };
      Response.Cookies.Append("jwtCookie", token, cookieOptions);
    }
    public async Task<IActionResult> Validate()
    {
        var jwt = Request.Cookies["jwtCookie"];

        List<Park> parkList = new List<Park>();    
    
        using (var httpClient = new HttpClient())
        {
          httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
          using (var response = await httpClient.GetAsync("https://localhost:5000/parks"))
          {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
              string apiResponse = await response.Content.ReadAsStringAsync();
              parkList = JsonConvert.DeserializeObject<List<Park>>(apiResponse);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
              return RedirectToAction("Index", new { message = "Please Login again" });
            }
          }
        }    
      return View(parkList);
    }

  }