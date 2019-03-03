using UnityEngine;

public class GroundCollision : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Pottery_Bowl_Mesh"))
        {
            Debug.Log(collision.gameObject.name);
        }
        //jump = true;
    }
}
