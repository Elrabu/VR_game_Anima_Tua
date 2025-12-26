using UnityEngine;
using UnityEngine.InputSystem;

public class OpenCloseBook : MonoBehaviour
{
    [SerializeField] private Animator mAnimator;
    [SerializeField] private InputActionProperty interactleft;
    [SerializeField] private InputActionProperty interactright;
    [SerializeField] private GrabListener grabListenerRight;
    [SerializeField] private GrabListener grabListenerLeft;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject followHandRight;
    [SerializeField] private GameObject followHandLeft;
    [SerializeField] private GameObject firePrefab;
    private GameObject currentFire;
    private float cooldown = 1f;
    private float trackcooldown;
    private string bookRight;
    private string bookLeft;
    private string lastBookHand = null;
    private string currentBookHand = null;
    private bool fireactive = false;
    private Transform handshootScript;
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
        
        handshootScript = hand.transform.GetChild(0);
        handshootScript.gameObject.SetActive(true); //Activate fireball shooter
        }
    }

    void despawnFire()
    {
        if (currentFire != null)
        {
            var ps = currentFire.GetComponent<ParticleSystem>();
            var ps_main = ps.main;
            //use Unity built in System to let the particles fade out
            ps_main.stopAction = ParticleSystemStopAction.Destroy;
            ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);

            currentFire = null;
            fireactive = false;

            handshootScript.gameObject.SetActive(false); //Deactivate fireball shooter
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
            if (itemRight != null) //assign values if book is in right hand
            {
                bookRight = itemRight.transform.name;
                bookLeft = null;
                currentBookHand = "right";
            } else if (itemLeft != null) //assign values if book is in left hand
            {
                bookLeft = itemLeft.transform.name;
                bookRight = null;
                currentBookHand = "left";
            } else //set values to null if book was dropped
            {
                bookLeft = null;
                bookRight = null;
            }
            
        
            if (valueLeft == 1 && bookLeft == "book" || valueRight == 1 && bookRight == "book")
            {
                //Check here if book is being helf in the right or left hand
                //also check if bookOpen is true, then spawn fire particle effect (Gameobject + particle System) on other hand
                //Allow shooting projectiles
                
                if (mAnimator.GetBool("IsOpen") == false && trackcooldown <= 0) //check for closed book and cooldown 0
                {
                    mAnimator.SetTrigger("Open");
                    mAnimator.SetBool("IsOpen", true);
                    trackcooldown = cooldown;

                    //Instantiate at specific hand
                    if (valueRight == 1 && bookRight == "book")
                    {
                        spawnFire(spawnPoint, followHandLeft);
                    } else if(valueLeft == 1 && bookLeft == "book")
                    {
                        spawnFire(spawnPoint, followHandRight); 
                    }
                } else if (mAnimator.GetBool("IsOpen") == true && trackcooldown <= 0) //check for opened book and cooldown 0
                {
                    mAnimator.SetTrigger("Close");
                    mAnimator.SetBool("IsOpen", false);
                    trackcooldown = cooldown;
                    despawnFire(); //despawn current fire;
                }
            }

           // Debug.Log("Left: " + bookLeft + "Right: " + bookRight);
            if (bookLeft == null && bookRight == null) //despawn fire if both book become null
            {
                despawnFire();
            }
            if (lastBookHand != currentBookHand)
            {
                despawnFire();
            }

            trackcooldown -= Time.deltaTime;
        }
        lastBookHand = currentBookHand;
    }
}
