// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Heros/HeroInputManager.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @HeroInputManager : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @HeroInputManager()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""HeroInputManager"",
    ""maps"": [
        {
            ""name"": ""HeroAction"",
            ""id"": ""2ee9e598-b355-42cd-b22d-4cfec486d69e"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""62b96047-d4b7-4b4e-be42-201fdd6dbcf7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""9e9510f4-4536-4a48-ac50-a6dcfe57c601"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6e4e35f3-c301-447a-97af-5c9a6c33fc6f"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32f8fd55-ad2d-4899-91c3-b016c47484b5"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // HeroAction
        m_HeroAction = asset.FindActionMap("HeroAction", throwIfNotFound: true);
        m_HeroAction_Move = m_HeroAction.FindAction("Move", throwIfNotFound: true);
        m_HeroAction_Look = m_HeroAction.FindAction("Look", throwIfNotFound: true);
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

    // HeroAction
    private readonly InputActionMap m_HeroAction;
    private IHeroActionActions m_HeroActionActionsCallbackInterface;
    private readonly InputAction m_HeroAction_Move;
    private readonly InputAction m_HeroAction_Look;
    public struct HeroActionActions
    {
        private @HeroInputManager m_Wrapper;
        public HeroActionActions(@HeroInputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_HeroAction_Move;
        public InputAction @Look => m_Wrapper.m_HeroAction_Look;
        public InputActionMap Get() { return m_Wrapper.m_HeroAction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(HeroActionActions set) { return set.Get(); }
        public void SetCallbacks(IHeroActionActions instance)
        {
            if (m_Wrapper.m_HeroActionActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_HeroActionActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_HeroActionActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_HeroActionActionsCallbackInterface.OnMove;
                @Look.started -= m_Wrapper.m_HeroActionActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_HeroActionActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_HeroActionActionsCallbackInterface.OnLook;
            }
            m_Wrapper.m_HeroActionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
            }
        }
    }
    public HeroActionActions @HeroAction => new HeroActionActions(this);
    public interface IHeroActionActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
    }
}
