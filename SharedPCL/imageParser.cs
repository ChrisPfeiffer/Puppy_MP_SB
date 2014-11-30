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
			//will return a dictionary containing multiple lists. string will be pair type.
			Dictionary<string, List<Pair>> pairLists = new Dictionary<string, List<Pair>> ();

			//A list for each type
			List<Pair> initConsonant = new List<Pair> ();
			List<Pair> finConsonant = new List<Pair> ();
			List<Pair> clusterReduction = new List<Pair> ();
			List<Pair> stopping = new List<Pair> ();
			List<Pair> fronting = new List<Pair> ();
			List<Pair> multi = new List<Pair> ();
			List<Pair> nasalization = new List<Pair> ();
			List<Pair> preVocalic = new List<Pair> ();
			List<Pair> postVocalic = new List<Pair> ();
			List<Pair> gliding = new List<Pair> ();
			List<Pair> stridencyDeletion = new List<Pair> ();

			//read the xml file and add each pair to the appropriate list 
			while (reader.ReadToFollowing ("Pair")) {
				Pair pairToAdd = new Pair ();

				reader.ReadToFollowing ("Left");
				pairToAdd.leftImageName = reader.ReadElementContentAsString ();
				reader.ReadToFollowing ("Right");
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

				case "Nasalization":
					reader.ReadToFollowing("Position");
					pairToAdd.position = reader.ReadElementContentAsString();
					nasalization.Add(pairToAdd);
					break;

				case "Stridency Deletion":
					stridencyDeletion.Add (pairToAdd);
					break;
				
				case "Gliding":
					gliding.Add (pairToAdd);
					break;

				case "Prevocalic":
					preVocalic.Add (pairToAdd);
					break;

				case "Postvocalic":
					postVocalic.Add (pairToAdd);
					break;

				case "Syllable Deletion":
					multi.Add (pairToAdd);
					break;
										
				}

			}

			//add each list that has been built along with type as the key
			pairLists.Add ("initial_consonant", initConsonant);
			pairLists.Add ("final_consonant", finConsonant);
			pairLists.Add ("cluster_reduction", clusterReduction);
			pairLists.Add ("stopping", stopping);
			pairLists.Add ("fronting", fronting);
			pairLists.Add ("postvocalic", postVocalic);
			pairLists.Add ("prevocalic", preVocalic);
			pairLists.Add ("gliding", gliding);
			pairLists.Add ("stridency_deletion", stridencyDeletion);
			pairLists.Add ("nasalization", nasalization);
			pairLists.Add ("multi", multi);


			return pairLists;

		}
	}
}

