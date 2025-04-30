using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonsultationApp.Models;
using KonsultationApp.Services;
using SQLite;



namespace KonsultationApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentPage : ContentPage
    {
        public StudentPage(StudentViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}