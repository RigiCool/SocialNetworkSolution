using SocialNetworkProject.Data.Interfaces;
using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Repository
{
    public class SubscriptionRepository : IAllSubscriptions
    {
        private AppDBContent appDBContent;

        public SubscriptionRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }

        public bool CheckSubscription(string CurrentUserNickName, long VisitedUserId)
        {
            User CurrentUser = appDBContent.User.FirstOrDefault(u => u.Nickname == CurrentUserNickName);
            Subscription subscription = appDBContent.Subscription.FirstOrDefault(s => s.UserId == VisitedUserId && s.OwnerId == CurrentUser.Id);
            if (subscription != null){
                return true;
            }
            else
            {
                return false;
            }
        }
        public string Subscribe(string CurrentUserNickName, string UserNickname)
        {
            User CurrentUser = appDBContent.User.FirstOrDefault(u => u.Nickname == CurrentUserNickName);
            User User = appDBContent.User.FirstOrDefault(u => u.Nickname == UserNickname);
            appDBContent.Subscription.Add(new Subscription { OwnerId = CurrentUser.Id, UserId = User.Id });
            appDBContent.SaveChanges();
            return UserNickname;
        }

        public List<User> Subscriptions(string CurrentUserNickName)
        {
            User currentUser = appDBContent.User.FirstOrDefault(u => u.Nickname == CurrentUserNickName);

            if (currentUser == null)
            {
                return new List<User>(); // Or handle the case where currentUser is not found
            }

            List<User> users = (
                from subscription in appDBContent.Subscription
                where subscription.OwnerId == currentUser.Id
                join user in appDBContent.User on subscription.UserId equals user.Id
                select user
            ).ToList();

            return users;
        }

        public string Unsubscribe(string CurrentUserNickName, string UserNickname)
        {
            User CurrentUser = appDBContent.User.FirstOrDefault(cu => cu.Nickname == CurrentUserNickName);
            User User = appDBContent.User.FirstOrDefault(u => u.Nickname == UserNickname);
            Subscription subscription=appDBContent.Subscription.FirstOrDefault(s => s.UserId == User.Id && s.OwnerId==CurrentUser.Id);
            appDBContent.Subscription.Remove(subscription);
            appDBContent.SaveChanges();
            return UserNickname;
        }
    }
}
