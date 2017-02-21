using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prism.Regions;
using YetiAdventure.LevelBuilder.Common;
using YetiAdventure.LevelBuilder.View;

namespace YetiAdventure.LevelBuilder.ViewModel
{
    public class MainWindowViewModel : CoreViewModel
    {

        IRegionManager _regionManager;
        public const string MainRegionName = "MainRegion";

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            var region = _regionManager.RegisterViewWithRegion(MainRegionName, typeof(LevelCanvasView));
            region.RegisterViewWithRegion(ToolBoxViewModel.RegionName, typeof(ToolBoxView));
        }

        #region newCommand
        private ICommand _newCommand;
        /// <summary>
        /// create new project
        /// </summary>
        public ICommand NewCommand
        {
            get { return _newCommand ?? (_newCommand = new RelayCommand(DoCreateNew, CanCreateNew)); }
        }

        /// <summary>
        /// create new project implementation
        /// </summary>
        /// <param name="param">command parameter</param>
        private void DoCreateNew(object param)
        {
            //first save if there is an active project
            if (CanSave(null)) DoSave(null);
            //start fresh

            //var newprojectwindow = new View.NewProjectWindow();
            //App.Current.Resources.MergedDictionaries.ToList().ForEach(newprojectwindow.Resources.MergedDictionaries.Add);
            //var result = newprojectwindow.ShowDialog();
            //if (!result.HasValue || !result.Value) return;

            //var tilesize = newprojectwindow.TileSize;
            //var canvasheight = newprojectwindow.CanvasHeight;
            //var canvaswidth = newprojectwindow.CanvasWidth;

            //_project = new Project(newprojectwindow.ProjectName, canvasheight / tilesize, canvaswidth / tilesize, tilesize, newprojectwindow.TileSheetResource);
            //OnPropertyChanged("Project");
            //newprojectwindow = null;
            
            _regionManager.RequestNavigate(MainRegionName, typeof(LevelCanvasView).ToString());
        }

        /// <summary>
        /// can create new project
        /// </summary>
        /// <param name="param">command parameter</param>
        /// <returns>True if can create project</returns>
        private bool CanCreateNew(object param)
        {
            return true;
        }
        #endregion

        #region SaveCommand
        private ICommand _saveCommand;
        /// <summary>
        /// save project command
        /// </summary>
        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(DoSave, CanSave)); }
        }

        /// <summary>
        /// save project implementation
        /// </summary>
        /// <param name="param">command parameter</param>
        private void DoSave(object param)
        {
            ////save level manager (.lev.config) file
            ////update active project
            //Save(Project.LevelConfiguration.Name);




        }

        /// <summary>
        /// can save project
        /// </summary>
        /// <param name="param">command parameter</param>
        /// <returns>True if can save project</returns>
        private bool CanSave(object param)
        {
            return false;// Project != null;
        }
        #endregion

        #region OpenCommand
        private ICommand _openCommand;
        /// <summary>
        /// Open project command
        /// </summary>
        public ICommand OpenCommand
        {
            get { return _openCommand ?? (_openCommand = new RelayCommand(DoOpen, CanOpen)); }
        }

        /// <summary>
        /// Open project implementation
        /// </summary>
        /// <param name="param">command parameter</param>
        private void DoOpen(object param)
        { 



        }

        /// <summary>
        /// can Open project
        /// </summary>
        /// <param name="param">command parameter</param>
        /// <returns>True if can Open project</returns>
        private bool CanOpen(object param)
        {
            return false;// Project != null;
        }
        #endregion

        #region CloseCommand
        private ICommand _closeCommand;
        /// <summary>
        /// Close project command
        /// </summary>
        public ICommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RelayCommand(DoClose, CanClose)); }
        }

        /// <summary>
        /// Close project implementation
        /// </summary>
        /// <param name="param">command parameter</param>
        private void DoClose(object param)
        {



        }

        /// <summary>
        /// can Close project
        /// </summary>
        /// <param name="param">command parameter</param>
        /// <returns>True if can Close project</returns>
        private bool CanClose(object param)
        {
            return false;// Project != null;
        }
        #endregion

        #region ExitCommand
        private ICommand _exitCommand;
        /// <summary>
        /// Exit project command
        /// </summary>
        public ICommand ExitCommand
        {
            get { return _exitCommand ?? (_exitCommand = new RelayCommand(DoExit, CanExit)); }
        }

        /// <summary>
        /// Exit implementation
        /// </summary>
        /// <param name="param">command parameter</param>
        private void DoExit(object param)
        {

            Application.Current.Shutdown();


        }

        /// <summary>
        /// can Exit 
        /// </summary>
        /// <param name="param">command parameter</param>
        /// <returns>True if can Exit </returns>
        private bool CanExit(object param)
        {
            return true;
        }
        #endregion

    }
}
