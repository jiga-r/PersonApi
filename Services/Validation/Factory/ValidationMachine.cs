public class ValidationMachine
{
    private readonly List<IValidationCommand> _commands;

    public ValidationMachine()
    {
        _commands = new List<IValidationCommand>();
    }

    public void AddCommand(IValidationCommand command)
    {
        _commands.Add(command);
    }

    public bool Validate(string payload)
    {
        foreach (var command in _commands)
        {
            if (!command.Execute(payload))
            {
                // If any command fails, return false
                return false;
            }
        }

        // If all commands succeed, return true
        return true;
    }
}
