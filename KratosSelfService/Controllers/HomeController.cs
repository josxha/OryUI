using KratosSelfService.Models;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

public class HomeController(
    IdentitySchemaService schemaService,
    ILogger<HomeController> logger,
    MinioService minioService) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> Home([FromQuery(Name = "avatar")] string avatarUrl)
    {
        var session = (KratosSession)HttpContext.Items[typeof(KratosSession)]!;
        var schema = await schemaService.FetchSchema(session.Identity.SchemaId,
            session.Identity.SchemaUrl);
        return View("Profile", new ProfileModel(session, IdentitySchemaService.GetTraits(schema), avatarUrl));
    }

    [HttpGet("welcome")]
    [AllowAnonymous]
    public IActionResult Welcome()
    {
        // this endpoint exists for parity reason to the ory kratos self service ui
        return Redirect("~/");
    }

    [HttpGet("links")]
    [AllowAnonymous]
    public IActionResult Links()
    {
        return View("Links");
    }

    [HttpPost("")]
    public async Task<IActionResult> UpdateProfilePicture([FromForm(Name = "avatar")] IFormFile? file)
    {
        if (file == null || file.Length == 0 || string.IsNullOrWhiteSpace(file.Headers.ContentType))
            return Redirect("~/");

        if (file.Length > 10485760) return BadRequest("Image is bigger than the maximum allowed file size of 10MB");
        var contentType = file.Headers.ContentType.ToString();
        if (!contentType.StartsWith("image/")) return BadRequest("Content type is no image");
        var fileExtension = file.FileName.Split(".").Last();
        var allowedExtensions = new List<string> { "png", "jpg", "webp", "jpeg" };
        if (!allowedExtensions.Contains(fileExtension)) return BadRequest("File extension not allowed");

        var stream = new MemoryStream((int)file.Length);
        await file.CopyToAsync(stream);
        stream.Position = 0;

        var fileName = Guid.NewGuid() + "." + fileExtension;
        await minioService.AddObject("test", fileName, file.Headers.ContentType!, stream);

        return Redirect("~/?avatar=https://s3.bodensee-navigator.com/test/" + fileName);
    }
}