using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private bool cone;
    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.GetComponent<CarStats>().damage(cone);
        gameObject.SetActive(false);
    }
      
}
