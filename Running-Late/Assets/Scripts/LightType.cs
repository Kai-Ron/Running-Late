using UnityEngine;

[CreateAssetMenu(fileName = "LightType", menuName = "Scriptable Objects/LightType")]
public class LightType : ScriptableObject
{
    public Color color;
    public float minIntensity, maxIntensity, intensityVar;
    public float flickerUpDuration, flickerDownDuration, flickerDuration;
    public float range;
}
