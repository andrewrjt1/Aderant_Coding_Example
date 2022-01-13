using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aderant_Coding_Example_mk2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string[] document = { "all is well", "ell that en", "hat end", "t ends well" };
            string[] document2 = { "all  is well", "ell  that en", "hat  end", "t ends  well" };
            string[] document3 = { "all is wELl", "ell that en", "hAt end", "t ends wElL" };
            string[] document4 = { "ell that en", "all is well", "t ends well", "hat end" };
            string[] document5 = { "hat end", "t ends well", "all is well", "ell that en" };
            string[] document6 = { "ell that  en", " all is  wELl", "t  ends  wElL", " hAt  end" };
            string[] document7 = { "all is well", "ell that en", "hat end", "t ends well", "ell" };

            CleanDocument(document);
            CleanDocument(document2);
            CleanDocument(document3);
            CleanDocument(document4);
            CleanDocument(document5);
            CleanDocument(document6);
            CleanDocument(document7);

            void CleanDocument(string[] document)
            {
                for (int i = 0; i < document.Length; i++)
                {
                    document[i] = Regex.Replace(document[i], @"\s+", " ").Trim();
                }
                for (int i = 0; i < document.Length + 1; i++)
                {
                    document = DocumentMerger(document);
                }
                if (document.Length > 0)
                {
                    Console.WriteLine(document[0]);
                }
            }

            string[] DocumentMerger(string[] document)
            {
                int highestcount = 0;
                int position1 = 0;
                int position2 = 0;

                for (int i = 0; i < document.Length; i++)
                {
                    for (int j = 0; j < document.Length; j++)
                    {
                        if (document[i] != document[j])
                        {
                            int tempcount = CountAmount(document[i], document[j]);
                            if (tempcount > highestcount)
                            {
                                highestcount = tempcount;
                                position1 = i;
                                position2 = j;
                            }
                        }
                    }
                }
                if (highestcount == 0)
                {
                    throw new InvalidOperationException("Not match found");
                }
                document[position1] = MergeStrings(document[position1], document[position2]);
                document = document.Where((source, index) => index != position2).ToArray();
                return document;
            }

            int CountAmount(string one, string two)
            {
                char[] onearry = one.ToCharArray();
                char[] twoarry = two.ToCharArray();

                int counter = 0;

                bool startedcounting = false;

                foreach (char c in onearry)
                {
                    if (counter < twoarry.Length)
                    {
                        if (Char.ToLower(c) == Char.ToLower(twoarry[counter]))
                        {
                            counter++;
                            startedcounting = true;
                        }
                        else if (startedcounting = true && Char.ToLower(c) != Char.ToLower(twoarry[counter]))
                        {
                            counter = 0;
                        }
                    }
                }
                return counter;
            }

            string MergeStrings(string one, string two)
            {
                char[] onearry = one.ToCharArray();
                char[] twoarry = two.ToCharArray();
                bool startedcounting = false;
                bool fullycontained = false;
                int counter = 0;
                int positionofarray = 0;
                int startingpoint = 0;

                foreach (char c in onearry)
                {
                    if (counter < twoarry.Length)
                    {
                        if (Char.ToLower(c) == Char.ToLower(twoarry[counter]))
                        {
                            if (counter == 0)
                            {
                                startingpoint = positionofarray;
                            }
                            counter++;
                        }
                        else if (startedcounting = true && Char.ToLower(c) != Char.ToLower(twoarry[counter]))
                        {
                            if (counter > 2)
                            {
                                fullycontained = true;
                            }
                            counter = 0;
                        }
                        positionofarray++;
                    }
                }
                if (counter > 2)
                {
                    if (!fullycontained)
                        return one = one.Remove(startingpoint) + two;
                    return one;
                }
                else
                {
                    throw new InvalidOperationException("Merge Failed");
                }
            }
        }
    }
}
