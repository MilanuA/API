using GameAPI.Classes;
using GameAPI.GenericCrud;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class PlatformsController : BaseController<Platform>
{
    public PlatformsController(IGenericRepository<Platform> repository) : base(repository) { }
}