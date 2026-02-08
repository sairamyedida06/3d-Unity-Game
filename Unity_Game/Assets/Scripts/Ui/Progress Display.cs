using TMPro;
using UnityEngine;

public class ProgressDisplay : MonoBehaviour
{

    [SerializeField]TextMeshProUGUI RemianingCoinsLabel;
    [SerializeField] GameObject MainPanel;
    [SerializeField] GameObject SuccessPanel;


    private void Start()
    {
        MainPanel.SetActive(true);
        SuccessPanel.SetActive(false);
    }



    public void SetRemainigCoints(int amount)
    {
        RemianingCoinsLabel.text = amount.ToString();

        if(amount > 0)
        {
            MainPanel.SetActive(true);
            SuccessPanel.SetActive(false);
        }

        else
        {
            MainPanel.SetActive(false);
            SuccessPanel.SetActive(true);
        }
 
    
    
    }





}
