using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _04_StationeryCompany.Components
{
    /// <summary>
    /// Interaction logic for ActionButton.xaml
    /// </summary>
    public partial class ActionButton : Component
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ActionButton));
        private ICommand _command;
        public ICommand Command
        {
            get { return _command; }
            set
            {
                _command = value;
                OnPropertyChanged(nameof(Command));
            }
        }

        public static readonly DependencyProperty SvgPathProperty = DependencyProperty.Register("SvgPath", typeof(string), typeof(ActionButton));
        private string _svgPath;
        public string SvgPath
        {
            get { return this._svgPath; }
            set
            {
                _svgPath = value;
                OnPropertyChanged(nameof(SvgPath));
            }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ActionButton));
        public string _text;
        public string Text
        {
            get { return this._text; }
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public static readonly DependencyProperty ToolTipProperty = DependencyProperty.Register("ToolTip", typeof(string), typeof(ActionButton));
        public string _toolTip;
        public string ToolTip
        {
            get { return this._toolTip; }
            set
            {
                _toolTip = value;
                OnPropertyChanged(nameof(ToolTip));
            }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(ActionButton));
        private object _commandParametr;
        public object CommandParameter
        {
            get { return this._commandParametr; }
            set
            {
                _commandParametr = value;
                OnPropertyChanged(nameof(CommandParameter));
            }
        }

        public ActionButton()
        {
            InitializeComponent();
        }
    }
}
