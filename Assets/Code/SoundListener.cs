using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundListener : MonoBehaviour
{
    public AudioSource source;
    public float updateStep = 0.1f;
    public int sampleDataLength = 512;

    private float currentUpdateTime = 0f;
    public float samplePower = 10f;
    public float smoothTime = 1;

    public float clipLoudness;
    private float[] clipSampleData;
    public Pass pass;
    public enum Pass
    {
        Low,
        Mid,
        High
    }
    public GameObject sprite;
    public float sizeFactor;
    public bool xScale, yScale, zScale;

    public float minSize = 0f;
    public float maxSize = 500f;

    void Awake()
    {
        clipSampleData = new float[sampleDataLength];
    }
    void Update()
    {
        currentUpdateTime += Time.deltaTime;
        if(currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0f;
            source.GetSpectrumData(clipSampleData, 0, FFTWindow.Blackman);
            int samples = 0;
            foreach (var sample in clipSampleData)
            {
                samples++;
                switch (pass)
                {
                    case Pass.Low:
                        clipLoudness += samples <= 10 ? Mathf.Abs(sample) * samplePower : 0;
                        break;
                    case Pass.Mid:
                        clipLoudness += samples <= 70 && samples <= 50 ? Mathf.Abs(sample) * samplePower : 0;
                        break;
                    case Pass.High:
                        clipLoudness += samples >= 70 ? Mathf.Abs(sample) * samplePower : 0;
                        break;
                }
                    
            }
            clipLoudness /= sampleDataLength;

            clipLoudness *= sizeFactor;
            clipLoudness = Mathf.Clamp(clipLoudness+minSize, minSize, maxSize);
            sprite.transform.localScale = Vector3.Lerp(sprite.transform.localScale, new Vector3(
                xScale ? clipLoudness : sprite.transform.localScale.x,
                yScale ? clipLoudness : sprite.transform.localScale.y,
                zScale ? clipLoudness : sprite.transform.localScale.z),
                smoothTime);
        }
    }
}
