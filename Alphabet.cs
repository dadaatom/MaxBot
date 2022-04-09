using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBot_V2
{
    class Alphabet
    {
        Dictionary<string, string> cursLetters = new Dictionary<string, string>();
        public Alphabet() {
            cursLetters.Add("a", "𝒶");
            cursLetters.Add("b", "𝒷");
            cursLetters.Add("c", "𝒸");
            cursLetters.Add("d", "𝒹");
            cursLetters.Add("e", "𝑒");
            cursLetters.Add("f", "𝒻");
            cursLetters.Add("g", "𝑔");
            cursLetters.Add("h", "𝒽");
            cursLetters.Add("i", "𝒾");
            cursLetters.Add("j", "𝒿");
            cursLetters.Add("k", "𝓀");
            cursLetters.Add("l", "𝓁");
            cursLetters.Add("m", "𝓂");
            cursLetters.Add("n", "𝓃");
            cursLetters.Add("o", "𝑜");
            cursLetters.Add("p", "𝓅");
            cursLetters.Add("q", "𝓆");
            cursLetters.Add("r", "𝓇");
            cursLetters.Add("s", "𝓈");
            cursLetters.Add("t", "𝓉");
            cursLetters.Add("u", "𝓊");
            cursLetters.Add("v", "𝓋");
            cursLetters.Add("w", "𝓌");
            cursLetters.Add("x", "𝓍");
            cursLetters.Add("y", "𝓎");
            cursLetters.Add("z", "𝓏");
            cursLetters.Add("A", "𝒜");
            cursLetters.Add("B", "𝐵");
            cursLetters.Add("C", "𝒞");
            cursLetters.Add("D", "𝒟");
            cursLetters.Add("E", "𝐸");
            cursLetters.Add("F", "𝐹");
            cursLetters.Add("G", "𝒢");
            cursLetters.Add("H", "𝐻");
            cursLetters.Add("I", "𝐼");
            cursLetters.Add("J", "𝒥");
            cursLetters.Add("K", "𝒦");
            cursLetters.Add("L", "𝐿");
            cursLetters.Add("M", "𝑀");
            cursLetters.Add("N", "𝒩");
            cursLetters.Add("O", "𝒪");
            cursLetters.Add("P", "𝒫");
            cursLetters.Add("Q", "𝒬");
            cursLetters.Add("R", "𝑅");
            cursLetters.Add("S", "𝒮");
            cursLetters.Add("T", "𝒯");
            cursLetters.Add("U", "𝒰");
            cursLetters.Add("V", "𝒱");
            cursLetters.Add("W", "𝒲");
            cursLetters.Add("X", "𝒳");
            cursLetters.Add("Y", "𝒴");
            cursLetters.Add("Z", "𝒵");
        }

        public Dictionary<string, string> getCursive() {
            return cursLetters;
        }
    }
}

