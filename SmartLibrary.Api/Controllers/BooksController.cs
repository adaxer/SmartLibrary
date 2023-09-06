using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SmartLibrary.Common.Models;

namespace SmartLibrary.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly ILogger<BooksController> _logger;
    private readonly IHubContext<BooksHub> _context;

    public BooksController(ILogger<BooksController> logger, IHubContext<BooksHub> context)
    {
        _logger = logger;
        this._context = context;
    }

    [HttpPost, Route("[action]")]
    public ActionResult Save(SavedBook book)
    {
        string json = JsonConvert.SerializeObject(book);
        _context.Clients.All.SendAsync("BookShared", json);
        return Ok();
    }
}
