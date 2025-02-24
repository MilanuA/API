using GameAPI.Classes;
using GameAPI.GenericCrud;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class GenresController : BaseController<Genre>
{
    public GenresController(IGenericRepository<Genre> repository) : base(repository) { }
}