using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlateSocketScript : MonoBehaviour
{
   /* [SerializeField] private GameObject tape;
    [SerializeField] private Transform tapespawn; */
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;
    private bool spawned = false;
    private bool plate = false;

    private AudioSource serveOrderAudio;
    private AudioSource jukeboxDistAudio;
    private AudioSource jukeboxNormAudio;

    private Light devLight;

    public bool filledplate => plate;

    private void OnEnable()
    {
        socket.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {   //casts args.interactableObject to XRGrabInteractable and assigns it to "grab"
        if (args.interactableObject is UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab) 
        {
           checkburger(grab);
           SetColliders(grab.transform, false);
        }

    }
    private void SetColliders(Transform root, bool enabled)
    {
         Collider[] colliders = root.GetComponentsInChildren<Collider>(true);

        foreach (Collider col in colliders)
        {
            col.enabled = enabled;
        }
    }

    private void checkburger(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab)
    {
         //check completed burger for completed var
        var checker = grab.GetComponent<SocketIngredientCheckerScript>();

        if (checker == null)
            return;

        if (checker.CompletedBurger)
        {
            if (spawned == false)
            {
                Debug.Log("Completed burger!");
                //  Instantiate(tape, tapespawn.position, tapespawn.rotation);
                // 
                // Sound change here
                //
                handleQuest();
            }

            spawned = true;
            plate = true;
            
        }
        else
        {
            Debug.Log("Burger not completed yet");
        }
    }

    private void handleQuest()
    {
        GameObject serveOrderObject = GameObject.Find("ServeOrder");
        GameObject jukeboxDistObject = GameObject.Find("JukeboxDistortedAudio");
        GameObject jukeboxNormObject = GameObject.Find("JukeboxNormalAudio");

        GameObject delivLightObj = GameObject.Find("DeliveryLight");

        if (serveOrderObject != null && jukeboxDistObject != null)
        {
            serveOrderAudio = serveOrderObject.GetComponent<AudioSource>();
            jukeboxDistAudio = jukeboxDistObject.GetComponent<AudioSource>();
            jukeboxNormAudio = jukeboxNormObject.GetComponent<AudioSource>();

            devLight = delivLightObj.GetComponent<Light>();

            if (serveOrderAudio == null)
            {
                Debug.LogWarning("ServeOrder GameObject found, but it has no AudioSource component!");
            }
            else
            {
                serveOrderAudio.Play();
                jukeboxDistAudio.volume = 1.0f;
                jukeboxNormAudio.volume = 0.0f;

                devLight.intensity = 40000.0f;
            }
        }
        else
        {
            Debug.LogWarning("ServeOrder GameObject not found in the scene!");
        }
    }
}
