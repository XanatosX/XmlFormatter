using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;

namespace XmlFormatterOsIndependent.MVVM.Views.Popups
{
    [Obsolete]
    public partial class AddHotfolderPopup : UserControl
    {
        public AddHotfolderPopup()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
