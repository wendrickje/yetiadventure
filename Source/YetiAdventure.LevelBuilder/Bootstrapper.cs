using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism;
using Prism.Unity;
using YetiAdventure.Shared.Interfaces;

namespace YetiAdventure.LevelBuilder
{
    public class Bootstrapper : UnityBootstrapper
    {

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            //.RegisterType<ILevelBuilderContext>();
            Container.RegisterInstance<ILevelBuilderContext>(new Engine.Levels.LevelBuilderContext());
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = Shell as Window;
            Application.Current.MainWindow.Show();
        }
    }
}
