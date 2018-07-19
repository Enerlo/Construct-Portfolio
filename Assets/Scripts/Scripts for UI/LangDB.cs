using UnityEngine;

namespace Enerlion
{
    public class LangDB : MonoBehaviour
    {
        public string ChanleLang(bool english, string name)
        {
            switch (name)
            {
                case "NewGameText":
                    {
                        return english ? "New game" : "Новая игра";
                    }

                case "SettingsText" :
                    {
                        return english ? "Settings" : "Настройки";
                    }

                case "QuitText":
                    {
                        return english ? "Quit" : "Выход";
                    }

                case "VideoText":
                    {
                        return english ? "Video" : "Видео";
                    }

                case "AudioText":
                    {
                        return english ? "Audio" : "Аудио";
                    }

                case "BackText":
                    {
                        return english ? "Back" : "Назад";
                    }

                case "LanguageText":
                    {
                        return english ? "Language" : "Язык";
                    }

                case "QualityText":
                    {
                        return english ? "Quality" : "Качество";
                    }

                case "MasterVolText":
                    {
                        return english ? "Master volume" : "Общий звук";
                    }

                case "MusicVolText":
                    {
                        return english ? "Music volume" : "Музыка";
                    }

                case "EffectsVolText":
                    {
                        return english ? "Effects volume" : "Эффекты";
                    }

                case "PauseMenuText":
                    {
                        return english ? "Pause" : "Пауза";
                    }

                case "ContText":
                    {
                        return english ? "Continue" : "Продолжить";
                    }

                case "ToMainMenuText":
                    {
                        return english ? "Main menu" : "В главное меню";
                    }

                case "HPText":
                    {
                        return english ? "HP" : "Жизнь";
                    }

                case "EngText":
                    {
                        return english ? "Energy" : "Энергия";
                    }

                case "Wep2Text":
                    {
                        return english ? "Weapon 2" : "Оружие 2";
                    }

                case "Wep1Text":
                    {
                        return english ? "Weapon 1" : "Оружие 1";
                    }
            }
            return null;
        }
    }
}
