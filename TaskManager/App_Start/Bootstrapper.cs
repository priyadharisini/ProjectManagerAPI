using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TM.Data;

namespace TaskManager.App_Start
{
    public class Bootstrapper
    {
        public static void Configure()
        {
            ObjectFactory.Container.Configure(x =>
            {
                x.AddRegistry<ServicesRegistry>();
            });

            var log = ObjectFactory.Container.WhatDoIHave();
        }
    }
    public class ServicesRegistry : StructureMap.Registry
    {
        public ServicesRegistry()
        {
            Scan(x =>
            {
                x.Assembly("TM.Business");                
                x.WithDefaultConventions();
            });

            For(typeof(IRepository<>)).Use(typeof(Repository<>));
        }
    }
}