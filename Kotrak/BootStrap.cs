using Kotrak.View;
using Kotrak.ViewModel;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ViewManagement;

namespace Kotrak
{
   public static class BootStrap
    {

        private static UnityContainer _ioc;

        #region Methods

        internal static void Init(ContentControl windowContent)
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            Database.SetInitializer<DbModelContainer>(new CreateDatabaseIfNotExists<DbModelContainer>());

            InitIoCManager();
            InitLogger();
            InitViewManager(windowContent, null);
            InitServices();

            var viewManager = _ioc.Resolve<ViewManager>();
            viewManager.Init(windowContent);

            viewManager.OpenView<MainViewModel>();
        }

        private static void InitLogger()
        {
            _ioc.RegisterType<LogManager>(new ContainerControlledLifetimeManager());

            var logger = _ioc.Resolve<LogManager>();
            logger.Init(log4net.LogManager.GetLogger(typeof(LogManager)));
            logger.LogInfo("Application is starting.");
        }

        private static void InitServices()
        {
            _ioc.RegisterType<DbModelContainer>(new ContainerControlledLifetimeManager());
            _ioc.RegisterType<DatabaseManager>(new ContainerControlledLifetimeManager());
        }

        private static void RegisterViews(ViewManager viewManager)
        {
            viewManager.RegisterViewModel<MainViewModel, MainView>();
        }

        private static void InitIoCManager()
        {
            _ioc = new UnityContainer();
            _ioc.RegisterInstance(_ioc);

            var _iocManager = new IoCManager(_ioc);
            _ioc.RegisterInstance<IoCManager>(_iocManager);
        }

        private static void InitViewManager(ContentControl windowContent, ContentControl popUpContent)
        {
            _ioc.RegisterType<ViewManager>(new ContainerControlledLifetimeManager());

            var viewManager = _ioc.Resolve<ViewManager>();
            viewManager.Init(windowContent, popUpContent);
            RegisterViews(viewManager);
        }

        #endregion Methods
    }
}
