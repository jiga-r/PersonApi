using System.Xml;
using System.Xml.Schema;

public class XsdValidationCommand : IValidationCommand
{
    private readonly string _xsdPath;

    public XsdValidationCommand(string xsdPath)
    {
        _xsdPath = xsdPath;
    }

    public bool Execute(string xmlPayload)
    {
        var settings = new XmlReaderSettings();
        settings.Schemas.Add(null, XmlReader.Create(new StreamReader(_xsdPath)));
        settings.ValidationType = ValidationType.Schema;

        var reader = XmlReader.Create(new StringReader(xmlPayload), settings);
        var document = new XmlDocument();
        document.Load(reader);

        try
        {
            document.Validate(null);
            return true;
        }
        catch (XmlSchemaValidationException)
        {
            return false;
        }
    }
}
