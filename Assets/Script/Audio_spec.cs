using UnityEngine;

public class Audio_spec : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip audioClip;

    private float[] samples;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = audioClip;
        samples = new float[2048];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Awake();
        
    }

    float time = 0;

    // Update is called once per frame
    void Update()
    {
        source.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);
        time += Time.deltaTime;

        if((time % 1) >= 0) { }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (samples == null) return;
        for (int i = 0; i < samples.Length; i++)
        {
            float x = i * 0.1f;
            float y = samples[i] * 10;
            Gizmos.DrawLine(new Vector3(x, 0, 0), new Vector3(x, y, 0));
        }
    }
}
