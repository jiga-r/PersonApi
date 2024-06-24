public class SchemaValidationMachineFactory : IValidationMachineFactory
{
    private readonly IConfiguration _configuration;

    public SchemaValidationMachineFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ValidationMachine CreateValidationMachine()
    {
        var machine = new ValidationMachine();
        machine.AddCommand(new XsdValidationCommand(_configuration["XsdPath"]));
        return machine;
    }
}