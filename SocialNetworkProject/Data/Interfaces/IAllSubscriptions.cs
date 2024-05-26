using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Interfaces
{
    public interface IAllSubscriptions
    {
        public bool CheckSubscription(string UserNickName, long VisitedUserId);
        public string Subscribe(string CurrentUserNickName, string UserNickname);
        public string Unsubscribe(string CurrentUserNickName, string UserNickname);
        public List<User> Subscriptions(string CurrentUserNickName);
    }
}
