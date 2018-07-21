using System.Collections.Generic;
using UnityEngine;

namespace Enerlion
{
    public class UpdateSystem : MonoBehaviour
    {
        public GameObject Event;
        private CameraControllSystem _ccs;
        private CameraWallControllSystem _cwcs;
        private PlayerControllSystem _pcs;
        private BotCore _bc;
        private CoreNormailzed _cn;
        private ActionSystem _as;

        void Awake()
        {
            _ccs = GetComponent<CameraControllSystem>();
            _cwcs = GetComponent<CameraWallControllSystem>();
            _bc = FindObjectOfType<BotCore>();
            _pcs = FindObjectOfType<PlayerControllSystem>();
            _cn = FindObjectOfType<CoreNormailzed>();
            _as = GetComponent<ActionSystem>();
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
            _bc.BotPatrol();
            _pcs.Move();
            _cn.CellMoving();

            if (Input.GetKey(KeyCode.E))
                _as.Action();

            if (Input.GetKey(KeyCode.Mouse0))
                _as.Shoot(false);

            if (Input.GetKey(KeyCode.R))
                _as.Shoot(true);

            //if (Input.anyKey)
            //{
            //    foreach (var key in KeyList)
            //    {
            //        if (Input.GetKey(key))
            //        {
            //            //вызов эвента
            //            Instantiate(Event);
            //            Event.GetComponent<InputEvent>().EnterKey(key);
            //            return;
            //        }
            //    }
            //}
        }


    }
}
