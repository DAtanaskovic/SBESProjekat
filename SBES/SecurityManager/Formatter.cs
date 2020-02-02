using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityManager
{
    public class Formatter
    {
        public static string ParseGroup(string name)
        {
			string group = "";

			// Dobije se OU=Admins,
			group = name.Substring(name.IndexOf("OU=")).Split(' ')[0];

			// Dobije se Admins, 
			group = group.Substring(group.IndexOf("=") + 1);
			
			// I samo izbrisemo zarez na kraju
			group = group.Remove(group.Length - 1);


			return group;

		}
    }
}
