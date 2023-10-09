using System;
using System.Threading.Tasks;
using Telegram.Bot;
using System.Data.SQLite;

namespace Telegram_Bot
{
    internal class Program
    { 

        const string TOKEN = "6521646220:AAEmBWbC-PS15famhUiVraGehCZBXrLivd4";

        public static SQLiteConnection DB;
    
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    GetMessages().Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
            }
        }

        static async Task GetMessages()
        {
            TelegramBotClient bot = new TelegramBotClient(TOKEN);
            int offset = 0;
            int timeout = 0;
            try
            {
                await bot.SetWebhookAsync("");
                while (true)
                {
                    var updates = await bot.GetUpdatesAsync(offset, timeout);

                    foreach (var update in updates)
                    {
                        var message = update.Message;

                        if (message.Text == "/start")
                        {
                            Console.WriteLine("Успешный запуск Бота, пользователь:" + message.Chat.Username);
                            Console.WriteLine("-----------------------------------------------------------");
                            await bot.SendTextMessageAsync(message.Chat.Id, "Добро пожаловать! " + message.Chat.Username + "\U0001F44B");
                            await bot.SendTextMessageAsync(message.Chat.Id, "Я Телеграм-Бот созданный специально для помощи абитуриентам!");
                            await bot.SendTextMessageAsync(message.Chat.Id, "С моей помощью, вы сможете с лёгкостью зарегистрироваться на предстоящие Мероприятия и Мастер-классы, не занимая более 5 минут вашего времени!");
                            await bot.SendTextMessageAsync(message.Chat.Id, "\U0001F60E");
                            await bot.SendTextMessageAsync(message.Chat.Id, "Я знаю вот такие команды:");
                            await bot.SendTextMessageAsync(message.Chat.Id, "/help ~ Список доступных команд!");
                            await bot.SendTextMessageAsync(message.Chat.Id, "/vk ~ Ссылка на нашу Группу ВК!");
                            await bot.SendTextMessageAsync(message.Chat.Id, "/events ~ Доступные Мероприятия!");
                            await bot.SendTextMessageAsync(message.Chat.Id, "/masterclasses ~ Доступные Мастер-классы!");
                        }
                        if (message.Text == "/help")
                        {
                            Console.WriteLine("Открыл список команд, пользователь:" + message.Chat.Username);
                            Console.WriteLine("-----------------------------------------------------------");
                            await bot.SendTextMessageAsync(message.Chat.Id, "/help ~ Список доступных команд!");
                            await bot.SendTextMessageAsync(message.Chat.Id, "/vk ~ Ссылка на нашу Группу ВК!");
                            await bot.SendTextMessageAsync(message.Chat.Id, "/events ~ Доступные Мероприятия!");
                            await bot.SendTextMessageAsync(message.Chat.Id, "/masterclasses ~ Доступные Мастер-классы!");
                        }
                        if (message.Text == "/vk")
                        {
                            Console.WriteLine("Запрос ссылки ВК, пользователь:" + message.Chat.Username);
                            Console.WriteLine("-----------------------------------------------------------");
                            await bot.SendTextMessageAsync(message.Chat.Id, "Группа ВК: СМК им. Бартенева В.В.");
                            await bot.SendTextMessageAsync(message.Chat.Id, "https://vk.com/cmt_11");
                            await bot.SendTextMessageAsync(message.Chat.Id, "\U0001F609");
                        }
                        if (message.Text == "/masterclasses")
                        {
                            Console.WriteLine("Открыт раздел Мастер классов, пользователь:" + message.Chat.Username);
                            Console.WriteLine("-----------------------------------------------------------");
                            await bot.SendTextMessageAsync(message.Chat.Id, "Доступные Мастер-Классы:");
                            await bot.SendTextMessageAsync(message.Chat.Id, "https://youtu.be/gS5JPGFU4tY?si=Y7yLnzbmmUH_cfDc");
                            await bot.SendTextMessageAsync(message.Chat.Id, "https://youtu.be/ZKU7-ktaa2o?si=FwNsQ6-ivI3zLI_L");
                            await bot.SendTextMessageAsync(message.Chat.Id, "https://youtu.be/k6yr_Fh__7U?si=Jd21qSM3c_0ZTT9p");
                            
                        }
                        if (message.Text == "/events")
                        {
                            Console.WriteLine("Открыт раздел Мероприятий, пользователь:" + message.Chat.Username);
                            Console.WriteLine("-----------------------------------------------------------");
                            await bot.SendTextMessageAsync(message.Chat.Id, "\U0001F4CC Обратите внимание! \U0001F4CC");
                            await bot.SendTextMessageAsync(message.Chat.Id, "Для успешной регистрации на Мероприятие, необходимо указать вашу настоящую Фамилию и Имя в настройках профиля Телеграм.");
                            await bot.SendTextMessageAsync(message.Chat.Id, "Для проверки данных, выполните следующие шаги:");
                            await bot.SendTextMessageAsync(message.Chat.Id, "1. Нажмите на три полоски, в левом верхнем углу экрана.");
                            await bot.SendTextMessageAsync(message.Chat.Id, "2. Перейдите в пункт настройки.");
                            await bot.SendTextMessageAsync(message.Chat.Id, "3. Нажмите на три точки и выберете пункт Изменить имя.");
                            await bot.SendTextMessageAsync(message.Chat.Id, "\U0001F609");

                            await bot.SendTextMessageAsync(message.Chat.Id, "На данный момент, в СМК им. Бартенева В.В. открыта регистрация на:");
                            await bot.SendTextMessageAsync(message.Chat.Id, "[1] ~День открытых дверей!(01.10.2023, с 10:00 до 14:00)");
                            await bot.SendTextMessageAsync(message.Chat.Id, "[2] ~ ?????????? (??.??.????, с ??:?? до ??:??)");
                            await bot.SendTextMessageAsync(message.Chat.Id, "[3] ~ ?????????? (??.??.????, с ??:?? до ??:??)");
                            await bot.SendTextMessageAsync(message.Chat.Id, "Для подачи заявки, напишите в чат номер мероприятия, которое вы желаете посетить!"); 
                        }
                        if (message.Text == "1")
                        {
                            Console.WriteLine("Регистрация в базу данных начата, пользователь:" + message.Chat.Username);
                            Console.WriteLine("-----------------------------------------------------------");
                            Regitration(message.Chat.Id.ToString(), message.Chat.Username.ToString(), message.Chat.FirstName.ToString(), message.Chat.LastName.ToString());
                            await bot.SendTextMessageAsync(message.Chat.Id, "Благодарим за регистрацию на Мероприятие под номером [1]! \U0001F506");
                            await bot.SendTextMessageAsync(message.Chat.Id, "\U0001F973");
                        }
                        
                        offset = update.Id + 1;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }

        public static void Regitration(string chatId, string username, string firstname, string lastname)
        {
            try
            {
                DB = new SQLiteConnection("Data Source=DB.db;");
                DB.Open();
                SQLiteCommand regcmd = DB.CreateCommand();
                regcmd.CommandText = "INSERT INTO RegUsers VALUES(@chatId, @username, @firstname, @lastname)";
                regcmd.Parameters.AddWithValue("@chatId", chatId);
                regcmd.Parameters.AddWithValue("@username", username);
                regcmd.Parameters.AddWithValue("@firstname", firstname);
                regcmd.Parameters.AddWithValue("@lastname", lastname);
                regcmd.ExecuteNonQuery();
                DB.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
    }
}
