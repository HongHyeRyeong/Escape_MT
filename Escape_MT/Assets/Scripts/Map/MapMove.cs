using UnityEngine;

public class MapMove : MonoBehaviour
{
    public static MapMove instance = null;

    private float speed = 5;

    public Transform StartPos;
    public Transform EndPos;

    public GameObject[] doors;

    private void Start()
    {
        DoorDelete();
        RandomDoorSpawn();
    }

    void Update()
    {
        if (!GameManager.Instance.isPlay || GameManager.Instance.isMiniGame)
            return;

        transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        PlayerPrefs.SetFloat("speed", speed);
        PlayerPrefs.Save();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EndPos"))
        {
            this.transform.position = new Vector3(StartPos.position.x, transform.position.y, transform.position.z);
            DoorDelete();
            RandomDoorSpawn();
        }
    }

    private void DoorDelete()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].SetActive(false);
        }
    }

    private void RandomDoorSpawn()
    {
        int range1 = Random.Range(0, 4);
        int range2 = Random.Range(0, 4);

        doors[range1].SetActive(true);
        doors[range2].SetActive(true);
    }
}
