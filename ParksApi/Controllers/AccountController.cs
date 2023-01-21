using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ParksApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ParksApi.Controllers;

  // [Route("api/[controller]")]
  // [ApiController]
  // [Authorize]
  public class AccountController : Controller
  {
    // private readonly ParksApiContext _db;

    // public AccountController(ParksApiContext db)
    // {
    //   _db = db;
    // }

    [HttpGet("/")]
    public IActionResult Login(string message)
    {   
        ViewBag.Message = message;
        return View();
    }
 
    [HttpPost("/")]
    public ActionResult Login(string password, string username)
    {
      if ((password != "secret") || (username != "secret"))
      {
        return RedirectToAction("Login", "Account", new {message = "Wrong username or password"});
      }
      else
      {
        var accessToken = GenerateJSONWebToken();
        SetJWTCookie(accessToken);  
        return RedirectToAction("Validation");
      }      
    }

    private string GenerateJSONWebToken()
    {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MynameisJamesBond007"));

      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
  
      var token = new JwtSecurityToken(
        issuer: "http://localhost:5000",
        audience: "http://localhost:5000",
        expires: DateTime.Now.AddMinutes(10),
        signingCredentials: credentials
        );  
      return new JwtSecurityTokenHandler().WriteToken(token);
    } 
    private void SetJWTCookie(string token)
    {
      var cookieOptions = new CookieOptions
      {
        HttpOnly = true,
        Expires = DateTime.UtcNow.AddMinutes(10),
      };
      Response.Cookies.Append("jwtCookie", token, cookieOptions);
    }

    [HttpGet ("/Validation")]
    public async Task<IActionResult> Validation()
    {
        var jwt = Request.Cookies["jwtCookie"];
    
        using (var httpClient = new HttpClient())
        {
          Console.WriteLine(httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt));
          using (var response = await httpClient.GetAsync("https://localhost:5000/api/parks"))
          {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
              string apiResponse = await response.Content.ReadAsStringAsync();
              List<Park> parkList = JsonConvert.DeserializeObject<List<Park>>(apiResponse);
              return View(parkList);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
              return RedirectToAction("Login", new { message = "Please Login again" });
            }
          }
        }    
      return View();
    }
  }