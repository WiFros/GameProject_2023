using UnityEngine;

public class BossManager : MonoBehaviour
{
    public PhaseController[] phases;
    public int currentPhaseIndex = 0;

    private void Start()
    {
        // 초기화 및 첫 번째 페이즈 로드
        phases[currentPhaseIndex].LoadPhase();
    }

    public void MoveToNextPhase()
    {
        // 현재 페이즈 종료
        phases[currentPhaseIndex].EndPhase();

        // 다음 페이즈로 이동
        currentPhaseIndex++;
        if (currentPhaseIndex < phases.Length)
        {
            phases[currentPhaseIndex].LoadPhase();
        }
        else
        {
            // 보스전 종료
            EndBossFight();
        }
    }

    public void EndBossFight()
    {
        // 보스전 종료 로직
    }
}