using System;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace TextRpg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.Start();
            
        }
        
    }
}
