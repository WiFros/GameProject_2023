using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthBar; // 체력바 UI 요소를 참조하는 변수
    public static UIManager Instance { get; private set; }

    public Toggle pushNotificationToggle;
    public Slider soundFxSlider;
    public Slider backgroundMusicSlider;
    public Toggle vibrationToggle;
    public TextMeshProUGUI actionText;
    public bool PushNotificationEnabled => pushNotificationToggle.isOn;
    public float SoundFxLevel => soundFxSlider.value;
    public float BackgroundMusicLevel => backgroundMusicSlider.value;
    public bool VibrationEnabled => vibrationToggle.isOn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
//        pushNotificationToggle.isOn = PlayerPrefs.GetInt("PushNotification", 1) == 1;
        //soundFxSlider.value = PlayerPrefs.GetFloat("SoundFx", 1f);
        //backgroundMusicSlider.value = PlayerPrefs.GetFloat("BackgroundMusic", 1f);
        //vibrationToggle.isOn = PlayerPrefs.GetInt("Vibration", 1) == 1;
    }

    public void OnPushNotificationToggleChanged()
    {
        PlayerPrefs.SetInt("PushNotification", pushNotificationToggle.isOn ? 1 : 0);
    }

    public void OnSoundFxSliderChanged()
    {
        PlayerPrefs.SetFloat("SoundFx", soundFxSlider.value);
        // 필요하다면 여기서 즉시 사운드 볼륨을 변경하는 로직을 추가합니다.
    }

    public void OnBackgroundMusicSliderChanged()
    {
        PlayerPrefs.SetFloat("BackgroundMusic", backgroundMusicSlider.value);
        // 필요하다면 여기서 즉시 배경음악 볼륨을 변경하는 로직을 추가합니다.
    }

    public void OnVibrationToggleChanged()
    {
        PlayerPrefs.SetInt("Vibration", vibrationToggle.isOn ? 1 : 0);
    }
    // 체력바 UI 업데이트 메소드
    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }
    public void HideUI(GameObject ui)
    {
        ui.SetActive(false);
    }
}
