using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.Extensibility;
using System.Xml;
using SharedPCL;
using System.Collections.Generic;
using System.IO;

namespace test
{
	[TestFixture]
	public class UnitTest
	{
		[Test]
		public void CheckThatAllWordsHavePictures ()
		{
			XmlReader reader = XmlReader.Create("Shared_Content/Pairs2.xml");

			imageParser parser = new imageParser ();

			Dictionary <String, List<Pair>> pairLists = new Dictionary<String, List<Pair>> ();

			List<String> pairTypes = new List<String> () {
				"initial_consonant",
				"final_consonant",
				"cluster_reduction",
				"stopping",
				"fronting",
				"postvocalic",
				"prevocalic",
				"gliding",
				"stridency_deletion",
				"nasalization",
				"multi",
			};
				
			pairLists = parser.parsePairs (reader);

			String leftImagePath;
			String rightImagePath;
			List<Pair> pairList;

			int missingCount = 0;
			string missingWords = "";

			foreach (String pairType in pairTypes) {
				pairList = pairLists [pairType];

				missingWords += pairType + " has " + pairList.Count.ToString () + " words \n";



				foreach (Pair pair in pairList) {
					leftImagePath = "ipad_images_pointer/" + pair.leftImageName + ".jpg";
					rightImagePath = "ipad_images_pointer/" + pair.rightImageName + ".jpg";

					if(!File.Exists(leftImagePath)){

						missingWords = missingWords + pair.leftImageName +"\n";
						Console.WriteLine (pair.leftImageName + "is missing");
						missingCount++;

					}
					if (!File.Exists (rightImagePath)) {

						missingWords = missingWords + pair.rightImageName+"\n";
						Console.WriteLine (pair.rightImageName + "is missing");
						missingCount++;
				
					}

				}
			}

			int zero = 0;
			Assert.AreEqual (zero, missingCount, missingWords);

		}

		[Test]
		public void CheckThatPicturesHaveWords ()
		{
			XmlReader reader = XmlReader.Create("Shared_Content/Pairs2.xml");

			imageParser parser = new imageParser ();

			Dictionary <String, List<Pair>> pairLists = new Dictionary<String, List<Pair>> ();

			List<String> pairTypes = new List<String> () {
				"initial_consonant",
				"final_consonant",
				"cluster_reduction",
				"stopping",
				"fronting",
				"postvocalic",
				"prevocalic",
				"gliding",
				"stridency_deletion",
				"nasalization",
				"multi",
			};

			pairLists = parser.parsePairs (reader);

			String leftImagePath;
			String rightImagePath;
			List<Pair> pairList;

			int missingCount = 0;
			string missingWords = "";

			foreach (String pairType in pairTypes) {
				pairList = pairLists [pairType];

				missingWords += pairType + " has " + pairList.Count.ToString () + " words \n";

				foreach (Pair pair in pairList) {
					leftImagePath = "ipad_images_pointer/" + pair.leftImageName + ".jpg";
					rightImagePath = "ipad_images_pointer/" + pair.rightImageName + ".jpg";

					if(!File.Exists(leftImagePath)){

						missingWords = missingWords + pair.leftImageName +"\n";
						Console.WriteLine (pair.leftImageName + "is missing");
						missingCount++;

					}
					if (!File.Exists (rightImagePath)) {

						missingWords = missingWords + pair.rightImageName+"\n";
						Console.WriteLine (pair.rightImageName + "is missing");
						missingCount++;

					}

				}
			}

			int zero = 0;
			Assert.AreEqual (zero, missingCount, missingWords);

		}

		[Test]
		public void Fail ()
		{
			Assert.That (1, Is.EqualTo (1));
			Assert.False (true);
		}

		[Test]
		[Ignore ("another time")]
		public void Ignore ()
		{
			Assert.True (false);
		}
	}
}
