using UnityEngine;

public class LightScript : MonoBehaviour
{
    public Light light;
    public LightType type;
    public float intensityTime;
    public bool flickerIntensity = false;
    public int intensityMod;
    private float minIntensity, maxIntensity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light = gameObject.GetComponent<Light>();

        light.color = type.color;
        light.intensity = type.minIntensity;
        light.range = type.range;

        if((type.minIntensity < type.maxIntensity) && type.flickerUpDuration <= 0 && type.flickerDownDuration > 0)
        {
            intensityTime = type.flickerDownDuration;
            flickerIntensity = true;
            intensityMod = -1;
        }
        else if((type.minIntensity < type.maxIntensity) && type.flickerUpDuration > 0)
        {
            intensityTime = 0;
            flickerIntensity = true;
            intensityMod = 1;
        }
        else
        {
            light.intensity = type.minIntensity - type.intensityVar/2;
            intensityTime = 0;
            flickerIntensity = false;
            intensityMod = 1;

            if(type.flickerDuration <= 0)
            {
                type.flickerDuration = 1;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(flickerIntensity)
        {
            float ratio = .5f;

            if(intensityMod == 1)
            {
                ratio = intensityTime/type.flickerUpDuration;
            }
            else if(intensityMod == -1)
            {
                ratio = intensityTime/type.flickerDownDuration;
            }

            light.intensity = Mathf.Lerp(type.minIntensity, type.maxIntensity, ratio);

            if(intensityMod < 0 && intensityTime <= 0)
            {
                if(type.flickerUpDuration <= 0)
                {
                    intensityMod = 0;
                    flickerIntensity = false;
                }
                else
                {
                    intensityMod = 1;
                }

                intensityTime = 0;
            }
            else if(intensityMod > 0 && intensityTime >= type.flickerUpDuration)
            {
                if(type.flickerDownDuration <= 0)
                {
                    intensityMod = 0;
                    flickerIntensity = false;
                }
                else
                {
                    intensityMod = -1;
                }

                intensityTime = type.flickerDownDuration;
            }

            intensityTime = intensityTime + (intensityMod*Time.fixedDeltaTime);
        }
        else if(type.intensityVar > 0 && type.flickerDuration > 0)
        {
            float ratio = .5f;

            if(intensityMod > 0)
            {
                ratio = intensityTime/type.flickerDuration;
            }
            else if(intensityMod < 0)
            {
                ratio = intensityTime/type.flickerDuration;
            }
            else
            {
                intensityMod = 1;
                intensityTime = 0;
                ratio = intensityTime/type.flickerDuration;

                if(light.intensity > type.intensityVar/2)
                {
                    minIntensity = light.intensity - type.intensityVar/2;
                    maxIntensity = light.intensity + type.intensityVar/2;
                }
                else
                {
                    minIntensity = light.intensity;
                    maxIntensity = light.intensity + type.intensityVar;
                }
            }

            light.intensity = Mathf.Lerp(minIntensity, maxIntensity, ratio);

            if(intensityMod < 0 && intensityTime <= 0)
            {
                intensityMod = 1;
                intensityTime = 0;
            }
            else if(intensityMod > 0 && intensityTime >= type.flickerDuration)
            {
                intensityMod = -1;
                intensityTime = type.flickerDuration;
            }

            intensityTime = intensityTime + (intensityMod*Time.fixedDeltaTime);
        }
    }
}
