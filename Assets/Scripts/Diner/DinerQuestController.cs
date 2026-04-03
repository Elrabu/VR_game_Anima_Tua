using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DinerQuestController : MonoBehaviour
{
    private const float GlitchChangeInterval = 0.15f;
    private const int GlitchStringLength = 13;
    private const string GlitchCharacters = "!@#$%^&*()_+-=[]{}|;:',.<>?/`~ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private const string QuestEndText = "Lonely You... I...";

    [Header("Audio")]
    [SerializeField] private AudioSource jukeboxNormAudio;
    [SerializeField] private AudioSource jukeboxDistortion1Audio;
    [SerializeField] private AudioSource jukeboxDistAudio;

    [Header("Quest End VFX")]
    [SerializeField] private ParticleSystem questEndParticleSystem;

    [Header("Mix")]
    [SerializeField] private float crossfadeDuration = 3.0f;

    [Header("Glitches")]
    [SerializeField] private GameObject glitchingText;

    [Header("Light Flicker")]
    [SerializeField] private float flickerDuration = 5.0f;

    private bool questEnded = false;
    private Text glitchTextComponent;
    private float glitchTimer = 0.0f;
    private bool isGlitching = false;
    private LightConeFlicker lightConeFlicker;

    public bool QuestEnded => questEnded;

    private void Awake()
    {
        StopQuestEndParticlesAtStart();
        InitializeMusicLayers();
        InitializeGlitchText();
        InitializeLightFlicker();
    }

    private void Update()
    {
        if (isGlitching)
        {
            UpdateGlitch();
        }
    }

    private void InitializeGlitchText()
    {
        if (glitchingText == null)
        {
            Debug.LogWarning("DinerQuestController: Glitching text GameObject reference is missing.");
            return;
        }

        glitchTextComponent = glitchingText.GetComponent<Text>();
        if (glitchTextComponent == null)
        {
            Debug.LogWarning("DinerQuestController: Glitching text does not have a Text component.");
            return;
        }

        StartGlitching();
    }

    private void InitializeLightFlicker()
    {
        lightConeFlicker = GetComponent<LightConeFlicker>();
        if (lightConeFlicker == null)
            Debug.LogWarning("DinerQuestController: LightConeFlicker component not found.");
    }

    private void StartGlitching()
    {
        isGlitching = true;
        glitchTimer = 0.0f;
    }

    private void StopGlitching()
    {
        isGlitching = false;
    }

    private void UpdateGlitch()
    {
        glitchTimer += Time.deltaTime;

        if (glitchTimer >= GlitchChangeInterval)
        {
            glitchTimer = 0.0f;
            glitchTextComponent.text = GenerateRandomGlitchString();
        }
    }

    private string GenerateRandomGlitchString()
    {
        string result = string.Empty;
        for (int i = 0; i < GlitchStringLength; i++)
        {
            result += GlitchCharacters[Random.Range(0, GlitchCharacters.Length)];
        }
        return result;
    }

    public void HandleFirstOrder()
    {
        if (questEnded)
            return;

        StartCoroutine(CrossfadeToDistortion1());
    }

    public void HandleQuest()
    {
        if (questEnded)
            return;

        questEnded = true;
        StopGlitching();

        if (glitchTextComponent != null)
            glitchTextComponent.text = QuestEndText;

        PlayQuestEndParticles();
        StartCoroutine(CrossfadeToFullDistortion());

        if (lightConeFlicker != null)
            lightConeFlicker.StartFlicker(flickerDuration);

        Debug.Log("Diner quest ended.");
    }

    private void InitializeMusicLayers()
    {
        if (!ValidateJukeboxSources())
            return;

        if (!jukeboxNormAudio.isPlaying)
            jukeboxNormAudio.Play();

        if (!jukeboxDistortion1Audio.isPlaying)
            jukeboxDistortion1Audio.Play();

        if (!jukeboxDistAudio.isPlaying)
            jukeboxDistAudio.Play();

        jukeboxNormAudio.volume = 1.0f;
        jukeboxDistortion1Audio.volume = 0.0f;
        jukeboxDistAudio.volume = 0.0f;
    }

    private bool ValidateJukeboxSources()
    {
        if (jukeboxNormAudio == null || jukeboxDistortion1Audio == null || jukeboxDistAudio == null)
        {
            Debug.LogWarning("DinerQuestController: Missing jukebox AudioSource references.");
            return false;
        }

        return true;
    }

    private IEnumerator CrossfadeToDistortion1()
    {
        if (!ValidateJukeboxSources())
            yield break;

        float startNorm = jukeboxNormAudio.volume;
        float startDist1 = jukeboxDistortion1Audio.volume;
        float elapsed = 0.0f;

        while (elapsed < crossfadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / crossfadeDuration);

            jukeboxNormAudio.volume = Mathf.Lerp(startNorm, 0.0f, t);
            jukeboxDistortion1Audio.volume = Mathf.Lerp(startDist1, 1.0f, t);

            yield return null;
        }

        jukeboxNormAudio.volume = 0.0f;
        jukeboxDistortion1Audio.volume = 1.0f;
    }

    private IEnumerator CrossfadeToFullDistortion()
    {
        if (!ValidateJukeboxSources())
            yield break;

        float startDist1 = jukeboxDistortion1Audio.volume;
        float startDist = jukeboxDistAudio.volume;
        float elapsed = 0.0f;

        while (elapsed < crossfadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / crossfadeDuration);

            jukeboxDistortion1Audio.volume = Mathf.Lerp(startDist1, 0.0f, t);
            jukeboxDistAudio.volume = Mathf.Lerp(startDist, 1.0f, t);

            yield return null;
        }

        jukeboxDistortion1Audio.volume = 0.0f;
        jukeboxDistAudio.volume = 1.0f;
    }

    private void PlayQuestEndParticles()
    {
        if (questEndParticleSystem == null)
        {
            Debug.LogWarning("DinerQuestController: Quest end particle system reference is missing.");
            return;
        }

        questEndParticleSystem.Play();
    }

    private void StopQuestEndParticlesAtStart()
    {
        if (questEndParticleSystem == null)
            return;

        questEndParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}
