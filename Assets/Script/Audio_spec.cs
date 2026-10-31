using UnityEngine;

public class Audio_spec : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip audioClip;

    [SerializeField] int check = 3;

    private const int BAR_COUNT = 48;
    private const int SAMPLE = 2048;
    private const float F_MAX = 16000.0f;
    private const float F_MIN = 30.0f;

    private Vector2[] Bar_Limit;

    private float[] samples;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = audioClip;
        samples = new float[SAMPLE];
        Bar_Limit = new Vector2[BAR_COUNT];
        float pow = 0.0f;
        for(int i = 0; i < BAR_COUNT; i++)
        {  
            if(i < 1) { Bar_Limit[0].x = 0.0f; } else { Bar_Limit[i].x = Bar_Limit[i - 1].y; }
            pow = i / (float)(BAR_COUNT);
            Bar_Limit[i].y = F_MIN * Mathf.Pow((F_MAX / F_MIN), pow);
            Debug.Log("index: " + i + "  min: " + Bar_Limit[i].x + "  max: " + Bar_Limit[i].y + "  pow: " + pow);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Awake();
    }

    // Update is called once per frame
    void Update()
    {
        source.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);
        
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
