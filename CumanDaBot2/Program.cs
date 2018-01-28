using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web; // для Http запросов погоды
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Examples.Echo
{
    public static class Program
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient("545867962:AAHHBkdDxtlepHrGWtQBTBtxjJ6szG-_cNQ");

        public static void Main(string[] args)
        {
            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnMessageEdited += BotOnMessageReceived;
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            Bot.OnInlineQuery += BotOnInlineQueryReceived;
            Bot.OnInlineResultChosen += BotOnChosenInlineResultReceived;
            Bot.OnReceiveError += BotOnReceiveError;

           

            var me = Bot.GetMeAsync().Result;
            


            Console.Title = me.Username;

            Bot.StartReceiving();
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            Bot.StopReceiving();
        }

        public static SortedDictionary<long, int> dickSizes = new SortedDictionary<long, int>();

        public sealed class HttpRequest ;


        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.TextMessage) return;

            IReplyMarkup keyboard = new ReplyKeyboardRemove();

            switch (message.Text.Split(' ').First())
            {
                case "/XYIsize":
                    Random rnd = new Random();
                    int size = rnd.Next(5, 15);
                    int ret = 0;
                    string msg = "Размер вашего дружка: ";

                    if (dickSizes.TryGetValue(message.From.Id, out ret))
                    {
                        size = ret;
                        msg = "А больше-то не вырастет :(\nРазмер вашего дружка: ";
                    }
                    else
                        dickSizes.Add(message.From.Id, size);

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        msg+size+" см",
                        replyMarkup: keyboard);
                    break;
                default:

                // Погода в Астане
                case "/weather":
                    








                    await Bot.SendTextMessageAsync(
                        msg = "-273 C",
                        replyMarkup: keyboard);
                    break;
                //default:
                    const string usage = @"Usage:
/XYIsize  - узнать размер болта
/weather  - Погода в Астане
";

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        usage,
                        replyMarkup: new ReplyKeyboardRemove());
                    break;
            }
        }

        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            await Bot.AnswerCallbackQueryAsync(
                callbackQueryEventArgs.CallbackQuery.Id,
                $"Received {callbackQueryEventArgs.CallbackQuery.Data}");
        }

        private static async void BotOnInlineQueryReceived(object sender, InlineQueryEventArgs inlineQueryEventArgs)
        {
            Console.WriteLine($"Received inline query from: {inlineQueryEventArgs.InlineQuery.From.Id}");

            InlineQueryResult[] results = {
                new InlineQueryResultLocation
                {
                    Id = "1",
                    Latitude = 40.7058316f, // displayed result
                    Longitude = -74.2581888f,
                    Title = "New York",
                    InputMessageContent = new InputLocationMessageContent // message if result is selected
                    {
                        Latitude = 40.7058316f,
                        Longitude = -74.2581888f,
                    }
                },

                new InlineQueryResultLocation
                {
                    Id = "2",
                    Longitude = 52.507629f, // displayed result
                    Latitude = 13.1449577f,
                    Title = "Berlin",
                    InputMessageContent = new InputLocationMessageContent // message if result is selected
                    {
                        Longitude = 52.507629f,
                        Latitude = 13.1449577f
                    }
                }
            };

            await Bot.AnswerInlineQueryAsync(
                inlineQueryEventArgs.InlineQuery.Id,
                results,
                isPersonal: true,
                cacheTime: 0);
        }

        private static void BotOnChosenInlineResultReceived(object sender, ChosenInlineResultEventArgs chosenInlineResultEventArgs)
        {
            Console.WriteLine($"Received inline result: {chosenInlineResultEventArgs.ChosenInlineResult.ResultId}");
        }

        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Console.WriteLine("Received error: {0} — {1}",
                receiveErrorEventArgs.ApiRequestException.ErrorCode,
                receiveErrorEventArgs.ApiRequestException.Message);
        }
    }
}

/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CumanDaBot2
{
    class Program
    {
        static async Task TestApiAsync()
        {
            var botClient = new Telegram.Bot.TelegramBotClient("545867962:AAHHBkdDxtlepHrGWtQBTBtxjJ6szG-_cNQ");
            var me = await botClient.GetMeAsync();

            

            //botClient.SendTextMessageAsync()

            System.Console.WriteLine($"Hello! My name is {me.FirstName}");
        }


        static void Main(string[] args)
        {
            TestApiAsync();
            System.Console.ReadLine();
        }
    }
}
*/
