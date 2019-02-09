using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Common
{
    public class CommonConstant
    {

        public static string TABLE_NAME_CHARACTER = "character";
        public static string TABLE_NAME_WEAPON = "weapon";
        public static string TABLE_NAME_STIGMATA = "stigmata";

        public static string FOLDER_KEY_CHARACTER = "characterFolder";
        public static string FOLDER_KEY_WEAPON = "weaponFolder";
        public static string FOLDER_KEY_STIGMATA = "stigmataFolder";
        public static string FOLDER_KEY_ANALYZE = "analyzeFolder";

        public enum WeaponType
        {
            手枪,
            太刀,
            火炮,
            大剑,
            十字架,
            拳套,
            镰刀
                
        }

        public enum StigmataType
        {
            上,
            中,
            下
        }


    }
}
