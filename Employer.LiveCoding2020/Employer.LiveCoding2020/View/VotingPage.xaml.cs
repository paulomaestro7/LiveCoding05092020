using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Employer.LiveCoding2020.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VotingPage : ContentPage
    {
        public VotingPage()
        {
            InitializeComponent();
            GC.Collect();
        }
    }
}