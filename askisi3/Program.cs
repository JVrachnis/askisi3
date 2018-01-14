using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace askisi3
{
    class Program
    {
        static string[] T = { "x", "y", "[", "]", ":", "+"};
        static string[] e = { "]"};
        static string[] S = { "[A]"};
        static string[] A = { "BE" };
        static string[] B = { "x", "y", "S" };

        static string[] E = { ":A", "+A", "]" };
        
        static Dictionary<string, string[]> states = new Dictionary<string, string[]>();
        static void Main(string[] args)
        {
            
            string stack = "S";
            string input = "[[x:y]+[y:x]]";
            states.Add("S", S);
            states.Add("A", A);
            states.Add("B", B);
            states.Add("E", E);
            bool flag = true;
            Console.WriteLine(stack + "  " + input);
            while (stack.Length > 0&& !e.Contains(stack) && flag&& input.Length>0) {
                
                if (states.Keys.Contains(stack.Last() + ""))
                {
                    flag= M(stack.Last() + "", input.Last() + "",ref input,ref stack);
                }
                else if (stack.Length >= 2 && T.Contains(stack[stack.Length - 2] + ""))
                {
                    flag = false;
                    break;
                }
                else if(stack.Length >= 2)
                {
                    string temp = stack.Last() + "";
                    if (stack.Length > 0)
                    {
                        stack = stack.Substring(0, stack.Length - 1);
                    }
                    if (states.Keys.Contains(stack.Last() + "")) {
                        flag = M(stack.Last() + "", temp, ref input, ref stack);
                    }
                }
            }
            flag = flag && (stack.Length==0|| e.Contains(stack)) && input.Length == 0;
            if (!flag)
            {
                Console.WriteLine("No");
            }
            if (flag)
            {
                Console.WriteLine("Yes");
            }
            Console.ReadLine();
        }
        static bool M(string State, string Item, ref string Input, ref string stack)
        {
            bool flag = false;
            string tempInput1 = Input, tempStack1 = stack;
            if (stack.Length > 0)
            {
                stack = stack.Substring(0, stack.Length - 1);
            }
            for (int i = 0; i < states[State].Length; i++)
            {
                if (states[State][i]=="")
                {

                }else
                if (Input[0] == states[State][i].First() && T.Contains(states[State][i].First() + ""))
                {
                    string tempInput = Input[0]+"";
                    Input = Input.Substring(1);
                    if (e.Contains(tempInput))
                    {
                        flag = true;
                        break;
                    }
                    if (states[State][i].Length>= 2 && T.Contains(states[State][i].Last() + ""))
                    {
                        stack += ReverseString(states[State][i].Substring(1));
                    }
                    else
                    {
                        stack += ReverseString(states[State][i].Substring(1));
                    }
                    if (Input!="")
                    {
                        stack+= Input.First();
                    }
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                Console.WriteLine(tempStack1 + "  " + tempInput1);
                Console.WriteLine(stack + "  " + Input);
            }
            if (!flag)
            {
                foreach (string s in states[State])
                {
                    if (s !=""&& states.Keys.Contains(s.Last() + ""))
                    {
                        string tempInput = Input;
                        string tempStack = (stack+ ReverseString(s));
                        if (states.Keys.Contains(tempStack.Last() + ""))
                        {
                            if (M(tempStack.Last() + "", Item, ref tempInput, ref tempStack))
                            {
                                Input = tempInput;
                                stack = tempStack;
                                flag = true;
                                break;
                            }
                        }
                    }
                }
            }
            
            return flag;
        }
        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
