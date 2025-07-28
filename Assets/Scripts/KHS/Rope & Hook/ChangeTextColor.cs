
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ChangeTextColor : MonoBehaviour
{
    private HookAttach hookAttach;
    private GuideLine guideLine;
    private TextMeshPro textMeshPro;
    public int characterIndex;
    public List<ChainJudge> chainJudges;

    private void Awake()
    {
        hookAttach = GetComponentInParent<ChainTextDet>().HookReference;
        guideLine = GetComponentInParent<ChainTextDet>().GuideLine;
    }
    private void Start()
    {
        if (hookAttach != null)
        {
            hookAttach.HookArrivedCallback += ChainCallbackInit;
        }
        if(guideLine != null)
        {
            guideLine.GuideArrivedCallback += ChainCallbackUpdate;
        }
        
    }

    private void ChainCallbackInit()
    {
        Debug.Log("체인 콜백들 연결");
        chainJudges = hookAttach.ropeGo.GetComponentsInChildren<ChainJudge>().ToList();
        chainJudges.RemoveAt(0);

        foreach (ChainJudge cj in chainJudges)
        {
            cj.ChainJudgeInCallback += ChangeCharacterColor;
            cj.ChainJudgeOutCallback += ChangeCharacterColor;
        }
        chainJudges.Clear();
    }

    private void ChainCallbackUpdate()
    {
        chainJudges = guideLine.nextChainGo.GetComponentsInChildren<ChainJudge>().ToList();
        chainJudges.RemoveAt(0);

        foreach (ChainJudge cj in chainJudges)
        {
            cj.ChainJudgeInCallback += ChangeCharacterColor;
            cj.ChainJudgeOutCallback += ChangeCharacterColor;
        }
        
    }
    public void Initialize(TextMeshPro _textMeshPro, int _characterIndex)
    {
        Debug.Log("InitialIzed");
        textMeshPro = _textMeshPro;
        characterIndex = _characterIndex;
    }

    void ChangeCharacterColor(Transform _tr, Color _color)
    {
        if (_tr == transform)
        {
            // 글자 색상 변경
            TMP_TextInfo textInfo = textMeshPro.textInfo;
            int meshIndex = textInfo.characterInfo[characterIndex].materialReferenceIndex;
            int vertexIndex = textInfo.characterInfo[characterIndex].vertexIndex;


            Color32[] vertexColors = textInfo.meshInfo[meshIndex].colors32;
            for (int i = 0; i < 4; i++)
            {
                vertexColors[vertexIndex + i] = _color;
            }

            textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }
    }

    private void OnDrawGizmos()
    {
        // 로컬 X축 (빨강)
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right);

        // 로컬 Y축 (초록)
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up);

        // 로컬 Z축 (파랑)
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}

