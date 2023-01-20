using Ninject;
using Ninject.Modules;
using wxDCS_Injector.Presentation;
using wxDCS_Injector.Service;

namespace wxDCS_Injector
{
    static class DI
    {
        static IKernel _kernel;

        public static void Wire(INinjectModule module) => _kernel = new StandardKernel(module);

        public static T Resolve<T>() => _kernel.Get<T>();
    }

    class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IMetarService>().To<MetarService>();
            Bind<IInjectService>().To<InjectService>();
            Bind<ILog>().To<frmLog>().InSingletonScope();
        }
    }
}
