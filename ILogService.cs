using System;
using System.IO;
using log4net;

/// <summary>
/// The <see cref="ILogService"/> specifying the methods relating
/// to configuring and obtaining an instance of <see cref="ILog"/>.
/// </summary>
public interface ILogService : IDisposable
{
    /// <summary>
    /// Gets the configuration file used to configure the <see cref="ILogService"/>.
    /// </summary>
    FileInfo Configuration { get; }
    
    /// <summary>
    /// Configures the logging service by using the specified configuration file.
    /// </summary>
    /// <param name="configFile"></param>
    void Configure(FileInfo configFile);

    /// <summary>
    /// Obtains an <see cref="ILog"/> for the given <paramref name="loggerName"/>.
    /// </summary>
    /// <param name="loggerName">The name for which an <see cref="ILog"/> should be returned</param>
    /// <returns>The <see cref="ILog"/></returns>
    ILog GetLogger(string loggerName);

    /// <summary>
    /// Obtains an <see cref="ILog"/> for the given <paramref name="loggerType"/>.
    /// </summary>
    /// <param name="loggerType">The <see cref="Type"/> for which an <see cref="ILog"/> should be returned</param>
    /// <returns>The <see cref="ILog"/></returns>
    ILog GetLogger(Type loggerType);

    /// <summary>
    /// Obtains an <see cref="ILog"/> for the given <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type for which an <see cref="ILog"/> should be returned</typeparam>
    /// <returns>The <see cref="ILog"/></returns>
    ILog GetLogger<T>();
}