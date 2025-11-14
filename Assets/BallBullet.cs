using UnityEngine;

public class BallBullet : BulletScript
{
    public float shootForce;

    public float timeToDestroy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setUpBullet(shootForce, timeToDestroy);
    }

    //Use this to override the base class:
   /* public override void HandleCollision(Collision collision)
    {
        Debug.Log("Child collision" + collision.gameObject.name);
        //call the method of the base class aswell:
        base.HandleCollision(collision);
    } */
}
