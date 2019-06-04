using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using StructureMap;

namespace TaskManager.App_Start
{
    public class HttpControllerActivatorFactory : IHttpControllerActivator
    {
        public HttpControllerActivatorFactory(HttpConfiguration configuration)
        {

        }
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            if (request == null || controllerType == null) return null;
            return ObjectFactory.Container.GetInstance(controllerType) as IHttpController;
        }
    }

    public static class ObjectFactory
    {
        static readonly Lazy<Container> ContainerBuilder = new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container
        {
            get { return ContainerBuilder.Value; }

        }
        private static Container DefaultContainer()
        {
            return new Container();
        }
    }
}