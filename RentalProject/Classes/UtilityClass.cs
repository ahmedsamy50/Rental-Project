using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Security.Cryptography;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Web.Profile;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Globalization;

namespace RentalProject.Classes
{
    public class UtilityClass
    {
        public UtilityClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void GoToLoginPage()
        {
            HttpContext.Current.Response.Redirect("~/Login.aspx?pg=" + HttpContext.Current.Request.Url);
        }


        #region Create TreeView Nodes Recursive



        //public static void AddTopTreeItems(List<Classes.CmsMenu> MenuLst, ref System.Web.UI.WebControls.TreeView tv)
        //{
        //    try
        //    {
        //        List<Classes.CmsMenu> ParentLst = new List<Classes.CmsMenu>();
        //        ParentLst = MenuLst.FindAll(obj => obj.ParentId == 0);
        //        for (int i = 0; i < ParentLst.Count; i++)
        //        {
        //            TreeNode parentMenu = default(TreeNode);
        //            parentMenu = CreateTreeViewItem(ParentLst[i].MenuTitle, ParentLst[i].Id.Value.ToString(), ParentLst[i].MenuTitle, ParentLst[i].Id.Value.ToString());
        //            tv.Nodes.Add(parentMenu);
        //            AddChildTreeViewItems(MenuLst, ParentLst[i].Id.Value, parentMenu);
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}
        //private static void AddChildTreeViewItems(List<Classes.CmsMenu> MenuLst, int parentID, TreeNode parentMenu)
        //{
        //    try
        //    {

        //        List<Classes.CmsMenu> ChildLst = new List<Classes.CmsMenu>();
        //        ChildLst = MenuLst.FindAll(obj => obj.ParentId == parentID);

        //        for (int i = 0; i < ChildLst.Count; i++)
        //        {
        //            TreeNode childMenu = default(TreeNode);
        //            childMenu = CreateTreeViewItem(ChildLst[i].MenuTitle, ChildLst[i].Id.Value.ToString(), ChildLst[i].MenuTitle, ChildLst[i].Id.Value.ToString());
        //            parentMenu.ChildNodes.Add(childMenu);
        //            AddChildTreeViewItems(MenuLst, ChildLst[i].Id.Value, childMenu);
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}
        //private static TreeNode CreateTreeViewItem(string strText, string strUrl, string strToolTip, string strValue)
        //{
        //    TreeNode menuItem = new TreeNode();
        //    menuItem.Text = strText;
        //    //  menuItem.NavigateUrl = strUrl;
        //    menuItem.ToolTip = strToolTip;
        //    menuItem.Value = strValue;
        //    return menuItem;

        //}



        //public static void AddTopTreeItems(List<Classes.ProductCategory> MenuLst, ref System.Web.UI.WebControls.TreeView tv)
        //{
        //    try
        //    {
        //        List<Classes.ProductCategory> ParentLst = new List<Classes.ProductCategory>();
        //        ParentLst = MenuLst.FindAll(obj => obj.ParentId == 0);
        //        for (int i = 0; i < ParentLst.Count; i++)
        //        {
        //            TreeNode parentMenu = default(TreeNode);
        //            parentMenu = CreateTreeViewItem(ParentLst[i].Title, ParentLst[i].Id.Value.ToString(), ParentLst[i].Title, ParentLst[i].Id.Value.ToString());
        //            tv.Nodes.Add(parentMenu);
        //            AddChildTreeViewItems(MenuLst, ParentLst[i].Id.Value, parentMenu);
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}
        //private static void AddChildTreeViewItems(List<Classes.ProductCategory> MenuLst, int parentID, TreeNode parentMenu)
        //{
        //    try
        //    {

        //        List<Classes.ProductCategory> ChildLst = new List<Classes.ProductCategory>();
        //        ChildLst = MenuLst.FindAll(obj => obj.ParentId == parentID);

        //        for (int i = 0; i < ChildLst.Count; i++)
        //        {
        //            TreeNode childMenu = default(TreeNode);
        //            childMenu = CreateTreeViewItem(ChildLst[i].Title, ChildLst[i].Id.Value.ToString(), ChildLst[i].Title, ChildLst[i].Id.Value.ToString());
        //            parentMenu.ChildNodes.Add(childMenu);
        //            AddChildTreeViewItems(MenuLst, ChildLst[i].Id.Value, childMenu);
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}
        #endregion

        #region Create Menu Item Nodes Recursive
        //public static void AddTopMenuItems(List<Classes.CmsMenu> MenuLst, ref System.Web.UI.WebControls.Menu menu)
        //{
        //    try
        //    {
        //        List<Classes.CmsMenu> ParentLst = new List<Classes.CmsMenu>();
        //        ParentLst = MenuLst.FindAll(obj => obj.ParentId == 0);
        //        for (int i = 0; i < ParentLst.Count; i++)
        //        {
        //            MenuItem parentMenu = default(MenuItem);
        //            parentMenu = CreateMenuItem(ParentLst[i].MenuTitle, ParentLst[i].Id.Value.ToString(), ParentLst[i].MenuTitle, ParentLst[i].Id.Value.ToString());
        //            menu.Items.Add(parentMenu);
        //            AddChildMenuItems(MenuLst, ParentLst[i].Id.Value, parentMenu);
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}
        //private static void AddChildMenuItems(List<Classes.CmsMenu> MenuLst, int parentID, MenuItem parentMenu)
        //{
        //    try
        //    {

        //        List<Classes.CmsMenu> ChildLst = new List<Classes.CmsMenu>();
        //        ChildLst = MenuLst.FindAll(obj => obj.ParentId == parentID);

        //        for (int i = 0; i < ChildLst.Count; i++)
        //        {
        //            MenuItem childMenu = default(MenuItem);
        //            childMenu = CreateMenuItem(ChildLst[i].MenuTitle, ChildLst[i].Id.Value.ToString(), ChildLst[i].MenuTitle, ChildLst[i].Id.Value.ToString());
        //            parentMenu.ChildItems.Add(childMenu);
        //            AddChildMenuItems(MenuLst, ChildLst[i].Id.Value, childMenu);
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}
        //private static MenuItem CreateMenuItem(string strText, string strUrl, string strToolTip, string strValue)
        //{
        //    MenuItem menuItem = new MenuItem();
        //    menuItem.Text = strText;
        //    //  menuItem.NavigateUrl = strUrl;
        //    menuItem.ToolTip = strToolTip;
        //    menuItem.Value = strValue;
        //    return menuItem;

        //}








        private static object GetPropertyName(object o, string PropertyName)
        {
            return o.GetType().InvokeMember(PropertyName, System.Reflection.BindingFlags.GetProperty, null, o, null);
        }


        #endregion

        #region Create Random Text
        private static int _randomTextLength = 5;
        private static string _randomTextChars = "qwertyuiopasdfghjklzxcvbnm1234567890";

        public static string GetRandomText()
        {
            Random _rand = new Random();
            System.Text.StringBuilder sb = new System.Text.StringBuilder(_randomTextLength);
            for (int i = 0; i < _randomTextLength; i++)
            {
                sb.Append(_randomTextChars.Substring(_rand.Next(_randomTextChars.Length), 1));
            }
            return sb.ToString();
        }
        #endregion

        #region Enum languages Names

        public enum LangName
        {
            Ar = 0,
            En = 1,
            Fr = 2,
            Gr = 3,
            Sp = 4
        }
        #endregion

        #region Connection String Reader


        ///// <summary>
        ///// returned open connection
        ///// </summary>
        ///// <param name="connectionString">the server and user name password</param>
        ///// <returns>returned open connection</returns>
        //public static SqlConnection GetConnection(string connectionString)
        //{
        //    SqlConnection connection = new SqlConnection(connectionString);
        //    connection.Open();
        //    return connection;
        //}

        //public static string ConnectionStringReader
        //{
        //    get { return ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString; }
        //}

        #endregion



        #region Read Key From Web Confige
        public static string AppSettingKeyReader(string strKey)
        {
            AppSettingsReader AppReader = new AppSettingsReader();
            return AppReader.GetValue(strKey, typeof(string)).ToString();
        }
        #endregion

        #region Send Email
        public static void SendEmail(string strHostEmailName, string strHostEmailUserName, string strHostEmailPassword, string strFromUserName, string strToEmailAddress, string strEmailSubject, ArrayList arCCEmailAddresses, ArrayList arBCCEmailAddresses, Boolean bl_IsBodyHtml, String strBody, Boolean bl_IsArabicContent)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();



            smtpClient.Host = strHostEmailName;
            smtpClient.Credentials = new System.Net.NetworkCredential(strHostEmailUserName, strHostEmailPassword);

            MailAddress fromAddress = new MailAddress(strHostEmailUserName, strFromUserName);

            //Default port will be 25
            smtpClient.Port = 25;

            //From address will be given as a MailAddress Object
            message.From = fromAddress;

            // To address collection of MailAddress
            message.To.Add(strToEmailAddress);
            message.Subject = strEmailSubject;

            // CC and BCC optional
            if (arCCEmailAddresses != null)
            {
                if (arCCEmailAddresses.Count > 0)
                {
                    for (int i = 0; i < arCCEmailAddresses.Count; i++)
                    {
                        message.CC.Add(new MailAddress(arCCEmailAddresses[i].ToString()));
                    }
                }
            }

            if (arBCCEmailAddresses != null)
            {
                if (arBCCEmailAddresses.Count > 0)
                {
                    for (int i = 0; i < arBCCEmailAddresses.Count; i++)
                    {
                        message.Bcc.Add(new MailAddress(arBCCEmailAddresses[i].ToString()));
                    }
                }
            }
            //Body can be Html or text format
            //Specify true if it  is html message
            message.IsBodyHtml = bl_IsBodyHtml;

            if (bl_IsArabicContent == true)
            {
                message.BodyEncoding = System.Text.Encoding.UTF8;
            }

            // Message body content
            message.Body = strBody;

            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            // Send SMTP mail
            smtpClient.Send(message);




        }
        #endregion

        #region Get Page Request From Any Path
        public static string GetPageRequest(string strFullRequestPath)
        {
            string str_Content = string.Empty;
            StreamReader objStreamReader = null;
            WebRequest wRequest;
            WebResponse wResponse;
            try
            {
                wRequest = WebRequest.Create(@"" + strFullRequestPath);
                wResponse = wRequest.GetResponse();
                objStreamReader = new StreamReader(wResponse.GetResponseStream(), System.Text.Encoding.Default);
                str_Content = objStreamReader.ReadToEnd();
            }
            catch (Exception)
            {
            }
            return str_Content;
        }
        #endregion

        #region Encrypting And Decrypting web.config Information
        public static void EncryptWebConfigData(string appPath, string protectedSection, string dataProtectionProvider)
        {
            try
            {
                System.Configuration.Configuration webConfig = WebConfigurationManager.OpenWebConfiguration(appPath);
                ConfigurationSection webConfigSection = webConfig.GetSection(protectedSection);
                if (webConfigSection.SectionInformation.IsProtected == false)
                {
                    webConfigSection.SectionInformation.ProtectSection(dataProtectionProvider);
                    webConfig.Save();
                }
            }
            catch (Exception)
            {

            }
        }


        public static void DecryptWebConfigData(string appPath, string protectedSection)
        {
            System.Configuration.Configuration webConfig = WebConfigurationManager.OpenWebConfiguration(appPath);
            ConfigurationSection webConfigSection = webConfig.GetSection(protectedSection);
            if (webConfigSection.SectionInformation.IsProtected == true)
            {
                webConfigSection.SectionInformation.UnprotectSection();
                webConfig.Save();
            }
        }


        #endregion

        #region Validation By Regular Expressions
        // Function to test for Positive Integers. 
        public static bool IsNaturalNumber(String strNumber)
        {
            Regex objNotNaturalPattern = new Regex("[^0-9]");
            Regex objNaturalPattern = new Regex("0*[1-9][0-9]*");
            return !objNotNaturalPattern.IsMatch(strNumber) && objNaturalPattern.IsMatch(strNumber);
        }

        // Function to test for Positive Integers with zero inclusive 
        public static bool IsWholeNumber(String strNumber)
        {
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strNumber);
        }

        // Function to Test for Integers both Positive & Negative 
        public static bool IsInteger(String strNumber)
        {
            Regex objNotIntPattern = new Regex("[^0-9-]");
            Regex objIntPattern = new Regex("^-[0-9]+$|^[0-9]+$");
            return !objNotIntPattern.IsMatch(strNumber) && objIntPattern.IsMatch(strNumber);
        }
        // Function to Test for Positive Number both Integer & Real 
        public static bool IsPositiveNumber(String strNumber)
        {
            Regex objNotPositivePattern = new Regex("[^0-9.]");
            Regex objPositivePattern = new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            return !objNotPositivePattern.IsMatch(strNumber) &&
            objPositivePattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber);
        }
        // Function to test whether the string is valid number or not
        public static bool IsNumber(String strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber) &&
            !objTwoMinusPattern.IsMatch(strNumber) &&
            objNumberPattern.IsMatch(strNumber);
        }
        // Function To test for Alphabets. 
        public static bool IsAlpha(String strToCheck)
        {
            Regex objAlphaPattern = new Regex("[^a-zA-Z]");
            return !objAlphaPattern.IsMatch(strToCheck);
        }
        // Function to Check for AlphaNumeric.
        public static bool IsAlphaNumeric(String strToCheck)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }

        #endregion

        #region EnDecrypt Password

        public enum SecurityAction
        {
            Encrypt = 0,
            Decrypt = 1
        }
        //encrypt and decrypt small strings using DES3
        public static string EnDecryptPassword(string UserPassword, SecurityAction Action)
        {
            string EncryptedPassword;
            string DecryptedPassword;
            string PassKey;

            MD5CryptoServiceProvider MD5Hash = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider DescPro = new TripleDESCryptoServiceProvider();


            byte[] ByteArray;
            byte[] PasswordHash;

            //'create a secret password. the password is used to encrypt
            //'and decrypt strings. Without the password, the encrypted
            //'string cannot be decrypted and is just garbage. You must
            //'use the same password to decrypt an encrypted string as the
            //'string was originally encrypted with.
            PassKey = "IREX";

            //'generate an MD5 hash from the password. 
            //'a hash is a one way encryption meaning once you generate
            //'the hash, you cant derive the password back from it.
            PasswordHash = MD5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(PassKey));
            MD5Hash = null;

            //'Create the password hash key .
            DescPro.Key = PasswordHash;

            //'the mode is the block cipher mode which is basically the
            //'details of how the encryption will work. There are several
            //'kinds of ciphers available in DES3 and they all have benefits
            //'and drawbacks. Here the Electronic Codebook cipher is used
            //'which means that a given bit of text is always encrypted
            //'exactly the same when the same password is used.
            DescPro.Mode = CipherMode.ECB;

            if (Action == SecurityAction.Encrypt)
            {

                //'----- encrypt an un-encrypted string ------------
                //'the UserPassword string, which needs encrypted, must be in byte
                //'array form to work with the des3 class. everything will because
                //'most encryption works at the byte level so you'll find that
                //'the class takes in byte arrays and returns byte arrays and
                //'you'll be converting those arrays to strings.
                ByteArray = ASCIIEncoding.ASCII.GetBytes(UserPassword);

                //'encrypt the byte buffer representation of the UserPassword string
                //'and base64 encode the encrypted string. the reason the encrypted
                //'bytes are being base64 encoded as a string is the encryption will
                //'have created some weird characters in there. Base64 encoding
                //'provides a platform independent view of the encrypted string 
                //'and can be sent as a plain text string to wherever.
                EncryptedPassword = Convert.ToBase64String(DescPro.CreateEncryptor().TransformFinalBlock(ByteArray, 0, ByteArray.Length)); ;
                DescPro = null;
                return EncryptedPassword;
            }
            else
            {
                //'----- decrypt an encrypted string ------------
                //'whenever you decrypt a string, you must do everything you
                //'did to encrypt the string, but in reverse order. To encrypt,
                //'first a normal string was des3 encrypted into a byte array
                //'and then base64 encoded for reliable transmission. So, to 
                //'decrypt this string, first the base64 encoded string must be 
                //'decoded so that just the encrypted byte array remains.
                ByteArray = Convert.FromBase64String(UserPassword);

                //'decrypt DES 3 encrypted byte buffer and return ASCII string
                DecryptedPassword = ASCIIEncoding.ASCII.GetString(DescPro.CreateDecryptor().TransformFinalBlock(ByteArray, 0, ByteArray.Length));
                DescPro = null;
                return DecryptedPassword;
            }




        }




        #endregion


        public static int GetQuarter(int m)
        {
            return ((m - 1) / 3) + 1;
        }

        public static void GetStartAndEndDateOfAWeekFromADate(DateTime Date, out DateTime WeekStartDate, out DateTime WeekEndDate)
        {
            CultureInfo myCI = new CultureInfo("en-US");
            System.Globalization.Calendar myCal = myCI.Calendar;

            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = DayOfWeek.Saturday;

            DateTime yearStart = DateTime.Now;

            int week = myCal.GetWeekOfYear(Date, myCWR, myFirstDOW);

            if (week == 1)
            {
                DateTime firstDayOftheWeek = new DateTime(Date.Year, 1, 1).AddDays((week * 7) - 7);
                if (firstDayOftheWeek.DayOfWeek != DayOfWeek.Saturday)
                {
                    int days = (int)Date.DayOfWeek;
                    days = days + 1;
                    Date = Date.Date.AddDays(-days);
                    week = week + 52;
                }
                yearStart = new DateTime(Date.Year, 1, 1);
                WeekStartDate = Date;
            }
            else
            {
                yearStart = new DateTime(Date.Year, 1, 1);
                if (yearStart.DayOfWeek != DayOfWeek.Saturday)
                {
                    int days = (int)yearStart.DayOfWeek;
                    days = days + 1;
                    yearStart = yearStart.Date.AddDays(-days);
                }
                WeekStartDate = yearStart.AddDays((week * 7) - 7);
            }

            WeekEndDate = WeekStartDate.AddDays(6);
        }


        public static int GetWeekNumberFromDate(DateTime Date)
        {
            CultureInfo myCI = new CultureInfo("en-US");
            System.Globalization.Calendar myCal = myCI.Calendar;

            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = DayOfWeek.Saturday;

            return myCal.GetWeekOfYear(Date, myCWR, myFirstDOW);
        }

        /// <summary>
        /// use this Function to enable and disable the pager of the gridview
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="gvPagerRow"></param>
        public static void SetPagerButtonStates(GridView gridView, GridViewRow gvPagerRow)
        {
            int pageIndex = gridView.PageIndex;
            int pageCount = gridView.PageCount;

            Button btnFirst = (Button)gvPagerRow.FindControl("btnFirst");
            Button btnPrevious = (Button)gvPagerRow.FindControl("btnPrevious");
            Button btnNext = (Button)gvPagerRow.FindControl("btnNext");
            Button btnLast = (Button)gvPagerRow.FindControl("btnLast");
            btnFirst.Enabled = btnPrevious.Enabled = (pageIndex != 0);
            btnNext.Enabled = btnLast.Enabled = (pageIndex < (pageCount - 1));
            DropDownList ddlPageSelector = (DropDownList)gvPagerRow.FindControl("ddlPageSelector");
            ddlPageSelector.Items.Clear();
            for (int i = 1; i <= gridView.PageCount; i++)
            {
                ddlPageSelector.Items.Add(i.ToString());
            }

            ddlPageSelector.SelectedIndex = pageIndex;
        }


        public static decimal GetDiscountPercentage(decimal TotalNumber, int Percentage)
        {
            TotalNumber = TotalNumber - ((TotalNumber * Percentage) / 100);
            return TotalNumber;
        }

        public static decimal GetTaxesPercentage(decimal TotalNumber, int Percentage)
        {
            TotalNumber = TotalNumber + ((TotalNumber * Percentage) / 100);
            return TotalNumber;
        }


        public static void ReadOnlyAttribute(ref TextBox ReadOnlyText)
        {
            //ReadOnlyText.Attributes.Add("contentEditable", "false");
            ReadOnlyText.Attributes.Add("readonly", "readonly");
        }

        public static byte[] ConvertStringToByte(string theString)
        {
            byte[] byteStream = new byte[0];
            if (string.IsNullOrEmpty(theString) == false)
            {
                try
                {
                    ASCIIEncoding encoder = new ASCIIEncoding();
                    byteStream = new byte[(uint)theString.Length];
                    encoder.GetBytes(theString, 0, theString.Length, byteStream, 0);
                    return byteStream;
                }
                catch (Exception)
                {
                }
            }
            return byteStream;
        }

        public static string ConvertByteToString(byte[] theBinaryData)
        {
            string strString = string.Empty;
            if (theBinaryData.Length > 0)
            {
                ASCIIEncoding encoder = new ASCIIEncoding();
                strString = encoder.GetString(theBinaryData);
            }
            return strString;
        }



        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IPrincipal CurrentUser
        {
            get { return HttpContext.Current.User; }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string CurrentUserIP
        {
            get { return HttpContext.Current.Request.UserHostAddress; }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string CurrentUserName
        {
            get
            {
                string userName = string.Empty;
                if (CurrentUser.Identity.IsAuthenticated)
                {
                    userName = CurrentUser.Identity.Name;
                }
                return userName;
            }
        }

        //public static Guid GetUserID(string UserName)
        //{
        //    MembershipUser MembershipUserID = Membership.GetUser(UserName);
        //    if (MembershipUserID != null)
        //    {
        //        return (Guid)MembershipUserID.ProviderUserKey;
        //    }
        //    else
        //    {
        //        Guid userId = new Guid(HttpContext.Current.Request.AnonymousID.Substring(0, 36));
        //        return Guid.Empty;
        //    }

        //}



        //public static Boolean IsUserExist(string UserName)
        //{
        //    try
        //    {
        //        MembershipUser MembershipUserCls = Membership.GetUser(UserName);
        //        if (MembershipUserCls != null)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //    return true;
        //}



        public static bool IsUserAuthenticated
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }





        public static string GetCurrentPageName()
        {


            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;


            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);


            string sRet = oInfo.Name;


            return sRet;


        }



        #region Directory IO
        /// <summary>
        /// </summary>
        /// <param name="vDirectoryPath"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static DirectoryInfo MakeDirectory(string vDirectoryPath)
        {
            if (Directory.Exists(vDirectoryPath))
            {
                return new DirectoryInfo(vDirectoryPath);
            }
            return Directory.CreateDirectory(vDirectoryPath);
        }

        public static string GetFileExtention(string FilePath)
        {


            string sPath = FilePath;


            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);


            string sRet = oInfo.Extension;


            return sRet;


        }



        public static void DeleteFile(string FilePath)
        {

            try
            {
                if (File.Exists(FilePath) == true)
                {
                    File.Delete(FilePath);
                }
            }
            catch (Exception)
            {

            }


        }

        #endregion


        public static string GetCheckBoxListSelected(ref CheckBoxList chkList)
        {
            string[] selected = chkList.Items.OfType<ListItem>().Where(x => x.Selected).Select(x => x.Value).ToArray();
            string result = String.Join(",", selected);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Lang">Arabic Ar_ , English En_ , French Fr_</param>
        /// <returns></returns>
        public static string GetCurrentPageByLang(string Lang)
        {
            try
            {
                string PageName = UtilityClass.GetCurrentPageName().ToLower();
                if ((PageName == "en_default.aspx" || PageName == "fr_default.aspx") && (Lang.ToLower() == "ar_"))
                {
                    return (PageName.Substring(3));
                }
                else if (PageName == "default.aspx")
                {
                    return (Lang + PageName);
                }

                return (Lang + PageName.Substring(3));
            }
            catch (Exception)
            {

            }
            return "default.aspx";
        }





        public static DateTime GetDateFormated(string strDate)
        {
            string requiredDate = string.Empty;
            try
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);
                requiredDate = (DateTime.Parse(strDate, culture, System.Globalization.DateTimeStyles.NoCurrentDateDefault)).ToString();
                return DateTime.Parse(requiredDate);
            }
            catch (Exception)
            {

            }
            return DateTime.MinValue;
        }


        public static string GetSelectedValues(ref CheckBoxList chkLst)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                for (int i = 0; i < chkLst.Items.Count; i++)
                {
                    if (chkLst.Items[i].Selected == true)
                    {
                        sb.Append(chkLst.Items[i].Value + ",");
                    }
                }
            }
            catch (Exception)
            {

            }
            return sb.ToString();
        }

        public static void SetValues(ref CheckBoxList chkLst, string strValues)
        {
            try
            {
                string[] Values = strValues.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < Values.Length; i++)
                {
                    for (int x = 0; x < chkLst.Items.Count; x++)
                    {
                        if (Values[i].ToString() == chkLst.Items[x].Value)
                        {
                            chkLst.Items[x].Selected = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }


        // This is used to flip the sorting arrow up/down
        // Base Css class is 'sort', the Ascending Css Class is 'up' and Descending is 'down'
        public static void GVSort(object sender, string SortExpression, SortDirection e)
        { // call on sort and sets the sorted field to the proper Css Class, while setting all others to the base class
            string BASE = "sort";
            string UP = "up";
            string DOWN = "down";
            GridView g = (GridView)sender;
            for (int i = 0; i < g.Columns.Count; i++)
            {
                //var c = g.Columns[i];
                g.Columns[i].HeaderStyle.CssClass = g.Columns[i].HeaderStyle.CssClass.Replace(UP, BASE).Replace(DOWN, BASE);
                if (g.Columns[i].SortExpression.Equals(SortExpression))
                {
                    g.Columns[i].HeaderStyle.CssClass =
                      e.Equals(System.Web.UI.WebControls.SortDirection.Ascending) ?
                        g.Columns[i].HeaderStyle.CssClass.Replace(BASE, UP).Replace(UP, DOWN) :
                        g.Columns[i].HeaderStyle.CssClass.Replace(BASE, DOWN).Replace(DOWN, UP);
                }
            }
        }




        public static string FullyQualifiedApplicationPath
        {
            get
            {
                //Return variable declaration
                string appPath = null;

                //Getting the current context of HTTP request
                HttpContext context = HttpContext.Current;

                //Checking the current context content
                if (context != null)
                {
                    //Formatting the fully qualified website url/name
                    appPath = string.Format("{0}://{1}{2}{3}",
                    context.Request.Url.Scheme,
                    context.Request.Url.Host,
                    context.Request.Url.Port == 80 ? string.Empty : ":" + context.Request.Url.Port, context.Request.ApplicationPath);
                }

                return appPath;
            }
        }


        /// <summary>
        /// This is a utlity function I used to wrap the Regex.Replace
        /// method. It just provides a shorthand way to do a replace.
        /// </summary>
        /// <param name="strin"></param>
        /// <param name="strExp"></param>
        /// <param name="strReplace"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string RegexReplace(string strin, string strExp, string strReplace)
        {
            return Regex.Replace(strin, strExp, strReplace);
        }

        public static string RemoveHTML(string strText)
        {
            return RegexReplace(strText, "<[^>]*>", string.Empty);
        }

        /// <summary>
        /// Cleans the whitespace, or unneeded characters for the
        /// page to redner in the browser.
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        /// <remarks>
        /// This make the page a little thinner by not including
        /// extra characters that do not make the page render,
        /// but rather help developers manage the markup in editors.</remarks>
        public static string RemoveWhiteSpace(string strText)
        {
            return RegexReplace(strText, @"(?<=>)\s+(?=</?)", string.Empty);
        }

        public static string ResolveUrl(string originalUrl)
        {
            if (originalUrl == null)
            {
                return null;
            }
            if (originalUrl.IndexOf("://") != -1)
            {
                return originalUrl;
            }
            if (!originalUrl.StartsWith("~"))
            {
                return originalUrl;
            }
            if (HttpContext.Current == null)
            {
                throw new ArgumentException("Invalid URL: Relative URL not allowed.");
            }
            return (HttpContext.Current.Request.ApplicationPath + originalUrl.Substring(1).Replace("//", "/"));
        }



        /// <summary>
        /// Returns an array with the names of all local Themes
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string[] GetThemes()
        {
            if ((HttpContext.Current.Cache["SiteThemes"] != null))
            {
                return (string[])HttpContext.Current.Cache["SiteThemes"];
            }

            string themesDirPath = HttpContext.Current.Server.MapPath("~/App_Themes");
            // get the array of themes folders under /App_Themes
            string[] themes = Directory.GetDirectories(themesDirPath);
            for (int i = 0; i <= themes.Length - 1; i++)
            {
                themes[i] = Path.GetFileName(themes[i]);
            }
            //var dep = new CacheDependency(themesDirPath);
            //HttpContext.Current.Cache.Insert("SiteThemes", themes, dep);
            return themes;
        }

        public static string GetURLPath(string vURL)
        {
            var _Regex = new Regex("://[^/]+/(?<path>[^?\\s<>#\"]+)");
            if (_Regex.Matches(vURL).Count > 0)
            {
                return _Regex.Match(vURL).Groups[1].ToString();
            }
            return vURL;
        }




        /// <summary>
        /// Adds the onfocus and onblur attributes to all input controls found in the specified parent,
        /// to change their apperance with the control has the focus.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="className"></param>
        /// <param name="onlyTextBoxes"></param>
        /// <remarks></remarks>
        public static void SetInputControlsHighlight(Control container, string className, bool onlyTextBoxes)
        {
            foreach (Control ctl in container.Controls)
            {
                if ((onlyTextBoxes && (ctl) is TextBox) || (ctl) is TextBox || (ctl) is DropDownList || (ctl) is ListBox ||
                    (ctl) is CheckBox || (ctl) is RadioButton || (ctl) is RadioButtonList || (ctl) is CheckBoxList)
                {
                    WebControl wctl = default(WebControl);
                    wctl = (WebControl)ctl;
                    wctl.Attributes.Add("onfocus", string.Format("this.className = '{0}';", className));
                    wctl.Attributes.Add("onblur", "this.className = '';");
                }
                else
                {
                    if ((ctl.Controls.Count > 0)) SetInputControlsHighlight(ctl, className, onlyTextBoxes);
                }
            }
        }


        public static List<T> MakeList<T>(T itemOftype)
        {
            List<T> newList = new List<T>();
            return newList;
        }


        public static string ExceptBlanks(string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                switch (c)
                {
                    case '\r':
                    case '\n':
                    case '\t':
                    case ' ':
                        continue;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }


        public static string GenerateRandomPassword(int Length) // Function for generate password
        {
            // We want all popular characters except for the ones that can easily be confused (e.g., l and 1)
            string[] characters = new string[56];
            characters[0] = "a";
            characters[1] = "b";
            characters[2] = "c";
            characters[3] = "d";
            characters[4] = "e";
            characters[5] = "f";
            characters[6] = "g";
            characters[7] = "h";
            characters[8] = "j";
            characters[9] = "k";
            characters[10] = "m";
            characters[11] = "n";
            characters[12] = "p";
            characters[13] = "q";
            characters[14] = "r";
            characters[15] = "s";
            characters[16] = "t";
            characters[17] = "u";
            characters[18] = "v";
            characters[19] = "w";
            characters[20] = "x";
            characters[21] = "y";
            characters[22] = "z";
            characters[23] = "A";
            characters[24] = "B";
            characters[25] = "C";
            characters[26] = "D";
            characters[27] = "E";
            characters[28] = "F";
            characters[29] = "G";
            characters[30] = "H";
            characters[31] = "J";
            characters[32] = "K";
            characters[33] = "L";
            characters[34] = "M";
            characters[35] = "N";
            characters[36] = "P";
            characters[37] = "Q";
            characters[38] = "R";
            characters[39] = "S";
            characters[40] = "T";
            characters[41] = "U";
            characters[42] = "V";
            characters[43] = "W";
            characters[44] = "X";
            characters[45] = "Y";
            characters[46] = "Z";
            characters[47] = "2";
            characters[48] = "3";
            characters[49] = "4";
            characters[50] = "5";
            characters[51] = "6";
            characters[52] = "7";
            characters[53] = "8";
            characters[54] = "9";

            Random randChar = new Random();
            string password = "";

            for (int i = 0; i < Length; i++)
            {
                password += characters[randChar.Next(0, 55)];
            }

            return password;
        }


        //public static void AddStyleSheet(string styleSheetUrl)
        //{
        //    HtmlLink link = new HtmlLink();
        //    link.Attributes.Add("type", "text/css");
        //    link.Attributes.Add("rel", "stylesheet");
        //    link.Attributes.Add("href", ResolveUrl(styleSheetUrl));
        //    this.Page.Header.Controls.Add(link);
        //}

        //public void AddJavascript(string javascriptUrl)
        //{
        //HtmlGenericControl script = new HtmlGenericControl("script");
        //script.Attributes.Add("type", "text/javascript");
        //script.Attributes.Add("src", ResolveUrl(javascriptUrl));
        //this.Page.Header.Controls.Add(script);
        //}

        //HtmlGenericControl body = (HtmlGenericControl)Master.FindControl("Body");
        //body.Attributes.Add("onload", "createMap()");
        //body.Attributes.Add("onunload", "GUnload()");





        //function WriteNumbersOnly()
        //           {
        //           if ((event.keyCode < 48) || (event.keyCode > 57))
        //                   event.returnValue = false;
        //           if (event.keyCode == 13)
        //                   event.returnValue = true;
        //           if (event.keyCode == 46)
        //                   event.returnValue = true;
        //           }





        // ***************************************** Start Using Sharp Zip Lib *************************************

        //using System;
        //using System.IO;
        //using System.Collections.Generic;
        //using ICSharpCode.SharpZipLib.Checksums;
        //using ICSharpCode.SharpZipLib.Zip;

        ///// <summary>
        ///// Writes the zip file.
        ///// </summary>
        ///// <param name="filesToZip">The files to zip.</param>
        ///// <param name="path">The destination path.</param>
        ///// <param name="compression">The compression level.</param>
        //private static void WriteZipFile(List<string> filesToZip, string path, int compression)
        //{
        //    if (compression < 0 || compression > 9)
        //        throw new ArgumentException("Invalid compression rate.");

        //    if (!Directory.Exists(new FileInfo(path).Directory.ToString()))
        //        throw new ArgumentException("The Path does not exist.");

        //    foreach (string c in filesToZip)
        //        if (!File.Exists(c))
        //            throw new ArgumentException(string.Format("The File{0}does not exist!", c));


        //    Crc32 crc32 = new Crc32();
        //    ZipOutputStream stream = new ZipOutputStream(File.Create(path));
        //    stream.SetLevel(compression);

        //    for (int i = 0; i < filesToZip.Count; i++)
        //    {
        //        ZipEntry entry = new ZipEntry(Path.GetFileName(filesToZip[i]));
        //        entry.DateTime = DateTime.Now;

        //        using (FileStream fs = File.OpenRead(filesToZip[i]))
        //        {
        //            byte[] buffer = new byte[fs.Length];
        //            fs.Read(buffer, 0, buffer.Length);
        //            entry.Size = fs.Length;
        //            fs.Close();
        //            crc32.Reset();
        //            crc32.Update(buffer);
        //            entry.Crc = crc32.Value;
        //            stream.PutNextEntry(entry);
        //            stream.Write(buffer, 0, buffer.Length);
        //        }
        //    }
        //    stream.Finish();
        //    stream.Close();
        //}
        // ***************************************** End Using Sharp Zip Lib *************************************




        // ***************************************** Start Gets the whois information *************************************

        ///// <summary>
        ///// Gets the whois information.
        ///// </summary>
        ///// <param name="whoisServer">The whois server.</param>
        ///// <param name="url">The URL.</param>
        ///// <returns></returns>
        //private string GetWhoisInformation(string whoisServer, string url)
        //{
        //    StringBuilder stringBuilderResult = new StringBuilder();
        //    TcpClient tcpClinetWhois = new TcpClient(whoisServer, 43);
        //    NetworkStream networkStreamWhois = tcpClinetWhois.GetStream();
        //    BufferedStream bufferedStreamWhois = new BufferedStream(networkStreamWhois);
        //    StreamWriter streamWriter = new StreamWriter(bufferedStreamWhois);

        //    streamWriter.WriteLine(url);
        //    streamWriter.Flush();

        //    StreamReader streamReaderReceive = new StreamReader(bufferedStreamWhois);

        //    while (!streamReaderReceive.EndOfStream)
        //        stringBuilderResult.AppendLine(streamReaderReceive.ReadLine());

        //    return stringBuilderResult.ToString();
        //}
        // ***************************************** End Gets the whois information *************************************




        // reset the identity column for database
        //DBCC CHECKIDENT ('tbl_Body', RESEED, 0)



        //1: /*Disable Constraints & Triggers*/
        //2: exec sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'
        //3: exec sp_MSforeachtable 'ALTER TABLE ? DISABLE TRIGGER ALL'
        //4:
        //5: /*Perform delete operation on all table for cleanup*/
        //6: exec sp_MSforeachtable 'DELETE ?'
        //7:&nbsp;&nbsp;&nbsp;&nbsp;
        //8: /*Enable Constraints & Triggers again*/
        //9: exec sp_MSforeachtable 'ALTER TABLE ? CHECK CONSTRAINT ALL'
        //10: exec sp_MSforeachtable 'ALTER TABLE ? ENABLE TRIGGER ALL'
        //11:&nbsp;&nbsp;&nbsp;
        //12: /*Reset Identity on tables with identity column*/
        //13: exec sp_MSforeachtable 'IF OBJECTPROPERTY(OBJECT_ID(''?''), ''TableHasIdentity'') = 1 BEGIN DBCC CHECKIDENT (''?'',RESEED,0) END'



    }
}