public class SchemaValidation
{
    private readonly ValidationMachine _validationMachine;

    public SchemaValidation(IValidationMachineFactory factory)
    {
        _validationMachine = factory.CreateValidationMachine();
    }

    public bool IsValid(string payload)
    {
        return _validationMachine.Validate(payload);
    }
}