namespace Spurious2.Function2;

public class GreeterService : IGreeterService
{
    public string GetGreeting(string name)
    {
        return $"Hello, {name}. This function executed successfully.";//monkey
    }
}
