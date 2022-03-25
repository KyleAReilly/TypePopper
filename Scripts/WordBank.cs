using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class WordBank : MonoBehaviour
{
    public TextAsset words;
    
    
    private List<string> workingWords = new List<string>();
    string path;
    
    

    

    // Start is called before the first frame update
    void Awake()
    {
        path = Application.dataPath;
        
        string readFromFilePath = path + "/Resources/WordBank.txt";
       
       List<string> orignalWords = File.ReadAllLines(readFromFilePath).ToList();
       Debug.Log(orignalWords.Count);


        workingWords.AddRange(orignalWords);
       Shuffle(workingWords);
       ConvertToLower(workingWords);
        Debug.Log(workingWords.Count);
        
        
    }

    // Update is called once per frame
   private void Shuffle(List<string> list)
   {
       for (int i = 0; i < list.Count; i++)
       {
           int random = Random.Range(i, list.Count);
           string temporary = list[i];

           list[i] = list[random];
           list[random] = temporary;

       }

   }
   private void ConvertToLower(List<string> list)
   {
       for(int i = 0; i < list.Count; i++)
       list[i] = list[i].ToLower();

   }
   public string GetWord()
   {
       string newWord = string.Empty;
      
       

       if(workingWords.Count != 0)
       {
           Debug.Log("GetWord");
           newWord = workingWords.Last();
           workingWords.Remove(newWord);
       }


       return newWord;
   }
}