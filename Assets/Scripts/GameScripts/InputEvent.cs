using UnityEngine;

namespace Enerlion
{
    class InputEvent : MonoBehaviour
    {
        public void EnterKey(KeyCode key)
        {
            if (key == KeyCode.W || key == KeyCode.A || key == KeyCode.S || key == KeyCode.D || key == KeyCode.Space)
            {
                FindObjectOfType<PlayerControllSystem>().GetComponent<PlayerControllSystem>().Move();
                return;
            }
            if (key == KeyCode.E)
            {
                FindObjectOfType<ActionSystem>().GetComponent<ActionSystem>().Action();
                return;
            }
            if (key == KeyCode.Mouse0)
            {
                FindObjectOfType<ActionSystem>().GetComponent<ActionSystem>().Shoot(false);
                return;
            }
            if (key == KeyCode.R)
            {
                FindObjectOfType<ActionSystem>().GetComponent<ActionSystem>().Shoot(true);
                return;
            }
        }
    }
}
