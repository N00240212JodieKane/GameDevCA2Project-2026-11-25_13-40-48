using UnityEngine;
using UnityEngine.UI; // Required for Button
using UnityEngine.SceneManagement; // Required to restart the scene
using TMPro;

public class GameBehavior : MonoBehaviour
{
    private int _itemsCollected = 0;
    private int _playerHP = 10;

    public int MaxItems = 4;

    private string _ItemBaseText = "Items Collected: ";
    private string _HealthBaseText = "Player Health: ";

    // Uses TMP_Text (the base TextMeshPro class for UI)
    public TMP_Text HealthText, ItemText, ProgressText;
    
    // Assign a UI Button in the Inspector to serve as your Win Screen/Button
    public Button WinButton;

    void Start()
    {
        UpdateText(ItemText, _ItemBaseText, _itemsCollected);
        UpdateText(HealthText, _HealthBaseText, _playerHP);

        // Ensure the win button is hidden at the start of the game
        if (WinButton != null)
        {
            WinButton.gameObject.SetActive(false);
        }
    }

    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            UpdateText(ItemText, _ItemBaseText, Items);
            Debug.Log($"Items: {_itemsCollected}");

            if (_itemsCollected >= MaxItems)
            {
                TriggerWin();
            }
            else
            {
                ProgressText.text = "Item found, only " + (MaxItems - _itemsCollected) + " more to go!";
            }
        }
    }

    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            UpdateText(HealthText, _HealthBaseText, HP);
        }
    }

    private void UpdateText(TMP_Text textField, string baseText, int value)
    {
        textField.text = baseText + value;
    }

    private void TriggerWin()
    {
        ProgressText.text = "You've found all the items! You win!";
        
        // Show the win button and pause the game
        if (WinButton != null)
        {
            WinButton.gameObject.SetActive(true);
        }
        
        Time.timeScale = 0f;
    }

    // Link this method to your WinButton's OnClick() event in the Inspector
    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
