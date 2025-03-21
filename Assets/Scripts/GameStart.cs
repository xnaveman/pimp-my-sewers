using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [Tooltip("Assign the GameObject to fade (should have a RawImage, SpriteRenderer, or Renderer with a color property).")]
    public GameObject targetObject;

    // Duration to wait before beginning the fade.
    public float delayBeforeFade = 3f;
    // Duration over which the alpha will fade to zero.
    public float fadeDuration = 1f;

    void Start()
    {
        if(targetObject != null)
            StartCoroutine(FadeOutRoutine());
    }

    IEnumerator FadeOutRoutine()
    {
        // Wait for the specified delay.
        yield return new WaitForSeconds(delayBeforeFade);

        // Try to get a RawImage first.
        RawImage ri = targetObject.GetComponent<RawImage>();
        if(ri != null)
        {
            Color originalColor = ri.color;
            float elapsed = 0f;
            while(elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(originalColor.a, 0f, elapsed / fadeDuration);
                ri.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }
            ri.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
            
            // After fade is complete, load scene "level 1".
            SceneManager.LoadScene("MainMenu");
            yield break;
        }

        // Otherwise try to get a SpriteRenderer.
        SpriteRenderer sr = targetObject.GetComponent<SpriteRenderer>();
        if(sr != null)
        {
            Color originalColor = sr.color;
            float elapsed = 0f;
            while(elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(originalColor.a, 0f, elapsed / fadeDuration);
                sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
            
            // After fade is complete, load scene "level 1".
            SceneManager.LoadScene("MainMenu");
            yield break;
        }

        // Otherwise try to get a Renderer and fade its first material's color.
        Renderer rend = targetObject.GetComponent<Renderer>();
        if(rend != null && rend.material.HasProperty("_Color"))
        {
            Color originalColor = rend.material.color;
            float elapsed = 0f;
            while(elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(originalColor.a, 0f, elapsed / fadeDuration);
                rend.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }
            rend.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
            
            // After fade is complete, load scene "level 1".
            SceneManager.LoadScene("MainMenu");
        }
    }
}