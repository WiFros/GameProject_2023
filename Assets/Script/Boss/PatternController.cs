using UnityEngine;

public class PatternController : MonoBehaviour
{
    public Vector3[] spawn = new Vector3[12]; //배열의 크기는 햇불의 갯수, 스폰 되는 장소 저장
    Vector3[] spawnsequence = new Vector3[3];
    bool[] touchnumber = new bool[4];
int[] touchsequence = new int[3];
Vector3 targetposition;
public bool bossspawn = false;
public int target;
public GameObject player;


private void Update()
{
    transform.position = Vector3.MoveTowards(gameObject.transform.position, targetposition, 0.1f); //목표를 향해 이동)
    }
public void StartPattern()
{
    //패턴 시작 로직
    }

public void EndPattern()
{
    //패턴 종료 로직
    }

private void OnTriggerEnter(Collider other)
{
    if (other.gameObject.tag == "Player")
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
//touch 번호를 받아와 거리 계산을 통해 스폰지점 배열값을 정해줌
public int SelectSpawnPoint(int touchnum)
{
    int sequence = 0;
    float distance = 0;
    int a = Random.Range(0, 3);
    for (int j = 0; j < 3; j++)
        {
        float b = Mathf.Sqrt(Mathf.Pow(player.transform.position.x - transform.position.x, 2) + Mathf.Pow(player.transform.position.z - transform.position.z, 2));
        if (distance < b)
            {
            sequence = j; //스폰될 지점 번호 spawn[sequence + i]로 저장될 예정
            }
    }
    return sequence + touchnum * 3; //스폰지점의 배열 인덱스
    }
//랜덤으로 touch가 켜져있는 번호를 찾아내 반환
    public int SpawnSequence()
{
    int a = Random.Range(0, 4);
    for (int i = 0; i < touchnumber.Length; i++)
        {
        if (touchnumber[a] == true)
        {
            break;
        }
        else if (a == 3)
        {
            a = 0;
        }
        else
        {
            a++;
        }
    }
    return a;
}
//켜진 touch 번호를 받아와 알려줌
    public void TouchOnOffControl(int touchnum, bool touchlight)
{
    touchnumber[touchnum] = touchlight;
    Invoke("SpawnBoss", 5);
    for (int i = 0; i < touchnumber.Length; i++)
        {
        if (touchnumber[i] == true)
        {
            return;
        }
    }
    CancelInvoke("SpawnBoss");
}
//켜진 touch번호를 랜덤으로 받고 그 번호를 이용해 타겟의 좌표와 스폰 좌표를 생성
    public void SpawnBoss()
{
    if (bossspawn == true)
    {
        CancelInvoke("SpawnBoss");
    }
    int touchnum = SpawnSequence();
    Debug.Log(spawn[SelectSpawnPoint(touchnum)]);

    //끄러갈 횃불의 위치 저장 스위치
        switch (touchnum)
    {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
    }
    targetposition = new Vector3(11.59f, -1.78f, -24.45f);
}
//보스 제거
    public void BossDisappear()
{
    gameObject.SetActive(false);
    bossspawn = false;
    Invoke("SpawnBoss", 5);
}
//보스 생성
    public void BossAppear()
{
    gameObject.tag = "Boss";
    bossspawn = true;
    gameObject.SetActive(true);
}

private void OnTriggerStay(Collider other)
{
    if (other.gameObject.tag == "Player")
    {
        RunBoss();
    }
}
}