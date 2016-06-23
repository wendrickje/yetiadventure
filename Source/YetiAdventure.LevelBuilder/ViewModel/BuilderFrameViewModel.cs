using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YetiAdventure.LevelBuilder.Common;
using YetiAdventure.LevelBuilder.Navigation;
using YetiAdventure.LevelBuilder.View;

namespace YetiAdventure.LevelBuilder.ViewModel
{
    public class BuilderFrameViewModel : CoreViewModel
    {

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
            //Navigator.Navigate(new NavigationData<ProjectPage>(Project));
            //newprojectwindow = null;
            Navigator.Navigate(new NavigationData<LevelCanvasPage>(0));
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
            return true;// Project != null;
        }
        #endregion

    }
}
