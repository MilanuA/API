using System.Linq.Expressions;
using GameAPI.Classes;
using GameAPI.GenericCrud;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class GamesController : BaseController<Game>
{
    public GamesController(IGenericRepository<Game> repository) : base(repository) {}
}