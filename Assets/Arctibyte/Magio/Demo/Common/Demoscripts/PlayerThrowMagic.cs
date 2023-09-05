#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

namespace Magio
{
    public class PlayerThrowMagic : MonoBehaviour
    {
        public GameObject nullify;

        public GameObject spread;

        public Animator animator;

#if ENABLE_INPUT_SYSTEM
        InputAction shoot;

        InputAction nextWeapon;
        InputAction previousWeapon;

        InputAction stopPlaying;

        void Start()
        {

            shoot = new InputAction("PlayerShoot", binding: "<Gamepad>/x");
            shoot.AddBinding("<Mouse>/leftButton");

            shoot.Enable();

            stopPlaying = new InputAction("StopPlaying", binding: "<Gamepad>/b");
            stopPlaying.AddBinding("<Keyboard>/Escape");

            stopPlaying.Enable();

            stopPlaying.started += StopPlaying_started;

            animator.StopPlayback();
        }

        private void StopPlaying_started(InputAction.CallbackContext obj)
        {
            Application.Quit();
        }

#else
        void Start()
        {

            animator.StopPlayback();
        }
#endif

        // Update is called once per frame
        void Update()
        {
            bool shootPressed = false;

#if ENABLE_INPUT_SYSTEM
            shootPressed = Mathf.Approximately(shoot.ReadValue<float>(), 1);
#else
            shootPressed = Input.GetKey(KeyCode.Mouse0);
            bool stopPlayingPressed = Input.GetKeyDown(KeyCode.Escape);

            if (stopPlayingPressed)
            {
                Application.Quit();
            }
#endif
            if (shootPressed)
            {
                nullify.SetActive(true);

                spread.SetActive(false);
            }
            else
            {
                nullify.SetActive(false);

                spread.SetActive(true);
            }
        }
    }

}
