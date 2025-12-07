using UnityEngine;
using UnityEngine.InputSystem;

public class OpenCloseBook : MonoBehaviour
{
    [SerializeField] private Animator mAnimator;
    [SerializeField] private InputActionProperty interactleft;
    [SerializeField] private InputActionProperty interactright;
    [SerializeField] private GrabListener grabListenerRight;
    [SerializeField] private GrabListener grabListenerLeft;
    public Transform spawnPoint;
    public GameObject followHandRight;
    public GameObject followHandLeft;
    public GameObject firePrefab;
    private GameObject currentFire;
    private bool bookOpen = false;
    private float cooldown = 1f;
    float trackcooldown;
    string bookRight;
    string bookLeft;
    bool fireactive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (mAnimator != null)
        {
            mAnimator.SetTrigger("Close");
        }    
    }

    void spawnFire(Transform point, GameObject hand)
    {
        if (!fireactive)
        {  
        currentFire = Instantiate(firePrefab, point.position, point.rotation);
       // Debug.Log("Fire = " + currentFire);
        GameObjectFollowScript follow = currentFire.GetComponent<GameObjectFollowScript>();
      //  Debug.Log("Follow script found? " + (follow != null));
        follow.SetHand(hand);
      //  Debug.Log("Hand = " + hand);
        fireactive = true;  
        }
    }

    void despawnFire()
    {
        if (currentFire != null)
        {
            Destroy(currentFire);
            currentFire = null;
            fireactive = false;
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
                bookLeft = null;
            } else if (itemLeft != null)
            {
                bookLeft = itemLeft.transform.name;
                bookRight = null;
            } else
            {
                bookLeft = null;
                bookRight = null;
            }
            
        
            if (valueLeft == 1 && bookLeft == "book" || valueRight == 1 && bookRight == "book")
            {
                //Check here if book is being helf in the right or left hand
                //also check if bookOpen is true, then spawn fire particle effect (Gameobject + particle System) on other hand
                //Allow shooting projectiles

                
                if (bookOpen == false && trackcooldown <= 0) //check for closed book and cooldown 0
                {
                    mAnimator.SetTrigger("Open");
                    bookOpen = true;
                    trackcooldown = cooldown;

                    //Instantiate at specific hand
                    if (valueRight == 1 && bookRight == "book")
                    {
                        spawnFire(spawnPoint, followHandLeft);
                    } else if(valueLeft == 1 && bookLeft == "book")
                    {
                        spawnFire(spawnPoint, followHandRight); 
                    }
                } else if (bookOpen == true && trackcooldown <= 0) //check for opened book and cooldown 0
                {
                    mAnimator.SetTrigger("Close");
                    bookOpen = false;
                    trackcooldown = cooldown;
                    despawnFire(); //despawn current fire;
                }
            }

            Debug.Log("Left: " + bookLeft + "Right: " + bookRight);
            if (bookLeft == null && bookRight == null) //despawn fire if both book become null
            {
                
                despawnFire();
            }

            trackcooldown -= Time.deltaTime;
        }
    }
}
