using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBurnOutShadingChain : MonoBehaviour
{
    public MeshRenderer[] renderers;
    public List<Material> burnMaterials;
    public float burnSpeed = 0.01f;
    private float threshold = 0.5f;
    private AudioSource burnSound;

    public bool debugTr = false;

    private void Awake()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
        burnSound = GetComponent<AudioSource>();
    }
    private void Start()
    {
        foreach (MeshRenderer ren in renderers)
        {
            burnMaterials.AddRange(ren.materials);
        }
        if (burnSound != null)
        {
            burnSound.Stop();
        }
        InitializeMaterial(0.5f);
    }

    private void Update()
    {
        if(debugTr && Input.GetKeyDown(KeyCode.L))
        {
            FireFadeOut();
        }
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
