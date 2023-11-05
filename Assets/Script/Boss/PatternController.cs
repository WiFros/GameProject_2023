using UnityEngine;

public class PatternController : MonoBehaviour
{
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
            gameObject.tag = "Untagged";
            Debug.Log("보스의 범위에 들어섰습니다.");
            Debug.Log("보스가 도망갑니다.");
        }
    }
}