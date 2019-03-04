using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    class Program
    {
        /// <summary>
        /// Mediator class.
        /// </summary>
        interface IChatRoom
        {
            void Add(Participant participant);

            void SendMessage(string fromUser, string toUser, string message);
            void SendBulkMessage(string fromUser, string message);
        }

        class ChatRoom : IChatRoom
        {
            public void Add(Participant participant)
            {
                if (!_participants.ContainsKey(participant.Name))
                {
                    _participants.Add(participant.Name, participant);
                    participant.EnterChatRoom(this);
                }
            }

            public void SendMessage(string fromUser, string toUser, string message)
            {
                if (_participants.ContainsKey(toUser))
                {
                    _participants[toUser].Receive(fromUser, message);
                }
            }

            public void SendBulkMessage(string fromUser, string message)
            {
                foreach (var participant in _participants.Values)
                {
                    if (participant.Name != fromUser)
                    {
                        participant.Receive(fromUser, message);
                    }
                }
            }

            private Dictionary<string, Participant> _participants = new Dictionary<string, Participant>();
        }

        interface IParticipant
        {
            void Send(string toUser, string message);
            void SendBulk(string message);
            void Receive(string fromUser, string message);
        }

        class Participant : IParticipant
        {
            public Participant(string name)
            {
                Name = name;
            }

            public string Name { get; private set; }

            public void Receive(string fromUser, string message)
            {
                Console.WriteLine("{0} received message from {1}: {2}", Name, fromUser, message);
            }

            public void EnterChatRoom(IChatRoom chatRoom)
            {
                _chatRoom = chatRoom;
            }

            public void Send(string toUser, string message)
            {
                _chatRoom.SendMessage(Name, toUser, message);
            }

            public void SendBulk(string message)
            {
                _chatRoom.SendBulkMessage(Name, message);
            }

            IChatRoom _chatRoom;
        }

        static void Main(string[] args)
        {
            var arthur = new Participant("Arthur");
            var trillian = new Participant("Trillian");
            var ford = new Participant("Ford");

            var chat = new ChatRoom();

            chat.Add(arthur);
            chat.Add(trillian);
            chat.Add(ford);

            ford.Send("Arthur", "Get your towel");
            arthur.Send("Trillian", "Trisha McMillan?!");
            ford.SendBulk("Forty-two");
        }
    }
}
