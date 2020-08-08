using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System.Collections.Generic;
using UnityEngine;


/*  
Контакты:
- https://vk.com/aud1t3k_community ( вк группа )
- https://vk.com/vi2games_unt ( Vi2 Games плагины для Unturned )
- https://discord.com/invite/E7k8Q7A ( дискорд группа )
- https://t.me/aud1t3k_community ( телеграм канал )
- https://www.youtube.com/channel/UCxANX_x9L1pHtfVU49d6NCA?sub_confirmation=1 ( ютуб канал )

Поддержка:
- https://qiwi.me/aud1t3k ( написать сообщение в лс )
- https://www.donationalerts.com/r/aud1t3k ( логосовое сообщение на стриме )   
*/


namespace Back
{
    // Главный класс команды
    public class Back : RocketPlugin<Config>
    {
        public static Back Instance; // Ссылка на наш плагин


        // Словать для того чтобы хранить информацию, кто и где умер чтобы мы могли телепортировать этого игрока на место смерти
        public Dictionary<CSteamID, Vector3> LastPlayersPosition = new Dictionary<CSteamID, Vector3>();



        // Срабатывает при загрузке плагина на сервер
        protected override void Load()
        {
            Instance = this;
            UnturnedPlayerEvents.OnPlayerDeath += onPlayerDeath; // Привязываем событие когда игрок умирает
        }

        // Срабатывает при выгрузке плагина из сервера
        protected override void Unload()
        {
            Instance = null;
            UnturnedPlayerEvents.OnPlayerDeath -= onPlayerDeath; // Отвязываем от события когда игрок умирает
        }


        // Перевод плагина
        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "command_lastposition_not_found", "Невозможно телепотироваться" },
            { "command_back_successful", "Вы успешно телепорированы на место смерти" }
        };



        // Когда умер игрок
        // player - Это игрок который умер
        // cause - Это причина от чего он умер
        // limb - Это часть тела игрока от которого он умер
        // murderer - Убийца (тот кто убил игрока)
        private void onPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
        {
            // Проверяем если ли он в словаре, чтобы указать позицию игрока
            if (LastPlayersPosition.ContainsKey(player.CSteamID))
            {
                LastPlayersPosition[player.CSteamID] = player.Position; // Перезаписываем позицию игрока 
            }
            else
            {
                LastPlayersPosition.Add(player.CSteamID, player.Position); // Добавляем позицию игрока
            }
        }
    }
}
