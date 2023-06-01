using Telegram.Bot;

namespace RestoranManager.Telegram
{
    public class TelegramService
    {
        private readonly TelegramBotClient bot;

        public TelegramService(TelegramBotClient bot)
        {
            this.bot = bot;
        }

        public void Start()
        {
            bot.ReceiveAsync<TelegramHandler>();
            Console.ReadKey();
        }
    }
}
