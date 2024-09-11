using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    public Light pointLight;
    public AudioSource audioSource;
    public Color triggeredLightColor = Color.red;
    private Color originalLightColor;

    public Renderer targetRenderer;
    private MaterialPropertyBlock propertyBlock;
    private Color originalEmissionColor;
    public Color triggeredEmissionColor = Color.red * 10f;
    private Coroutine emissionCoroutine;
    private float timeToChange = 3f;

    void Start()
    {
        // Save the original color of the Point Light
        if (pointLight != null)
        {
            originalLightColor = pointLight.color;
        }

        // Initialize MaterialPropertyBlock
        propertyBlock = new MaterialPropertyBlock();

        // Get the initial emission color of the target object
        if (targetRenderer != null)
        {
            targetRenderer.GetPropertyBlock(propertyBlock);
            originalEmissionColor = targetRenderer.sharedMaterial.GetColor("_EmissionColor");
            propertyBlock.SetColor("_EmissionColor", originalEmissionColor);
            targetRenderer.SetPropertyBlock(propertyBlock);
        }
    }

    // When the player enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Ensure only the player triggers
        {
            // Change the color of the Point Light
            if (pointLight != null)
            {
                pointLight.color = triggeredLightColor;
            }

            // Loop audio playback
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.loop = true;  // Set the audio to loop
                audioSource.Play();  // Play the audio
            }

            // Smoothly change the emission color of the target object
            if (targetRenderer != null)
            {
                if (emissionCoroutine != null) StopCoroutine(emissionCoroutine);
                emissionCoroutine = StartCoroutine(ChangeEmissionColor(targetRenderer, originalEmissionColor, triggeredEmissionColor, timeToChange)); // Duration of 3 seconds
            }
        }
    }

    // When the player leaves the trigger zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // Ensure only the player triggers
        {
            // Restore the original color of the Point Light
            if (pointLight != null)
            {
                pointLight.color = originalLightColor;
            }

            // Stop audio playback
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();  // Stop the audio
                audioSource.loop = false;  // Cancel audio loop
            }

            // Smoothly restore the emission color of the target object
            if (targetRenderer != null)
            {
                if (emissionCoroutine != null) StopCoroutine(emissionCoroutine);
                emissionCoroutine = StartCoroutine(ChangeEmissionColor(targetRenderer, triggeredEmissionColor, originalEmissionColor, timeToChange)); // Duration of 3 seconds
            }
        }
    }

    // Coroutine to smoothly change the emission color
    private IEnumerator ChangeEmissionColor(Renderer renderer, Color fromColor, Color toColor, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            Color newColor = Color.Lerp(fromColor, toColor, time / duration);  // Interpolate to calculate the new emission color
            propertyBlock.SetColor("_EmissionColor", newColor);
            renderer.SetPropertyBlock(propertyBlock);
            yield return null;
        }

        // Ensure the final color is correctly set
        propertyBlock.SetColor("_EmissionColor", toColor);
        renderer.SetPropertyBlock(propertyBlock);
    }
}