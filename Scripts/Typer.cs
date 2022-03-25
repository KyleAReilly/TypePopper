using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Typer : MonoBehaviour

{
  //Code doesnt work because of current play BOOL variable
    #region Variables
    public GameObject Timer;
    public AudioClip pop;
    //Wordbank
    public bool newTarget = false;
    public WordBank wordBank = null;
    public Text wordOutput = null;
    public Text scoreOutput = null;
    public Text countOutput = null;
    public Text wpmOutput = null;
    public Text accuracyOutput;
    public GameObject target;
    [SerializeField] bool Play = false;
    

    public string remainingWord = string.Empty;
    public string currentWord = string.Empty;
    public GameObject projectile;
    private bool started = false;
    public int score = 0;
    public int count = 0;
    public int multiplier = 1;
    public int wpm = 0;
    float time;
    int streak;
    int incorrect;
    float accuracy;
    #endregion Variables
    



   
    private void Start()
    {
        Time.timeScale = 0;
        SetCurrentWord();
       StartCoroutine(delayTargetSpawn());
       time = 60 - Timer.GetComponent<Timer>().timeValue;
        //delayTargetSpawn();
        
              
    }       
    private void SetCurrentWord()
    {
        //newTarget = false;
        currentWord = wordBank.GetWord();
        
        //get bank word
        SetRemainingWord(currentWord);
        // if(!started)
        // {
        //     delayTargetSpawn();
        //     started = true;
        // }
        

    }
    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        wordOutput.text = remainingWord;
        

    }
   private void Update()
    {
        if(Play == true)
        {
            Time.timeScale = 1;
        }
        CheckInput();   
        Score();
        Accuracy();
          
        
        // if(gos.Length > 1)
        // {
        //     Destroy(GameObject.FindGameObjectWithTag("Target"));
        // }
        
    }
    private void CheckInput()
    {
        if(Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;
            if(keysPressed.Length == 1)
            EnterLetter(keysPressed);
        }

    }
    private void EnterLetter(string typedLetter)
    {
        if(IsCorrectLetter(typedLetter))
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
             Instantiate(projectile, new Vector3(0,-5,0), Quaternion.identity);
             Destroy(GameObject.FindGameObjectWithTag("Target"));
              count += 1;
        multiplier = 1 + (count / 20);
        score += 97 * multiplier;
             
            RemoveLetter();

            if(IsWordComplete())
            {
               // newTarget = true;
               
                
            SetCurrentWord();
            
          StartCoroutine(delayTargetSpawn());

            }

        }
        else
        {
            incorrect += 1;
            streak = 0;
            multiplier = 1;
        }

    }
    IEnumerator delayTargetSpawn()
    {
        
        yield return new WaitForSeconds(.001f);
        
        int targetsToSpawn = currentWord.Length;
        for (int i = 0; i < targetsToSpawn; i++)
        {
            Debug.Log(targetsToSpawn);
            Vector3 randomLocation = new Vector3(Random.Range(-3, 3), Random.Range(-.5f, 2.5f), -6.5f);
            Instantiate(target, randomLocation, Quaternion.identity);

        }


        
        // Instantiate(target, randomLocation, Quaternion.identity);

        // var gos = GameObject.FindGameObjectsWithTag("Target");
        // if(gos.Length > 1)
        // {
        //     Destroy(GameObject.FindGameObjectWithTag("Target"));
        // }

    }
    private bool IsCorrectLetter(string letter)
    {
        
       
        return remainingWord.IndexOf(letter) == 0;
         
    
       
    

    }
    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0,1);
        SetRemainingWord(newString);

    }
    public bool IsWordComplete()
    {
        
        return remainingWord.Length == 0;
        
    }
    public void Score()
    {
       
        //float ope = Timer.GetComponent<Timer>().GetTime;
        
        wpm = Mathf.FloorToInt((count / 5) / ((60 - Timer.GetComponent<Timer>().timeValue) / 60));
         wpmOutput.text = wpm.ToString(); 

        

        //Set text value on UI to reflect score
       
        scoreOutput.text = score.ToString();
        countOutput.text = count.ToString();

    }
   //determine accuracy
    private void Accuracy()
    {
        float calc = (((float)count - (float)incorrect) / (float)count);
        accuracy = Mathf.Round(calc * 100f);
        Debug.Log(incorrect);
        accuracyOutput.text = accuracy.ToString() + "%";


    }
    
    //UI menu system
    public void Exit()
        {
            Application.Quit();
            Debug.Log("Close");
        }
    public void StartGame()
    {
        Play = true;
        Debug.Log("Play");
        Debug.Log(Time.timeScale);
    }
    public void Options()
    {
        
        Debug.Log("OptionsMenu");
    }
    public void Leaderboard()
    {
        Debug.Log("Leaderboard");
    }



}
