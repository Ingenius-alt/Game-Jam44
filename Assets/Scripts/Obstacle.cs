using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private string damagetype;
    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.GetComponent<CarStats>().damage(damagetype);
        if (damagetype != "crash")
        {
            gameObject.SetActive(false);
        }
    }
      
}
