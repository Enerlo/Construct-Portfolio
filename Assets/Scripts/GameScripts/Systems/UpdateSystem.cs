using UnityEngine;

namespace Enerlion
{
    public class UpdateSystem : MonoBehaviour
    {
        private CameraControllSystem _ccs;
        private CameraWallControllSystem _cwcs;
        private PlayerControllSystem _pcs;
        private BotCore _bc;
        private CoreNormailzed _cn;
        private ActionSystem _as;
        private GivenPlatform[] _givenPlatform;
        private GUIInfo _gi;

        void Awake()
        {
            _ccs = GetComponent<CameraControllSystem>();
            _cwcs = GetComponent<CameraWallControllSystem>();
            _bc = FindObjectOfType<BotCore>();
            _pcs = FindObjectOfType<PlayerControllSystem>();
            _cn = FindObjectOfType<CoreNormailzed>();
            _as = GetComponent<ActionSystem>();
            _givenPlatform = FindObjectsOfType<GivenPlatform>();
            _gi = FindObjectOfType<GUIInfo>();
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
            _gi.ChekInfo();

            if (Input.GetKey(KeyCode.E))
                _as.Action();

            if (Input.GetKey(KeyCode.Mouse0))
                _as.Shoot(false);

            if (Input.GetKey(KeyCode.R))
                _as.Shoot(true);

            foreach(var a in _givenPlatform)
            {
                a.VarObject();
            }

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
