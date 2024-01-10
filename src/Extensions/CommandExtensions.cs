namespace System.CommandLine;

public static class CommandExtensions
{
    public static Argument<T> Arg<T>(
        this Command cmd,
        string name,
        string desc,
        object? defaultValue = null,
        Func<object>? defaultValueFactory = null
    )
    {
        var arg = new Argument<T>(name, desc);

        if (defaultValue != null)
            arg.SetDefaultValue(defaultValue);

        if (defaultValueFactory != null)
            arg.SetDefaultValueFactory(defaultValueFactory);

        cmd.AddArgument(arg);
        return arg;
    }

    public static Option<T> Opt<T>(
        this Command cmd,
        string name,
        string desc,
        object? defaultValue = null,
        Func<object>? defaultValueFactory = null
    )
    {
        var opt = new Option<T>(name, desc);

        if (defaultValue != null)
            opt.SetDefaultValue(defaultValue);

        if (defaultValueFactory != null)
            opt.SetDefaultValueFactory(defaultValueFactory);

        cmd.AddOption(opt);
        return opt;
    }
}
