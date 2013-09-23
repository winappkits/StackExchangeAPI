using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using APIMASHLib;
using APIMASH_StackExchangeLib;
using APIMASH_StackExchange_StarterKit.Resources;
using Caliburn.Micro;

namespace APIMASH_StackExchange_StarterKit.ViewModels
{
    public class HomeViewModel : Screen
    {
        private readonly INavigationService navigationService;

        public HomeViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;    DisplayName = "Home";
            var api = new APIMASHInvoke();
            api.OnResponse += api_OnResponse;
            api.Invoke<APIMASH_StackExchangeLib.StackExchangeQuestions>(Constants.QuestionsApi);
        }

        void api_OnResponse(object sender, APIMASHEvent e)
        {
            StackExchangeQuestions response = (StackExchangeQuestions)e.Object;

            if (e.Status == APIMASHStatus.SUCCESS)
            {
                // copy data into bindable format for UI
                APIMASH_StackExchangeCollection.Copy(response, System.Guid.NewGuid().ToString(), "All");
                Items = APIMASH_StackExchangeCollection.GetGroups("AllGroups").FirstOrDefault().Items;
            }
        }

        private ObservableCollection<QuestionItem> items = new ObservableCollection<QuestionItem>();

        public ObservableCollection<QuestionItem> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                NotifyOfPropertyChange(() => Items);
            }

        }

        private QuestionItem selectedItem;

        public QuestionItem SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
                navigationService.UriFor<QuestionDetailViewModel>().WithParam(x => x.ItemId, SelectedItem.Id.ToString()).Navigate();
            }
        }

    }
}