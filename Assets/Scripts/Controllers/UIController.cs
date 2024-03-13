using RollTheDie.Die;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RollTheDie.Controllers
{
    /// <summary>
    /// Control view of UI elements
    /// </summary>
    public class UIController : MonoBehaviour
    {
        [SerializeField] Result resultChecker;
        [SerializeField] TextMeshProUGUI resultText;
        [SerializeField] string textResultShown = "Result:";
        [SerializeField] string textWaitResult = "?";
        [SerializeField] TextMeshProUGUI totalText;
        [SerializeField] string textTotalShown = "Total:";
        [SerializeField] Button buttonRoll;

        private void Awake()
        {
            InitEvents();
        }

        private void InitEvents()
        {
            resultChecker.OnGetNewResult += ShowNewResult;
            resultChecker.OnWaitResult += ShowWaitResult;
            resultChecker.OnNoResult += ShowNoResult;
        }

        void ShowNewResult(int result, int totalResult)
        {
            resultText.text = textResultShown + ' ' + result;
            totalText.text = textTotalShown + ' ' + totalResult;
            buttonRoll.interactable = true;
        }

        void ShowWaitResult()
        {
            resultText.text = textResultShown + ' ' + textWaitResult;
            buttonRoll.interactable = false;
        }

        private void ShowNoResult()
        {
            resultText.text = textResultShown;
            buttonRoll.interactable = true;
        }
    }
}
