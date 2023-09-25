using Cinemachine;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField] float initialIntensity;
    [SerializeField] float shakeTimer;
    [SerializeField] float shakeTimerTotal;
    public static CameraShaker instance { get; private set; }
    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        instance = this;
    }
    public void CameraShake(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cbMultiChanelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cbMultiChanelPerlin.m_AmplitudeGain = intensity;
        initialIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
        return;

    }
    private void Update()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cbMultiChanelPerlin =
                    virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cbMultiChanelPerlin.m_AmplitudeGain = Mathf.Lerp(initialIntensity, 0f, 1f - (shakeTimer / shakeTimerTotal));
            }   
        }
    }
}
