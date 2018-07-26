using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        public bool m_Knife;
        public bool m_Kick;

        public bool isHeroAlive = true;

        public event EventHandler OnPlayerAttack;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
                m_Knife = CrossPlatformInputManager.GetButtonDown("Knife");
                m_Kick = CrossPlatformInputManager.GetButtonDown("Kick");
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump, m_Knife, m_Kick);
            m_Jump = false;
            m_Knife = false;
            m_Kick = false;

        }

        public void GotAttacked()
        {
            Transform result = transform.Find("HealthBar").Find("Bar");

            if (result)
            {
                result.localScale = new Vector3(result.localScale.x - 0.1f, result.localScale.y, result.localScale.z);
            }

            if (result.localScale.x <= 0)
            {
                isHeroAlive = false;
            }
        }
    }
}
