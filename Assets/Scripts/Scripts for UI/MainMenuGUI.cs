using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Enerlion
{
    public sealed class MainMenuGUI : MonoBehaviour
    {
        [Header("Main")]
        public GameObject MainMenuPanel;
        public GameObject SettingsPanel;
        public GameObject AudioPanel;
        public GameObject VideoPanel;
        [Header("Audio")]
        public AudioMixer AudioMixer;
        public Slider MasterVolume;
        public Slider MusicVolume;
        public Slider EffectsVolume;
        [Header("Video")]
        public Dropdown Lang;
        public Dropdown Quality;

        private TextUI[] _labels;

        private void Awake()
        {
            _labels = FindObjectsOfType<TextUI>();
            Quality.value = QualitySettings.GetQualityLevel();

            OnChangeLanguage();

            float a, b, c;
            AudioMixer.GetFloat("VolMaster", out a);
            MasterVolume.value = a;
            AudioMixer.GetFloat("VolMusic", out b);
            MusicVolume.value = b;
            AudioMixer.GetFloat("VolEffects", out c);
            EffectsVolume.value = c;

            MainMenuPanel.SetActive(true);
            SettingsPanel.SetActive(false);
            AudioPanel.SetActive(false);
            VideoPanel.SetActive(false);
        }

        #region Video
        public void OnChangeLanguage()
        {
            bool engl = Lang.value == 0 ? true : false;

            foreach(var a in _labels)
            {
                a.GetComponent<Text>().text = GetComponent<LangDB>().ChanleLang(engl, a.name);
            }
        }
        public void OnChangeQuality()
        {
            QualitySettings.SetQualityLevel(Quality.value);
        }
        #endregion

        #region Audio
        public void MasterVolumeChange()
        {
            AudioMixer.SetFloat("VolMaster", MasterVolume.value);
        }

        public void MusicVolumeChange()
        {
            AudioMixer.SetFloat("VolMusic", MusicVolume.value);
        }

        public void EffectsVolumeChange()
        {
            AudioMixer.SetFloat("VolEffects", EffectsVolume.value);
        }
        #endregion

        #region Buttons
        public void OnQuit()
        {
            Application.Quit();
            Debug.Log("Quit!");
        }

        public void OnBack()
        {
            MainMenuPanel.SetActive(true);
            SettingsPanel.SetActive(false);
            AudioPanel.SetActive(false);
            VideoPanel.SetActive(false);
        }

        public void OnSettings()
        {
            MainMenuPanel.SetActive(false);
            SettingsPanel.SetActive(true);
            AudioPanel.SetActive(false);
            VideoPanel.SetActive(false);
        }

        public void OnAudio()
        {
            AudioPanel.SetActive(true);
            VideoPanel.SetActive(false);
        }

        public void OnVideo()
        {
            AudioPanel.SetActive(false);
            VideoPanel.SetActive(true);
        }

        public void OnNewGame()
        {
            SceneManager.LoadScene("Game");
        }
        #endregion
    }

}
