using UnityEngine;
using UnityEngine.InputSystem;

public class OpenCloseBook : MonoBehaviour
{
    [SerializeField] private Animator mAnimator;
    [SerializeField] private InputActionProperty interactleft;
    [SerializeField] private InputActionProperty interactright;
    [SerializeField] private GrabListener grabListenerRight;
    [SerializeField] private GrabListener grabListenerLeft;
    private bool bookOpen = false;
    private float cooldown = 1f;
    float trackcooldown;
    string bookRight;
    string bookLeft;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (mAnimator != null)
        {
            mAnimator.SetTrigger("Close");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float valueLeft = interactleft.action.ReadValue<float>(); //Left
        float valueRight = interactright.action.ReadValue<float>(); //right

        
        if (mAnimator != null)
        {
            var itemRight = grabListenerRight.CurrentlyHeld;
            var itemLeft = grabListenerLeft.CurrentlyHeld;
            if (itemRight != null)
            {
                bookRight = itemRight.transform.name;
            } else if (itemLeft != null)
            {
                bookLeft = itemLeft.transform.name;
            } else
            {
                bookLeft = "null";
                bookRight = "null";
            }
            //Debug.Log("Left: " + bookLeft + "Right: " + bookRight);
        
            if (valueLeft == 1 && bookLeft == "book" || valueRight == 1 && bookRight == "book")
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
}
