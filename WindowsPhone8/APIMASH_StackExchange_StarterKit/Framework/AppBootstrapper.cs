using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using APIMASH_StackExchange_StarterKit.ViewModels;
using Caliburn.Micro;
using Microsoft.Phone.Controls;

namespace APIMASH_StackExchange_StarterKit.Framework
{
    public class AppBootstrapper : PhoneBootstrapper
    {
        PhoneContainer _container;

        protected override void Configure()
        {
            _container = new PhoneContainer();

            if (!Execute.InDesignMode)
                _container.RegisterPhoneServices(RootFrame);

            _container.PerRequest<HomeViewModel>();
            _container.PerRequest<QuestionDetailViewModel>();
            
        }

        protected override void OnUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
                e.Handled = true;
            }
            else
            {
                MessageBox.Show("An unexpected error occured, sorry about the troubles.", "Oops...", MessageBoxButton.OK);
                e.Handled = true;
            }

            base.OnUnhandledException(sender, e);
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
