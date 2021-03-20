using UnityEngine;

public class EnemyController : MonoBehaviour
{
    void Start()
    {
        Invoke("EnemyDestroy", 3);
    }

    void EnemyDestroy()
    {
        Destroy(this.gameObject);
    }
}
