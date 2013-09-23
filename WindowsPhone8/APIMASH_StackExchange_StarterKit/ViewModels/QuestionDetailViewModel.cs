using APIMASH_StackExchangeLib;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIMASH_StackExchange_StarterKit.ViewModels
{
    public class QuestionDetailViewModel : Screen
    {
        public QuestionDetailViewModel()
        {
        }

        private string _itemId;
        public string ItemId { get { return _itemId; }
            set
            {
                _itemId = value;
                NotifyOfPropertyChange(() => ItemId);
                LoadItem(Int32.Parse(_itemId));
            }
        }

        private QuestionItem _item;
        public QuestionItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
                NotifyOfPropertyChange(() => Item);

            }
        }

        private void LoadItem(int itemId)
        {
            var group = APIMASH_StackExchangeCollection.GetGroupByTitle("All");
            Item = APIMASH_StackExchangeCollection.GetItem(itemId);
            DisplayName = "";
        }
    }
}
