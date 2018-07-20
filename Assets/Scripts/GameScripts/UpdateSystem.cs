using System.Collections.Generic;
using UnityEngine;

namespace Enerlion
{
    public class UpdateSystem : MonoBehaviour
    {
        public GameObject Event;
        private CameraControllSystem _ccs;
        private CameraWallControllSystem _cwcs;
        private CoreCell _coreCell;

        public KeyCode MoveForvard = KeyCode.W;
        public KeyCode MoveDown = KeyCode.S;
        public KeyCode TurnLeft = KeyCode.A;
        public KeyCode TurnRight = KeyCode.D;
        public KeyCode Jump = KeyCode.Space;
        public KeyCode Action = KeyCode.E;
        public KeyCode ReloadKey = KeyCode.R;
        public KeyCode Fire = KeyCode.Mouse0;
        List<KeyCode> KeyList = new List<KeyCode>();

        void Start()
        {
            KeyList.Add(MoveForvard);
            KeyList.Add(MoveDown);
            KeyList.Add(TurnLeft);
            KeyList.Add(TurnRight);
            KeyList.Add(Jump);
            KeyList.Add(Action);
            KeyList.Add(ReloadKey);
            KeyList.Add(Fire);
        }



        void Awake()
        {
            _ccs = GetComponent<CameraControllSystem>();
            _cwcs = GetComponent<CameraWallControllSystem>();
            _coreCell = FindObjectOfType<CoreCell>();
        }

        void LateUpdate()
        {
            _cwcs.CameraWall();
        }

        void FixedUpdate()
        {
            _ccs.FollowTarget(Time.deltaTime);
        }

        void Update()
        {
            _ccs.HandleRotationMovement();
            _coreCell.CellMoving();

            if (Input.anyKey)
            {
                foreach (var key in KeyList)
                {
                    if (Input.GetKey(key))
                    {
                        //вызов эвента
                        Instantiate(Event);
                        Event.GetComponent<InputEvent>().EnterKey(key);
                        return;
                    }
                }
            }
        }


    }
}
