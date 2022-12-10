using System;
using c_sharp_angular.Entities;
using c_sharp_angular.Helpers;

namespace c_sharp_angular.Interfaces
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        void DeleteMessage(Message message);

        Task<Message> GetMessage(int id);

        Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams);

        Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserName,
            string recipientName);

        Task<bool> SaveAllAsync();


    }
}

