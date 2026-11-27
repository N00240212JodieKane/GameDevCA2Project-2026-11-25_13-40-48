using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public GameBehavior GameManager;

    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.transform.gameObject);
            GameManager.Items += 1;
        }
    }
}
