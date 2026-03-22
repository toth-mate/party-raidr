using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyRaidR.Mobile.ViewModels
{
    public abstract partial class BaseVM : ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy;

        protected BaseVM()
        {
            IsBusy = false;
        }

        protected virtual Task OnInitialized()
        {
            return Task.CompletedTask;
        }
    }
}
