using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI[] Texts;
    public TextMeshProUGUI promptText;
    private Camera camera;

    private PlayerController controller;

    private string canvasName = "Canvas";


    private void Awake()
    {
        GameObject canvasObject = GameObject.Find(canvasName);
        if (canvasObject != null)
        {
            Texts = canvasObject.GetComponentsInChildren<TextMeshProUGUI>();
            if (Texts != null)
            {
                foreach (var text in Texts)
                {
                    switch (text.name)
                    {
                        case "PromptText":
                            promptText = text;
                            Debug.Log("PromptText ���� �Ϸ�");
                            promptText.gameObject.SetActive(false);
                            break;

                        default:
                            Debug.LogError("������ �� �ִ� �ؽ�Ʈ�� �����ϴ�.");
                            break;
                    }
                }
                
            }
            else
            {
                Debug.LogError("Texts�� �ƹ��͵� �����ϴ�.");
            }
        }
        else
        {
            Debug.LogError("canvasObject�� ã�� �� �����ϴ�.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        controller = CharacterManager.Instance.Player.controller;

        controller.interation += Interact;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3((Screen.width / 2), (Screen.height / 2)));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void Interact()
    {
        if (curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }

}
