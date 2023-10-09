using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MagicSystem
{
    public class MagicInputs
    {
        public Vector2 mousePosition;
    }

    public class MagicController : MonoBehaviour
    {
        private Camera m_camera;
        public MagicInputAction magicInput { get; private set; }
        public MagicInputs Inputs { get; private set; }

        private void Awake()
        {
            m_camera = Camera.main;

            magicInput = new MagicInputAction();
            magicInput.Enable();

            Inputs = new MagicInputs();
        }

        private void Update()
        {
            Inputs.mousePosition = m_camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }
    }
}
