using UnityEngine;
using UnityEngine.InputSystem;

public class OpenCloseBook : MonoBehaviour
{
    private Animator mAnimator;
    [SerializeField] private InputActionProperty interactleft;
    [SerializeField] private InputActionProperty interactright;
    private bool bookOpen = false;
    private float cooldown = 1f;
    float trackcooldown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        mAnimator.SetTrigger("Close");
    }

    // Update is called once per frame
    void Update()
    {
        float value = interactleft.action.ReadValue<float>();
        
        if (value == 1)
        {
            if (bookOpen == false && trackcooldown <= 0) //check for closed book and cooldown 0
            {
                mAnimator.SetTrigger("Open");
                bookOpen = true;
                trackcooldown = cooldown;
            } else if (bookOpen == true && trackcooldown <= 0) //check for opened book and cooldown 0
            {
                mAnimator.SetTrigger("Close");
                bookOpen = false;
                trackcooldown = cooldown;
            }
        }
        trackcooldown -= Time.deltaTime;
    }
}
