using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YetiAdventure.LevelBuilder.Common;
using YetiAdventure.LevelBuilder.Interfaces;
using YetiAdventure.Shared.Icons;
using Prism.Events;
using YetiAdventure.Shared.Events;
using YetiAdventure.Shared;
using YetiAdventure.Shared.Interfaces;
using Microsoft.Practices.Unity;
using System.Windows;
using YetiAdventure.Shared.Models;
using YetiAdventure.Shared.Common;

namespace YetiAdventure.LevelBuilder.ViewModel
{
    /// <summary>
    /// tool box view model
    /// </summary>
    /// <seealso cref="YetiAdventure.LevelBuilder.ViewModel.CoreViewModel" />
    public class ToolBoxViewModel : CoreViewModel
    {
        public const string RegionName = "ToolBoxViewModel";
        ILevelBuilderService _levelBuilderService;
        IUnityContainer _container;
        IEventAggregator _eventManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolBoxViewModel"/> class.
        /// </summary>
        /// <param name="eventManager">The event manager.</param>
        /// <param name="container">The container.</param>
        /// <param name="levelBuilder">The level builder.</param>
        public ToolBoxViewModel(IEventAggregator eventManager, IUnityContainer container, ILevelBuilderService levelBuilder)
        {
            _eventManager = eventManager;
            _eventManager.GetEvent<SelectionChangedEvent>().Subscribe(args => { SelectedPrimitive = args.NewItem; });
            _container = container;
            _levelBuilderService = levelBuilder;
            _toolBoxItems = new ObservableCollection<IToolboxItem>(GetToolBoxItems());
            ActiveToolBoxItem = _toolBoxItems.First();
        }

        /// <summary>
        /// Gets the level builder service.
        /// </summary>
        /// <value>
        /// The level builder service.
        /// </value>
        protected ILevelBuilderService LevelBuilderService
        {
            get
            {
                return _levelBuilderService;
            }
        }


        private ObservableCollection<IToolboxItem> _toolBoxItems;

        /// <summary>
        /// Gets the tool box items.
        /// </summary>
        /// <value>
        /// The tool box items.
        /// </value>
        public ObservableCollection<IToolboxItem> ToolBoxItems
        {
            get
            {
                return _toolBoxItems;
            }
        }

        /// <summary>
        /// Gets the tool box items.
        /// </summary>
        /// <returns></returns>
        private List<IToolboxItem> GetToolBoxItems()
        {
            var items = new List<IToolboxItem>();
            //cursor tool
            items.Add(new ToolBoxItemViewModel(Resources.Constants.Controls_ToolboxItem_Cursor_Caption,
                Resources.Constants.Controls_ToolboxItem_Cursor_Tooltip,
                IconType.Cursor,
                UseCursorCommand));

            //polygon tool
            items.Add(new ToolBoxItemViewModel(Resources.Constants.Controls_ToolboxItem_Polygon_Caption,
                Resources.Constants.Controls_ToolboxItem_Polygon_Tooltip,
                IconType.Polygon,
                DrawPolygonCommand));

            //selection-move tool
            items.Add(new ToolBoxItemViewModel(Resources.Constants.Controls_ToolboxItem_Transform_Caption,
                Resources.Constants.Controls_ToolboxItem_Transform_Tooltip,
                IconType.Transform,
                TransformCommand));

            return items;
        }

        IToolboxItem _activeToolBoxItem;

        /// <summary>
        /// Gets or sets the active tool box item.
        /// </summary>
        /// <value>
        /// The active tool box item.
        /// </value>
        public IToolboxItem ActiveToolBoxItem
        {
            get { return _activeToolBoxItem; }
            set { SetProperty(ref _activeToolBoxItem, value); }
        }

        private Primitive _selectedPrimitive;

        /// <summary>
        /// selected primitive object within game builder context
        /// </summary>
        public Primitive SelectedPrimitive
        {
            get { return _selectedPrimitive; }
            set
            {
                _selectedPrimitive = value;
                SetProperty(ref _selectedPrimitive, value);
            }
        }



        #region transform command
        private ICommand _transformCommand;

        /// <summary>
        /// Gets the transform command.
        /// </summary>
        /// <value>
        /// The transform command.
        /// </value>
        public ICommand TransformCommand
        {
            get { return _transformCommand ?? (_transformCommand = new RelayCommand(DoTransform, CanTransform)); }
        }

        /// <summary>
        /// Determines whether this instance can transform the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///   <c>true</c> if this instance can transform the specified object; otherwise, <c>false</c>.
        /// </returns>
        private bool CanTransform(object obj)
        {
            return true;
        }

        /// <summary>
        /// Does the transform.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void DoTransform(object obj)
        {
            LevelBuilderService.SetActiveLevelBuilderTool(LevelBuilderTool.Transform);

        }
        #endregion

        #region polygon command
        private ICommand _drawPolygonCommand;

        /// <summary>
        /// Gets the draw polygon command.
        /// </summary>
        /// <value>
        /// The draw polygon command.
        /// </value>
        public ICommand DrawPolygonCommand
        {
            get { return _drawPolygonCommand ?? (_drawPolygonCommand = new RelayCommand(DoDrawPolygon, CanDrawPolygon)); }
        }

        /// <summary>
        /// Determines whether this instance [can draw polygon] the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can draw polygon] the specified object; otherwise, <c>false</c>.
        /// </returns>
        private bool CanDrawPolygon(object obj)
        {
            return true;
        }

        /// <summary>
        /// Does the draw polygon.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void DoDrawPolygon(object obj)
        {
            LevelBuilderService.SetActiveLevelBuilderTool(LevelBuilderTool.DrawPolygon);
        }
        #endregion

        #region Cursor command
        private ICommand _useCursorCommand;

        /// <summary>
        /// Gets the use cursor command.
        /// </summary>
        /// <value>
        /// The use cursor command.
        /// </value>
        public ICommand UseCursorCommand
        {
            get { return _useCursorCommand ?? (_useCursorCommand = new RelayCommand(DoUseCursor, CanUseCursor)); }
        }

        /// <summary>
        /// Determines whether this instance [can use cursor] the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can use cursor] the specified object; otherwise, <c>false</c>.
        /// </returns>
        private bool CanUseCursor(object obj)
        {
            return true;
        }

        /// <summary>
        /// Does the use cursor.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void DoUseCursor(object obj)
        {
            LevelBuilderService.SetActiveLevelBuilderTool(LevelBuilderTool.Transform);
        }
        #endregion
    }
}
