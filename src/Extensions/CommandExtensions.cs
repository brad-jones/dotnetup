namespace System.CommandLine;

public static class CommandExtensions
{
  public static Argument<T> Arg<T>(
    this Command cmd,
    string name,
    string desc,
    T? defaultValue = default,
    Func<T>? defaultValueFactory = null
  )
  {
    var arg = new Argument<T>(name) { Description = desc };

    if (defaultValue is not null)
    {
      arg.DefaultValueFactory = _ => defaultValue;
    }

    if (defaultValueFactory != null)
    {
      arg.DefaultValueFactory = _ => defaultValueFactory();
    }

    cmd.Arguments.Add(arg);
    return arg;
  }

  public static Option<T> Opt<T>(
    this Command cmd,
    string name,
    string desc,
    T? defaultValue = default,
    Func<T>? defaultValueFactory = null
  )
  {
    var opt = new Option<T>(name) { Description = desc };

    if (defaultValue is not null)
    {
      opt.DefaultValueFactory = _ => defaultValue;
    }

    if (defaultValueFactory != null)
    {
      opt.DefaultValueFactory = _ => defaultValueFactory();
    }

    cmd.Options.Add(opt);
    return opt;
  }
}
