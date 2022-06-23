namespace OrderEntry.Infrastructure;

public static class DotEnv
{
    public static void TryLoad()
    {
        var dotenv = string.Empty;
        var maxAttempt = 4; 
        var attempts = 0;
        do
        {
            var level = string.Join("/", Enumerable.Repeat("..", attempts).ToList());

            dotenv = Path.Combine(Directory.GetCurrentDirectory(), level, ".env");

            if (File.Exists(dotenv))
                break;

            attempts++;
        } while (maxAttempt > attempts);

        if (!File.Exists(dotenv))
            return;

        var variables = File.ReadAllText(dotenv)
            .Split("\n")
            .Where(e => !string.IsNullOrEmpty(e))
            .Where(e => !e.StartsWith("#"))
            .ToList();

        var environmentVariables = variables.ToDictionary(
            variable => variable.Substring(0, variable.IndexOf("=", StringComparison.Ordinal))
            , variable =>
            {
                var value = variable.Remove(0, variable.IndexOf("=", StringComparison.Ordinal) + 1);
                if (value.StartsWith("\"") && value.EndsWith("\""))
                    return value.Remove(0, 1)
                        .Remove(value.LastIndexOf("\"", StringComparison.Ordinal) - 1,
                            1); //.Substring(value.LastIndexOf("\""));
                return value;
            });

        foreach (var environmentVariable in environmentVariables)
            Environment.SetEnvironmentVariable(environmentVariable.Key, environmentVariable.Value);
    }
}