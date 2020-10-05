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
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""1fba306c-214a-478c-82bf-78d7f85f3388"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Build"",
                    ""type"": ""Button"",
                    ""id"": ""cd621183-aaa9-4a93-8168-6228c4f644b6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Value"",
                    ""id"": ""15aad98e-93bb-4448-ac0d-9c0d79e44a0a"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""545a19d6-e9a7-4451-a2c2-3465b41c95b4"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d270eff-d2ad-44d9-9bc3-3ca2e4625391"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Build"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8055128-dab6-4097-aba2-bb6c71316f22"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2400554d-1cde-405b-bd92-b2d2798b1711"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
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
        m_HeroAction_Shoot = m_HeroAction.FindAction("Shoot", throwIfNotFound: true);
        m_HeroAction_Build = m_HeroAction.FindAction("Build", throwIfNotFound: true);
        m_HeroAction_Select = m_HeroAction.FindAction("Select", throwIfNotFound: true);
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
    private readonly InputAction m_HeroAction_Shoot;
    private readonly InputAction m_HeroAction_Build;
    private readonly InputAction m_HeroAction_Select;
    public struct HeroActionActions
    {
        private @HeroInputManager m_Wrapper;
        public HeroActionActions(@HeroInputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_HeroAction_Move;
        public InputAction @Look => m_Wrapper.m_HeroAction_Look;
        public InputAction @Shoot => m_Wrapper.m_HeroAction_Shoot;
        public InputAction @Build => m_Wrapper.m_HeroAction_Build;
        public InputAction @Select => m_Wrapper.m_HeroAction_Select;
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
                @Shoot.started -= m_Wrapper.m_HeroActionActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_HeroActionActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_HeroActionActionsCallbackInterface.OnShoot;
                @Build.started -= m_Wrapper.m_HeroActionActionsCallbackInterface.OnBuild;
                @Build.performed -= m_Wrapper.m_HeroActionActionsCallbackInterface.OnBuild;
                @Build.canceled -= m_Wrapper.m_HeroActionActionsCallbackInterface.OnBuild;
                @Select.started -= m_Wrapper.m_HeroActionActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_HeroActionActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_HeroActionActionsCallbackInterface.OnSelect;
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
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Build.started += instance.OnBuild;
                @Build.performed += instance.OnBuild;
                @Build.canceled += instance.OnBuild;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
            }
        }
    }
    public HeroActionActions @HeroAction => new HeroActionActions(this);
    public interface IHeroActionActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnBuild(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
    }
}
