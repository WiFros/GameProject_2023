using UnityEngine;

public class PatternController : MonoBehaviour
{
    Vector3[] spawn = new Vector3[12]; //배열의 크기는 햇불의 갯수, 스폰 되는 장소 저장
    Vector3[] spawnsequence = new Vector3[3];
    Vector3 targetposition;
    TouchOnOff too;
    public bool bossspawn = false;
    public int target;
    public GameObject player;

    private void Start()
    {
        too = GameObject.Find("TouchManager").GetComponent<TouchOnOff>(); //토치 매니저에서 토치온오프 함수 가져오기
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, targetposition, 10); //목표를 향해 이동)
    }
    public void StartPattern()
    {
        // 패턴 시작 로직
    }

    public void EndPattern()
    {
        // 패턴 종료 로직
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            RunBoss();
        }
    }

    public void RunBoss()
    {
        bossspawn = false;
        gameObject.tag = "Untagged";
        Debug.Log("보스의 범위에 들어섰습니다.");
        Debug.Log("보스가 도망갑니다.");
    }

    public void SpawnBoss(GameObject touch)
    {
        for (int i = 0; i < too.touchlight.Length; i++)
        {
            if(too.touches[i] == touch)
            {
                float distance = 0;
                int sequence = 1;
                bossspawn = true;
                target = i;
                int a = Random.Range(0, 3);
                for (int j = 0; j < 3; j++)
                {
                    float b = Mathf.Sqrt(Mathf.Pow(player.transform.position.x - transform.position.x, 2) + Mathf.Pow(player.transform.position.z - transform.position.z, 2));
                    if (distance < b)
                    {
                        sequence = j + 1; //스폰될 지점 번호 spawn[sequence+i]로 저장될 예정
                    }
                }
                
            }
        }
    }

    public void BossDisappear()
    {
        gameObject.SetActive(false);
    }

    public void BossAppear()
    {
        gameObject.SetActive(true);
    }
}