using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        
        private CarController m_Car; // the car controller we want to use

        public Material m_brakeLightLit;
        public Material m_brakeLightUnlit;

        public GameObject m_leftBrakeLight;
        public GameObject m_rightBrakeLight;
        public GameObject m_centreBrakeLight;

        public Light m_leftLight;
        public Light m_rightLight;
        public Light m_centreLight;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif

            if (v < 0f)
            {
                m_leftBrakeLight.GetComponent<Renderer>().material = m_brakeLightLit;
                m_rightBrakeLight.GetComponent<Renderer>().material = m_brakeLightLit;
                m_centreBrakeLight.GetComponent<Renderer>().material = m_brakeLightLit;

                m_leftLight.enabled = true;
                m_rightLight.enabled = true;
                m_centreLight.enabled = true;
            } else
            {
                m_leftBrakeLight.GetComponent<Renderer>().material = m_brakeLightUnlit;
                m_rightBrakeLight.GetComponent<Renderer>().material = m_brakeLightUnlit;
                m_centreBrakeLight.GetComponent<Renderer>().material = m_brakeLightUnlit;

                m_leftLight.enabled = false;
                m_rightLight.enabled = false;
                m_centreLight.enabled = false;
            }
        }
    
    }
        
    
}
