﻿using AskQuestion.Core.Infrastructure;
using AskQuestion.Core.Repository;
using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace AskQuestion.Service.Class
{
    public class BootStrapper
    {
        public static void RunConfig()
        {
            BuildAutoFac();
        }

        private static void BuildAutoFac()
        {

            var builder = new ContainerBuilder();

            builder.RegisterType<QuestionRepository>().As<IQuestionRepository>();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();

            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly); //Register WebApi Controllers

            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container); //Set the WebApi DependencyResolver

        }


    }
}