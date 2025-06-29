using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.GetComponent<CarStats>().objectiveFulfilled();
        gameObject.SetActive(false);
    }
}
