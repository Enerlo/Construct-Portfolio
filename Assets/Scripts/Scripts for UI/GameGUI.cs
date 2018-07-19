using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Enerlion
{
    public sealed class GameGUI : MonoBehaviour
    {
        [Header("Active UI")]
        public Text HP;
        public Text Energy;
        public Text Weapon1;
        public Text Weapon2;
        [Header("Pause Panels")]
        public GameObject PausePanel;
        public GameObject SettingsPanel;
        public GameObject VideoPanel;
        public GameObject AudioPanel;
        public GameObject ActivePanel;
        [Header("Audio")]
        public AudioMixer AudioMixer;
        public Slider MasterVolume;
        public Slider MusicVolume;
        public Slider EffectsVolume;
        [Header("Video")]
        public Dropdown Lang;
        private TextUI[] _labels;

        private bool _isEnabled = false;
        private bool _active = true;

        private void Awake()
        {
            _labels = FindObjectsOfType<TextUI>();

            OnChangeLanguage();

            float a,b,c;
            AudioMixer.GetFloat("VolMaster", out  a);
            MasterVolume.value = a;
            AudioMixer.GetFloat("VolMusic", out b);
            MusicVolume.value = b;
            AudioMixer.GetFloat("VolEffects", out c);
            EffectsVolume.value = c;

            ActivePanel.SetActive(true);
            PausePanel.SetActive(false);
            SettingsPanel.SetActive(false);
            AudioPanel.SetActive(false);
            VideoPanel.SetActive(false);

        }

        public void OnChangeLanguage()
        {
            bool engl = Lang.value == 0 ? true : false;

            foreach (var a in _labels)
            {
                a.GetComponent<Text>().text = GetComponent<LangDB>().ChanleLang(engl, a.name);
            }
        }

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
        public void OnBack()
        {
            PausePanel.SetActive(true);
            SettingsPanel.SetActive(false);
            AudioPanel.SetActive(false);
            VideoPanel.SetActive(false);
        }

        public void OnSettings()
        {
            PausePanel.SetActive(false);
            SettingsPanel.SetActive(true);
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

        public void OnContinue()
        {
            ActivePanel.SetActive(true);
            PausePanel.SetActive(false);
            SettingsPanel.SetActive(false);
            AudioPanel.SetActive(false);
            VideoPanel.SetActive(false);
        }

        public void OnPause()
        {
            ActivePanel.SetActive(false);
            PausePanel.SetActive(true);
        }

        public void OnMainMenu()
        {
            SceneManager.LoadScene("Menu");
        }
        #endregion

        void OnGUI()
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                if(_active)
                {
                    _active = false;
                    Invoke("ActiveTrue", 0.5f);
                    if (_isEnabled)
                    {
                        _isEnabled = !_isEnabled;
                        OnPause();
                        return;
                    }
                    else
                    {
                        _isEnabled = !_isEnabled;
                        OnContinue();
                        return;
                    }
                }
            }
        }

        void ActiveTrue()
        {
            _active = true;
        }
    }
}