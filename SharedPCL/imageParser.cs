using System;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Collections.Generic;
using System.IO;





namespace SharedPCL
{
	public class imageParser
	{
		public imageParser ()
		{

		}

		//xmlreader cannot be instantiated in PCL, must be passed in.
		public Dictionary<string, List<Pair>> parsePairs(XmlReader reader)
		{
			//will return a dictionary containing six lists. string will be pair type.
			Dictionary<string, List<Pair>> pairLists = new Dictionary<string, List<Pair>> ();

			//A list for each type
			List<Pair> initConsonant = new List<Pair> ();
			List<Pair> finConsonant = new List<Pair> ();
			List<Pair> clusterReduction = new List<Pair> ();
			List<Pair> stopping = new List<Pair> ();
			List<Pair> fronting = new List<Pair> ();
			List<Pair> syllableDeletion = new List<Pair> ();

			//read the xml file and add each pair to the appropriate list 
			while (reader.ReadToFollowing ("Pair")) {
				Pair pairToAdd = new Pair ();

				reader.ReadToFollowing ("LeftImage");
				pairToAdd.leftImageName = reader.ReadElementContentAsString ();
				reader.ReadToFollowing ("RightImage");
				pairToAdd.rightImageName = reader.ReadElementContentAsString ();
				reader.ReadToFollowing ("Type");
				string type = reader.ReadElementContentAsString ();

				switch (type) {

				case "Initial Consonant Deletion":
					initConsonant.Add (pairToAdd);
					break;

				case "Final Consonant Deletion":
					finConsonant.Add (pairToAdd);
					break;

				case "Cluster Reduction":
					reader.ReadToFollowing ("Position");
					pairToAdd.position = reader.ReadElementContentAsString ();
					clusterReduction.Add (pairToAdd);
					break;

				case "Stopping":
					reader.ReadToFollowing ("Position");
					pairToAdd.position = reader.ReadElementContentAsString ();
					stopping.Add (pairToAdd);
					break;

				case "Fronting":
					reader.ReadToFollowing ("Position");
					pairToAdd.position = reader.ReadElementContentAsString ();
					fronting.Add (pairToAdd);
					break;

				case "Syllable Deletion":
					syllableDeletion.Add (pairToAdd);
					break;
										
				}

			}

			//add each list that has been built along with type as the key
			pairLists.Add ("Initial Consonant Deletion", initConsonant);
			pairLists.Add ("Final Consonant Deletion", finConsonant);
			pairLists.Add ("Cluster Reduction", clusterReduction);
			pairLists.Add ("Stopping", stopping);
			pairLists.Add ("Fronting", fronting);
			pairLists.Add ("Syllable Deletion", syllableDeletion);


			return pairLists;

		}
	}
}

