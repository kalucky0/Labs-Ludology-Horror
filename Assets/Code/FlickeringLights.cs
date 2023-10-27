using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLights : MonoBehaviour
{
    [SerializeField] private Material onMaterial;
    [SerializeField] private Material offMaterial;
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private List<Light> lights;

    private float randomNumber = 0;
    private float timer = 0f;

    private void Start()
    {
        randomNumber = Random.Range(4f, 8f);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > randomNumber)
        {
            timer = 0;
            randomNumber = Random.Range(2f, 8f);
            foreach (Light light in lights)
            {
                StartCoroutine(flickerLight(light));
            }
        }
    }

    IEnumerator flickerLight(Light light)
    {
        float initialIntensity = light.intensity;
        light.intensity = initialIntensity * 0.1f;
        renderer.SetMaterials(new List<Material> { offMaterial });

        yield return new WaitForSeconds(0.1f);
        light.intensity = initialIntensity * 0.4f;
        yield return new WaitForSeconds(0.2f);
        light.intensity = initialIntensity * 0.1f;
        yield return new WaitForSeconds(0.3f);
        light.intensity = initialIntensity * 0.4f;

        yield return new WaitForSeconds(.15f);
        light.intensity = initialIntensity * 0.1f;

        yield return new WaitForSeconds(0.15f);
        light.intensity = initialIntensity * 0.1f;
        yield return new WaitForSeconds(0.2f);
        light.intensity = initialIntensity * 0.4f;
        yield return new WaitForSeconds(0.4f);

        renderer.SetMaterials(new List<Material> { onMaterial });
        light.intensity = initialIntensity;
    }
}
