using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;

namespace CumanDaBot2
{
    public static class Program
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient("545867962:AAHHBkdDxtlepHrGWtQBTBtxjJ6szG-_cNQ");
        private static Vote currentVote = new Vote();

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

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.TextMessage) return;

            IReplyMarkup keyboard = new ReplyKeyboardRemove();

            switch (message.Text.Split(' ', '@').First())
            {
                case "/RegVote":
                    if(currentVote.addVariant(message.From.FirstName))
                        await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        message.From.FirstName + " успешно зарегистрирован",
                        replyMarkup: keyboard);
                    else
                        await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        message.From.FirstName + "Вы уже зарегистрированы!",
                        replyMarkup: keyboard);
                    break;
                case "/Vote":
                    InlineKeyboardButton[] buttons = new InlineKeyboardButton[currentVote.Variants.Count];
                    for (int i = 0; i < currentVote.Variants.Count; i++)
                        buttons[i] = currentVote.Variants[i];

                    keyboard = new InlineKeyboardMarkup(buttons);

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Choose",
                        replyMarkup: keyboard);
                    break;
                case "/Results":
                    await Bot.SendTextMessageAsync(
                    message.Chat.Id,
                    currentVote.getStatsString(),
                    replyMarkup: keyboard);
                    break;
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
                    const string usage = @"Usage:
/XYIsize@CumAnDaBot  - узнать размер болта
/RegVote@CumAnDaBot - Зарегистрироваться для голосования на пидара месяца
/Vote@CumAnDaBot - Голосовать
/Results@CumAnDaBot - результаты последнего голосования
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
            try
            {
                if (currentVote.vote(callbackQueryEventArgs.CallbackQuery.From.Id, callbackQueryEventArgs.CallbackQuery.Data))
                    await Bot.AnswerCallbackQueryAsync(
                        callbackQueryEventArgs.CallbackQuery.Id,
                        $"Received {callbackQueryEventArgs.CallbackQuery.Data}");
                else
                    await Bot.AnswerCallbackQueryAsync(
                    callbackQueryEventArgs.CallbackQuery.Id,
                    $"Вы уже проголосовали!");
            }
            catch (Exception)
            {
                try
                {
                    await Bot.AnswerCallbackQueryAsync(
                    callbackQueryEventArgs.CallbackQuery.Id,
                    $"Некорректный вариант");
                }
                catch (Exception)
                { }
            }
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

