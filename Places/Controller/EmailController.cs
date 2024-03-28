using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Places.Interfaces;
using Places.Repository;
using System.Net;
using System.Net.Mail;

[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{

    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IMapper _mapper;
    public EmailController(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository;
       
    }
    [HttpPost]
    [Route("send")]
    public IActionResult SendEmail([FromBody] EmailRequest request)
    {

        try
        {
            var smtpClient = new SmtpClient("smtp.gmail.com") // Înlocuiește cu serverul tău SMTP
            {
                Port = 587, // Portul standard pentru SMTP
                Credentials = new NetworkCredential("romaniahp@gmail.com", "nbml imuw jlgj dnio"), // Înlocuiește cu credențialele tale
                EnableSsl = true,
            };
            var foundUser = _userProfileRepository.GetUserProfileByEmail(request.To);
            string personalizedBody = $"Salut {foundUser.FirstName},\n\n{request.Body} , parola este {foundUser.Password}";
            smtpClient.Send("romaniahp@gmail.com", request.To, "Cont creat", personalizedBody);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}

public class EmailRequest
{
    public string To { get; set; }
    public string Body { get; set; }
}
