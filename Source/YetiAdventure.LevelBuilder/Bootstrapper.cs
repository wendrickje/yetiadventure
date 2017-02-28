﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using Microsoft.Xna.Framework.Graphics;
using Prism;
using Prism.Unity;
using YetiAdventure.Engine;
using YetiAdventure.LevelBuilder.Common;
using YetiAdventure.LevelBuilder.Controls;
using YetiAdventure.Shared.Interfaces;

namespace YetiAdventure.LevelBuilder
{
    /// <summary>
    /// boot strapper
    /// </summary>
    /// <seealso cref="Prism.Unity.UnityBootstrapper" />
    public class Bootstrapper : UnityBootstrapper
    {
        /// <summary>
        /// Configures the container.
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

        }

        /// <summary>
        /// Creates the shell.
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        protected override void InitializeShell()
        {
            Application.Current.MainWindow = Shell as Window;
            Application.Current.MainWindow.Show();
        }
    }
}