using UnityEngine;
using System.Collections;

public class ParticleTest_Script : MonoBehaviour
{
    public GameObject spawnedParticle;
    public Transform spawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject particle = Instantiate(spawnedParticle, spawnPoint.position, spawnPoint.rotation);
        particle.GetComponent<ParticleSystem>().Play();
    }
}
