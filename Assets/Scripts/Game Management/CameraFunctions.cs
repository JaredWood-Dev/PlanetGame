using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraFunctions : MonoBehaviour
{
    public float cameraFOV = 10f;
    private CinemachineCamera _cam;

    void Start()
    {
        _cam = GetComponent<CinemachineCamera>();
    }
    
    public IEnumerator Zoom(float newFOV, float duration)
    {
        float counter = 0f;
        float currentFOV = _cam.Lens.OrthographicSize;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            _cam.Lens.OrthographicSize = Mathf.SmoothStep(currentFOV, newFOV, counter / duration);
            yield return null;
        }

        yield return new WaitForSeconds(duration);

    }

    public void ResetZoom()
    {
        StartCoroutine(Zoom(cameraFOV, 0.5f));
    }
}
