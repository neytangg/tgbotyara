using System;
using System.ComponentModel.DataAnnotations;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Data.SQLite;
using Telegram.Bot.Polling;




namespace tg_bot
{
    class Program
    {
        static void Main(string[] args)
        {

            var botClient = new TelegramBotClient("5765090761:AAHRd1Nc8qA08i-ibhSdXtHjYyr7uluuCNk");

            using var cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            botClient.StartReceiving(
                HandleUpdatesAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: cts.Token);

            /* var me = await botClient.GetMeAsync();*/

            /*Console.WriteLine($"Почав @{me.Username}");*/
            Console.ReadLine();

            cts.Cancel();

            async Task HandleUpdatesAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                if (update.Type == UpdateType.Message && update?.Message?.Text != null)
                {
                    await HandleMessage(botClient, update.Message);
                    return;
                }

            }

            async Task HandleMessage(ITelegramBotClient botClient, Message message)
            {


                if (message.Text == "/start")
                {
                    Register(message.Chat.Id.ToString(), message.Chat.Username.ToString());
                    ReplyKeyboardMarkup keyboard = new(new[]
                    {
            new KeyboardButton[] {"Програмісти", "Авто"},
            new KeyboardButton[] { "Фінансисти", "Бул"},
            new KeyboardButton[] { "m", "a"}
        })
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Виберіть group:", replyMarkup: keyboard);
                    return;
                }


                //Прогарамісти
                if (message.Text == "Програмісти")
                {

                    ReplyKeyboardMarkup keyboard = new(new[]
                    {
            new KeyboardButton[] {"П-11", "П-12" },
            new KeyboardButton[] {"П-21", "П-22"},
            new KeyboardButton[] {"П-31", "П-32"},
            new KeyboardButton[] {"П-41", "П-42"}
        })
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Виберіть курс:", replyMarkup: keyboard);
                    return;
                }


                //Фінансисти
                if (message.Text == "Фінансисти")
                {

                    ReplyKeyboardMarkup keyboard = new(new[]
                    {
            new KeyboardButton[] {"Ф-11", "Ф-12"},
            new KeyboardButton[] {"Ф-21", "Ф-22"},
            new KeyboardButton[] {"Ф-31", "Ф-32"}
        })

                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Виберіть курс:", replyMarkup: keyboard);
                    return;
                }


                //Автомат
                if (message.Text == "Авто")
                {

                    ReplyKeyboardMarkup keyboard = new(new[]
                    {
            new KeyboardButton[] {"А-11", "А-12" },
            new KeyboardButton[] {"А-21", "А-22"},
            new KeyboardButton[] {"А-31", "А-32"},
            new KeyboardButton[] {"А-41", "А-42"}
        })
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Виберіть курс:", replyMarkup: keyboard);
                    return;
                }


                //Бугалтери

                {

                    ReplyKeyboardMarkup keyboard = new(new[]
                    {
            new KeyboardButton[] {"Б-11", "Б-12" },
            new KeyboardButton[] {"Б-21", "Б-22"},
            new KeyboardButton[] {"Б-31", "Б-32"}
        })
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Виберіть курс:", replyMarkup: keyboard);
                    return;
                }


                await botClient.SendTextMessageAsync(message.Chat.Id, $"You said:\n{message.Text}");
            }

            Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
            {
                var ErrorMessage = exception switch
                {
                    ApiRequestException apiRequestException
                        => $"Ошибка телеграм АПИ:\n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
                    _ => exception.ToString()
                };
                Console.WriteLine(ErrorMessage);
                return Task.CompletedTask;
            }

            }

            public static SQLiteConnection DB;
            public static void Register(string user_id, string username )
            {
                try
                {
                    DB = new SQLiteConnection("Data Source =userbd.db;");
                    DB.Open();

                    SQLiteCommand regcmd = DB.CreateCommand();
                    regcmd.CommandText = "INSERT INTO user VALUES(@user_id ,@username)";
                    regcmd.Parameters.AddWithValue("@user_id", user_id);
                    regcmd.Parameters.AddWithValue("@username", username);
                    regcmd.ExecuteNonQuery();

                    DB.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex);
                }

            }

        

    }
}


 