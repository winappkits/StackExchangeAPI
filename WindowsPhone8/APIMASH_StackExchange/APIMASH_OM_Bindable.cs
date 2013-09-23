﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace APIMASH_StackExchangeLib
{
    [Windows.Foundation.Metadata.WebHostHidden]
    public class APIMASH_OM_Bindable : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [Windows.Foundation.Metadata.WebHostHidden]
    public class QuestionGroup : APIMASH_OM_Bindable
    {
        public QuestionGroup(String id, String title)
        {
            this._id = id;
            this._title = title;

            Items.CollectionChanged += ItemsCollectionChanged;
        }

        private string _id = string.Empty;
        public string Id
        {
            get { return this._id; }
            set { this.SetProperty(ref this._id, value); }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value); }
        }

        public override string ToString()
        {
            return this.Title;
        }

        private void ItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex < 12)
                    {
                        TopItems.Insert(e.NewStartingIndex, Items[e.NewStartingIndex]);
                        if (TopItems.Count > 12)
                        {
                            TopItems.RemoveAt(12);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    if (e.OldStartingIndex < 12 && e.NewStartingIndex < 12)
                    {
                        TopItems.Move(e.OldStartingIndex, e.NewStartingIndex);
                    }
                    else if (e.OldStartingIndex < 12)
                    {
                        TopItems.RemoveAt(e.OldStartingIndex);
                        TopItems.Add(Items[11]);
                    }
                    else if (e.NewStartingIndex < 12)
                    {
                        TopItems.Insert(e.NewStartingIndex, Items[e.NewStartingIndex]);
                        TopItems.RemoveAt(12);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex < 12)
                    {
                        TopItems.RemoveAt(e.OldStartingIndex);
                        if (Items.Count >= 12)
                        {
                            TopItems.Add(Items[11]);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldStartingIndex < 12)
                    {
                        TopItems[e.OldStartingIndex] = Items[e.OldStartingIndex];
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    TopItems.Clear();
                    while (TopItems.Count < Items.Count && TopItems.Count < 12)
                    {
                        TopItems.Add(Items[TopItems.Count]);
                    }
                    break;
            }
        }

        private ObservableCollection<QuestionItem> _items = new ObservableCollection<QuestionItem>();
        public ObservableCollection<QuestionItem> Items
        {
            get { return this._items; }
        }

        private ObservableCollection<QuestionItem> _topItem = new ObservableCollection<QuestionItem>();
        public ObservableCollection<QuestionItem> TopItems
        {
            get { return this._topItem; }
        }

    }

    public class QuestionItem : APIMASH_OM_Bindable
    {

        public QuestionItem(Question question)
        {
            this.id = question.Id;
            this.title = question.Title;
            this.voteCount = question.UpVoteCount - question.DownVoteCount;
            this.answerCount = question.AnswerCount;
            this.viewCount = question.ViewCount;
            this.description = WebUtility.HtmlDecode(Regex.Replace(question.Body, "<[^>]*(>|$)", string.Empty));
            this.body = question.Body;
            this.owner = new UserItem(question.Owner);
            this.url = question.Url;

            this.tags = new List<string>(question.Tags);
            if (null != question.Comments && question.Comments.Count != 0)
            {
                foreach (var comment in question.Comments)
                {
                    this.comments.Add(new CommentItem(comment));
                }
            }
        }

        private int id = 0;
        public int Id
        {
            get { return this.id; }
            set { this.SetProperty(ref this.id, value); }
        }

        private string title = string.Empty;
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        private string url = string.Empty;
        public string Url
        {
            get { return this.url; }
            set { this.SetProperty(ref this.url, value); }
        }

        private string description = string.Empty;
        public string Description
        {
            get { return this.description; }
            set { this.SetProperty(ref this.description, value); }
        }

        private string body = string.Empty;
        public string Body
        {
            get { return this.body; }
            set { this.SetProperty(ref this.body, value); }
        }

        private int voteCount = 0;
        public int VoteCount
        {
            get { return this.voteCount; }
            set { this.SetProperty(ref this.voteCount, value); }

        }

        private int answerCount = 0;
        public int AnswerCount
        {
            get { return this.answerCount; }
            set { this.SetProperty(ref this.answerCount, value); }
        }

        private int viewCount = 0;
        public int ViewCount
        {
            get { return this.viewCount; }
            set { this.SetProperty(ref this.viewCount, value); }
        }

        private UserItem owner = null;
        public UserItem Owner 
        { 
            get { return owner; }
            set { this.SetProperty(ref this.owner, value); }
        }

        private List<string> tags = new List<string>();
        public List<string> Tags
        {
            get { return this.tags; }
            set { this.SetProperty(ref this.tags, value); }
        }

        private List<CommentItem> comments = new List<CommentItem>();
        public List<CommentItem> Comments
        {
            get { return this.comments; }
            set { this.SetProperty(ref this.comments, value);  }
        }
    }

    public class QuestionDetailItem : APIMASH_OM_Bindable
    {

    }

    public class AnswerItem : APIMASH_OM_Bindable
    {

    }

    public class CommentItem : APIMASH_OM_Bindable
    {
        public CommentItem(Comment comment)
        {
            this.id = comment.Id;
            this.body = comment.Body;
        }

        private int id = 0;
        public int Id
        {
            get { return this.id; }
            set { this.SetProperty(ref this.id, value); }
        }

        private string body = string.Empty;
        public string Body
        {
            get { return this.body; }
            set { this.SetProperty(ref this.body, value); }
        }
    }

    public class TagItem : APIMASH_OM_Bindable
    {

    }

    public class UserItem : APIMASH_OM_Bindable
    {
        public UserItem(User user)
        {
            this.id = user.Id;
            this.title = user.DisplayName;
            this.reputation = user.Reputation;
            this.profileImageUrl = user.ProfileImage;
            this.acceptanceRate = user.Reputation;
        }

        private int id = 0;
        public int Id
        {
            get { return this.id; }
            set { this.SetProperty(ref this.id, value); }
        }

        private string title = string.Empty;
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        private int reputation = 0;
        public int Reputation
        {
            get { return this.reputation; }
            set { this.SetProperty(ref this.reputation, value); }
        }

        private string profileImageUrl = string.Empty;
        public string ProfileImageUrl
        {
            get { return this.profileImageUrl; }
            set { this.SetProperty(ref this.profileImageUrl, value); }
        }

        private int acceptanceRate = 0;
        public int AcceptanceRate
        {
            get { return this.acceptanceRate; }
            set { this.SetProperty(ref this.acceptanceRate, value); }
        }

    }
}
