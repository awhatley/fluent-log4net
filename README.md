Fluent Log4Net
==============

An alternative to XML-based log4net configuration, allowing logging to be configured using a 
strongly-typed fluent interface.

The goal of the project is to fully support all built-in appenders and other log4net configuration 
settings, and make the intention of the various settings less opaque and easier to read at a 
glance.


Usage
-----

All configuration begins with the root `Log4Net` class and its `Configure()` method. The 
simplest possible configuration with a single console appender would be:

    Log4Net.Configure()
        .Logging.Root(log => log.To.Console())
        .ApplyConfiguration();

More advanced scenarios including repository settings and custom renderers, as well as 
individual logger configurations can also be done. Note that most of the specific appender
configurations haven't been written yet, but they are coming.

    Log4Net.Configure()
        .InternalDebugging(true)
        .Overwrite(true)
        .Threshold(Level.Debug)

        .Logging.Root(log => log
            .To.Console(c => c.Targeting.ConsoleOut))

        .Logging.For<MyClass>(log => log
            .At(Level.Warn)
            .To.File( ... )
            .To.Database( ... )
            .To.ColoredConsole( ... )

        .Logging.For<MyOtherClass>(log => log
            .At(Level.Error)
            .To.EventLog( ... )

        .Render.Type<Int32>().Using<Int32Renderer>()
        .Render.Type<Int64>().Using(typeof(Int64Renderer))
        .Render.Type(typeof(Single)).Using(typeof(SingleRenderer))
        .Render.Type(typeof(Double)).Using<DoubleRenderer>()

        .ApplyConfiguration();

In most cases, appenders are usually shared among several loggers. The above syntax creates 
separate appenders for each logger, which may not be desired. To share appender references, 
you can define them ahead of time using the `Append.To` definition builder.

    var myConsoleAppender = Append.To.Console(c => c.Targeting.ConsoleOut);
    var myDbAppender = Append.To
        .File(f => f
            .Named("myfile.log")
            .Append(true)
            .LockingMinimally());

    Log4Net.Configure()
        .Logging.Root(log => log
            .At(Level.Debug)
            .To.Appender(myConsoleAppender))

        .Logging.For<MyClass>(log => log
            .At(Level.Error)
            .To.Appender(myDbAppender))

        .Logging.For<MyOtherClass(log => log
            .At(Level.Error)
            .To.Appender(myDbAppender))

        .ApplyConfiguration();    


Todo List
---------

* Support for all built-in log4net appenders
* Validate config before applying
* Common threshold, filter, layout, and error-handler interfaces for appenders