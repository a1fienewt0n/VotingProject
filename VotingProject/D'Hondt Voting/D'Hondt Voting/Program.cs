using System;
using System.IO;
using System.Collections.Generic;

public class Get_Votes
{
    public static List<string> parties = new List<string>();
    public static List<int> party_votes = new List<int>(); // Variables for the lines of the text file, a counter and a couple lists

    public static void ReadFiles()
    {
        string fileName = @"Votes.txt"; //assigns the text file to a string

        using (StreamReader reader = new StreamReader(fileName)) // Opens an instance of the file
        {
            string line;
            int counter = 0;

            while ((line = reader.ReadLine()) != null) // Do while the line of the text file is not empty
            {
                if (counter == 1) // check for the line in the text file with the total seats on it
                {
                    int total_seats = System.Convert.ToInt32(line); // Assigns a integer version of the text file's line to a variable
                }

                if (counter == 2) // check for the line in the text file with the total votes on it
                {
                    int total_votes = System.Convert.ToInt32(line);
                }

                if (counter > 2) // The rest of the text file's lines are parties and their votes
                {
                    parties.Add(line.Substring(0, line.IndexOf(","))); // Gets the name of the party and adds it to the 'parties' list
                    line = line.Substring(line.IndexOf(",") + 1); // Cuts the name of the party off the string
                    party_votes.Add(System.Convert.ToInt32(line.Substring(0, line.IndexOf(",")))); // Retrieves the partie's votes and adds them to a list

                }

                counter += 1; // Makes sure the next line of the file is read
            }

        }
    }

    public class Party
    {
        //fields
        public string name;
        public int numVotes;
        public int numWins;
        public List<string> seatsleft = new List<string>();

        //constructor
        public Party(string partyName, int partyNumVotes, int partyNumSeats)
        {
            name = partyName;
            numVotes = partyNumVotes;
            for (int i = 1; i != partyNumSeats + 1; i++)
            {
                seatsleft.Add("SEAT" + i);
            }
            numWins = 0;
        }

        //methods
        public void SeatWon()
        {
            numWins++;
            seatsleft.RemoveAt(0);
        }

        //!!! FOR TESTING
        public void Check()
        {
            Console.WriteLine("Party's name = " + name);
            Console.WriteLine("Votes = " + numVotes);
            Console.WriteLine("Wins = " + numWins);
            for (int i = 0; i < seatsleft.Count; i++)
                Console.WriteLine(seatsleft[i]);
        }
        public Party[] partyObjects;
        public static void Main(string[] args)
        {
            Get_Votes.ReadFiles();

            //Creating dictionary for party objects
            Dictionary<int, Party> partyObjects = new Dictionary<int, Party>();

            //Creating the individual party objects
            for (int i = 0; i < parties.Count - 1; i++)
            {
                partyObjects.Add(i, new Party(parties[i], party_votes[i], 5));
            }
            //Creation exception for Independent which only has 1 seat
            partyObjects.Add(8, new Party(parties[8], party_votes[8], 1));

            //Testing each party object's contents
            for (int i = 0; i < parties.Count; i++)
            {
                partyObjects[i].Check();
            }
        }
    }
}
