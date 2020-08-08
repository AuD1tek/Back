using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System.Collections.Generic;
using UnityEngine;

namespace Back
{
    public class Back : RocketPlugin<Config>
    {
        public static Back Instance; // Ссылка на наш плагин

        public Dictionary<CSteamID, Vector3> LastPlayerPositions = new Dictionary<CSteamID, Vector3>();



        // Срабатывает при загрузке плагина на сервер
        protected override void Load()
        {
            Instance = this;
            UnturnedPlayerEvents.OnPlayerDeath += onPlayerDeath;
        }

        // Срабатывает при выгрузке плагина из сервера
        protected override void Unload()
        {
            Instance = null;
            UnturnedPlayerEvents.OnPlayerDeath -= onPlayerDeath;
        }


        // Перевод плагина
        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "command_lastposition_not_found", "Невозможно телепотироваться" },
            { "command_back_successful", "Вы успешно телепорированы на место смерти" }
        };



        // Когда умер игрок
        private void onPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
        {
            if (LastPlayerPositions.ContainsKey(player.CSteamID))
            {
                LastPlayerPositions[player.CSteamID] = player.Position;
            }
            else
            {
                LastPlayerPositions.Add(player.CSteamID, player.Position);
            }
        }
    }
}
