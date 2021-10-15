using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.SeriLog;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Core.CrossCuttingConcerns.Logging;
using Core.Exceptions;
using System.Linq;
using System.Security.Claims;

namespace Core.Aspects.Autofac.Exception
{
    public class ExceptionLogAspect : MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ExceptionLogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new ArgumentException("Wrongloggertype");
            }

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnException(IInvocation invocation, System.Exception e)
        {
            var errorlog = GetLogDetail(invocation, e);
            _loggerServiceBase.Error(errorlog);
        }

        private string GetLogDetail(IInvocation invocation, System.Exception e)
        {
            if (e.InnerException is ApiException || e.GetType() == typeof(ApiException))
            {
                var ex = e.InnerException != null ? (ApiException)e.InnerException : (ApiException)e;
                var errorlogDetail = new ErrorLog
                {
                    Errors = ex.Errors,
                    MethodName = invocation.Method.Name,
                    ManagerName = invocation.MethodInvocationTarget.ReflectedType.Name,
                    UserId = (_httpContextAccessor?.HttpContext == null || _httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value == null) ? "?" : _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    Username = (_httpContextAccessor?.HttpContext == null || _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value == null) ? "?" : _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
                };
                return JsonConvert.SerializeObject(errorlogDetail);
            }
            else
            {
                List<string> exceptions = new List<string>();
                exceptions.Add(e.Message);
                var errorlogDetail = new ErrorLog
                {
                    Errors = exceptions,
                    MethodName = invocation.Method.Name,
                    ManagerName = invocation.MethodInvocationTarget.ReflectedType.Name,
                    UserId = (_httpContextAccessor?.HttpContext == null || _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value == null) ? "?" : _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    Username = (_httpContextAccessor?.HttpContext == null || _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value == null) ? "?" : _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
                };
                return JsonConvert.SerializeObject(errorlogDetail);
            }
        }
    }
}
