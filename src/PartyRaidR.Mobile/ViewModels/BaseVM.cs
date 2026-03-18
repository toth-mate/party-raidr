using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyRaidR.Mobile.ViewModels
{
    public abstract class BaseVM : ObservableObject
    {
        protected virtual Task OnInitialized()
        {
            return Task.CompletedTask;
        }
    }
}
