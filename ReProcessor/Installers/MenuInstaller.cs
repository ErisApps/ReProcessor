﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using ReProcessor.Managers;

namespace ReProcessor.Installers
{
    class MenuInstaller : Installer
    {
        private static MenuInstaller Instance;
        public override void InstallBindings()
        {
            Instance = this;
            Plugin.Log.Notice("Binding Menu Camera Manager");
            Container.Bind(typeof(IInitializable), typeof(MenuCoreManager)).To<MenuCoreManager>().AsSingle();
        }
        public static void UninstallBindings()
        {
            Plugin.Log.Notice("Uninding Menu Camera Manager");
            Instance.Container.Bind(typeof(IInitializable), typeof(MenuCoreManager)).To<MenuCoreManager>().AsSingle();
        }
    }
}
