using UnityEngine;
using TMPro;

public class ChainTextDet : MonoBehaviour
{
    public TextMeshPro textMeshPro; // 대상 TextMeshPro
    [SerializeField]
    private GameObject[] characterColliders; // 글자별 Collider 오브젝트
    public Vector3 colVec = Vector3.zero;   // 글자 Collider 사이즈
    public string onLight = string.Empty;   // 불이 들어온 글자 조합.

    public HookAttach hookAttach = null;
    public GuideLine guideLine = null;

    public HookAttach HookReference
    {
        get { return hookAttach; }
    }
    public GuideLine GuideLine
    {
        get { return guideLine; }
    }

    void Start()
    {
        GenerateCharacterColliders();
    }

    public void LogRedText()
    {
        if (textMeshPro == null)
        {
            Debug.LogWarning("TextMeshPro is not assigned.");
            return;
        }

        TMP_TextInfo textInfo = textMeshPro.textInfo;

        string redText = "";

        // 텍스트의 각 문자 정보를 확인
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

            if (charInfo.isVisible)
            {
                int meshIndex = charInfo.materialReferenceIndex;
                int vertexIndex = charInfo.vertexIndex;

                // 문자에 설정된 색상 배열 가져오기
                Color32[] vertexColors = textInfo.meshInfo[meshIndex].colors32;

                // 4개의 정점 색상을 확인
                bool isRed = true;
                for (int j = 0; j < 4; j++)
                {
                    if (vertexColors[vertexIndex + j] != Color.red)
                    {
                        isRed = false;
                        break;
                    }
                }

                if (isRed)
                {
                    redText += charInfo.character;
                }
            }
        }

        if (!string.IsNullOrEmpty(redText))
        {
            Debug.Log($"Red text: {redText}");
            onLight = redText;
        }
        else
        {
            Debug.Log("No red text found.");
        }
    }

    void GenerateCharacterColliders()
    {
        // 텍스트 정보를 초기화
        TMP_TextInfo textInfo = textMeshPro.textInfo;
        textMeshPro.ForceMeshUpdate();

        
        // 글자 개수만큼 Collider 오브젝트 생성
        characterColliders = new GameObject[textInfo.characterCount];

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

            // 보이지 않는 글자는 스킵
            if (!charInfo.isVisible) continue;

            // 글자별 Collider 오브젝트 생성
            GameObject charCollider = new GameObject($"CharCollider_{i}");
            charCollider.transform.parent = textMeshPro.transform;

            // 글자 위치와 크기 설정
            Vector3 bottomLeft = textMeshPro.transform.TransformPoint(charInfo.bottomLeft);
            Vector3 topRight = textMeshPro.transform.TransformPoint(charInfo.topRight);

            // 텍스트의 로컬 회전을 반영한 중심 위치 및 크기
            charCollider.transform.position = (bottomLeft + topRight) / 2;
            charCollider.transform.rotation = textMeshPro.transform.rotation; // 텍스트의 회전값 적용
            charCollider.transform.localScale = colVec;
            charCollider.transform.localPosition += colVec.z * Vector3.back /2f;

            // BoxCollider 추가 및 초기화
            BoxCollider boxCollider = charCollider.AddComponent<BoxCollider>();
            boxCollider.isTrigger = true; // Trigger로 설정

            // ChangeTextColor 스크립트를 추가
            ChangeTextColor collisionHandler = charCollider.AddComponent<ChangeTextColor>();
            collisionHandler.Initialize(textMeshPro, i);
            

            characterColliders[i] = charCollider;
        }
    }

}
