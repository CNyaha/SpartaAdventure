using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition stamina;

    private void Awake()
    {
        Condition[] conditions = GetComponentsInChildren<Condition>();

        foreach (Condition cdt in conditions)
        {
            switch (cdt.gameObject.name)
            {
                case "Health":
                    if (health != null) break;
                    health = cdt;
                    break;

                case "Stamina":
                    if (stamina != null) break;
                    stamina = cdt;
                    break; 
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
