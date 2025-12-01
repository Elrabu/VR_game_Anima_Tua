using UnityEngine;
using UnityEngine.InputSystem;

public class A_Button_Test : MonoBehaviour
{

    public InputActionProperty button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float value = button.action.ReadValue<float>();
        Debug.Log(value);
    }
}
