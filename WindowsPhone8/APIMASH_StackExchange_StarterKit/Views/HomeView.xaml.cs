using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace APIMASH_StackExchange_StarterKit.Views
{
    public partial class HomeView : PhoneApplicationPage
    {
        public HomeView()
        {
            InitializeComponent();
        }
        protected override void OnDoubleTap(System.Windows.Input.GestureEventArgs e)
        {
            MessageBox.Show(this.Items.Items.Count.ToString());
            base.OnDoubleTap(e);
        }
    }
}