using NHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using Store.Business.Layer;
using Store.Business.Layer.RepositoryInterfaces;
using Store.Infraestructure.Layer.NHibernateMaps;
using Store.Infraestructure.Layer.Repositories;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using SharpArch.Domain.DomainModel;

namespace Store.WebApi
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
        {
            try
            {
                var autoPersistenceModel = AutoMap.AssemblyOf<Category>(new AutomappingConfiguration()).UseOverridesFromAssemblyOf<OrderMap>();
                autoPersistenceModel.IgnoreBase<Entity>();
                autoPersistenceModel.IgnoreBase(typeof(EntityWithTypedId<>));
                var sessionFactory = Fluently.Configure()
                    //.ExposeConfiguration(x => x.SetInterceptor(new SqlStatementInterceptor()))
                    .Database(MySQLConfiguration.Standard.ConnectionString(connectionString).ShowSql())
                    .Mappings(m => 
                    {
                        m.AutoMappings.Add(autoPersistenceModel);
                    })
                    .BuildSessionFactory();

                var assemblyInfra = Assembly.Load(new AssemblyName("Store.Infraestructure.Layer"));
                var assemblyLogic = Assembly.Load(new AssemblyName("Store.Logic.Layer"));

                services.AddSingleton(sessionFactory);
                services.AddScoped(factory => sessionFactory.OpenSession());
                services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                services.AddScoped(typeof(IItemRepository<>), typeof(ItemRepository<>));

                RegisterScopedServices(services, assemblyInfra, "Repository");
                RegisterScopedServices(services, assemblyLogic, "Logic");
            }
            catch (Exception ex)
            {

            }
            return services;
        }

        static void RegisterScopedServices(IServiceCollection services, Assembly assembly, string subfix)
        {
            foreach (var type in assembly.ExportedTypes.Where(e => e.Name.EndsWith(subfix)))
            {
                var faces = type.GetInterfaces().Where(i => i.Name.EndsWith(subfix)).ToList();
                foreach (var face in faces)
                {
                    services.AddScoped(face, type);
                }
            }
        }
    }

    public class SqlStatementInterceptor : EmptyInterceptor
    {
        public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            Trace.WriteLine("NHibernate SQL: " + sql.ToString());
            return sql;
        }
    }
}
