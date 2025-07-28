using System.Collections;
using UnityEngine;

public class FireBurnOutShadingUI : MonoBehaviour
{
    public Material[] burnMaterials;
    public float burnSpeed = 0.01f;
    private float threshold = 1f;
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
        InitializeMaterial(1f);
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
        InitializeMaterial(1f);
        StartCoroutine(FireBurnOutEffectCoroutine());
    }
    public void FireFadeIn()
    {
        InitializeMaterial(-1f);
        StartCoroutine(FireBurnInEffectCoroutine());
    }

    private IEnumerator FireBurnOutEffectCoroutine()
    {
        if (burnSound != null)
        {
            burnSound.Play();
        }
        while (threshold > -1f)
        {
            threshold -= burnSpeed * 2f;
            foreach (Material mat in burnMaterials)
            {
                mat.SetFloat("_Threshold", threshold);
            }
            yield return null;
        }
        if (burnSound != null)
        {
            burnSound.Stop();
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
        while (threshold < 1f)
        {
            threshold += burnSpeed * 2f;
            foreach (Material mat in burnMaterials)
            {
                mat.SetFloat("_Threshold", threshold);
            }
            yield return null;
        }
        if (burnSound != null)
        {
            burnSound.Stop();
        }
    }
}
