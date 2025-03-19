using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HudManager : MonoBehaviour
{
    public static HudManager instance = null;
    
    private int pv_max = 100;
    private int pv = 100;
    private Item item = Item.None;
    
    [SerializeField] private GameObject hud_item;
    [SerializeField] private GameObject hud_pv;
    [SerializeField] private GameObject hud_message;
    [SerializeField] private GameObject panel_pause;
    
    [SerializeField] private float delay_message = 3.0f; // Temps où le message auto-disparaît
    // Remove the “has_message” flag and timer_message usage for non-timed messages
    private float timer_message = 0f;
    // New flag to indicate if the current message should auto-remove
    private bool autoRemove = false;
    
    public static bool pause = false;
    
    // Ajouter les sprites des items ici
    [SerializeField] private Sprite[] item_sprites;
    
    public string CurrentMessage { get; private set; } = "";
    
    // Pattern singleton, pour récupérer facilement un objet unique dans le jeu
    void Awake(){
        if(instance == null){
            instance = this;
        }
    }
    
    void Start()
    {
        if(hud_item == null || hud_pv == null || hud_message == null){
            Debug.Log("hud mal configuré");
        }
        
        updateItem();
        updatePV();
        hud_message.SetActive(false);
        
        AudioManager am = AudioManager.instance;
        am.PlayMusic(am.music_list.music1);
        
        panel_pause.SetActive(false);
    }

    void Update()
    {
        // Only update the timer if the message should auto-remove.
        if(autoRemove){
            timer_message -= Time.deltaTime;
            if(timer_message <= 0)
            {
                eraseMessage();
                autoRemove = false;
            }
        }
        
        // Si le joueur n'a plus de PV, on le redirige vers la scène de game over
        if(pv == 0){
            SceneManager.LoadScene("GameOver");
        }
        // Toggle pause with the P key.
        if(Input.GetKeyDown(KeyCode.P)){
            pause = !pause;
            panel_pause.SetActive(pause);
            Time.timeScale = pause ? 0.0f : 1.0f;
        }
    }
    
    public bool fullPV(){
        return pv == pv_max;
    }
    
    public void addPV(int val){
        pv = Mathf.Min(pv_max, pv + val);
        updatePV();
    }
    
    public void subPV(int val){
        pv = Mathf.Max(0, pv - val);
        updatePV();
    }
    
    public void updatePV(){
        hud_pv.GetComponent<TMP_Text>().SetText("PV : " + pv.ToString());
    }
    
    public bool hasItem(){
        return item != Item.None;
    }
    
    public bool gotItem(Item check){
        return item == check;
    }
    
    public void addItem(Item new_item){
        item = new_item;
        updateItem();
    }
    
    public void deleteItem(){
        item = Item.None;
        updateItem();
    }
    
    public void updateItem(){	
        if(!hasItem()){
            hud_item.SetActive(false);
        } else {
            hud_item.SetActive(true);
            switch(item){
                case Item.FlashLight:
                    hud_item.GetComponent<Image>().sprite = item_sprites[0];
                    break;
            }
        }
    }
    
    // Afficher un message qui reste jusqu'à ce qu'il soit explicitement effacé.
    public void showMessage(string message){
        CurrentMessage = message;
        hud_message.SetActive(true);
        hud_message.GetComponent<TMP_Text>().SetText(message);
        autoRemove = false;
    }
    
    public void eraseMessage(){
        hud_message.SetActive(false);
        CurrentMessage = "";
        autoRemove = false;
    }
    
    // Afficher un message qui se supprime automatiquement après delay_message secondes.
    public void showTimedMessage(string message){
        hud_message.SetActive(true);
        hud_message.GetComponent<TMP_Text>().SetText(message);
        timer_message = delay_message;
        autoRemove = true;
    }
    
    // Optionnellement, on peut garder cette méthode qui démarre la coroutine sur HudManager.
    public void RemoveMessageAfterDelay(float delay)
    {
        StartCoroutine(RemoveMessageCoroutine(delay));
    }
    
    private IEnumerator RemoveMessageCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        eraseMessage();
    }
}

// Liste des items du jeu
public enum Item
{
    None,
    FlashLight,
}



