using UnityEngine;

public class LightScript : MonoBehaviour
{
    public Light light;
    public LightType type;
    public float intensityTime;
    public float rangeTime;
    public bool flickerIntensity = false;
    public bool flickerRange = false;
    public int intensityMod;
    private float minIntensity, maxIntensity;
    public int rangeMod;
    private float minRange, maxRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light = gameObject.GetComponent<Light>();

        light.color = type.color;
        light.intensity = type.minIntensity;
        light.range = type.minRange;

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
            if(light.intensity > type.intensityVar/2)
            {
                minIntensity = light.intensity - type.intensityVar/2;
                maxIntensity = light.intensity + type.intensityVar/2;
            }
            else
            {
                minIntensity = 0;
                maxIntensity = type.intensityVar;
            }

            light.intensity = minIntensity;
            intensityTime = 0;
            flickerIntensity = false;
            intensityMod = 1;

            if(type.flickerDuration <= 0)
            {
                type.flickerDuration = 1;
            }
        }

        if((type.minRange < type.maxRange) && type.flickerUpSpeed <= 0 && type.flickerDownSpeed > 0)
        {
            rangeTime = type.flickerDownSpeed;
            flickerRange = true;
            rangeMod = -1;
        }
        else if((type.minRange < type.maxRange) && type.flickerUpSpeed > 0)
        {
            rangeTime = 0;
            flickerRange = true;
            rangeMod = 1;
        }
        else
        {
            if(light.range > type.rangeVar/2)
            {
                minRange = light.range - type.rangeVar/2;
                maxRange = light.range + type.rangeVar/2;
            }
            else
            {
                minRange = 0;
                maxRange = type.rangeVar;
            }

            light.range = minRange;
            rangeTime = 0;
            flickerRange = false;
            rangeMod = 1;

            if(type.flickerSpeed <= 0)
            {
                type.flickerSpeed = 1;
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
                    minIntensity = 0;
                    maxIntensity = type.intensityVar;
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

        if(flickerRange)
        {
            float ratio = .5f;

            if(rangeMod == 1)
            {
                ratio = rangeTime/type.flickerUpSpeed;
            }
            else if(rangeMod == -1)
            {
                ratio = rangeTime/type.flickerDownSpeed;
            }

            light.range = Mathf.Lerp(type.minRange, type.maxRange, ratio);

            if(rangeMod < 0 && rangeTime <= 0)
            {
                if(type.flickerUpSpeed <= 0)
                {
                    rangeMod = 0;
                    flickerRange = false;
                }
                else
                {
                    rangeMod = 1;
                }

                rangeTime = 0;
            }
            else if(rangeMod > 0 && rangeTime >= type.flickerUpSpeed)
            {
                if(type.flickerDownSpeed <= 0)
                {
                    rangeMod = 0;
                    flickerRange = false;
                }
                else
                {
                    rangeMod = -1;
                }

                rangeTime = type.flickerDownSpeed;
            }

            rangeTime = rangeTime + (rangeMod*Time.fixedDeltaTime);
        }
        else if(type.rangeVar > 0 && type.flickerSpeed > 0)
        {
            float ratio = .5f;

            if(rangeMod > 0)
            {
                ratio = rangeTime/type.flickerSpeed;
            }
            else if(rangeMod < 0)
            {
                ratio = rangeTime/type.flickerSpeed;
            }
            else
            {
                rangeMod = 1;
                rangeTime = 0;
                ratio = rangeTime/type.flickerSpeed;

                if(light.range > type.rangeVar/2)
                {
                    minRange = light.range - type.rangeVar/2;
                    maxRange = light.range + type.rangeVar/2;
                }
                else
                {
                    minRange = 0;
                    maxRange = type.rangeVar;
                }
            }

            light.range = Mathf.Lerp(minRange, maxRange, ratio);

            if(rangeMod < 0 && rangeTime <= 0)
            {
                rangeMod = 1;
                rangeTime = 0;
            }
            else if(rangeMod > 0 && rangeTime >= type.flickerSpeed)
            {
                rangeMod = -1;
                rangeTime = type.flickerSpeed;
            }

            rangeTime = rangeTime + (rangeMod*Time.fixedDeltaTime);
        }
    }
}
