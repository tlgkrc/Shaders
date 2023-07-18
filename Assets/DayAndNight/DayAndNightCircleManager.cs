using UnityEngine;

namespace DayAndNight
{
    public class DayAndNightCircleManager : MonoBehaviour
    {
        [SerializeField]private Vector3 startPosSun;
        [SerializeField]private Vector3 startPosMoon;
        [SerializeField]private float delay;
        [SerializeField]private float steps;
        [SerializeField] private Light sun;
        [SerializeField] private Light moon;
        [SerializeField] private Color skyColorInSunAtTop;
        [SerializeField] private Color skyColorInSunAtSide;
        [SerializeField] private Color skyColorInMoonAtTop;
        [SerializeField] private Color skyColorInMoonAtSide;
        
        private Material _shaderMat;
        private float _timeLeft;
        private static readonly int ZenitColorM = Shader.PropertyToID("_ZenitColorM");
        private const float EulerAngleForAnHour = 15;

        private void Awake()
        {
            _shaderMat = RenderSettings.skybox;

        }

        private void Update()
        {
            transform.Rotate(Vector3.forward * EulerAngleForAnHour/steps * Time.deltaTime);

            var lastAngle = transform.localEulerAngles.z;
            
            if (lastAngle%360<45)
            {
                SetSkyColor(skyColorInSunAtSide);
                moon.enabled = false;
                sun.enabled = true;
                sun.intensity = 1;
                sun.shadowStrength = .002f;
            }
            else if (lastAngle%360>=45 &&lastAngle%360<135 )
            {
                SetSkyColor(skyColorInSunAtSide);
                SetLight(sun,1.5f,.1f,true);
                
            }
            else if (lastAngle%360>=135 &&lastAngle%360<180)
            {
                SetSkyColor(skyColorInSunAtTop);
                SetLight(sun,1f,.5f,false);
            }
            else if (lastAngle%360>=180 &&lastAngle%360<225)
            {
                SetSkyColor(skyColorInMoonAtSide);
                _shaderMat.SetColor(ZenitColorM,skyColorInMoonAtSide);
                moon.enabled = true;
                sun.enabled = false;
                moon.intensity = .7f;
                moon.shadowStrength = .1f;
            }
            else if (lastAngle%360>=225 &&lastAngle%360<315)
            {
                SetSkyColor(skyColorInMoonAtTop);
                SetLight(moon,1,.3f,true);
            }
            else
            {
                SetSkyColor(skyColorInMoonAtSide);
                SetLight(moon,.7f,.1f,false);
            }
        }

        private void SetLight(Light selectedLight,float intensityBorder,float shadowStrengthBorder,bool isShining)
        {
            if (isShining)
            {
                if (selectedLight.intensity < intensityBorder)
                {
                    selectedLight.intensity += .002f;
                }

                if (selectedLight.shadowStrength < shadowStrengthBorder)
                {
                    selectedLight.shadowStrength += .002f;
                }
            }
            else
            {
                if (selectedLight.intensity>intensityBorder)
                {
                    selectedLight.intensity -= .002f;
                }

                if (selectedLight.shadowStrength > shadowStrengthBorder)
                {
                    moon.shadowStrength -= .002f;
                }
            }
        }

        private void SetSkyColor(Color selectedColor)
        {
            if (_shaderMat.GetColor(ZenitColorM) != selectedColor)
            {
                var lerpedColor = Color.Lerp(_shaderMat.GetColor(ZenitColorM), selectedColor, .1f);
                _shaderMat.SetColor(ZenitColorM, lerpedColor);
            }
        }
    }
}