using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Back
{
    public class CommandBack : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player; 
        public string Name => "back";
        public string Help => "Возвращает игрока на место сметри";
        public string Syntax => "/back";
        public List<string> Aliases => new List<string>() { "b" };
        public List<string> Permissions => new List<string>() { "vi2g.back" };


        private UnturnedPlayer player;


        public void Execute(IRocketPlayer caller, string[] command)
        {
            player = (UnturnedPlayer)caller; // Получаем игрока который вызвал команду


            
            if (Back.Instance.LastPlayerPositions.TryGetValue(player.CSteamID, out Vector3 lastPosition))
            {
                UnturnedChat.Say(player, Back.Instance.Translate("command_back_successful"), Color.green);
                player.Teleport(lastPosition, player.Rotation); // Телепортировать игрока в указаные кординаты
            }
            else
            {
                UnturnedChat.Say(player, Back.Instance.Translate("command_lastposition_not_found"), Color.red);
            }
        }
    }
}
