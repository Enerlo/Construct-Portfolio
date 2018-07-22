using UnityEngine.UI;
using UnityEngine;

namespace Enerlion
{
    public class GUIInfo : MonoBehaviour
    {
        private string _player;
        private CellComponent[] _components;

        public Text HPText;
        public Text Weapon1Text;
        public Text Weapon2Text;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerCore>().GetComponent<PlayerCore>().HP.ToString();
            _components = GetComponentsInChildren<CellComponent>();
        }

        public void ChekInfo()
        {
            HPText.text = FindObjectOfType<PlayerCore>().GetComponent<PlayerCore>().HP.ToString();

            if (_components[0].Element == null)
                Weapon1Text.text = "";
            else
            {
                Weapon1Text.text = _components[0].GetComponentInChildren<Weapon>().CurrettBullet.ToString();
            }
            if(_components[1].Element == null)
                Weapon2Text.text = "";
            else
            {
                Weapon2Text.text = _components[1].GetComponentInChildren<Weapon>().CurrettBullet.ToString();
            }
        }

    }
}