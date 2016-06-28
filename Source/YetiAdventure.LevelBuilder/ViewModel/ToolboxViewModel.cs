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

namespace YetiAdventure.LevelBuilder.ViewModel
{
    public class ToolboxViewModel : CoreViewModel
    {
        public const string RegionName = "ToolboxViewModel";
        ILevelBuilderContext _builderContext;
        public ToolboxViewModel(IEventAggregator eventManager, ILevelBuilderContext builderContext)
        {
            eventManager.GetEvent<SelectionChangedEvent>().Subscribe(args => { SelectedPrimitive = args.NewItem; });
            _builderContext = builderContext;
            

        }

        private ObservableCollection<IToolboxItem> _toolboxItems;

        public ObservableCollection<IToolboxItem> ToolboxItems
        {
            get
            {
                return _toolboxItems ?? (_toolboxItems = new ObservableCollection<IToolboxItem>(GetToolboxItems()));
            }
        }

        private List<IToolboxItem> GetToolboxItems()
        {
            var items = new List<IToolboxItem>();
            //cursor tool
            items.Add(new ToolboxItemViewModel(Resources.Constants.Controls_ToolboxItem_Cursor_Caption,
                Resources.Constants.Controls_ToolboxItem_Cursor_Tooltip,
                IconType.Cursor,
                UseCursorCommand));

            //polygon tool
            items.Add(new ToolboxItemViewModel(Resources.Constants.Controls_ToolboxItem_Polygon_Caption,
                Resources.Constants.Controls_ToolboxItem_Polygon_Tooltip,
                IconType.Polygon,
                DrawPolygonCommand));

            //selection-move tool
            items.Add(new ToolboxItemViewModel(Resources.Constants.Controls_ToolboxItem_SelectionMove_Caption,
                Resources.Constants.Controls_ToolboxItem_SelectionMove_Tooltip,
                IconType.MoveSelection,
                MoveSelectionCommand));

            return items;
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



        #region move command
        private ICommand _moveSelectionCommand;

        public ICommand MoveSelectionCommand
        {
            get { return _moveSelectionCommand ?? (_moveSelectionCommand = new RelayCommand(DoMoveSelection, CanMoveSelection)); }
        }

        private bool CanMoveSelection(object obj)
        {
            return true;
            //return SelectedPrimitive != null;
        }

        private void DoMoveSelection(object obj)
        {
            _builderContext.MovePrimitive(SelectedPrimitive ?? new Primitive(), 10, 23);
        }
        #endregion

        #region polygon command
        private ICommand _drawPolygonCommand;

        public ICommand DrawPolygonCommand
        {
            get { return _drawPolygonCommand ?? (_drawPolygonCommand = new RelayCommand(DoDrawPolygon, CanDrawPolygon)); }
        }

        private bool CanDrawPolygon(object obj)
        {
            return true;
        }

        private void DoDrawPolygon(object obj)
        {
        }
        #endregion

        #region Cursor command
        private ICommand _useCursorCommand;

        public ICommand UseCursorCommand
        {
            get { return _useCursorCommand ?? (_useCursorCommand = new RelayCommand(DoUseCursor, CanUseCursor)); }
        }

        private bool CanUseCursor(object obj)
        {
            return true;
        }

        private void DoUseCursor(object obj)
        {
        }
        #endregion
    }
}
