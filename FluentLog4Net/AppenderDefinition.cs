namespace FluentLog4Net
{
    public abstract class AppenderDefinition
    {
        public abstract IAppenderConfiguration Configure();
    }
}