using UnityEngine;
using TMPro;

public class ChainTextDet : MonoBehaviour
{
    public TextMeshPro textMeshPro; // ��� TextMeshPro
    [SerializeField]
    private GameObject[] characterColliders; // ���ں� Collider ������Ʈ
    public Vector3 colVec = Vector3.zero;   // ���� Collider ������
    public string onLight = string.Empty;   // ���� ���� ���� ����.

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

        // �ؽ�Ʈ�� �� ���� ������ Ȯ��
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

            if (charInfo.isVisible)
            {
                int meshIndex = charInfo.materialReferenceIndex;
                int vertexIndex = charInfo.vertexIndex;

                // ���ڿ� ������ ���� �迭 ��������
                Color32[] vertexColors = textInfo.meshInfo[meshIndex].colors32;

                // 4���� ���� ������ Ȯ��
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
        // �ؽ�Ʈ ������ �ʱ�ȭ
        TMP_TextInfo textInfo = textMeshPro.textInfo;
        textMeshPro.ForceMeshUpdate();

        
        // ���� ������ŭ Collider ������Ʈ ����
        characterColliders = new GameObject[textInfo.characterCount];

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

            // ������ �ʴ� ���ڴ� ��ŵ
            if (!charInfo.isVisible) continue;

            // ���ں� Collider ������Ʈ ����
            GameObject charCollider = new GameObject($"CharCollider_{i}");
            charCollider.transform.parent = textMeshPro.transform;

            // ���� ��ġ�� ũ�� ����
            Vector3 bottomLeft = textMeshPro.transform.TransformPoint(charInfo.bottomLeft);
            Vector3 topRight = textMeshPro.transform.TransformPoint(charInfo.topRight);

            // �ؽ�Ʈ�� ���� ȸ���� �ݿ��� �߽� ��ġ �� ũ��
            charCollider.transform.position = (bottomLeft + topRight) / 2;
            charCollider.transform.rotation = textMeshPro.transform.rotation; // �ؽ�Ʈ�� ȸ���� ����
            charCollider.transform.localScale = colVec;
            charCollider.transform.localPosition += colVec.z * Vector3.back /2f;

            // BoxCollider �߰� �� �ʱ�ȭ
            BoxCollider boxCollider = charCollider.AddComponent<BoxCollider>();
            boxCollider.isTrigger = true; // Trigger�� ����

            // ChangeTextColor ��ũ��Ʈ�� �߰�
            ChangeTextColor collisionHandler = charCollider.AddComponent<ChangeTextColor>();
            collisionHandler.Initialize(textMeshPro, i);
            

            characterColliders[i] = charCollider;
        }
    }

}
