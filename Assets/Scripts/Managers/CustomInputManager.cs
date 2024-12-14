using UnityEngine;

public class CustomInputManager : MonoBehaviour
{
    public static CustomInputManager Instance { get; private set; }
    public CustomInput customInputsBindings { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            SetupSingleton();
        }
    }

    private void SetupSingleton()
    {
        customInputsBindings = new CustomInput();
        customInputsBindings.Enable();
    }
}
