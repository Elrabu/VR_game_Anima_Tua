using UnityEngine;

public class SocketIngredientCheckerScript : MonoBehaviour
{
      void Update()
    {
        var sockets = GetComponentsInChildren<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();

        foreach (var socket in sockets)
        {
            if (socket.hasSelection)
            {
                Debug.Log($"{socket.name} contains {socket.interactablesSelected[0].transform.name}");
            }
            else
            {
                Debug.Log($"{socket.name} is empty");
            }
        }
    }
}
