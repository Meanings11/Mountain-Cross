using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class PlayerStats
{
    public const int insurance = 1;
    public const int plantTicket = 2;
    

    public static int getItemNum(int index) { 
        if (index > quantity.Length) {
            Debug.Log("Wrong product index");
            return 0;
        } 

        return quantity[index-1];
     }

     public static void addItem(int index) {
         if (index > quantity.Length) {
            Debug.Log("Wrong product index");
            return;
        } 

        quantity[index-1]++;
        Debug.Log( index + ": " +quantity[index-1]);
     }

     public static void useOneItem(int index) {
         if (index > quantity.Length) {
            Debug.Log("Wrong product index");
            return;
        } 

        quantity[index-1]--;
        Debug.Log( index + ": " +quantity[index-1]);
     }

     public static void setMode(int m) {
         if (m < 0 || m > 3) {
            Debug.Log("Invalid mode");
            return;
         }
         mode = m;
     }

     public static int getMode() {
         return mode;
     }
   
    private static int[] quantity = {0,0};
    private static int mode = 0;
}