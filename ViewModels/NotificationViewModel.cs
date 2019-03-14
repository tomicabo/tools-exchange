using Orodjarne.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Orodjarne.ViewModels
{
    class NotificationViewModel
    {
        //public ObservableCollection<NotificationModel> Notification
        //{
        //    get;
        //    set;
        //}

        private NotificationModel message;

        public NotificationViewModel()
        {
          
            message = new NotificationModel();
            if ((Application.Current as App).novo_sporocilo == 1)
                message.NovoSporocilo = "Novo Sporocilo";
            else if ((Application.Current as App).novo_sporocilo == 0)
                message.NovoSporocilo = "";
        }

        public NotificationModel Notification
        {
            get { return message; }
            set { message = value; }
        }
    }
}
