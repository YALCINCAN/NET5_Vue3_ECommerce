using Core.CrossCuttingConcerns.Logging.SeriLog.ConfigurationModels;
using Core.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging.SeriLog.Loggers
{
	public class MsSqlLogger : LoggerServiceBase
	{
		public MsSqlLogger()
		{
			var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

			var logConfig = configuration.GetSection("SeriLogConfigurations:MsSqlConfiguration")
					.Get<MsSqlConfiguration>() ?? throw new Exception("Null");
			var sinkOpts = new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true};

			var columnOptions = new ColumnOptions();
			columnOptions.Store.Remove(StandardColumn.Message);
			columnOptions.Store.Remove(StandardColumn.Properties);
			columnOptions.Store.Remove(StandardColumn.Exception);

			var seriLogConfig = new LoggerConfiguration()
										.WriteTo.MSSqlServer(connectionString: logConfig.ConnectionString, sinkOptions: sinkOpts,columnOptions:columnOptions)
										.CreateLogger();
			Logger = seriLogConfig;
		}
	}
}
