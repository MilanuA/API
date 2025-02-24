using GameAPI.Classes;
using GameAPI.GenericCrud;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class EnginesController : BaseController<Engine>
{
    public EnginesController(IGenericRepository<Engine> repository) : base(repository) { }
}