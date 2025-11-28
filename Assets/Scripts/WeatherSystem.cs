using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    public Light directionalLight;
    public Material skyboxMaterial;
    public ParticleSystem rainParticles;
    public ParticleSystem fogParticles;

    public float dayLength = 1440f; // 24 minutes in game time
    public float timeOfDay = 0f; // 0-1, 0 = midnight, 0.5 = noon

    public enum WeatherType { Clear, Rainy, Foggy, Stormy }
    public WeatherType currentWeather = WeatherType.Clear;

    void Start()
    {
        if (directionalLight == null)
        {
            directionalLight = FindObjectOfType<Light>();
        }
        UpdateLighting();
        InvokeRepeating("ChangeWeather", 300f, 600f); // Change weather every 5-10 minutes
    }

    void Update()
    {
        timeOfDay += Time.deltaTime / dayLength;
        if (timeOfDay >= 1f) timeOfDay = 0f;
        UpdateLighting();
    }

    void UpdateLighting()
    {
        float sunAngle = timeOfDay * 360f;
        directionalLight.transform.rotation = Quaternion.Euler(sunAngle - 90f, 170f, 0f);

        // Adjust light intensity based on time
        float intensity = Mathf.Clamp01(Mathf.Sin(sunAngle * Mathf.Deg2Rad));
        directionalLight.intensity = intensity * 1.5f;

        // Skybox color
        if (skyboxMaterial != null)
        {
            Color skyColor = Color.Lerp(Color.black, Color.cyan, intensity);
            skyboxMaterial.SetColor("_Tint", skyColor);
        }
    }

    void ChangeWeather()
    {
        currentWeather = (WeatherType)Random.Range(0, System.Enum.GetValues(typeof(WeatherType)).Length);
        ApplyWeatherEffects();
    }

    void ApplyWeatherEffects()
    {
        switch (currentWeather)
        {
            case WeatherType.Clear:
                rainParticles.Stop();
                fogParticles.Stop();
                directionalLight.intensity *= 1.2f;
                break;
            case WeatherType.Rainy:
                rainParticles.Play();
                fogParticles.Stop();
                directionalLight.intensity *= 0.7f;
                break;
            case WeatherType.Foggy:
                rainParticles.Stop();
                fogParticles.Play();
                directionalLight.intensity *= 0.5f;
                RenderSettings.fog = true;
                RenderSettings.fogDensity = 0.02f;
                break;
            case WeatherType.Stormy:
                rainParticles.Play();
                fogParticles.Play();
                directionalLight.intensity *= 0.3f;
                RenderSettings.fog = true;
                RenderSettings.fogDensity = 0.05f;
                break;
        }
    }
}