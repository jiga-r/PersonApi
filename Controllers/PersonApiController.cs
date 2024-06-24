using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/person")]
public class PersonApiController : ControllerBase
{

    private readonly SchemaValidation _schemaValidation;
    public PersonApiController(SchemaValidation schemaValidation) {
        _schemaValidation = schemaValidation;
    }

    [HttpPost]
    public IActionResult Post([FromBody] XElement personXml)
    {
        var personPayload = personXml.ToString();
        if (_schemaValidation.IsValid(personPayload))
        {
            // If the person is valid, return a 200 OK response
            return Ok(personPayload);
        }
        else
        {
            // If the person is not valid, return a 400 Bad Request response
            return BadRequest("Invalid person");
        }
    }
}
