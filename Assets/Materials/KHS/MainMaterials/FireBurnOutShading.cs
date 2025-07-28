using System.Collections;
using UnityEngine;

public class FireBurnOutShading : MonoBehaviour
{
    public Material[] burnMaterials;
    public float burnSpeed = 0.01f;
    private float threshold = 0.5f;
    private AudioSource burnSound;

    private void Awake()
    {
        burnMaterials = GetComponent<MeshRenderer>().materials;
        burnSound = GetComponent<AudioSource>();
    }
    private void Start()
    {
        if (burnSound != null)
        {
            burnSound.Stop();
        }
        InitializeMaterial(0.5f);
    }

    public void InitializeMaterial(float _threshold)
    {
        threshold = _threshold; // 초기 Threshold 값 설정
        foreach (Material mat in burnMaterials)
        {
            mat.SetFloat("_Threshold", _threshold); // Material 초기화
        }
    }

    public void FireFadeOut()
    {
        InitializeMaterial(0.5f);
        StartCoroutine(FireBurnOutEffectCoroutine());
    }
    public void FireFadeIn()
    {
        InitializeMaterial(-0.5f);
        StartCoroutine(FireBurnInEffectCoroutine());
    }

    private IEnumerator FireBurnOutEffectCoroutine()
    {
        if (burnSound != null)
        {
            burnSound.Play();
        }
        while (threshold > -0.5f)
        {
            threshold -= burnSpeed;
            foreach (Material mat in burnMaterials)
            {
                mat.EnableKeyword("_EffectOn");
                mat.SetFloat("_Threshold", threshold);
            }
            yield return null;
        }
        if (burnSound != null)
        {
            burnSound.Stop();
        }
        foreach (Material mat in burnMaterials)
        {
            mat.DisableKeyword("_EffectOn");
        }
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }
    private IEnumerator FireBurnInEffectCoroutine()
    {
        if (burnSound != null)
        {
            burnSound.Play();
        }
        while (threshold < 0.5f)
        {
            threshold += burnSpeed;
            foreach (Material mat in burnMaterials)
            {
                mat.EnableKeyword("_EffectOn");
                mat.SetFloat("_Threshold", threshold);
            }
            yield return null;
        }
        if (burnSound != null)
        {
            burnSound.Stop();
        }
        foreach (Material mat in burnMaterials)
        {
            mat.DisableKeyword("_EffectOn");
        }
    }
}
