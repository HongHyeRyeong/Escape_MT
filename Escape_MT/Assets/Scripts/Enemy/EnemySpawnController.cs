using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject[] spawnPos;

    int randEnemy;
    int randPos;

    public GameObject curEnemy;

    private Animator animator;
    private bool isOpen;

    enum UpNDown
    {
        Up,
        Down
    }

    [SerializeField] private UpNDown upn;
    [SerializeField] private float speed;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (curEnemy != null)
        {
            float y = curEnemy.transform.position.y;

            Vector3 destination = spawnPos[randPos].transform.position;

            if (destination.y - y > 0.01f || destination.y - y < -0.01f)
            {
                if (upn == UpNDown.Up)
                {
                    curEnemy.transform.Translate(Vector3.down * speed * Time.deltaTime);
                }
                if (upn == UpNDown.Down)
                {
                    curEnemy.transform.Translate(Vector3.up * speed * Time.deltaTime);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameManager.Instance.isPlay || GameManager.Instance.isMiniGame)
            return;

        if (other.gameObject.CompareTag("SpawnPoint"))
        {
            isOpen = true;
            animator.SetBool("isOpen", isOpen);

            randEnemy = Random.Range(0, 3);
            randPos = Random.Range(0, 2);
            curEnemy = Instantiate(enemy[randEnemy], this.transform);
        }

        if (other.gameObject.CompareTag("EndPos"))
        {
            isOpen = false;
            animator.SetBool("isOpen", isOpen);
        }
    }
}
