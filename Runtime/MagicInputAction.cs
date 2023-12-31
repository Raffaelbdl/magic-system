//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Packages/MagicSystem/MagicInputAction.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @MagicInputAction: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MagicInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MagicInputAction"",
    ""maps"": [
        {
            ""name"": ""Magic"",
            ""id"": ""e6715a10-86f5-4853-9803-ef5e6733b477"",
            ""actions"": [
                {
                    ""name"": ""Skill1"",
                    ""type"": ""Button"",
                    ""id"": ""cf0441f4-e434-457b-8301-f8b6b6ffcd78"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill2"",
                    ""type"": ""Button"",
                    ""id"": ""590e0428-3dcc-4cec-b98f-f282ae567269"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill3"",
                    ""type"": ""Button"",
                    ""id"": ""66d43927-d422-45af-9da0-e80ae1b702fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill4"",
                    ""type"": ""Button"",
                    ""id"": ""e75ad948-6a4d-49ba-b486-99d8b0f636b3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""86da8ced-5574-4234-ad7e-04d90a980178"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ea88949-d5e9-472c-a58a-4c0d0225ca65"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3125cdc-0600-44a9-90eb-88450a824f34"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0432f627-361d-4341-81a1-9f8f73971dae"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Magic
        m_Magic = asset.FindActionMap("Magic", throwIfNotFound: true);
        m_Magic_Skill1 = m_Magic.FindAction("Skill1", throwIfNotFound: true);
        m_Magic_Skill2 = m_Magic.FindAction("Skill2", throwIfNotFound: true);
        m_Magic_Skill3 = m_Magic.FindAction("Skill3", throwIfNotFound: true);
        m_Magic_Skill4 = m_Magic.FindAction("Skill4", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Magic
    private readonly InputActionMap m_Magic;
    private List<IMagicActions> m_MagicActionsCallbackInterfaces = new List<IMagicActions>();
    private readonly InputAction m_Magic_Skill1;
    private readonly InputAction m_Magic_Skill2;
    private readonly InputAction m_Magic_Skill3;
    private readonly InputAction m_Magic_Skill4;
    public struct MagicActions
    {
        private @MagicInputAction m_Wrapper;
        public MagicActions(@MagicInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Skill1 => m_Wrapper.m_Magic_Skill1;
        public InputAction @Skill2 => m_Wrapper.m_Magic_Skill2;
        public InputAction @Skill3 => m_Wrapper.m_Magic_Skill3;
        public InputAction @Skill4 => m_Wrapper.m_Magic_Skill4;
        public InputActionMap Get() { return m_Wrapper.m_Magic; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MagicActions set) { return set.Get(); }
        public void AddCallbacks(IMagicActions instance)
        {
            if (instance == null || m_Wrapper.m_MagicActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MagicActionsCallbackInterfaces.Add(instance);
            @Skill1.started += instance.OnSkill1;
            @Skill1.performed += instance.OnSkill1;
            @Skill1.canceled += instance.OnSkill1;
            @Skill2.started += instance.OnSkill2;
            @Skill2.performed += instance.OnSkill2;
            @Skill2.canceled += instance.OnSkill2;
            @Skill3.started += instance.OnSkill3;
            @Skill3.performed += instance.OnSkill3;
            @Skill3.canceled += instance.OnSkill3;
            @Skill4.started += instance.OnSkill4;
            @Skill4.performed += instance.OnSkill4;
            @Skill4.canceled += instance.OnSkill4;
        }

        private void UnregisterCallbacks(IMagicActions instance)
        {
            @Skill1.started -= instance.OnSkill1;
            @Skill1.performed -= instance.OnSkill1;
            @Skill1.canceled -= instance.OnSkill1;
            @Skill2.started -= instance.OnSkill2;
            @Skill2.performed -= instance.OnSkill2;
            @Skill2.canceled -= instance.OnSkill2;
            @Skill3.started -= instance.OnSkill3;
            @Skill3.performed -= instance.OnSkill3;
            @Skill3.canceled -= instance.OnSkill3;
            @Skill4.started -= instance.OnSkill4;
            @Skill4.performed -= instance.OnSkill4;
            @Skill4.canceled -= instance.OnSkill4;
        }

        public void RemoveCallbacks(IMagicActions instance)
        {
            if (m_Wrapper.m_MagicActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMagicActions instance)
        {
            foreach (var item in m_Wrapper.m_MagicActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MagicActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MagicActions @Magic => new MagicActions(this);
    public interface IMagicActions
    {
        void OnSkill1(InputAction.CallbackContext context);
        void OnSkill2(InputAction.CallbackContext context);
        void OnSkill3(InputAction.CallbackContext context);
        void OnSkill4(InputAction.CallbackContext context);
    }
}
