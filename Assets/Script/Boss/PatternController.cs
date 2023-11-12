using UnityEngine;

public class PatternController : MonoBehaviour
{
    Vector3[] spawn = new Vector3[12]; //배열의 크기는 햇불의 갯수, 스폰 되는 장소 저장
    Vector3 targetposition;
    TouchOnOff too;
    public bool bossspawn = false;
    public int target;

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
            bossspawn = false;
            gameObject.tag = "Untagged";
            Debug.Log("보스의 범위에 들어섰습니다.");
            Debug.Log("보스가 도망갑니다.");
        }
    }

    public void SpawnBoss(GameObject touch)
    {
        for (int i = 0; i < too.touchlight.Length; i++)
        {
            if(too.touches[i] == touch)
            {
                bossspawn = true;
                target = i;
                int a = Random.Range(1, 3);
                targetposition = spawn[a * i];
            }
        }
    }
}