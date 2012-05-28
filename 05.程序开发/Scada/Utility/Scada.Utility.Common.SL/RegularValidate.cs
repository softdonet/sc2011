using System;
using System.Text.RegularExpressions;


namespace Scada.Utility.Common.SL
{


    public class RegularValidate
    {


        #region 测试函数

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Test(string info)
        {
            bool isValid = true;
            Regex regex = new Regex(@"^$|^[0-9]{6}$");
            Match match = regex.Match(info);
            if (!match.Success)
            {
                isValid = false;
            }
            return isValid;
        }

        #endregion


        #region 下面的代码用于判断邮箱地址是否是有效的

        /// <summary>
        /// 用来判断“email”地址是否是有效的
        /// </summary>
        /// <returns></returns>
        public static bool JudgeEmailIsValid(string email)
        {
            bool isValid = true;

            Regex regex = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

            Match match = regex.Match(email);

            if (!match.Success)
            {
                isValid = false;
            }

            return isValid;
        }

        #endregion


        #region 下面的代码用于判断一个十进制数是否是有效的

        /// <summary>
        /// 用来判断一个十进制数是否是有效的
        /// </summary>
        /// <returns></returns>
        public static bool JudgeDecimalIsValid(string content)
        {
            bool isValid = true;

            Regex regex = new Regex(@"^[0-9]*[.]?[0-9]*$");

            Match match = regex.Match(content);

            if (!match.Success)
            {
                isValid = false;
            }

            return isValid;
        }
        #endregion


        #region 下面的代码用于判断一个整数是否是有效的

        /// <summary>
        /// 用来判断一个整数是否是有效的
        /// </summary>
        /// <returns></returns>
        public static bool JudgeIntegerIsValid(string integer)
        {
            bool isValid = true;

            Regex regex = new Regex(@"^[0-9]+$");

            Match match = regex.Match(integer);

            if (!match.Success)
            {
                isValid = false;
            }

            return isValid;
        }
        #endregion


        #region 下面的代码用于判断手机号码是否是有效

        /// <summary>
        /// 用来判断手机号码是否是有效
        /// </summary>
        /// <returns></returns>
        public static bool JudgePhoneNumIsValid(string integer)
        {
            bool isValid = true;

            Regex regex = new Regex(@"^1(30|31|32|33|34|35|36|37|38|39|50|53|57|58|59)\d{8}$");

            Match match = regex.Match(integer);

            if (!match.Success)
            {
                isValid = false;
            }

            return isValid;
        }

        #endregion


        #region 下面的代码用于判断身份证号码是否是有效

        /// <summary>
        /// 用来判断身份证号码是否是有效
        /// </summary>
        /// <returns></returns>
        public static bool JudgeIdCardIsValid(string integer)
        {
            bool isValid = true;

            Regex regex = new Regex(@"^\d{14}(\d{1}|X)$|^\d{17}(\d{1}|X)$");

            Match match = regex.Match(integer);

            if (!match.Success)
            {
                isValid = false;
            }

            return isValid;
        }

        #endregion


        #region 下面的代码用来判断是否字母是有效的

        public static bool JudgeLetterIsValid(string letter)
        {
            bool isValid = true;
            Regex regex = new Regex(@"^[A-Za-z]+$");
            Match match = regex.Match(letter);
            if (!match.Success)
            {
                isValid = false;
            }

            return isValid;
        }

        #endregion


        #region 下面的代码用来判断是否IP地址是有效的

        public static bool JudgeIPAddressIsValid(string ip)
        {
            bool isValid = true;
            Regex regex = new Regex(@"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
            Match match = regex.Match(ip);
            if (!match.Success)
            {
                isValid = false;
            }
            return isValid;
        }

        #endregion


        #region 下面的代码用来判断是否域名是有效的

        public static bool JudgeRealmNameIsValid(string ip)
        {
            bool isValid = true;
            Regex regex = new Regex(@"(?i)http://(\w+\.){2,3}(com(\.cn)?|cn|net)\b");
            Match match = regex.Match(ip);
            if (!match.Success)
            {
                isValid = false;
            }
            return isValid;
        }

        #endregion


    }

}
