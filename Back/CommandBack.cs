using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Back
{
    // Класс команды
    public class CommandBack : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player; // Тот кто может вызивать данную команду
        public string Name => "back"; // Имя команды
        public string Help => "Возвращает игрока на место сметри"; // Информация о команде
        public string Syntax => "/back"; // Синтаксис команды
        public List<string> Aliases => new List<string>() { "b" }; // Возможное вариации команды
        public List<string> Permissions => new List<string>() { "vi2g.back" }; // Пермишионы команды


        private UnturnedPlayer player; // игрок который вызвал команду


        // Функция которая будет срабатывать при использование данной команды
        public void Execute(IRocketPlayer caller, string[] command)
        {
            player = (UnturnedPlayer)caller; // Получаем игрока который вызвал команду


            if (Back.Instance.LastPlayersPosition.TryGetValue(player.CSteamID, out Vector3 lastPosition))
            {
                UnturnedChat.Say(player, Back.Instance.Translate("command_back_successful"), Color.green); // Написать игроку сообщение что он был успешно телепортирован на место смерти
                player.Teleport(lastPosition, player.Rotation); // Телепортировать игрока в указаные кординаты
            }
            else
            {
                UnturnedChat.Say(player, Back.Instance.Translate("command_lastposition_not_found"), Color.red); // Написать игроку что телепортироваться не возможно
            }
        }
    }
}
